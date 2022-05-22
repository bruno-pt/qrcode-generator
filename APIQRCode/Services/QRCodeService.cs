using APIQRCode.Models;
using QRCoder;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace APIQRCode.Services
{
    public class QRCodeService
    {
        public string GenerateQRCodeBase64(string text)
        {
            string qrCodeModel;

            using (MemoryStream ms = new MemoryStream())
            {
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);

                Bitmap bitmap = qrCode.GetGraphic(20);

                byte[] bitmapArray = bitmap.BitmapToByteArray();

                qrCodeModel = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(bitmapArray));
            }

            return qrCodeModel;
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
