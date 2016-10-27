using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FU_UWP
{
    public unsafe class Functions
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

        [DllImport("ImageRender.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static void Line(byte* src, byte* dest, int height, int width, int stride);

        [DllImport("ImageRender.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static void setbri(byte* src, byte* dest, int height, int width, int stride, int b);

        [DllImport("ImageRender.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static void setCompare(byte* src, byte* dest, int height, int width, int stride, int b);





        //调节类
        //平滑
        [DllImport("ImageRender.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static void Dermabrasion(byte* src, byte* dest, int width, int height, int stride, int ks, int opacity, int white);

        public static BitmapImage fun(Bitmap bi, string name)
        {
            Bitmap bmp = (Bitmap)bi.Clone();
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
                case "复古": Worlde(src, output, bmp.Height, bmp.Width, ImageStride); break;
                case "雕刻": Carving(src, output, bmp.Height, bmp.Width, ImageStride); break;
                case "彩色素描": MtxxCQ(src, output, bmp.Width, bmp.Height, ImageStride); break;
                case "GTA5Style": GTA5Style(src, output, bmp.Width, bmp.Height, ImageStride); break;
                case "卡通": Cartoon(src, output, bmp.Width, bmp.Height, ImageStride); break;
                case "抽象画": CoherenceFilter(src, output, bmp.Width, bmp.Height, ImageStride); break;
                case "线条": LineDrawing(src, output, bmp.Width, bmp.Height, ImageStride); break;
                case "油画": OilPaint(src, output, bmp.Width, bmp.Height, ImageStride); break;
                case "油画2": MtxxOilPaint(src, output, bmp.Width, bmp.Height, ImageStride); break;
                case "素描": MtxxLifeSketch(src, output, bmp.Width, bmp.Height, ImageStride); break;
                case "水彩": WaterColor(src, output, bmp.Width, bmp.Height, ImageStride); break;
                case "粉笔": Fenbi(src, output, bmp.Width, bmp.Height, ImageStride); break;
                case "线条2": Line(src, output, bmp.Width, bmp.Height, ImageStride); break;
            }

            return Bitmap2BitmapImage(dest);
        }

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
                case "复古": Worlde(src, output, bmp.Height, bmp.Width, ImageStride); break;
                case "雕刻": Carving(src, output, bmp.Height, bmp.Width, ImageStride); break;
                case "彩色素描": MtxxCQ(src, output, bmp.Width, bmp.Height, ImageStride); break;
                case "GTA5Style": GTA5Style(src, output, bmp.Width, bmp.Height, ImageStride); break;
                case "卡通": Cartoon(src, output, bmp.Width, bmp.Height, ImageStride); break;
                case "抽象画": CoherenceFilter(src, output, bmp.Width, bmp.Height, ImageStride); break;
                case "线条": LineDrawing(src, output, bmp.Width, bmp.Height, ImageStride); break;
                case "油画": OilPaint(src, output, bmp.Width, bmp.Height, ImageStride); break;
                case "油画2": MtxxOilPaint(src, output, bmp.Width, bmp.Height, ImageStride); break;
                case "素描": MtxxLifeSketch(src, output, bmp.Width, bmp.Height, ImageStride); break;
                case "水彩": WaterColor(src, output, bmp.Width, bmp.Height, ImageStride); break;
                case "粉笔": Fenbi(src, output, bmp.Width, bmp.Height, ImageStride); break;
                case "线条2": Line(src, output, bmp.Width, bmp.Height, ImageStride); break;
            }

            return Bitmap2BitmapImage(dest);
        }

        #region 调节部分
        public static Bitmap bmp2;
        public static Bitmap dest2;
        public static BitmapData adata2;
        public static BitmapData data2;
        public static byte* src2;
        public static int ImageStride2;
        public static byte* output2;
        public static void inti(BitmapImage bi)
        {
            bmp2 = BitMapImage2Bitmap(bi);
            dest2 = (Bitmap)bmp2.Clone();
            adata2 = bmp2.LockBits(new Rectangle(0, 0, bmp2.Width, bmp2.Height), System.Drawing.Imaging.ImageLockMode.ReadWrite, bmp2.PixelFormat);
            data2 = dest2.LockBits(new Rectangle(0, 0, dest2.Width, dest2.Height), System.Drawing.Imaging.ImageLockMode.ReadWrite, dest2.PixelFormat);
            src2 = (byte*)adata2.Scan0;
            ImageStride2 = adata2.Stride;
            output2 = (byte*)data2.Scan0;
            bmp2.UnlockBits(adata2);
            dest2.UnlockBits(data2);
        }
        public static BitmapImage fun2(string name, int b)
        {
            switch (name)
            {
                case "亮度": setbri(src2, output2, bmp2.Height, bmp2.Width, ImageStride2, b); break;
                case "对比度": setCompare(src2, output2, bmp2.Height, bmp2.Width, ImageStride2, b); break;
            }
            return Bitmap2BitmapImage(dest2);
        }
        #endregion

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

        public static BitmapImage Canvas2BitmapImage(Canvas surface)
        {
            Transform transform = surface.LayoutTransform;
            surface.LayoutTransform = null;

            System.Windows.Size size = new System.Windows.Size(surface.Width, surface.Height);
            surface.Measure(size);
            surface.Arrange(new Rect(size));

            RenderTargetBitmap renderBitmap =
            new RenderTargetBitmap(
            1920,
            1080,
            96d,
            96d,
            PixelFormats.Pbgra32);
            renderBitmap.Render(surface);

            var ta = new CroppedBitmap(BitmapFrame.Create(renderBitmap), new Int32Rect(816, 72, 1000, 750));
            BitmapSource bitmapSource = ta;

            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            MemoryStream memoryStream = new MemoryStream();
            BitmapImage bImg = new BitmapImage();

            encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
            encoder.Save(memoryStream);

            memoryStream.Position = 0;
            bImg.BeginInit();
            bImg.StreamSource = new MemoryStream(memoryStream.ToArray());
            bImg.EndInit();
            memoryStream.Close();
            return bImg;
        }

        public static void printImage(BitmapImage bi)
        {
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bi));
            FileStream files = new FileStream("1.jpg", FileMode.Create, FileAccess.ReadWrite);
            encoder.Save(files);
            files.Close();

            PrintDocument pd = new PrintDocument();
            pd.DefaultPageSettings.PrinterSettings.PrinterName = "Canon SELPHY CP1200";
            PaperSize psize = new PaperSize();
            foreach (PaperSize i in pd.PrinterSettings.PaperSizes)
            {
                if (i.PaperName == "P 无边距 100x148mm 4x6\"") //无边距可正常居中，有边距0,0点位置需考虑边距
                {
                    psize = i;
                    break;

                }
                Console.WriteLine(i.PaperName);
            }
            pd.DefaultPageSettings.PaperSize = psize;
            pd.PrintPage += (s, args) =>
            {
                System.Drawing.Image i = System.Drawing.Image.FromFile("1.jpg");
                System.Drawing.Rectangle m = args.PageBounds;
                if (i.Width < i.Height)
                    i.RotateFlip(RotateFlipType.Rotate90FlipNone);

                if (i.Width >= i.Height)
                {
                    if ((double)i.Width / (double)i.Height <= (double)m.Width / (double)m.Height)
                    {
                        int w = (int)((double)i.Width / (double)i.Height * (double)m.Height);
                        int dx = (m.Width - w) / 2;
                        m.X = dx;
                        m.Y = 0;
                        m.Width = w;
                    }
                    else
                    {
                        int h = (int)((double)i.Height / (double)i.Width * (double)m.Width);
                        int dy = (m.Height - h) / 2;
                        m.X = 0;
                        m.Y = dy;
                        m.Height = h;
                    }
                }
                args.Graphics.DrawImage(i, m);
            };
            pd.Print();
        }

    }
}
