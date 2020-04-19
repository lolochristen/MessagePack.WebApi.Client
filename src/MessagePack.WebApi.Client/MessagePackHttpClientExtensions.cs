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

        private static readonly MediaTypeWithQualityHeaderValue ContentTypeMediaTypeHeaderValue = new MediaTypeWithQualityHeaderValue(ContentTypeString);

        /// <summary>
        /// Adds default Acceot Header to given <see cref="HttpClient"/>.
        /// </summary>
        /// <param name="client">HttpClient to adjust.</param>
        public static void AddDefaultMessagePackAcceptHeader(this HttpClient client)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            if (!client.DefaultRequestHeaders.Accept.Contains(ContentTypeMediaTypeHeaderValue))
                client.DefaultRequestHeaders.Accept.Add(ContentTypeMediaTypeHeaderValue);
        }

        /// <summary>
        /// Calls given Uri and deserialize object from MessagePack.
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="client">client to call</param>
        /// <param name="requestUri">Uri to call</param>
        /// <returns>Deserialized object.</returns>
        public static async Task<T> GetFromMessagePackAsync<T>(this HttpClient client, Uri requestUri)
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

        /// <summary>
        /// Calls given Uri and deserialize object from MessagePack.
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="client">client to use</param>
        /// <param name="requestUri">Uri to call</param>
        /// <returns>Deserialized object.</returns>
        public static async Task<T> GetFromMessagePackAsync<T>(this HttpClient client, string requestUri)
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

        /// <summary>
        /// Post the given value using MessagePack formatter.
        /// </summary>
        /// <typeparam name="T">Type of value</typeparam>
        /// <param name="client">client to use</param>
        /// <param name="requestUri">Uri to call</param>
        /// <param name="value">value</param>
        /// <returns><see cref="HttpResponseMessage"/></returns>
        public static async Task<HttpResponseMessage> PostAsMessagePackAsync<T>(this HttpClient client, Uri requestUri, T value)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            using (var content = new ObjectContent(typeof(T), value, MessagePackMediaTypeFormatter.DefaultInstance))
                return await client.PostAsync(requestUri, content).ConfigureAwait(false);
        }

        /// <summary>
        /// Post the given value using MessagePack formatter.
        /// </summary>
        /// <typeparam name="T">Type of value</typeparam>
        /// <param name="client">client to use</param>
        /// <param name="requestUri">Uri to call</param>
        /// <param name="value">value</param>
        /// <returns><see cref="HttpResponseMessage"/></returns>
        public static async Task<HttpResponseMessage> PostAsMessagePackAsync<T>(this HttpClient client, string requestUri, T value)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            using (var content = new ObjectContent(typeof(T), value, MessagePackMediaTypeFormatter.DefaultInstance))
                return await client.PostAsync(requestUri, content).ConfigureAwait(false);
        }

        /// <summary>
        /// Post the given value using MessagePack formatter.
        /// </summary>
        /// <typeparam name="T">Type of value</typeparam>
        /// <param name="client">client to use</param>
        /// <param name="requestUri">Uri to call</param>
        /// <param name="value">value</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns><see cref="HttpResponseMessage"/></returns>
        public static async Task<HttpResponseMessage> PostAsMessagePackAsync<T>(this HttpClient client, Uri requestUri, T value, CancellationToken cancellationToken)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            using (var content = new ObjectContent(typeof(T), value, MessagePackMediaTypeFormatter.DefaultInstance))
                return await client.PostAsync(requestUri, content, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Post the given value using MessagePack formatter.
        /// </summary>
        /// <typeparam name="T">Type of value</typeparam>
        /// <param name="client">client to use</param>
        /// <param name="requestUri">Uri to call</param>
        /// <param name="value">value</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns><see cref="HttpResponseMessage"/></returns>
        public static async Task<HttpResponseMessage> PostAsMessagePackAsync<T>(this HttpClient client, string requestUri, T value, CancellationToken cancellationToken)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            using (var content = new ObjectContent(typeof(T), value, MessagePackMediaTypeFormatter.DefaultInstance))
                return await client.PostAsync(requestUri, content, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Put the given value using MessagePack formatter.
        /// </summary>
        /// <typeparam name="T">Type of value</typeparam>
        /// <param name="client">client to use</param>
        /// <param name="requestUri">Uri to call</param>
        /// <param name="value">value</param>
        /// <returns><see cref="HttpResponseMessage"/></returns>
        public static async Task<HttpResponseMessage> PutAsMessagePackAsync<T>(this HttpClient client, Uri requestUri, T value)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            using (var content = new ObjectContent(typeof(T), value, MessagePackMediaTypeFormatter.DefaultInstance))
                return await client.PutAsync(requestUri, content).ConfigureAwait(false);
        }

        /// <summary>
        /// Put the given value using MessagePack formatter.
        /// </summary>
        /// <typeparam name="T">Type of value</typeparam>
        /// <param name="client">client to use</param>
        /// <param name="requestUri">Uri to call</param>
        /// <param name="value">value</param>
        /// <returns><see cref="HttpResponseMessage"/></returns>
        public static async Task<HttpResponseMessage> PutAsMessagePackAsync<T>(this HttpClient client, string requestUri, T value)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            using (var content = new ObjectContent(typeof(T), value, MessagePackMediaTypeFormatter.DefaultInstance))
                return await client.PutAsync(requestUri, content).ConfigureAwait(false);
        }

        /// <summary>
        /// Put the given value using MessagePack formatter.
        /// </summary>
        /// <typeparam name="T">Type of value</typeparam>
        /// <param name="client">client to use</param>
        /// <param name="requestUri">Uri to call</param>
        /// <param name="value">value</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns><see cref="HttpResponseMessage"/></returns>
        public static async Task<HttpResponseMessage> PutAsMessagePackAsync<T>(this HttpClient client, Uri requestUri, T value, CancellationToken cancellationToken)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            using (var content = new ObjectContent(typeof(T), value, MessagePackMediaTypeFormatter.DefaultInstance))
                return await client.PutAsync(requestUri, content, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Put the given value using MessagePack formatter.
        /// </summary>
        /// <typeparam name="T">Type of value</typeparam>
        /// <param name="client">client to use</param>
        /// <param name="requestUri">Uri to call</param>
        /// <param name="value">value</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns><see cref="HttpResponseMessage"/></returns>
        public static async Task<HttpResponseMessage> PutAsMessagePackAsync<T>(this HttpClient client, string requestUri, T value, CancellationToken cancellationToken)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            using (var content = new ObjectContent(typeof(T), value, MessagePackMediaTypeFormatter.DefaultInstance))
                return await client.PutAsync(requestUri, content, cancellationToken).ConfigureAwait(false);
        } 
    }
}
