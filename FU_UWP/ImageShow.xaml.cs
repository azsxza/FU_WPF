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
    /// ImageShow.xaml 的交互逻辑
    /// </summary>
    public partial class ImageShow : UserControl
    {
        public ImageShow()
        {
            InitializeComponent();
        }

        #region 正常状态下显示的背景图片  
        public ImageSource ImageSourceNormal
        {
            get { return (ImageSource)GetValue(ImageSourceNormalProperty); }
            set { SetValue(ImageSourceNormalProperty, value); }
        }

        public static readonly DependencyProperty ImageSourceNormalProperty =
            DependencyProperty.Register("ImageSourceNormal", typeof(ImageSource), typeof(ImageShow),
            new PropertyMetadata(new BitmapImage(new Uri(@"..\..\images\图标\拍摄.png", UriKind.Relative))));
        #endregion
    }
}
