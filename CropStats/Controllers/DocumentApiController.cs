using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using Raven.Client;

namespace CropStats.Controllers
{
    public abstract class DocumentController : Controller
    {
        protected IDocumentSession DocumentSession { get; set; }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            DocumentSession = MvcApplication.DocumentStore.OpenSession();
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.IsChildAction)
                return;

            using (DocumentSession)
            {
                if (filterContext.Exception != null)
                    return;

                if (DocumentSession != null)
                    DocumentSession.SaveChanges();
            }
        }
    }

    public abstract class DocumentApiController : ApiController
    {
        public IDocumentSession DocumentSession { get; set; }

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            DocumentSession = MvcApplication.DocumentStore.OpenSession();
            base.Initialize(controllerContext);
        }

        protected override void Dispose(bool disposing)
        {
            using (DocumentSession)
            {
                DocumentSession.SaveChanges();
            }

            base.Dispose(disposing);
        }
    }
}