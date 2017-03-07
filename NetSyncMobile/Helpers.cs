namespace NetSyncMobile
{
    using System.IO;
    using Android.Content;
    using Android.Runtime;
    using Android.Views;

    public static class Helpers
    {
        ///   Will obtain an instance of a LayoutInflater for the specified Context.
        public static LayoutInflater GetLayoutInflater(this Context context)
        {
            return context.GetSystemService(Context.LayoutInflaterService).JavaCast<LayoutInflater>();
        }

        ///   This method will tell us if the given FileSystemInfo instance is a directory.
        public static bool IsDirectory(this FileSystemInfo fsi)
        {
            if (fsi == null || !fsi.Exists)
                return false;

            return (fsi.Attributes & FileAttributes.Directory) == FileAttributes.Directory;
        }

        ///This method will tell us if the the given FileSystemInfo instance is a file.
        public static bool IsFile(this FileSystemInfo fsi)
        {
            if (fsi == null || !fsi.Exists)
                return false;

            return !IsDirectory(fsi);
        }

        public static bool IsVisible(this FileSystemInfo fsi)
        {
            if (fsi == null || !fsi.Exists)
                return false;

            var isHidden = (fsi.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden;
            return !isHidden;
        }
    }
}
