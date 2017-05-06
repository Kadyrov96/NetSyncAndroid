using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Widget;

using System.Collections.Generic;
using System.Linq;

namespace NetSync_Mobile
{
    [Activity(Label = "Sync profiles menu")]
    public class ProfilesMenuActivity : Activity
    {
        ListView profilesList;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SyncProfilesHandler.LoadProfiles();
            SetContentView(Resource.Layout.ProfilesMenu);

            profilesList = FindViewById<ListView>(Resource.Id.listView1);
            profilesList.ItemClick += OnListItemClick;
            profilesList.Adapter = new CusotmListAdapter(this, SyncProfilesHandler.AvailableProfilesList);

            Button addProfile_btn = FindViewById<Button>(Resource.Id.addNewProfile);
            addProfile_btn.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(AddingProfileActivity));
                StartActivity(intent);
            };
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
                        if(SyncProfilesHandler.SelectedProfilesList.Any(
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

        public CusotmListAdapter(Activity _context, List<SyncProfile> _list): base()
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
            view.FindViewById<TextView>(Resource.Id.ProfilePathRow).Text = item.ProfileSyncFolderPath;
            view.FindViewById<TextView>(Resource.Id.SyncDateRow).Text = item.SyncDateTime;

            return view;
        }
    }
}