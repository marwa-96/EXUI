using Ext_FraudingSystem.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Ext_FraudingSystem
{

    public class MvcApplication : System.Web.HttpApplication
    {
        public UserManager<ApplicationUser> UserManager { get; private set; }
        public RoleManager<IdentityRole> RoleManager { get; private set; }
        public string UserId { get; private set; }
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
             QuickRegister();
        }
        public void QuickRegister()
        {
            using (var clientx = new HttpClient())
            {
                clientx.BaseAddress = new Uri(WebConfigurationManager.AppSettings["ApiServer"].ToString());
                clientx.DefaultRequestHeaders.Accept.Clear();
                clientx.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var jsonString = JsonConvert.SerializeObject("");
                try
                {

                    HttpResponseMessage Res = clientx.PostAsync("api/Values/QuickRegister", new StringContent(jsonString.ToString(), Encoding.UTF8, "application/json")).Result;

                    if (Res.IsSuccessStatusCode)
                    {
                        var result = Res.Content.ReadAsStringAsync().Result;
                        var s = Newtonsoft.Json.JsonConvert.DeserializeObject(result);
                        GeneralErrors Data = JsonConvert.DeserializeObject<GeneralErrors>(s.ToString());
                      
                    }
                   
                }
                catch (Exception e)
                {
                    
                }
            }
          
        }
    }
}
