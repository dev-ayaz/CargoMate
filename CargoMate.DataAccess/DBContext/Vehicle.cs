namespace CargoMate.DataAccess.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Vehicles.Vehicles")]
    public partial class Vehicle
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Vehicle()
        {
            VehicleDriverCombinations = new HashSet<VehicleDriverCombination>();
            VehicleInsurances = new HashSet<VehicleInsurance>();
        }

        public long Id { get; set; }

        [StringLength(50)]
        public string PlateNumber { get; set; }

        [StringLength(50)]
        public string EngineNumber { get; set; }

        [StringLength(50)]
        public string RegistrationNumber { get; set; }

        public string RegistrationImage { get; set; }

        public DateTime? RegistrationExpiry { get; set; }

        public string ImageBack { get; set; }

        public string ImageFront { get; set; }

        public string ImageLeft { get; set; }

        public string ImageRight { get; set; }

        public bool? IsInsured { get; set; }

        public long? CapacityId { get; set; }

        public long? ConfigurationId { get; set; }

        public long? YearId { get; set; }

        public long? DriverId { get; set; }

        public long? CountryId { get; set; }

        [StringLength(50)]
        public string TripTypes { get; set; }

        public bool? IsVerified { get; set; }

        public int? Status { get; set; }

        public bool? IsActive { get; set; }

        public virtual Country Country { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VehicleDriverCombination> VehicleDriverCombinations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VehicleInsurance> VehicleInsurances { get; set; }
    }
}
