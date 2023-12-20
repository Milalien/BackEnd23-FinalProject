# BackEnd23-FinalProject

A simple backend for a messaging app.
 
## Startup

### Before running the program:

In appsettings.json, check if the connection string is correct for your setup.
I'm using the express version of Microsoft SQL for this project, so the default is my laptop's SQL server's string.
The "localhost" version is commented above it.

### Running

After this you can run the program and it will open the Swagger UI in your browser.
When you run the program the first time, it will create a database on your server. 

## Using

In the Swagger page you can see the possible HTTP methods. These you can use with Postman.
First you need to add the Apikey to Postman on the Authorization tab. You can find the key in appsettings.json.

### POST user and message

First you need to create a user by calling POST on /api/users and give the information for that user.
Required fields are "username", "password", "firstname" and "lastname". It will automatically generate the "joindate" and "lastlogin".

When you have your user, you need to encode the user's "username:password" at https://www.base64encode.org/.
Add that as a header to Postman. Authorization - Basic *encoded credentials*.

Then you can POST a message to /api/messages as the user. It needs the fields "sender", "title", "body" and optionally "recipient" and "prevMessageId" if you are sending the message to someone or replying to a message.

### GET users and messages

api/users 
Responds with a list of all the users and their information (without the passwords ofcourse).

api/users/{id}
Gets the user with that id number if it exists.

api/messages 
Gets a list of all public (without recipient) messages.

api/messages/{id} 
Gets the message with that id number if it exists.

api/messages/search/{searchtext} 
Searches all public messages for the text you write on the last place and responds with a list of them.

api/messages/sent/{username} 
Gets all messages that person has sent when you are "logged in" as that user.

api/messages/received/{username} 
Gets all messages that person has received when you are "logged in" as that user.

### PUT user

api/users/{id} 
You can update a user when "logged in" as that user.
Body requires the field "id", "username, "password", "firstname" and "lastname". You can update all but the username. 
Succesfull update responds with "No Content" on Postman.

### PUT message

api/messages/{id}
You can update a message when you are "logged in" as the sender. 
Required fields are "id", "sender" and "title". You can also modify the message's "body".

### DELETE

api/users/{id}
You can delete the user with it's id number when you are "logged in" as that user.

api/messages/{id}
You can delete a message with it's id number when you are "logged in" as the sender.

## In conclusion

This was a fun project to do and it taught me a lot.
It is quite simple as is, but it could be developed into something more and I will use all I've learned on some other projects. 
