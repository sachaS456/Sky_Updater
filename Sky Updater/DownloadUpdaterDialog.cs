/*--------------------------------------------------------------------------------------------------------------------
 Copyright (C) 2022 Himber Sacha

 This program is free software: you can redistribute it and/or modify
 it under the +terms of the GNU General Public License as published by
 the Free Software Foundation, either version 3 of the License, or
 any later version.

 This program is distributed in the hope that it will be useful,
 but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 GNU General Public License for more details.

 You should have received a copy of the GNU General Public License
 along with this program.  If not, see https://www.gnu.org/licenses/gpl-3.0.html. 

--------------------------------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using Sky_UI;
using System.Threading;
using System.IO;
using System.Diagnostics;

namespace Sky_Updater
{
    public sealed class DownloadUpdaterDialog : Sky_UI.Rectangle
    {
        private Sky_UI.ProgressBar progressBar1;
        private Label label1;
        private Label label2;
        private string App;
        private delegate void progressBarInvokeHandler(int value);
        private delegate void labelInvokeHandler(string text);
        private readonly sbyte lang;

        public DownloadUpdaterDialog(sbyte langage, string app)
        {
            App = app;
            lang = langage;
            InitializeComponent();

            Thread thread = new Thread(DownloadUpdate);
            thread.Priority = ThreadPriority.Highest;
            thread.Start();
        }

        private void progressBar1ValueSet(int value)
        {
            progressBar1.SetValuePixels(value);
        }

        private void label2TextSet(string text)
        {
            this.label2.Text = text;
        }

        private void ProgressChanged(long? totalFileSize, long totalBytesDownloaded, double? progressPercentage, int nbFile, int FileDownloaded)
        {
            if (label2.InvokeRequired)
            {
                if (lang == 0) // FR
                {
                    label2.Invoke(new labelInvokeHandler(label2TextSet), "Fichier : " + FileDownloaded + " / " + nbFile);
                }
                else // EN
                {
                    label2.Invoke(new labelInvokeHandler(label2TextSet), "File : " + FileDownloaded + " / " + nbFile);
                }
            }
            else
            {
                if (lang == 0)
                {
                    label2TextSet("Fichier : " + FileDownloaded + " / " + nbFile);
                }
                else
                {
                    label2TextSet("File : " + FileDownloaded + " / " + nbFile);
                }
            }

            if (progressBar1.InvokeRequired)
            {
                progressBar1.Invoke(new progressBarInvokeHandler(progressBar1ValueSet), FileDownloaded * progressBar1.Width / nbFile);
            }
            else
            {
                progressBar1ValueSet(FileDownloaded * progressBar1.Width / nbFile);
            }
        }

        private void DownloadCompleted()
        {
            Stream streamWriter = new FileStream(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\" + App + @"\"+ App + " setup x64.exe", FileMode.Create, 
                FileAccess.Write, FileShare.None);
            byte[] buffer = new byte[8192];
            int nbFile = Directory.EnumerateFiles(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\" + App + @"\" + App + " setup x64").Count();

            for (int index = 0; index < nbFile; index++)
            {
                Stream streamReader = new FileStream(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\" + App + @"\" + App + @" setup x64\" + index, FileMode.Open,
                    FileAccess.Read, FileShare.None);

                bool FileWrite = true;
                while (FileWrite)
                {
                    int ByteRead = streamReader.Read(buffer, 0, buffer.Length);

                    if (ByteRead == 0)
                    {
                        FileWrite = false;
                        break;
                    }

                    streamWriter.Write(buffer, 0, ByteRead);
                }

                streamReader.Close();
                File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\" + App + @"\" + App + @" setup x64\" + index);
            }

            streamWriter.Close();
            buffer = null;

            if (lang == 0)
            {
                MessageBox.Show("Attention! Tous les processus" + App + " seront fermés.", App, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Warning! All " + App + " processes will be closed.", App, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            Process process = new Process();
            process.StartInfo.UseShellExecute = true;
            if (Environment.Is64BitProcess)
            {
                process.StartInfo.FileName = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\" + App + @"\" + App + " setup x64.exe";
            }
            else
            {
                process.StartInfo.FileName = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\" + App + @"\" + App + " setup x86.exe";
            }

            for (bool NoError = false; NoError == false;)
            {
                try
                {
                    process.Start();
                    NoError = true;
                }
                catch
                {
                    NoError = false;
                }
            }

            process.Close();

            foreach (Process i in Process.GetProcessesByName(App))
            {
                if (i.Id != Process.GetCurrentProcess().Id)
                {
                    i.Kill();
                }
            }

            Environment.Exit(0);
        }

        private void DownloadUpdate()
        {
            if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\" + App))
            {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\" + App);
            }

            if (Environment.Is64BitProcess)
            {
                if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\" + App + @"\" + App + " setup x64.exe"))
                {
                    File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\" + App + @"\" + App + " setup x64.exe");
                }

                if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\" + App + @"\" + App + " setup x64"))
                {
                    Directory.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\" + App + @"\" + App + " setup x64", true);
                }

                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\" + App + @"\" + App + " setup x64");

                int nbFile = Convert.ToInt32(Sky_Updater.Update.DownloadString("https://serie-Sky.netlify.app/Download/" + App + "/" + App + " setup x64/NbFile.txt"));
                List<string> stringList = new List<string>();

                for (int index = 0; index <= nbFile; index++)
                {
                    stringList.Add("https://serie-Sky.netlify.app/Download/" + App + "/" + App + " setup x64/" + index);
                }

                Downloader downloader = new Downloader(stringList.ToArray(), Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\" + App + @"\" + App + " setup x64");
                downloader.ProgressChanged += new ProgressChangedHandler(ProgressChanged);
                downloader.DownloadCompleted += new DownloadCompletedHandler(DownloadCompleted);
                downloader.StartDownloadListAsync();
            }
            else
            {
                if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\" + App + @"\" + App + " setup x86.exe"))
                {
                    File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\" + App + @"\" + App + " setup x86.exe");
                }

                if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\" + App + @"\" + App + " setup x86"))
                {
                    Directory.Delete(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\" + App + @"\" + App + " setup x86", true);
                }

                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\" + App + @"\" + App + " setup x86");

                int nbFile = Convert.ToInt32(Sky_Updater.Update.DownloadString("https://serie-Sky.netlify.app/Download/" + App + "/" + App + @" setup x86/NbFile.txt"));
                List<string> stringList = new List<string>();

                for (int index = 0; index <= nbFile; index++)
                {
                    stringList.Add("https://serie-Sky.netlify.app/Download/" + App + "/" + App + @"Sky multi setup x86/" + index);
                }

                Downloader downloader = new Downloader(stringList.ToArray(), Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Sky multi\Sky multi setup x86");
                downloader.ProgressChanged += new ProgressChangedHandler(ProgressChanged);
                downloader.DownloadCompleted += new DownloadCompletedHandler(DownloadCompleted);
                downloader.StartDownloadListAsync();
            }
        }

        private void InitializeComponent()
        {
            this.progressBar1 = new Sky_UI.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.progressBar1.BorderRadius = 15;
            this.progressBar1.Color = System.Drawing.Color.CornflowerBlue;
            this.progressBar1.Location = new System.Drawing.Point(34, 108);
            this.progressBar1.MouseClick = null;
            this.progressBar1.MouseDown = null;
            this.progressBar1.MouseMove = null;
            this.progressBar1.MouseUp = null;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(298, 17);
            this.progressBar1.TabIndex = 3;
            this.progressBar1.ValuePourcentages = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(85, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(200, 32);
            this.label1.TabIndex = 4;
            if (lang == 0) // FR
            {
                this.label1.Text = "Mise à jour logiciel :";
            }
            else // EN
            {
                this.label1.Text = "Update software :";
            }
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(259, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(200, 32);
            this.label2.TabIndex = 4;
            if (lang == 0)
            {
                this.label2.Text = "Fichier : 0 / 0";
            }
            else
            {
                this.label2.Text = "File : 0 / 0";
            }
            // 
            // DownloadUpdaterDialog
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.BorderColor = System.Drawing.Color.CornflowerBlue;
            this.Border = true;
            this.BorderRadius = 15;
            this.BorderWidth = 3;
            this.ClientSize = new System.Drawing.Size(365, 206);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.progressBar1);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "DownloadUpdaterDialog";
            this.Controls.SetChildIndex(this.progressBar1, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
