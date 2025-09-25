# Twitter Clone Implementation Steps

This document breaks down the implementation of the Twitter clone project into small, self-contained, and ordered steps.

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

## 2. Backend Development (.NET 9)

-   [ ] **Step 2.1: Initialize .NET Web API Project**
    -   Inside the `backend` directory, create a new .NET 9 Web API project.
    -   Choose the "API" template with controllers.
    -   Enable Docker support (optional, but recommended).

-   [ ] **Step 2.2: Setup Database with Entity Framework Core**
    -   Add the necessary NuGet packages for EF Core and the PostgreSQL provider (`Npgsql.EntityFrameworkCore.PostgreSQL`).
    -   Define the data models (`User`, `Post`, `Follow`, `Like`, `RefreshToken`) in the project.
    -   Configure the `DbContext` and the database connection string.
    -   Create the initial database migration and apply it to the database.

-   [ ] **Step 2.3: Implement User Authentication**
    -   Set up ASP.NET Core Identity for user management.
    -   Implement the `POST /api/auth/register` endpoint for user registration.
    -   Implement the `POST /api/auth/login` endpoint for user login, returning a JWT access token and a refresh token.
    -   Implement the `POST /api/auth/refresh` endpoint to get a new access token using a refresh token.
    -   Implement the `POST /api/auth/logout` endpoint to revoke refresh tokens.

-   [ ] **Step 2.4: Implement User Profile Management**
    -   Implement the `GET /api/users/{username}` endpoint to get a user's public profile.
    -   Implement the `PUT /api/users/me` endpoint to update the authenticated user's profile.

-   [ ] **Step 2.5: Implement Follow System**
    -   Implement the `POST /api/users/{username}/follow` endpoint to follow a user.
    -   Implement the `POST /api/users/{username}/unfollow` endpoint to unfollow a user.

-   [ ] **Step 2.6: Implement Post Management**
    -   Implement the `POST /api/posts` endpoint to create a new post.
    -   Implement the `GET /api/posts/{id}` endpoint to get a single post.
    -   Implement the `GET /api/posts` endpoint to list posts by author.
    -   Implement the `GET /api/timeline` endpoint to get the user's timeline.

-   [ ] **Step 2.7: Implement Likes and Replies**
    -   Implement the `POST /api/posts/{id}/like` endpoint to like/unlike a post.
    -   Implement the `POST /api/posts/{id}/reply` endpoint to reply to a post.

-   [ ] **Step 2.8: Implement Basic Search**
    -   Implement a basic search functionality to search for users by username and posts by text content.

-   [ ] **Step 2.9: Implement Media Uploads**
    -   Implement the `POST /api/media/request-upload` endpoint to handle media uploads.
    -   Choose a storage option (local storage or MinIO) and configure it.

## 3. Frontend Development (React)**

-   [ ] **Step 3.1: Initialize React Project**
    -   Inside the `frontend` directory, create a new React project using Vite.
    -   Choose the TypeScript template.

-   [ ] **Step 3.2: Setup Project Structure and Routing**
    -   Organize the project into `pages`, `components`, `hooks`, `api`, and `state` directories.
    -   Set up client-side routing using a library like `react-router-dom`.

-   [ ] **Step 3.3: Implement Authentication Pages**
    -   Create the `LoginPage` and `RegisterPage` components.
    -   Implement the authentication forms and API calls to the backend.
    -   Implement a `useAuth` hook for managing authentication state.

-   [ ] **Step 3.4: Implement Core UI Components**
    -   Create reusable UI components like `Button`, `Avatar`, `PostCard`, and `PostComposer`.

-   [ ] **Step 3.5: Implement Main Pages**
    -   Create the `HomePage` to display the user's timeline.
    -   Create the `ProfilePage` to display user profiles.
    -   Create the `PostPage` to display a single post with replies.

-   [ ] **Step 3.6: Implement State Management**
    -   Use React Context and hooks for simple state management.
    -   Consider using a library like Zustand or Redux for more complex state.

-   [ ] **Step 3.7: Connect Frontend to Backend**
    -   Create an API client to handle communication with the backend API.
    -   Implement token handling and automatic token refresh.

## 4. Dockerization**

-   [ ] **Step 4.1: Dockerize the Backend**
    -   Create a `Dockerfile` for the .NET API.
    -   Ensure the Dockerfile is optimized for build and runtime performance.

-   [ ] **Step 4.2: Dockerize the Frontend**
    -   Create a `Dockerfile` for the React frontend.
    -   Use a multi-stage build to create a small production image.

## 5. Deployment**

-   [ ] **Step 5.1: Create Docker Compose Configuration**
    -   Create a `docker-compose.yml` file to orchestrate the `backend`, `frontend`, `postgres`, and an optional `minio` service.
    -   Configure environment variables and volumes.

-   [ ] **Step 5.2: Local Development Setup**
    -   Create a `docker-compose.dev.yml` for local development with features like hot-reloading.
    -   Provide instructions on how to set up and run the project locally.

-   [ ] **Step 5.3: VPS Deployment**
    -   Provide instructions on how to deploy the application to a single VPS.
    -   Include instructions for setting up a reverse proxy (nginx or Caddy) for SSL termination.
