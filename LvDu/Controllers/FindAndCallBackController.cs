using LvDu.dboUtils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LvDu.Controllers
{
    public class FindAndCallBackController : Controller
    {
        // GET: FindAndCallBack
        public ActionResult Index()
        {

            List<Entity.Nav> list = new List<Entity.Nav>();
            list = getList();
            ViewBag.list = list;            
            return View();
        }




        private List<Entity.Nav> getList()
        {


            DboUtils db = new DboUtils();
            String sql = "select top_sum.id ,top_sum.position,top_sum.title,top_sum.nov_type,top_sub.id sonid,top_sub.position sonposition,up_item,createtime,top_sub.title sontitle,top_sub.pagetype from top_sum left join top_sub on top_sum.id = top_sub.up_item order by top_sum.position,top_sub.position";
            DataSet ds = db.query(sql);

            List<Entity.Nav> list = new List<Entity.Nav>();

            foreach (DataRow mDr in ds.Tables[0].Rows)
            {
                Guid id = (Guid)mDr["id"];
                if (list.Where(t => t.id == id).Count() == 0)
                {
                    Entity.Nav nav = new Entity.Nav();
                    nav.id = id;
                    nav.title = mDr["title"].ToString();
                    nav.position = (int)mDr["position"];
                    nav.novType = (Boolean)mDr["nov_type"];
                    nav.sonList = new List<Entity.Son>();
                    list.Add(nav);
                    if (nav.novType)
                        AddSon(nav, mDr);
                }
                else
                {
                    var nav = list.Where(t => t.id == id).FirstOrDefault();
                    AddSon(nav, mDr);
                }
            }

            return list;
        }
        private void AddSon(Entity.Nav nav, DataRow mDr)
        {
            Entity.Son son = new Entity.Son();

            son.id = (Guid)mDr["sonid"];
            son.up_item = (Guid)mDr["up_item"];
            son.position = (int)mDr["sonposition"];
            son.title = mDr["sontitle"].ToString();

            son.createtime = (DateTime)mDr["createtime"];
            son.pageType = mDr["pagetype"].ToString();

            nav.sonList.Add(son);
        }
    }
}