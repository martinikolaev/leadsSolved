namespace Leads.Client.Models
{
    public class LeadsSaveViewModel
    {
        public string Name { get; set; }
        public string PinCode { get; set; }
        public int SubAreaId { get; set; }
        public string Address { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }

    }

    public class LeadViewModel
    {
        public string Name { get; set; }
        public string PinCode { get; set; }
        public int SubAreaId { get; set; }
        public string Address { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public SubAreaViewModel SubArea { get; set; }

    }

    public class LeadsSaveSuccessModel
    {
        public string Id { get; set; }
    }
}
