using MyShopAPI.Services.RSA;
using Org.BouncyCastle.Crypto.IO;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace MyShopAPI.customMiddlewares
{
    public class CardDecryptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        private readonly IRSAService _raService;

        public CardDecryptionMiddleware(RequestDelegate next, IConfiguration configuration, IRSAService rSAService)
        {
            _next = next;
            _configuration = configuration;
            _raService = rSAService;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path == "/api/MonnifyCheckout/card_charge")
            {

                //var cipherStream = context.Request.Body;

                //try
                //{
                //    var length = context.Request.ContentLength;
                //    using (var aes = Aes.Create())
                //    {
                //        aes.Key = Convert.FromBase64String(_configuration["card_decripytor:key"]!);
                //        aes.IV = Convert.FromBase64String(_configuration["card_decripytor:iv"]!);

                //        aes.Mode = CipherMode.CBC;
                //        aes.BlockSize = 128;
                //        aes.Padding = PaddingMode.PKCS7;
                //        aes.KeySize = 128;

                //        FromBase64Transform base64Transform = new FromBase64Transform(FromBase64TransformMode.IgnoreWhiteSpaces);
                //        CryptoStream base64DecodedStream = new CryptoStream(cipherStream, base64Transform, CryptoStreamMode.Read);
                //        ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                //        CryptoStream decryptedStream = new CryptoStream(base64DecodedStream, decryptor, CryptoStreamMode.Read);
                //        context.Request.Body = decryptedStream;
                //        await _next.Invoke(context);
                //    }
                //}
                //catch (Exception)
                //{


                //}
                //try
                //{
                //    //byte[] cipherBytes = new byte[Convert.ToInt32(context.Request.ContentLength)];

                //    var data = context.Request.Body.ToString(); //.ReadAsync(cipherBytes, 0, cipherBytes.Length);

                //    var reader = new StreamReader(context.Request.Body);

                //    var plainText = await reader.ReadToEndAsync();


                //    //byte[] bytes = Encoding.UTF8.GetBytes(data);

                //    //string encoded = Convert.ToBase64String(bytes);

                //    //var base64String = Convert.ToBase64String(data);

                //    var cipherBytes = Convert.FromBase64String(plainText);

                //    var decrytedData = _raService.Decrypt(cipherBytes);

                //    var content = new StringContent(decrytedData, Encoding.UTF8, "application/json");

                //    var stream = content.ReadAsStream();

                //    context.Request.Body = stream;

                //}
                //catch (Exception)
                //{

                //}

                try
                {
                    //var length = 16 * (int)Math.Ceiling(((decimal)context.Request.ContentLength!)/16);
                    byte[] cipherBytes = new byte[Convert.ToInt32(context.Request.ContentLength)];

                    await context.Request.Body.ReadAsync(cipherBytes, 0, cipherBytes.Length);

                    var plainText = String.Empty;

                    using (var aes = Aes.Create())
                    {
                        aes.Key = Convert.FromHexString(_configuration["card_decripytor:key"]!.PadRight(32)!);

                        aes.Mode = CipherMode.CBC;
                        aes.BlockSize = 128;
                        aes.Padding = PaddingMode.PKCS7;
                        aes.KeySize = 128;


                        aes.IV = Convert.FromHexString(_configuration["card_decripytor:iv"]!);

                        using (ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                        {
                            using (MemoryStream msDecrpyt = new MemoryStream(cipherBytes))
                            {
                                using (CryptoStream csDecrpt = new CryptoStream(msDecrpyt, decryptor, CryptoStreamMode.Read))
                                {
                                    using (StreamReader srDecrypt = new StreamReader(csDecrpt))
                                    {
                                        plainText = srDecrypt.ReadToEnd();
                                    }
                                }

                            }
                        }

                    }

                    var content = new StringContent(plainText, Encoding.UTF8, "application/json");

                    var stream = content.ReadAsStream();

                    context.Request.Body = stream;
                }
                catch (Exception ex)
                {

                }
            }

            try
            {
                await _next.Invoke(context);
            }
            catch (Exception)
            {

                
            }
        }

    }
}
