using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using Android.Views;
using Android.Graphics.Drawables;
using Android.Graphics;

namespace NetSync_Mobile
{
    [Activity(Label = "NetSync_Mobile", MainLauncher = true, Icon = "@drawable/Icon")]
    public class MainActivity : Activity
    {
        TCP_Client client;
        ProfileProcessor profileProcessor;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            var colorDrawable = new ColorDrawable(Color.DeepSkyBlue);
            ActionBar.SetBackgroundDrawable(colorDrawable);

            client = new TCP_Client(this);
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
                try
                {
                    if (SyncProfilesHandler.SelectedProfilesList != null)
                        foreach (var profile in SyncProfilesHandler.SelectedProfilesList)
                        {
                            //Toast.MakeText(this, "profile #" + idx.ToString(), ToastLength.Short).Show();
                            profileProcessor = new ProfileProcessor(TCP_Client.SSLStream);
                            profileProcessor.ProcessingProfile(profile);
                            Toast.MakeText(this, profile.ProfileName, ToastLength.Short).Show();
                            Toast.MakeText(this, AppData.Message1, ToastLength.Short).Show();
                            Toast.MakeText(this, AppData.Message2, ToastLength.Short).Show();
                        }
                }
                catch (System.Exception)
                {

                    throw;
                }

            };

            Button devInfo_dtn = FindViewById<Button>(Resource.Id.connectionMenu);
            devInfo_dtn.Click += (sender, e) =>
            {

            };
        }

        void CreateNetSettingsDialog()
        {
            ContextThemeWrapper ctw = new ContextThemeWrapper(this, Android.Resource.Style.ThemeHoloLightDarkActionBar);
            AlertDialog.Builder builder = new AlertDialog.Builder(ctw);
            LayoutInflater inflater = Application.Context.GetSystemService(LayoutInflaterService) as LayoutInflater;
            View view = inflater.Inflate(Resource.Layout.ConnectionsSettings, null);
            builder.SetView(view);

            EditText serverIPAddress = view.FindViewById<EditText>(Resource.Id.ip_text);
            EditText serverHostname = view.FindViewById<EditText>(Resource.Id.port_text);
            //Button selectFolder_btn = view.FindViewById<Button>(Resource.Id.SelectFolderPath_btn);

            builder.SetTitle("НАСТРОЙКИ ПОДКЛЮЧЕНИЯ")
    
            .SetPositiveButton("Accept", (senderAlert, args) =>
            {
                if (serverIPAddress.Text != "")
                    AppData.ServerIPAddress = serverIPAddress.Text;

                if (serverHostname.Text != "")
                    AppData.ServerHostname = serverHostname.Text;

                client.ConnectToServer(AppData.ServerIPAddress, 888, AppData.ServerIPAddress);
                //Toast.MakeText(this, "Connected", ToastLength.Short).Show();
            })
            .SetNegativeButton("Cancel", (senderAlert, args) =>
            {

            });

            Dialog dialog = builder.Create();
            dialog.Show();
        }
    }
}

