# Messaging Service API

## Used Technologies

- .NET Core 3.1
- MongoDb
- Nginx
- Docker

## Installation

- Run `docker-compose up -d` command in your terminal and navigate to `http://localhost`

## Documentions

Navigate to `http://localhost/help/index.html` address for endpoint documentations.

### Endpoints

#### Register User

Send a POST HTTP request to `http://localhost/register` endpoint.

```http
POST /auth/register HTTP/1.1
Content-Type: application/json
Host: localhost

{"Name":"<SAMPLE_USERNAME>","Email":"<SAMPLE_EMAIL>","Password":"<SAMPLE_PASSWORD>"}
```

#### Login User

Send a POST HTTP request to `http://localhost/login` endpoint.

```http
POST /auth/login HTTP/1.1
Content-Type: application/json
Host: localhost

{"Name":"<SAMPLE_USERNAME>","Password":"SAMPLE_PASSWORD"}
```

If username and password are correct, response with the access token in JWT format will be returned.

```json
{
  "success": true,
  "message": "Login successfully.",
  "data": {
    "accessToken": "<JWT_TOKEN>"
  }
}
```

#### List All Messages

Send a GET HTTP request to `http://localhost/messages/all` endpoint

```http
GET /messages/all HTTP/1.1
Content-Type: application/json
Authorization: Bearer <YOUR_JWT_TOKEN>
Host: localhost
```

Server will return a response as follows:

```json
{
  "success": true,
  "message": "Messages listed successfully.",
  "data": [
    {
      "from": "mennan",
      "to": "ekrem",
      "content": "Hello!",
      "isRead": true,
      "readDate": "2020-10-17T15:18:33.05Z",
      "sentDate": "2020-10-17T15:17:42.079Z"
    },
    {
      "from": "mennan",
      "to": "ekrem",
      "content": "Hi!",
      "isRead": true,
      "readDate": "2020-10-17T15:18:33.049Z",
      "sentDate": "2020-10-17T15:15:46.096Z"
    }
  ],
  "page": 1,
  "totalPages": 1,
  "perPage": 100
}
```

#### List Unread Messages

Send a GET HTTP request to `http://localhost/messages/unread` endpoint.

```http
GET /messages/unread HTTP/1.1
Content-Type: application/json
Authorization: Bearer <YOUR_JWT_TOKEN>
Host: localhost
```

Server will return a response as follows:

```json
{
  "success": true,
  "message": "Messages listed successfully.",
  "data": [
    {
      "from": "mennan",
      "to": "ekrem",
      "content": "Hello!",
      "isRead": false,
      "sentDate": "2020-10-17T15:17:42.079Z"
    }
  ],
  "page": 1,
  "totalPages": 1,
  "perPage": 100
}
```

#### Send A Message To A User Whose Name Is Known

Send a POST HTTP request to `http://localhost/messages/send` endpoint.

```http
POST /messages/send HTTP/1.1
Content-Type: application/json
Authorization: Bearer <YOUR_JWT_TOKEN>
Host: localhost

{"To":"<TO_USER_NAME>","Content":"Hi!"}
```

#### Block User You Don't Want To Receive Messages

Send a POST HTTP request to `http://localhost/users/block` endpoint.

```http
POST /users/block HTTP/1.1
Content-Type: application/json
Authorization: Bearer <YOUR_JWT_TOKEN>
Host: localhost

{"UserName":"<BLOCKED_USERNAME>"}
```
