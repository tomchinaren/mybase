using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;

namespace RPC
{
    public class RPCHttpClient
    {
        private HttpClient _httpClient;
        private static MediaTypeFormatter[] _mediaTypeFormatters = new MediaTypeFormatter[]
        {
            new JsonMediaTypeFormatter(),
            new FormUrlEncodedMediaTypeFormatter(),
            new XmlMediaTypeFormatter(),
        };

        public RPCHttpClient()
        {
            _httpClient = new HttpClient();
        }
        public async Task<TOut> PostAsync<TIn, TOut>(string url, TIn data = default(TIn))
        {
            HttpContent content = null;
            if (data != null)
            {
                Type type = data.GetType();
                var mediaType = new JsonMediaTypeFormatter();
                content = new ObjectContent(type, data, mediaType);
            }
            var response = await _httpClient.PostAsync(url, content);
            return await response.Content.ReadAsAsync<TOut>();
        }

        public async Task<TOut> PostAsync<TOut>(string url)
        {
            HttpContent content = null;
            var response = await _httpClient.PostAsync(url, content);
            return await response.Content.ReadAsAsync<TOut>(_mediaTypeFormatters);
        }

        public async Task<TOut> PutAsync<TIn, TOut>(string url, TIn data = default(TIn))
        {
            HttpContent content = null;
            if (data != null)
            {
                Type type = data.GetType();
                var mediaType = new JsonMediaTypeFormatter();
                content = new ObjectContent(type, data, mediaType);
            }
            var response = await _httpClient.PutAsync(url, content);
            return await response.Content.ReadAsAsync<TOut>();
        }
        public async Task<TOut> PutAsync<TOut>(string url)
        {
            HttpContent content = null;
            var response = await _httpClient.PutAsync(url, content);
            return await response.Content.ReadAsAsync<TOut>(_mediaTypeFormatters);
        }
    }
}
