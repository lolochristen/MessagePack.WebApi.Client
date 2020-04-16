using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace MessagePack.WebApi.Client
{
    /// <summary>
    /// MessagePack Extensions for <see cref="System.Net.Http.HttpClient"/>
    /// </summary>
    public static class MessagePackHttpClientExtensions
    {
        public static readonly string ContentTypeString = "application/x-msgpack";
        private static readonly MediaTypeWithQualityHeaderValue _contentTypeMediaTypeHeaderValue = new MediaTypeWithQualityHeaderValue(ContentTypeString);

        public static void AddMessagePackAcceptHeader(this HttpClient client)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            if (!client.DefaultRequestHeaders.Accept.Contains(_contentTypeMediaTypeHeaderValue))
                client.DefaultRequestHeaders.Accept.Add(_contentTypeMediaTypeHeaderValue);
        }

        public static async Task<T> GetMessagePackAsync<T>(this HttpClient client, Uri requestUri)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            using (var request = new HttpRequestMessage(HttpMethod.Get, requestUri))
            {
                request.Headers.Add("Accept", ContentTypeString);
                var response = await client.SendAsync(request).ConfigureAwait(false);
                return await response.Content.ReadAsMessagePackAsync<T>().ConfigureAwait(false);
            }
        }

        public static async Task<T> GetMessagePackAsync<T>(this HttpClient client, string requestUri)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            using (var request = new HttpRequestMessage(HttpMethod.Get, requestUri))
            {
                request.Headers.Add("Accept", ContentTypeString);
                var response = await client.SendAsync(request).ConfigureAwait(false);
                return await response.Content.ReadAsMessagePackAsync<T>().ConfigureAwait(false);
            }
        }

        public static async Task<HttpResponseMessage> PostAsMessagePackAsync<T>(this HttpClient client, Uri requestUri, T value)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            using (var content = new ObjectContent(typeof(T), value, MessagePackMediaTypeFormatter.DefaultInstance))
                return await client.PostAsync(requestUri, content).ConfigureAwait(false);
        }

        public static async Task<HttpResponseMessage> PostAsMessagePackAsync<T>(this HttpClient client, string requestUri, T value)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            using (var content = new ObjectContent(typeof(T), value, MessagePackMediaTypeFormatter.DefaultInstance))
                return await client.PostAsync(requestUri, content).ConfigureAwait(false);
        }

        public static async Task<HttpResponseMessage> PostAsMessagePackAsync<T>(this HttpClient client, Uri requestUri, T value, CancellationToken cancellationToken)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            using (var content = new ObjectContent(typeof(T), value, MessagePackMediaTypeFormatter.DefaultInstance))
                return await client.PostAsync(requestUri, content, cancellationToken).ConfigureAwait(false);
        }

        public static async Task<HttpResponseMessage> PostAsMessagePackAsync<T>(this HttpClient client, string requestUri, T value, CancellationToken cancellationToken)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            using (var content = new ObjectContent(typeof(T), value, MessagePackMediaTypeFormatter.DefaultInstance))
                return await client.PostAsync(requestUri, content, cancellationToken).ConfigureAwait(false);
        }

        public static async Task<HttpResponseMessage> PutAsMessagePackAsync<T>(this HttpClient client, Uri requestUri, T value)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            using (var content = new ObjectContent(typeof(T), value, MessagePackMediaTypeFormatter.DefaultInstance))
                return await client.PutAsync(requestUri, content).ConfigureAwait(false);
        }

        public static async Task<HttpResponseMessage> PutAsMessagePackAsync<T>(this HttpClient client, string requestUri, T value)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            using (var content = new ObjectContent(typeof(T), value, MessagePackMediaTypeFormatter.DefaultInstance))
                return await client.PutAsync(requestUri, content).ConfigureAwait(false);
        }

        public static async Task<HttpResponseMessage> PutAsMessagePackAsync<T>(this HttpClient client, Uri requestUri, T value, CancellationToken cancellationToken)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            using (var content = new ObjectContent(typeof(T), value, MessagePackMediaTypeFormatter.DefaultInstance))
                return await client.PutAsync(requestUri, content, cancellationToken).ConfigureAwait(false);
        }

        public static async Task<HttpResponseMessage> PutAsMessagePackAsync<T>(this HttpClient client, string requestUri, T value, CancellationToken cancellationToken)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            using (var content = new ObjectContent(typeof(T), value, MessagePackMediaTypeFormatter.DefaultInstance))
                return await client.PutAsync(requestUri, content, cancellationToken).ConfigureAwait(false);
        } 
    }
}
