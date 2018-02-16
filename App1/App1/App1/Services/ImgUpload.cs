using Newtonsoft.Json;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace App1.Services
{



    public class ImgUpload
    {
        // public static string BaseUrl = "http://www.teqso.in/ocr/imageupload.php/";

        MediaFile filetosend;

        /*   async static public Task<> PostImg(MediaFile file)
           {
               try
               {
                   using (var client = new HttpClient())
                   {
                       client.BaseAddress = new Uri(BaseUrl);
                       StreamContent streamContent = new StreamContent(file.GetStream());
                       streamContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                       var result = await client.PostAsync(streamContent);
                       if (!result.IsSuccessStatusCode)
                           return null;
                       else return Response.FromJson(await result.Content.ReadAsStringAsync());
                   }
               }
               catch (Exception e)
               {
                   //keeping it simple
                   //if you want handle each exception and show it in UI
                   return null;
               }

           }*/
        public static string PostImg(MediaFile filetosend)
        {
            System.Net.WebClient Client = new System.Net.WebClient();

            Client.Headers.Add("Content-Type", "binary/octet-stream");


            string Filepath = filetosend.Path;
           
            byte[] result = Client.UploadFile("http://www.teqso.in/ocr/imageupload.php", "POST",
                                              "Filepath");

            string s = System.Text.Encoding.UTF8.GetString(result, 0, result.Length);
            return s;

        }
    }
}     
        
    

