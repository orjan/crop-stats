using System.Collections.Generic;
using System.Web.Http;

namespace crop_stats.Controllers
{
    public class CropTypeController : ApiController
    {
        // GET api/api
        public IEnumerable<CropType> Get()
        {
            return new[]
                       {
                           new CropType() { Id = 1, Name = "Vete"},
                           new CropType() { Id = 2, Name = "Havre"},
                       };
        }
    }

    public class CropType
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
