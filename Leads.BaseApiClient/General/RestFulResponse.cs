using System.Net;

namespace Leads.ApiClient.General
{
    public class RestFulResponse
    {
        #region Public Constructors

        /// <summary>
        /// Initializes the class with the specified status code and content.
        /// </summary>
        /// <param name="statusCode">The http status code.</param>
        /// <param name="isSuccessStatusCode">A boolean value indicating if the http response was successful.</param>
        /// <param name="content">The http response content.</param>
        public RestFulResponse(HttpStatusCode statusCode, bool isSuccessStatusCode, string content)
        {
            StatusCode = statusCode;
            IsSuccessStatusCode = isSuccessStatusCode;
            Content = content;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets http status code.
        /// </summary>
        public HttpStatusCode StatusCode { get; }

        /// <summary>
        /// Gets a value indicating if the http response was successful.
        /// </summary>
        public bool IsSuccessStatusCode { get; }

        /// <summary>
        /// Gets the http response content.
        /// </summary>
        public string Content { get; }

        #endregion
    }
}
