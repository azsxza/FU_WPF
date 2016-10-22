using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace FU_UWP
{
    /// <summary>
    /// dayintishi.xaml 的交互逻辑
    /// </summary>
    public partial class dayintishi : Window
    {
        public dayintishi()
        {
            InitializeComponent();
            DoubleAnimation daV5 = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromSeconds(0.4)));
            main_grid.BeginAnimation(OpacityProperty, daV5);
        }


        private void button_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation daV5 = new DoubleAnimation(1, 0, new Duration(TimeSpan.FromSeconds(0.4)));
            main_grid.BeginAnimation(OpacityProperty, daV5);
            Timer tmr = new Timer(1000 * 0.42);
            tmr.Elapsed += delegate
            {
                tmr.Stop();
                Dispatcher.Invoke(DispatcherPriority.Normal, (Action)delegate ()
                {
                    Close();
                });
            };
            tmr.Start();
        }

        private void textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            //ShowInputPanel();

        }

        public static int ShowInputPanel()
        {
            try
            {
                dynamic file = "C:\\Program Files\\Common Files\\microsoft shared\\ink\\TabTip.exe";
                if (!System.IO.File.Exists(file))
                    return -1;
                Process.Start(file);
                return 1;
            }
            catch (Exception)
            {
                return 255;
            }
        }

        private void textBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //ShowInputPanel();
        }
    }
}
