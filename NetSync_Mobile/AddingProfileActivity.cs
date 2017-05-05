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

namespace NetSync_Mobile
{
    [Activity(Label = "Adding new profile")]
    public class AddingProfileActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AddingProfile);

            Button some_btn = FindViewById<Button>(Resource.Id.btn_first);
            some_btn.Click += (sender, e) =>
            {

            };
        }
    }
}