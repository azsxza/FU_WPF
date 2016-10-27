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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FU_UWP
{
    /// <summary>
    /// tiezhikuang.xaml 的交互逻辑
    /// </summary>
    public partial class tiezhikuang : UserControl
    {
        public tiezhikuang()
        {
            InitializeComponent();
        }

        private void image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //((Canvas)Parent).Children.Remove(this);
        }

        private void image11_TouchDown(object sender, TouchEventArgs e)
        {
            ((Canvas)Parent).Children.Remove(this);
        }

        private void image11_Copy_TouchDown(object sender, TouchEventArgs e)
        {
            border1.Visibility = Visibility.Hidden;
            image11.Visibility = Visibility.Hidden;
            image11_Copy.Visibility = Visibility.Hidden;
            this.IsManipulationEnabled = false;
            SetValue(Panel.ZIndexProperty, 1);
        }
    }
}
