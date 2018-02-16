using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App1
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
            
                InitializeComponent();
            MediaFile file;
            

            takePhoto.Clicked += async (sender, args) =>
            {

                    if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                    {
                        DisplayAlert("No Camera", ":( No camera avaialble.", "OK");
                        return;
                    }

                     file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                    {
                        Directory = "Test",
                        SaveToAlbum = true,
                        CompressionQuality = 75,
                        CustomPhotoSize = 50,
                        PhotoSize = PhotoSize.MaxWidthHeight,
                        MaxWidthHeight = 2000,
                        DefaultCamera = CameraDevice.Front
                    });

                    if (file == null)
                        return;

                    DisplayAlert("File Location", file.Path, "OK");

                    image.Source = ImageSource.FromStream(() =>
                    {
                        var stream = file.GetStream();
                        file.Dispose();
                        return stream;
                    });
            };

                pickPhoto.Clicked += async (sender, args) =>
                {
                    if (!CrossMedia.Current.IsPickPhotoSupported)
                    {
                        DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
                        return;
                    }
                     file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                    {
                        PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,

                    });


                    if (file == null)
                        return;

                  //  image.Source = ImageSource.FromStream(() =>
                   // {
                     //   var stream = file.GetStream();
                     //   file.Dispose();
                     //   return stream;
                 //   });
                    PhotoUpload();
                };
         
            async void PhotoUpload()
            {
                //ActionButton.Text = TakeaPhoto;

                if (file == null)
                {
                    await DisplayAlert("Error", "No Image Taken", "Ok");
                    return;
                }

                var result = Services.ImgUpload.PostImg(file);
                
                if (result == null)
                {
                    await DisplayAlert("Error", "Unable to send photo to the service", "Ok");
                    return;
                }

               // await Navigation.PushAsync(new ResultPage(result, file));
                //Going to Display the result as alert, if multiple faces selecting the first major one from api
            }


        }
    }
	
}
