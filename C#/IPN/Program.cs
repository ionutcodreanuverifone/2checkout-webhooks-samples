using System;
using System.Collections;
using System.Collections.Generic;

namespace VerifoneIPNSample
{
    class Program
    {
        static void Main(string[] args)
        {
            IpnSignatureHandler ipnHandler = new IpnSignatureHandler();
            List<KeyValuePair<string, string>> requestBody = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("IPN_PID[]", "1"),
                new KeyValuePair<string, string>("IPN_PNAME[]", "Software program"),
                new KeyValuePair<string, string>("IPN_DATE", "20050303123434"),
            };
            string secret = "test";

            Console.WriteLine(ipnHandler.generateTag(requestBody, secret));
        }
    }
}
