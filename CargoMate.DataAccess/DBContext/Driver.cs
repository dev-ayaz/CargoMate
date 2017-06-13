namespace CargoMate.DataAccess.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Master.Drivers")]
    public partial class Driver
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Driver()
        {
            VehicleDriverCombinations = new HashSet<VehicleDriverCombination>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(250)]
        public string LegalName { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateOfBirth { get; set; }

        [StringLength(20)]
        public string PhoneNumber { get; set; }

        public bool? IsPhoneNumberVerified { get; set; }

        [StringLength(50)]
        public string EmailAddress { get; set; }

        public string ImageUrl { get; set; }

        public long? CountryId { get; set; }

        public int? Gender { get; set; }

        [StringLength(50)]
        public string LicenseNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime? LicenseExpiryDate { get; set; }

        public string LicenseImage { get; set; }

        [StringLength(50)]
        public string ResidenceNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ResidenceExpiryDate { get; set; }

        public string ResidenceImage { get; set; }

        public decimal? Rating { get; set; }

        public long? TotalTrips { get; set; }

        [StringLength(50)]
        public string DriverId { get; set; }

        public bool? FixedRate { get; set; }

        public bool? IdVerified { get; set; }

        public DateTime? MembershipDate { get; set; }

        public bool? Validated { get; set; }

        public long? Status { get; set; }

        public long? GeoAddressId { get; set; }

        public virtual DriverStatus DriverStatus { get; set; }

        public virtual Country Country { get; set; }

        public virtual GeoAddress GeoAddress { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VehicleDriverCombination> VehicleDriverCombinations { get; set; }
    }
}
