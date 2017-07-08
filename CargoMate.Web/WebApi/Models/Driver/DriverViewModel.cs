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


        [StringLength(50)]
        public string PostalCode { get; set; }

        public string AdministrativeAreaLevel1 { get; set; }

        public string AdministrativeAreaLevel2 { get; set; }

        public string Locality { get; set; }

        public string SubLocality { get; set; }

        public string Route { get; set; }

    }
}