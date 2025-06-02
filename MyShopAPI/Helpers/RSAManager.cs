using System.Security.Cryptography;

namespace MyShopAPI.Helpers
{
    public static class RSAManager
    {
        public static RSAParameters PublicKey;
        public static RSAParameters PrivateKey;

        static RSAManager()
        {
            using var rsa = RSA.Create(2048);
            PublicKey = rsa.ExportParameters(false);
            PrivateKey = rsa.ExportParameters(true);
        }

        public static RSA CreateRsaFromPrivateKey() =>
        RSA.Create(PrivateKey);
    }
}
