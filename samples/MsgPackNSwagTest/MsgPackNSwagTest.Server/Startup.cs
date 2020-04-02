using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessagePack.NSwag;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MsgPackNSwagTest.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.InputFormatters.Add(new MessagePack.AspNetCoreMvcFormatter.MessagePackInputFormatter());
                options.OutputFormatters.Add(new MessagePack.AspNetCoreMvcFormatter.MessagePackOutputFormatter());
            });

            //issue with v3.NSwag ignores consumes / produces content - types
            //services.AddOpenApiDocument(c =>
            //{
            //    c.SchemaProcessors.Add(new MessagePackAttributesSchemaProcessor());
            //    c.OperationProcessors.Add(new EnforceProducesConsumesAttributesProcessor());
            //    c.DocumentName = "openapi";
            //    c.PostProcess = d => d.Info.Title = "MessagePack Test Service";
            //}); // add OpenAPI v3 document

            services.AddSwaggerDocument(c =>
            {
                c.SchemaProcessors.Add(new MessagePackAttributesSchemaProcessor());
                //c.OperationProcessors.Add(new EnforceProducesConsumesAttributesProcessor());
                c.PostProcess = d => d.Info.Title = "MessagePack Contacts Service";
            }); // add Swagger v2 document
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseOpenApi(); // serve OpenAPI/Swagger documents
            app.UseSwaggerUi3(); // serve Swagger UI
            app.UseReDoc(); // serve ReDoc UI

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
