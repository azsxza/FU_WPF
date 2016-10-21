using System;
using System.Collections.Generic;
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
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Q)
                this.Close();
        }
        void MainPageUpdate1()
        {
            ThicknessAnimation anim = new ThicknessAnimation();
            anim.From = new Thickness(462, 255, 1058, 0);
            anim.To = new Thickness(10, 465, 1710, 0);
            anim.Duration = TimeSpan.FromSeconds(0.36);
            paizhao.BeginAnimation(MarginProperty, anim);

            ThicknessAnimation anim2 = new ThicknessAnimation();
            anim2.From = new Thickness(1127, 250, 393, 430);
            anim2.To = new Thickness(1710, 460, 10, 420);
            anim2.Duration = TimeSpan.FromSeconds(0.36);
            shangchuan.BeginAnimation(MarginProperty, anim2);

            DoubleAnimation daV = new DoubleAnimation(400, 200, new Duration(TimeSpan.FromSeconds(0.36)));
            paizhao.BeginAnimation(WidthProperty, daV);
            DoubleAnimation daV2 = new DoubleAnimation(400, 200, new Duration(TimeSpan.FromSeconds(0.36)));
            paizhao.BeginAnimation(HeightProperty, daV2);

            DoubleAnimation daV3 = new DoubleAnimation(400, 200, new Duration(TimeSpan.FromSeconds(0.36)));
            shangchuan.BeginAnimation(WidthProperty, daV3);
            DoubleAnimation daV4 = new DoubleAnimation(400, 200, new Duration(TimeSpan.FromSeconds(0.36)));
            shangchuan.BeginAnimation(HeightProperty, daV4);
        }

        private void ImageButtonMouseEnter(object sender, RoutedEventArgs e)
        {
            
        }
        private void ImageButtonMouseLeave(object sender, RoutedEventArgs e)
        {

        }

        private void paizhao_Click(object sender, RoutedEventArgs e)
        {
            MainPageUpdate1();
            //((ImageBrush)paizhao.Background).ImageSource = new BitmapImage(new Uri(@"..\..\images\图标\返回.png"));
        }

        private void shangchuan_Click(object sender, RoutedEventArgs e)
        {
            MainPageUpdate1();
        }
    }
}
