using System.Net.Http;
using System.Threading.Tasks;

namespace MessagePack.WebApi.Client
{
    /// <summary>
    /// Extensions to <see cref="HttpContent"/>
    /// </summary>
    public static class MessagePackHttpContentExtensions
    {
        /// <summary>
        /// Reads to given content as MessagePack.
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="content">Content</param>
        /// <returns>Deserialized object.</returns>
        public static async Task<T> ReadAsMessagePackAsync<T>(this HttpContent content)
        {
            return await content.ReadAsAsync<T>(MessagePackMediaTypeFormatter.DefaultMediaTypeFormatters).ConfigureAwait(false);
        }
    }
}
