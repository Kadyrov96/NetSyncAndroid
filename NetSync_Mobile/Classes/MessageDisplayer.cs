using Android.App;
using Android.Widget;

namespace NetSync_Mobile
{
    class MessageDisplayer
    {
        static void ShowMessage(Activity currentActivity, string title, string message, int msgTypeFlag)
        {
            AlertDialog.Builder alert = new AlertDialog.Builder(currentActivity);
            alert.SetIcon(msgTypeFlag);
            alert.SetTitle(title);
            alert.SetMessage(message);
            alert.SetPositiveButton("OK", (senderAlert, args) =>
            {
                Toast.MakeText(currentActivity, "Accepted!", ToastLength.Short).Show();
            });

            Dialog dialog = alert.Create();
            dialog.Show();
        }

        public static void ShowSuccessMessage(Activity currentActivity, string title, string message)
        {
            ShowMessage(currentActivity, title, message, Resource.Drawable.file);
        }

        public static void ShowAlertMessage(Activity currentActivity, string title, string message)
        {
            ShowMessage(currentActivity, title, message, Resource.Drawable.alert);
        }
    }
}