using Octokit;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Xandrin.Core
{
    public class VersionManager
    {
        private long currentID = 61711184;
        private bool outdatedVersionByID = false;
        private MainWindow _mw;
        private string latestVersionDl = "";
        private string path = "";
        private string filename = "";
        private Release latestRelease = null;
        public VersionManager(MainWindow mw)
        {
            _mw = mw;
        }


        public async Task checkForUpdates()
        {
            // Github Version Control System based on release-id.
            // Does not update the program from any branch but instead the releases.
            var github = new GitHubClient(new ProductHeaderValue("Xandrin"));
            string username = "Montrii";
            string repos = "Xandrin";
            var releases = await github.Repository.Release.GetAll(username, repos);
            MessageBoxCreator mbc = new MessageBoxCreator();
            for (int i = 0; i < releases.Count; i++)
            {
                Console.WriteLine(releases[i].Id);
                if (i == 0 && releases[i].Id != currentID)
                {
                    outdatedVersionByID = true;
                    latestVersionDl = "github.com/" + username + "/" + repos + "/archive/refs/tags/" + releases[i].TagName + ".zip";
                    filename = releases[i].TagName + ".zip";
                    path = System.Reflection.Assembly.GetExecutingAssembly().Location.Replace(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + ".exe", "latestRelease\\");
                    latestRelease = releases[i];
                }
                
            }
            if(outdatedVersionByID)
            {

                await downloadLatestRelease();
                transferFilesToDirecoty(path, path.Replace("latestRelease", ""));
            }
            else
            {
                mbc.createCustomMessageBox(_mw, "Warning", "Version is Up-To-Date!");
            }
        }

        private async Task downloadLatestRelease()
        {
            MessageBoxCreator mbc = new MessageBoxCreator();
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
            await wc.DownloadFileTaskAsync(new Uri(latestRelease.ZipballUrl), download);
            ZipFile.ExtractToDirectory(download, download.Replace("latestRelease.zip", ""));
            mbc.createCustomMessageBox(_mw, "Warning", "Download extracted successfully!");
        }

        public void transferFilesToDirecoty(string sourceFolder, string destFolder)
        {
            if (!Directory.Exists(destFolder))
                Directory.CreateDirectory(destFolder);

            // Get Files & Copy
            string[] files = Directory.GetFiles(sourceFolder);
            foreach (string file in files)
            {
                string name = Path.GetFileName(file);

                // ADD Unique File Name Check to Below!!!!
                string dest = Path.Combine(destFolder, name);
                File.Copy(file, dest);
            }

            // Get dirs recursively and copy files
            string[] folders = Directory.GetDirectories(sourceFolder);
            foreach (string folder in folders)
            {
                string name = Path.GetFileName(folder);
                string dest = Path.Combine(destFolder, name);
                transferFilesToDirecoty(folder, dest);
            }
        }

    }
}
