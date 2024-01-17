using Ext_FraudingSystem.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace Whistleblowing
{
    public class AdminSettingsController : Controller
    {
        //
        // GET: /AminSettings/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SaveAllowBeCustomer(bool AllowBeCustomer)
        {
            using (var clientx = new HttpClient())
            {
                clientx.BaseAddress = new Uri(WebConfigurationManager.AppSettings["ApiServer"].ToString());
                clientx.DefaultRequestHeaders.Accept.Clear();
                clientx.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientx.DefaultRequestHeaders.Authorization =
              new AuthenticationHeaderValue("Bearer", Session["Access"] as string);
                try
                {
                    var response = clientx.GetAsync(string.Format("api/Fraud/SaveAllowBeCustomer?AllowBeCustomer={0}", AllowBeCustomer)).Result;

                    var Result = response.Content.ReadAsStringAsync().Result;

                    GeneralErrors Data = JsonConvert.DeserializeObject<GeneralErrors>(Result);
                    if (Data.Result)
                        return Json(true);
                    else
                        return Json(false);

                }
                catch (Exception e)
                {
                    return Json(new { Result = false, Message = "Sorry,Error Occured" });
                }
            }
        }
        public ActionResult GetAllowBeCustomerStatus()
        {
            using (var clientx = new HttpClient())
            {
                ChangeFraudData data = new ChangeFraudData();
                clientx.BaseAddress = new Uri(WebConfigurationManager.AppSettings["ApiServer"].ToString());

                clientx.DefaultRequestHeaders.Accept.Clear();
                clientx.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientx.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Session["Access"] as string);
                var response = clientx.GetAsync(string.Format("api/Fraud/GetAllowBeCustomerStatus")).Result;

                var Result = response.Content.ReadAsStringAsync().Result;
                bool AllowBeCustomer = JsonConvert.DeserializeObject<bool>(Result);
                return Json(new { AllowBeCustomer = AllowBeCustomer });

            }
        }
	}
}