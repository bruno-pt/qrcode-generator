using APIQRCode.Models;
using APIQRCode.Services;
using Microsoft.AspNetCore.Mvc;
using QRCoder;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Security.Policy;

namespace APIQRCode.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class HomeController : Controller
    {
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Index()
        {
            return View("Index", "~/Views/Shared/_Layout.cshtml");
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost]
        public IActionResult Index(string qrCodeText)
        {
            try
            {
                QRCodeService qrCodeService = new QRCodeService();

                string qrCodeBase64 = qrCodeService.GenerateQRCodeBase64(qrCodeText);

                ViewBag.QRCodeImage = qrCodeBase64;

                return View();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [SwaggerOperation(Tags = new[] { "QRCode" })]
        [ProducesResponseType(typeof(QRCodeModel), 200)]
        [ProducesResponseType(typeof(ErrorModel), 500)]
        [HttpGet("api/qrcode/{text}")]
        public ActionResult<QRCodeModel> Base64(string text)
        {
            try
            {
                QRCodeModel qrCodeModel = new QRCodeModel();
                QRCodeService qrCodeService = new QRCodeService();

                qrCodeModel.QRCodeBase64 = qrCodeService.GenerateQRCodeBase64(text);

                return StatusCode(200, qrCodeModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorModel(ex.Message));
            }
        }
    }



    public static class BitmapExtension
    {
        public static byte[] BitmapToByteArray(this Bitmap bitmap)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Png);
                return ms.ToArray();
            }
        }
    }
}
