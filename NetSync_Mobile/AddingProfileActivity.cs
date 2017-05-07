using Android.App;
using Android.OS;
using Android.Widget;

namespace NetSync_Mobile
{
    [Activity(Label = "Adding new profile")]
    public class AddingProfileActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AddingProfile);
            EditText profilePath = FindViewById<EditText>(Resource.Id.SyncProfilePath);

        }
    }
}