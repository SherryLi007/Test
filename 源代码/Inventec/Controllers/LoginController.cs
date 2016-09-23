using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inventec.Models;
using Inventec.DAL;
using System.Threading;
using System.Globalization;
using System.Reflection;

namespace Inventec.Controllers
{
    public class LoginController : Controller
    {
        private Result result = new Result();
        private InventecbizContext db = new InventecbizContext();

        public ActionResult Tips()
        {
            return View();
        }

        //首页
        public ActionResult Index()
        {
            if (Session["Lang"] == null)
            {
                Session["Lang"] = "zh-CN";
                Thread.CurrentThread.CurrentCulture = new CultureInfo("zh-CN");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("zh-CN");
            }

            HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies.Get("LoginRemeber");
            if (cookie != null)
            {
                ViewBag.UserName = (cookie["UserName"] == null ? "" : cookie["UserName"].ToString().Trim());
                ViewBag.company = new SelectList(db.OrgOrganization.Where(p => p.isactive == true), "CompanyCode", "organization", (cookie["Company"] != null ? cookie["Company"].ToString() : ""));
                ViewBag.Remember = "1";
            }
            else
                ViewBag.company = new SelectList(db.OrgOrganization.Where(p=>p.isactive==true), "CompanyCode", "organization");

            return View();
        }

        //切换中文版本
        public ActionResult CN()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("zh-CN");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("zh-CN");
            Session["Lang"] = "zh-CN";
            return RedirectToAction("Index");
        }

        //切换英文版本
        public ActionResult TW()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
            Session["Lang"] = "zh-TW";
            return RedirectToAction("Index");
        }

        //用户登录
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public JsonResult Login()
        {
            string password = Common.Encryption.MD5Encrypt(Request.Form["userpassword"].ToString().Trim());
            string loginName = "Inventec\\" + Request.Form["username"].ToString().Trim();
            string orgcompany = Request.Form["company"].ToString();
            string loginUser = Request.Form["username"].ToString().Trim().Replace("/","");
            Models.OrgUser user = BaseDAL<OrgUser>.Find(db, p => (p.loginname == loginName || p.email == loginUser || p.usercode == loginUser || p.username == loginUser) && p.password == password && p.isactive == "1");
            if (user != null)
            {
                //Models.OrgJob job = BaseDAL<OrgJob>.Find(db, p => p.org_department.org_organization.companycode == orgcompany && p.userid == user.id);

                //if (job != null)
               // {
                    //VOrgUserdepartment userdept = BaseDAL<VOrgUserdepartment>.Find(db, p => p.loginname == user.loginname && p.organization.IndexOf(orgcompany) >= 0);
                    //if (userdept != null)
                    //{
                    //    Session["Deptment"] = userdept.departmentname;
                    //    Session["Deptmentid"] = userdept.departmentid;
                    //}

                    Session["User"] = user;
                    Session["Lang"] = user.language == "TW" ? "zh-CN" : "zh-CN";
                    Session["UserName"] = user.usernamecn;
                    Session["UserPhoto"] = user.picture;
                    Session["Company"] = orgcompany;
                    Session["Currency"] = BaseDAL<OrgOrganization>.Find(db, p => p.companycode == orgcompany).currencyid;
                    Session["Organization"] = BaseDAL<OrgOrganization>.Find(db, p => p.companycode == orgcompany).organization;
                    Session["Theme"] = user.theme;
                    Session["RoleMenus"] = String.Join(",", db.SecMenurightsobject.Where(n => db.SecMenurightsmember.Where(m => m.groupname.ToLower() == "all users" || m.memberid == user.id || db.OrgGroupmember.Where(g => g.memberid == user.id).Select(g => g.groupid).Contains(m.groupid)).Select(m => m.rightsid).Contains(n.rightsid)).Select(n => n.menuid).Distinct().ToArray());

                    //Session["Agent"] = String.Join(",", BaseDAL<WfAgent>.FindList(db, p => p.agentuserid == user.id && p.startdate <= DateTime.Now && p.enddate >= DateTime.Now, true, p => p.id).Select(p => p.org_user.loginname).ToArray());            

                    result.status = 1;
                    result.info = LangString("LoginOK");

                    if (Request["remberMe"] != null)
                    {
                        HttpCookie cookiee = new HttpCookie("LoginRemeber");
                        cookiee.Expires = DateTime.Now.AddMonths(1);
                        cookiee["UserName"] = Request.Form["username"].ToString();
                        cookiee["Company"] = Request.Form["company"].ToString();
                        System.Web.HttpContext.Current.Response.Cookies.Add(cookiee);
                    }
                //}
                //else
                //{
                //    result.status = 0;
                //    result.info = LangString("LoginFail");
                //}
            }
            else
            {
                result.status = 0;
                result.info = LangString("LoginFail");
            }

            return Json(result);
        }


        public ActionResult Logout()
        {
            Session.Abandon();
            return View();
        }

        private string LangString(string key)
        {
            Type resourceType = (Thread.CurrentThread.CurrentUICulture.Name == "zh-TW") ? typeof(App_GlobalResources.zh_TW) : typeof(App_GlobalResources.zh_CN);
            if (Session["Lang"] != null)
            {
                resourceType = (Session["Lang"].ToString() == "zh-TW") ? typeof(App_GlobalResources.zh_TW) : typeof(App_GlobalResources.zh_CN);
            }
            PropertyInfo p = resourceType.GetProperty(key);
            if (p != null)
                return p.GetValue(null, null).ToString();
            else
                return "undefined";
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ForgetPassword()
        {
            result.info = LangString("ResetPasswordFail");
            string usermail = Request.Form["usermail"].ToString().Trim();
            Models.OrgUser orgUser = BaseDAL<OrgUser>.Find(db, p => p.email == usermail && p.isactive == "1");
            if (orgUser != null)
            {
                string strNewPW = Common.ValidateHelper.CreateValidateCode(8);
                orgUser.istemp = true;
                orgUser.password = Common.Encryption.MD5Encrypt(strNewPW);

                if (BaseDAL<OrgUser>.Update(db, orgUser))
                {
                    String template = "";  //GetMailTemplate("RESETPASSWORD");
                    if (!String.IsNullOrEmpty(template))
                    {
                        template = String.Format(template, orgUser.username, strNewPW);
                        Common.MailHelper.SendMail(orgUser.email, orgUser.username, "E-Paper重置密码", template);
                    }

                    result.status = 1;
                    result.info = LangString("ResetPasswordOK");
                    result.url = Url.Action("Index");
                }
            }

            return Json(result);
        }

    }
}