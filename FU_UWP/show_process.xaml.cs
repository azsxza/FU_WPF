using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace FU_UWP
{
    /// <summary>
    /// show_process.xaml 的交互逻辑
    /// </summary>
    public partial class show_process : Window, INotifyPropertyChanged
    {
        private DispatcherTimer timer;
        private int currentRate = 0;
        public show_process()
        {
            InitializeComponent();
            this.DataContext = this;

            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 50);
            timer.Tick += timer_Tick;
            timer.Start();
            ResizeMode = ResizeMode.NoResize;
        }

        #region Properties

        private int successRate = 100;
        public int SuccessRate
        {
            get
            {
                return successRate;
            }
            set
            {
                if (value != successRate)
                {
                    successRate = value;
                    OnPropertyChanged("SuccessRate");
                }
            }
        }

        #endregion

        double i = 0;
        private void timer_Tick(object sender, EventArgs e)
        {
            currentRate = (int)i;
            i += 1;
            SuccessRate = currentRate * 100 / 100;
            if (SuccessRate == 100)
            {
                timer.Stop();
                Thread.Sleep(100);
                Close();
            }
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

    }
}
