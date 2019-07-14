using RESTy.Transaction;
using RESTy.Transaction.Attributes;
using System.Collections.Generic;

namespace Leads.RestyTests.Models
{
    public class SubAreaViewModel : RESTFulRequest
    {
        public int Id { get; set; }
        public string PinCode { get; set; }
        public string Name { get; set; }

        public SubAreaViewModel(string address)
        {
            this.Url = $"{address}/api/SubAreas";
            this.AcceptType = AcceptType.Json;
        }
    }

    public class SubAreaViewModelFilterRequest : RESTFulRequest
    {
        public SubAreaViewModelFilterRequest(string address, string pinCode)
        {
            this.Url = $"{address}/api/SubAreas/Filter/PinCode/{pinCode}";
            this.AcceptType = AcceptType.Json;
        }
    }


    public class SubAreaViewModelResponse : RESTFulResponse
    {
        [JsonPath("$")]
        public List<SubAreaViewModel> SubAreas { get; set; }

        public SubAreaViewModelResponse()
        { this.ContentType = ContentType.Json; }
    }
}
