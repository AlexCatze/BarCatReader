using System.Text.Json.Serialization;
using ZXing;

namespace BarCatReader.Models
{
    public class BarcodeModel
    {
        public string? BarcodeType { get; set; }
        public string? Value { get; set; }
        public byte[]? BinaryValue { get; set; }
        public float X1 { get; set; }
        public float Y1 { get; set; }
        public float X2 { get; set; }
        public float Y2 { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }

        public BarcodeModel() { }

        public BarcodeModel(Result res)
        {
            BarcodeType = res.BarcodeFormat.ToString();
            Value = res.Text;
            BinaryValue = res.RawBytes;
            X1 = res.ResultPoints[0].X;
            Y1 = res.ResultPoints[0].Y;
            X2 = res.ResultPoints[1].X;
            Y2 = res.ResultPoints[1].Y;
            Width = X2 - X1;
            Height = Y2 - Y1;
        }
    }
}
