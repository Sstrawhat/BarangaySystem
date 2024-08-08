using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Security
{
    public class CryptographicSignature
    {
        private readonly string secretPass = "3y2H@ckMe2023!!!!!!";

        public static byte[] ComputeHashSha256(byte[] toBeHashed)
        {
            using var sha256 = SHA256.Create();
            return sha256.ComputeHash(toBeHashed);
        }

        public string SignData(string username, string password)
        {


            using (var rsa = new RSACryptoServiceProvider())
            {
                // Load the private key
                rsa.ImportParameters(this.GeneratedPrivateKey());

                // Create a hash of the login data (for demonstration, we're using SHA-256)
                byte[] loginDataBytes = Encoding.UTF8.GetBytes(username + password + "salt" + secretPass);
                byte[] hash = new SHA256Managed().ComputeHash(loginDataBytes);

                // Sign the hash
                byte[] signatureBytes = rsa.SignHash(hash, CryptoConfig.MapNameToOID("SHA256"));

                // Convert the signature to Base64
                string signature = Convert.ToBase64String(signatureBytes);

                return signature;
            }
        }

        public bool VerifySignature(string username, string password, string encryptedPassword)
        {


            //var data = Encoding.UTF8.GetBytes(encryptedPassword);

            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportParameters(this.GeneratedPrivateKey());


                // Create a hash of the login data (for demonstration, we're using SHA-256)
                byte[] loginDataBytes = Encoding.UTF8.GetBytes(username + password + "salt" + secretPass);
                byte[] hash = new SHA256Managed().ComputeHash(loginDataBytes);

                // Convert the Base64 signature back to bytes
                byte[] signatureBytes = Convert.FromBase64String(encryptedPassword);

                // Verify the signature
                return rsa.VerifyHash(hash, CryptoConfig.MapNameToOID("SHA256"), signatureBytes);
            }
        }

        private RSAParameters GeneratedPrivateKey()
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                var paramerter = rsa.ExportParameters(false);
                if (false)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append($"D : {Convert.ToBase64String(paramerter.D)}");
                    sb.Append($"\nDP : {Convert.ToBase64String(paramerter.DP)}");
                    sb.Append($"\nDQ : {Convert.ToBase64String(paramerter.DQ)}");
                    sb.Append($"\nExponent : {Convert.ToBase64String(paramerter.Exponent)}");
                    sb.Append($"\nInverseQ : {Convert.ToBase64String(paramerter.InverseQ)}");
                    sb.Append($"\nModulus : {Convert.ToBase64String(paramerter.Modulus)}");
                    sb.Append($"\nP : {Convert.ToBase64String(paramerter.P)}");
                    sb.Append($"\nQ : {Convert.ToBase64String(paramerter.Q)}");

                }
                else
                {
                    paramerter.D = Convert.FromBase64String("JttguKHIkCxZH9NFcUDCGTHN0ZAxAjDyTLYEAQWcJ/OSJbV24VTBNHKa8rqBguNxS4nQ4RFtKh/26f6dStKmcQTZd6cbnEqfbVaRo9LDrQw9a17i0fFLSz77WJHl3+TtbajfAgE4AUXuG2o9FdqHwJi2ZRrUFQ4JzQE8TxdUhNLYl+jikaCglWUo0IiW/YAuXFYHce+IOuQQ4Jj4CQ9OnR4jpY6dbFeQedrGKLdqyHJAq6xiqbwfa7UpAwjC4XhcF5FviFb2Y5axRuuCa1v0UPgYnc6m0X2G18oSbuyd6dC+n9sGJwTKu26lyscGmwW/fKZaM48MYN93HXUpzN7I5Q==");
                    paramerter.DP = Convert.FromBase64String("HfQVYkBxWqX040f+qj/YhuiQDRV0QIB5yQu+IJb8Q/znSNUaraQhefaf0MVBs6aUDpsdoRcPottJuHlwoQ8rGZL8y8aPMkX8k/0wtGjAo5ngnDHVpo3o48dDoThF/JeFUxz6dKrQU/mSVNt7TMnPB9V8nYVkpaF9oWMg4lBRlb0=");
                    paramerter.DQ = Convert.FromBase64String("So6LKQn5ovxMKMxSQFEzeLQQAzC7i7pKi+n5IW/omunc+svlJOeA2ijNqB8jNDN7czChwKnibLG/rpXNoZiPKbBLRX08BXteZl91dgnBiWLeLx4x2PgNO9onxH1qrwfCvX3Wxmaat7mZWwsLkWVjbAXiZxBGCgtvpP3Qy+Z5CA8=");
                    paramerter.Exponent = Convert.FromBase64String("AQAB");
                    paramerter.InverseQ = Convert.FromBase64String("NiYBgBFZx5COFrkflEKdPf1+ws2TjI0AGE2EmRaKoNpDYJxgrUHa5WMOG5qbr1NRdtOO3875o55tv0nUCVKnq2pZprDwBdw0D64xrQp/8nssBoLQs492naBn2hOJ8+1NcGLAHYjTH7hD0I3Hkq/IYIPx/+Ua/iswMUcbDIr6dS8=");
                    paramerter.Modulus = Convert.FromBase64String("nTSKL3tFG5dm1fQKgHQdC8WjWacxlNoYZSWjqTulH44fN1pz0fl/WOxIlyrIoYa6Lhmz98Fj8x9Km3+Z/uSRxAfQiM2/zAKmiTyvVGGH7CZIiBqlyo33UQ9q71C/L9Furkj9qLypY5urgSKqNF3VqiGlON1EWRoKsxncAUIxyIan4wCEdC9S9QtaHODhY6Mr64s4n80x6GUy8SXMJM2X/tooRdVP/WzndziazeI2DgSk0sN9SP9U5sWNoOCeFJF5FrjKoiVns28nCMyOVRWyJc0jbaJlvfsrNC8VmrshDWxHylWOcVctfQfDBcb3b1kKLNVPDVIIOmp6o3WARAskTQ==");
                    paramerter.P = Convert.FromBase64String("wRm4I5pbCP9vLzaX2n3hC4+PutZ7vj4066QdhYr5x4VlU1y1LYyk8b/hkJ82XZVSX/kOQhhLMPofODxDWWQTwukk4rjpGvl5YGQKUISvYE8TpM1UeFvZkH+BswqbWB/g1zZ7onSrAWhcoU+hHwBy3wZOivRR3gH+9uc6TrLj5fs=");
                    paramerter.Q = Convert.FromBase64String("0GmV1tjctXQ/QXzOgqcgZWmFEFURgippvlZ4K4NppueYMA3NGoIF2VelbLWDUVKT8LbMF+5NDU/3F/ZDMKWhWvYxs6MHYNXiKn7xyeXa1/a8YJ3yCUIjv5H5aXACwarT9G6XoFHGZN/xM1t0agOGYDZRDW1S6aog0+Q9u+DTNFc=");
                }

                return paramerter;
            }
        }


    }
}
