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
using Android.Util;
using Android.Support.V7.App;
using System.Threading.Tasks;

namespace SplashScreenTest
{
    [Activity(MainLauncher = true, Theme="@style/MyTheme.Splash", NoHistory = true)]
    public class SplashActivity : AppCompatActivity
    {
        static readonly string TAG = "X: " + typeof(SplashActivity).Name;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            Log.Debug(TAG, "OnCreate for Splash Activity");
        }

        protected override void OnResume()
        {
            base.OnResume();

            Task startupWork = new Task(() =>
            {
                Log.Debug(TAG, "Performing some startup work...");
                Task.Delay(5000); //delay while we show the splash screen
                Log.Debug(TAG, "Done waiting");
            });
            //what do we want to do when we're done waiting?
            //launch the main activity!
            startupWork.ContinueWith(t =>
            {
                Log.Debug(TAG, "Waiting done, launching main activity");
                StartActivity(new Intent(Application.Context, typeof(MainActivity)));
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}