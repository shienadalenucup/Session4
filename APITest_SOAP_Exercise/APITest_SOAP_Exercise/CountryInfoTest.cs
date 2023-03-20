using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics.Metrics;
using System.Drawing;

namespace APITest_SOAP_Exercise
{
    [TestClass]
    public class CountryInfoTest
    {
        //Global Variable
        private readonly ServiceReference1.CountryInfoServiceSoapTypeClient countryInfoTest =
            new ServiceReference1.CountryInfoServiceSoapTypeClient(ServiceReference1.CountryInfoServiceSoapTypeClient.EndpointConfiguration.CountryInfoServiceSoap);

        [TestMethod]
        public void CountryByAscOrder()
        {
            //Validate the return of ‘ListOfCountryNamesByCode()’ API is by Ascending Order of Country Code
            var countryOrder = countryInfoTest.ListOfCountryNamesByCode();
            var isoCodes = countryOrder.Select(c => c.sISOCode).ToList();
            //var expectedCountryList = isoCodes.OrderByDescending(x => x.sISOCode);

            Assert.IsTrue(isoCodes.SequenceEqual(isoCodes.OrderBy(x=>x)));
        }

        [TestMethod]
        public void InvalidCountryCodeByName()
        {
            //Validate passing of invalid Country Code to ‘CountryName()’ API returns ‘Country not found in the database’
            var expectedError = "Country not found in the database";
            var actualError = countryInfoTest.CountryName("OO");

            Assert.AreEqual(expectedError, actualError);

        }

        [TestMethod]
        public void LastEntryCountryNameByCode()
        {
            /* Get the last entry from ‘ListOfCountryNamesByCode()’ API and pass the return value Country Code to 
              ‘CountryName()’ API then validate the Country Name from both API is the same*/
            var lastCountry = countryInfoTest.ListOfCountryNamesByCode().Last();
            var actualCountryName = countryInfoTest.CountryName(lastCountry.sISOCode);

            Assert.AreEqual(lastCountry.sName, actualCountryName);
        }
    }
}