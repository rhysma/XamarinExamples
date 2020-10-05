using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;

namespace Listviews
{
    [Activity(Label = "Listviews", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : ListActivity
    {
        string[] items = new string[5];
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            ISharedPreferences prefs = Application.Context.GetSharedPreferences("USER_DATA", FileCreationMode.Private);

            // Set our view from the "main" layout resource
            // SetContentView (Resource.Layout.Main);
            //items = new string[] {"Steak", "Rice", "Pasta", "Salad", "Chicken" };

            //read from shared preferences to get the data 
            //and then populate the listadapter
            items[0] = prefs.GetString("item1", null);
            items[1] = prefs.GetString("item2", null);
            items[2] = prefs.GetString("item3", null);
            items[3] = prefs.GetString("item4", null);
            items[4] = prefs.GetString("item5", null);

            ListAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, items);

            ////setting up shared preferences
            //
            //ISharedPreferencesEditor editor = prefs.Edit();

            ////key-value pairs go here
            //editor.PutString("item1", "Steak");
            //editor.PutString("item2", "Rice");
            //editor.PutString("item3", "Pasta");
            //editor.PutString("item4", "Salad");
            //editor.PutString("item5", "Chicken");

            ////apply the edits
            //editor.Apply();

        }
    }
}

