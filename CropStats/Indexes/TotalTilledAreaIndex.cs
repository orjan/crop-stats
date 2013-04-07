using System.Linq;
using CropStats.Models;
using Raven.Client.Indexes;

namespace CropStats.Indexes
{
    public class TotalTilledAreaIndex : AbstractIndexCreationTask<Crop, TotalTilledAreaIndex.Result>
    {
        public TotalTilledAreaIndex()
        {
            Map = harvesting => from harvest in harvesting
                                select new Result
                                           {
                                               Year = harvest.Year,
                                               FarmerId = harvest.FarmerId,
                                               Hectare = harvest.Hectare
                                           };

            Reduce = timeLogs => from log in timeLogs
                                 group log by new {log.FarmerId, log.Year}
                                 into g
                                 select new Result
                                            {
                                                FarmerId = g.Key.FarmerId,
                                                Year = g.Key.Year,
                                                Hectare = g.Sum(h => h.Hectare)
                                            };
        }

        public class Result
        {
            public int FarmerId { get; set; }
            public int Year { get; set; }

            public int Hectare { get; set; }
        }
    }

    public class AverageHarvestPerCropAndYearIndex : AbstractIndexCreationTask<Crop, AverageHarvestPerCropAndYearIndex.Result>
    {
        public AverageHarvestPerCropAndYearIndex()
        {
            Map = harvesting => from harvest in harvesting
                                select new Result
                                           {
                                               Year = harvest.Year,
                                               CropTypeId = harvest.CropTypeId,
                                               Estimated = harvest.Estimated,
                                               Hectare = harvest.Hectare,
                                               YieldPerHectare = harvest.YieldPerHectare,
                                               NumberOfFields = 1
                                           };

            Reduce = timeLogs => from log in timeLogs
                                 group log by new {log.CropTypeId, log.Year, log.Estimated}
                                 into g
                                 select new Result
                                            {
                                                CropTypeId = g.Key.CropTypeId,
                                                Year = g.Key.Year,
                                                Estimated = g.Key.Estimated,
                                                Hectare = g.Sum(h => h.Hectare),
                                                YieldPerHectare = g.Sum(h => h.YieldPerHectare),
                                                NumberOfFields = g.Sum(h => h.NumberOfFields)
                                            };
        }

        public class Result
        {
            public int CropTypeId { get; set; }
            public int Year { get; set; }
            public bool Estimated { get; set; }

            public int Hectare { get; set; }
            public int YieldPerHectare { get; set; }

            public int NumberOfFields { get; set; }
        }
    }
}