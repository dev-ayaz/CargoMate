using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CargoMateSolution.WebApi.Models.Vehicle;

namespace CargoMateSolution.WebApi.Controllers
{
    public class SharedController : BaseController
    {
        [System.Web.Http.HttpGet]
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

        [System.Web.Http.HttpGet]
        public YearViewModel YearsList(string cultureCode = "en-US", int limit = 10)
        {
            var yearsList = DbContext.Years.Select(y => new YearModel
            {
                Id = y.Id,
                Name = y.YearName
            }).Take(limit).ToList();

            return new YearViewModel { Items = yearsList };
        }
    }
}
