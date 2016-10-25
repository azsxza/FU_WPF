using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace FU_UWP
{
    public  static unsafe class Functions
    {
        [DllImport("ImageRender.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static void Worlde(byte* src, byte* dest, int height, int width, int stride);

        [DllImport("ImageRender.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static void Carving(byte* src, byte* dest, int height, int width, int stride);

        [DllImport("ImageRender.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static void MtxxCQ(byte* src, byte* dest, int width, int height, int stride, float opacity = 100);

        [DllImport("ImageRender.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static void GTA5Style(byte* src, byte* dest, int width, int height, int stride, int brushSize = 5, int coarseness = 50);

        [DllImport("ImageRender.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static void Cartoon(byte* src, byte* dest, int width, int height, int stride, float sigma_s = 60, float sigma_r = 0.45f);

        [DllImport("ImageRender.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static void CoherenceFilter(byte* src, byte* dest, int width, int height, int stride, int sigma = 9, int str_sigma = 11, float blend = 0.5f, int iter = 4);

        [DllImport("ImageRender.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static void LineDrawing(byte* src, byte* dest, int width, int height, int stride, int kernel = 4, int iter = 3, float sigma_c = 1.0f, float sigma_m = 3.0f, int style = 1);

        [DllImport("ImageRender.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static void OilPaint(byte* src, byte* dest, int width, int height, int stride, int brushsize = 2, int coarseness = 5);

        [DllImport("ImageRender.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static void MtxxOilPaint(byte* src, byte* dest, int width, int height, int stride, float opacity = 100);

        [DllImport("ImageRender.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static void MtxxLifeSketch(byte* src, byte* dest, int width, int height, int stride, float opacity = 100);

        [DllImport("ImageRender.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static void WaterColor(byte* src, byte* dest, int width, int height, int stride, int bruishsize_w = 5, int brushsize_h = 2);

        [DllImport("ImageRender.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static void Fenbi(byte* src, byte* dest, int height, int width, int stride);





        //调节类
        //平滑
        [DllImport("ImageRender.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static void Dermabrasion(byte* src, byte* dest, int width, int height, int stride, int ks, int opacity, int white);



        public static BitmapImage fun(BitmapImage bi, string name)
        {
            Bitmap bmp = BitMapImage2Bitmap(bi);
            Bitmap dest = (Bitmap)bmp.Clone();
            var adata = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), System.Drawing.Imaging.ImageLockMode.ReadWrite, bmp.PixelFormat);
            var data = dest.LockBits(new Rectangle(0, 0, dest.Width, dest.Height), System.Drawing.Imaging.ImageLockMode.ReadWrite, dest.PixelFormat);
            var src = (byte*)adata.Scan0;
            var ImageStride = adata.Stride;
            var output = (byte*)data.Scan0;
            bmp.UnlockBits(adata);
            dest.UnlockBits(data);

            switch (name)
            {
                case "Worlde": Worlde(src, output, bmp.Height, bmp.Width, ImageStride); break;
                case "Carving": Carving(src, output, bmp.Height, bmp.Width, ImageStride); break;
                case "MtxxCQ": MtxxCQ(src, output, bmp.Width, bmp.Height, ImageStride); break;
                case "GTA5Style": GTA5Style(src, output, bmp.Width, bmp.Height, ImageStride); break;
                case "Cartoon": Cartoon(src, output, bmp.Width, bmp.Height, ImageStride); break;
                case "CoherenceFilter": CoherenceFilter(src, output, bmp.Width, bmp.Height, ImageStride); break;
                case "LineDrawing": LineDrawing(src, output, bmp.Width, bmp.Height, ImageStride); break;
                case "OilPaint": OilPaint(src, output, bmp.Width, bmp.Height, ImageStride); break;
                case "MtxxOilPaint": MtxxOilPaint(src, output, bmp.Width, bmp.Height, ImageStride); break;
                case "MtxxLifeSketch": MtxxLifeSketch(src, output, bmp.Width, bmp.Height, ImageStride); break;
                case "WaterColor": WaterColor(src, output, bmp.Width, bmp.Height, ImageStride); break;
                case "Fenbi": Fenbi(src, output, bmp.Width, bmp.Height, ImageStride); break;
            }

            return Bitmap2BitmapImage(dest);
        }

        public static Bitmap BitMapImage2Bitmap(BitmapImage bitmapImage)
        {
            using (MemoryStream outstream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapImage));
                enc.Save(outstream);
                Bitmap bitmap = new Bitmap(outstream);
                return new Bitmap(bitmap);
            }
        }

        public static BitmapImage Bitmap2BitmapImage(Bitmap bitmap)
        {
            Bitmap bitmapSource = new Bitmap(bitmap);

            MemoryStream ms = new MemoryStream();
            bitmapSource.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = new MemoryStream(ms.ToArray());
            bitmapImage.EndInit();

            return bitmapImage;
        }
    }
}
