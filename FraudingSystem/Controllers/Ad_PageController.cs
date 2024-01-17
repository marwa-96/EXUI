using Ext_FraudingSystem.Filters;
using Ext_FraudingSystem.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Text;
using System.Web.Configuration;
using System.Web.Mvc;
using Newtonsoft.Json;
using FraudingSystem.Models;
using static System.Net.Mime.MediaTypeNames;

namespace Ext_FraudingSystem.Controllers
{
    public class Ad_PageController : Controller
    {
        //
        // GET: /Ad_Page/
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
                var responseLogoUrl = clientx.GetAsync(string.Format("api/Values/getLogo")).Result;

                var Result = response.Content.ReadAsStringAsync().Result;
                var Auth = JsonConvert.DeserializeObject<Vm_Auth>(Result);

                var ResultLogo = responseLogoUrl.Content.ReadAsStringAsync().Result;
                var  LogoUrl = JsonConvert.DeserializeObject<string>(ResultLogo);

                if (!Auth.IsAUth && Auth.Message.Length > 0)
                {
                    Session["Access"] = "";
                    return Redirect("~/Home/Index");
                }


                else
                {
                    ViewBag.IsAdmin = Auth.IsAdmin;
                    ViewBag.IsCustomer = Auth.IsCustomer;
                    ViewBag.Roles = Auth.Roles.ToArray();
                    ViewBag.Logo = LogoUrl;
                    return View();
                }

            }
        }

        public ActionResult AdminCases_Read([DataSourceRequest] DataSourceRequest request)
        {
            using (var clientx = new HttpClient())
            {
                clientx.BaseAddress = new Uri(WebConfigurationManager.AppSettings["ApiServer"].ToString());

                clientx.DefaultRequestHeaders.Accept.Clear();
                clientx.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientx.DefaultRequestHeaders.Authorization =
  new AuthenticationHeaderValue("Bearer", Session["Access"] as string);
                var response = clientx.GetAsync(string.Format("api/Fraud/AdminCases_Read")).Result;

                var Result = response.Content.ReadAsStringAsync().Result;
                List<FraudCases> Data = JsonConvert.DeserializeObject<List<FraudCases>>(Result);
                return Json(Data.ToDataSourceResult(request));

            }
        }
        public ActionResult AdminCasesLog_Read([DataSourceRequest] DataSourceRequest request, int? Id)
        {
            using (var clientx = new HttpClient())
            {
                clientx.BaseAddress = new Uri(WebConfigurationManager.AppSettings["ApiServer"].ToString());

                clientx.DefaultRequestHeaders.Accept.Clear();
                clientx.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientx.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Session["Access"] as string);
                var response = clientx.GetAsync(string.Format("api/Fraud/AdminCasesLog_Read?Id={0}", Id ?? 0)).Result;

                var Result = response.Content.ReadAsStringAsync().Result;
                List<CasesLog> Data = JsonConvert.DeserializeObject<List<CasesLog>>(Result);
                return Json(Data.ToDataSourceResult(request));

            }

        }

        public ActionResult AdminCaseAttachmentsGrid_Read([DataSourceRequest] DataSourceRequest request, int FraudId)
        {

            using (var clientx = new HttpClient())
            {
                clientx.BaseAddress = new Uri(WebConfigurationManager.AppSettings["ApiServer"].ToString());

                clientx.DefaultRequestHeaders.Accept.Clear();
                clientx.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientx.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Session["Access"] as string);
                var response = clientx.GetAsync(string.Format("api/Fraud/UserCaseAttachmentsGrid_Read?FraudId={0}", FraudId)).Result;

                var Result = response.Content.ReadAsStringAsync().Result;
                List<FraudCasesAttachments> Data = JsonConvert.DeserializeObject<List<FraudCasesAttachments>>(Result);
                return Json(Data.ToDataSourceResult(request));

            }
        }
        public ActionResult AdminCaseAttachments_Read(int FraudId)
        {

            using (var clientx = new HttpClient())
            {
                clientx.BaseAddress = new Uri(WebConfigurationManager.AppSettings["ApiServer"].ToString());

                clientx.DefaultRequestHeaders.Accept.Clear();
                clientx.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientx.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Session["Access"] as string);
                var response = clientx.GetAsync(string.Format("api/Fraud/UserCaseAttachmentsGrid_Read?FraudId={0}", FraudId)).Result;

                var Result = response.Content.ReadAsStringAsync().Result;
                List<FraudCasesAttachments> Data = JsonConvert.DeserializeObject<List<FraudCasesAttachments>>(Result);
                return Json(Data);

            }
        }
        [SystemMonitoring]
        public ActionResult AdminCaseAttachmentsGrid_Destroy(int Id)
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
                    var response = clientx.GetAsync(string.Format("api/Fraud/AdminCaseAttachmentsGrid_Destroy?Id={0}", Id)).Result;

                    var Result = response.Content.ReadAsStringAsync().Result;

                    GeneralErrors Data = JsonConvert.DeserializeObject<GeneralErrors>(Result);
                    if (Data.Result)
                        return Json(new { Result = true });
                    else
                        return Json(new { Result = false, Message = Data.ErrorMessage });

                }
                catch (Exception e)
                {
                    return Json(new { Result = false, Message = "Sorry,Error Occured" });
                }
            }


        }
        public ActionResult GetOldAttachements(int FraudId)
        {

            using (var clientx = new HttpClient())
            {
                clientx.BaseAddress = new Uri(WebConfigurationManager.AppSettings["ApiServer"].ToString());

                clientx.DefaultRequestHeaders.Accept.Clear();
                clientx.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientx.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Session["Access"] as string);
                var response = clientx.GetAsync(string.Format("api/Fraud/GetOldAttachements?FraudId={0}", FraudId)).Result;

                var Result = response.Content.ReadAsStringAsync().Result;
                List<FraudCasesAttachments> Attachs = JsonConvert.DeserializeObject<List<FraudCasesAttachments>>(Result);
                return Json(new { Attachs });

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
                var response = clientx.GetAsync(string.Format("api/Fraud/GetCaseDataAndReplys?FraudId={0}&Type={1}", FraudId, 1)).Result;
                var Result = response.Content.ReadAsStringAsync().Result;
                VM_CasesReplies Data = JsonConvert.DeserializeObject<VM_CasesReplies>(Result);

                return Json(new { Replys = Data.Replys, Case = Data.Case });

            }

        }
        [SystemMonitoring]
        public ActionResult SaveAdminReply(FraudCaseTrace entity)
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

                        HttpResponseMessage Res = clientx.PostAsync("api/Fraud/SaveAdminReply", new StringContent(jsonString.ToString(), Encoding.UTF8, "application/json")).Result;

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
                            return Json(new { Result = false, Message = "Sorry,Check Api NetWork" });
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
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult StatusGrid_Read([DataSourceRequest] DataSourceRequest request)
        {
            using (var clientx = new HttpClient())
            {
                clientx.BaseAddress = new Uri(WebConfigurationManager.AppSettings["ApiServer"].ToString());

                clientx.DefaultRequestHeaders.Accept.Clear();
                clientx.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientx.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Session["Access"] as string);
                var response = clientx.GetAsync(string.Format("api/Fraud/StatusGrid_Read")).Result;

                var Result = response.Content.ReadAsStringAsync().Result;
                List<FraudStatus> Data = JsonConvert.DeserializeObject<List<FraudStatus>>(Result);
                return Json(Data.ToDataSourceResult(request));

            }
        }
        [SystemMonitoring]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult StatusGrid_Create([DataSourceRequest] DataSourceRequest request, FraudStatus FraudStatus)
        {

            using (var clientx = new HttpClient())
            {
                clientx.BaseAddress = new Uri(WebConfigurationManager.AppSettings["ApiServer"].ToString());
                clientx.DefaultRequestHeaders.Accept.Clear();
                clientx.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientx.DefaultRequestHeaders.Authorization =
              new AuthenticationHeaderValue("Bearer", Session["Access"] as string);
                var jsonString = JsonConvert.SerializeObject(FraudStatus);
                try
                {

                    HttpResponseMessage Res = clientx.PostAsync("api/Fraud/StatusGrid_Create", new StringContent(jsonString.ToString(), Encoding.UTF8, "application/json")).Result;

                    if (Res.IsSuccessStatusCode)
                    {
                        var result = Res.Content.ReadAsStringAsync().Result;
                        var s = Newtonsoft.Json.JsonConvert.DeserializeObject(result);
                        FraudStatus Data = JsonConvert.DeserializeObject<FraudStatus>(s.ToString());
                        if (Data.Id > 0)
                            return Json(new[] { false }.ToDataSourceResult(request, ModelState));
                        else
                        {
                            ModelState.AddModelError("Exist", "Duplicated Data");
                            return Json(new[] { FraudStatus }.ToDataSourceResult(request, ModelState));
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Error", "Sorry an Error Occured");
                        return Json(new[] { FraudStatus }.ToDataSourceResult(request, ModelState));

                    }

                }
                catch (Exception e)
                {
                    ModelState.AddModelError("Exception", "Sorry an Error Occured");
                    return Json(new[] { FraudStatus }.ToDataSourceResult(request, ModelState));
                }
            }

        }
        public ActionResult StatusGrid_Update([DataSourceRequest] DataSourceRequest request, FraudStatus FraudStatus)
        {
            using (var clientx = new HttpClient())
            {
                clientx.BaseAddress = new Uri(WebConfigurationManager.AppSettings["ApiServer"].ToString());
                clientx.DefaultRequestHeaders.Accept.Clear();
                clientx.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientx.DefaultRequestHeaders.Authorization =
              new AuthenticationHeaderValue("Bearer", Session["Access"] as string);
                var jsonString = JsonConvert.SerializeObject(FraudStatus);
                try
                {

                    HttpResponseMessage Res = clientx.PostAsync("api/Fraud/StatusGrid_Update", new StringContent(jsonString.ToString(), Encoding.UTF8, "application/json")).Result;

                    if (Res.IsSuccessStatusCode)
                    {
                        var result = Res.Content.ReadAsStringAsync().Result;
                        var s = Newtonsoft.Json.JsonConvert.DeserializeObject(result);
                        FraudStatus Data = JsonConvert.DeserializeObject<FraudStatus>(s.ToString());
                        if (Data.Id > 0)
                            return Json(new[] { false }.ToDataSourceResult(request, ModelState));
                        else
                        {
                            ModelState.AddModelError("Exist", "Duplicated Data");
                            return Json(new[] { FraudStatus }.ToDataSourceResult(request, ModelState));
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Error", "Sorry an Error Occured");
                        return Json(new[] { FraudStatus }.ToDataSourceResult(request, ModelState));

                    }

                }
                catch (Exception e)
                {
                    ModelState.AddModelError("Exception", "Sorry an Error Occured");
                    return Json(new[] { FraudStatus }.ToDataSourceResult(request, ModelState));
                }
            }

        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult StatusGrid_Destroy([DataSourceRequest] DataSourceRequest request, FraudStatus FraudStatus)
        {

            using (var clientx = new HttpClient())
            {
                clientx.BaseAddress = new Uri(WebConfigurationManager.AppSettings["ApiServer"].ToString());
                clientx.DefaultRequestHeaders.Accept.Clear();
                clientx.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientx.DefaultRequestHeaders.Authorization =
              new AuthenticationHeaderValue("Bearer", Session["Access"] as string);
                var jsonString = JsonConvert.SerializeObject(FraudStatus);
                try
                {

                    HttpResponseMessage Res = clientx.PostAsync("api/Fraud/StatusGrid_Destroy", new StringContent(jsonString.ToString(), Encoding.UTF8, "application/json")).Result;

                    if (Res.IsSuccessStatusCode)
                    {
                        var result = Res.Content.ReadAsStringAsync().Result;
                        var s = Newtonsoft.Json.JsonConvert.DeserializeObject(result);
                        FraudStatus Data = JsonConvert.DeserializeObject<FraudStatus>(s.ToString());
                        if (Data.Id > 0)
                            return Json(new[] { false }.ToDataSourceResult(request, ModelState));
                        else
                        {
                            ModelState.AddModelError("Exist", "Duplicated Data");
                            return Json(new[] { FraudStatus }.ToDataSourceResult(request, ModelState));
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Error", "Sorry an Error Occured");
                        return Json(new[] { FraudStatus }.ToDataSourceResult(request, ModelState));

                    }

                }
                catch (Exception e)
                {
                    ModelState.AddModelError("Exception", "Sorry an Error Occured");
                    return Json(new[] { FraudStatus }.ToDataSourceResult(request, ModelState));
                }
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EmployeesGrid_Read([DataSourceRequest] DataSourceRequest request)
        {
            using (var clientx = new HttpClient())
            {
                clientx.BaseAddress = new Uri(WebConfigurationManager.AppSettings["ApiServer"].ToString());

                clientx.DefaultRequestHeaders.Accept.Clear();
                clientx.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientx.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Session["Access"] as string);
                var response = clientx.GetAsync(string.Format("api/Fraud/EmployeesGrid_Read")).Result;

                var Result = response.Content.ReadAsStringAsync().Result;
                List<FraudEmployees> Data = JsonConvert.DeserializeObject<List<FraudEmployees>>(Result);
                return Json(Data.ToDataSourceResult(request));

            }
        }
        [SystemMonitoring]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EmployeesGrid_Create([DataSourceRequest] DataSourceRequest request, FraudEmployees FraudEmployees)
        {
            using (var clientx = new HttpClient())
            {
                clientx.BaseAddress = new Uri(WebConfigurationManager.AppSettings["ApiServer"].ToString());
                clientx.DefaultRequestHeaders.Accept.Clear();
                clientx.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientx.DefaultRequestHeaders.Authorization =
              new AuthenticationHeaderValue("Bearer", Session["Access"] as string);
                var jsonString = JsonConvert.SerializeObject(FraudEmployees);
                try
                {

                    HttpResponseMessage Res = clientx.PostAsync("api/Fraud/EmployeesGrid_Create", new StringContent(jsonString.ToString(), Encoding.UTF8, "application/json")).Result;

                    if (Res.IsSuccessStatusCode)
                    {
                        var result = Res.Content.ReadAsStringAsync().Result;
                        var s = Newtonsoft.Json.JsonConvert.DeserializeObject(result);
                        FraudEmployees Data = JsonConvert.DeserializeObject<FraudEmployees>(s.ToString());
                        if (Data.Id > 0)
                            return Json(new[] { false }.ToDataSourceResult(request, ModelState));
                        else
                        {
                            ModelState.AddModelError("Exist", "Duplicated Data");
                            return Json(new[] { FraudEmployees }.ToDataSourceResult(request, ModelState));
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Error", "Sorry an Error Occured");
                        return Json(new[] { FraudEmployees }.ToDataSourceResult(request, ModelState));

                    }

                }
                catch (Exception e)
                {
                    ModelState.AddModelError("Exception", "Sorry an Error Occured");
                    return Json(new[] { FraudEmployees }.ToDataSourceResult(request, ModelState));
                }
            }
        }
        [SystemMonitoring]
        public ActionResult EmployeesGrid_Update([DataSourceRequest] DataSourceRequest request, FraudEmployees FraudEmployees)
        {

            using (var clientx = new HttpClient())
            {
                clientx.BaseAddress = new Uri(WebConfigurationManager.AppSettings["ApiServer"].ToString());
                clientx.DefaultRequestHeaders.Accept.Clear();
                clientx.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientx.DefaultRequestHeaders.Authorization =
              new AuthenticationHeaderValue("Bearer", Session["Access"] as string);
                var jsonString = JsonConvert.SerializeObject(FraudEmployees);
                try
                {

                    HttpResponseMessage Res = clientx.PostAsync("api/Fraud/EmployeesGrid_Update", new StringContent(jsonString.ToString(), Encoding.UTF8, "application/json")).Result;

                    if (Res.IsSuccessStatusCode)
                    {
                        var result = Res.Content.ReadAsStringAsync().Result;
                        var s = Newtonsoft.Json.JsonConvert.DeserializeObject(result);
                        FraudEmployees Data = JsonConvert.DeserializeObject<FraudEmployees>(s.ToString());
                        if (Data.Id > 0)
                            return Json(new[] { false }.ToDataSourceResult(request, ModelState));
                        else
                        {
                            ModelState.AddModelError("Exist", "Duplicated Data");
                            return Json(new[] { FraudEmployees }.ToDataSourceResult(request, ModelState));
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Error", "Sorry an Error Occured");
                        return Json(new[] { FraudEmployees }.ToDataSourceResult(request, ModelState));

                    }

                }
                catch (Exception e)
                {
                    ModelState.AddModelError("Exception", "Sorry an Error Occured");
                    return Json(new[] { FraudEmployees }.ToDataSourceResult(request, ModelState));
                }
            }

        }
        [SystemMonitoring]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EmployeesGrid_Destroy([DataSourceRequest] DataSourceRequest request, FraudEmployees FraudEmployees)
        {
            using (var clientx = new HttpClient())
            {
                clientx.BaseAddress = new Uri(WebConfigurationManager.AppSettings["ApiServer"].ToString());
                clientx.DefaultRequestHeaders.Accept.Clear();
                clientx.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientx.DefaultRequestHeaders.Authorization =
              new AuthenticationHeaderValue("Bearer", Session["Access"] as string);
                var jsonString = JsonConvert.SerializeObject(FraudEmployees);
                try
                {

                    HttpResponseMessage Res = clientx.PostAsync("api/Fraud/EmployeesGrid_Destroy", new StringContent(jsonString.ToString(), Encoding.UTF8, "application/json")).Result;

                    if (Res.IsSuccessStatusCode)
                    {
                        var result = Res.Content.ReadAsStringAsync().Result;
                        var s = Newtonsoft.Json.JsonConvert.DeserializeObject(result);
                        FraudEmployees Data = JsonConvert.DeserializeObject<FraudEmployees>(s.ToString());
                        if (Data.Id > 0)
                            return Json(new[] { false }.ToDataSourceResult(request, ModelState));
                        else
                        {
                            ModelState.AddModelError("Exist", "Duplicated Data");
                            return Json(new[] { FraudEmployees }.ToDataSourceResult(request, ModelState));
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Error", "Sorry an Error Occured");
                        return Json(new[] { FraudEmployees }.ToDataSourceResult(request, ModelState));

                    }

                }
                catch (Exception e)
                {
                    ModelState.AddModelError("Exception", "Sorry an Error Occured");
                    return Json(new[] { FraudEmployees }.ToDataSourceResult(request, ModelState));
                }
            }
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult LabelCasesGrid_Read([DataSourceRequest] DataSourceRequest request)
        {
            using (var clientx = new HttpClient())
            {
                clientx.BaseAddress = new Uri(WebConfigurationManager.AppSettings["ApiServer"].ToString());

                clientx.DefaultRequestHeaders.Accept.Clear();
                clientx.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientx.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Session["Access"] as string);
                var response = clientx.GetAsync(string.Format("api/Fraud/LabelCasesGrid_Read")).Result;

                var Result = response.Content.ReadAsStringAsync().Result;
                List<LabelCases> Data = JsonConvert.DeserializeObject<List<LabelCases>>(Result);
                return Json(Data.ToDataSourceResult(request));

            }
        }
        [SystemMonitoring]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult LabelCasesGrid_Create([DataSourceRequest] DataSourceRequest request, LabelCases label)
        {
            using (var clientx = new HttpClient())
            {
                clientx.BaseAddress = new Uri(WebConfigurationManager.AppSettings["ApiServer"].ToString());
                clientx.DefaultRequestHeaders.Accept.Clear();
                clientx.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientx.DefaultRequestHeaders.Authorization =
              new AuthenticationHeaderValue("Bearer", Session["Access"] as string);
                var jsonString = JsonConvert.SerializeObject(label);
                try
                {

                    HttpResponseMessage Res = clientx.PostAsync("api/Fraud/LabelCasesGrid_Create", new StringContent(jsonString.ToString(), Encoding.UTF8, "application/json")).Result;

                    if (Res.IsSuccessStatusCode)
                    {
                        var result = Res.Content.ReadAsStringAsync().Result;
                        var s = Newtonsoft.Json.JsonConvert.DeserializeObject(result);
                        LabelCases Data = JsonConvert.DeserializeObject<LabelCases>(s.ToString());
                        if (Data.Id > 0)
                            return Json(new[] { false }.ToDataSourceResult(request, ModelState));
                        else
                        {
                            ModelState.AddModelError("Exist", "Duplicated Data");
                            return Json(new[] { label }.ToDataSourceResult(request, ModelState));
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Error", "Sorry an Error Occured");
                        return Json(new[] { label }.ToDataSourceResult(request, ModelState));

                    }

                }
                catch (Exception e)
                {
                    ModelState.AddModelError("Exception", "Sorry an Error Occured");
                    return Json(new[] { label }.ToDataSourceResult(request, ModelState));
                }
            }
        }
        [SystemMonitoring]
        public ActionResult LabelCasesGrid_Update([DataSourceRequest] DataSourceRequest request, LabelCases label)
        {

            using (var clientx = new HttpClient())
            {
                clientx.BaseAddress = new Uri(WebConfigurationManager.AppSettings["ApiServer"].ToString());
                clientx.DefaultRequestHeaders.Accept.Clear();
                clientx.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientx.DefaultRequestHeaders.Authorization =
              new AuthenticationHeaderValue("Bearer", Session["Access"] as string);
                var jsonString = JsonConvert.SerializeObject(label);
                try
                {

                    HttpResponseMessage Res = clientx.PostAsync("api/Fraud/LabelCasesGrid_Update", new StringContent(jsonString.ToString(), Encoding.UTF8, "application/json")).Result;

                    if (Res.IsSuccessStatusCode)
                    {
                        var result = Res.Content.ReadAsStringAsync().Result;
                        var s = Newtonsoft.Json.JsonConvert.DeserializeObject(result);
                        LabelCases Data = JsonConvert.DeserializeObject<LabelCases>(s.ToString());
                        if (Data.Id > 0)
                            return Json(new[] { false }.ToDataSourceResult(request, ModelState));
                        else
                        {
                            ModelState.AddModelError("Exist", "Duplicated Data");
                            return Json(new[] { label }.ToDataSourceResult(request, ModelState));
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Error", "Sorry an Error Occured");
                        return Json(new[] { label }.ToDataSourceResult(request, ModelState));

                    }

                }
                catch (Exception e)
                {
                    ModelState.AddModelError("Exception", "Sorry an Error Occured");
                    return Json(new[] { label }.ToDataSourceResult(request, ModelState));
                }
            }

        }
        [SystemMonitoring]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult LabelCasesGrid_Destroy([DataSourceRequest] DataSourceRequest request, LabelCases label)
        {
            using (var clientx = new HttpClient())
            {
                clientx.BaseAddress = new Uri(WebConfigurationManager.AppSettings["ApiServer"].ToString());
                clientx.DefaultRequestHeaders.Accept.Clear();
                clientx.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientx.DefaultRequestHeaders.Authorization =
              new AuthenticationHeaderValue("Bearer", Session["Access"] as string);
                var jsonString = JsonConvert.SerializeObject(label);
                try
                {

                    HttpResponseMessage Res = clientx.PostAsync("api/Fraud/LabelCasesGrid_Destroy", new StringContent(jsonString.ToString(), Encoding.UTF8, "application/json")).Result;

                    if (Res.IsSuccessStatusCode)
                    {
                        var result = Res.Content.ReadAsStringAsync().Result;
                        var s = Newtonsoft.Json.JsonConvert.DeserializeObject(result);
                        LabelCases Data = JsonConvert.DeserializeObject<LabelCases>(s.ToString());
                        if (Data.Id > 0)
                            return Json(new[] { false }.ToDataSourceResult(request, ModelState));
                        else
                        {
                            ModelState.AddModelError("Exist", "Duplicated Data");
                            return Json(new[] { label }.ToDataSourceResult(request, ModelState));
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Error", "Sorry an Error Occured");
                        return Json(new[] { label }.ToDataSourceResult(request, ModelState));

                    }

                }
                catch (Exception e)
                {
                    ModelState.AddModelError("Exception", "Sorry an Error Occured");
                    return Json(new[] { label }.ToDataSourceResult(request, ModelState));
                }
            }
        }

        [SystemMonitoring]
        public ActionResult SaveChangeFraudData(ChangeFraudData entity)
        {


            using (var clientx = new HttpClient())
            {
                clientx.BaseAddress = new Uri(WebConfigurationManager.AppSettings["ApiServer"].ToString());
                clientx.DefaultRequestHeaders.Accept.Clear();
                clientx.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientx.DefaultRequestHeaders.Authorization =
              new AuthenticationHeaderValue("Bearer", Session["Access"] as string);
                var jsonString = JsonConvert.SerializeObject(entity);
                try
                {

                    HttpResponseMessage Res = clientx.PostAsync("api/Fraud/SaveChangeFraudData", new StringContent(jsonString.ToString(), Encoding.UTF8, "application/json")).Result;

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
                        return Json(new { Result = false, Message = "Sorry,Check Api NetWork" });
                }
                catch (Exception e)
                {
                    return Json(new { Result = false, Message = "Sorry,Error Occured" });
                }
            }

        }
        public ActionResult GetFraudDataIds(int FraudId)
        {
            using (var clientx = new HttpClient())
            {
               
                clientx.BaseAddress = new Uri(WebConfigurationManager.AppSettings["ApiServer"].ToString());

                clientx.DefaultRequestHeaders.Accept.Clear();
                clientx.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientx.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Session["Access"] as string);
                var response = clientx.GetAsync(string.Format("api/Fraud/GetFraudDataIds?FraudId={0}", FraudId)).Result;

                var Result = response.Content.ReadAsStringAsync().Result;
                ChangeFraudData Data = JsonConvert.DeserializeObject<ChangeFraudData>(Result);
                return Json(new { UserPrivId = Data.AssignedPrivId ?? 0, StatusId = Data.StatusId ?? 0, LabelCaseId = Data.LabelCaseId ?? 0 });

            }
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

        [SystemMonitoring]
        public ActionResult SaveDescriptionSettings(FraudDescriptionContent DescriptionContent)
        {


            using (var clientx = new HttpClient())
            {
                clientx.BaseAddress = new Uri(WebConfigurationManager.AppSettings["ApiServer"].ToString());
                clientx.DefaultRequestHeaders.Accept.Clear();
                clientx.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientx.DefaultRequestHeaders.Authorization =
              new AuthenticationHeaderValue("Bearer", Session["Access"] as string);
                var jsonString = JsonConvert.SerializeObject(DescriptionContent);
                try
                {

                    HttpResponseMessage Res = clientx.PostAsync("api/Fraud/SaveDescriptionSettings", new StringContent(jsonString.ToString(), Encoding.UTF8, "application/json")).Result;

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
                        return Json(new { Result = false, Message = "Sorry,Check Api NetWork" });
                }
                catch (Exception e)
                {
                    return Json(new { Result = false, Message = "Sorry,Error Occured" });
                }
            }


        }

        [SystemMonitoring]
        public ActionResult SaveEmailSettings(FraudEmailSettings EmailSetting)
        {


            using (var clientx = new HttpClient())
            {
                clientx.BaseAddress = new Uri(WebConfigurationManager.AppSettings["ApiServer"].ToString());
                clientx.DefaultRequestHeaders.Accept.Clear();
                clientx.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientx.DefaultRequestHeaders.Authorization =
              new AuthenticationHeaderValue("Bearer", Session["Access"] as string);
                var jsonString = JsonConvert.SerializeObject(EmailSetting);
                try
                {

                    HttpResponseMessage Res = clientx.PostAsync("api/Fraud/SaveEmailSettings", new StringContent(jsonString.ToString(), Encoding.UTF8, "application/json")).Result;

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
                        return Json(new { Result = false, Message = "Sorry,Check Api NetWork" });
                }
                catch (Exception e)
                {
                    return Json(new { Result = false, Message = "Sorry,Error Occured" });
                }
            }


        }
        public ActionResult GetDescriptionSetting()
        {
            using (var clientx = new HttpClient())
            {
                clientx.BaseAddress = new Uri(WebConfigurationManager.AppSettings["ApiServer"].ToString());
                FraudDescriptionContent data = new FraudDescriptionContent();
                clientx.DefaultRequestHeaders.Accept.Clear();
                clientx.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientx.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Session["Access"] as string);
                var response = clientx.GetAsync(string.Format("api/Fraud/GetDescriptionSetting")).Result;

                var Result = response.Content.ReadAsStringAsync().Result;
                data = JsonConvert.DeserializeObject<FraudDescriptionContent>(Result);
                return Json(data);

            }

        }
            public ActionResult GetEmailSetting()
        {
            using (var clientx = new HttpClient())
            {
                clientx.BaseAddress = new Uri(WebConfigurationManager.AppSettings["ApiServer"].ToString());
                FraudEmailSettings data = new FraudEmailSettings();
                clientx.DefaultRequestHeaders.Accept.Clear();
                clientx.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientx.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Session["Access"] as string);
                var response = clientx.GetAsync(string.Format("api/Fraud/GetEmailSetting")).Result;

                var Result = response.Content.ReadAsStringAsync().Result;
                data = JsonConvert.DeserializeObject<FraudEmailSettings>(Result);
                return Json(data);

            }

        }

        public ActionResult GetCasesStatusCount()
        {

            using (var clientx = new HttpClient())
            {
                ChangeFraudData data = new ChangeFraudData();
                clientx.BaseAddress = new Uri(WebConfigurationManager.AppSettings["ApiServer"].ToString());

                clientx.DefaultRequestHeaders.Accept.Clear();
                clientx.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientx.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Session["Access"] as string);
                var response = clientx.GetAsync(string.Format("api/Fraud/GetCasesStatusCount")).Result;

                var Result = response.Content.ReadAsStringAsync().Result;
                List<Fraud_VM_CaseStatus> classificationsChart = JsonConvert.DeserializeObject<List<Fraud_VM_CaseStatus>>(Result);
                return Json(classificationsChart);

            }
        }
        public ActionResult GetLbelCasesSCount()
        {

            using (var clientx = new HttpClient())
            {
                ChangeFraudData data = new ChangeFraudData();
                clientx.BaseAddress = new Uri(WebConfigurationManager.AppSettings["ApiServer"].ToString());

                clientx.DefaultRequestHeaders.Accept.Clear();
                clientx.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientx.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Session["Access"] as string);
                var response = clientx.GetAsync(string.Format("api/Fraud/GetLbelCasesSCount")).Result;

                var Result = response.Content.ReadAsStringAsync().Result;
                List<Fraud_VM_LbelCases> classificationsChart = JsonConvert.DeserializeObject<List<Fraud_VM_LbelCases>>(Result);
                return Json(classificationsChart);

            }
        }
        public ActionResult SystemMonitoringGrid_Read([DataSourceRequest] DataSourceRequest request, DateTime StartDate, DateTime EndDate, string Text)
        {

            using (var clientx = new HttpClient())
            {
                clientx.BaseAddress = new Uri(WebConfigurationManager.AppSettings["ApiServer"].ToString());

                clientx.DefaultRequestHeaders.Accept.Clear();
                clientx.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientx.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Session["Access"] as string);
                var response = clientx.GetAsync(string.Format("api/Fraud/SystemMonitoringGrid_Read?StartDate={0}&EndDate={1}&Text={2}", StartDate, EndDate, Text)).Result;

                var Result = response.Content.ReadAsStringAsync().Result;
                List<MonitoringLog> Data = JsonConvert.DeserializeObject<List<MonitoringLog>>(Result);
                var jsonResult = Json(Data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
        }
        public ActionResult GetClassificationChartPercent()
        {
            using (var clientx = new HttpClient())
            {
                ChangeFraudData data = new ChangeFraudData();
                clientx.BaseAddress = new Uri(WebConfigurationManager.AppSettings["ApiServer"].ToString());

                clientx.DefaultRequestHeaders.Accept.Clear();
                clientx.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientx.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Session["Access"] as string);
                var response = clientx.GetAsync(string.Format("api/Fraud/GetClassificationChartPercent")).Result;

                var Data = response.Content.ReadAsStringAsync().Result;
                List<VM_ChartPercent> Result = JsonConvert.DeserializeObject<List<VM_ChartPercent>>(Data);
                return Json(new { Result }, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult GetStatusChartPercent()
        {
            using (var clientx = new HttpClient())
            {
                ChangeFraudData data = new ChangeFraudData();
                clientx.BaseAddress = new Uri(WebConfigurationManager.AppSettings["ApiServer"].ToString());

                clientx.DefaultRequestHeaders.Accept.Clear();
                clientx.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientx.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Session["Access"] as string);
                var response = clientx.GetAsync(string.Format("api/Fraud/GetStatusChartPercent")).Result;

                var Data = response.Content.ReadAsStringAsync().Result;
                List<VM_ChartPercent> Results = JsonConvert.DeserializeObject<List<VM_ChartPercent>>(Data);

                return Json(new { Results }, JsonRequestBehavior.AllowGet);

            }
        }
        public ActionResult AdminUsersGrid_Read([DataSourceRequest] DataSourceRequest request)
        {
            using (var clientx = new HttpClient())
            {
                clientx.BaseAddress = new Uri(WebConfigurationManager.AppSettings["ApiServer"].ToString());

                clientx.DefaultRequestHeaders.Accept.Clear();
                clientx.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientx.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Session["Access"] as string);
                var response = clientx.GetAsync(string.Format("api/Fraud/AdminUsersGrid_Read")).Result;

                var Result = response.Content.ReadAsStringAsync().Result;
                var Data = JsonConvert.DeserializeObject<List<VM_adUsRead>>(Result);
                var jsonResult = Json(Data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }

        }
        public ActionResult AdminCasesReply_Read([DataSourceRequest] DataSourceRequest request, string Text)
        {

            using (var clientx = new HttpClient())
            {
                clientx.BaseAddress = new Uri(WebConfigurationManager.AppSettings["ApiServer"].ToString());

                clientx.DefaultRequestHeaders.Accept.Clear();
                clientx.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientx.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Session["Access"] as string);
                var response = clientx.GetAsync(string.Format("api/Fraud/AdminCasesReply_Read?Text={0}", Text)).Result;

                var Result = response.Content.ReadAsStringAsync().Result;
                List<FraudCases> Data = JsonConvert.DeserializeObject<List<FraudCases>>(Result);
                var jsonResult = Json(Data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
        }

        public ActionResult PrivilegeGrid_Read([DataSourceRequest] DataSourceRequest request)
        {
            var Privilege = new List<Privilege>();
            using (var clientx = new HttpClient())
            {
                clientx.BaseAddress = new Uri(WebConfigurationManager.AppSettings["ApiServer"].ToString());

                clientx.DefaultRequestHeaders.Accept.Clear();
                clientx.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientx.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Session["Access"] as string);
                var response = clientx.GetAsync(string.Format("api/Fraud/GetListOfPrivilege")).Result;

                var Result = response.Content.ReadAsStringAsync().Result;
                Privilege = JsonConvert.DeserializeObject<List<Privilege>>(Result);
                
            }

            var jsonResult = Json(Privilege.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;


        }

        public ActionResult ReadAdminPrivilegeData(int Id)
        {

            using (var clientx = new HttpClient())
            {
                clientx.BaseAddress = new Uri(WebConfigurationManager.AppSettings["ApiServer"].ToString());

                clientx.DefaultRequestHeaders.Accept.Clear();
                clientx.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientx.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Session["Access"] as string);
                var response = clientx.GetAsync(string.Format("api/Fraud/GetRolesByAdminId?Id={0}", Id)).Result;

                var Result = response.Content.ReadAsStringAsync().Result;
                HashSet<int> RoleIds = JsonConvert.DeserializeObject<HashSet<int>>(Result);

                return Json(new { RoleIds, JsonRequestBehavior.AllowGet });
            }
        }

        [HttpPost]
        public ActionResult SaveAdminPrivilege(int Id, List<int> RolesList)
        {


            using (var clientx = new HttpClient())
            {
                clientx.BaseAddress = new Uri(WebConfigurationManager.AppSettings["ApiServer"].ToString());

                clientx.DefaultRequestHeaders.Accept.Clear();
                clientx.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientx.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Session["Access"] as string);
                
                var jsonString = JsonConvert.SerializeObject(new VM_PrivilegeAdmin { Id=Id,RolesList=RolesList});

                try
                {

                    var Res = clientx.PostAsync("api/Fraud/SaveAdminPrivilege", new StringContent(jsonString.ToString(), Encoding.UTF8, "application/json")).Result;

                    if (Res.IsSuccessStatusCode)
                    {
                        var result = Res.Content.ReadAsStringAsync().Result;
                        var s = JsonConvert.DeserializeObject(result);
                        GeneralErrors Data = JsonConvert.DeserializeObject<GeneralErrors>(s.ToString());
                        if (Data.Result)
                            return Json(new { Result = true });
                        else
                            return Json(new { Result = false, Message = Data.ErrorMessage });
                    }
                    else
                        return Json(new { Result = false, Message = "Sorry,Check Api NetWork" });
                }
                catch (Exception e)
                {
                    return Json(new { Result = false, Message = "Sorry,Error Occured" });
                }
            }
        }

        public ActionResult ReadFormConfiguration([DataSourceRequest] DataSourceRequest request)
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
                var jsonResult = Json(Data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
        }
        [SystemMonitoring]
        public ActionResult UpdateFormConfiguration([DataSourceRequest] DataSourceRequest request, FormConfiguration entity)
        {

            using (var clientx = new HttpClient())
            {
                clientx.BaseAddress = new Uri(WebConfigurationManager.AppSettings["ApiServer"].ToString());
                clientx.DefaultRequestHeaders.Accept.Clear();
                clientx.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientx.DefaultRequestHeaders.Authorization =
              new AuthenticationHeaderValue("Bearer", Session["Access"] as string);
                var jsonString = JsonConvert.SerializeObject(entity);
                try
                {

                    HttpResponseMessage Res = clientx.PostAsync("api/Fraud/UpdateFormConfiguration", new StringContent(jsonString.ToString(), Encoding.UTF8, "application/json")).Result;

                    if (Res.IsSuccessStatusCode)
                    {
                        var result = Res.Content.ReadAsStringAsync().Result;
                        var s = Newtonsoft.Json.JsonConvert.DeserializeObject(result);
                        GeneralErrors Data = JsonConvert.DeserializeObject<GeneralErrors>(s.ToString());
                        if (Data.Result )
                            return Json(new[] { false }.ToDataSourceResult(request, ModelState));

                        ModelState.AddModelError("Error", Data.ErrorMessage);
                        return Json(new[] { entity }.ToDataSourceResult(request, ModelState));
                    }
                    else
                    {
                        ModelState.AddModelError("Error", "Sorry an Error Occured");
                        return Json(new[] { entity }.ToDataSourceResult(request, ModelState));

                    }

                }
                catch (Exception e)
                {
                    ModelState.AddModelError("Exception", "Sorry an Error Occured");
                    return Json(new[] { entity }.ToDataSourceResult(request, ModelState));
                }
            }

        }

        [HttpPost]
        public ActionResult ReadAttachments()
        {
            using (var clientx = new HttpClient())
            {
                clientx.BaseAddress = new Uri(WebConfigurationManager.AppSettings["ApiServer"].ToString());

                clientx.DefaultRequestHeaders.Accept.Clear();
                clientx.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientx.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Session["Access"] as string);
                var response = clientx.GetAsync(string.Format("api/Fraud/ReadAttachments")).Result;

                var Result = response.Content.ReadAsStringAsync().Result;
                var Data = JsonConvert.DeserializeObject<List<ReportingPolicy_Attachment>>(Result);
                var jsonResult = Json(Data, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }
        }
        [HttpPost]
        public ActionResult SaveReportingPolicyAttachment(ReportingPolicy_Attachment attachments)
        {
            try
            {

                var Attachments = new List<ReportingPolicy_Attachment>();

                if (Request.Files.Count > 0)
                {
                    var BaseDomain = AppDomain.CurrentDomain.BaseDirectory;
                    var path = @"/Uploads/ReportingPolicy/" + (DateTime.Now.ToString("dd_MM_yyy_HH_mm_ss_ms"));
                    var CompletelyPath = BaseDomain + path;

                    var FileExists = Directory.Exists(CompletelyPath);
                    var host = "";
                    if (string.IsNullOrEmpty(host))
                    {
                        if (string.IsNullOrEmpty(host))
                        {
                            var uri = HttpContext.Request.Url;
                            host = uri.GetLeftPart(UriPartial.Authority);
                        }
                    }

                    if (!FileExists)
                        Directory.CreateDirectory(CompletelyPath);


                    for (var i = 0; i < Request.Files.Count; i++)
                    {
                        var ext = Path.GetExtension(Request.Files[i].FileName);
                        string[] extensions = { ".jpg", ".zip", ".gif", ".png", ".rar", ".doc", ".docx", ".xls", ".xlsx", ".pdf", ".bmp", ".jpeg" };
                        if (extensions.Contains(ext.ToLower()))
                        {
                            var Type = int.Parse(Request.Files.GetKey(i));
                            var FileName = Path.GetFileName(Request.Files[i].FileName);
                            

                            var Attachment = new ReportingPolicy_Attachment
                            {

                                Name = FileName.Replace(" ", ""),
                                URL = host + path + "/" + FileName.Replace(" ", "")
                            };
                            Request.Files[i].SaveAs(Server.MapPath(path + "/" + FileName.Replace(" ", "")));
                            Attachments.Add(Attachment);
                          
                           
                        }
                    }

                }
                using (var clientx = new HttpClient())
                {
                    clientx.BaseAddress = new Uri(WebConfigurationManager.AppSettings["ApiServer"].ToString());

                    clientx.DefaultRequestHeaders.Accept.Clear();
                    clientx.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    clientx.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", Session["Access"] as string);

                    var jsonString = JsonConvert.SerializeObject(Attachments);

                    try
                    {

                        var Res = clientx.PostAsync("api/Fraud/SaveReportingPolicyAttachment", new StringContent(jsonString.ToString(), Encoding.UTF8, "application/json")).Result;

                        if (Res.IsSuccessStatusCode)
                        {
                            var result = Res.Content.ReadAsStringAsync().Result;
                            var s = JsonConvert.DeserializeObject(result);
                            GeneralErrors Data = JsonConvert.DeserializeObject<GeneralErrors>(s.ToString());
                            if (Data.Result)
                                return Json(new { Result = true });
                            else
                                return Json(new { Result = false, Message = Data.ErrorMessage });
                        }
                        else
                            return Json(new { Result = false, Message = "Sorry,Check Api NetWork" });
                    }
                    catch (Exception e)
                    {
                        return Json(new { Result = false, Message = "Sorry,Error Occured" });
                    }
                }
                



            }

            catch (Exception e)
            {
                return Json(new { Result = false, ErrorMessage = "Saved Failed" });
            }
        }

        [HttpPost]
        public ActionResult ResetReportingPolicyAttachment()
        {
            using (var clientx = new HttpClient())
            {
                clientx.BaseAddress = new Uri(WebConfigurationManager.AppSettings["ApiServer"].ToString());

                clientx.DefaultRequestHeaders.Accept.Clear();
                clientx.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientx.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Session["Access"] as string);

                var jsonString = JsonConvert.SerializeObject("");

                try
                {

                    var Res = clientx.PostAsync("api/Fraud/ResetReportingPolicyAttachment", new StringContent(jsonString.ToString(), Encoding.UTF8, "application/json")).Result;

                    if (Res.IsSuccessStatusCode)
                    {
                        var result = Res.Content.ReadAsStringAsync().Result;
                        var s = JsonConvert.DeserializeObject(result);
                        GeneralErrors Data = JsonConvert.DeserializeObject<GeneralErrors>(s.ToString());
                        if (Data.Result)
                            return Json(new { Result = true });
                        else
                            return Json(new { Result = false, Message = Data.ErrorMessage });
                    }
                    else
                        return Json(new { Result = false, Message = "Sorry,Check Api NetWork" });
                }
                catch (Exception e)
                {
                    return Json(new { Result = false, Message = "Sorry,Error Occured" });
                }
            }
           
        }

        [HttpPost]
        public ActionResult SaveLogo()
        {
            try
            {

                var Logo = new Logo();
                if (Request.Files.Count > 0)
                {
                    var BaseDomain = AppDomain.CurrentDomain.BaseDirectory;
                    var path = @"/Uploads/Logo/" + (DateTime.Now.ToString("dd_MM_yyy_HH_mm_ss_ms"));
                    var CompletelyPath = BaseDomain + path;

                    var FileExists = Directory.Exists(CompletelyPath);
                    var host = "";
                    if (string.IsNullOrEmpty(host))
                    {
                        if (string.IsNullOrEmpty(host))
                        {
                            var uri = HttpContext.Request.Url;
                            host = uri.GetLeftPart(UriPartial.Authority);
                        }
                    }

                    if (!FileExists)
                        Directory.CreateDirectory(CompletelyPath);



                    var ext = Path.GetExtension(Request.Files[0].FileName);
                    string[] extensions = { ".jpg", ".gif", ".png", ".jpeg" };
                    if (extensions.Contains(ext.ToLower()))
                    {
                        var FileName = Path.GetFileName(Request.Files[0].FileName);

                        Logo.Name = FileName.Replace(" ", "");
                        Logo.URL = host + path + "/" + FileName.Replace(" ", "");
                       
                        Request.Files[0].SaveAs(Server.MapPath(path + "/" + FileName.Replace(" ", "")));

                    }
                    else { 
                        return Json(new { Result = false, Message ="Check Path" });

                    }


                }
                using (var clientx = new HttpClient())
                {
                    clientx.BaseAddress = new Uri(WebConfigurationManager.AppSettings["ApiServer"].ToString());

                    clientx.DefaultRequestHeaders.Accept.Clear();
                    clientx.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    clientx.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", Session["Access"] as string);

                    var jsonString = JsonConvert.SerializeObject(Logo);

                    try
                    {

                        var Res = clientx.PostAsync("api/Fraud/SaveLogo", new StringContent(jsonString.ToString(), Encoding.UTF8, "application/json")).Result;

                        if (Res.IsSuccessStatusCode)
                        {
                            var result = Res.Content.ReadAsStringAsync().Result;
                            var s = JsonConvert.DeserializeObject(result);
                            var Data = JsonConvert.DeserializeObject<GeneralErrors>(s.ToString());
                            if (Data.Result)
                                return Json(new { Result = true });
                            else
                                return Json(new { Result = false, Message = Data.ErrorMessage });
                        }
                        else
                            return Json(new { Result = false, Message = "Sorry,Check Api NetWork" });
                    }
                    catch (Exception e)
                    {
                        return Json(new { Result = false, Message = "Sorry,Error Occured" });
                    }
                }




            }

            catch (Exception e)
            {
                return Json(new { Result = false, ErrorMessage = "Saved Failed" });
            }
        }

        [HttpPost]
        public ActionResult DeleteLogo()
        {
            using (var clientx = new HttpClient())
            {
                clientx.BaseAddress = new Uri(WebConfigurationManager.AppSettings["ApiServer"].ToString());

                clientx.DefaultRequestHeaders.Accept.Clear();
                clientx.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientx.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Session["Access"] as string);

                var jsonString = JsonConvert.SerializeObject("");

                try
                {

                    var Res = clientx.PostAsync("api/Fraud/DeleteLogo", new StringContent(jsonString.ToString(), Encoding.UTF8, "application/json")).Result;

                    if (Res.IsSuccessStatusCode)
                    {
                        var result = Res.Content.ReadAsStringAsync().Result;
                        var s = JsonConvert.DeserializeObject(result);
                        var Data = JsonConvert.DeserializeObject<GeneralErrors>(s.ToString());
                        if (Data.Result)
                            return Json(new { Result = true });
                        else
                            return Json(new { Result = false, Message = Data.ErrorMessage });
                    }
                    else
                        return Json(new { Result = false, Message = "Sorry,Check Api NetWork" });
                }
                catch (Exception e)
                {
                    return Json(new { Result = false, Message = "Sorry,Error Occured" });
                }
            }

        }

        [HttpPost]
        public ActionResult AssignAdminRole(int Id)
        {
           


            using (var clientx = new HttpClient())
            {
                clientx.BaseAddress = new Uri(WebConfigurationManager.AppSettings["ApiServer"].ToString());

                clientx.DefaultRequestHeaders.Accept.Clear();
                clientx.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientx.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Session["Access"] as string);

                var jsonString = JsonConvert.SerializeObject(new AdminRole { AdminId=Id});

                try
                {

                    var Res = clientx.PostAsync("api/Fraud/AssignAdminRole", new StringContent(jsonString.ToString(), Encoding.UTF8, "application/json")).Result;

                    if (Res.IsSuccessStatusCode)
                    {
                        var result = Res.Content.ReadAsStringAsync().Result;
                        var s = JsonConvert.DeserializeObject(result);
                        var Data = JsonConvert.DeserializeObject<GeneralErrors>(s.ToString());
                        if (Data.Result)
                            return Json(new { Result = true, Message = Data.ErrorMessage });
                        else
                            return Json(new { Result = false, Message = Data.ErrorMessage });
                    }
                    else
                        return Json(new { Result = false, Message = "Sorry,Check Api NetWork" });
                }
                catch (Exception e)
                {
                    return Json(new { Result = false, Message = "Sorry,Error Occured" });
                }
            }
        }

        [HttpPost]
        public ActionResult UnAssignAdminRole(int Id)
        {
           


            using (var clientx = new HttpClient())
            {
                clientx.BaseAddress = new Uri(WebConfigurationManager.AppSettings["ApiServer"].ToString());

                clientx.DefaultRequestHeaders.Accept.Clear();
                clientx.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientx.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Session["Access"] as string);

                var jsonString = JsonConvert.SerializeObject(new AdminRole { Id = Id });

                try
                {

                    var Res = clientx.PostAsync("api/Fraud/UnAssignAdminRole", new StringContent(jsonString.ToString(), Encoding.UTF8, "application/json")).Result;

                    if (Res.IsSuccessStatusCode)
                    {
                        var result = Res.Content.ReadAsStringAsync().Result;
                        var s = JsonConvert.DeserializeObject(result);
                        var Data = JsonConvert.DeserializeObject<GeneralErrors>(s.ToString());
                        if (Data.Result)
                            return Json(new { Result = true, Message = Data.ErrorMessage });
                        else
                            return Json(new { Result = false, Message = Data.ErrorMessage });
                    }
                    else
                        return Json(new { Result = false, Message = "Sorry,Check Api NetWork" });
                }
                catch (Exception e)
                {
                    return Json(new { Result = false, Message = "Sorry,Error Occured" });
                }
            }

        }

        public ActionResult AdminsGrid_Read([DataSourceRequest] DataSourceRequest request)
        {

            using (var clientx = new HttpClient())
            {
                clientx.BaseAddress = new Uri(WebConfigurationManager.AppSettings["ApiServer"].ToString());

                clientx.DefaultRequestHeaders.Accept.Clear();
                clientx.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientx.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Session["Access"] as string);
                var response = clientx.GetAsync(string.Format("api/Fraud/AdminsGrid_Read")).Result;

                var Result = response.Content.ReadAsStringAsync().Result;
                var Data = JsonConvert.DeserializeObject<List<VM_adUsRead>>(Result);
                var jsonResult = Json(Data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;
            }

        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CCEmailGrid_Create([DataSourceRequest] DataSourceRequest request, CCEmail entity)
        {

            using (var clientx = new HttpClient())
            {
                clientx.BaseAddress = new Uri(WebConfigurationManager.AppSettings["ApiServer"].ToString());
                clientx.DefaultRequestHeaders.Accept.Clear();
                clientx.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientx.DefaultRequestHeaders.Authorization =
              new AuthenticationHeaderValue("Bearer", Session["Access"] as string);
                var jsonString = JsonConvert.SerializeObject(entity);
                try
                {

                    HttpResponseMessage Res = clientx.PostAsync("api/Fraud/CCEmailGrid_Create", new StringContent(jsonString.ToString(), Encoding.UTF8, "application/json")).Result;

                    if (Res.IsSuccessStatusCode)
                    {
                        var result = Res.Content.ReadAsStringAsync().Result;
                        var s = Newtonsoft.Json.JsonConvert.DeserializeObject(result);
                        var Data = JsonConvert.DeserializeObject<CCEmail>(s.ToString());
                        if (Data.Id > 0)
                            return Json(new[] { false }.ToDataSourceResult(request, ModelState));
                        else
                        {
                            ModelState.AddModelError("Exist", "Duplicated Data");
                            return Json(new[] { entity }.ToDataSourceResult(request, ModelState));
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Error", "Sorry an Error Occured");
                        return Json(new[] { entity }.ToDataSourceResult(request, ModelState));

                    }

                }
                catch (Exception e)
                {
                    ModelState.AddModelError("Exception", "Sorry an Error Occured");
                    return Json(new[] { entity }.ToDataSourceResult(request, ModelState));
                }
            }

        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CCEmailGrid_Read([DataSourceRequest] DataSourceRequest request)
        {
            using (var clientx = new HttpClient())
            {
                clientx.BaseAddress = new Uri(WebConfigurationManager.AppSettings["ApiServer"].ToString());

                clientx.DefaultRequestHeaders.Accept.Clear();
                clientx.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientx.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Session["Access"] as string);
                var response = clientx.GetAsync(string.Format("api/Fraud/CCEmailGrid_Read")).Result;

                var Result = response.Content.ReadAsStringAsync().Result;
                var Data = JsonConvert.DeserializeObject<List<CCEmail>>(Result);
                return Json(Data.ToDataSourceResult(request));

            }
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CCEmailGrid_Destroy([DataSourceRequest] DataSourceRequest request, CCEmail entity)
        {

            using (var clientx = new HttpClient())
            {
                clientx.BaseAddress = new Uri(WebConfigurationManager.AppSettings["ApiServer"].ToString());
                clientx.DefaultRequestHeaders.Accept.Clear();
                clientx.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientx.DefaultRequestHeaders.Authorization =
              new AuthenticationHeaderValue("Bearer", Session["Access"] as string);
                var jsonString = JsonConvert.SerializeObject(entity);
                try
                {

                    HttpResponseMessage Res = clientx.PostAsync("api/Fraud/CCEmailGrid_Destroy", new StringContent(jsonString.ToString(), Encoding.UTF8, "application/json")).Result;

                    if (Res.IsSuccessStatusCode)
                    {
                        var result = Res.Content.ReadAsStringAsync().Result;
                        var s = Newtonsoft.Json.JsonConvert.DeserializeObject(result);
                        var Data = JsonConvert.DeserializeObject<CCEmail>(s.ToString());
                        if (Data.Id > 0)
                            return Json(new[] { false }.ToDataSourceResult(request, ModelState));
                        else
                        {
                            ModelState.AddModelError("Exist", "Duplicated Data");
                            return Json(new[] { entity }.ToDataSourceResult(request, ModelState));
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Error", "Sorry an Error Occured");
                        return Json(new[] { entity }.ToDataSourceResult(request, ModelState));

                    }

                }
                catch (Exception e)
                {
                    ModelState.AddModelError("Exception", "Sorry an Error Occured");
                    return Json(new[] { entity }.ToDataSourceResult(request, ModelState));
                }
            }
        }

        public ActionResult CCEmailGrid_Update([DataSourceRequest] DataSourceRequest request, CCEmail entity)
        {
            using (var clientx = new HttpClient())
            {
                clientx.BaseAddress = new Uri(WebConfigurationManager.AppSettings["ApiServer"].ToString());
                clientx.DefaultRequestHeaders.Accept.Clear();
                clientx.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientx.DefaultRequestHeaders.Authorization =
              new AuthenticationHeaderValue("Bearer", Session["Access"] as string);
                var jsonString = JsonConvert.SerializeObject(entity);
                try
                {

                    HttpResponseMessage Res = clientx.PostAsync("api/Fraud/CCEmailGrid_Update", new StringContent(jsonString.ToString(), Encoding.UTF8, "application/json")).Result;

                    if (Res.IsSuccessStatusCode)
                    {
                        var result = Res.Content.ReadAsStringAsync().Result;
                        var s = Newtonsoft.Json.JsonConvert.DeserializeObject(result);
                        var Data = JsonConvert.DeserializeObject<CCEmail>(s.ToString());
                        if (Data.Id > 0)
                            return Json(new[] { false }.ToDataSourceResult(request, ModelState));
                        else
                        {
                            ModelState.AddModelError("Exist", "Duplicated Data");
                            return Json(new[] { entity }.ToDataSourceResult(request, ModelState));
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Error", "Sorry an Error Occured");
                        return Json(new[] { entity }.ToDataSourceResult(request, ModelState));

                    }

                }
                catch (Exception e)
                {
                    ModelState.AddModelError("Exception", "Sorry an Error Occured");
                    return Json(new[] { entity }.ToDataSourceResult(request, ModelState));
                }
            }

        }



    }
}