using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LvDu.dboUtils;

namespace LvDu.Controllers
{
    public class HomeController : Controller
    {


        public ActionResult CheckLogin(string username, string password)
        {
            string a = Session["a"].ToString();

            return View();
        }

        // GET: Home
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Home()
        {
            List<Entity.Nav> list = new List<Entity.Nav>();
            list = getList();
            ViewBag.top_nav = list;
            return View();
        }
        /// <summary>
        /// 单页跳转到这儿
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Text(Guid id)
        {
            List<Entity.Nav> list = new List<Entity.Nav>();
            List<Entity.Son> sonList;
            List<Entity.Info> infoList = new List<Entity.Info>();

            list = getList();
            sonList = getSonList(id);
            infoList = getInfoList(id);

            ViewBag.sonList = sonList;
            ViewBag.list = list;
            ViewBag.infoList = infoList;
            ViewBag.id = id;

            return View();
        }
        /// <summary>
        /// 多页（正常的列表）跳转到这儿
        /// </summary>
        /// <param name="nav"></param>
        /// <param name="mDr"></param>
        public ActionResult WaterFall(Guid id)
        {

            List<Entity.Nav> list = new List<Entity.Nav>();
            List<Entity.Son> sonList = new List<Entity.Son>();
            List<Entity.Info> infoList = new List<Entity.Info>();

            list = getList();
            sonList = getSonList(id);
            infoList = getInfoList(id);

            ViewBag.sonList = sonList;
            ViewBag.list = list;
            ViewBag.infoList = infoList;
            ViewBag.id = id;

            return View();
        }
        ///单页不带图片的
        ///
        public ActionResult Page(Guid id)
        {
            List<Entity.Nav> list = new List<Entity.Nav>();
            List<Entity.Son> sonList = new List<Entity.Son>();
            List<Entity.Info> infoList = new List<Entity.Info>();

            list = getList();
            sonList = getSonList(id);
            infoList = getInfoList(id);

            ViewBag.sonList = sonList;
            ViewBag.list = list;
            ViewBag.infoList = infoList;
            ViewBag.id = id;
            return View();

        }

        /// <summary>
        /// 图片点击之后跳转的详情页
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Info(Guid id)
        {
            List<Entity.Nav> list = new List<Entity.Nav>();
            list = getList();

            List<Entity.Info> info = new List<Entity.Info>();
            info = getInfo(id);

            ViewBag.top_nav = list;
            ViewBag.info = info;
            return View();
        }
        public ActionResult Video(Guid id)
        {
            List<Entity.Nav> list = new List<Entity.Nav>();
            list = getList();

            List<Entity.Info> info = new List<Entity.Info>();
            info = getInfo(id);

            ViewBag.list = list;
            ViewBag.id = id;
            ViewBag.info = info;

            return View();
        }

        /// <summary>
        /// 添加二级列表
        /// </summary>
        /// <param name="nav"></param>
        /// <param name="mDr"></param>
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
        /// <summary>
        /// 得到一二级所有列表
        /// 并对二级列表进行归并处理
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// 根据二级列表元素的ID
        /// 查找其上级元素的ID
        /// 然后找出一级元素下的所有的二级元素
        /// </summary>
        /// <param name="sonId"></param>
        /// <returns></returns>
        private List<Entity.Son> getSonList(Guid sonId)
        {
            DboUtils db = new DboUtils();
            String sql1 = "select up_item from top_sub where id='" + sonId + "'";
            DataSet ds1 = db.query(sql1);
            if (ds1.Tables[0].Rows.Count > 0)
            {
                Guid parentId = (Guid)ds1.Tables[0].Rows[0]["up_item"];

                String sql = "select * from top_sub where up_item='" + parentId + "' order by position";
                DataSet ds = db.query(sql);
                List<Entity.Son> list = new List<Entity.Son>();
                foreach (DataRow mDr in ds.Tables[0].Rows)
                {
                    Entity.Son son = new Entity.Son()
                    {
                        id = (Guid)mDr["id"],
                        title = mDr["title"].ToString(),
                        up_item = (Guid)mDr["up_item"],
                        position = (int)mDr["position"],
                        pageType = mDr["pagetype"].ToString()
                    };
                    list.Add(son);
                }
                return list;
            }
            else
                return new List<Entity.Son>();
        }

        /// <summary>
        /// 获取信息表
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        private List<Entity.Info> getInfoList(Guid id)
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
        private List<Entity.Info> getInfo(Guid id)
        {
            DboUtils db = new DboUtils();
            String sql = "select * from top_info where id='" + id + "'";
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