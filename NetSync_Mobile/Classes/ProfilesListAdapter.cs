using System.Collections.Generic;

using Android.App;
using Android.Views;
using Android.Widget;

namespace NetSync_Mobile
{
    public class ProfilesListAdapter : BaseAdapter<SyncProfile>
    {
        Activity context;
        List<SyncProfile> list;

        public ProfilesListAdapter(Activity _context, List<SyncProfile> _list) : base()
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
            view.FindViewById<TextView>(Resource.Id.ProfilePathRow).Text = "КАТАЛОГ: " + item.ProfileSyncFolderPath;
            view.FindViewById<TextView>(Resource.Id.SyncDateRow).Text = "ДАТА И ВРЕМЯ СИНХРОНИЗАЦИИ: " + item.SyncDateTime;

            return view;
        }
    }
}