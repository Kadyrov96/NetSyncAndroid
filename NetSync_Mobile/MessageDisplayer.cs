using Android.App;
using Android.Widget;

namespace NetSync_Mobile
{
    class MessageDisplayer
    {
        public static void ShowAlertMessage(Activity currentActivity, string title, string message)
        {
            AlertDialog.Builder alert = new AlertDialog.Builder(currentActivity);
            alert.SetIcon(Resource.Drawable.a);
            alert.SetTitle(title);
            alert.SetMessage(message);
            alert.SetPositiveButton("OK", (senderAlert, args) => {
                Toast.MakeText(currentActivity, "Accepted!", ToastLength.Short).Show();
            });

            alert.SetNegativeButton("Cancel", (senderAlert, args) => {
                Toast.MakeText(currentActivity, "Cancelled!", ToastLength.Short).Show();
            });

            Dialog dialog = alert.Create();
            dialog.Show();
        }
    }
}