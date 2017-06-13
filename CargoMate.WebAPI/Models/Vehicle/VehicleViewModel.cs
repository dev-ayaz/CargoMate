using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CargoMate.WebAPI.Models.Vehicle
{
    public class VehicleViewModel
    {
            public long Id { get; set; }

            public string PlateNumber { get; set; }

            public string EngineNumber { get; set; }

            public string RegistrationNumber { get; set; }

            public string RegistrationImage { get; set; }

            public DateTime? RegistrationExpiry { get; set; }

            public string ImageBack { get; set; }

            public string ImageFront { get; set; }

            public string ImageLeft { get; set; }

            public string ImageRight { get; set; }

            public bool? IsInsured { get; set; }

            public decimal? InsuranceAmount { get; set; }

            public string InsuranceCompany { get; set; }

            public string PolicyNumber { get; set; }

            public DateTime? InsuranceExpiryDate { get; set; }

            public decimal? AveragePricePerKiloMeter { get; set; }

            public bool? IsEquipment { get; set; }

            public int? TripType { get; set; }

            public bool? IsAgency { get; set; }

            public long? AgencyId { get; set; }

            public long? CapacityId { get; set; }

            public long? ConfigurationId { get; set; }

            public long? MakeId { get; set; }

            public long? ModelId { get; set; }

            public long? TypeId { get; set; }

            public long? YearId { get; set; }

            public string PayLoadTypes { get; set; }

            public long? DriverId { get; set; }

            public bool? IsVerified { get; set; }

            public int? Status { get; set; }

            public bool? IsActive { get; set; }
        
    }
}