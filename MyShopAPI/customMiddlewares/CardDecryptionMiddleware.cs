using MyShopAPI.Helpers;
using System.Security.Cryptography;
using System.Text;

namespace MyShopAPI.CustomMiddlewares
{
    public class CardDecryptionMiddleware
    {
        private readonly RequestDelegate _next;
        public CardDecryptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }   

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Method != HttpMethods.Options && context.Request.Path == "/api/MonnifyCheckout/card_charge")
            {
                var rsa = RSAManager.CreateRsaFromPrivateKey();

                var encryptedKeyBase64 = context.Request.Headers["X-Encrypted-Key"];
                var encryptedIVBase64 = context.Request.Headers["X-Encrypted-IV"];


                if (string.IsNullOrWhiteSpace(encryptedKeyBase64) || string.IsNullOrWhiteSpace(encryptedIVBase64))
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsync("Missing encryption headers.");
                    return;
                }

                if (!string.IsNullOrEmpty(encryptedKeyBase64) && !string.IsNullOrEmpty(encryptedIVBase64))
                {
                        var aesKey = rsa.Decrypt(Convert.FromBase64String(encryptedKeyBase64!), RSAEncryptionPadding.OaepSHA256);
                        var aesIV = rsa.Decrypt(Convert.FromBase64String(encryptedIVBase64!), RSAEncryptionPadding.OaepSHA256);

                        context.Request.EnableBuffering();
                        using var reader = new StreamReader(context.Request.Body);
                        var encryptedBody = await reader.ReadToEndAsync();
                        context.Request.Body.Position = 0;

                        var encryptedBytes = Convert.FromBase64String(encryptedBody);

                        var tagSize = 16;
                        int cipherLength = encryptedBytes.Length - tagSize;

                        byte[] ciphertext = encryptedBytes[..cipherLength];
                        byte[] tag = encryptedBytes[^tagSize..];

                        byte[] plaintextBytes = new byte[cipherLength];

                        using var aesGcm = new AesGcm(aesKey, tagSize);
                        aesGcm.Decrypt(aesIV, ciphertext, tag, plaintextBytes);

                        string json = Encoding.UTF8.GetString(plaintextBytes);

                        var newBody = new MemoryStream(Encoding.UTF8.GetBytes(json));
                        context.Request.Body = newBody;
                        context.Request.ContentLength = newBody.Length;
                        context.Request.Body.Position = 0;
                        context.Request.ContentType = "application/json";

                }

            }

            await _next(context);

        }

    }
}
