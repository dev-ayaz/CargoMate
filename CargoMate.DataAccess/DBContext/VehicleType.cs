namespace CargoMate.DataAccess.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Configuration.VehicleTypes")]
    public partial class VehicleType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public VehicleType()
        {
            PayLoadTypes = new HashSet<PayLoadType>();
            VehicleCapacities = new HashSet<VehicleCapacity>();
            VehicleTypeConfigurations = new HashSet<VehicleTypeConfiguration>();
            LocalizedVehicleTypes = new HashSet<LocalizedVehicleType>();
        }

        public long Id { get; set; }

        public string ImageUrl { get; set; }

        public bool? IsEquipment { get; set; }

        [StringLength(50)]
        public string Source { get; set; }

        public bool? IsActive { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PayLoadType> PayLoadTypes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VehicleCapacity> VehicleCapacities { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VehicleTypeConfiguration> VehicleTypeConfigurations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LocalizedVehicleType> LocalizedVehicleTypes { get; set; }
    }
}
