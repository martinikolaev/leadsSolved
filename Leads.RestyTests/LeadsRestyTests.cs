using Leads.RestyTests.Models;
using NUnit.Framework;
using RESTy.Transaction;

namespace Tests
{
    public class Tests
    {

        #region Setup

        private string baseUrl;


        [SetUp]
        public void Setup()
        {
            this.baseUrl = "https://localhost:44399";
        }

        #endregion

        #region Positive Scenarios


        [Test]
        public void CreateLead()
        {
            var requestModel = new LeadsSaveViewModel(baseUrl)
            {
                name = "User1",
                email = "user1@fakemail.com",
                mobileNumber = "123456789",
                address = "FakeAddress",
                pinCode = "123",
                subAreaId = 1
            };

            var result = requestModel.POST<LeadsSaveSuccessModel>();

            Assert.IsNotEmpty(result.Id, $"[{nameof(result.Id)}] empty");
        }

        [Test]
        public void CreateLeadWithAreaSearch()
        {
            var subAreaRequest = new SubAreaViewModel(baseUrl).GET<SubAreaViewModelResponse>();
            var selectedArea = subAreaRequest.SubAreas[0];

            var requestModel = new LeadsSaveViewModel(baseUrl)
            {
                name = "User1",
                email = "user1@fakemail.com",
                mobileNumber = "123456789",
                address = "FakeAddress",
                pinCode = selectedArea.PinCode,
                subAreaId = selectedArea.Id
            };

            var result = requestModel.POST<LeadsSaveSuccessModel>();

            Assert.IsNotEmpty(result.Id, $"[{nameof(result.Id)}] empty");
        }

        [Test]
        public void CreateLeadAndSearch()
        {
            var subAreaRequest = new SubAreaViewModel(baseUrl).GET<SubAreaViewModelResponse>();
            var selectedArea = subAreaRequest.SubAreas[0];

            var leadModel = new LeadsSaveViewModel(baseUrl)
            {
                name = "User1",
                email = "user1@fakemail.com",
                mobileNumber = "123456789",
                address = "FakeAddress",
                pinCode = selectedArea.PinCode,
                subAreaId = selectedArea.Id
            };

            var result = leadModel.POST<LeadsSaveSuccessModel>();

            Assert.IsNotEmpty(result.Id, $"[{nameof(result.Id)}] empty");

            var searchLead = new LeadViewModelRequest(baseUrl, result.Id).GET<LeadViewModelResponse>();

            
            //compare lead model
            Assert.AreEqual(leadModel.name, searchLead.name, $"[{nameof(searchLead.name)}] not as expected");
            Assert.AreEqual(leadModel.address, searchLead.address, $"[{nameof(searchLead.address)}] not as expected");
            Assert.AreEqual(leadModel.email, searchLead.email, $"[{nameof(searchLead.email)}] not as expected");
            Assert.AreEqual(leadModel.mobileNumber, searchLead.mobileNumber, $"[{nameof(searchLead.mobileNumber)}] not as expected");
            Assert.AreEqual(leadModel.pinCode, searchLead.pinCode, $"[{nameof(searchLead.pinCode)}] not as expected");
            Assert.AreEqual(leadModel.subAreaId, searchLead.subAreaId, $"[{nameof(searchLead.subAreaId)}] not as expected");

            //compare subarea model
            Assert.AreEqual(selectedArea.Name, searchLead.subArea.Name, $"[{nameof(searchLead.subArea.Name)}] not as expected");
            Assert.AreEqual(selectedArea.Id, searchLead.subArea.Id, $"[{nameof(searchLead.subArea.Id)}] not as expected");
            Assert.AreEqual(selectedArea.PinCode, searchLead.subArea.PinCode, $"[{nameof(searchLead.subArea.PinCode)}] not as expected");
        }

        #endregion
    }
}