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

            Button some_btn = FindViewById<Button>(Resource.Id.btn_first);
            some_btn.Click += (sender, e) =>
            {
                EditText profileName = FindViewById<EditText>(Resource.Id.profileName);
                EditText profilePath = FindViewById<EditText>(Resource.Id.profilePath);
                if (SyncProfilesHandler.AddNewProfile(profileName.Text, profilePath.Text, this))
                {
                    MessageDisplayer.ShowSuccessMessage(this, "System notifications", "New profile was successfully added.");
                }             
            };
        }
    }
}