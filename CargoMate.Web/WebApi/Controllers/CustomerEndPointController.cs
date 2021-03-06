﻿using System;
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

            DbContext.Companies.Add(new Company
            {

                Address = companyForm.Address,

                CountryId = companyForm.CountryId,

                Name = companyForm.Name,

                Logo = ImageUploader.SaveImageFromBase64(companyForm.Logo),

                PhoneNumber = companyForm.PhoneNumber,

                CrNumber = companyForm.CrNumber,

                Location = companyForm.Location,


                WebSiteUrl = companyForm.WebSiteUrl
                    
                

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
            return DbContext.Customers.Include("Company").Include("Company").Where(c => c.PhoneNumber == phoneNumber).Select(c => new CustomerDisplayViewModel
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
                    WebSiteUrl = c.Company.WebSiteUrl,
                    CountryId = c.Company.CountryId.Value,
                    Address = c.Company.Address
                    
                }

            }).FirstOrDefault();
        }
    }
}
