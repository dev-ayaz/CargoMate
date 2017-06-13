using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using CargoMate.DataAccess.DBContext;
using CargoMateSolution.Shared;
using CargoMateSolution.WebApi.Models;
using CargoMateSolution.WebApi.Models.Driver;

namespace CargoMateSolution.WebApi.Controllers
{
    public class DriverEndpointController : BaseController
    {

        public List<KeyValuePairViewModel> GetDriverStatuses(string cultureCode = "en-US", int limit = 10)
        {

            return DbContext.DriverStatuses.Select(ds => new KeyValuePairViewModel
            {
                Id = ds.Id,
                Name = ds.LocalizedDriverStatuses.FirstOrDefault(lic => lic.CultureCode == cultureCode).Name

            }).ToList();

        }

        public HttpResponseMessage AddDriver([FromBody] DriverViewModel driverModel)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.Ambiguous, ModelState);
            }

            DbContext.GeoAddresses.Add(new GeoAddress
            {
                AdministrativeAreaLevel1 = driverModel.GeoAddress.AdministrativeAreaLevel1,

                AdministrativeAreaLevel2 = driverModel.GeoAddress.AdministrativeAreaLevel2,

                Country = driverModel.GeoAddress.Country,

                Locality = driverModel.GeoAddress.Locality,

                SubLocality = driverModel.GeoAddress.SubLocality,

                PostalCode = driverModel.GeoAddress.PostalCode,

                Route = driverModel.GeoAddress.Route,

                Drivers = new List<Driver>{
                    new Driver{
                Name = driverModel.Name,
                CountryId = driverModel.CountryId,
                DateOfBirth = driverModel.DateOfBirth,
                EmailAddress = driverModel.EmailAddress,
                FixedRate = driverModel.FixedRate,
                Gender = driverModel.Gender,
                ImageUrl = ImageUploader.SaveImageFromBase64(driverModel.ImageUrl),
                LegalName = driverModel.LegalName,
                LicenseNumber = driverModel.LicenseNumber,
                LicenseExpiryDate = driverModel.LicenseExpiryDate,
                LicenseImage = driverModel.LicenseImage,
                DriverId = driverModel.DriverId,
                MembershipDate = DateTime.Now.Date,
                PhoneNumber = driverModel.PhoneNumber,
                ResidenceNumber = driverModel.ResidenceNumber,
                ResidenceExpiryDate = driverModel.ResidenceExpiryDate,
                ResidenceImage = ImageUploader.SaveImageFromBase64(driverModel.ResidenceImage),
                Status = driverModel.Status }
               }
            });

            return Request.CreateResponse(HttpStatusCode.OK, DbContext.SaveChanges());
        }
    }
}
