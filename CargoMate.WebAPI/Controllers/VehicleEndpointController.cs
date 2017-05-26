using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Results;
using CargoMate.WebAPI.Models.Vehicle;
using CargoMate.WebAPI.Shared;


namespace CargoMate.WebAPI.Controllers
{
    public class VehicleEndpointController : BaseController
    {
        [HttpGet]
        public VehicleTypes VehicleTypes(string cultureCode = "en-US", int limit = 10)
        {
            var vehicleTypeList = DbContext.VehicleTypes.Include("LocalizedVehicleTypes").Select(t => new VehicleTypeViewModel
            {
                TypeId = t.Id,
                Name = t.LocalizedVehicleTypes.FirstOrDefault(lt => lt.CultureCode == cultureCode).Name,
                Description = t.LocalizedVehicleTypes.FirstOrDefault(lt => lt.CultureCode == cultureCode).Descreption,
                IsEquipment = t.IsEquipment,
                ImageUrl = WebConfigKeys.ImagesBasePath+t.ImageUrl
            }).Take(limit).ToList();

            return new VehicleTypes { Items = vehicleTypeList };
        }


        [HttpGet]
        public VehicleCapacityViewModel VehcilCapacities(string cultureCode = "en-US", int limit = 10)
        {
            var capacities = DbContext.VehicleCapacities.Include("LocalizedCapacities").Select(c => new CapacityViewModel
            {
                Id = c.Id,
                Capacity = c.Capacity.Value,
                Length = c.Length.Value,
                PalletNumber = c.PalletNumber.Value,
                Name = c.LocalizedCapacities.FirstOrDefault(lc=>lc.CultureCode==cultureCode).Name
            }).Take(limit).ToList();

            return new VehicleCapacityViewModel{Items = capacities};

        }

        [HttpGet]

        public VehicleConfigurationsViewModel VehicleConfigurations(string cultureCode = "en-US", int limit = 10)
        {
            var configurations = DbContext.VehicleTypeConfigurations.Include("LocalizedVehicleTypesConfigurations").Select(c => new ConfigurationsViewModel
            {
                Id = c.Id,
                ImageUrl = c.ImageUrl,
                Name = c.LocalizedVehicleTypesConfigurations.FirstOrDefault(lc=>lc.CultureCode==cultureCode).Name,
                Description = c.LocalizedVehicleTypesConfigurations.FirstOrDefault(lc=>lc.CultureCode==cultureCode).Descreption

            }).ToList();

            return new VehicleConfigurationsViewModel{Items = configurations};
        }

        [HttpGet]
        public PayLoadTypeViewModel PayLoadTypes(string cultureCode = "en-US", int limit = 10)
        {
            var payloadTypes = DbContext.PayLoadTypes.Include("LocalizedPayLoadTypes").Select(pt => new PayLoadType
            {
                Id = pt.Id,
                ImageUrl = pt.ImageUrl,
                Name = pt.LocalizedPayLoadTypes.FirstOrDefault(lpt=>lpt.CultureCode==cultureCode).Name
            }).Take(limit).ToList();

            return  new PayLoadTypeViewModel{Items = payloadTypes};
        }
    }
}
