#!/bin/bash

set -e

echo "Setting up Twitter Clone with Docker..."

# Copy environment file if it doesn't exist
if [ ! -f .env ]; then
    echo "Creating .env file from template..."
    cp .env.example .env
    echo "Please review and update .env file with your configuration"
fi

# Build and start development environment
echo "Building and starting development environment..."
docker compose -f docker-compose.dev.yml up --build -d

# Wait for database to be ready
echo "Waiting for database to be ready..."
MAX_RETRIES=30
RETRY_COUNT=0

while [ $RETRY_COUNT -lt $MAX_RETRIES ]; do
    if docker compose -f docker-compose.dev.yml exec -T db pg_isready -U twitter_user -d twitter_clone > /dev/null 2>&1; then
        echo "Database is ready!"
        break
    fi

    RETRY_COUNT=$((RETRY_COUNT + 1))
    echo "Waiting for database... (attempt $RETRY_COUNT/$MAX_RETRIES)"
    sleep 2
done

if [ $RETRY_COUNT -eq $MAX_RETRIES ]; then
    echo "Database failed to start after $MAX_RETRIES attempts"
    echo "Checking database logs:"
    docker compose -f docker-compose.dev.yml logs db
    exit 1
fi

# Run database migrations
echo "Running database migrations..."
if ! docker compose -f docker-compose.dev.yml exec backend dotnet ef database update; then
    echo "Database migrations failed"
    echo "Checking backend logs:"
    docker compose -f docker-compose.dev.yml logs backend
    exit 1
fi

echo "Setup complete!"
echo "Backend API: http://localhost:8080"
echo "Swagger UI: http://localhost:8080/swagger"
echo "Database: localhost:5432"
echo ""
echo "To stop: docker compose -f docker-compose.dev.yml down"
echo "To view logs: docker compose -f docker-compose.dev.yml logs -f"