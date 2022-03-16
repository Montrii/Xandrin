using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
using Xandrin.GTA;
using Xandrin.MVVM__MVC_.Control;

namespace Xandrin.MVVM__MVC_.View
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        SAHandler sa;
        public HomeView()
        {

            InitializeComponent();
            sa = new SAHandler();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            HomeViewModel hvm = (HomeViewModel)this.DataContext;
            MainViewModel m = (MainViewModel)hvm.M;
            m.GtasaLoaded = sa.findGTA();
            MessageBox.Show(Convert.ToString(sa.getGTAVersion()));
        }
    }
}
