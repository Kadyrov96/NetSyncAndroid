using Android.App;
using Android.OS;

namespace NetSync_Mobile
{
    [Activity(Label = "Adding new profile")]
    public class AddingProfileActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            AppData.SelectedFolderPath = "";
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AddingProfile);
        }
    }
}