using BE;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAL
{
    public class GoogleDriveApi
    {
        public string[] Scopes = { DriveService.Scope.DriveReadonly };
        public  string ApplicationName = "Drive API .NET Quickstart";
        Repository rep;
        public GoogleDriveApi()
        {
            rep = new Repository();

        }
        private  void DownloadFile(Google.Apis.Drive.v3.DriveService service, Google.Apis.Drive.v3.Data.File file, string saveTo)
        {

         
            var stream = new System.IO.MemoryStream();
            var request = service.Files.Get(file.Id);
           
       
            // Add a handler which will be notified on progress changes.
            // It will notify on each chunk download and when the
            // download is completed or failed.
            request.MediaDownloader.ProgressChanged += (Google.Apis.Download.IDownloadProgress progress) =>
            {
                switch (progress.Status)
                {
                    case Google.Apis.Download.DownloadStatus.Downloading:
                        {
                            Console.WriteLine(progress.BytesDownloaded);
                            break;
                        }
                    case Google.Apis.Download.DownloadStatus.Completed:
                        {
                            Console.WriteLine("Download complete.");
                            SaveStream(stream, saveTo);
                            break;
                        }
                    case Google.Apis.Download.DownloadStatus.Failed:
                        {
                            Console.WriteLine("Download failed.");
                            break;
                        }
                }
            };
            request.DownloadAsync(stream);

        }
        private  void SaveStream(System.IO.MemoryStream stream, string saveTo)
        {
            using (System.IO.FileStream file = new System.IO.FileStream(saveTo, System.IO.FileMode.Create, System.IO.FileAccess.Write))
            {
                stream.WriteTo(file);
            }
            DAL.Barcode_reader reader = new Barcode_reader();
            
            string text= reader.Decode(saveTo);

            string time = saveTo.Substring(saveTo.Length - 1 - 9, 6);
            string hour = time.Substring(0, 2);
            string minnutes = time.Substring(2, 2);
            string dayNight = time.Substring(4, 2);
            if (dayNight == "PM")
                hour = (Int32.Parse(hour) + 12).ToString();
            DateTime dateTime = DateTime.Parse(saveTo.Substring(69, saveTo.IndexOf("at") - 69) + " " + hour + ":" + minnutes + ":" + "00" + " " + dayNight);
            string[] str = text.Split(',');

            ScannedProduct s=new ScannedProduct(Int32.Parse(str[0]), str[1], dateTime, Int32.Parse(str[2]), Int32.Parse(str[3]));
            rep.saveProductFromDrive(s, str[4]);
            
        }

        public void Connect(DateTime dt)
        {
            UserCredential credential;

            using (var stream =
                new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Drive API service.
            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            // Define parameters of request.
            FilesResource.ListRequest listRequest = service.Files.List();
            listRequest.PageSize = 1000;
            listRequest.Fields = "nextPageToken, files(id, name)";

          
            string date = String.Format("{0:yyyy-MM-dd}", dt);
            //date = date + "T00:00:00";
            //dt.GetDateTimeFormats().
            listRequest.Q= "createdTime >= '"+date+ "' ";
            // List files.
            IList<Google.Apis.Drive.v3.Data.File> files = listRequest.Execute()
                .Files;


            Console.WriteLine("Files:");

            if (files != null && files.Count > 0)
            {
                DateTime maxDate = DateTime.MinValue;
                foreach (var file in files)
                {
                    if (file.Name.Substring(file.Name.Length - 2, 2) == "PM" || file.Name.Substring(file.Name.Length - 2, 2) == "AM")
                    {
                        string time = file.Name.Substring(file.Name.Length - 6, 6);
                        int hour = Int32.Parse(time.Substring(0, 2));
                        int minnutes = Int32.Parse(time.Substring(2, 2));
                        string dayNight = time.Substring(4, 2);
                        if (dayNight == "PM")
                            hour += 12;

                        string temp = file.Name.Substring(0, file.Name.IndexOf("at") - 1);
                        DateTime dateTime = DateTime.Parse(temp);
                        dateTime = dateTime.AddHours(hour);
                        dateTime = dateTime.AddMinutes(minnutes);
                        if (dateTime > dt)
                        {
                            if (dateTime > maxDate)
                                maxDate = dateTime;
                            Console.WriteLine(file.CreatedTime);
                            string saveTo = String.Format(@"C:\courses\SmartShopping\SmartShopping\SmartShopping\Images\Barcodes\{0}.png", file.Name);
                            Console.WriteLine("{0} ({1})", file.Name, file.Id);

                            DownloadFile(service, file, saveTo);
                        }
                    }
                }
                rep.update_lastDate(maxDate);
            }
            else
            {
                Console.WriteLine("No files found.");
            }
            Console.Read();
        }
    }
}


