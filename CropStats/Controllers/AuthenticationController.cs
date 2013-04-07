using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CropStats.Models;
using CropStats.Security;

namespace CropStats.Controllers
{
    public class AuthenticationController : DocumentController
    {
        PasswordBuilder passwordBuilder;

        public AuthenticationController()
        {
            passwordBuilder = new PasswordBuilder();
        }

        [HttpPost]
        public ActionResult Login(AuthenticationLogin login)
        {
            var farmer = DocumentSession.Query<Farmer>().SingleOrDefault(c=>c.CustomerNumber == login.CustomerNumber);
            if (farmer == null)
            {
                throw new Exception("User doesn't exists");
            }

            if (farmer.Password == null)
            {
                throw new Exception("It's not possible to login with this user");
            }

            var isValid = passwordBuilder.IsValidPassword(farmer.Password, login.ActivationCode);

            if (!isValid)
            {
                throw new Exception("Wrong password");
            }


            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                     2,
                     login.CustomerNumber.ToString(CultureInfo.InvariantCulture),
                     DateTime.Now,
                     DateTime.Now.AddMinutes(15),
                     false,
                     string.Empty);

            string encTicket = FormsAuthentication.Encrypt(authTicket);
            HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);

            Response.Cookies.Add(faCookie);

            return RedirectToAction("Index", "Home");

        }
    }

    

    public class AuthenticationLogin
    {
        public int CustomerNumber { get; set; }
        public string ActivationCode { get; set; }
    }
}

namespace CropStats.Security
{
    public interface IAuthenticationProvider
    {
        bool IsValidUser(int customerNumber, string activationCode);
    }
}

namespace CropStats.Models
{
    public class Farmer
    {
        public int Id { get; set; }
        public int CustomerNumber { get; set; }
        public Password Password { get; set; }
    } 
}