using Android.App;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;

namespace NetSync_Mobile
{
    [Activity(Label = "НАСТРОЙКИ ПОДКЛЮЧЕНИЯ")]
    public class NetSettingsActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            var colorDrawable = new ColorDrawable(Color.DeepSkyBlue);
            ActionBar.SetBackgroundDrawable(colorDrawable);
            SetTheme(Android.Resource.Style.ThemeMaterialLight);
            SetContentView(Resource.Layout.ConnectionsSettings);
        }
    }
}