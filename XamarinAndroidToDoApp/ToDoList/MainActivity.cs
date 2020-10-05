using Android.App;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using Android.Content;
using Android.Views;

namespace ToDoList
{
    [Activity(Label = "My ToDo List", MainLauncher = true, Icon = "@drawable/icon", WindowSoftInputMode = SoftInput.AdjustPan | SoftInput.StateHidden)]
    public class MainActivity : ListActivity
    {
        //a list to hold the items for the list
        //this list is also what we will be using to store the data
        public List<string> Items { get; set; }

        //This adapter is used to connect data to the listview
        ArrayAdapter<string> adapter;

        //setup the shared preferences file where todo items will be stored
        ISharedPreferences prefs = Application.Context.GetSharedPreferences("TODO_DATA", FileCreationMode.Private);

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            //initialize the list
            Items = new List<string>();

            //load in any existing list items from Shared Preferences
            LoadList();

            //Add the list of items to the listview
            adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItemMultipleChoice, Items);
            ListAdapter = adapter;


            //getting clicks from the add button
            Button addButton = FindViewById<Button>(Resource.Id.AddButton);
            addButton.Click += delegate
            {
                //button click code

                //get our EditText box and take the string item out of it
                EditText itemText = FindViewById<EditText>(Resource.Id.itemText);
                string item = itemText.Text;

                //make sure we have a non-null item to add
                if(item == "" || item == null)
                {
                    //this is a blank item, just return
                    return;
                }

                //add this new item to our main List of Items
                Items.Add(item);

                //add this new item to our adapter list
                adapter.Add(item);

                //let the listview know the adapter list has changed
                adapter.NotifyDataSetChanged();

                //clear out the textbox for new entry
                itemText.Text = "";

                //update the stored key/value pairs in shared preferences
                UpdatedStoredData();
            };


        }//end of OnCreate

        //this is the method that is fired when an item in the list is checked
        protected override void OnListItemClick(ListView l, View v, int position, long id)
        {
            base.OnListItemClick(l, v, position, id);

            //when the user clicks on the check box we want to remove all checked items
            //of the list as "done" and remove the from shared prefs
            //give a confirmation alert check first
            RunOnUiThread(() =>
            {
                AlertDialog.Builder builder;
                builder = new AlertDialog.Builder(this);
                builder.SetTitle("Confirm");
                builder.SetMessage("Are you done with this task?");
                builder.SetCancelable(true);

                builder.SetPositiveButton("OK", delegate
                {
                    //remove the item from the listview and the Items list
                    var item = Items[position];
                    Items.Remove(item);
                    adapter.Remove(item);

                    //reset the listview so nothing is selected
                    l.ClearChoices();
                    l.RequestLayout();

                    //update the data stored in shared preferences
                    UpdatedStoredData();
                });

                builder.SetNegativeButton("Cancel", delegate
                {
                    return;
                });

                //this launches the "modal" popup 
                builder.Show();

            });

        }

        //this method loads in the items that are in shared preferences
        //and populates the list
        public void LoadList()
        {
            //first we need to find out how many items we have in shared preferences
            //use the itemCount key to find out
            int count = prefs.GetInt("itemCount", 0);

            //loop through the number of items we should have
            //as we get each key/value pair in SP add them to the Items List
            if(count > 0)
            {
                Toast.MakeText(this, "Getting saved items...", Android.Widget.ToastLength.Short).Show();

                for(int i=0; i <= count; i++)
                {
                    string item = prefs.GetString(i.ToString(), null);
                    if(item != null)
                    {
                        //put the item in the list
                        Items.Add(item);
                    }
                }
            }

        }//end of LoadList

        //this method updates the stored key/value pairs we holding in shared preferences
        public void UpdatedStoredData()
        {
            //remove the current items in shared preferences
            ISharedPreferencesEditor editor = prefs.Edit();
            editor.Clear();
            editor.Commit();

            //add all of the items in the list to the shared preferences 
            //so if the app is closed we can re-open the list
            editor = prefs.Edit();

            //key that keeps track of how many items we have stored in SP
            editor.PutInt("itemCount", Items.Count);

            int counter = 0;
            //loop through each item in the list and add it to the shared preferences
            //list to be written
            foreach(string item in Items)
            {
                editor.PutString(counter.ToString(), item);
                counter++;
            }

            //write to SP
            editor.Apply();

        }

    }//end of Class
}

