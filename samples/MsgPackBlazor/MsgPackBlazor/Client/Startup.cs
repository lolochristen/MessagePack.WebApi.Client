using MessagePack;
using MessagePack.Resolvers;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace MsgPackBlazor.Client
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // define a minimalistic set of resolver because Blazor WASM runtime cannot execute System.Reflection.Emit operations
            StaticCompositeResolver.Instance.Register(
                MessagePack.Resolvers.GeneratedResolver.Instance
                , MessagePack.Resolvers.BuiltinResolver.Instance
                , MessagePack.Resolvers.DynamicGenericResolver.Instance
            );
            MessagePackSerializer.DefaultOptions = new MessagePackSerializerOptions(StaticCompositeResolver.Instance);
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
