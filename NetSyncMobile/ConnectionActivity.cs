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

namespace NetSyncMobile
{
    [Activity(Label = "ConnectionActivity")]
    public class ConnectionActivity : Activity
    {
        EditText ipAddress;
        EditText port;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Connection);

            ipAddress = FindViewById<EditText>(Resource.Id.ip_text);
            port = FindViewById<EditText>(Resource.Id.port_text);

            Button connectToServer = this.FindViewById<Button>(Resource.Id.connect_button);
            connectToServer.Click += (sender, e) =>
            {
                Toast.MakeText(this, "CONNECTED", ToastLength.Long).Show();
            };
        }
    }
}