using RESTy.Transaction;
using RESTy.Transaction.Attributes;

namespace Leads.RestyTests.Models
{
    public class LeadsSaveViewModel : RESTFulRequest
    {
        [Required] public string name { get; set; }
        [Required] public string pinCode { get; set; }
        [Required] public int subAreaId { get; set; }
        [Required] public string address { get; set; }
        public string mobileNumber { get; set; }
        public string email { get; set; }

        public LeadsSaveViewModel(string address)
        {
            this.Url = $"{address}/api/Leads";
            this.AcceptType = AcceptType.Json;
        }
    }

    public class LeadViewModelRequest : RESTFulRequest
    {
        public LeadViewModelRequest(string baseUrl, string id)
        {
            this.Url = $"{baseUrl}/api/Leads/{id}";
            this.AcceptType = AcceptType.Json;
        }
    }

    public class LeadViewModelResponse : RESTFulResponse
    {
        public string name { get; set; }
        public string pinCode { get; set; }
        public int subAreaId { get; set; }
        public string address { get; set; }
        public string mobileNumber { get; set; }
        public string email { get; set; }
        public SubAreaViewModel subArea { get; set; }

        public LeadViewModelResponse()
        { this.ContentType = ContentType.Json; }

    }

    public class LeadsSaveSuccessModel : RESTFulResponse
    {
        public string Id { get; set; }

        public LeadsSaveSuccessModel()
        { this.ContentType = ContentType.Json; }
    }
}
