using System;
using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;
using Android.Locations;
using Android.Util;
using Android.Gms.Location.Places.UI;

namespace PizzaMe
{

    [Activity(Label = "PizzaMe", MainLauncher = true, Icon = "@drawable/icon")]
    //Implement ILocationListener interface to get location updates
    public class MainActivity : Activity, ILocationListener
    {
        LocationManager locMgr;
        ImageButton pizzaButton;
        string latitude;
        string longitude;
        string provider;
        string tag = "PizzaMe";
        private static readonly int PLACE_PICKER_REQUEST = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            Log.Debug(tag, "OnCreate called");

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            //remove the action bar from the top
            ActionBar.Hide();

            //get the pizza button reference
            pizzaButton = FindViewById<ImageButton>(Resource.Id.pizzaButton);
            pizzaButton.Click += OnPizzaClick;

        }

        protected override void OnStart()
        {
            base.OnStart();
            Log.Debug(tag, "OnStart called");
        }

        // OnResume gets called every time the activity starts, so we'll put our RequestLocationUpdates
        // code here
        protected override void OnResume()
        {
            base.OnResume();
            Log.Debug(tag, "OnResume called");

            // initialize location manager
            locMgr = GetSystemService(Context.LocationService) as LocationManager;

                //pizzaButton.Click += delegate
                //{
                //    // pass in the provider (GPS), 
                //    // the minimum time between updates (in seconds), 
                //    // the minimum distance the user needs to move to generate an update (in meters),
                //    // and an ILocationListener (recall that this class impletents the ILocationListener interface)
                //    if (locMgr.AllProviders.Contains(LocationManager.NetworkProvider)
                //        && locMgr.IsProviderEnabled(LocationManager.NetworkProvider))
                //    {
                //        locMgr.RequestLocationUpdates(LocationManager.NetworkProvider, 2000, 1, this);

                //        // Create a Uri from an intent string. Use the result to create an Intent.
                //        var mapsIntentUri = Android.Net.Uri.Parse("geo:" + latitude + "," + longitude + "?q=pizza");
                //        Intent mapIntent = new Intent(Intent.ActionView, mapsIntentUri);
                //        mapIntent.SetPackage("com.google.android.apps.maps");

                //        //start the intent
                //        StartActivity(mapIntent);
                //    }
                //    else
                //    {
                //        Toast.MakeText(this, "The Network Provider does not exist or is not enabled!", ToastLength.Long).Show();
                //    }
                //};
            }

        protected override void OnPause()
        {
            base.OnPause();
            // RemoveUpdates takes a pending intent - here, we pass the current Activity
            locMgr.RemoveUpdates(this);
            Log.Debug(tag, "Location updates paused because application is entering the background");
        }

        protected override void OnStop()
        {
            base.OnStop();
            Log.Debug(tag, "OnStop called");
        }

        public void OnLocationChanged(Android.Locations.Location location)
        {
            Log.Debug(tag, "Location changed");

            latitude = location.Latitude.ToString();
            longitude = location.Longitude.ToString();
            provider = location.Provider.ToString();

            Log.Debug(tag, "location:" + latitude + " " + longitude);
        }
        public void OnProviderDisabled(string provider)
        {
            Log.Debug(tag, provider + " disabled by user");
        }
        public void OnProviderEnabled(string provider)
        {
            Log.Debug(tag, provider + " enabled by user");
        }
        public void OnStatusChanged(string provider, Availability status, Bundle extras)
        {
            Log.Debug(tag, provider + " availability has changed to " + status.ToString());
        }

        private void OnPizzaClick(object sender, EventArgs eventArgs)
        {
            //create a builder for the place picker
            var builder = new PlacePicker.IntentBuilder();
            StartActivityForResult(builder.Build(this), PLACE_PICKER_REQUEST);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (requestCode == PLACE_PICKER_REQUEST && resultCode == Result.Ok)
            {
                GetPlaceFromPicker(data);
            }

            base.OnActivityResult(requestCode, resultCode, data);
        }

        private void GetPlaceFromPicker(Intent data)
        {
           
        }
    }
}

