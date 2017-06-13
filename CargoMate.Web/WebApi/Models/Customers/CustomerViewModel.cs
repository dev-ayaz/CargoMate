using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CargoMateSolution.WebApi.Models.Customers
{
    public class CustomerViewModel
    {

        public string Name { get; set; }

        public string CustomerId { get; set; }

        public string EmailAddress { get; set; }

        public string PhoneNumber { get; set; }

        public string ImageUrl { get; set; }

        public DateTime DateOfBirth { get; set; }

        public bool Gender { get; set; }

        public long? CompanyId { get; set; }

    }
}