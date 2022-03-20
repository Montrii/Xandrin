using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
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
        APICommunicator api = new APICommunicator();


        public MainWindow()
        {
            InitializeComponent();
            VersionManager v = new VersionManager(this);
            m = (MainViewModel)this.DataContext;
            v.checkForUpdates();
            Application.Current.SessionEnding += systemEnding;
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

        private void buttonRegister_Click(object sender, RoutedEventArgs e)
        {
            
            MessageBox.Show(api.registerToDatabase(new Account(textBoxUsername.Text, textBoxPassword.Text)));
        }

        private void buttonLogin_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(api.loginToDatabase(new Account(textBoxUsername.Text, textBoxPassword.Text)));
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            api.signOutOfDatabase(api.UsedAccount);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            api.signOutOfDatabase(api.UsedAccount);
        }

        private void systemEnding(object sender, SessionEndingCancelEventArgs e)
        {
            api.signOutOfDatabase(api.UsedAccount);
        }
    }

}
