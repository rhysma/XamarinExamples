using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;


namespace AndroidData
{
    [Activity(Label = "AndroidData", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.SubmitButton);

            button.Click += delegate 
            {
                //get the information from the boxes
                EditText nameBox = FindViewById<EditText>(Resource.Id.nameBox);
                string name = nameBox.Text;
                EditText phoneBox = FindViewById<EditText>(Resource.Id.phoneBox);
                string phone = phoneBox.Text;

                //add the new contact to the share preferences
                var localContacts = Application.Context.GetSharedPreferences("MyContacts", FileCreationMode.Private);
                var contactEdit = localContacts.Edit();
                contactEdit.PutString("Name", name);
                contactEdit.PutString("Phone", phone);
                contactEdit.Commit();

                //create a toast notification to confirm the submission
                Android.Widget.Toast.MakeText(this, "Item Added", ToastLength.Short).Show();


                //clear the boxes of the text
                nameBox.Text = "";
                phoneBox.Text = "";
            };

            Button viewContactButton = FindViewById<Button>(Resource.Id.viewContactButton);
            viewContactButton.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(ViewContactsActivity));
                StartActivity(intent);
            };
        }
    }
}

