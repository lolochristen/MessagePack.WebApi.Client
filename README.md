# MessagePack.WebApi.Client
A library (Formatters, Client Generators) for simplifing ASP.NET Core based WebApi's using the lightning fast serializer MessagePack.
It does provide a end-to-end solutions  to use MessagePack as a serializer/formatter for Api including documentation using Swagger (NSwag) and client generation (NSwag/NJsonSchema). Finally your API's are almost 3 times faster then using Json.

[![NuGet Version](https://img.shields.io/nuget/v/MessagePack.WebApi.Client.svg)](https://www.nuget.org/packages?q=MessagePack.WebApi.Client)

## MessagePack.WebApi.Client

- System.Net.HttpClient extensions Methods:
```C#
  var client = new System.Net.HttpClient();
  await client.PostAsMessagePackAsync(uri, obj);

  var response = await client.GetAsync(uri); 
  var obj = await response.Content.ReadAsMessagePackAsync<ObjectType>();
```

- MessagePackContent Class implementing System.Net.Http.HttpContent

## MessagePack.NSwag

- MessagePackAttributesSchemaProcessor. A processor implementing NJsonSchema.Generation.ISchemaProcessor that creates extended properties based on MessagePack.Annotations attributes.
  - x-msgpack: true if it object ist marked with MessagePackObjectAttribute 
  - x-msgpack-key: # of the MessagePack.KeyAttribute
  - x-msgpack-ignore: true if marked with MessagePack.IgnoreAttribute
- Templates for using with NSwag.CodeGeneration to produce client classes out of a OpenApi/Swagger definition which strictly use MessagePack as serializer.

---
## Usage Server side: ASPNET.Core WepApi
Startup.cs
```C#
      public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.InputFormatters.Add(new MessagePack.AspNetCoreMvcFormatter.MessagePackInputFormatter());
                options.OutputFormatters.Add(new MessagePack.AspNetCoreMvcFormatter.MessagePackOutputFormatter());
            });

            services.AddSwaggerDocument(c =>
            {
                c.SchemaProcessors.Add(new MessagePackAttributesSchemaProcessor());
            }); 
        }
```
Controllers: (Remark: application/json is still required because NSwag cannot handle well others than Json)
```C#
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/x-msgpack", "application/json")]
    [Consumes("application/x-msgpack", "application/json")]  
    public class ContactController : ControllerBase
    {
      ...
```
## Client Code Generation
NSwag provides different ways to create a client code out of the OpenAPI/Swagger definition. A simple way is using a tool and passing the directory of the templates:
```
nswag openapi2csclient /input:https://localhost:5001/swagger/v1/swagger.json /classname:ContactsService  /namespace:MsgPackNSwagTest.Client.MessagePack  /output:.\MessagePack\ContactsService.cs /templatedirectory:..\..\..\src\MessagePack.NSwag\Templates

```

## Performance Test and Samples
**MsgPackNSwagTest**: The sample shows how to integrate all the components. It containts also a simple performance test to compare MessagePack and Json. The Results shows that MessagePack is by far more efficient thant Json. Sure, the result will vary in depend on many different factors, but still: Go for MessagePack.
- MessagePack: 00.589 s
- JsonSerializer: 01.717 s 

**MsgPackBlazor**: Sample of of a Blazor WASM (preview) application using MessagePack for API calls. Client/Server communication is factor 2-3 times faster then Json Serializer (System.Net.Http.Json)

## Libraries, References used
- https://github.com/neuecc/MessagePack-CSharp
- https://github.com/RicoSuter/NSwag
- https://github.com/RSuter/NJsonSchema
