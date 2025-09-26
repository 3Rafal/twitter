# Twitter Clone Implementation Steps

This document breaks down the implementation of the Twitter clone project into small, self-contained, and ordered feature slices. Each step represents a full-stack feature, from database to UI, to ensure iterative and concurrent development.

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
    -   Add `.gitignore` for .NET, Node, and Docker.

-   [x] **Step 1.3: Initialize Backend and Frontend Projects**
    -   **Backend**: Inside the `backend` directory, create a new .NET 9 Web API project.
    -   **Frontend**: Inside `frontend`, run `npm create vite@latest` with a TypeScript template.

---

## 2. Feature Slice: User Authentication and Profiles

-   [x] **Step 2.1: Backend - Database and User Model**
    -   Add EF Core NuGet packages (`Microsoft.EntityFrameworkCore`, `Npgsql.EntityFrameworkCore.PostgreSQL`, `Microsoft.AspNetCore.Identity.EntityFrameworkCore`).
    -   Define `User` and `RefreshToken` data models.
    -   Configure `AppDbContext` and create the initial database migration.

-   [x] **Step 2.2: Backend - Authentication Endpoints**
    -   Implement ASP.NET Core Identity.
    -   Create endpoints for user registration (`/api/auth/register`), login (`/api/auth/login`), token refresh (`/api/auth/refresh`), and logout (`/api/auth/logout`).
    -   Use HttpOnly cookies for refresh tokens and short-lived JWTs.

-   [ ] **Step 2.3: Frontend - Project Structure and Routing**
    -   Install dependencies: `react-router-dom`, `axios`, `zustand` (optional), `tailwindcss`.
    -   Set up folder structure (`src/api`, `src/components`, `src/pages`, `src/hooks`, `src/state`).
    -   Configure basic routing.

-   [ ] **Step 2.4: Frontend - Authentication UI & Logic**
    -   Create `LoginPage` and `RegisterPage` with forms.
    -   Implement `useAuth` hook to manage authentication state and interact with the backend.
    -   Connect forms to the backend authentication endpoints.

-   [ ] **Step 2.5: Backend - User Profile Management**
    -   Create endpoints to get a user's public profile (`GET /api/users/{username}`) and update the current user's profile (`PUT /api/users/me`).

-   [ ] **Step 2.6: Frontend - User Profile Page**
    -   Create a `ProfilePage` to display user information (username, bio, etc.).
    -   Allow users to edit their profile information.

---

## 3. Feature Slice: Post Creation and Timeline

-   [ ] **Step 3.1: Backend - Post Model and Endpoints**
    -   Define `Post` data model and update `AppDbContext`.
    -   Create a new database migration.
    -   Implement endpoints to create a new post (`POST /api/posts`), get a single post (`GET /api/posts/{id}`), and get a user's posts (`GET /api/posts?author=username`).

-   [ ] **Step 3.2: Frontend - Post Components**
    -   Create a `PostCard` component to display a single post.
    -   Create a `PostComposer` component for writing new posts.

-   [ ] **Step 3.3: Backend - Timeline Endpoint**
    -   Implement the main timeline endpoint (`GET /api/timeline`) to fetch posts from the user and people they follow.

-   [ ] **Step 3.4: Frontend - Home Page and Timeline**
    -   Create a `HomePage` to display the user's timeline.
    -   Integrate the `PostComposer` and `PostCard` components.
    -   Fetch and display timeline data from the backend.

---

## 4. Feature Slice: Follow System

-   [ ] **Step 4.1: Backend - Follow Model and Endpoints**
    -   Define `Follow` data model and update `AppDbContext`.
    -   Create a new database migration.
    -   Implement endpoints to follow (`POST /api/users/{username}/follow`) and unfollow (`POST /api/users/{username}/unfollow`) a user.

-   [ ] **Step 4.2: Frontend - Follow/Unfollow Buttons**
    -   Add "Follow" and "Unfollow" buttons to the `ProfilePage`.
    -   Connect the buttons to the corresponding backend endpoints.
    -   Update the UI to reflect the current follow status.

---

## 5. Feature Slice: Likes and Replies

-   [ ] **Step 5.1: Backend - Like and Reply Models and Endpoints**
    -   Define `Like` model and update `AppDbContext`.
    -   Create a new database migration.
    -   Implement endpoints to like/unlike a post (`POST /api/posts/{id}/like`) and reply to a post (`POST /api/posts/{id}/reply`).

-   [ ] **Step 5.2: Frontend - Like and Reply Functionality**
    -   Add a "Like" button to the `PostCard` component and update the UI to show the like count.
    -   Create a `Reply` component and integrate it into the `PostPage` to display and create replies.

---

## 6. Feature Slice: Search

-   [ ] **Step 6.1: Backend - Basic Search Endpoints**
    -   Implement endpoints to search for users by username and posts by text content.
    -   Use simple SQL `LIKE` queries for the initial implementation.

-   [ ] **Step 6.2: Frontend - Search UI**
    -   Add a search bar to the application.
    -   Create a `SearchPage` to display search results for users and posts.

---

## 7. Feature Slice: Media Uploads

-   [ ] **Step 7.1: Backend - Media Upload Endpoint**
    -   Implement an endpoint for media uploads (`POST /api/media/request-upload`).
    -   Choose a storage option (local storage or a service like MinIO/S3).
    -   Validate file size and type.

-   [ ] **Step 7.2: Frontend - Media Upload UI**
    -   Add functionality to the `PostComposer` to allow users to attach images or videos to their posts.
    -   Display uploaded media in the `PostCard` component.

---

## 8. Dockerization and Deployment

-   [ ] **Step 8.1: Dockerize Backend and Frontend**
    -   Create multi-stage Dockerfiles for both the .NET backend and the React frontend.

-   [ ] **Step 8.2: Create Docker Compose Configuration**
    -   Create a `docker-compose.yml` file to define services for the database, backend, and frontend.
    -   Set up a `docker-compose.dev.yml` for a hot-reloading development environment.

-   [ ] **Step 8.3: Deploy the Application**
    -   Document the steps for deploying the application to a VPS using Docker Compose.
    -   Configure a reverse proxy (like nginx or Caddy) to handle incoming traffic and SSL.

---

## 9. Phase 2 (Nice-to-Have Features)

-   Real-time updates with WebSockets/SignalR.
-   Pagination & cursor-based APIs.
-   Full-text search with Postgres `tsvector`.
-   Image resizing & avatar cropping.
-   Rate limiting.
-   Email verification & password reset.
-   Admin panel for moderation.
-   Observability (Prometheus, Grafana, structured logs).

---