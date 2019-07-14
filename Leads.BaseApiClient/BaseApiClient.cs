using Leads.ApiClient.General;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Leads.ApiClient
{
    public class BaseApiClient
    {

        #region Public Fields

        /// <summary>
        /// The default content type for REST api.
        /// </summary>
        public const string JsonContentType = "application/json";

        /// <summary>
        /// Form content type for REST api
        /// </summary>
        public const string FormContentType = "application/x-www-form-urlencoded";

        /// <summary>
        /// The default accept type for REST api.
        /// </summary>
        public const string JsonAcceptType = "application/json";

        #endregion

        #region Protected Methods

        /// <summary>
        /// Calls the REST api with GET method.
        /// </summary>
        /// <param name="url">The url for the REST api.</param>
        /// <returns>The http response having status code and content.</returns>
        protected RestFulResponse Get(string url)
        {
            return GetInternal(url, JsonAcceptType, null);
        }

        /// <summary>
        /// Calls the REST api with POST method.
        /// </summary>
        /// <param name="url">The url for the REST api.</param>
        /// <param name="content">The content for the REST api.</param>
        /// <param name="contentType">The content type string.</param>
        /// <returns>The http response having status code and content.</returns>
        protected RestFulResponse Post(string url, object content)
        {
            return PostInternal(url, content, JsonAcceptType, null);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Calls the REST api with POST method.
        /// </summary>
        /// <param name="url">The url for the REST api.</param>
        /// <param name="content">The content for the REST api.</param>
        /// <param name="contentType">The content type string.</param>
        /// <param name="requestHeaders">The request headers for the REST api.</param>
        /// <returns>The http response having status code and content.</returns>
        private RestFulResponse PostInternal(string url, object content, string contentType, Dictionary<string, string> requestHeaders)
        {
            // Create http client with auto disposition
            using (var httpClient = CreateHttpClient(requestHeaders))
            {
                // Add 'Accept' header in the request
                if (requestHeaders != null && requestHeaders.All(rh => rh.Key != HttpRequestHeader.Accept.ToString()))
                {
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(MediaTypeWithQualityHeaderValue.Parse(contentType));
                }

                // Build the http content
                HttpContent httpContent = null;

                var jsonContent = content is string ? content.ToString() : JsonConvert.SerializeObject(content);
                httpContent = new StringContent(jsonContent, Encoding.UTF8, contentType);

                // Call the REST api with POST method
                var result = httpClient.PostAsync(url, httpContent).Result;

                // Get the response content
                var responseContent = result.Content.ReadAsStringAsync().Result;

                // Return the RESTFul response
                return new RestFulResponse(result.StatusCode, result.IsSuccessStatusCode, responseContent);
            }
        }

        /// <summary>
        /// Calls the REST api with GET method.
        /// </summary>
        /// <param name="url">The url for the REST api.</param>
        /// <param name="acceptType">The accept type string.</param>
        /// <param name="requestHeaders">The request headers for the REST api.</param>
        /// <returns>The http response having status code and content.</returns>
        private RestFulResponse GetInternal(string url, string acceptType, Dictionary<string, string> requestHeaders)
        {
            // Create http client with auto disposition
            using (var httpClient = CreateHttpClient(requestHeaders))
            {
                // Add 'Accept' header in the request
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(MediaTypeWithQualityHeaderValue.Parse(acceptType));

                // Call the REST api with GET method
                var result = httpClient.GetAsync(url).Result;

                // Get the response content
                var responseContent = result.Content.ReadAsStringAsync().Result;

                // Return the RESTFul response
                return new RestFulResponse(result.StatusCode, result.IsSuccessStatusCode, responseContent);
            }
        }

        /// <summary>
        /// Creates the http client with the specified request headers.
        /// </summary>
        /// <param name="requestHeaders">The collection having request headers.</param>
        /// <returns>The http client object.</returns>
        private HttpClient CreateHttpClient(IReadOnlyDictionary<string, string> requestHeaders)
        {
            // Declares local variables
            HttpClient httpClient;

            httpClient = new HttpClient();
            
            // Add request headers in the http client
            if (requestHeaders != null && requestHeaders.Count > 0)
            {
                httpClient.DefaultRequestHeaders.Clear();
                foreach (var header in requestHeaders)
                {
                    httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }

            // Return the http client
            return httpClient;
        }

        #endregion
    }

}
