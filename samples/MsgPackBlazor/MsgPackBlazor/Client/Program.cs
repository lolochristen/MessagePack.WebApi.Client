using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using MessagePack;
using MessagePack.Resolvers;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace MsgPackBlazor.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddSingleton(new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            // define a minimalistic set of resolver because Blazor WASM runtime cannot execute System.Reflection.Emit operations
            StaticCompositeResolver.Instance.Register(
                MessagePack.Resolvers.GeneratedResolver.Instance
                , MessagePack.Resolvers.BuiltinResolver.Instance
                , MessagePack.Resolvers.DynamicGenericResolver.Instance
            );
            MessagePackSerializer.DefaultOptions = new MessagePackSerializerOptions(StaticCompositeResolver.Instance);

            await builder.Build().RunAsync();
        }
    }
}