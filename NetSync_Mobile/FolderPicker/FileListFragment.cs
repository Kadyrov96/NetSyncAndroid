namespace NetSync_Mobile
{
    using Android.App;
    using Android.Graphics;
    using Android.Graphics.Drawables;
    using Android.OS;
    using Android.Views;
    using Android.Widget;
    using Java.Lang;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    ///   A ListFragment that will show the files and subdirectories of a given directory.
    /// </summary>
    public class FileListFragment : ListFragment
    {
        public static readonly string DefaultInitialDirectory = "/storage/emulated/0/";
        public static string CurrentDirectory = "/storage/emulated/0/";
        
        FileListAdapter _adapter;
        DirectoryInfo _directory;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            var colorDrawable = new ColorDrawable(Color.DeepSkyBlue);
            Activity.ActionBar.SetBackgroundDrawable(colorDrawable);
            _adapter = new FileListAdapter(Activity, new FileSystemInfo[0]);
            ListAdapter = _adapter;
        }

        public override void OnListItemClick(ListView l, View v, int position, long id)
        {
            var fileSystemInfo = _adapter.GetItem(position);
            if (Directory.Exists(fileSystemInfo.FullName))
            {
                var menu = new PopupMenu(Activity, v);
                menu.Inflate(Resource.Layout.folder_popup_menu);
                menu.MenuItemClick += (s, a) =>
                {
                    switch (a.Item.ItemId)
                    {
                        case Resource.Id.folder_pop_button1:
                            CurrentDirectory = fileSystemInfo.FullName;
                            RefreshFilesList(fileSystemInfo.FullName);
                            base.OnListItemClick(l, v, position, id);
                            break;

                        case Resource.Id.folder_pop_button2:
                            AppData.SelectedFolderPath = fileSystemInfo.FullName;
                            Activity.OnBackPressed();
                            Toast.MakeText(Activity, "��������� ������� - " + fileSystemInfo.FullName, ToastLength.Long).Show();
                            break;
                    }
                };
                menu.Show();
            }
        }

        public override void OnResume()
        {
            base.OnResume();
            RefreshFilesList(DefaultInitialDirectory);
        }

        public void RefreshFilesList(string directory)
        {
            IList<FileSystemInfo> folderEntries = new List<FileSystemInfo>();
            var dir = new DirectoryInfo(directory);

            try
            {
                foreach (var item in dir.GetFileSystemInfos())
                    folderEntries.Add(item);
            }
            catch (Exception ex)
            {
                Toast.MakeText(Activity, "Problem retrieving contents of " + directory, ToastLength.Long).Show();
                return;
            }

            _directory = dir;
            _adapter.AddDirectoryContents(folderEntries);
            ListView.RefreshDrawableState();
        }
    }
}
