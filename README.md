### Set up
\- Open the .sln file in visual studio 2022 or newer with .net sdk 6.0 support.
\- Copy the `appsettings.Environment.json` file and rename to `appsettings.Development.json`

fill in the appropriate information:
\- `Database` is the mongoDB connection string.
\- `Private_Key` is the JWT token secret key that should remain serverside and private.
\- `Expires_In` is the time that the token is valid for.

##### Running the server
In visual studio, click on the run dropdown, then select `API`

##### Running the frontend
\- Open a new terminal window and navigate to `/Client`
\- Run the command `ng serve`