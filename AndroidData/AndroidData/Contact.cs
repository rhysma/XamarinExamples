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
    class Contact
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }

        public Contact(string name, string phone)
        {
            Name = name;
            PhoneNumber = phone;
        }

        public override string ToString()
        {
            return Name + "  " + PhoneNumber;
        }

    }
}