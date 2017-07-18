using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CargoMateSolution.Models.Company
{
    public class CompanyViewModel
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Please Enter Name")]
        public string Name { get; set; }

        public string Location { get; set; }

        [Required(ErrorMessage = "Please Enter Phone Number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Please Enter CR Number")]
        public long CrNumber { get; set; }

        public string Logo { get; set; }
        public string WebSiteUrl { get; set; }

        public string Address { get; set; }

        public string CountryName { get; set; }
    }
}