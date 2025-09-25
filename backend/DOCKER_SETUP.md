# Docker Setup Guide

This guide explains how to set up the Twitter Clone backend with PostgreSQL using Docker Compose.

## Prerequisites

- Docker and Docker Compose installed
- .NET 9 SDK (for development)

## Quick Start

### Development Environment (with Hot Reload)

1. **Copy environment configuration:**
   ```bash
   cp .env.example .env
   ```

2. **Run the setup script:**
   ```bash
   ./setup-docker.sh
   ```

   Or manually:
   ```bash
   # Build and start containers
   docker-compose -f docker-compose.dev.yml up --build -d

   # Run database migrations
   docker-compose -f docker-compose.dev.yml exec backend dotnet ef database update
   ```

3. **Access the application:**
   - Backend API: http://localhost:8080
   - Swagger UI: http://localhost:8080/swagger
   - Database: localhost:5432

### Production Environment

```bash
# Build and start production containers
docker-compose up --build -d
```

## Configuration

### Environment Variables

| Variable | Description | Default |
|----------|-------------|---------|
| `POSTGRES_DB` | Database name | `twitter_clone` |
| `POSTGRES_USER` | Database username | `twitter_user` |
| `POSTGRES_PASSWORD` | Database password | `twitter_pass_123` |
| `ASPNETCORE_ENVIRONMENT` | Environment | `Development` |
| `ASPNETCORE_URLS` | Application URLs | `http://0.0.0.0:8080` |

### Database Connection

The connection string is automatically configured in Docker Compose:
```
Host=db;Database=twitter_clone;Username=twitter_user;Password=twitter_pass_123
```

## Services

### PostgreSQL (`db`)
- **Image:** postgres:16-alpine
- **Port:** 5432
- **Volume:** postgres_data (persistent storage)
- **Health check:** Monitors database availability

### Backend API (`backend`)
- **Build:** Multi-stage Docker build
- **Port:** 8080 (HTTP), 8081 (gRPC)
- **Environment:** Development/Production
- **Health check:** Monitors API availability

## Development Workflow

### Hot Reload (Development)
```bash
# Start development environment with hot reload
docker-compose -f docker-compose.dev.yml up

# View logs
docker-compose -f docker-compose.dev.yml logs -f backend

# Stop containers
docker-compose -f docker-compose.dev.yml down
```

### Manual Database Operations
```bash
# Access database directly
docker-compose exec db psql -U twitter_user -d twitter_clone

# Run migrations manually
docker-compose exec backend dotnet ef database update

# Create new migration
docker-compose exec backend dotnet ef migrations add MigrationName
```

### Rebuilding Containers
```bash
# Rebuild backend only
docker-compose build backend

# Rebuild all services
docker-compose build
```

## Volume Management

### Database Data
- **Volume:** `postgres_data`
- **Location:** Managed by Docker
- **Persistence:** Survives container restarts

### Backup and Restore
```bash
# Create backup
docker-compose exec db pg_dump -U twitter_user twitter_clone > backup.sql

# Restore backup
docker-compose exec -i db psql -U twitter_user twitter_clone < backup.sql
```

## Troubleshooting

### Common Issues

1. **Port conflicts:**
   - Check if ports 5432 or 8080 are already in use
   - Modify ports in docker-compose.yml if needed

2. **Database connection errors:**
   - Ensure PostgreSQL container is healthy
   - Check environment variables in .env file
   - Verify database credentials

3. **Migration errors:**
   - Ensure backend container can connect to database
   - Check connection string format
   - Run migrations manually

### Health Checks

```bash
# Check container status
docker-compose ps

# Check database health
docker-compose exec db pg_isready -U twitter_user -d twitter_clone

# Check API health
curl http://localhost:8080/health
```

### Logs and Debugging

```bash
# View all logs
docker-compose logs

# View specific service logs
docker-compose logs db
docker-compose logs backend

# View real-time logs
docker-compose logs -f backend
```

## Security Notes

⚠️ **Production Security:**

1. **Database Credentials:**
   - Change default passwords in production
   - Use Docker secrets or environment files
   - Never commit secrets to version control

2. **Network Security:**
   - Use internal networks for service communication
   - Restrict database port exposure in production
   - Consider using reverse proxy with HTTPS

3. **Environment Variables:**
   - Store sensitive data in environment files
   - Use .dockerignore to exclude sensitive files
   - Rotate secrets regularly

## Cleanup

```bash
# Stop and remove containers
docker-compose down

# Remove containers, networks, and volumes
docker-compose down -v

# Remove all unused Docker objects
docker system prune -a
```