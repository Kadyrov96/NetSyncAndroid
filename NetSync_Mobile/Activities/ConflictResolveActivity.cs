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
using Android.Graphics;

namespace NetSync_Mobile
{
    [Activity(Label = "РАЗРЕШЕНИЕ КОНФЛИКТА ФАЙЛОВ")]
    public class ConflictResolveActivity : Activity
    {
        ListView conflictFilesListView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            AppData.CoflictFilesList = new List<string>();
            AppData.CoflictFilesList.Add("TestFile3.txt");
            AppData.CoflictFilesList.Add("TestFile4.txt");
            AppData.CoflictFilesList.Add("TestFile5.txt");

            var colorDrawable = new ColorDrawable(Color.DeepSkyBlue);
            ActionBar.SetBackgroundDrawable(colorDrawable);

            SetContentView(Resource.Layout.ConflictLayout);
            conflictFilesListView = FindViewById<ListView>(Resource.Id.ConflictFilesListView);
            //profilesListView.ItemClick += OnListItemClick;
            conflictFilesListView.Adapter = new ConflictFilesListAdapter(this,AppData.CoflictFilesList);

            //ImageButton addProfile_btn = FindViewById<ImageButton>(Resource.Id.addNewProfile_btn);
        }
    }
}