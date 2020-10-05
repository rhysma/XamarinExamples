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
    class HomeScreenAdapter : BaseAdapter<Contacts>
    {
        List<Contacts> myContacts;
        Activity context;

        public HomeScreenAdapter(Activity context, List<Contacts> contacts) : base()
        {
            this.myContacts = contacts;
            this.context = context;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override Contacts this[int position]
        {
            get
            {
                return myContacts[position];
            }
        }

        public override int Count
        {
            get
            {
                return myContacts.Count;
            }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            //this sets up to re-use an existing view if one is available
            View view = convertView;
            if(view == null)
            {
                view = context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem1, null);
            }

            view.FindViewById<TextView>(Android.Resource.Id.Text1).Text = myContacts[position].ToString();
            return view;
        }
    }
}