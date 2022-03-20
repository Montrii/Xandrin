using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Xandrin.MVVM__MVC_.View;

namespace Xandrin.Core
{
    public class MessageBoxCreator
    {
        private MessageBoxWindow window;

        public MessageBoxWindow Window { get => window; set => window = value; }

        public void createCustomMessageBox(MainWindow mw, string titel, string content)
        {
            if(window == null)
            {
                window = new MessageBoxWindow(mw);
                window.textBoxTitle.Text = titel;
                window.textBoxContent.Text = content;
                window.ShowInTaskbar = false;
                window.Owner = mw;
                window.Topmost = true;
                window.Top = mw.Top + 200;
                window.Left = mw.Left + 275;
                window.Show();
            }
            else
            {
                window.Close();
                window = new MessageBoxWindow(mw);
                window.ShowInTaskbar = false;
                window.textBoxTitle.Text = titel;
                window.textBoxContent.Text = content;
                window.Owner = mw;
                window.Topmost = true;
                window.Top = mw.Top + 200;
                window.Left = mw.Left + 275;
                window.Show();
            }
        }
    }
}
