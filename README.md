# BackEnd23-FinalProject
 
## Startup

Before running the program:
In appsettings.json, check if the connection string is correct for your setup.
I'm using the express version of Microsoft SQL for this project, so the default is my laptop server's string.
The "localhost" version is commented above it.

After this you can run the program and it will open the Swagger UI in your browser.

## Using

In the Swagger page you can see the possible HTTP methods. These you can use with Postman.
First you need to add the Apikey to postman, you can find it in appsettings.json.

When you first start with the program, it will create a database on your server. 
Then you need to create a user by calling POST on /api/users/ and give the information for that user.

When you have your user, you need to encode the user's "username:password" at https://www.base64encode.org/.
Add that as a header to Postman. Authorization - Basic *encoded credentials*.

Then you can POST a message to /api/messages/ as the user. It need fields "sender", "title", "body" and optionally "recipient" and "prevMessageId".

You can use the GET method /api/messages/search/{searchtext} to search from all public (without recipient) messages.
