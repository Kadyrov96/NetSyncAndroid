namespace NetSync_Mobile
{
    using Android.Widget;
    using Java.Lang;
    public class FileListRowViewHolder : Object
    {
        public FileListRowViewHolder(TextView textView, ImageView imageView)
        {
            TextView = textView;
            ImageView = imageView;
        }

        public ImageView ImageView { get; private set; }
        public TextView TextView { get; private set; }

        ///   This method will update the TextView and the ImageView that are
        public void Update(string fileName, int fileImageResourceId)
        {
            TextView.Text = fileName;
            ImageView.SetImageResource(fileImageResourceId);
        }
    }
}
