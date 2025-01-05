using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace AuthorizeServer.Helpers
{
    public class GeneratorKey
    {
        private static readonly string DefaultFile = Path.Combine("Keys", "PrivateKey.xml");
        public static RsaSecurityKey GetRsaKey()
        {
            var rsaKey = RSA.Create();
            string xmlKey = File.ReadAllText(DefaultFile);
            rsaKey.FromXmlString(xmlKey);
            var rsaSecurityKey = new RsaSecurityKey(rsaKey);
            return rsaSecurityKey;
        }
    }
}
