using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using CropStats.Models;
using Raven.Client.Linq;

namespace CropStats.Controllers
{
    public class CropTypeController : DocumentApiController
    {
        public IEnumerable<CropType> Get()
        {
            return DocumentSession.Query<CropType>().OrderBy(x=>x.Name).OrderBy(x=>x.SubType);
        }

        public CropType Post(CropType crop)
        {
            DocumentSession.Store(crop);

            return crop;
        }

        public HttpResponseMessage Delete(int id)
        {
            var crop = DocumentSession.Load<CropType>(id);
            DocumentSession.Delete(crop);

            return new HttpResponseMessage(HttpStatusCode.Accepted);
        }
    }

    public class CropsController : DocumentApiController
    {
        public IEnumerable<Crop> Get()
        {
            return DocumentSession.Query<Crop>().Where(c=> c.FarmerId == 133 && c.Year == 2012);
        }

        public Crop Post(Crop crop)
        {
            crop.FarmerId = 133;
            crop.Year = 2012;

            DocumentSession.Store(crop);

            return crop;
        }
        
        public HttpResponseMessage Delete(int id)
        {
            var crop = DocumentSession.Load<Crop>(id);
            DocumentSession.Delete(crop);

            return new HttpResponseMessage(HttpStatusCode.Accepted);
        }
    }


}
