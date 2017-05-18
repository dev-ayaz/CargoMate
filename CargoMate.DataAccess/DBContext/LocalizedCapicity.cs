namespace CargoMate.DataAccess.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Localization.LocalizedCapicities")]
    public partial class LocalizedCapicity
    {
        public int Id { get; set; }

        [StringLength(500)]
        public string Name { get; set; }

        [StringLength(10)]
        public string CultureCode { get; set; }

        public long? CapicityId { get; set; }

        public virtual VehicleCapicity VehicleCapicity { get; set; }
    }
}
