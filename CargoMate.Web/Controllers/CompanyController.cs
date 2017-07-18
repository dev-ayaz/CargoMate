using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CargoMate.DataAccess.DBContext;
using CargoMateSolution.Areas.Administration.Controllers;
using CargoMateSolution.Models.Company;
using CargoMateSolution.Shared;

namespace CargoMateSolution.Controllers
{
    public class CompanyController : BaseController
    {
        // GET: Company
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Create( CompanyViewModel companyForm)
        {
             if (!ModelState.IsValid)
            {
                return Json(CargoMateMessages.ModelError);
            }
             var country = DbContext.LocalizedCountries.FirstOrDefault(c => c.Name.Contains(companyForm.CountryName));
            
            DbContext.Companies.Add(new Company
            {
                Address = companyForm.Address,
                CountryId = country.Country.Id ,
                CrNumber = companyForm.CrNumber,
                Location = companyForm.Location,
                Logo = ImageUploader.SaveImageFromBase64(companyForm.Logo),
                Name = companyForm.Name,
                PhoneNumber = companyForm.PhoneNumber,
                WebSiteUrl = companyForm.WebSiteUrl
                
            });
           
            return Json(DbContext.SaveChanges()>0? CargoMateMessages.SuccessResponse : CargoMateMessages.FailureResponse);
        }
    }
}