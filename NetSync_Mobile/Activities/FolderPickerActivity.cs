namespace NetSync_Mobile
{
    using Android.App;
    using Android.OS;
    using Android.Support.V4.App;

    [Activity(Label = "ВЫБЕРИТЕ КАТАЛОГ")]
    public class FolderPickerActivity : FragmentActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.FolderPicker);
        }
    }
}