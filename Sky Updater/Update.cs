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
using System.Net.Http;
using System.IO;
using System.Diagnostics;

namespace Sky_Updater
{
    public static class Update
    {
        public static bool CheckUpdate(string AppName, string Version)
        {
            foreach (Process i in Process.GetProcessesByName("Sky multi"))
            {
                if (i.Id != Process.GetCurrentProcess().Id && i.MainModule.FileName == System.Windows.Forms.Application.ExecutablePath)
                {
                    return false;
                }
            }

            try
            {
                if (DownloadString("https://serie-sky.netlify.app/Download/" + AppName + "/Version.txt") != Version)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (System.Net.Http.HttpRequestException e)
            {
                if (e.Message == "Hôte inconnu. (serie-sky.netlify.app:443)")
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
            catch
            {
                throw;
            }
        }

        public static async Task<bool> CheckUpdateAsync(string AppName, string Version)
        {
            foreach (Process i in Process.GetProcessesByName("Sky multi"))
            {
                if (i.Id != Process.GetCurrentProcess().Id && i.MainModule.FileName == System.Windows.Forms.Application.ExecutablePath)
                {
                    return false;
                }
            }

            try
            {
                if (await DownloadStringAsync("https://serie-sky.netlify.app/Download/" + AppName + "/Version.txt") != Version)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (System.Net.Http.HttpRequestException e)
            {
                if (e.Message == "Hôte inconnu. (serie-sky.netlify.app:443)")
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
            catch
            {
                throw;
            }
        }

        public static long sizeFile(Uri requestUri)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, requestUri))
                {
                    long? size = httpClient.Send(request).Content.Headers.ContentLength;

                    if (size == null)
                    {
                        return (long)size;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
        }

        public static string DownloadString(string requestUri)
        {
            if (requestUri == null)
                throw new ArgumentNullException("requestUri");

            return DownloadString(new Uri(requestUri));
        }

        public static string DownloadString(Uri requestUri)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, requestUri))
                {
                    using (StreamReader reader = new StreamReader(httpClient.Send(request).Content.ReadAsStream(), Encoding.UTF8))
                    {
                        return reader.ReadToEnd();
                    }

                    /*using (StreamReader reader = new StreamReader((await httpClient.SendAsync(request)).Content.ReadAsStreamAsync().Result, Encoding.UTF8))
                    {
                        return reader.ReadToEnd();
                    }*/
                }
            }
        }

        public static async Task<string> DownloadStringAsync(string requestUri)
        {
            if (requestUri == null)
                throw new ArgumentNullException("requestUri");

            return await DownloadStringAsync(new Uri(requestUri));
        }

        public static async Task<string> DownloadStringAsync(Uri requestUri)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, requestUri))
                {
                    using (StreamReader reader = new StreamReader(await (await httpClient.SendAsync(request)).Content.ReadAsStreamAsync(), Encoding.UTF8))
                    {
                        return reader.ReadToEnd();
                    }

                    /*using (StreamReader reader = new StreamReader((await httpClient.SendAsync(request)).Content.ReadAsStreamAsync().Result, Encoding.UTF8))
                    {
                        return reader.ReadToEnd();
                    }*/
                }
            }
        }

        public static Task<Stream> DownloadStreamAsync(string requestUri)
        {
            if (requestUri == null)
                throw new ArgumentNullException("requestUri");

            return DownloadStreamAsync(new Uri(requestUri));
        }

        public static async Task<Stream> DownloadStreamAsync(Uri requestUri)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, requestUri))
                {
                    using (Stream contentStream = await (await httpClient.SendAsync(request)).Content.ReadAsStreamAsync())
                    {
                        return contentStream;
                    }
                }
            }
        }
    }
}
