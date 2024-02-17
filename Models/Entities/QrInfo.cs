using QRCoder;
using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;

namespace copyqr.Models.Entities;

public partial class QrInfo
{
    public int Id { get; set; }

    public byte[]? Image { get; set; }

    public string? ContentEncode { get; set; }

    public DateTime? DateCreated { get; set; }

    public long UserId { get; set; }

    public string? Classroom { get; set; }

    public virtual User User { get; set; } = null!;

    public QrInfo() { }
    public QrInfo(string context,string classroom)
    {
        DateCreated = DateTime.Now;
        Classroom = classroom;
        ContentEncode = $"http://{IpConfiguration.Ip}:8000/api/Qr/{context}";

        QRCodeData qrCodeData;
        using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
        {
            qrCodeData = qrGenerator.CreateQrCode(ContentEncode, QRCodeGenerator.ECCLevel.Q);
        }

        // Image Format
        var imgType = Base64QRCode.ImageType.Png;
        Color rndColor = Color.ParseHex(String.Format("#{0:X6}", new Random().Next(0x1000000)));

        var qrCode = new Base64QRCode(qrCodeData);
        //Base64 Format
        string qrCodeImageAsBase64 = qrCode.GetGraphic(20, rndColor, Color.White, true, imgType);
        Image = Convert.FromBase64String(qrCodeImageAsBase64);
    }
}
