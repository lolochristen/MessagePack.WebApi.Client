using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace MessagePack.WebApi.Client
{
    /// <summary>
    /// Implementation of <see cref="System.Net.Http.HttpContent"/> for MessagePack
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MessagePackContent<T> : System.Net.Http.HttpContent
    {
        private readonly T _value;
        private readonly MessagePackSerializerOptions _options;

        public MessagePackContent(T value, MessagePackSerializerOptions options = null)
        {
            _value = value;
            _options = options;
        }

        protected override async Task SerializeToStreamAsync(Stream stream, TransportContext context)
        {
            await MessagePackSerializer.SerializeAsync(stream, _value, _options).ConfigureAwait(false);
        }

        protected override bool TryComputeLength(out long length)
        {
            length = -1;
            return false;
        }
    }

    /// <summary>
    /// Implementation of <see cref="System.Net.Http.HttpContent"/> for MessagePack
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MessagePackContent : System.Net.Http.HttpContent
    {
        private readonly object _value;
        private readonly MessagePackSerializerOptions _options;

        public MessagePackContent(object value, MessagePackSerializerOptions options = null)
        {
            _value = value;
            _options = options;
        }

        protected override async Task SerializeToStreamAsync(Stream stream, TransportContext context)
        {
            await MessagePackSerializer.SerializeAsync(stream, _value, _options).ConfigureAwait(false);
        }

        protected override bool TryComputeLength(out long length)
        {
            length = 0;
            return false;
        }
    }
}
