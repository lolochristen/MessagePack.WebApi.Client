echo off
rem Alternative way to create to classes from commandline instead of using OpenApiReference generator.
echo messagepack
call nswag openapi2csclient /input:https://localhost:5001/swagger/v1/swagger.json /classname:ContactsService  /namespace:MsgPackNSwagTest.Client.MessagePack  /output:.\MessagePack\ContactsService.cs /templatedirectory:..\..\..\src\MessagePack.NSwag\Templates

echo json
call nswag openapi2csclient /input:https://localhost:5001/swagger/v1/swagger.json /classname:ContactsService  /namespace:MsgPackNSwagTest.Client.Json  /output:.\Json\ContactsService.cs
echo done.
