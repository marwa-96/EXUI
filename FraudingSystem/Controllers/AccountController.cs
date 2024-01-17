using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Ext_FraudingSystem.Models;
using Ext_FraudingSystem.Filters;
using System.Net.Http;
using System.Web.Configuration;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;

namespace Ext_FraudingSystem.Controllers
{
    public class AccountController : Controller
    {
        public AccountController()
            
        {
        }

        public AccountController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }

        public UserManager<ApplicationUser> UserManager { get; private set; }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                using (var clientx = new HttpClient())
                {
                    clientx.BaseAddress = new Uri(WebConfigurationManager.AppSettings["ApiServer"].ToString());
                    clientx.DefaultRequestHeaders.Accept.Clear();
                    clientx.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var jsonString = "username=" + model.UserName + "&Password=" + model.Password + "&grant_type=password&Type=0";
                    try
                    {
                        HttpResponseMessage Res = clientx.PostAsync("token", new StringContent(jsonString, Encoding.UTF8, "application/json")).Result;

                        if (Res.IsSuccessStatusCode)
                        {
                            var result = Res.Content.ReadAsStringAsync().Result;
                            VM_token Data = JsonConvert.DeserializeObject<VM_token>(result.ToString());
                            Session["Access"] = Data.access_token;
                        }
                        else
                        {
                            ModelState.AddModelError("", "Incorrect UserName Or Password.");
                            return View(model);
                        }
                        ////var ExistUser = await UserManager.FindAsync(model.UserName, model.Password);
                        //if (ExistUser != null)
                        //{
                        //    await SignInAsync(ExistUser, model.RememberMe ?? false);

                        //}
                        //else
                        //{
                        //    ModelState.AddModelError("", "Incorrect UserName Or Password.");
                        //    return View(model);

                        //}
                    }
                    catch (Exception e)
                    {

                    }
                    return Redirect("~/Us_Page/Index");
                }

