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
    public class HomeController : Controller
    {
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Index()
        {
            return View();
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost]
        public IActionResult Index(string qrCodeText)
        {
            if (qrCodeText == null)
                return RedirectToAction("Index");

            try
            {
                QRCodeService qrCodeService = new QRCodeService();

                string base64 = qrCodeService.GenerateQRCodeBase64(qrCodeText);

                ViewBag.QRCodeImage = base64;

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
}
