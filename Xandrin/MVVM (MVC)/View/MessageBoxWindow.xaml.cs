using Octokit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
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

namespace Xandrin.MVVM__MVC_.View
{
    /// <summary>
    /// Interaction logic for MessageBoxWindow.xaml
    /// </summary>
    public partial class MessageBoxWindow : Window
    {
        MainWindow _mw;
        public MessageBoxWindow(MainWindow mw)
        {
            InitializeComponent();
            _mw = mw;
        }
        public void startDownload(Release latestRelease, string path)
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
                Directory.CreateDirectory(path);
            }
            else
            {
                Directory.CreateDirectory(path);
            }
            var wc = new WebClient();
            wc.Headers.Add(HttpRequestHeader.UserAgent, "MyUserAgent");
            string download = path + "latestRelease.zip";

            wc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
            wc.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);
            wc.DownloadFileAsync(new Uri(latestRelease.ZipballUrl), download);
            //ZipFile.ExtractToDirectory(download, download.Replace("latestRelease.zip", ""));
        }


        void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            double bytesIn = Convert.ToDouble(e.BytesReceived.ToString());
            double totalBytes = Convert.ToDouble(e.TotalBytesToReceive.ToString());
            double percentage = bytesIn / totalBytes * 100;
            Console.WriteLine(bytesIn);
            Console.WriteLine(totalBytes);
            progressBarDownload.Value = percentage;
        }
        void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            textBoxContent.Text = "Download completed!";
        }

        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
