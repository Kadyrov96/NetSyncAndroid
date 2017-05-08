using Android.App;
using Android.OS;

namespace NetSync_Mobile
{
    [Activity(Label = "Network settings")]
    public class NetSettingsActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ConnectionsSettings);
            // Create your application here
        }
    }
}