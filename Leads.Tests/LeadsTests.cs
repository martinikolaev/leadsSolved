using Leads.Client;
using Leads.Client.Models;
using NUnit.Framework;
using System;

namespace Leads.Tests
{
    public class LeadsTests
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
            var client = new LeadsClient(baseUrl);
            var subAreas = client.GetAllSubAreas();
            var selectedArea = subAreas[0];

            var leadModel = new LeadsSaveViewModel()
            {
                Name = "User1",
                Email = "user1@fakemail.com",
                MobileNumber = "123456789",
                Address = "FakeAddress",
                PinCode = selectedArea.PinCode,
                SubAreaId = selectedArea.Id
            };

            var result = client.CreateLead(leadModel);

            Assert.IsNotEmpty(result.Id, $"[{nameof(result.Id)}] empty");

        }

        [Test]
        public void CreateLeadAndSearch()
        {
            var client = new LeadsClient(baseUrl);
            var subAreas = client.GetAllSubAreas();
            var selectedArea = subAreas[0];

            var leadModel = new LeadsSaveViewModel()
            {
                Name = "User1",
                Email = "user1@fakemail.com",
                MobileNumber = "123456789",
                Address = "FakeAddress",
                PinCode = selectedArea.PinCode,
                SubAreaId = selectedArea.Id
            };

            var result = client.CreateLead(leadModel);

            Assert.IsNotEmpty(result.Id, $"[{nameof(result.Id)}] empty");

            //search for created lead
            var searchLead = client.SearchLead(result.Id);

            //compare lead model
            Assert.AreEqual(leadModel.Name, searchLead.Name, $"[{nameof(searchLead.Name)}] not as expected");
            Assert.AreEqual(leadModel.Address, searchLead.Address, $"[{nameof(searchLead.Address)}] not as expected");
            Assert.AreEqual(leadModel.Email, searchLead.Email, $"[{nameof(searchLead.Email)}] not as expected");
            Assert.AreEqual(leadModel.MobileNumber, searchLead.MobileNumber, $"[{nameof(searchLead.MobileNumber)}] not as expected");
            Assert.AreEqual(leadModel.PinCode, searchLead.PinCode, $"[{nameof(searchLead.PinCode)}] not as expected");
            Assert.AreEqual(leadModel.SubAreaId, searchLead.SubAreaId, $"[{nameof(searchLead.SubAreaId)}] not as expected");

            //compare subarea model
            Assert.AreEqual(selectedArea.Name, searchLead.SubArea.Name, $"[{nameof(searchLead.SubArea.Name)}] not as expected");
            Assert.AreEqual(selectedArea.Id, searchLead.SubArea.Id, $"[{nameof(searchLead.SubArea.Id)}] not as expected");
            Assert.AreEqual(selectedArea.PinCode, searchLead.SubArea.PinCode, $"[{nameof(searchLead.SubArea.PinCode)}] not as expected");

        }

        [Test]
        public void CreateLeadNameSpecialCharacters()
        {
            var client = new LeadsClient(baseUrl);
            var subAreas = client.GetAllSubAreas();
            var selectedArea = subAreas[0];

            var leadModel = new LeadsSaveViewModel()
            {
                Name = "User1!@#$%^&*()_0",
                Email = "user1@fakemail.com",
                MobileNumber = "123456789",
                Address = "FakeAddress",
                PinCode = selectedArea.PinCode,
                SubAreaId = selectedArea.Id
            };

            var result = client.CreateLead(leadModel);

            Assert.IsNotEmpty(result.Id, $"[{nameof(result.Id)}] empty");

        }



        [Test]
        public void CreateLeadWithEmptyEmail()
        {
            var client = new LeadsClient(baseUrl);
            var subAreas = client.GetAllSubAreas();
            var selectedArea = subAreas[0];

            var leadModel = new LeadsSaveViewModel()
            {
                Name = "User1",
                Email = "",
                MobileNumber = "123456789",
                Address = "FakeAddress",
                PinCode = selectedArea.PinCode,
                SubAreaId = selectedArea.Id
            };

            var result = client.CreateLead(leadModel);

            Assert.IsNotEmpty(result.Id, $"[{nameof(result.Id)}] empty");

        }

        [Test]
        public void CreateLeadWithEmptyMobile()
        {
            var client = new LeadsClient(baseUrl);
            var subAreas = client.GetAllSubAreas();
            var selectedArea = subAreas[0];

            var leadModel = new LeadsSaveViewModel()
            {
                Name = "User1",
                Email = "user1@fakemail.com",
                MobileNumber = "",
                Address = "FakeAddress",
                PinCode = selectedArea.PinCode,
                SubAreaId = selectedArea.Id
            };

            var result = client.CreateLead(leadModel);

            Assert.IsNotEmpty(result.Id, $"[{nameof(result.Id)}] empty");
        }

        [Test]
        public void CreateLeadWithFilteredSubArea()
        {
            var client = new LeadsClient(baseUrl);

            var selectedPin = "567";

            var subAreas = client.GetFilteredSubArea(selectedPin);
            var selectedArea = subAreas[0];

            var leadModel = new LeadsSaveViewModel()
            {
                Name = "User1",
                Email = "user1@fakemail.com",
                MobileNumber = "123456789",
                Address = "FakeAddress",
                PinCode = selectedArea.PinCode,
                SubAreaId = selectedArea.Id
            };

            var result = client.CreateLead(leadModel);

            Assert.IsNotEmpty(result.Id, $"[{nameof(result.Id)}] empty");

            //search for created lead
            var searchLead = client.SearchLead(result.Id);

            //compare lead model
            Assert.AreEqual(leadModel.Name, searchLead.Name, $"[{nameof(searchLead.Name)}] not as expected");
            Assert.AreEqual(leadModel.Address, searchLead.Address, $"[{nameof(searchLead.Address)}] not as expected");
            Assert.AreEqual(leadModel.Email, searchLead.Email, $"[{nameof(searchLead.Email)}] not as expected");
            Assert.AreEqual(leadModel.MobileNumber, searchLead.MobileNumber, $"[{nameof(searchLead.MobileNumber)}] not as expected");
            Assert.AreEqual(leadModel.PinCode, searchLead.PinCode, $"[{nameof(searchLead.PinCode)}] not as expected");
            Assert.AreEqual(leadModel.SubAreaId, searchLead.SubAreaId, $"[{nameof(searchLead.SubAreaId)}] not as expected");

            //compare subarea model
            Assert.AreEqual(selectedArea.Name, searchLead.SubArea.Name, $"[{nameof(searchLead.SubArea.Name)}] not as expected");
            Assert.AreEqual(selectedArea.Id, searchLead.SubArea.Id, $"[{nameof(searchLead.SubArea.Id)}] not as expected");
            Assert.AreEqual(selectedArea.PinCode, searchLead.SubArea.PinCode, $"[{nameof(searchLead.SubArea.PinCode)}] not as expected");

            //Additional verificaion
            Assert.AreEqual(selectedPin, searchLead.PinCode, $"[{nameof(searchLead.PinCode)}] not as expected");
        }

        #endregion

        #region Negative Scenarios

        [Test]
        public void CreateLeadNameEmpty()
        {
            var client = new LeadsClient(baseUrl);
            var subAreas = client.GetAllSubAreas();
            var selectedArea = subAreas[0];

            var leadModel = new LeadsSaveViewModel()
            {
                Name = " ",
                Email = "user1@fakemail.com",
                MobileNumber = "123456789",
                Address = "FakeAddress",
                PinCode = selectedArea.PinCode,
                SubAreaId = selectedArea.Id
            };

            Assert.Throws<InvalidOperationException>(() => client.CreateLead(leadModel), "[exception] was not thrown as expected");
        }

        [Test]
        public void CreateLeadWithEmptySpaceName()
        {
            var client = new LeadsClient(baseUrl);
            var subAreas = client.GetAllSubAreas();
            var selectedArea = subAreas[0];

            var leadModel = new LeadsSaveViewModel()
            {
                Name = " ",
                Email = "user1@fakemail.com",
                MobileNumber = "123456789",
                Address = "FakeAddress",
                PinCode = "123",
                SubAreaId = selectedArea.Id
            };

            Assert.Throws<InvalidOperationException>(() => client.CreateLead(leadModel), "[exception] was not thrown as expected");

        }


        [Test]
        public void CreateLeadWithEmptyAddress()
        {
            var client = new LeadsClient(baseUrl);
            var subAreas = client.GetAllSubAreas();
            var selectedArea = subAreas[0];

            var leadModel = new LeadsSaveViewModel()
            {
                Name = "User1",
                Email = "user1@fakemail.com",
                MobileNumber = "123456789",
                Address = "",
                PinCode = selectedArea.PinCode,
                SubAreaId = selectedArea.Id
            };

            Assert.Throws<InvalidOperationException>(() => client.CreateLead(leadModel), "[exception] was not thrown as expected");

        }

        [Test]
        public void CreateLeadWithEmptySpaceAddress()
        {
            var client = new LeadsClient(baseUrl);
            var subAreas = client.GetAllSubAreas();
            var selectedArea = subAreas[0];

            var leadModel = new LeadsSaveViewModel()
            {
                Name = "User1",
                Email = "user1@fakemail.com",
                MobileNumber = "123456789",
                Address = " ",
                PinCode = selectedArea.PinCode,
                SubAreaId = selectedArea.Id
            };

            Assert.Throws<InvalidOperationException>(() => client.CreateLead(leadModel), "[exception] was not thrown as expected");

        }

        [Test]
        public void CreateLeadWithAddressSpecialCharacters()
        {
            var client = new LeadsClient(baseUrl);
            var subAreas = client.GetAllSubAreas();
            var selectedArea = subAreas[0];

            var leadModel = new LeadsSaveViewModel()
            {
                Name = "User1",
                Email = "user1@fakemail.com",
                MobileNumber = "123456789",
                Address = "!@#$%^&*()",
                PinCode = selectedArea.PinCode,
                SubAreaId = selectedArea.Id
            };

            var result = client.CreateLead(leadModel);

            Assert.IsNotEmpty(result.Id, $"[{nameof(result.Id)}] empty");
        }


        [Test]
        public void CreateLeadWithMismatchingSubAreaAndPin()
        {
            var client = new LeadsClient(baseUrl);
            var subAreas = client.GetAllSubAreas();
            var selectedArea = subAreas[0];

            var leadModel = new LeadsSaveViewModel()
            {
                Name = "User1",
                Email = "user1@fakemail.com",
                MobileNumber = "123456789",
                Address = "FakeAddress",
                PinCode = "6",
                SubAreaId = 1
            };

            Assert.Throws<InvalidOperationException>(() => client.CreateLead(leadModel), "[exception] was not thrown as expected");

        }

        #endregion

    }
}