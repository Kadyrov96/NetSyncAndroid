using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;

namespace NetSync_Mobile
{
    [Activity(Label = "NetSync_Mobile", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            //Button connectSettingsBtn = FindViewById<Button>(Resource.Layout);
            //connectSettingsBtn.Click += (sender, e) =>
            //{
            //    var intent = new Intent(this, typeof(ConnectionActivity));
            //    StartActivity(intent);
            //};

            //Button filePickerBtn = FindViewById<Button>(Resource.Id.folderSelect);
            //filePickerBtn.Click += (sender, e) =>
            //{
            //    var intent = new Intent(this, typeof(FilePickerActivity));
            //    StartActivity(intent);
            //};
        }
    }
}

