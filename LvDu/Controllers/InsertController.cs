using LvDu.dboUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LvDu.Controllers
{
    public class InsertController : Controller
    {
        // GET: Insert
        public ActionResult Index()
        {
            DboUtils db = new DboUtils();
            int[] array = new int[10];
            for (int i = 1; i < 10; i++)
            {
                String sql = "INSERT INTO top_info(title, up_item, position, info, img_s, img_b, video) values('俏江南公告-"+i+ "','C81B33D6-BA93-4761-B740-988D20589643','" + i+"','','/image/notice"+i+".jpg' , '' , '')";
                //String sql = "insert into user_info(username,pwd)values('1','')";
                array[i-1] = db.insert(sql);
                
            }

            ViewBag.array = array;
            return View();
        }
    }
}