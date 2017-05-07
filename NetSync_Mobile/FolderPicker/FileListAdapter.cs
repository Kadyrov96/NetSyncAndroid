namespace NetSync_Mobile
{
    using Android.App;
    using Android.Views;
    using Android.Widget;

    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class FileListAdapter : ArrayAdapter<FileSystemInfo>
    {
        private readonly Activity _context;

        public FileListAdapter(Activity context, IList<FileSystemInfo> fsi)
            : base(context, Resource.Layout.file_picker_list_item, Android.Resource.Id.Text1, fsi)
        {
            _context = context;
        }

        public void AddDirectoryContents(IEnumerable<FileSystemInfo> directoryContents)
        {
            Clear();
            // Notify the _adapter that things have changed or that there is nothing 
            // to display.
            if (directoryContents.Any())
            {
                AddAll(directoryContents.ToArray());
                NotifyDataSetChanged();
            }
            else
                NotifyDataSetInvalidated();
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var fileSystemEntry = GetItem(position);

            FileListRowViewHolder viewHolder;
            View row;
            if (convertView == null)
            {
                row = _context.LayoutInflater.Inflate(Resource.Layout.file_picker_list_item, parent, false);
                viewHolder = new FileListRowViewHolder(row.FindViewById<TextView>(Resource.Id.file_picker_text), row.FindViewById<ImageView>(Resource.Id.file_picker_image));
                row.Tag = viewHolder;
            }
            else
            {
                row = convertView;
                viewHolder = (FileListRowViewHolder)row.Tag;
            }
            viewHolder.Update(fileSystemEntry.Name, Directory.Exists(fileSystemEntry.FullName) ? Resource.Drawable.folder : Resource.Drawable.file);
            return row;
        }
    }
}