using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LvDu.Entity
{
    public class Nav
    {
        public Guid id { set; get; }
        public Boolean novType { set; get; }
        public int position { set; get; }
        public String title { set; get; }
        public List<Son> sonList { set; get;}
    }

    public class Son
    {
        public Guid id { set; get; }
        public Guid up_item { set; get; }
        public int position { set; get; }
        public String title { set; get; }
        public DateTime createtime { set; get; }
        public String pageType { set; get; }
    }
    public class Info
    {
        public Guid id { set; get; }
        public Guid up_item { set; get; }
        public int position { set; get; }
        public String title { set; get; }
        public String info { set; get; }
        public String img_s { set; get; }
        public String img_b{ set; get; }
        public String video{ set; get; }
    }
}