using IronBarCode;
using IronSoftware.Drawing;
using System.Text.Json.Serialization;

namespace BarCatReader.Models
{
    public class BarcodeModel
    {
        public string? BarcodeType { get; set; }
        public string? Value { get; set; }
        public byte[]? BinaryValue { get; set; }
        public int? X1 { get; set; }
        public int? Y1 { get; set; }
        public int? X2 { get; set; }
        public int? Y2 { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }

        public BarcodeModel() { }

        public BarcodeModel(BarcodeResult res) {
            BarcodeType = res.BarcodeType.ToString();
            Value = res.Value;
            BinaryValue = res.BinaryValue;
            X1 = res.X1;
            Y1 = res.Y1;
            X2 = res.X2;
            Y2 = res.Y2;
            Width = res.Width;
            Height = res.Height;
        }
    }
}
