using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace MessagePack.WebApi.Client
{
    public class MessagePackMediaTypeFormatter : MediaTypeFormatter
    {
        private static readonly MediaTypeHeaderValue _contentTypeMediaTypeHeader = MediaTypeHeaderValue.Parse(MessagePackHttpClientExtensions.ContentTypeString);

        public static readonly MessagePackMediaTypeFormatter DefaultInstance = new MessagePackMediaTypeFormatter();

        public static readonly MediaTypeFormatter[] DefaultMediaTypeFormatters = new []{ DefaultInstance };

        private static MediaTypeFormatterCollection _defaultMediaTypeFormatterCollection = null;

        public MessagePackSerializerOptions SerializerOptions { get;set; }

        public static MediaTypeFormatterCollection DefaultMediaTypeFormatterCollection
        {
            get
            {
                if (_defaultMediaTypeFormatterCollection == null)
                {
                    _defaultMediaTypeFormatterCollection = new MediaTypeFormatterCollection();
                    _defaultMediaTypeFormatterCollection.AddRange(DefaultMediaTypeFormatters);
                }
                return _defaultMediaTypeFormatterCollection;
            }
        }

        public MessagePackMediaTypeFormatter(MessagePackSerializerOptions options = null)
        {
            SupportedMediaTypes.Add(_contentTypeMediaTypeHeader);
            SerializerOptions = options;
        }

        public override bool CanReadType(Type type)
        {
            return true;
        }

        public override bool CanWriteType(Type type)
        {
            return CanReadType(type);
        }

        public override void SetDefaultContentHeaders(Type type, HttpContentHeaders headers, MediaTypeHeaderValue mediaType)
        {
            if (headers == null)
                throw new ArgumentNullException(nameof(headers));

            headers.ContentType = _contentTypeMediaTypeHeader;
        }

        public override async Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content,
            TransportContext transportContext)
        {
            await MessagePackSerializer.SerializeAsync(writeStream, value, SerializerOptions).ConfigureAwait(false);
        }

        public override async Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content,
            TransportContext transportContext, CancellationToken cancellationToken)
        {
            await MessagePackSerializer.SerializeAsync(writeStream, value, SerializerOptions).ConfigureAwait(false);
        }

        public override async Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger,
            CancellationToken cancellationToken)
        {
            return await MessagePackSerializer.DeserializeAsync(type, readStream, SerializerOptions, cancellationToken: cancellationToken);
        }

        public override async Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
        {
            return await MessagePackSerializer.DeserializeAsync(type, readStream, SerializerOptions);
        }
    }
}