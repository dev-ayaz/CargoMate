namespace CargoMate.DataAccess.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Localization.LocalizedCapicities")]
    public partial class LocalizedCapacity
    {
        public int Id { get; set; }

        [StringLength(500)]
        public string Name { get; set; }

        [StringLength(10)]
        public string CultureCode { get; set; }

        public long? CapacityId { get; set; }

        public virtual VehicleCapacity VehicleCapacity { get; set; }
    }
}
