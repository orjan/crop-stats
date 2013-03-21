using System.Collections.Generic;

namespace CropStats.Controllers
{
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
}
