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
using SQLite;

namespace AndroidListsDB
{
    class Contacts
    {
        [PrimaryKey, AutoIncrement]
        public int EmpID { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }

        public Contacts(string name, string phone)
        {
            Name = name;
            PhoneNumber = phone;
        }

        public Contacts()
        {

        }

        public override string ToString()
        {
            return Name + "  " + PhoneNumber;
        }

    }
}