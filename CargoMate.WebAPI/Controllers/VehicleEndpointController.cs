using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using CargoMate.DataAccess.DBContext;
using CargoMate.WebAPI.Models.Vehicle;
using CargoMate.WebAPI.Shared;
using PayLoadType = CargoMate.WebAPI.Models.Vehicle.PayLoadType;


namespace CargoMate.WebAPI.Controllers
{
    public class VehicleEndpointController : BaseController
    {
        [HttpGet]
        public VehicleTypes VehicleTypes(string cultureCode = "en-US", int limit = 10)
        {
            var vehicleTypeList = DbContext.VehicleTypes.Include("LocalizedVehicleTypes,VehicleCapacities,VehicleCapacities.LocalizedCapacities,VehicleTypeConfigurations,VehicleTypeConfigurations.LocalizedVehicleTypesConfigurations").Select(t => new VehicleTypeViewModel
            {
                TypeId = t.Id,
                Name = t.LocalizedVehicleTypes.FirstOrDefault(lt => lt.CultureCode == cultureCode).Name,
                Description = t.LocalizedVehicleTypes.FirstOrDefault(lt => lt.CultureCode == cultureCode).Descreption,
                IsEquipment = t.IsEquipment,
                ImageUrl = WebConfigKeys.ImagesBasePath+t.ImageUrl,
                VehicleCapacities = t.VehicleCapacities.Select(c=>c!=null? new CapacityViewModel
                {
                    Id = c.Id,
                    Capacity = c.Capacity.Value,
                    Length = c.Length.Value,
                    PalletNumber = c.PalletNumber.Value,
                    Name = c.LocalizedCapacities.FirstOrDefault(lt => lt.CultureCode == cultureCode).Name,
                    
                }:null).ToList(),
                Configurations = t.VehicleTypeConfigurations.Select(c=>c!=null? new ConfigurationsViewModel
                {
                 Id   = c.Id,
                 ImageUrl = c.ImageUrl,
                 Description = c.LocalizedVehicleTypesConfigurations.FirstOrDefault(lt => lt.CultureCode == cultureCode).Descreption,
                 Name = c.LocalizedVehicleTypesConfigurations.FirstOrDefault(lt => lt.CultureCode == cultureCode).Name
                }:null).ToList()
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


        [HttpGet]
        public YearViewModel YearsList(string cultureCode = "en-US", int limit = 10)
        {
            var yearsList = DbContext.Years.Select(y => new YearModel
            {
                Id = y.Id,
                Name = y.YearName
            }).Take(limit).ToList();

            return new YearViewModel { Items = yearsList };
        }


        [HttpGet]
        public VehicleModelViewModel VehicleModels(string cultureCode = "en-US", int limit = 10)
        {
            var models = DbContext.Models.Include("LocalizedModels,ModelYearCombinations,ModelYearCombinations.Year").Select(m => new VehicleModel   
            {
                Id = m.Id,
                Name = m.LocalizedModels.FirstOrDefault(lm=>lm.CultureCode=="en-US").Name,
                ImageUrl = m.ImageURL,
                Years = m.ModelYearCombinations.Select(y=>new YearModel
                {
                    Id = y.Id,
                    Name = y.Year.YearName
                }).ToList()
            }).Take(limit).ToList();

            return new VehicleModelViewModel { Items = models };
        }

        [HttpGet]
        public CountriesViewModel Countries(string cultureCode = "en-US", int limit = 10)
        {
            var countries = DbContext.Countries.Include("LocalizedCountries").Select(c => new CountryModel
            {
                Id = c.Id,
                Name = c.LocalizedCountries.FirstOrDefault(lc => lc.CultureCode == "en-US").Name,
                CountryCode = c.LocalizedCountries.FirstOrDefault(lc => lc.CultureCode == "en-US").CountryCode,
                CurrencyCode = c.LocalizedCountries.FirstOrDefault(lc => lc.CultureCode == "en-US").CurrencyCode,
                CurrencyLong = c.LocalizedCountries.FirstOrDefault(lc => lc.CultureCode == "en-US").CurrencyLong,
                CurrencySymbol = c.CurrencySymbol,
                Flag = c.Flag,
                PhonCode = c.PhonCode
            }).Take(limit).ToList();

            return new CountriesViewModel { Items = countries };
        }

        [HttpGet]
        public VehicleMakeViewModel VehcileMakes(string cultureCode = "en-US", int limit = 10)
        {

            var makes = DbContext.Makes.Include("LocalizedMakes,Country,Country.LocalizedCountries,Models,Models.LocalizedModels,Models.ModelYearCombinations,Models.ModelYearCombinations.Year").Select(m => new VehicleMakeModel
            {
                Id = m.Id,
                ImageUrl = m.ImageUrl,
                Name = m.LocalizedMakes.FirstOrDefault(lm=>lm.CultureCode==cultureCode).Name,
                Country = new CountryModel
                {
                    Id = m.Country.Id,
                    Flag = m.Country.Flag,
                    PhonCode = m.Country.PhonCode,
                    CurrencySymbol = m.Country.CurrencySymbol,
                    Name = m.Country.LocalizedCountries.FirstOrDefault(lc => lc.CultureCode == "en-US").Name,
                    CurrencyCode = m.Country.LocalizedCountries.FirstOrDefault(lc => lc.CultureCode == "en-US").CurrencyCode,
                    CurrencyLong = m.Country.LocalizedCountries.FirstOrDefault(lc => lc.CultureCode == "en-US").CurrencyLong,
                },
                Models = m.Models.Select(model=>new VehicleModel
                {
                 Id   = model.Id,
                 ImageUrl = model.ImageURL,
                 Name = model.LocalizedModels.FirstOrDefault(lm => lm.CultureCode == "en-US").Name,
                 Years = model.ModelYearCombinations.Select(y=>new YearModel
                {
                    Id = y.Id,
                    Name = y.Year.YearName
                }).ToList()

                }).ToList()
            }).ToList();


            return new VehicleMakeViewModel { Items = makes};
        }

        [HttpPost]
        public HttpResponseMessage AddVehicle(VehicleViewModel vehicleModel)
        {
            DbContext.Vehicles.Add(new Vehicle
            {
                AgencyId = vehicleModel.AgencyId,
                AveragePricePerKiloMeter = vehicleModel.AveragePricePerKiloMeter,
                CapacityId = vehicleModel.CapacityId,
                ConfigurationId = vehicleModel.ConfigurationId,
                DriverId = vehicleModel.DriverId,
                EngineNumber = vehicleModel.EngineNumber,
                ImageBack = 
            });

            return Request.CreateResponse(HttpStatusCode.OK, "Success");
        } 


    }
}
