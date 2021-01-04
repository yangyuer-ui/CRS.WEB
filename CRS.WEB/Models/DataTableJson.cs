using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRS.Web.Models
{
    public class DataTableJson<T> where T : class
    {
        public int code { get; set; }
        public string msg { get; set; }
        public int count { get; set; }
        public IList<T> data { get; set; }
    }
}