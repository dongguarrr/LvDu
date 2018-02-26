using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestConfig.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            DboUtils dboUtils = new DboUtils();
            DataSet ds = dboUtils.query("select * from top_sub");

            var showMsg = ds.Tables[0].Rows[0][0].ToString();

            ViewBag.showMsg = showMsg;
            return View();
        }


    }
}