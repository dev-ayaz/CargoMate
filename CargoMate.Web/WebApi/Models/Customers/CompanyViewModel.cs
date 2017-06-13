using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using CargoMate.DataAccess.DBContext;

namespace CargoMateSolution.WebApi.Models.Customers
{
    public class CompanyViewModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public GeoAddress GeoAddress { get; set; }

 
        public string Location { get; set; }


        public string PhoneNumber { get; set; }

        public long CrNumber { get; set; }

        public string PoBox { get; set; }

        public string Logo { get; set; }


        public string WebSiteUrl { get; set; }

    }
}