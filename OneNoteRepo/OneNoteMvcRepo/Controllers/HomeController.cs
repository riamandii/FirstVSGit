using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Newtonsoft.Json;
using OneNoteMvcRepo.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OneNoteMvcRepo.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {


        private const string AuthorizeUri = "https://login.live.com/oauth20_authorize.srf";
        private const string TokenUri = "https://login.live.com/oauth20_token.srf";
        private const string DesktopUri = "https://login.live.com/oauth20_desktop.srf";
        private const string RedirectPath = "/oauth20_desktop.srf";
        private const string ConsentUriFormatter = "{0}?client_id={1}&scope=bingads.manage&response_type=code&redirect_uri={2}";
        private const string AccessUriFormatter = "{0}?client_id={1}&client_secret=FSjqWU-TDg4WvOTzYpnbW7hwEGsGrBZV&code={2}&grant_type=authorization_code&redirect_uri={3}";
        private const string RefreshUriFormatter = "{0}?client_id={1}&grant_type=refresh_token&redirect_uri={2}&refresh_token={3}";

        public ActionResult Index()
        {


            return View();
        }

        

       

        public void CreateJsonNotebook(string newNotebookName)
        {
            
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        private async Task<TokenResponseModel> GetAccessToken(IPrincipal user)
        {
            var claims = ((ClaimsIdentity)user.Identity);

            var x = claims.FindFirst(ClaimTypes.NameIdentifier).Value;

            var uri = string.Format(AccessUriFormatter, TokenUri, "000000004C11330A", x, "http%3A%2F%2Frazvaniamandii.localtest.me%3A8080%2F");
            var realUri = new Uri(uri, UriKind.Absolute);

            var addy = realUri.AbsoluteUri.Substring(0, realUri.AbsoluteUri.Length - realUri.Query.Length);
            var request = (HttpWebRequest)WebRequest.Create(addy);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";

            using (var writer = new StreamWriter(request.GetRequestStream()))
            {
                writer.Write(realUri.Query.Substring(1));
            }

            HttpWebResponse httpResponse = (HttpWebResponse)(await request.GetResponseAsync());
            string json;
            using (Stream responseStream = httpResponse.GetResponseStream())
            {
                json = new StreamReader(responseStream).ReadToEnd();
            }
            TokenResponseModel tokenResponse = JsonConvert.DeserializeObject<TokenResponseModel>(json);
            return tokenResponse;
        }
    }

    
}