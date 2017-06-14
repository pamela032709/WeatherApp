


using System;
using Android.App;
using Android.Widget;
using Android.OS;
using Android.Graphics;
using System.Net;

namespace WeatherApp.Droid
{
    [Activity(Label = " Weather App", MainLauncher = true, Icon = "@drawable/sun")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            Button button = FindViewById<Button>(Resource.Id.weatherBtn);

            button.Click += Button_Click;
        }

		private Bitmap GetImageBitmapFromUrl(string url)
		{
			Bitmap imageBitmap = null;

			using (var webClient = new WebClient())
			{
				var imageBytes = webClient.DownloadData(url);
				if (imageBytes != null && imageBytes.Length > 0)
				{
					imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
				}
			}

			return imageBitmap;
		}

        private async void Button_Click(object sender, EventArgs e)
        {
            EditText zipCodeEntry = FindViewById<EditText>(Resource.Id.zipCodeEntry);

            /*
			AlertDialog.Builder alert = new AlertDialog.Builder(this);
			alert.SetTitle("Debugging");
			alert.SetMessage("BREAKPOINT 1");
            Dialog dialog = alert.Create();
            dialog.Show();
            */

            if (!String.IsNullOrEmpty(zipCodeEntry.Text))
            {
                Weather weather = await Core.GetWeather(zipCodeEntry.Text);
                if (weather != null)
				{
                    FindViewById<TextView>(Resource.Id.locationText).Text = weather.Title;
                    FindViewById<TextView>(Resource.Id.tempText).Text = weather.Temperature;
                    FindViewById<TextView>(Resource.Id.windText).Text = weather.Wind;
                    FindViewById<TextView>(Resource.Id.visibilityText).Text = weather.Visibility;
                    FindViewById<TextView>(Resource.Id.humidityText).Text = weather.Humidity;
					FindViewById<TextView>(Resource.Id.sunriseText).Text = weather.Sunrise;
					FindViewById<TextView>(Resource.Id.sunsetText).Text = weather.Sunset;

                    var bitmap = GetImageBitmapFromUrl("http://openweathermap.org/img/w/" + weather.Icon + ".png");
					FindViewById<ImageView>(Resource.Id.imgWeather).SetImageBitmap(bitmap);

                }
            }


			// We retrieve the image
			

				//IWeatherImageProvider provider = new WeatherImageProvider();
			 //   provider.getImage(code, requestQueue, new IWeatherImageProvider.WeatherImageListener() {
    //            @Override
    //            public void onImageReady(Bitmap image) {
				//weatherImage.setImageBitmap(image);
				//}
		
        }
    }
}