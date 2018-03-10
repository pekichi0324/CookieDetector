using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace CookieDetector
{
    public class SiteCookieGroupList : IEnumerable<SiteCookieGroup>
    {
        private List<SiteCookieGroup> cookieGroupList;

        public SiteCookieGroupList()
        {
            cookieGroupList = new List<SiteCookieGroup>();
        }
        
        public SiteCookieGroupList(SiteCookieGroup group)
        {
            cookieGroupList = new List<SiteCookieGroup>();
            cookieGroupList.Add(group);
        }

        public void Add(SiteCookieGroup group)
        {
            cookieGroupList.Add(group);
        }

        public string ToJSON()
        {
            return (JsonConvert.SerializeObject(cookieGroupList, Formatting.Indented));
        }

        public void Extend(SiteCookieGroupList list)
        {
            cookieGroupList.AddRange(list.ToList());
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<SiteCookieGroup> GetEnumerator()
        {
            return cookieGroupList.GetEnumerator();
        }
    }

}
