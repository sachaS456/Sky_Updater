using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;

namespace Sky_Updater
{
    public delegate void ProgressChangedHandler(long? totalFileSize, long totalBytesDownloaded, double? progressPercentage, int nbFile, int FileDownloaded);
    public delegate void DownloadCompletedHandler();

    public class Downloader : IDisposable
    {
        private readonly string _downloadUrl;
        private readonly string _destinationDiretoryPath;
        private readonly string[] _downloadUrlList;
        private int nbFileDownloaded = 0;
        private string _destinationFilePath2;
        private HttpClient _httpClient;

        public ProgressChangedHandler ProgressChanged;
        public DownloadCompletedHandler DownloadCompleted;

        public Downloader(string downloadUrl, string destinationFilePath)
        {
            _downloadUrl = downloadUrl;
            _destinationDiretoryPath = destinationFilePath;
            _destinationFilePath2 = _destinationDiretoryPath;
        }

        public Downloader(string[] downloadUrlList, string destinationDiretoryPath)
        {
            _downloadUrlList = downloadUrlList;
            _destinationDiretoryPath = destinationDiretoryPath;
        }

        public async Task StartDownloadListAsyncTask()
        {
            _httpClient = new HttpClient { Timeout = TimeSpan.FromDays(1) };

            foreach (string URL in _downloadUrlList)
            {
                _destinationFilePath2 = _destinationDiretoryPath + @"\" + nbFileDownloaded;
                using (HttpResponseMessage response = await _httpClient.GetAsync(URL, HttpCompletionOption.ResponseHeadersRead))
                {
                    await DownloadFileFromHttpResponseMessage(response);
                }

                nbFileDownloaded++;
            }

            if (DownloadCompleted != null)
            {
                DownloadCompleted();
            }
        }

        public async void StartDownloadListAsync()
        {
            _httpClient = new HttpClient { Timeout = TimeSpan.FromDays(1) };

            foreach (string URL in _downloadUrlList)
            {
                _destinationFilePath2 = _destinationDiretoryPath + @"\" + nbFileDownloaded;
                using (HttpResponseMessage response = await _httpClient.GetAsync(URL, HttpCompletionOption.ResponseHeadersRead))
                {
                    await DownloadFileFromHttpResponseMessage(response);
                }

                nbFileDownloaded++;
            }

            if (DownloadCompleted != null)
            {
                DownloadCompleted();
            }
        }

        public async Task StartDownloadAsyncTask()
        {
            _httpClient = new HttpClient { Timeout = TimeSpan.FromDays(1) };

            using (HttpResponseMessage response = await _httpClient.GetAsync(_downloadUrl, HttpCompletionOption.ResponseHeadersRead))
            {
                await DownloadFileFromHttpResponseMessage(response);
            }

            nbFileDownloaded++;

            if (DownloadCompleted != null)
            {
                DownloadCompleted();
            }
        }

        public async void StartDownloadAsync()
        {
            _httpClient = new HttpClient { Timeout = TimeSpan.FromDays(1) };

            using (HttpResponseMessage response = await _httpClient.GetAsync(_downloadUrl, HttpCompletionOption.ResponseHeadersRead))
            {
                await DownloadFileFromHttpResponseMessage(response);
            }

            nbFileDownloaded++;

            if (DownloadCompleted != null)
            {
                DownloadCompleted();
            }
        }

        private async Task DownloadFileFromHttpResponseMessage(HttpResponseMessage response)
        {
            response.EnsureSuccessStatusCode();

            long? totalBytes = response.Content.Headers.ContentLength;

            using (Stream contentStream = await response.Content.ReadAsStreamAsync())
            {
                await ProcessContentStream(totalBytes, contentStream);
            }
        }

        private async Task ProcessContentStream(long? totalDownloadSize, Stream contentStream)
        {
            long totalBytesRead = 0L;
            long readCount = 0L;
            byte[] buffer = new byte[8192];
            bool isMoreToRead = true;

            using (FileStream fileStream = new FileStream(_destinationFilePath2, FileMode.Create, FileAccess.Write, FileShare.None, 8192, true))
            {
                do
                {
                    try
                    {
                        int bytesRead = await contentStream.ReadAsync(buffer, 0, buffer.Length);
                        if (bytesRead == 0)
                        {
                            isMoreToRead = false;
                            TriggerProgressChanged(totalDownloadSize, totalBytesRead);
                            fileStream.Close();
                            contentStream.Close();
                            buffer = null;
                            return;
                        }

                        await fileStream.WriteAsync(buffer, 0, bytesRead);

                        totalBytesRead += bytesRead;
                        readCount += 1;

                        if (readCount % 100 == 0)
                        {
                            TriggerProgressChanged(totalDownloadSize, totalBytesRead);
                        }
                    }
                    catch
                    {
                        await Task.Delay(1);
                    }
                }
                while (isMoreToRead);
            }
        }

        private void TriggerProgressChanged(long? totalDownloadSize, long totalBytesRead)
        {
            if (ProgressChanged == null)
            {
                return;
            }

            double? progressPercentage = null;
            if (totalDownloadSize.HasValue)
            {
                progressPercentage = Math.Round((double)totalBytesRead / totalDownloadSize.Value * 100, 2);
            }

            ProgressChanged(totalDownloadSize, totalBytesRead, progressPercentage, _downloadUrlList.Length - 1, nbFileDownloaded);
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}
