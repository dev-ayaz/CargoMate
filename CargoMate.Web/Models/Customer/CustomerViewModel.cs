using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CargoMateSolution.Models.Customer
{
    public class CustomerViewModel
    {
        public long Id { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(50)]
        public string CustomerId { get; set; }

        [StringLength(50)]
        public string EmailAddress { get; set; }

        [StringLength(20)]
        public string PhoneNumber { get; set; }

        public string ImageUrl { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public bool? Gender { get; set; }
    }
}