using Android.App;
using Android.Views;
using Android.Widget;

namespace NetSync_Mobile
{
    class MessageDisplayer
    {
        static void ShowMessage(Activity currentActivity, string title, string message, int msgTypeFlag)
        {
            ContextThemeWrapper ctw = new ContextThemeWrapper(currentActivity, Android.Resource.Style.ThemeHoloLightDarkActionBar);
            AlertDialog.Builder alert = new AlertDialog.Builder(ctw);
            alert.SetIcon(msgTypeFlag);
            alert.SetTitle(title);
            alert.SetMessage(message);
            alert.SetPositiveButton("OK", (senderAlert, args) =>
            {
                Toast.MakeText(currentActivity, "Принято!", ToastLength.Short).Show();
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