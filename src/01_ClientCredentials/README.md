# Client Credentials

This first quickstart covers the most basic scenario: **Protecting APIs for server-to-server communication**

The [Client](./src/Client) will request an access token from [IdentityServer](./src/IdentityServer) using its client ID and secret and then use the token to gain access to the [Protected API](./src/ProtectedAPI).

## Actors

- **[Client](./src/Client)**: The application that wants to access the Protected API
- **[IdentityServer](./src/IdentityServer)**: The authorization server
- **[Protected API](./src/ProtectedAPI)**: The resource server

## Flow

To test the flow, start the IdentityServer and Protected API projects. Once they are both running, run the Client project.

1. The client requests an access token from IdentityServer
1. IdentityServer validates the client and issues an access token
1. The client uses the access token to access the Protected API
1. The Protected API validates the access token and responds with the requested data
1. The client processes the data
