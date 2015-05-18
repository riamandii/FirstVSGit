using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.MicrosoftAccount;
using Microsoft.Owin.Security;
using Owin;
using OneNoteMvcRepo.Models;
using Microsoft.Owin.Security.OAuth;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using Microsoft.Owin.Security;

namespace OneNoteMvcRepo
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context, user manager and signin manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            var msOptions = new MicrosoftAccountAuthenticationOptions
            {
                ClientId = "000000004C11330A",
                ClientSecret = "FSjqWU-TDg4WvOTzYpnbW7hwEGsGrBZV",
                Caption = "Test",
                SignInAsAuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                Provider = new MicrosoftAccountAuthenticationProvider
                {
                    OnAuthenticated = (context)=>
                    {
                        
                        HttpContext.Current.GetOwinContext()
                         .Response.Cookies.Append("OwinCookie", "SomeValue");
                        HttpContext.Current.Response.Cookies["ASPCookie"].Value = "SomeValue";
                        HttpContext.Current.GetOwinContext().Authentication.User.AddIdentity(context.Identity);
                        return null;
                    }
                }
            };

            msOptions.Scope.Add("wl.basic,office.onenote_create,wl.offline_access");

            app.UseMicrosoftAccountAuthentication(msOptions);

            //app.UseMicrosoftAccountAuthentication("000000004C11330A", "FSjqWU-TDg4WvOTzYpnbW7hwEGsGrBZV");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            //app.UseFacebookAuthentication(
            //   appId: "",
            //   appSecret: "");

            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = "",
            //    ClientSecret = ""
            //});
        }
    }

    class BearerOaut : IOAuthBearerAuthenticationProvider
    {
        public Task ApplyChallenge(OAuthChallengeContext context)
        {
            return null;
        }

        public Task RequestToken(OAuthRequestTokenContext context)
        {
            return null;
        }

        public Task ValidateIdentity(OAuthValidateIdentityContext context)
        {
            return null;
        }
    }
}