namespace CargoMate.DataAccess.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Configuration.VehicleCapacities")]
    public partial class VehicleCapacity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public VehicleCapacity()
        {
            LocalizedCapacities = new HashSet<LocalizedCapacity>();
        }

        public long Id { get; set; }

        public int? Capacity { get; set; }

        public int? Length { get; set; }

        public int? PalletNumber { get; set; }

        [StringLength(10)]
        public string CultureCode { get; set; }

        public long? VehicleTypeId { get; set; }

        [StringLength(50)]
        public string Source { get; set; }

        public bool? ISActive { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LocalizedCapacity> LocalizedCapacities { get; set; }

        public virtual VehicleType VehicleType { get; set; }
    }
}
