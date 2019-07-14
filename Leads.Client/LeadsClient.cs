using Leads.ApiClient;
using Leads.Client.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Leads.Client
{
    public class LeadsClient : BaseApiClient
    {
        private readonly string baseUrl = string.Empty;


        #region Public Methods

        public LeadsClient(string baseUrl)
        {
            this.baseUrl = baseUrl;
        }

        /// <summary>
        /// Sends a GET request to retrieve all SubAreas
        /// </summary>
        /// <returns></returns>
        public List<SubAreaViewModel> GetAllSubAreas()
        {
            var endpoint = $"{baseUrl}/api/SubAreas";

            var result = this.Get(endpoint);

            if (result.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<SubAreaViewModel>>(result.Content);
            }

            throw new InvalidOperationException($"URL: {endpoint}\n\nRESTful Exception: {result.StatusCode}\nError: {result.Content}");
        }


        /// <summary>
        /// Sends a GET request to retrieve all SubAreas with assigned given PinCode
        /// </summary>
        /// <param name="pinCode">Used as a filter of SubAreas</param>
        /// <returns></returns>
        public List<SubAreaViewModel> GetFilteredSubArea(string pinCode)
        {
            var endpoint = $"{baseUrl}/api/SubAreas/Filter/PinCode/{pinCode}";

            var result = this.Get(endpoint);

            if (result.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<SubAreaViewModel>>(result.Content);
            }

            throw new InvalidOperationException($"URL: {endpoint}\n\nRESTful Exception: {result.StatusCode}\nError: {result.Content}");
        }


        /// <summary>
        /// Sends a POST request to create a Lead into the database
        /// </summary>
        /// <param name="leadsModel"></param>
        /// <returns></returns>
        public LeadsSaveSuccessModel CreateLead(LeadsSaveViewModel leadsModel)
        {
            var endpoint = $"{baseUrl}/api/Leads";

            var result = this.Post(endpoint, leadsModel);

            if(result.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<LeadsSaveSuccessModel>(result.Content);
            }

            throw new InvalidOperationException($"URL: {endpoint}\n\nRESTful Exception: {result.StatusCode}\nError: {result.Content}");
        }

        /// <summary>
        /// Sends GET request to search for a Lead with an ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public LeadViewModel SearchLead(string id)
        {
            var endpoint = $"{baseUrl}/api/Leads/{id}";

            var result = this.Get(endpoint);

            if (result.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<LeadViewModel>(result.Content);
            }

            throw new InvalidOperationException($"URL: {endpoint}\n\nRESTful Exception: {result.StatusCode}\nError: {result.Content}");
        }

        #endregion


        #region Private Methods



        #endregion
    }
}
