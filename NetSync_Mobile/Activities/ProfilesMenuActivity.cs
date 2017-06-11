using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Widget;
using System.Linq;
using Android.Content;
using Android.Graphics.Drawables;

namespace NetSync_Mobile
{
    [Activity(Label = "ПРОФИЛИ СИНХРОНИЗАЦИИ")]
    public class ProfilesMenuActivity : Activity
    {
        ListView profilesListView;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SyncProfilesHandler.LoadProfiles();

            var colorDrawable = new ColorDrawable(Color.DeepSkyBlue);
            ActionBar.SetBackgroundDrawable(colorDrawable);

            SetContentView(Resource.Layout.ProfilesMenu);

            profilesListView = FindViewById<ListView>(Resource.Id.ProfilesListView);
            profilesListView.ItemClick += OnListItemClick;
            profilesListView.Adapter = new ProfilesListAdapter(this, SyncProfilesHandler.AvailableProfilesList);

            ImageButton addProfile_btn = FindViewById<ImageButton>(Resource.Id.addNewProfile_btn);
            addProfile_btn.Click += (sender, e) => CreateAddingDialog();
        }

        protected override void OnResume()
        {
            base.OnResume();
            profilesListView.Adapter = new ProfilesListAdapter(this, SyncProfilesHandler.AvailableProfilesList);
        }

        void CreateAddingDialog()
        {
            ContextThemeWrapper ctw = new ContextThemeWrapper(this, Android.Resource.Style.ThemeHoloLightDarkActionBar);
            AlertDialog.Builder builder = new AlertDialog.Builder(ctw);
            LayoutInflater inflater = Application.Context.GetSystemService(LayoutInflaterService) as LayoutInflater;
            View view = inflater.Inflate(Resource.Layout.AddingProfile, null);
            builder.SetView(view);

            EditText profileName = view.FindViewById<EditText>(Resource.Id.SyncProfileName);
            ImageButton selectFolder_btn = view.FindViewById<ImageButton>(Resource.Id.SelectFolder_btn);
            selectFolder_btn.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(FolderPickerActivity));
                StartActivity(intent);
            };

            builder.SetTitle("ДОБАВИТЬ НОВЫЙ ПРОФИЛЬ")
            .SetPositiveButton("ОК", (senderAlert, args) =>
            {
                if (SyncProfilesHandler.AddNewProfile(profileName.Text, AppData.SelectedFolderPath, this))
                {
                    MessageDisplayer.ShowSuccessMessage(this, "Системные оповещения", "Новый профиль успешно добавлен.");
                    Synchroniser service = new Synchroniser(new FolderHandler(AppData.SelectedFolderPath));
                    service.CreateSyncDataStore();
                }
                profilesListView.Adapter = new ProfilesListAdapter(this, SyncProfilesHandler.AvailableProfilesList);
            })
            .SetNegativeButton("Отмена", (senderAlert, args) =>
            {
                Toast.MakeText(this, "Добавление профиля отменено!", ToastLength.Short).Show();
            });

            Dialog dialog = builder.Create();
            dialog.Show();
        }

        void OnListItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            View selectedItem = profilesListView.GetChildAt(e.Position);
            SyncProfile selectedProfile = SyncProfilesHandler.AvailableProfilesList[e.Position];

            var menu = new PopupMenu(this, selectedItem);
            menu.Inflate(Resource.Layout.popup_menu);
            menu.MenuItemClick += (s, a) =>
            {
                switch (a.Item.ItemId)
                {
                    case Resource.Id.pop_button1:
                        SyncProfilesHandler.DeleteProfile(selectedProfile.ProfileName);
                        profilesListView.Adapter = new ProfilesListAdapter(this, SyncProfilesHandler.AvailableProfilesList);
                        break;

                    case Resource.Id.pop_button2:
                        if (SyncProfilesHandler.SelectedProfilesList.Any(
                            profile => profile.ProfileName == selectedProfile.ProfileName))
                        {
                            SyncProfilesHandler.SelectedProfilesList.Remove(selectedProfile);
                            selectedItem.SetBackgroundColor(Color.Black);
                        }
                        else
                        {
                            SyncProfilesHandler.SelectedProfilesList.Add(selectedProfile);
                            selectedItem.SetBackgroundColor(Color.Gray);
                        }
                        break;
                }
            };
            menu.Show();
        }
    }
}