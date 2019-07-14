using Leads.Client;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Leads.Tests
{
    public class SubAreaTests
    {
        private string baseUrl;


        [SetUp]
        public void Setup()
        {
            this.baseUrl = "https://localhost:44399";
        }

        #region Positive Scenarios

        [Test]
        public void GetAllSubAreas()
        {
            var client = new LeadsClient(baseUrl);
            var subAreas = client.GetAllSubAreas();

            Assert.IsTrue(subAreas.Count > 0, $"{nameof(subAreas)} is empty");
        }

        [Test]
        public void GetFilteredSubArea()
        {
            var pinCode = "567";
            var client = new LeadsClient(baseUrl);
            var subAreas = client.GetFilteredSubArea(pinCode);

            Assert.IsTrue(subAreas.Count == 3, $"{nameof(subAreas)} quantity is not expected amount");
        }

        #endregion

        
        #region Negative Scenarios

        [Test]
        public void GetFilteredSubAreaWithNoResults()
        {
            var pinCode = "000";
            var client = new LeadsClient(baseUrl);
            var subAreas = client.GetFilteredSubArea(pinCode);

            Assert.IsTrue(subAreas.Count == 0, $"{nameof(subAreas)} quantity is not expected amount");
        }

        [Test]
        public void GetFilteredSubAreaWithEmptyPin()
        {
            var pinCode = string.Empty;
            var client = new LeadsClient(baseUrl);

            Assert.Throws<InvalidOperationException>(() => client.GetFilteredSubArea(pinCode), "[exception] was not thrown as expected");
        }

        [Test]
        public void GetFilteredSubAreaWithSpecialCharacters()
        {
            var pinCode = "{]}@(*&)";
            var client = new LeadsClient(baseUrl);
            var subAreas = client.GetFilteredSubArea(pinCode);

            Assert.IsTrue(subAreas.Count == 0, $"{nameof(subAreas)} quantity is not expected amount");
        }

        [Test]
        public void GetFilteredSubAreaWithSpecialCharacters2()
        {
            //Expected to fail as handing '+' on server side is not well done

            var pinCode = "+";
            var client = new LeadsClient(baseUrl);
            var subAreas = client.GetFilteredSubArea(pinCode);

            Assert.IsTrue(subAreas.Count == 0, $"{nameof(subAreas)} quantity is not expected amount");
        }
        #endregion

    }
}
