using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using CargoMate.DataAccess.DBContext;
using CargoMateSolution.Shared;
using CargoMateSolution.WebApi.Models;
using CargoMateSolution.WebApi.Models.Vehicle;
using PayLoadType = CargoMateSolution.WebApi.Models.Vehicle.PayLoadType;


namespace CargoMateSolution.WebApi.Controllers
{
    public class VehicleEndpointController : BaseController
    {
        [System.Web.Http.HttpGet]
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


        [System.Web.Http.HttpGet]
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

        [System.Web.Http.HttpGet]

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

        [System.Web.Http.HttpGet]
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


        [System.Web.Http.HttpGet]
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

    

        [System.Web.Http.HttpGet]
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

        [System.Web.Http.HttpPost]
        public HttpResponseMessage AddVehicle([FromBody] VehicleViewModel vehicleModel)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.Ambiguous,ModelState); 
            }
            DbContext.Vehicles.Add(new Vehicle
            {
                ImageBack = ImageUploader.SaveImageFromBase64(vehicleModel.ImageBack),
                ImageFront = ImageUploader.SaveImageFromBase64(vehicleModel.ImageFront),
                ImageLeft = ImageUploader.SaveImageFromBase64(vehicleModel.ImageLeft),
                ImageRight = ImageUploader.SaveImageFromBase64(vehicleModel.ImageRight),
                RegistrationImage = ImageUploader.SaveImageFromBase64(vehicleModel.RegistrationImage),

                CapacityId = vehicleModel.CapacityId,
                ConfigurationId = vehicleModel.ConfigurationId,
                EngineNumber = vehicleModel.EngineNumber,
                IsActive = vehicleModel.IsActive,
                IsInsured = vehicleModel.IsInsured,
                IsVerified = vehicleModel.IsVerified,
                PlateNumber = vehicleModel.PlateNumber,
                RegistrationExpiry = vehicleModel.RegistrationExpiry,
                RegistrationNumber = vehicleModel.RegistrationNumber,
                Status = vehicleModel.Status,
                YearId = vehicleModel.YearId,
                TripTypes = vehicleModel.TripTypes,
                CountryId = vehicleModel.CountryId,
                VehicleDriverCombinations = new List<VehicleDriverCombination>
                {
                    new VehicleDriverCombination
                    {
                        DriverId = vehicleModel.DriverId
                    }
                }

            });

            
            return Request.CreateResponse(HttpStatusCode.OK, DbContext.SaveChanges());
        }

        public List<KeyValuePairViewModel> InsuranceCompaniesList(string cultureCode = "en-US", int limit = 10)
        {

            return DbContext.InsuranceCompanies.Select(ic => new KeyValuePairViewModel
            {
                Id = ic.Id,
                Name = ic.LocalizedInsuranceCompanies.FirstOrDefault(lic=>lic.CultureCode==cultureCode).Name

            }).ToList();
        }

        public List<KeyValuePairViewModel> TripTypesList(string cultureCode = "en-US", int limit = 10)
        {

            return DbContext.TripTypes.Select(ic => new KeyValuePairViewModel
            {
                Id = ic.Id,
                Name = ic.LocalizedTripTypes.FirstOrDefault(lic => lic.CultureCode == cultureCode).Name

            }).ToList();
        }



    }
}
