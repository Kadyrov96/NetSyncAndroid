using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics.Drawables;
using Android.Content.Res;

namespace NetSync_Mobile
{
    public class ConflictFilesListAdapter : BaseAdapter<string>
    {
        Activity context;
        List<string> list;

        public ConflictFilesListAdapter(Activity _context, List<string> _list) : base()
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

        public override string this[int index]
        {
            get { return list[index]; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;

            // re-use an existing view, if one is available
            // otherwise create a new one
            if (view == null)
                view = context.LayoutInflater.Inflate(Resource.Layout.conflictFileItemRow, parent, false);

            string item = this[position];
            view.FindViewById<TextView>(Resource.Id.ConflictFileNameRow).Text = item;

            Drawable icon = context.Resources.GetDrawable(Resource.Drawable.alert);
            view.FindViewById<ImageView>(Resource.Id.ConflictStatusRow).SetImageDrawable(icon);

            return view;
        }
    }
}