# Othello Game API Documentation

Welcome to the Othello Game API. This document provides instructions on how to test the API endpoints using Swagger UI. Our API supports functionalities such as user authentication, game management, chat during a game, and more.

## Getting Started

Before testing the API endpoints that require authentication, you need to log in and obtain a JWT (JSON Web Token). Follow these steps to authenticate and use the token for subsequent requests.

### Step 1: Login to Obtain JWT

1. Open Swagger UI by navigating to `http://localhost:<port>/swagger` in your web browser.
2. Click on the /api/auth/register endpoint.
3. Try out endpoint
   - Comlete registration by entiring all the fields required
5. Click on the `/api/auth/login` endpoint.
6. Try out the endpoint:
    - Enter your username and password in the request body.
    - Execute the request.
7. If the credentials are correct, the response will include a JWT. Copy this token.

### Step 2: Authorize Your Session

1. In the Swagger UI, locate and click the `Authorize` button at the top of the page.
2. In the authorization modal that appears, enter `Bearer <your_token>` in the value field, replacing `<your_token>` with the JWT you copied from the login response.
3. Click the `Authorize` button in the modal to apply the token to your requests.

### Step 3: Test Endpoints Requiring Authorization

Now that you are authorized, you can test endpoints that require authentication:

- **Game Management**
  - `/api/games/new`: Start a new game.
  - `/api/games/join`: Join an existing game session.
  - `/api/games/{gameId}/move`: Make a move in an active game.
  - `/api/games/waiting`: Retrieves all waiting game sessions.
  - `/api/games/{gameId}/undo`: Undo last move within 3 time seconds. 
  - `/api/games/{gameId}/hint`: Shows hints for user.
    
- **Chat Functionality**
  - `/api/chat/{gameId}/send`: Send a chat message in a game.
  - `/api/games/{gameId}/get`: Retrieve chat messages from a game session.

- **User Management**
  - `/api/users/{username}/stats`: Retrieve statistics for a specific user.

Each of these endpoints may require specific parameters or request bodies, so refer to the detailed documentation for each endpoint in the Swagger UI.

## Important Notes

- Ensure that you include the `Bearer` prefix followed by a space before your token when authorizing your session.
- If your token expires or if you receive a `401 Unauthorized` status, re-login to obtain a new JWT.
- Keep your JWT secure and do not expose it in untrusted environments.

## Troubleshooting

If you encounter issues with obtaining a JWT or with authorization:
- Check your username and password.
- Ensure there are no extra spaces or typographical errors in the JWT when you enter it in the authorization modal.
- If problems persist, check the server logs or contact the system administrator for assistance.

Thank you for using the Othello Game API!
