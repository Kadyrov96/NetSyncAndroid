using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using Android.Views;

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
            netSettings_btn.Click += (sender, e) => CreateNetSettingsDialog();

            Button startSync_btn = FindViewById<Button>(Resource.Id.startSync);
            startSync_btn.Click += (sender, e) =>
            {

            };

            Button devInfo_dtn = FindViewById<Button>(Resource.Id.connectionMenu);
            devInfo_dtn.Click += (sender, e) =>
            {

            };

        }

        void CreateNetSettingsDialog()
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(this);
            LayoutInflater inflater = Application.Context.GetSystemService(LayoutInflaterService) as LayoutInflater;
            View view = inflater.Inflate(Resource.Layout.ConnectionsSettings, null);
            builder.SetView(view);

            EditText serverIPAddress = view.FindViewById<EditText>(Resource.Id.ip_text);
            EditText serverHostname = view.FindViewById<EditText>(Resource.Id.port_text);
            //Button selectFolder_btn = view.FindViewById<Button>(Resource.Id.SelectFolderPath_btn);

            builder.SetTitle("Network settings")
            .SetPositiveButton("Accept", (senderAlert, args) =>
            {
                if (serverIPAddress.Text != "")
                    AppData.ServerIPAddress = serverIPAddress.Text;

                if (serverHostname.Text != "")
                    AppData.ServerHostname = serverHostname.Text;

                Toast.MakeText(this, AppData.ServerIPAddress, ToastLength.Short).Show();
            })
            .SetNegativeButton("Cancel", (senderAlert, args) =>
            {

            });

            Dialog dialog = builder.Create();
            dialog.Show();
        }
    }
}

