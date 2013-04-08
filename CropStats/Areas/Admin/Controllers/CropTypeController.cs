using System.Web.Mvc;
using CropStats.Controllers;
using CropStats.Models;
using Raven.Client.Document;

namespace CropStats.Areas.Admin.Controllers
{
    public class CropTypeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}