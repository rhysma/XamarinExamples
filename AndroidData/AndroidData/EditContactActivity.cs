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

namespace AndroidData
{
    [Activity(Label = "EditContactActivity")]
    class EditContactActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.EditContact);

            //put the information in the boxes
            EditText nameBox = FindViewById<EditText>(Resource.Id.nameBox);
            string name = Intent.GetStringExtra("Name") ?? "Data not available";
            EditText phoneBox = FindViewById<EditText>(Resource.Id.phoneBox);
            string phone = Intent.GetStringExtra("Phone") ?? "Data not available";

            Button viewUpdateButton = FindViewById<Button>(Resource.Id.UpdateButton);
            viewUpdateButton.Click += (sender, e) =>
            {
                //add the new contact to the share preferences
                var localContacts = Application.Context.GetSharedPreferences("MyContacts", FileCreationMode.Private);
                var contactEdit = localContacts.Edit();
                contactEdit.PutString("Name", name);
                contactEdit.PutString("Phone", phone);
                contactEdit.Commit();

                var intent = new Intent(this, typeof(ViewContactsActivity));
                StartActivity(intent);
            };
        }
    }
}