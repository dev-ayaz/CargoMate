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
        public VehicleTypes Get(string cultureCode = "en-US", int limit = 10)
        {
            var vehicleTypeList = DbContext.VehicleTypes.Include("LocalizedVehicleTypes").Select(t => new VehicleTypeViewModel
            {
                TypeId = t.Id,
                Name = t.LocalizedVehicleTypes.FirstOrDefault(lt => lt.CultureCode == cultureCode).Name,
                Descreption = t.LocalizedVehicleTypes.FirstOrDefault(lt => lt.CultureCode == cultureCode).Descreption,
                IsEquipment = t.IsEquipment,
                ImageUrl = WebConfigKeys.ImagesBasePath+t.ImageUrl.Substring(9,t.ImageUrl.Length-9)
            }).Take(limit).ToList();

            return new VehicleTypes { Items = vehicleTypeList };
        } 
    }
}
