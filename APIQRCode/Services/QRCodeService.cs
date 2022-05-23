using APIQRCode.Models;
using QRCoder;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using SkiaSharp;
using SkiaSharp.QrCode.Image;
using MessagingToolkit.QRCode.Codec;

namespace APIQRCode.Services
{
    public class QRCodeService
    {
        /*public string GenerateQRCodeBase644(string text)
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
        }*/

        public string GenerateQRCodeBase64(string text)
        {
            using var output = new FileStream("qrcode.png", FileMode.OpenOrCreate);

            // generate QRCode
            var qrCode = new QrCode(text, new Vector2Slim(256, 256), SKEncodedImageFormat.Png);

            // output to file
            qrCode.GenerateImage(output);

            output.Close();

            var base64 = GetBase64StringForImage("qrcode.png");

            string qrCode64 = string.Format("data:image/png;base64," + base64);

            return qrCode64;
        }

        public string GetBase64StringForImage(string imgPath)
        {
            byte[] imageBytes = File.ReadAllBytes(imgPath);
            string base64String = Convert.ToBase64String(imageBytes);
            return base64String;
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
