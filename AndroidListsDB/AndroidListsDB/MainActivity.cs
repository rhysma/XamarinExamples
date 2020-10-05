using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.IO;
using SQLite;
using System.Collections.Generic;

namespace AndroidListsDB
{
    [Activity(Label = "AndroidListsDB", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        //string that holds the path information for our database
        string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "dbContacts.db3");
        //Contacts[] myContacts;
        int id;
        List<Contacts> myContacts;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            //setup the db connection
            var db = new SQLiteConnection(dbPath);
           deleteAll();

            //setup a table
            db.CreateTable<Contacts>();

            //create some contact objects
            Contacts contact1 = new Contacts("Sam", "555-555-5555");
            Contacts contact2 = new Contacts("Debbie", "555-555-5544");
            Contacts contact3 = new Contacts("Sarah", "555-555-5588");

            //store the objects in the table
            db.Insert(contact1);
            db.Insert(contact2);
            db.Insert(contact3);

            myContacts = new List<Contacts>();
            var table = db.Table<Contacts>();

            foreach (var item in table)
            {
                myContacts.Add(item);
            }

            //get our listview 
            var lv = FindViewById<ListView>(Resource.Id.contactsListView);

            //assign the adapter
            lv.Adapter = new HomeScreenAdapter(this, myContacts);

            //we have to register the listview for context menu
            //so that the system knows the behavior to use
            RegisterForContextMenu(lv);
        }

        public override void OnCreateContextMenu(IContextMenu menu, View v, IContextMenuContextMenuInfo menuInfo)
        {
            if(v.Id == Resource.Id.contactsListView)
            {
                var info = (AdapterView.AdapterContextMenuInfo)menuInfo;
                menu.SetHeaderTitle("Menu Title");
                var menuItems = Resources.GetStringArray(Resource.Array.menu);
                for(int i=0; i < menuItems.Length; i++)
                {
                    menu.Add(Menu.None, i, i, menuItems[i]);
                }
            }
        }

        public override bool OnContextItemSelected(IMenuItem item)
        {
            var info = (AdapterView.AdapterContextMenuInfo)item.MenuInfo;
            var index = item.ItemId;
            var menuItem = Resources.GetStringArray(Resource.Array.menu);
            var menuItemName = menuItem[index];
            Contacts contactName = myContacts[info.Position];
            id = info.Position;

            //Toast.MakeText(this, string.Format("Selected {0} for item {1}", menuItemName, contactName), ToastLength.Short).Show();

            //did they choose edit or delete?

            if (menuItemName == "Edit")
            {
                //open up our other activity and populate it with the current contact info
                //so the user can edit if they want
                var editActivity = new Intent(this, typeof(ContactEditActivity));
                editActivity.PutExtra("Name", Convert.ToString(contactName.Name));
                editActivity.PutExtra("Phone", Convert.ToString(contactName.PhoneNumber));
                StartActivityForResult(editActivity, 1);


            }
            else if(menuItemName == "Delete")
            {
                //delete the contact from the DB and update the displayed contacts
                var builder = new AlertDialog.Builder(this);
                builder.SetMessage("This will delete contact: " + contactName.Name + " OK?");
                builder.SetPositiveButton("OK", (s, e) => {
                    //they clicked OK
                    //delete the contact from the db
                    var db = new SQLiteConnection(dbPath);
                    db.Delete(contactName);

                    //delete the contact from the list
                    myContacts.Remove(contactName);

                });
                builder.SetNegativeButton("Cancel",(s, e) =>
                {
                    //they clicked cancel
                    return;
                 });
                builder.Create().Show();

            }

            return true;
        }


        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            if(requestCode == 1)
            {
                //get the object we are working on
                Contacts myContact = myContacts[id];

                //collect the new/updated name and phone number for our contact
                string name = data.GetStringExtra("Name");
                string phone = data.GetStringExtra("Phone");

                //update the contact with this name and phone info
                myContact.Name = name;
                myContact.PhoneNumber = phone;

                //update the database from our list
                var db = new SQLiteConnection(dbPath);
                db.Update(myContact);

            }
        }




        //a method that deletes all of the records in the table
        //for use in testing the app so we don't fill the DB with
        //records every time we run
        public void deleteAll()
        {
            var db = new SQLiteConnection(dbPath);
            db.Execute("delete from Contacts");
        }

    }
}

