using Emgu.CV;
using Emgu.CV.CvEnum;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace FU_UWP
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        //用于显示的图片
        BitmapImage MainBitmap = null;
        List<BitmapImage> history;
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
            onstart();
            //初始化Emgucv和接收摄像头图像的Mat
            CvInvoke.UseOpenCL = true;
            capframe = new Mat();
            cap = new Capture();
            cap.SetCaptureProperty(CapProp.Fps, 30);
            cap.SetCaptureProperty(CapProp.FrameHeight, 720);
            cap.SetCaptureProperty(CapProp.FrameWidth, 960);
            cap.ImageGrabbed += Cap_ImageGrabbed;
            cap.Stop();
            history = new List<BitmapImage>();

            this.Loaded += new RoutedEventHandler(MainWindow_Loaded);

            slider.ValueChanged += Slider_ValueChanged;
        }


        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            paizhaokuang.ManipulationStarting += new EventHandler<ManipulationStartingEventArgs>(image_ManipulationStarting);
            paizhaokuang.ManipulationDelta += new EventHandler<ManipulationDeltaEventArgs>(image_ManipulationDelta);
            paizhaokuang.ManipulationInertiaStarting += new EventHandler<ManipulationInertiaStartingEventArgs>(canvas_ManipulationInertiaStarting);
        }
        void canvas_ManipulationInertiaStarting(object sender, ManipulationInertiaStartingEventArgs e)
        {
            e.TranslationBehavior = new InertiaTranslationBehavior()
            {
                InitialVelocity = e.InitialVelocities.LinearVelocity,
                DesiredDeceleration = 10.0 * 96.0 / (1000.0 * 1000.0)
            };
            e.ExpansionBehavior = new InertiaExpansionBehavior()
            {
                InitialVelocity = e.InitialVelocities.ExpansionVelocity,
                DesiredDeceleration = 0.1 * 96 / 1000.0 * 1000.0
            };
            e.RotationBehavior = new InertiaRotationBehavior()
            {
                InitialVelocity = e.InitialVelocities.AngularVelocity,
                DesiredDeceleration = 720 / (1000.0 * 1000.0)
            };
            e.Handled = true;
        }
        UIElement last;
        void image_ManipulationStarting(object sender, ManipulationStartingEventArgs e)
        {
            var uie = e.OriginalSource as UIElement;
            if (uie != null)
            {
                if (last != null) Canvas.SetZIndex(last, 0);
                Canvas.SetZIndex(uie, 2);
                last = uie;
            }
            e.ManipulationContainer = paizhaokuang;
            e.Handled = true;       
        }
        void image_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            var element = e.Source as FrameworkElement;
            if (element != null)
            {
                var deltaManipulation = e.DeltaManipulation;
                var matrix = ((MatrixTransform)element.RenderTransform).Matrix;
                System.Windows.Point center = new System.Windows.Point(element.ActualWidth / 2, element.ActualHeight / 2);
                center = matrix.Transform(center);
                matrix.ScaleAt(deltaManipulation.Scale.X, deltaManipulation.Scale.Y, center.X, center.Y);
                matrix.RotateAt(e.DeltaManipulation.Rotation, center.X, center.Y);
                matrix.Translate(e.DeltaManipulation.Translation.X, e.DeltaManipulation.Translation.Y);
                ((MatrixTransform)element.RenderTransform).Matrix = matrix;
                e.Handled = true;
                if (e.IsInertial)
                {
                    Rect containingRect = new Rect(((FrameworkElement)e.ManipulationContainer).RenderSize);
                    Rect shapeBounds = element.RenderTransform.TransformBounds(new Rect(element.RenderSize));
                    if (e.IsInertial && !containingRect.Contains(shapeBounds))
                    {
                        e.ReportBoundaryFeedback(e.DeltaManipulation);       
                        e.Complete();
                    }
                }
            }
        }

        void onstart()
        {
            #region 初始化算法列表
            String line;
            #region 风格模仿
            StreamReader sr = new StreamReader("..\\风格模仿.txt", Encoding.Default);
            while ((line = sr.ReadLine()) != null)
            {
                ImageShow Is = new ImageShow();
                Is.image.Source = new BitmapImage(new Uri(@"..\..\models\" + line + ".jpg", UriKind.Relative));
                Is.textBlock.Text = line;
                Is.MouseDown += fengetrans;
                stack_fenggemofang.Children.Add(Is);
                stack_fenggemofang.Height += 300;
            }
            #endregion
            #region 滤镜
            StreamReader sr2 = new StreamReader("..\\滤镜.txt", Encoding.Default);
            while ((line = sr2.ReadLine()) != null)
            {
                ImageShow Is = new ImageShow();
                Is.image.Source = new BitmapImage(new Uri(@"..\..\images\QQ截图20161025211605.png", UriKind.Relative));
                Is.textBlock.Text = line;
                Is.MouseDown += lvjingtrans;
                stack_lvjing.Children.Add(Is);
                stack_lvjing.Height += 300;
            }
            #endregion
            #region 贴纸
            StreamReader sr3 = new StreamReader("..\\贴纸.txt", Encoding.Default);
            while ((line = sr3.ReadLine()) != null)
            {
                ImageShow Is = new ImageShow();
                Is.image.Source = new BitmapImage(new Uri(@"..\..\images\贴纸\" + line + ".png", UriKind.Relative));
                Is.textBlock.Text = line;
                Is.MouseDown += tiezhitrans;
                stack_tiezhi.Children.Add(Is);
                stack_tiezhi.Height += 300;
            }
            #endregion
            #region 调整
            StreamReader sr4 = new StreamReader("..\\调整.txt", Encoding.Default);
            while ((line = sr4.ReadLine()) != null)
            {
                ImageShow Is = new ImageShow();
                Is.image.Source = new BitmapImage(new Uri(@"..\..\images\QQ截图20161025211605.png", UriKind.Relative));
                Is.textBlock.Text = line;
                Is.MouseDown += tiaozhengtrans;
                stack_tiaozheng.Children.Add(Is);
                stack_tiaozheng.Height += 300;
            }
            #endregion
            #endregion
        }

        string currenttiaozheng = "";
        private void tiaozhengtrans(object sender, MouseButtonEventArgs e)
        {
            currenttiaozheng = ((ImageShow)sender).textBlock.Text;
            Functions.inti(MainBitmap);
        }
        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            image.Source = Functions.fun2(currenttiaozheng, (int)slider.Value);
        }

        //贴纸的按钮点击触发
        private void tiezhitrans(object sender, MouseButtonEventArgs e)
        {
            //var t = new System.Windows.Controls.Image();
            //t.Source= new BitmapImage(new Uri(@"..\..\images\贴纸\" + ((ImageShow)sender).textBlock.Text + ".png", UriKind.Relative));
            //t.RenderTransform = new MatrixTransform(1.5929750047527, 0.585411309251951, -0.585411309251951, 1.5929750047527, 564.691807426081, 79.4658072348299);
            //t.Width = 150;
            //t.IsManipulationEnabled = true;
            //t.SetValue(Canvas.TopProperty, 1.0);
            //t.SetValue(Canvas.LeftProperty, 1.0);
            //paizhaokuang.Children.Add(t);
            var t = new tiezhikuang();
            t.main1.Source = new BitmapImage(new Uri(@"..\..\images\贴纸\" + ((ImageShow)sender).textBlock.Text + ".png", UriKind.Relative));
            t.RenderTransform= new MatrixTransform(1, 0, 0, 1, 455, 335);
            t.main1.Width = 300;
            t.IsManipulationEnabled = true;
            t.SetValue(Canvas.TopProperty, 1.0);
            t.SetValue(Canvas.LeftProperty, 1.0);
            paizhaokuang.Children.Add(t);
        }
        //滤镜的按钮的点击触发
        private void lvjingtrans(object sender, MouseButtonEventArgs e)
        {
            var tmp = Functions.fun(MainBitmap, ((ImageShow)sender).textBlock.Text);
            image.Source = tmp;
            history.Add(tmp);
        }
        //风格转换的按钮的点击触发
        private void fengetrans(object sender, MouseButtonEventArgs e)
        {
            fenggebianhuan(((ImageShow)sender).textBlock.Text);
        }

        void fenggebianhuan(string name)
        {
            new Thread(() =>
            {
                JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(MainBitmap));
                FileStream files = new FileStream("1.jpg", FileMode.Create, FileAccess.ReadWrite);
                encoder.Save(files);
                files.Close();
                System.Diagnostics.Process p = new System.Diagnostics.Process();
                p.StartInfo.FileName = "cmd.exe";
                p.StartInfo.UseShellExecute = false;    //是否使用操作系统shell启动
                p.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
                p.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
                p.StartInfo.RedirectStandardError = true;//重定向标准错误输出
                p.StartInfo.CreateNoWindow = true;//不显示程序窗口
                p.Start();

                p.StandardInput.WriteLine("python video.py 1.jpg -m models/" + name + ".model" + "&exit");
                p.WaitForExit();
                p.Close();


                Dispatcher.Invoke(new Action(() =>
                {
                    BitmapImage tmp = GetImage(System.IO.Directory.GetCurrentDirectory() + "\\out.jpg");
                    image.Source = tmp;
                    history.Add(tmp);
                }));
                File.Delete("out.jpg");
            }).Start();
        }

        public static BitmapImage GetImage(string imagePath)
        {
            BitmapImage bitmap = new BitmapImage();
            if (File.Exists(imagePath))
            {
                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.OnLoad;

                using (Stream ms = new MemoryStream(File.ReadAllBytes(imagePath)))
                {
                    bitmap.StreamSource = ms;
                    bitmap.EndInit();
                    bitmap.Freeze();
                }
            }
            return bitmap;
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
            //重拍按钮在界面中时说明在拍照界面
            if(iscongpaiin)
            {
                ThicknessAnimation anim5 = new ThicknessAnimation();
                anim5.From = new Thickness(1710, 665, 0, 225);
                anim5.To = new Thickness(1920, 670, -200, 210);
                anim5.Duration = TimeSpan.FromSeconds(0.36);
                chongpai.BeginAnimation(MarginProperty, anim5);
                iscongpaiin = false;
            }
            //拍好照了，说明在处理界面,同时 切换到打印的确认界面也用这个
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

                ThicknessAnimation anim4 = new ThicknessAnimation();
                anim4.From = new Thickness(1470, 854, 250, 26);
                anim4.To = new Thickness(1470, 1080, 250, -200);
                anim4.Duration = TimeSpan.FromSeconds(0.36);
                chexiao.BeginAnimation(MarginProperty, anim4);
            }
            //其他情况 在拍照界面
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

        //在最后完成后返回到主页时使用
        public void BacktoMain()
        {
            isinpaishe = false;
            iscongpaiin = true;
            istakeaphoto = false;
            isdone = false;
            MainPageUpdate2();
            ((ImageBrush)paizhao.Background).ImageSource = new BitmapImage(new Uri(@"..\..\images\图标\拍照.png", UriKind.Relative));
            ((ImageBrush)shangchuan.Background).ImageSource = new BitmapImage(new Uri(@"..\..\images\图标\上传.png", UriKind.Relative));
            ((ImageBrush)chongpai.Background).ImageSource = new BitmapImage(new Uri(@"..\..\images\图标\重拍.png", UriKind.Relative));
        }

        private void paizhao_Click(object sender, RoutedEventArgs e)
        {
            //在打印确定页面，功能为返回上一级
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

                ThicknessAnimation anim6 = new ThicknessAnimation();
                anim6.From = new Thickness(1470, 1080, 250, -200);
                anim6.To = new Thickness(1470, 854, 250, 26);
                anim6.Duration = TimeSpan.FromSeconds(0.36);
                chexiao.BeginAnimation(MarginProperty, anim6);

                chexiao2.Visibility = Visibility.Visible;

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

                chexiao2.Visibility = Visibility.Hidden;
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
                BacktoMain();
            }
            //拍过照了，说明在处理的页面
            else if(istakeaphoto)
            {
                MainPageUpdate2();
                ((ImageBrush)shangchuan.Background).ImageSource = new BitmapImage(new Uri(@"..\..\images\图标\打印.png", UriKind.Relative));
                ((ImageBrush)chongpai.Background).ImageSource = new BitmapImage(new Uri(@"..\..\images\图标\发送电子版.png", UriKind.Relative));
                isdone = true;
                chexiao2.Visibility = Visibility.Hidden;
                MainBitmap = (BitmapImage)image.Source;
                switch (currentlist)
                {
                    case "fenggemofangbutton": chooselistmoveoin(fenggemofang); break;
                    case "lvjingbutton": chooselistmoveoin(lvjing); break;
                    case "tiaozhengbutton": chooselistmoveoin(tiaozheng); break;
                    case "tiezhibutton": chooselistmoveoin(tiezhi); break;
                }
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

                ThicknessAnimation anim6 = new ThicknessAnimation();
                anim6.From = new Thickness(1470, 1080, 250, -200);
                anim6.To = new Thickness(1470, 854, 250, 26);
                anim6.Duration = TimeSpan.FromSeconds(0.36);
                chexiao.BeginAnimation(MarginProperty, anim6);

                chexiao2.Visibility = Visibility.Visible;

                istakeaphoto = true;
                iscongpaiin = false;
            }
            //如果在拍摄中，而且重拍按钮不在界面中，则该按钮功能为拍摄
            else if(isinpaishe)
            {
                //capframe = CvInvoke.Imread(@"..\..\images\psb.jpg", LoadImageType.AnyColor);
                tt = capframe.Bitmap;
                cap.Stop();

                #region 拍照时的闪烁
                shan.Visibility = Visibility.Visible;
                System.Timers.Timer tmr = new System.Timers.Timer(1000 * 0.25);
                bool once = false;
                tmr.Elapsed += delegate
                {
                    if (!once)
                    {
                        Dispatcher.Invoke(DispatcherPriority.Normal, (Action)delegate ()
                      {
                          DoubleAnimation daV6 = new DoubleAnimation(1, 0, new Duration(TimeSpan.FromSeconds(0.25)));
                          shan.BeginAnimation(OpacityProperty, daV6);
                      });
                        once = true;
                    }
                    else
                    {
                        Dispatcher.Invoke(DispatcherPriority.Normal, (Action)delegate ()
                        {
                            shan.Visibility = Visibility.Hidden;
                        });
                        tmr.Stop();
                    }
                };
                DoubleAnimation daV5 = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromSeconds(0.25)));
                shan.BeginAnimation(OpacityProperty, daV5);
                tmr.Start();
                #endregion

                tt.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                byte[] bytes = ms.GetBuffer();
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.StreamSource = new MemoryStream(bytes);
                bitmap.EndInit();
                MainBitmap = bitmap;
                image.Source = MainBitmap;
                history.Add(bitmap);
                ((ImageBrush)shangchuan.Background).ImageSource = new BitmapImage(new Uri(@"..\..\images\图标\完成.png", UriKind.Relative));
                ThicknessAnimation anim = new ThicknessAnimation();
                anim.From = new Thickness(1920, 670, -200, 210);
                anim.To = new Thickness(1710, 665, 0, 225);
                anim.Duration = TimeSpan.FromSeconds(0.36);
                chongpai.BeginAnimation(MarginProperty, anim);
                iscongpaiin = true;

                //在另一个线程对列表进行图片更新
                int sum = stack_lvjing.Children.Count;
                new Thread(() =>
                {
                    Mat mini = new Mat();
                    CvInvoke.Resize(capframe, mini, new System.Drawing.Size(200, 200));
                    var bmp = mini.Bitmap;
                    for(int i=0;i<sum;i++)
                    {
                        Dispatcher.Invoke(new Action(() =>
                        {
                            string text = ((ImageShow)stack_lvjing.Children[i]).textBlock.Text;
                            ((ImageShow)stack_lvjing.Children[i]).image.Source = Functions.fun(mini.Bitmap, text);
                        }));
                        Thread.Sleep(5);
                    }
                }).Start();
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

        //列表那的逻辑代码
        string currentlist = "";
        public void chooselistmoveout(ScrollViewer sc)
        {
            ThicknessAnimation anim2 = new ThicknessAnimation();
            anim2.From = new Thickness(315, 0, 0, 0);
            anim2.To = new Thickness(0, 0, 0, 0);
            anim2.Duration = TimeSpan.FromSeconds(0.36);
            sc.BeginAnimation(MarginProperty, anim2);
        }
        public void chooselistmoveoin(ScrollViewer sc)
        {
            ThicknessAnimation anim2 = new ThicknessAnimation();
            anim2.From = new Thickness(0, 0, 0, 0);
            anim2.To = new Thickness(315, 0, 0, 0);
            anim2.Duration = TimeSpan.FromSeconds(0.36);
            sc.BeginAnimation(MarginProperty, anim2);
        }
        private void Algorithomlist_click(object sender, RoutedEventArgs e)
        {
            currentlist = ((Button)sender).Name;
            switch(currentlist)
            {
                case "fenggemofangbutton": chooselistmoveout(fenggemofang);break;
                case "lvjingbutton": chooselistmoveout(lvjing); break;
                case "tiaozhengbutton": chooselistmoveout(tiaozheng); break;
                case "tiezhibutton": chooselistmoveout(tiezhi); break;
            }
            DoubleAnimation daV6 = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromSeconds(0.25)));
            chexiao2.BeginAnimation(OpacityProperty, daV6);
        }
        private void SCManipulationBoundaryFeedback(object sender, ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
        }
        private void chexiao2_Click(object sender, RoutedEventArgs e)
        {
            switch (currentlist)
            {
                case "fenggemofangbutton": chooselistmoveoin(fenggemofang); break;
                case "lvjingbutton": chooselistmoveoin(lvjing); break;
                case "tiaozhengbutton": chooselistmoveoin(tiaozheng); break;
                case "tiezhibutton": chooselistmoveoin(tiezhi); break;
            }
            currentlist = "";
            DoubleAnimation daV6 = new DoubleAnimation(1, 0, new Duration(TimeSpan.FromSeconds(0.25)));
            chexiao2.BeginAnimation(OpacityProperty, daV6);
            MainBitmap = (BitmapImage)image.Source;
        }

        private void chexiao_Click(object sender, RoutedEventArgs e)
        {
            if (history.Count >= 2)
            {
                image.Source = history[history.Count - 2];
                MainBitmap = history[history.Count - 2];
                history.Remove(history[history.Count - 1]);
            }
        }
    }
}
