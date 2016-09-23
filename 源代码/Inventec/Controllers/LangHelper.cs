using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Inventec
{
    public static class LangHelper
    {
        //界面普通文字的多语言
        public static string LangString(this HtmlHelper htmlhelper, string key)
        {
            Type resourceType = (Thread.CurrentThread.CurrentUICulture.Name == "zh-TW") ? typeof(App_GlobalResources.zh_TW) : typeof(App_GlobalResources.zh_CN);
            if (htmlhelper.ViewContext.HttpContext.Session["Lang"] != null)
            {
                resourceType = (htmlhelper.ViewContext.HttpContext.Session["Lang"].ToString() == "zh-TW") ? typeof(App_GlobalResources.zh_TW) : typeof(App_GlobalResources.zh_CN);
            }
            PropertyInfo p = resourceType.GetProperty(key);
            if (p != null)
                return p.GetValue(null, null).ToString();
            else
                return "undefined";
        }

        public static string LangString(this HtmlHelper htmlhelper, string key, string langType)
        {
            if (String.IsNullOrEmpty(langType))
                return "undefined";
            Type resourceType = (langType == "tw") ? typeof(App_GlobalResources.zh_TW) : typeof(App_GlobalResources.zh_CN);
            PropertyInfo p = resourceType.GetProperty(key);
            if (p != null)
                return p.GetValue(null, null).ToString();
            else
                return "undefined";
        }

        //js定义多语言弹出框
        public static string LangOutJsVar(this HtmlHelper htmlhelper, string key)
        {
            Type resourceType = (Thread.CurrentThread.CurrentUICulture.Name == "zh-TW") ? typeof(App_GlobalResources.zh_TW) : typeof(App_GlobalResources.zh_CN);
            if (htmlhelper.ViewContext.HttpContext.Session["Lang"] != null)
            {
                resourceType = (htmlhelper.ViewContext.HttpContext.Session["Lang"].ToString() == "zh-TW") ? typeof(App_GlobalResources.zh_TW) : typeof(App_GlobalResources.zh_CN);
            }
            PropertyInfo p = resourceType.GetProperty(key);
            if (p != null)
                return string.Format("var {0} = '{1}'", key, p.GetValue(null, null).ToString());
            else
                return string.Format("var {0} = '{1}'", key, "undefined");
        }
    }
}
