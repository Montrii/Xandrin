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

namespace Xandrin.MVVM__MVC_.View
{
    /// <summary>
    /// Interaction logic for InfoView.xaml
    /// </summary>
    public partial class InfoView : UserControl
    {
        public InfoView()
        {
            
            InitializeComponent();

        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            InfoDataContext info = (InfoDataContext)this.DataContext;
            if(e.Key == Key.Enter)
            {
                if(textBoxTestOM.Text == "1")
                {
                    listBoxOnMission.Items.Add("OnMission Flag changed to 1");
                    listBoxOnMission.ScrollIntoView(listBoxOnMission.SelectedItem);
                }
            }
        }
    }
}
