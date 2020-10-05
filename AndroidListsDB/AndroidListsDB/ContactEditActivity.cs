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

namespace AndroidListsDB
{
    [Activity(Label = "ContactEditActivity")]
    public class ContactEditActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.ContactEdit);

            string name = Intent.GetStringExtra("Name");
            string phone = Intent.GetStringExtra("Phone");

            //populate our textboxes with this information
            EditText nameBox = FindViewById<EditText>(Resource.Id.editName);
            nameBox.Text = name;
            EditText phoneBox = FindViewById<EditText>(Resource.Id.editPhone);
            phoneBox.Text = phone;

            //button click event
            Button submit = FindViewById<Button>(Resource.Id.Submit);
            submit.Click += delegate
            {
                //when the user submits the activity form we want to pass
                //the name and phone number back to the main activity
                name = nameBox.Text;
                phone = phoneBox.Text;

                Intent returnIntent = new Intent();
                returnIntent.PutExtra("Name", name);
                returnIntent.PutExtra("Phone", phone);

                SetResult(Result.Ok, returnIntent);
                Finish();
            };


        }
    }
}