using Emgu.CV;
using Emgu.CV.CvEnum;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FU_UWP
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        //用于显示的图片
        BitmapImage MainBitmap = null;
        //摄像头和用于接收摄像头的Mat
        Capture cap;
        Mat capframe;
        Bitmap tt;

        bool isinpaishe = false;
        bool iscongpaiin = false;
        bool istakeaphoto = false;
        bool isdone = false;

        public MainWindow()
        {
            InitializeComponent();

            //初始化Emgucv和接收摄像头图像的Mat
            CvInvoke.UseOpenCL = true;
            capframe = new Mat();
            cap = new Capture();
            cap.SetCaptureProperty(CapProp.Fps, 30);
            cap.SetCaptureProperty(CapProp.FrameHeight, 720);
            cap.SetCaptureProperty(CapProp.FrameWidth, 960);
            cap.ImageGrabbed += Cap_ImageGrabbed;
            cap.Stop();

        }

        //退出
        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Q)
            {
                if (cap != null)
                {
                    cap.Stop();
                    cap.Dispose();
                    cap = null;
                }
                this.Close();
            }
        }

        //在主页点按钮时调用，使用动画切换场景
        void MainPageUpdate1()
        {
            ThicknessAnimation anim = new ThicknessAnimation();
            anim.From = new Thickness(462, 255, 1058, 0);
            anim.To = new Thickness(10, 465, 1710, 0);
            anim.Duration = TimeSpan.FromSeconds(0.36);
            paizhao.BeginAnimation(MarginProperty, anim);

            ThicknessAnimation anim2 = new ThicknessAnimation();
            anim2.From = new Thickness(1062, 250, 458, 430);
            anim2.To = new Thickness(1710, 460, 10, 420);
            anim2.Duration = TimeSpan.FromSeconds(0.36);
            shangchuan.BeginAnimation(MarginProperty, anim2);

            ThicknessAnimation anim3 = new ThicknessAnimation();
            anim3.From = new Thickness(0, 790, 0, 0);
            anim3.To = new Thickness(0, 1086, 0, -296);
            anim3.Duration = TimeSpan.FromSeconds(0.36);
            show_bar.BeginAnimation(MarginProperty, anim3);

            ThicknessAnimation anim4 = new ThicknessAnimation();
            anim4.From = new Thickness(462, 0, 0, 0);
            anim4.To = new Thickness(462, -252, 0, 0);
            anim4.Duration = TimeSpan.FromSeconds(0.36);
            FU_NAME.BeginAnimation(MarginProperty, anim4);

            DoubleAnimation daV = new DoubleAnimation(400, 200, new Duration(TimeSpan.FromSeconds(0.36)));
            paizhao.BeginAnimation(WidthProperty, daV);
            DoubleAnimation daV2 = new DoubleAnimation(400, 200, new Duration(TimeSpan.FromSeconds(0.36)));
            paizhao.BeginAnimation(HeightProperty, daV2);

            DoubleAnimation daV3 = new DoubleAnimation(400, 200, new Duration(TimeSpan.FromSeconds(0.36)));
            shangchuan.BeginAnimation(WidthProperty, daV3);
            DoubleAnimation daV4 = new DoubleAnimation(400, 200, new Duration(TimeSpan.FromSeconds(0.36)));
            shangchuan.BeginAnimation(HeightProperty, daV4);

            DoubleAnimation daV5 = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromSeconds(0.7)));
            paizhaokuang.BeginAnimation(OpacityProperty, daV5);
        }

        //返回上一级使用
        void MainPageUpdate2()
        {
            if(iscongpaiin)
            {
                ThicknessAnimation anim5 = new ThicknessAnimation();
                anim5.From = new Thickness(1710, 665, 0, 225);
                anim5.To = new Thickness(1920, 670, -200, 210);
                anim5.Duration = TimeSpan.FromSeconds(0.36);
                chongpai.BeginAnimation(MarginProperty, anim5);
                iscongpaiin = false;
            }
            if(istakeaphoto)
            {
                ThicknessAnimation anim6 = new ThicknessAnimation();
                anim6.From = new Thickness(0, 0, 1720, 0);
                anim6.To = new Thickness(10, 465, 1710, 0);
                anim6.Duration = TimeSpan.FromSeconds(0.36);
                paizhao.BeginAnimation(MarginProperty, anim6);

                ThicknessAnimation anim7 = new ThicknessAnimation();
                anim7.From = new Thickness(1216, 854, 504, 26);
                anim7.To = new Thickness(1710, 460, 10, 420);
                anim7.Duration = TimeSpan.FromSeconds(0.36);
                shangchuan.BeginAnimation(MarginProperty, anim7);

                ThicknessAnimation anim8 = new ThicknessAnimation();
                anim8.From = new Thickness(1920, 665, -200, 225);
                anim8.To = new Thickness(1710, 665, 10, 225);
                anim8.Duration = TimeSpan.FromSeconds(0.36);
                chongpai.BeginAnimation(MarginProperty, anim8);

                ThicknessAnimation anim9 = new ThicknessAnimation();
                anim9.From = new Thickness(806, 62, 0, 0);
                anim9.To = new Thickness(350, 80, 0, 0);
                anim9.Duration = TimeSpan.FromSeconds(0.36);
                paizhaokuang.BeginAnimation(MarginProperty, anim9);

                DoubleAnimation daV6 = new DoubleAnimation(1020, 1220, new Duration(TimeSpan.FromSeconds(0.36)));
                paizhaokuang.BeginAnimation(WidthProperty, daV6);
                DoubleAnimation daV7 = new DoubleAnimation(770, 920, new Duration(TimeSpan.FromSeconds(0.36)));
                paizhaokuang.BeginAnimation(HeightProperty, daV7);

                DoubleAnimation daV8 = new DoubleAnimation(1000, 1200, new Duration(TimeSpan.FromSeconds(0.36)));
                image.BeginAnimation(WidthProperty, daV8);
                DoubleAnimation daV9 = new DoubleAnimation(750, 900, new Duration(TimeSpan.FromSeconds(0.36)));
                image.BeginAnimation(HeightProperty, daV9);

                ThicknessAnimation anim5 = new ThicknessAnimation();
                anim5.From = new Thickness(266, 0, 0, 0);
                anim5.To = new Thickness(266, -1080, 0, 0);
                anim5.Duration = TimeSpan.FromSeconds(0.36);
                choose.BeginAnimation(MarginProperty, anim5);


            }
            else
            {
                ThicknessAnimation anim = new ThicknessAnimation();
                anim.From = new Thickness(10, 465, 1710, 0);
                anim.To = new Thickness(462, 255, 1058, 0);
                anim.Duration = TimeSpan.FromSeconds(0.36);
                paizhao.BeginAnimation(MarginProperty, anim);

                ThicknessAnimation anim2 = new ThicknessAnimation();
                anim2.From = new Thickness(1710, 460, 10, 420);
                anim2.To = new Thickness(1062, 250, 458, 430);
                anim2.Duration = TimeSpan.FromSeconds(0.36);
                shangchuan.BeginAnimation(MarginProperty, anim2);

                ThicknessAnimation anim3 = new ThicknessAnimation();
                anim3.From = new Thickness(0, 1086, 0, -296);
                anim3.To = new Thickness(0, 790, 0, 0);
                anim3.Duration = TimeSpan.FromSeconds(0.36);
                show_bar.BeginAnimation(MarginProperty, anim3);

                ThicknessAnimation anim4 = new ThicknessAnimation();
                anim4.From = new Thickness(462, -252, 0, 0);
                anim4.To = new Thickness(462, 0, 0, 0);
                anim4.Duration = TimeSpan.FromSeconds(0.36);
                FU_NAME.BeginAnimation(MarginProperty, anim4);

                DoubleAnimation daV = new DoubleAnimation(200, 400, new Duration(TimeSpan.FromSeconds(0.36)));
                paizhao.BeginAnimation(WidthProperty, daV);
                DoubleAnimation daV2 = new DoubleAnimation(200, 400, new Duration(TimeSpan.FromSeconds(0.36)));
                paizhao.BeginAnimation(HeightProperty, daV2);

                DoubleAnimation daV3 = new DoubleAnimation(200, 400, new Duration(TimeSpan.FromSeconds(0.36)));
                shangchuan.BeginAnimation(WidthProperty, daV3);
                DoubleAnimation daV4 = new DoubleAnimation(200, 400, new Duration(TimeSpan.FromSeconds(0.36)));
                shangchuan.BeginAnimation(HeightProperty, daV4);

                DoubleAnimation daV5 = new DoubleAnimation(1, 0, new Duration(TimeSpan.FromSeconds(0.7)));
                paizhaokuang.BeginAnimation(OpacityProperty, daV5);

            }
        }

        private void paizhao_Click(object sender, RoutedEventArgs e)
        {
            if(isdone)
            {
                ThicknessAnimation anim = new ThicknessAnimation();
                anim.From = new Thickness(10, 465, 1710, 0);
                anim.To = new Thickness(0, 0, 1720, 0);
                anim.Duration = TimeSpan.FromSeconds(0.36);
                paizhao.BeginAnimation(MarginProperty, anim);

                ThicknessAnimation anim2 = new ThicknessAnimation();
                anim2.From = new Thickness(1710, 460, 10, 420);
                anim2.To = new Thickness(1216, 854, 504, 26);
                anim2.Duration = TimeSpan.FromSeconds(0.36);
                shangchuan.BeginAnimation(MarginProperty, anim2);

                ThicknessAnimation anim3 = new ThicknessAnimation();
                anim3.From = new Thickness(1710, 665, 10, 225);
                anim3.To = new Thickness(1920, 665, -200, 225);
                anim3.Duration = TimeSpan.FromSeconds(0.36);
                chongpai.BeginAnimation(MarginProperty, anim3);

                ThicknessAnimation anim4 = new ThicknessAnimation();
                anim4.From = new Thickness(350, 80, 0, 0);
                anim4.To = new Thickness(806, 62, 0, 0);
                anim4.Duration = TimeSpan.FromSeconds(0.36);
                paizhaokuang.BeginAnimation(MarginProperty, anim4);

                DoubleAnimation daV = new DoubleAnimation(1220, 1020, new Duration(TimeSpan.FromSeconds(0.36)));
                paizhaokuang.BeginAnimation(WidthProperty, daV);
                DoubleAnimation daV2 = new DoubleAnimation(920, 770, new Duration(TimeSpan.FromSeconds(0.36)));
                paizhaokuang.BeginAnimation(HeightProperty, daV2);

                DoubleAnimation daV3 = new DoubleAnimation(1200, 1000, new Duration(TimeSpan.FromSeconds(0.36)));
                image.BeginAnimation(WidthProperty, daV3);
                DoubleAnimation daV4 = new DoubleAnimation(900, 750, new Duration(TimeSpan.FromSeconds(0.36)));
                image.BeginAnimation(HeightProperty, daV4);

                ThicknessAnimation anim5 = new ThicknessAnimation();
                anim5.From = new Thickness(266, -1080, 0, 0);
                anim5.To = new Thickness(266, 0, 0, 0);
                anim5.Duration = TimeSpan.FromSeconds(0.36);
                choose.BeginAnimation(MarginProperty, anim5);


                istakeaphoto = true;
                iscongpaiin = false;
                isdone = false;

                ((ImageBrush)shangchuan.Background).ImageSource = new BitmapImage(new Uri(@"..\..\images\图标\完成.png", UriKind.Relative));
                ((ImageBrush)chongpai.Background).ImageSource = new BitmapImage(new Uri(@"..\..\images\图标\重拍.png", UriKind.Relative));
            }
            //在主页的状态下
            else if (isinpaishe == false)
            {
                MainPageUpdate1();
                ((ImageBrush)paizhao.Background).ImageSource = new BitmapImage(new Uri(@"..\..\images\图标\返回2.png", UriKind.Relative));
                ((ImageBrush)shangchuan.Background).ImageSource = new BitmapImage(new Uri(@"..\..\images\图标\拍摄.png", UriKind.Relative));
                cap.Start();
                isinpaishe = true;
            }
            //不在主页的情况下
            else
            {
                cap.Stop();
                MainPageUpdate2();
                if (!istakeaphoto)
                {
                    ((ImageBrush)paizhao.Background).ImageSource = new BitmapImage(new Uri(@"..\..\images\图标\拍照.png", UriKind.Relative));
                    ((ImageBrush)shangchuan.Background).ImageSource = new BitmapImage(new Uri(@"..\..\images\图标\从相册中选择.png", UriKind.Relative));

                    isinpaishe = false;
                }
                if(istakeaphoto)
                {
                    istakeaphoto = false;
                    iscongpaiin = true;
                }
            }
        }

        //摄像头的函数
        MemoryStream ms = new MemoryStream();
        private void Cap_ImageGrabbed(object sender, EventArgs e)
        {
            cap.Retrieve(capframe);
            Dispatcher.Invoke(new Action(() =>
            {
                tt = capframe.Bitmap;
                tt.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                byte[] bytes = ms.GetBuffer();
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.StreamSource = new MemoryStream(bytes);
                bitmap.EndInit();
                image.Source = bitmap;
                
            }));

        }

        private void shangchuan_Click(object sender, RoutedEventArgs e)
        {
            //MainPageUpdate1();
            //用户确认后，选择取回照片时,打印
            if(isdone)
            {
                var tmp = new dayintishi();
                tmp.ShowDialog();
            }
            //拍过照了，说明在处理的页面
            else if(istakeaphoto)
            {
                MainPageUpdate2();
                ((ImageBrush)shangchuan.Background).ImageSource = new BitmapImage(new Uri(@"..\..\images\图标\打印.png", UriKind.Relative));
                ((ImageBrush)chongpai.Background).ImageSource = new BitmapImage(new Uri(@"..\..\images\图标\发送电子版.png", UriKind.Relative));
                isdone = true;
            }
            //如果重拍按钮在界面中，则该按钮的功能为完成
            else if(iscongpaiin)
            {
                ThicknessAnimation anim = new ThicknessAnimation();
                anim.From = new Thickness(10, 465, 1710, 0);
                anim.To = new Thickness(0, 0, 1720, 0);
                anim.Duration = TimeSpan.FromSeconds(0.36);
                paizhao.BeginAnimation(MarginProperty, anim);

                ThicknessAnimation anim2 = new ThicknessAnimation();
                anim2.From = new Thickness(1710, 460, 10, 420);
                anim2.To = new Thickness(1216, 854, 504, 26);
                anim2.Duration = TimeSpan.FromSeconds(0.36);
                shangchuan.BeginAnimation(MarginProperty, anim2);

                ThicknessAnimation anim3 = new ThicknessAnimation();
                anim3.From = new Thickness(1710, 665, 10, 225);
                anim3.To = new Thickness(1920, 665, -200, 225);
                anim3.Duration = TimeSpan.FromSeconds(0.36);
                chongpai.BeginAnimation(MarginProperty, anim3);

                ThicknessAnimation anim4 = new ThicknessAnimation();
                anim4.From = new Thickness(350, 80, 0, 0);
                anim4.To = new Thickness(806, 62, 0, 0);
                anim4.Duration = TimeSpan.FromSeconds(0.36);
                paizhaokuang.BeginAnimation(MarginProperty, anim4);

                DoubleAnimation daV = new DoubleAnimation(1220, 1020, new Duration(TimeSpan.FromSeconds(0.36)));
                paizhaokuang.BeginAnimation(WidthProperty, daV);
                DoubleAnimation daV2 = new DoubleAnimation(920, 770, new Duration(TimeSpan.FromSeconds(0.36)));
                paizhaokuang.BeginAnimation(HeightProperty, daV2);

                DoubleAnimation daV3 = new DoubleAnimation(1200, 1000, new Duration(TimeSpan.FromSeconds(0.36)));
                image.BeginAnimation(WidthProperty, daV3);
                DoubleAnimation daV4 = new DoubleAnimation(900, 750, new Duration(TimeSpan.FromSeconds(0.36)));
                image.BeginAnimation(HeightProperty, daV4);

                ThicknessAnimation anim5 = new ThicknessAnimation();
                anim5.From = new Thickness(266, -1080, 0, 0);
                anim5.To = new Thickness(266, 0, 0, 0);
                anim5.Duration = TimeSpan.FromSeconds(0.36);
                choose.BeginAnimation(MarginProperty, anim5);


                istakeaphoto = true;
                iscongpaiin = false;
            }
            //如果在拍摄中，而且重拍按钮不在界面中，则该按钮功能为拍摄
            else if(isinpaishe)
            {
                tt = capframe.Bitmap;
                cap.Stop();
                //cap.Dispose();
                tt.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                byte[] bytes = ms.GetBuffer();
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.StreamSource = new MemoryStream(bytes);
                bitmap.EndInit();
                MainBitmap = bitmap;
                image.Source = MainBitmap;
                ((ImageBrush)shangchuan.Background).ImageSource = new BitmapImage(new Uri(@"..\..\images\图标\完成.png", UriKind.Relative));
                ThicknessAnimation anim = new ThicknessAnimation();
                anim.From = new Thickness(1920, 670, -200, 210);
                anim.To = new Thickness(1710, 665, 0, 225);
                anim.Duration = TimeSpan.FromSeconds(0.36);
                chongpai.BeginAnimation(MarginProperty, anim);
                iscongpaiin = true;
            }
        }

        private void chongpai_Click(object sender, RoutedEventArgs e)
        {
            if (!isdone)
            {
                cap.Start();
                ThicknessAnimation anim = new ThicknessAnimation();
                anim.From = new Thickness(1710, 665, 10, 225);
                anim.To = new Thickness(1920, 670, -200, 210);
                anim.Duration = TimeSpan.FromSeconds(0.36);
                chongpai.BeginAnimation(MarginProperty, anim);
                ((ImageBrush)shangchuan.Background).ImageSource = new BitmapImage(new Uri(@"..\..\images\图标\拍摄.png", UriKind.Relative));
                iscongpaiin = false;
            }
            //发送电子版
            else
            {

            }
        }
    }
}
