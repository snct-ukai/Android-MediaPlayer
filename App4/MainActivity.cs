using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Plugin.FilePicker;
using Android.Media;
using System.IO;

namespace App4
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        public TextView path { get; private set; }
        public string filepath = null;
        protected MediaPlayer player;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            path = FindViewById<TextView>(Resource.Id.text);

            Button filefind = FindViewById<Button>(Resource.Id.find_file);
            filefind.Click += Filefind_Click;

            Button fileplay = FindViewById<Button>(Resource.Id.play_file);
            fileplay.Click += Fileplay_Click;
        }

        public void StartPlayer(string filePath)
        {
            if (player == null)
            {
                player = new MediaPlayer();
            }
            else
            {
                player.Reset();
                player.SetDataSource(filePath);
                player.Prepare();
                player.Start();
            }
        }

        private void Fileplay_Click(object sender, System.EventArgs e)
        {
            if (filepath != null)
            {
                if (Path.GetExtension(filepath) == ".mp3")
                {
                    StartPlayer(filepath);
                }
            }
        }

        private async void Filefind_Click(object sender, System.EventArgs e)
        {
            var file = await CrossFilePicker.Current.PickFile();
            path.Text = file.FileName;
            filepath = file.FilePath;
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}