                // If we got this far, something failed, redisplay form

            }
            return Redirect("~/Us_Page/Index");
        }
        [AllowAnonymous]
        public ActionResult AdminLogin(LoginViewModel model)
        {
            using (var clientx = new HttpClient())
            {


                //DateTime ValidDT = new DateTime(2020, 2, 1);
                //if (ValidDT < DateTime.Now)
                //{
                //    return Json(new { Result = false, Message = "Please Contact system administrator to reactivate License" });
                //}
                clientx.BaseAddress = new Uri(WebConfigurationManager.AppSettings["ApiServer"].ToString());
                clientx.DefaultRequestHeaders.Accept.Clear();
                clientx.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var jsonString = "username=" + model.UserName + "&Password=" + model.Password + "&grant_type=password&Type=1";
                try
                {
                    HttpResponseMessage Res = clientx.PostAsync("token", new StringContent(jsonString, Encoding.UTF8, "application/json")).Result;

                    if (Res.IsSuccessStatusCode)
                    {
                        var result = Res.Content.ReadAsStringAsync().Result;
                        VM_token Data = JsonConvert.DeserializeObject<VM_token>(result.ToString());
                        Session["Access"] = Data.access_token;
                    }
                    else
                    {
                        var result = Res.Content.ReadAsStringAsync().Result;
                        VM_entityError Data = JsonConvert.DeserializeObject<VM_entityError>(result.ToString());
                        var Error = new GeneralErrors { Result = false, ErrorMessage = Data.error_description };
                        return Json(new { Result = false,Message =Error.ErrorMessage });
                    }
                  
                }
                catch (Exception e)
                {

                }
                return Json(new { Result = true });
            }


        }
        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
      
        [AllowAnonymous]
        //public async Task<ActionResult> UserLogin(LoginViewModel model)
        //{
        //    //DateTime ValidDT = new DateTime(2020, 2, 1);
        //    //if (ValidDT < DateTime.Now)
        //    //{
        //    //    return Json(new { Result = false, Message = "Please Contact system administrator to reactivate License" });
        //    //}

        //}

        //
        // POST: /Account/Disassociate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Disassociate(string loginProvider, string providerKey)
        {
            ManageMessageId? message = null;
            IdentityResult result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("Manage", new { Message = message });
        }
        [SystemMonitoring]
        public ActionResult CustomChanePassword(ManageUserViewModel model)
        {
            //DateTime ValidDT = new DateTime(2020, 2, 1);
            //if (ValidDT < DateTime.Now)
            //{
            //    return Json(new { Result = false, Message = "Please Contact system administrator to reactivate License" });
            //}
            using (var clientx = new HttpClient())
            {
                clientx.BaseAddress = new Uri(WebConfigurationManager.AppSettings["ApiServer"].ToString());
                clientx.DefaultRequestHeaders.Accept.Clear();
                clientx.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientx.DefaultRequestHeaders.Authorization =
              new AuthenticationHeaderValue("Bearer", Session["Access"] as string);
                var jsonString = JsonConvert.SerializeObject(model);
                try
                {

                    HttpResponseMessage Res = clientx.PostAsync("api/Fraud/CustomChanePassword", new StringContent(jsonString.ToString(), Encoding.UTF8, "application/json")).Result;

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
           
   
            return View(model);
        }
        [SystemMonitoring]
        public ActionResult CreateAdminUser(AdminBindingModel model)
        {
           
            using (var clientx = new HttpClient())
            {
                clientx.BaseAddress = new Uri(WebConfigurationManager.AppSettings["ApiServer"].ToString());
                clientx.DefaultRequestHeaders.Accept.Clear();
                clientx.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientx.DefaultRequestHeaders.Authorization =
              new AuthenticationHeaderValue("Bearer", Session["Access"] as string);
                var jsonString = JsonConvert.SerializeObject(model);
                try
                {

                    HttpResponseMessage Res = clientx.PostAsync("api/Fraud/CreateAdminUser", new StringContent(jsonString.ToString(), Encoding.UTF8, "application/json")).Result;

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

        //
        // GET: /Account/Manage
        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            ViewBag.HasLocalPassword = HasPassword();
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }

        //
        // POST: /Account/Manage
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Manage(ManageUserViewModel model)
        {
            bool hasPassword = HasPassword();
            ViewBag.HasLocalPassword = hasPassword;
            ViewBag.ReturnUrl = Url.Action("Manage");
            if (hasPassword)
            {
                if (ModelState.IsValid)
                {
                    IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }
            else
            {
                // User does not have a password so remove any validation errors caused by a missing OldPassword field
                ModelState state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }

                if (ModelState.IsValid)
                {
                    IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var user = await UserManager.FindAsync(loginInfo.Login);
            if (user != null)
            {
                await SignInAsync(user, isPersistent: false);
                return RedirectToLocal(returnUrl);
            }
            else
            {
                // If the user does not have an account, then prompt the user to create an account
                ViewBag.ReturnUrl = returnUrl;
                ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { UserName = loginInfo.DefaultUserName });
            }
        }

        //
        // POST: /Account/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new ChallengeResult(provider, Url.Action("LinkLoginCallback", "Account"), User.Identity.GetUserId());
        }

        //
        // GET: /Account/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
            }
            var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
            if (result.Succeeded)
            {
                return RedirectToAction("Manage");
            }
            return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser() { UserName = model.UserName };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInAsync(user, isPersistent: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult RemoveAccountList()
        {
            var linkedAccounts = UserManager.GetLogins(User.Identity.GetUserId());
            ViewBag.ShowRemoveButton = HasPassword() || linkedAccounts.Count > 1;
            return (ActionResult)PartialView("_RemoveAccountPartial", linkedAccounts);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && UserManager != null)
            {
                UserManager.Dispose();
                UserManager = null;
            }
            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        private class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties() { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        public ActionResult SignOut()
        {

            Session.Clear();
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return Json(new { });
        }
        [SystemMonitoring]
        public ActionResult ResetAdminUser(ResetPassById model)
        {
           
            using (var clientx = new HttpClient())
            {
                clientx.BaseAddress = new Uri(WebConfigurationManager.AppSettings["ApiServer"].ToString());
                clientx.DefaultRequestHeaders.Accept.Clear();
                clientx.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientx.DefaultRequestHeaders.Authorization =
              new AuthenticationHeaderValue("Bearer", Session["Access"] as string);
                var jsonString = JsonConvert.SerializeObject(model);
                try
                {

                    HttpResponseMessage Res = clientx.PostAsync("api/Fraud/ResetAdminUser", new StringContent(jsonString.ToString(), Encoding.UTF8, "application/json")).Result;

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
        public ActionResult EditEmailUser(EditEmailUserById model)
        {

            using (var clientx = new HttpClient())
            {
                clientx.BaseAddress = new Uri(WebConfigurationManager.AppSettings["ApiServer"].ToString());
                clientx.DefaultRequestHeaders.Accept.Clear();
                clientx.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                clientx.DefaultRequestHeaders.Authorization =
              new AuthenticationHeaderValue("Bearer", Session["Access"] as string);
                var jsonString = JsonConvert.SerializeObject(model);
                try
                {

                    HttpResponseMessage Res = clientx.PostAsync("api/Fraud/EditEmailUser", new StringContent(jsonString.ToString(), Encoding.UTF8, "application/json")).Result;

                    if (Res.IsSuccessStatusCode)
                    {
                        var result = Res.Content.ReadAsStringAsync().Result;
                        var s = Newtonsoft.Json.JsonConvert.DeserializeObject(result);
                        GeneralErrors Data = JsonConvert.DeserializeObject<GeneralErrors>(s.ToString());
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
        #endregion
    }
}