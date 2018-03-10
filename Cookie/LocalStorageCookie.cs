using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookieDetector
{
    public class LocalStorageCookie :Cookie
    {
        public LocalStorageCookie() : base() { }

        public override string ToString()
        { 
            return
                "\n\nName: " + Name + "\n"
                + "Type: Local Storage\n"
                + "Retrieved From: " + RetrievedFrom + "\n"
                + "Data Created: " + (new DateTime(DateCreated)).ToString() + "\n"
                + "Domain: " + Domain + "\n"
                + "Path: " + Path + "\n"
                + "Key: " +  Content;
        }

    }
}
