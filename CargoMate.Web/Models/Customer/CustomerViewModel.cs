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

        [Required(ErrorMessage = "Please Enter Name")]
        public string Name { get; set; }

        [StringLength(50)]
        public string CustomerId { get; set; }

        [Required(ErrorMessage = "Please Enter Email Address")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string EmailAddress { get; set; }

        [StringLength(20)]
        [Required(ErrorMessage = "Please Enter Phone Number")]
        [Display(Name = "Phone")]
        public string PhoneNumber { get; set; }

        public string ImageUrl { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public bool? Gender { get; set; }

        [Display(Name = "Company Name")]
        public long? CompanyId { get; set; }

        public string Address { get; set; }

        public string Location { get; set; }
    }

    public class RegisterVithEmailAndPassword
    {

        [Required(ErrorMessage = "Please Enter Email Address")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Please Enter Password")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please Enter Confirm Password")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
            [Compare("Password")]
        public string ConfirmPassword { get; set; }
    

    }
}