# Twitter Clone Implementation Steps

This document breaks down the implementation of the Twitter clone project into small, self-contained, and ordered steps.  
It integrates the high-level plan with concrete development steps for clarity and iterative delivery.

---

## 1. Project Setup

-   [x] **Step 1.1: Initialize Git Repository**
    -   Create a new Git repository for the project.
    -   Create a `README.md` file with a brief project description.

-   [x] **Step 1.2: Create Project Structure**
    -   Create the root directory for the project.
    -   Inside the root directory, create the following subdirectories:
        -   `backend`: For the .NET API project.
        -   `frontend`: For the React frontend project.
        -   `docs`: For project documentation.
    -   Add `.gitignore` for .NET, Node, and Docker.

---

## 2. Backend Development (.NET 9)

### 2.1 Project Initialization

-   [x] **Step 2.1: Initialize .NET Web API Project**
    -   Inside the `backend` directory, create a new .NET 9 Web API project.
    -   Choose the "API" template with controllers (or minimal APIs).
    -   Enable Docker support (optional, but recommended).
    -   Add base packages for configuration, logging, and Swagger.

### 2.2 Database Setup

-   [ ] **Step 2.2: Setup Database with Entity Framework Core**
    -   Add NuGet packages:
        -   `Microsoft.EntityFrameworkCore`
        -   `Npgsql.EntityFrameworkCore.PostgreSQL`
        -   `Microsoft.AspNetCore.Identity.EntityFrameworkCore`
    -   Define data models:
        -   `User` (Id, Username, Email, PasswordHash, DisplayName, Bio, AvatarUrl, CreatedAt, UpdatedAt).
        -   `Post` (Id, AuthorId, Content, ReplyToPostId, MediaUrls, CreatedAt, UpdatedAt).
        -   `Follow` (Id, FollowerId, FollowedId, CreatedAt).
        -   `Like` (Id, UserId, PostId, CreatedAt).
        -   `RefreshToken` (Id, UserId, TokenHash, ExpiresAt, RevokedAt).
    -   Configure `AppDbContext` with relationships and constraints.
    -   Create initial migration and apply with `dotnet ef database update`.
    -   (Optional) Add a seeding step with a test user and sample posts.

### 2.3 Authentication & Security

-   [ ] **Step 2.3: Implement User Authentication**
    -   Use ASP.NET Core Identity with EF Core.
    -   Implement endpoints:
        -   `POST /api/auth/register` — create new user (hash password).
        -   `POST /api/auth/login` — verify credentials, return JWT + refresh token.
        -   `POST /api/auth/refresh` — issue new JWT from refresh token. use HttpOnly, Secure cookie for refresh tokens
        -   `POST /api/auth/logout` — revoke refresh token.
    -   Security notes:
        -   Use HttpOnly + Secure cookies for refresh tokens.
        -   Store refresh tokens hashed in DB.
        -   Short TTL for JWTs (e.g., 15m).
        -   Rate-limit login attempts.

### 2.4 User Profile

-   [ ] **Step 2.4: Implement User Profile Management**
    -   `GET /api/users/{username}` — fetch public profile.
    -   `PUT /api/users/me` — update profile (bio, display name, avatar).
    -   Enforce authentication for self-updates.

### 2.5 Follow System

-   [ ] **Step 2.5: Implement Follow System**
    -   `POST /api/users/{username}/follow` — follow user.
    -   `POST /api/users/{username}/unfollow` — unfollow user.
    -   Store relationships in `Follow` table.

### 2.6 Posts & Timeline

-   [ ] **Step 2.6: Implement Post Management**
    -   `POST /api/posts` — create post (text + optional media).
    -   `GET /api/posts/{id}` — get single post.
    -   `GET /api/posts?author=username&cursor=...&limit=20` — list author’s posts (paginated).
    -   `GET /api/timeline?cursor=...&limit=20` — fetch timeline (own + followed users).
    -   Add configurable character limit (e.g., 280 chars).

### 2.7 Likes & Replies

-   [ ] **Step 2.7: Implement Likes and Replies**
    -   `POST /api/posts/{id}/like` — like/unlike a post (toggle).
    -   `POST /api/posts/{id}/reply` — reply to a post (threaded, one-level or nested).

### 2.8 Search

