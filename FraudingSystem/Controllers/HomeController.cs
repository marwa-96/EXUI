using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ext_FraudingSystem.Models;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Text;
//using System.Web.Mail;
using System.Net.Mail;
using System.IO;
using System.Net.Http;
using System.Web.Configuration;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using FraudingSystem.Models;

namespace Ext_FraudingSystem.Controllers
{
    public class HomeController : Controller
    {
        private object enc_dec_Sec;

        public ActionResult Index()
        {
            bool IsCustomer = false;
            string MainPageTextAR = "";
            string MainPageTextEN = "";


            using (var clientx = new HttpClient())
            {
                clientx.BaseAddress = new Uri(WebConfigurationManager.AppSettings["ApiServer"].ToString());

                clientx.DefaultRequestHeaders.Accept.Clear();
                clientx.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = clientx.GetAsync(string.Format("api/Values/CheckCustomerStatus")).Result;
                var mainpagetext = clientx.GetAsync(string.Format("api/Fraud/GetDescriptionSetting")).Result;


                var Result = response.Content.ReadAsStringAsync().Result;
                IsCustomer = JsonConvert.DeserializeObject<bool>(Result);

                var Mainpagetext = mainpagetext.Content.ReadAsStringAsync().Result;
                var  Data = JsonConvert.DeserializeObject<FraudDescriptionContent>(Mainpagetext);
                MainPageTextAR = Data.Description_box;
                MainPageTextEN = Data.DescriptionboxEN;

            }
            ViewBag.MainPageTextAR = MainPageTextAR;
            ViewBag.MainPageTextEN = MainPageTextEN;
            ViewBag.IsCustomer = IsCustomer;
            var TypePersonalCase = FillNewTypePersonalCase();
            ViewBag.TypePersonalCase = new SelectList(TypePersonalCase, "Value", "Text") { };
            return View();
        }
        public UserManager<ApplicationUser> UserManager { get; private set; }
        public  ActionResult SaveCase(FraudCases entity)
        {
            using (var clientx = new HttpClient())
            {
                clientx.BaseAddress = new Uri(WebConfigurationManager.AppSettings["ApiServer"].ToString());
                entity.BaseAddress = WebConfigurationManager.AppSettings["ApiServer"].ToString();
                clientx.DefaultRequestHeaders.Accept.Clear();
                clientx.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                RegisterViewModel BindingModel = new RegisterViewModel();
                try
                {
                    //DateTime ValidDT = new DateTime(2020, 2, 1);
                    //if (ValidDT < DateTime.Now)
                    //{
                    //    return Json(new { Result = false, Message = "Please Contact system administrator to reactivate License" });
                    //}
                    // Generate Random Username and Password
                    entity.Docs = new List<FraudCasesAttachments>();
                    #region get fraudid

                    try
                    {

                        HttpResponseMessage Res = clientx.PostAsync("api/Values/getfraudid", new StringContent("", Encoding.UTF8, "application/json")).Result;

                        if (Res.IsSuccessStatusCode)
                        {
                            var result = Res.Content.ReadAsStringAsync().Result;
                            var s = Newtonsoft.Json.JsonConvert.DeserializeObject(result);
                            entity.Id = JsonConvert.DeserializeObject<int>(s.ToString());
                           
                        }
                    }


                    catch (Exception e)
                    {
                        return Json(new { Result = false, Message = "Sorry an Error Occured" });
                    } 
                    #endregion
                    //entity.Id = db.FraudCases.FirstOrDefault() == null ? 1 : (db.FraudCases.Max(a => a.Id)) + 1;
                    if (Request.Files.Count > 0)
                    {
                        string BaseDomain = AppDomain.CurrentDomain.BaseDirectory;
                        string path = @"/Uploads/CV/" + (DateTime.Now.ToString("dd_MM_yyy_HH_mm_ss_ms")).ToString();

                        string CompletelyPath = BaseDomain + path;
                        bool FileExists = System.IO.Directory.Exists(CompletelyPath);
                        if (!FileExists)
                            System.IO.Directory.CreateDirectory(CompletelyPath);
                        string host = "";
                        if (string.IsNullOrEmpty(host))
                        {
                            if (string.IsNullOrEmpty(host))
                            {
                                var uri = HttpContext.Request.Url;
                                host = uri.GetLeftPart(UriPartial.Authority);
                            }
                        }
                        string FileName;
                      
                        for (int i = 0; i < Request.Files.Count; i++)
                        {
                            var Attachment = new FraudCasesAttachments();
                            Attachment.FraudId = entity.Id;
                            FileName = Path.GetFileName(Request.Files[i].FileName);
                            Attachment.AttachmentName = FileName.Replace(" ", "");
                            Attachment.URL = host + path + "/" + FileName.Replace(" ", "");
                            Request.Files[i].SaveAs(Server.MapPath(path + "/" + FileName.Replace(" ", "")));
                            entity.Docs.Add(Attachment);
                        }
                    }

                  
                    var jsonString = JsonConvert.SerializeObject(entity);
                    try
                    {

                        HttpResponseMessage Res = clientx.PostAsync("api/Values/SaveCase", new StringContent(jsonString.ToString(), Encoding.UTF8, "application/json")).Result;

                        if (Res.IsSuccessStatusCode)
                        {
                            var result = Res.Content.ReadAsStringAsync().Result;
                            var s = Newtonsoft.Json.JsonConvert.DeserializeObject(result);
                            VM_CaseReturn Data = JsonConvert.DeserializeObject<VM_CaseReturn>(s.ToString());
                            if (Data.Result)
                            {
                                var uri = HttpContext.Request.Url;
                                string host = uri.GetLeftPart(UriPartial.Authority);
                                string Url = @"\Account\Login";
                                BindingModel.RetuenURL = host + Url;
                                BindingModel.UserName = Data.UName;
                                BindingModel.Password = Data.Pwd;
                                BindingModel.CaseId=Data.CaseId;
                                return Json(new { Result= true , BindingModel = BindingModel });
                            }
                            else
                                return Json(new { Result = false, Message = Data.Message });
                        }
                        else
                            return Json(new { Result = false, Message = "Sorry an Error Occured" });
                    }

                    catch (Exception e)
                    {
                        return Json(new { Result = false, Message = "Sorry an Error Occured" });
                    }
                   
                }
                catch (Exception e)
                {

                }
                return Json(new { BindingModel });
            }
        }
        public ActionResult GetDataFormConfiguration( )
        {

            using (var clientx = new HttpClient())
            {
                clientx.BaseAddress = new Uri(WebConfigurationManager.AppSettings["ApiServer"].ToString());

                clientx.DefaultRequestHeaders.Accept.Clear();
                clientx.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientx.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Session["Access"] as string);
                var response = clientx.GetAsync(string.Format("api/Fraud/ReadFormConfiguration")).Result;

                var Result = response.Content.ReadAsStringAsync().Result;
                List<FormConfiguration> Data = JsonConvert.DeserializeObject<List<FormConfiguration>>(Result);
                var jsonResult = Json(Data, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
        }
        public IEnumerable<object> FillNewTypePersonalCase()
        {
            var Data = new List<SelectListItem>()
            {
                new SelectListItem() {Text = "Customer",Value = "1"},
                new SelectListItem() {Text = "NonCustomer",Value = "2"},
                new SelectListItem() {Text = "Employee",Value = "3"},
            };
            return Data;
        }
    }
}