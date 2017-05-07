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

            Button syncProfileMenu_btn = FindViewById<Button>(Resource.Id.profileMenu);
            syncProfileMenu_btn.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(ProfilesMenuActivity));
                StartActivity(intent);
            };

            Button netSettings_btn = FindViewById<Button>(Resource.Id.connectionMenu);
            netSettings_btn.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(NetSettingsActivity));
                StartActivity(intent);
            };

            Button startSync_btn = FindViewById<Button>(Resource.Id.startSync);
            startSync_btn.Click += (sender, e) =>
            {
                
            };

            Button devInfo_dtn = FindViewById<Button>(Resource.Id.connectionMenu);
            devInfo_dtn.Click += (sender, e) =>
            {

            };
        }
    }
}

