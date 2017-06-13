using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CargoMate.DataAccess.DBContext;

namespace CargoMateSolution.WebApi.Models.Driver
{
    public class DriverViewModel
    {

        public string Name { get; set; }

        public string LegalName { get; set; }

        
        public DateTime? DateOfBirth { get; set; }


        public string PhoneNumber { get; set; }


        public string EmailAddress { get; set; }

        public string ImageUrl { get; set; }

        public long? CountryId { get; set; }

        public int? Gender { get; set; }

        public string LicenseNumber { get; set; }

        public DateTime? LicenseExpiryDate { get; set; }

        public string LicenseImage { get; set; }

        public string ResidenceNumber { get; set; }

        public DateTime? ResidenceExpiryDate { get; set; }

        public string ResidenceImage { get; set; }


        public string DriverId { get; set; }

        public bool FixedRate { get; set; }

        public long Status { get; set; }

        public GeoAddress GeoAddress { get; set; }

    }
}