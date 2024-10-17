using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Services.Helpers
{
    public static class Utils
    {
        public static string GenerateSignature(string orderId, string paymentId, string secret)
        {
            // Concatenate the orderId and paymentId with a pipe "|"
            var data = $"{orderId}|{paymentId}";

            // Convert the secret into a byte array
            var key = Encoding.UTF8.GetBytes(secret);

            // Generate the HMACSHA256 hash
            using (var hmacsha256 = new HMACSHA256(key))
            {
                var hash = hmacsha256.ComputeHash(Encoding.UTF8.GetBytes(data));
                return BitConverter.ToString(hash).Replace("-", "").ToLower(); // Convert hash to hex string
            }
        }
    }
}
