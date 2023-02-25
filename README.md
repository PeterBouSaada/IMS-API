# Inventory Management System API

## <u>Endpoints</u>

### All the following endpoints are prefixed by the API url, in this case it is `http://localhost:5000` or `https://localhost:5001`

<br/>

> ### **Table Explanation:**
>
> **Endpoint:** the path of the request.<br/>
>
> **Request Method:** HTTP request method to use.<br/>
>
> **Authorized:** Requires an Authorization header with a Bearer Token.<br/>
>
> **Return On Success:** HTTP Response status code on success. <br/>
>
> **Return On Failure:** HTTP Response status code on failure. <br/>
>
> **Example Success Return:** JSON Response body on successful return. <br/>

<br/>

### User Endpoints

<br/>

> | Endpoint            | Request Method | Authorized | Return On Success | Return On Failure | Example Success Return                                                                                                          |
> | :------------------ | :------------: | :--------: | :---------------- | :---------------- | ------------------------------------------------------------------------------------------------------------------------------- |
> | /users/Authenticate |      POST      |     NO     | 200 OK            | 401 Unauthorized  | JSON containing token: `{"token":"someToken"} `                                                                                 |
> | /users              |      GET       |    YES     | 200 OK            | 400 Bad Request   | JSON array containing users: `[{"id":"someId", . . . "salt":"someSalt"}, . . . {"id":"AnotherId", . . . "salt":"AnotherSalt"}]` |
> | /users/{id}         |      GET       |    YES     | 200 OK            | 400 Bad Request   | JSON Object containing user: `{"id":"someId", . . . "salt":"someSalt"}`                                                         |
> | /users/{id}         |     DELETE     |    YES     | 200 OK            | 400 Bad Request   | No return body                                                                                                                  |
> | /users/{id}         |      PUT       |    YES     | 200 OK            | 400 Bad Request   | JSON Object containing updated user: `{"id":"someId", . . . "salt":"someSalt"}`                                                 |
> | /users/add          |      POST      |    YES     | 201 Created       | 400 Bad Request   | JSON Object containing inserted user: `{"id":"someId", . . . "salt":"someSalt"}`                                                |
> | /users/search       |      POST      |    YES     | 200 OK            | 400 Bad Request   | JSON array containing users: `[{"id":"someId", . . . "salt":"someSalt"}, . . . {"id":"AnotherId", . . . "salt":"AnotherSalt"}]` |

<br/>

### Item Endpoints

<br/>

> | Endpoint      | Request Method | Authorized | Return On Success | Return On Failure | Example Success Return                                                                                                     |
> | :------------ | :------------: | :--------: | :---------------- | :---------------- | -------------------------------------------------------------------------------------------------------------------------- |
> | /items        |      GET       |    YES     | 200 OK            | 400 Bad Request   | JSON array containing item: `[{"id":"someId", . . . "rpm":"someRPM"}, . . . {"id":"AnotherId", . . . "rpm":"AnotherRPM"}]` |
> | /items/{id}   |      GET       |    YES     | 200 OK            | 400 Bad Request   | JSON Object containing item: `{"id":"someId", . . . "rpm":"someRPM"}`                                                      |
> | /items/{id}   |     DELETE     |    YES     | 200 OK            | 400 Bad Request   | No return body                                                                                                             |
> | /items/{id}   |      PUT       |    YES     | 200 OK            | 400 Bad Request   | JSON Object containing updated item: `{"id":"someId", . . . "rpm":"someRPM"}`                                              |
> | /items/add    |      POST      |    YES     | 201 Created       | 400 Bad Request   | JSON Object containing inserted item: `{"id":"someId", . . . "rpm":"someRPM"}`                                             |
> | /items/search |      POST      |    YES     | 200 OK            | 400 Bad Request   | JSON array containing item: `[{"id":"someId", . . . "rpm":"someRPM"}, . . . {"id":"AnotherId", . . . "rpm":"AnotherRPM"}]` |

<br/>

## <u>Setup</u>

<br/>

> ### **Windows:**
>
> \- Open the command line with administrator privileges.
>
> \- use the following commands (make sure to add the `"quotes"`): <br/>
>
> | command                                                 | effect                                                                                                                                                              |
> | :------------------------------------------------------ | :------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
> | `setx MONGO_STRING "<YOUR-MONGO-CONNECTION-STRING>" /m` | Creates a machine wide environment variable called MONGO_STRING that contains the mongodb connection string that the API will use for storage.                      |
> | `setx JWT_PRIVATE_KEY "<YOUR-JWT-PRIVATE-KEY>" /m`      | Creates a machine wide environment variable called JWT_PRIVATE_KEY that contains the JSON Web Token private key used to manage and sign the keys created by the API |
> | `setx JWT_EXPIRES_IN "<JWT_EXPIRES_IN>" /m`             | Creates a machine wide environment variable called JWT_EXPIRES_IN that contains the amount in hours that a JWT Bearer token will be valid for.                      |

<br/>

> ### **Mac/Linux:**
>
> \- Open a terminal window.
>
> \- Navigate to ~ using the command `cd ~`
>
> \- Edit the `.profile` file using any editor, you can use vim using `vim .profile` from the terminal and add the following commands to the end (on seperate lines, and without any `"quotes"`):
>
> | command                                              | effect                                                                                                                                                              |
> | :--------------------------------------------------- | :------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
> | `export MONGO_STRING=<YOUR-MONGO-CONNECTION-STRING>` | Creates a machine wide environment variable called MONGO_STRING that contains the mongodb connection string that the API will use for storage.                      |
> | `export JWT_PRIVATE_KEY=<YOUR-JWT-PRIVATE-KEY\>`     | Creates a machine wide environment variable called JWT_PRIVATE_KEY that contains the JSON Web Token private key used to manage and sign the keys created by the API |
> | `export JWT_EXPIRES_IN=<JWT_EXPIRES_IN>`             | Creates a machine wide environment variable called JWT_EXPIRES_IN that contains the amount in hours that a JWT Bearer token will be valid for.                      |
>
> \- Save the `.profile` file.

<br/>
<br/>

## <u>Running the xUnit tests</u>

> \- Open the `IMS.sln` file in visual studio 2022 or newer with .net sdk 6.0 support.
>
> In visual studio click on the run dropdown, then select `API.xUnitTests`

<br/>

## <u>Running the Server</u>

> \- Open the `IMS.sln` file in visual studio 2022 or newer with .net sdk 6.0 support.
>
> In visual studio, click on the run dropdown, then select `API`
