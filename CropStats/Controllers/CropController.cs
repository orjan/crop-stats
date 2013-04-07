using System.Web.Mvc;

namespace CropStats.Controllers
{
    public class CropController : Controller
    {
        //
        // GET: /Crop/

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Overview()
        {
            return View();
        }

    }
}
