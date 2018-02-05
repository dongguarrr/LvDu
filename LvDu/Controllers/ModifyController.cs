using LvDu.dboUtils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace LvDu.Controllers
{
    public class ModifyController : Controller
    {
        // GET: Modify
        public ActionResult Index()
        {
            List<Entity.Nav> list = new List<Entity.Nav>();
            list = getList();
            ViewBag.list = list;
            return View();
        }


        public ActionResult GetSonList(Guid id)
        {

            List<Entity.Info> infoList = new List<Entity.Info>();

            ViewBag.list = getInfoListByUp(id);

            return View();
        }

        public ActionResult QueryFromInfo(Guid id)
        {
            List<Entity.Info> infoList = new List<Entity.Info>();
            List<Entity.Nav> navList = new List<Entity.Nav>();
            String[] title = new String[100];
            for (int i = 0; i < navList.Count; i++)
            {
                title[i] = navList[i].title;
            }
            ViewBag.titleArray = title; 
            ViewBag.infoList = getInfoListById(id);
            return View();
        }

        public string UpdateInfo(Guid id, String position, String info, String img_s, String img_b, String video)
        {

            DboUtils db = new DboUtils();
            String s = Request.Form["orgLookup.id"];


            Guid up_item = Guid.Parse(s);

            String sql = "update top_info set position='" + position + "',up_item='" +
                up_item + "',info='" + info + "',img_s='" + img_s + "',img_b='" + img_b + "',video='" + video +
                "'where id='" + id + "'";
            int x = db.update(sql);

            string message = "操作失败";
            if (x > 0)
            {
                message = "操作成功";
                string json = JsonConvert.SerializeObject(new { message = message, statusCode = 200, navTabId = up_item, callbackType = "closeCurrent", forwardUrl = "" });
                return json;
            }
            else
            {
                string json = JsonConvert.SerializeObject(new { message = message, statusCode = 300, navTabId = up_item, callbackType = "", forwardUrl = "" });
                return json;
            }
        }

        public string DeleteFromInfo(Guid id)
        {

            DboUtils db = new DboUtils();

            List<Entity.Info> list = new List<Entity.Info>();
            list = getInfoListById(id);

            Guid up_item = list[0].up_item;
            

            String sql = "delete from top_info where id='"+id+"'";
            int x = db.delete(sql);

            string message = "操作失败";
            if (x > 0)
            {
                message = "操作成功";
                string json = JsonConvert.SerializeObject(new { message = message, statusCode = 200, navTabId = up_item, callbackType = "", forwardUrl = "" });
                return json;
            }
            else
            {
                string json = JsonConvert.SerializeObject(new { message = message, statusCode = 300, navTabId = up_item, callbackType = "", forwardUrl = "" });
                return json;
            }
        }

        public ActionResult InsertView()
        {

            List<Entity.Nav> navList = new List<Entity.Nav>();
            String[] title = new String[100];
            for (int i = 0; i < navList.Count; i++)
            {
                title[i] = navList[i].title;
            }
            ViewBag.titleArray = title;
            return View();

        }

        public String InsertIntoInfo(String title, String position, String info, String img_s, String img_b, String video)
        {
            DboUtils db = new DboUtils();
            String s = Request.Form["orgLookup.id"];
            Guid up_item = Guid.Parse(s);

            String sql = "INSERT INTO top_info(title, up_item, position, info, img_s, img_b, video)"
                                    +" VALUES('"+title+"','"+up_item+"','"+position+"','"+info+"','"+img_s+"','"+img_b+"','"+video+"')";
            int x = db.delete(sql);
            string message = "操作失败";
            if (x > 0)
            {
                message = "操作成功";
                string json = JsonConvert.SerializeObject(new { message = message, statusCode = 200, navTabId = up_item, callbackType = "c", forwardUrl = "" });
                return json;
            }
            else
            {
                string json = JsonConvert.SerializeObject(new { message = message, statusCode = 300, navTabId = up_item, callbackType = "", forwardUrl = "" });
                return json;
            }
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

        private List<Entity.Info> getInfoListByUp(Guid id)
        {
            DboUtils db = new DboUtils();
            String sql = "select * from top_info where up_item='" + id + "'order by position";
            DataSet ds = db.query(sql);
            List<Entity.Info> list = new List<Entity.Info>();
            foreach (DataRow mDr in ds.Tables[0].Rows)
            {
                Entity.Info son = new Entity.Info()
                {
                    id = (Guid)mDr["id"],
                    title = mDr["title"].ToString(),
                    up_item = (Guid)mDr["up_item"],
                    position = (int)mDr["position"],
                    info = mDr["info"].ToString(),
                    img_b = mDr["img_b"].ToString(),
                    img_s = mDr["img_s"].ToString(),
                    video = mDr["video"].ToString()
                };
                list.Add(son);
            }
            return list;
        }
        private List<LvDu.Entity.Info> getInfoListById(Guid id)
        {
            DboUtils db = new DboUtils();
            String sql = "select * from top_info where id='" + id + "'order by position";
            DataSet ds = db.query(sql);
            List<Entity.Info> list = new List<Entity.Info>();
            foreach (DataRow mDr in ds.Tables[0].Rows)
            {
                Entity.Info son = new Entity.Info()
                {
                    id = (Guid)mDr["id"],
                    title = mDr["title"].ToString(),
                    up_item = (Guid)mDr["up_item"],
                    position = (int)mDr["position"],
                    info = mDr["info"].ToString(),
                    img_b = mDr["img_b"].ToString(),
                    img_s = mDr["img_s"].ToString(),
                    video = mDr["video"].ToString()
                };
                list.Add(son);
            }
            return list;
        }
    }
}