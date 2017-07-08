﻿using System;
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

        public string Location { get; set; }


        public string PhoneNumber { get; set; }

        public long CrNumber { get; set; }

        public string PoBox { get; set; }

        public string Logo { get; set; }


        public string WebSiteUrl { get; set; }

        public long CountryId { get; set; }


        public string PostalCode { get; set; }

        public string AdministrativeAreaLevel1 { get; set; }

        public string AdministrativeAreaLevel2 { get; set; }

        public string Locality { get; set; }

        public string SubLocality { get; set; }

        public string Route { get; set; }

    }
}