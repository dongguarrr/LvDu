using LvDu.dboUtils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LvDu.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Login(String username, String password)
        {
            String sql = "select * from admin where username='" + username + "'and pwd='" + password + "'";

            DboUtils db = new DboUtils();
            DataSet ds = db.query(sql);

            if (ds.Tables[0].Rows.Count > 0)
            {
                return Redirect("/Modify/Index");

            }
            else
            {
                ViewBag.errorMessage = "用户名或密码错误";
                return View();
            }

        }

        public ActionResult GetAdminList()
        {
            DboUtils db = new DboUtils();
            String sql = "select * from admin";
            DataSet ds = db.query(sql);
            
            List<Entity.Admin> list = new List<Entity.Admin>();

            foreach (DataRow mDr in ds.Tables[0].Rows)
            {
                Entity.Admin ad = new Entity.Admin();
                ad.username = mDr["username"].ToString();
                ad.pwd = mDr["pwd"].ToString();
                list.Add(ad);
            }
            ViewBag.adminList = list;
            return View();
        }

        public ActionResult QueryFromAdmin(String username)
        {
            List<Entity.Admin> list = new List<Entity.Admin>();
            list = getAdminListByName(username);
            ViewBag.adminList = list;
            return View();
        }

        public String UpdateAdmin(String username,String pwd_confirm)
        {
            String nav = "admin2";
            DboUtils db = new DboUtils();
            String sql = "update admin set pwd='" + pwd_confirm + "'";
            int x = db.update(sql);

            string message = "操作失败";
            if (x > 0)
            {
                message = "操作成功";
                string json = JsonConvert.SerializeObject(new { message = message, statusCode = 200, navTabId = nav, callbackType = "closeCurrent", forwardUrl = "" });
                return json;
            }
            else
            {
                string json = JsonConvert.SerializeObject(new { message = message, statusCode = 300, navTabId = username, callbackType = "", forwardUrl = "" });
                return json;
            }
        }

        public String DeleteFromAdmin(String username)
        {

            String nav = "GoodsClassTree";
            DboUtils db = new DboUtils();
            String sql = "delete from admin where username='" + username + "'";
            int x = db.delete(sql);

            string message = "操作失败";
            if (x > 0)
            {
                message = "操作成功";
                string json = JsonConvert.SerializeObject(new { message = message, statusCode = 200, navTabId = nav, callbackType = "", forwardUrl = "" });
                return json;
            }
            else
            {
                string json = JsonConvert.SerializeObject(new { message = message, statusCode = 300, navTabId = username, callbackType = "", forwardUrl = "" });
                return json;
            }
        }
        private List<LvDu.Entity.Admin> getAdminListByName(String username)
        {
            DboUtils db = new DboUtils();
            String sql = "select * from admin where username='" + username+"'";
            DataSet ds = db.query(sql);
            List<Entity.Admin> list = new List<Entity.Admin>();
            foreach (DataRow mDr in ds.Tables[0].Rows)
            {
                Entity.Admin ad = new Entity.Admin() {
                    username = mDr["username"].ToString(),
                    pwd = mDr["pwd"].ToString()
                };
                list.Add(ad);
            }
            return list;
        }
    }
}