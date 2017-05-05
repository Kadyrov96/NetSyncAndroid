
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

namespace NetSync_Mobile
{
    [Activity(Label = "Sync profiles menu")]
    public class ProfilesMenuActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ProfilesMenu);

            Button addProfile_btn = FindViewById<Button>(Resource.Id.addNewProfile);
            addProfile_btn.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(AddingProfileActivity));
                StartActivity(intent);
            };
        }
    }
}