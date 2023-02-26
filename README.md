# Inventory Management System API

## <u>Endpoints</u>

### All the following endpoints are prefixed by the API url, in this case it is `http://localhost:5000` or `https://localhost:5001`

<br/>

### User Endpoints

<br/>

![Users Endpoints](https://github.com/PeterBouSaada/IMS-API/blob/master/README%20Assets/UsersEndpoints.png)

<br/>

### Item Endpoints

<br/>

![Items Endpoints](https://github.com/PeterBouSaada/IMS-API/blob/master/README%20Assets/ItemsEndpoints.png)

<br/>

## <u>Setup</u>

<br/>

### **Windows:**

\- Open the command line with administrator privileges.<br/>
\- use the following commands (make sure to add the `"quotes"`): <br/>

| command                                                 | effect                                                                                                                                                              |
| :------------------------------------------------------ | :------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| `setx MONGO_STRING "<YOUR-MONGO-CONNECTION-STRING>" /m` | Creates a machine wide environment variable called MONGO_STRING that contains the mongodb connection string that the API will use for storage.                      |
| `setx JWT_PRIVATE_KEY "<YOUR-JWT-PRIVATE-KEY>" /m`      | Creates a machine wide environment variable called JWT_PRIVATE_KEY that contains the JSON Web Token private key used to manage and sign the keys created by the API |
| `setx JWT_EXPIRES_IN "<JWT_EXPIRES_IN>" /m`             | Creates a machine wide environment variable called JWT_EXPIRES_IN that contains the amount in hours that a JWT Bearer token will be valid for.                      |

<br/>

### **Mac/Linux:**

\- Open a terminal window.<br/>
\- Navigate to ~ using the command `cd ~`<br/>
\- Edit the `.profile` file using any editor, you can use vim using `vim .profile` from the terminal and add the following commands to the end (on seperate lines, and without any `"quotes"`):<br/>

| command                                              | effect                                                                                                                                                              |
| :--------------------------------------------------- | :------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| `export MONGO_STRING=<YOUR-MONGO-CONNECTION-STRING>` | Creates a machine wide environment variable called MONGO_STRING that contains the mongodb connection string that the API will use for storage.                      |
| `export JWT_PRIVATE_KEY=<YOUR-JWT-PRIVATE-KEY\>`     | Creates a machine wide environment variable called JWT_PRIVATE_KEY that contains the JSON Web Token private key used to manage and sign the keys created by the API |
| `export JWT_EXPIRES_IN=<JWT_EXPIRES_IN>`             | Creates a machine wide environment variable called JWT_EXPIRES_IN that contains the amount in hours that a JWT Bearer token will be valid for.                      |

\- Save the `.profile` file.

<br/>
<br/>

## <u>Running the xUnit tests</u>

\- Open the `IMS.sln` file in visual studio 2022 or newer with .net sdk 6.0 support. <br/>
\- In visual studio, right click on the `API.xUnitTests` project then click `Run Tests`

<br/>

## <u>Running the Server</u>

\- Open the `IMS.sln` file in visual studio 2022 or newer with .net sdk 6.0 support. <br/>
\- In visual studio, click on the run dropdown, then select `API`
