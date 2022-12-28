
using Prism.Events;
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
using System.Windows.Shapes;
using ToDoNote.Extension;

namespace ToDoNote.Views
{
    /// <summary>
    /// MainView.xaml 的交互逻辑
    /// </summary>
    public partial class MainView : Window
    {
        public MainView(IEventAggregator aggregator)
        {
            InitializeComponent(); btnMin.Click += (o, e) => { this.WindowState = WindowState.Minimized; };

            //注册等待消息的窗口
            aggregator.Register(arg =>
            {
                DialogHost.IsOpen = arg.IsOpen;
                if (DialogHost.IsOpen)
                {
                    DialogHost.DialogContent = new ProgressView();
                }
            });


            btnMax.Click += (o, e) =>
            {
                if (this.WindowState == WindowState.Maximized)
                    this.WindowState = WindowState.Normal;
                else this.WindowState = WindowState.Maximized;
            };

            btnClose.Click += (o, e) => { this.Close(); };

            ColorZone.MouseMove += (o, e) =>
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    this.DragMove();
                }
            };
            ColorZone.MouseDoubleClick += (o, e) =>
            {
                if (this.WindowState == WindowState.Normal)
                    this.WindowState = WindowState.Maximized;
                else this.WindowState = WindowState.Normal;
            };

            menuBar.SelectionChanged += (o, e) =>
            {
                drawhost.IsLeftDrawerOpen = false;
            };
        }
    }
}
