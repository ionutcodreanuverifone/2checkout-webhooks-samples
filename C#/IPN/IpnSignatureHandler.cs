using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace VerifoneIPNSample
{
    public class IpnSignatureHandler
    {
        public string calculateSignature(
            IEnumerable<KeyValuePair<string, string>> requestPayloadValues,
            string secretKey,
            string currentDate
            )
        {
            int size = 0;
            string hash = string.Empty;
            UTF8Encoding encoding = new UTF8Encoding();
            System.Text.StringBuilder PlainDataTohash = new System.Text.StringBuilder();
            var res = requestPayloadValues.GetEnumerator();

            while (res.MoveNext())
            {
                switch (res.Current.Key)
                {
                    case "IPN_PID[]":
                        size = res.Current.Value.Length; 
                                        
                        PlainDataTohash.Append(size); 
                        PlainDataTohash.Append(res.Current.Value); 
                        break; 
                    case "IPN_PNAME[]":
                        size = res.Current.Value.Length; 
                                        
                        PlainDataTohash.Append(size); 
                        PlainDataTohash.Append(res.Current.Value); 
                        break; 
                    case "IPN_DATE":
                        size = res.Current.Value.Length; 
                                        
                        PlainDataTohash.Append(size); 
                        PlainDataTohash.Append(res.Current.Value); 
                        break; 
                }
            }
            size = currentDate.Length;
            PlainDataTohash.Append(size);
            PlainDataTohash.Append(currentDate);
            
            Console.WriteLine(PlainDataTohash);

            string pass = secretKey;
            byte[] passBytes = encoding.GetBytes(pass);
            
            HMACMD5 hmacmd5 = new HMACMD5(passBytes);
            string HashData = PlainDataTohash.ToString();
            
            byte[] baseStringForHashBytes = encoding.GetBytes(HashData);
            byte[] hashBytes = hmacmd5.ComputeHash(baseStringForHashBytes);
            
            hash = ByteToString(hashBytes);

            return hash;
        }

        private static string ByteToString(byte[] buff)
        {
            string sbinary = "";
            for (int i = 0; i < buff.Length; i++)
            {
                sbinary += buff[i].ToString("x2"); // hex format
            }
            return (sbinary);
        }

        public string generateTag(
            IEnumerable<KeyValuePair<string, string>> requestPayloadValues,
            string secretKey
            )
        {
            var now = DateTime.Now.ToString("yyyyMMddHHmmss");
            var responseHash = calculateSignature(requestPayloadValues, secretKey, now);
            var response = now + "|" + responseHash;

            return "<EPAYMENT>" + response + "</EPAYMENT>";
        }
    }
}