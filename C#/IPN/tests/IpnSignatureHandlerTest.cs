using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VerifoneIPNSample.Tests
{
    [TestClass()]
    public class IpnSignatureHandlerTests
    {
        [TestMethod()]
        public void TestCalculateSignature() {
            List<KeyValuePair<string, string>> requestPayloadValues = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("IPN_PID[]", "1"),
                new KeyValuePair<string, string>("IPN_PNAME[]", "Software program"),
                new KeyValuePair<string, string>("IPN_DATE", "20050303123434"),
            };

            string testedDate = "20050303123434";
            string secretKey = "test";
            
            IpnSignatureHandler ipnHandler = new IpnSignatureHandler();

            var responseHash = ipnHandler.calculateSignature(requestPayloadValues, secretKey, testedDate);
            
            Console.WriteLine(responseHash);

            Assert.AreEqual(responseHash, "3d7a5eb5557b70a80e5141c4fee0ed0d");
        }
    }
}