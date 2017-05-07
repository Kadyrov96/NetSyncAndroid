using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Widget;

using System.Collections.Generic;
using System.Linq;
using Android.Content;

namespace NetSync_Mobile
{
    [Activity(Label = "Sync profiles menu")]
    public class ProfilesMenuActivity : Activity
    {
        ListView profilesList;
        static LayoutInflater inflater;

        public static void SetPath()
        {
            inflater = Application.Context.GetSystemService(LayoutInflaterService) as LayoutInflater;
            View view = inflater.Inflate(Resource.Layout.AddingProfile, null);
            EditText profilePath = view.FindViewById<EditText>(Resource.Id.SyncProfilePath);
            profilePath.Text = "folderpath, bitch";
        }
        void CreateAddingDialog()
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(this);
            inflater = Application.Context.GetSystemService(LayoutInflaterService) as LayoutInflater;
            View view = inflater.Inflate(Resource.Layout.AddingProfile, null);
            builder.SetView(view);

            EditText profileName = view.FindViewById<EditText>(Resource.Id.SyncProfileName);
            EditText profilePath = view.FindViewById<EditText>(Resource.Id.SyncProfilePath);
            Button selectFolder_btn = view.FindViewById<Button>(Resource.Id.folderSelectPath);

            selectFolder_btn.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(FilePickerActivity));
                StartActivity(intent);
            };

            builder.SetTitle("Adding new sync profile")
            .SetPositiveButton("Accept", (senderAlert, args) =>
            {
                if (SyncProfilesHandler.AddNewProfile(profileName.Text, profilePath.Text, this))
                {
                    MessageDisplayer.ShowSuccessMessage(this, "System notifications", "New profile was successfully added.");
                }
                profilesList.Adapter = new CusotmListAdapter(this, SyncProfilesHandler.AvailableProfilesList);
            })
            .SetNegativeButton("Cancel", (senderAlert, args) =>
            {
                Toast.MakeText(this, "Declined!", ToastLength.Short).Show();
            });

            Dialog dialog = builder.Create();
            dialog.Show();
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SyncProfilesHandler.LoadProfiles();
            SetContentView(Resource.Layout.ProfilesMenu);

            profilesList = FindViewById<ListView>(Resource.Id.listView1);
            profilesList.ItemClick += OnListItemClick;
            profilesList.Adapter = new CusotmListAdapter(this, SyncProfilesHandler.AvailableProfilesList);

            Button addProfile_btn = FindViewById<Button>(Resource.Id.addNewProfile);
            addProfile_btn.Click += (sender, e) => CreateAddingDialog();
        }

        protected override void OnResume()
        {
            base.OnResume();
            profilesList.Adapter = new CusotmListAdapter(this, SyncProfilesHandler.AvailableProfilesList);
        }

        void OnListItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            View selectedItem = profilesList.GetChildAt(e.Position);
            SyncProfile selectedProfile = SyncProfilesHandler.AvailableProfilesList[e.Position];

            var menu = new PopupMenu(this, selectedItem);
            menu.Inflate(Resource.Layout.popup_menu);
            menu.MenuItemClick += (s, a) =>
            {
                switch (a.Item.ItemId)
                {
                    case Resource.Id.pop_button1:
                        SyncProfilesHandler.DeleteProfile(selectedProfile.ProfileName);
                        profilesList.Adapter = new CusotmListAdapter(this, SyncProfilesHandler.AvailableProfilesList);
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

    public class CusotmListAdapter : BaseAdapter<SyncProfile>
    {
        Activity context;
        List<SyncProfile> list;

        public CusotmListAdapter(Activity _context, List<SyncProfile> _list) : base()
        {
            context = _context;
            list = _list;
        }

        public override int Count
        {
            get { return list.Count; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override SyncProfile this[int index]
        {
            get { return list[index]; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;

            // re-use an existing view, if one is available
            // otherwise create a new one
            if (view == null)
                view = context.LayoutInflater.Inflate(Resource.Layout.listitemrow, parent, false);

            SyncProfile item = this[position];
            view.FindViewById<TextView>(Resource.Id.ProfileNameRow).Text = item.ProfileName;
            view.FindViewById<TextView>(Resource.Id.ProfilePathRow).Text = "Folder path: " + item.ProfileSyncFolderPath;
            view.FindViewById<TextView>(Resource.Id.SyncDateRow).Text = "Last sync date & time: " + item.SyncDateTime;

            return view;
        }
    }
}