﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookieDetector
{
    public interface ICookieRetriever
    {
        CookieJar GetCookies(string url);
    }
}
