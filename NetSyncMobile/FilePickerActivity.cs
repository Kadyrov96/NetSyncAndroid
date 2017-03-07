namespace NetSyncMobile
{
    using Android.App;
    using Android.OS;
    using Android.Support.V4.App;

    [Activity(Label = "NetSyncMobile", MainLauncher = false, Icon = "@drawable/icon")]
    public class FilePickerActivity : FragmentActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.FilePickLayout);
        }
    }
}