-   [ ] **Step 2.8: Implement Basic Search**
    -   Search for users by username.
    -   Search for posts by text content.
    -   Simple SQL `LIKE` queries initially; Postgres full-text later (nice-to-have).

### 2.9 Media Uploads

-   [ ] **Step 2.9: Implement Media Uploads**
    -   `POST /api/media/request-upload` — return signed URL (MinIO/S3) or handle multipart upload (local storage).
    -   Save URLs/paths in `Post.MediaUrls` JSONB field.
    -   Storage options:
        -   Local: `/data/uploads`.
        -   MinIO (S3-compatible) with Docker Compose.
    -   Validate file size/type (e.g., max 5–10MB, `image/jpeg/png`).

---

## 3. Frontend Development (React + Vite + TS)

### 3.1 Project Setup

-   [ ] **Step 3.1: Initialize React Project**
    -   Inside `frontend`, run `npm create vite@latest` with TypeScript template.
    -   Install dependencies: `react-router-dom`, `axios`, `zustand` (optional), `tailwindcss`.

### 3.2 Structure & Routing

-   [ ] **Step 3.2: Setup Project Structure and Routing**
    -   Folder structure:
        -   `src/api` — API client.
        -   `src/components` — reusable UI.
        -   `src/pages` — routed pages.
        -   `src/hooks` — custom hooks (`useAuth`, `useTimeline`).
        -   `src/state` — context or Zustand store.
    -   Configure routing with `react-router-dom`.

### 3.3 Authentication Pages

-   [ ] **Step 3.3: Implement Authentication Pages**
    -   `LoginPage` + `RegisterPage` with forms.
    -   Call backend `/auth` endpoints.
    -   Implement `useAuth` hook to track auth state, JWT refresh.

### 3.4 Core UI Components

-   [ ] **Step 3.4: Implement Core UI Components**
    -   `Button`, `Avatar`, `PostCard`, `PostComposer`, `ProfileHeader`.
    -   TailwindCSS for styling.

### 3.5 Main Pages

-   [ ] **Step 3.5: Implement Main Pages**
    -   `HomePage` — user’s timeline.
    -   `ProfilePage` — user profile with posts.
    -   `PostPage` — single post + replies.

### 3.6 State Management

-   [ ] **Step 3.6: Implement State Management**
    -   Start with Context + hooks.
    -   Consider Zustand or Redux for advanced state.

### 3.7 Backend Integration

-   [ ] **Step 3.7: Connect Frontend to Backend**
    -   API client wrapper for fetch/axios.
    -   Handle JWT refresh automatically on 401.
    -   Proxy API requests in Vite dev server.

---

## 4. Dockerization

-   [ ] **Step 4.1: Dockerize the Backend**
    -   Multi-stage Dockerfile: build (SDK) → runtime (ASP.NET Core).
    -   Expose port 5000.
    -   Include migrations in container entrypoint.

-   [ ] **Step 4.2: Dockerize the Frontend**
    -   Multi-stage Dockerfile: build with Node → serve with nginx.
    -   Expose port 80.

---

## 5. Deployment

-   [ ] **Step 5.1: Create Docker Compose Configuration**
    -   `docker-compose.yml` with services:
        -   `db` (Postgres).
        -   `backend` (.NET API).
        -   `frontend` (React/nginx).
        -   `minio` (optional).
        -   `nginx` (reverse proxy).
    -   Use volumes for Postgres + media storage.

-   [ ] **Step 5.2: Local Development Setup**
    -   Create `docker-compose.dev.yml` for hot reloading.
    -   Mount source into containers (`dotnet watch`, Vite dev server).
    -   Document `cp .env.example .env` setup.

-   [ ] **Step 5.3: VPS Deployment**
    -   Deploy to a single VPS.
    -   Run `docker compose up -d`.
    -   Use nginx or Caddy as reverse proxy.
        -   Caddy can auto-issue TLS certs.
        -   nginx requires manual LetsEncrypt setup.
    -   Expose only ports 80/443.

---

## 6. Phase 2 (Nice-to-Have Features)

-   Real-time updates with WebSockets/SignalR.
-   Pagination & cursor-based APIs.
-   Full-text search with Postgres `tsvector`.
-   Image resizing & avatar cropping.
-   Rate limiting.
-   Email verification & password reset.
-   Admin panel for moderation.
-   Observability (Prometheus, Grafana, structured logs).

---
