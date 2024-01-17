using Ext_FraudingSystem.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Configuration;
using System.Web.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace Ext_FraudingSystem.Controllers
{
    public class GeneralController : Controller
    {
        //
        // GET: /General/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult FillFraudStatus(int? Id, string text)
        {
            using (var clientx = new HttpClient())
            {
                clientx.BaseAddress = new Uri(WebConfigurationManager.AppSettings["ApiServer"].ToString());

                clientx.DefaultRequestHeaders.Accept.Clear();
                clientx.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientx.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Session["Access"] as string);
                var response = clientx.GetAsync(string.Format("api/Fraud/FillFraudStatustext?text={0}", text)).Result;
                var Result = response.Content.ReadAsStringAsync().Result;
                List<VM_ComboBox> Data = JsonConvert.DeserializeObject<List<VM_ComboBox>>(Result);
                return Json(Data, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult FillFraudEmployees(int? Id, string text)
        {
            using (var clientx = new HttpClient())
            {
                clientx.BaseAddress = new Uri(WebConfigurationManager.AppSettings["ApiServer"].ToString());

                clientx.DefaultRequestHeaders.Accept.Clear();
                clientx.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientx.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Session["Access"] as string);
                var response = clientx.GetAsync(string.Format("api/Fraud/FillFraudEmployees?text={0}", text)).Result;
                var Result = response.Content.ReadAsStringAsync().Result;
                List<VM_ComboBox> Data = JsonConvert.DeserializeObject<List<VM_ComboBox>>(Result);
                return Json(Data, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult FillLabelCases(int? Id, string text)
        {

            using (var clientx = new HttpClient())
            {
                clientx.BaseAddress = new Uri(WebConfigurationManager.AppSettings["ApiServer"].ToString());

                clientx.DefaultRequestHeaders.Accept.Clear();
                clientx.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientx.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Session["Access"] as string);
                var response = clientx.GetAsync(string.Format("api/Fraud/FillLabelCases?text={0}", text)).Result;
                var Result = response.Content.ReadAsStringAsync().Result;
                List<VM_ComboBox> Data = JsonConvert.DeserializeObject<List<VM_ComboBox>>(Result);
                return Json(Data, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult FillFraudAssignAdminPrivilege(int? Id, string text)
        {
            using (var clientx = new HttpClient())
            {
                clientx.BaseAddress = new Uri(WebConfigurationManager.AppSettings["ApiServer"].ToString());

                clientx.DefaultRequestHeaders.Accept.Clear();
                clientx.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientx.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Session["Access"] as string);
                var response = clientx.GetAsync(string.Format("api/Fraud/FillFraudAssignAdminPrivilege?text={0}", text)).Result;
                var Result = response.Content.ReadAsStringAsync().Result;
                List<VM_ComboBox> Data = JsonConvert.DeserializeObject<List<VM_ComboBox>>(Result);
                return Json(Data, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult FillTypePersonalCase()
        {
            var Data = new List<SelectListItem>()
            {
                new SelectListItem() {Text = "Customer",Value = "1"},
                new SelectListItem() {Text = "NonCustomer",Value = "2"},
                new SelectListItem() {Text = "Employee",Value = "3"},
            };
            return Json(Data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetLogo()
        {
            using (var clientx = new HttpClient())
            {
                clientx.BaseAddress = new Uri(WebConfigurationManager.AppSettings["ApiServer"].ToString());

                clientx.DefaultRequestHeaders.Accept.Clear();
                clientx.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientx.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Session["Access"] as string);
                var responseLogoUrl = clientx.GetAsync(string.Format("api/Values/getLogo")).Result;

                var ResultLogo = responseLogoUrl.Content.ReadAsStringAsync().Result;
                var LogoUrl = JsonConvert.DeserializeObject<string>(ResultLogo);
                return Json(LogoUrl, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult FillEmailAdmin(int? Id, string text)
        {
            using (var clientx = new HttpClient())
            {
                clientx.BaseAddress = new Uri(WebConfigurationManager.AppSettings["ApiServer"].ToString());

                clientx.DefaultRequestHeaders.Accept.Clear();
                clientx.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientx.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Session["Access"] as string);
                var response = clientx.GetAsync(string.Format("api/Fraud/FillEmailAdmin?text={0}", text)).Result;
                var Result = response.Content.ReadAsStringAsync().Result;
                var Data = JsonConvert.DeserializeObject<List<VM_ComboBox>>(Result);
                return Json(Data, JsonRequestBehavior.AllowGet);
            }
        }
    }
}