namespace CargoMate.DataAccess.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Configuration.VehicleTypeConfigurations")]
    public partial class VehicleTypeConfiguration
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public VehicleTypeConfiguration()
        {
            LocalizedVehicleTypesConfigurations = new HashSet<LocalizedVehicleTypesConfiguration>();
        }

        public long Id { get; set; }

        public string ImageUrl { get; set; }

        public long? VehicleTypeId { get; set; }

        [StringLength(50)]
        public string Source { get; set; }

        public bool? IsActive { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LocalizedVehicleTypesConfiguration> LocalizedVehicleTypesConfigurations { get; set; }

        public virtual VehicleType VehicleType { get; set; }
    }
}
