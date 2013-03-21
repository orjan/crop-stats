using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Controllers;
using Raven.Client;

namespace crop_stats.Controllers
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

    public class CropTypeController : DocumentApiController
    {
        // GET api/api
        public IEnumerable<CropType> Get()
        {
            DocumentSession.Store(new CropType() { Name = "Vete" });
            DocumentSession.Store(new CropType() { Name = "Havre" });

            return DocumentSession.Query<CropType>();
        }
    }

    public class CropType
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
