using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CargoMate.DataAccess.DBContext;
using CargoMateSolution.Shared;
using CargoMateSolution.WebApi.Models;
using CargoMateSolution.WebApi.Models.Customers;

namespace CargoMateSolution.WebApi.Controllers
{
    public class CustomerEndPointController : BaseController
    {
        public HttpResponseMessage AddCompany(CompanyViewModel companyForm)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.Ambiguous, ModelState);
            }

            DbContext.GeoAddresses.Add(new GeoAddress
            {
                AdministrativeAreaLevel1 = companyForm.GeoAddress.AdministrativeAreaLevel1,

                AdministrativeAreaLevel2 = companyForm.GeoAddress.AdministrativeAreaLevel2,

                Country = companyForm.GeoAddress.Country,

                Locality = companyForm.GeoAddress.Locality,

                SubLocality = companyForm.GeoAddress.SubLocality,

                PostalCode = companyForm.GeoAddress.PostalCode,

                Route = companyForm.GeoAddress.Route,

                Companies = new List<Company>
                {
                    new Company
                    {
                        Name = companyForm.Name,
                        Logo = ImageUploader.SaveImageFromBase64(companyForm.Logo),
                        PhoneNumber = companyForm.PhoneNumber,
                        CrNumber = companyForm.CrNumber,
                        Location = companyForm.Location,
                        PoBox = companyForm.PoBox,
                        WebSiteUrl = companyForm.WebSiteUrl
                    }
                }

            });


            return Request.CreateResponse(HttpStatusCode.OK, DbContext.SaveChanges());

        }

        public HttpResponseMessage AddCustomer(CustomerViewModel customerForm)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.Ambiguous, ModelState);
            }


            DbContext.Customers.Add(new Customer
            {
                Name = customerForm.Name,
                CompanyId = customerForm.CompanyId,
                DateOfBirth = customerForm.DateOfBirth,
                EmailAddress = customerForm.EmailAddress,
                Gender = customerForm.Gender,
                IsCompany = customerForm.CompanyId > 0,
                PhoneNumber = customerForm.PhoneNumber,
                ImageUrl = ImageUploader.SaveImageFromBase64(customerForm.ImageUrl)

            });

            return Request.CreateResponse(HttpStatusCode.OK, DbContext.SaveChanges());

        }

        public CustomerDisplayViewModel GetCustomerByPhoneNumber(string phoneNumber)
        {
            return DbContext.Customers.Include("Company").Include("Company.GeoAddress").Where(c => c.PhoneNumber == phoneNumber).Select(c => new CustomerDisplayViewModel
            {
                Customer = new CustomerViewModel
                {
                    CompanyId = c.CompanyId,
                    CustomerId = c.CustomerId,
                    DateOfBirth = c.DateOfBirth.Value,
                    EmailAddress = c.EmailAddress,
                    Gender = c.Gender.Value,
                    ImageUrl = c.ImageUrl,
                    Name = c.Name,
                    PhoneNumber = c.PhoneNumber
                },
                Company = new CompanyViewModel
                {
                    CrNumber = c.Company.CrNumber.Value,
                    Id = c.Company.Id,
                    Location = c.Company.Location,
                    Logo = c.Company.Logo,
                    Name = c.Company.Name,
                    PhoneNumber = c.Company.PhoneNumber,
                    PoBox = c.Company.PoBox,
                    WebSiteUrl = c.Company.WebSiteUrl,
                    GeoAddress = new GeoAddressViewModel
                    {
                        Id = c.Company.GeoAddress.Id,
                        AdministrativeAreaLevel1 = c.Company.GeoAddress.AdministrativeAreaLevel1,
                        AdministrativeAreaLevel2 = c.Company.GeoAddress.AdministrativeAreaLevel2,
                        Country = c.Company.GeoAddress.Country,
                        Locality = c.Company.GeoAddress.Locality,
                        PostalCode = c.Company.GeoAddress.PostalCode,
                        Route = c.Company.GeoAddress.Route,
                        SubLocality = c.Company.GeoAddress.SubLocality
                    }
                }

            }).FirstOrDefault();
        }
    }
}
