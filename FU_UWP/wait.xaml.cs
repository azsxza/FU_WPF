using System;
using System.Collections.Generic;
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
    /// Interaction logic for wait.xaml
    /// </summary>
    public partial class wait : Window
    {
        public wait()
        {
            InitializeComponent();
            DoubleAnimation daV5 = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromSeconds(0.4)));
            main_grid.BeginAnimation(OpacityProperty, daV5);
        }
        public void close1()
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
    }
}
