using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing;
using System.Drawing.Imaging;
using System.Drawing;
using ZXing.Common;

namespace DAL
{
    public class Barcode_reader
    {

        public Barcode_reader(){ }
        public string Decode(string url)
        {
            var qrcodebitmap = (Bitmap)Image.FromFile(url);


            var reader = new BarcodeReader(null, null, ls => new GlobalHistogramBinarizer(ls)) { AutoRotate = false, TryInverted = false };
            var result = reader.Decode(qrcodebitmap);
            if (result != null)
                return result.Text;
            else
                return "Error";
        }
    }
}
