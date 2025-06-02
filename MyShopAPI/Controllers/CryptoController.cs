using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyShopAPI.Helpers;
using System.Security.Cryptography;

namespace MyShopAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class CryptoController : ControllerBase
    {
        [HttpGet("public-key")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetPublicKey()
        {
            using var rsa = RSA.Create(RSAManager.PublicKey);
            var key = rsa.ExportSubjectPublicKeyInfo(); // DER format
            var pem = "-----BEGIN PUBLIC KEY-----\n" +
                      Convert.ToBase64String(key, Base64FormattingOptions.InsertLineBreaks) +
                      "\n-----END PUBLIC KEY-----";
            return Ok(pem);
        }
    }
}
