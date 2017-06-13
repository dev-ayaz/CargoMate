using System;
using System.ComponentModel.DataAnnotations;

namespace CargoMateSolution.WebApi.Models.Vehicle
{
    public class VehicleViewModel
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Please Enter PlateNumber")]
        public string PlateNumber { get; set; }

        [Required(ErrorMessage = "Please Enter EngineNumber")]

        public string EngineNumber { get; set; }

        public string RegistrationNumber { get; set; }

        public string RegistrationImage { get; set; }

        public DateTime? RegistrationExpiry { get; set; }

        public bool IsInsured { get; set; }
        public string ImageBack { get; set; }

        public string ImageFront { get; set; }

        public string ImageLeft { get; set; }

        public string ImageRight { get; set; }

        public string TripTypes { get; set; }


        public long? CapacityId { get; set; }

        public long? ConfigurationId { get; set; }


        public long? TypeId { get; set; }

        public long? YearId { get; set; }

        public long CountryId { get; set; }

        public string DriverId { get; set; }

        public bool? IsVerified { get; set; }

        public int? Status { get; set; }

        public bool? IsActive { get; set; }

    }
}