using Ext_FraudingSystem.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace Ext_FraudingSystem.Filters
{
    public class SystemMonitoring : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session.Contents["Access"] as string != null && HttpContext.Current.Session.Contents["Access"] as string !="")
            { 
                // filterContext.HttpContext.Request.Cookies
                var ControllerName = filterContext.Controller.ControllerContext.RouteData.Values["Controller"].ToString();
                var ActionName = filterContext.Controller.ControllerContext.RouteData.Values["Action"].ToString();
                try
                {
                    // get Pc Data-----
                    string ip = HttpContext.Current.Request.UserHostName;
                    //IPAddress myIP = IPAddress.Parse(ip);
                    List<string> compName = new List<string>();
                    if (!(filterContext.HttpContext.Request.Browser.IsMobileDevice))
                    {
                        compName = new List<string> { "PC" };

                    }
                    else
                    {
                        compName = new List<string> { "Mobile" };
                    }

                    //End
                    //Get Mac Address
                    string macAddresses = string.Empty;
                    foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
                    {
                        // Only consider Ethernet network interfaces
                        if ((nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet || nic.NetworkInterfaceType == NetworkInterfaceType.Wireless80211) &&
                            nic.OperationalStatus == OperationalStatus.Up)
                        {
                            macAddresses = nic.GetPhysicalAddress().ToString();
                        }
                    }
                    // End
                    MonitoringLog MonitoringLog = new MonitoringLog();
                
                    MonitoringLog.IPAddress =ip;
                    MonitoringLog.ControllerName = ControllerName;
                    MonitoringLog.ComputerName =compName.First().ToUpper();
                    MonitoringLog.BrowserName =HttpContext.Current.Request.Browser.Browser;
                    MonitoringLog.ActionName = ActionName;
                    MonitoringLog.MacAddress =macAddresses;
                    using (var clientx = new HttpClient())
                    {
                        clientx.BaseAddress = new Uri(WebConfigurationManager.AppSettings["ApiServer"].ToString());
                        clientx.DefaultRequestHeaders.Accept.Clear();
                        clientx.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        clientx.DefaultRequestHeaders.Authorization =
                      new AuthenticationHeaderValue("Bearer",HttpContext.Current.Session.Contents["Access"] as string);
                        var jsonString = JsonConvert.SerializeObject(MonitoringLog);                  
                            HttpResponseMessage Res = clientx.PostAsync("api/Fraud/SaveMontoringSystem", new StringContent(jsonString.ToString(), Encoding.UTF8, "application/json")).Result;

                            if (Res.IsSuccessStatusCode)
                            {
                                var result = Res.Content.ReadAsStringAsync().Result;
                                var s = Newtonsoft.Json.JsonConvert.DeserializeObject(result);
                                GeneralErrors Data = JsonConvert.DeserializeObject<GeneralErrors>(s.ToString());
                               
                            }  
                      
                    }

                }
                catch (Exception ex)
                {
                    string Message = "\n Controller Name: " + ControllerName + "\n  Action Name: " + ActionName + "\n  InnerException: " + ex.InnerException.ToString() + "\n  Message: " + ex.Message.ToString();
                    string folderPath = filterContext.HttpContext.Server.MapPath("~/ErrorsLog/");
                    bool folderExists = Directory.Exists(folderPath);
                    if (!folderExists) Directory.CreateDirectory(folderPath);
                    string filePath = Path.Combine(folderPath, "log.txt");
                    if (!File.Exists(filePath))
                        File.WriteAllText(filePath, Message);
                    else
                        File.AppendAllText(filePath, Message);
                }
            }
          
            }
        }

    }