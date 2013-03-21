using System.Web.Http;
using System.Web.Http.Controllers;
using Raven.Client;

namespace CropStats.Controllers
{
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