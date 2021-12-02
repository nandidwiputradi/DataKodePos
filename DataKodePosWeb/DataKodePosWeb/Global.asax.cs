using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using DataKodePosWeb.API;
using Newtonsoft.Json;

namespace DataKodePosWeb
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            Controller ctrl = new Controller();
            string path = Request.Path;
            var QueryString = Request.QueryString;
            var frm = Request.Form;
            List<string> _list = path.Split('/').ToList();
            if (_list.Count > 1)
            {
                string action = _list[1];

                var rawReqData = Request.Form.AllKeys.ToDictionary(k => k, k => Request.Form.GetValues(k));
                Dictionary<string, string> filterReqData = new Dictionary<string, string>();
                foreach(var dict in rawReqData)
                {
                    filterReqData.Add(dict.Key, dict.Value[0]);
                }

                string ser = JsonConvert.SerializeObject(filterReqData);
                RequestData.Data = JsonConvert.DeserializeObject<SearchData>(ser);
                
                if (!string.IsNullOrEmpty(action))
                {
                    System.Reflection.MethodInfo mi = ctrl.GetType().GetMethod(action);
                    object obj = mi.Invoke(ctrl, null);
                    Response.Write(obj);
                    Response.Flush();
                    Response.Close();
                }
            }
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}