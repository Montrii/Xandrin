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
using Xandrin.Core;
using Xandrin.MVVM__MVC_.Control;
using Xandrin.MVVM__MVC_.View;

namespace Xandrin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainViewModel m;
        MessageBoxCreator mbc = new MessageBoxCreator();
        public MainWindow()
        {
            InitializeComponent();
            VersionManager v = new VersionManager(this);
            m = (MainViewModel)this.DataContext;
            m.OnViewChanged += OnViewChanged;

            APICommunicator api = new APICommunicator();
            api.loginToDatabase("montri", "test123");
            
        }


        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private bool OnViewChanged(object newView)
        {
            m = (MainViewModel)this.DataContext;
            if(m.GtasaLoaded == false && newView.GetType() == typeof(InfoViewModel))
            {

                mbc.createCustomMessageBox(this, "Error 001", "Info cannot be opened when GTA:SA isn't loaded!");
                radioButtonInfo.IsChecked = false;
                radioButtonHome.IsChecked = true;
                return false;
            }
            else
            {
                mbc.createCustomMessageBox(this, "Changed!", "View was successfully changed.");
            }
            return true;
        }
    }

}
