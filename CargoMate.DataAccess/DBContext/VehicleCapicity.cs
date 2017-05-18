namespace CargoMate.DataAccess.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Configuration.VehicleCapicities")]
    public partial class VehicleCapicity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public VehicleCapicity()
        {
            LocalizedCapicities = new HashSet<LocalizedCapicity>();
        }

        public long Id { get; set; }

        public int? Capicity { get; set; }

        public int? Length { get; set; }

        public int? PalletNumber { get; set; }

        [StringLength(10)]
        public string CultureCode { get; set; }

        public long? VehicleTypeId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LocalizedCapicity> LocalizedCapicities { get; set; }

        public virtual VehicleType VehicleType { get; set; }
    }
}
