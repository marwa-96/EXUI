using Ext_FraudingSystem.Models;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Microsoft.AspNet.Identity;
using System.IO;
using System.Net.Mail;
using System.Net.Http;
using System.Web.Configuration;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;
namespace Ext_FraudingSystem.Controllers
{
    public class Us_PageController : Controller
    {
        //
        // GET: /Us_Page/
      
        public ActionResult Index()
        {
         
            using (var clientx = new HttpClient())
            {
                clientx.BaseAddress = new Uri(WebConfigurationManager.AppSettings["ApiServer"].ToString());

                clientx.DefaultRequestHeaders.Accept.Clear();
                clientx.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientx.DefaultRequestHeaders.Authorization =
      new AuthenticationHeaderValue("Bearer", Session["Access"] as string);
                var response = clientx.GetAsync(string.Format("api/Fraud/checkToken")).Result;

                var Result = response.Content.ReadAsStringAsync().Result;
                var Auth = JsonConvert.DeserializeObject<Vm_Auth>(Result);
                if (!Auth.IsAUth && Auth.Message.Length > 0 ){
                    Session["Access"] = "";
                     return Redirect("~/Account/Login");
                }

                else return View(); ;

            }
        }
       
     
        public ActionResult UserCases_Read([DataSourceRequest] DataSourceRequest request)
        {
            using (var clientx = new HttpClient())
            {
                clientx.BaseAddress = new Uri(WebConfigurationManager.AppSettings["ApiServer"].ToString());

                clientx.DefaultRequestHeaders.Accept.Clear();
                clientx.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientx.DefaultRequestHeaders.Authorization =
  new AuthenticationHeaderValue("Bearer", Session["Access"] as string);
                var response = clientx.GetAsync(string.Format("api/Fraud/UserCases_Read")).Result;

                var Result = response.Content.ReadAsStringAsync().Result;
                List<FraudCases> Data = JsonConvert.DeserializeObject<List<FraudCases>>(Result);
                return Json(Data.ToDataSourceResult(request));

            }
        }
        public ActionResult UserCaseAttachmentsGrid_Read([DataSourceRequest] DataSourceRequest request, int FraudId)
        {
            using (var clientx = new HttpClient())
            {
                clientx.BaseAddress = new Uri(WebConfigurationManager.AppSettings["ApiServer"].ToString());

                clientx.DefaultRequestHeaders.Accept.Clear();
                clientx.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientx.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Session["Access"] as string);
                var response = clientx.GetAsync(string.Format("api/Fraud/UserCaseAttachmentsGrid_Read?FraudId={0}",FraudId)).Result;

                var Result = response.Content.ReadAsStringAsync().Result;
                List<object> Data = JsonConvert.DeserializeObject<List<object>>(Result);
                return Json(Data.ToDataSourceResult(request));

            }
        }
       
        public ActionResult SaveUserReply(FraudCaseTrace entity)
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
                    //DateTime ValidDT = new DateTime(2020, 2, 1);
                    //if (ValidDT < DateTime.Now)
                    //{
                    //    return Json(new { Result = false, Message = "Please Contact system administrator to reactivate License" });
                    //}

                    entity.Docs = new List<FraudCasesAttachments>();
                    if (Request.Files.Count > 0)
                    {
                        string BaseDomain = AppDomain.CurrentDomain.BaseDirectory;
                        string path = @"Uploads/FraudCases/" + entity.FraudId + "/" + (DateTime.Now.ToString("dd_MM_yyy_HH_mm_ss_ms")).ToString();

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
                            Attachment.FraudId = entity.FraudId;
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

                        HttpResponseMessage Res = clientx.PostAsync("api/Fraud/SaveUserReply", new StringContent(jsonString.ToString(), Encoding.UTF8, "application/json")).Result;

                        if (Res.IsSuccessStatusCode)
                        {
                            var result = Res.Content.ReadAsStringAsync().Result;
                            var s = Newtonsoft.Json.JsonConvert.DeserializeObject(result);
                            GeneralErrors Data = JsonConvert.DeserializeObject<GeneralErrors>(s.ToString());
                            if (Data.Result)
                                return Json(new { Result = true });
                            else
                                return Json(new { Result = false, Message = Data.ErrorMessage });
                        }
                        else
                            return Json(new { Result = false, Message = "Sorry an Error Occured" });
                    }
                    catch (Exception e)
                    {
                        return Json(new { Result = false, Message = "Sorry,Error Occured" });
                    }
                }
                catch (Exception e)
                {
                    return Json(new { Result = false, Message = "Sorry,Error Occured" });
                }
            }


        }

        public ActionResult GetCaseDataAndReplys(int FraudId)
        {
            using (var clientx = new HttpClient())
            {
                clientx.BaseAddress = new Uri(WebConfigurationManager.AppSettings["ApiServer"].ToString());
                clientx.DefaultRequestHeaders.Accept.Clear();
                clientx.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientx.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Session["Access"] as string);
                var response = clientx.GetAsync(string.Format("api/Fraud/GetCaseDataAndReplys?FraudId={0}&Type={1}", FraudId,0)).Result;
                var Result = response.Content.ReadAsStringAsync().Result;
                VM_CasesReplies Data = JsonConvert.DeserializeObject<VM_CasesReplies>(Result);

                return Json(new { Replys =Data.Replys, Case =Data.Case });

            }
        }
	}



}