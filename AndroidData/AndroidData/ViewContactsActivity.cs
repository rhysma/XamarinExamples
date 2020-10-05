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
    [Activity(Label = "ViewContactsActivity")]
    public class ViewContactsActivity : ListActivity
    {
        List<Contact> contactList = new List<Contact>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //retrieve the information from shared preferences
            var localContacts = Application.Context.GetSharedPreferences("MyContacts", FileCreationMode.Private);
            string name = localContacts.GetString("Name", null);
            string phone = localContacts.GetString("Phone", null);

            Contact myContact = new Contact(name, phone);

            //create an array of items that will go in my list
            contactList.Add(myContact);

            //add the list to the list adapter
            ListAdapter = new ArrayAdapter<Contact>(this, Android.Resource.Layout.SimpleListItem1, contactList);
            
        }

        protected override void OnListItemClick(ListView l, View v, int position, long id)
        {
            //get the item they have clicked so we can modify it
            Contact myContact = contactList.ElementAt(position);

            //remove the old one from the share preferences data
            var localContacts = Application.Context.GetSharedPreferences("MyContacts", FileCreationMode.Private);
            var contactEdit = localContacts.Edit();
            contactEdit.Remove("Name");
            contactEdit.Remove("Phone");
            contactEdit.Commit();

            var intent = new Intent(this, typeof(EditContactActivity));
            intent.PutExtra("Name", myContact.Name);
            intent.PutExtra("Phone", myContact.PhoneNumber);
            StartActivity(intent);
        }


    }
}
    