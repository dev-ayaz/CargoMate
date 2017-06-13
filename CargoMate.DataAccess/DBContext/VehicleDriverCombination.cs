namespace CargoMate.DataAccess.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Master.VehicleDriverCombinations")]
    public partial class VehicleDriverCombination
    {
        public long Id { get; set; }

        [StringLength(50)]
        public string DriverId { get; set; }

        public long? VehicleId { get; set; }

        public bool? IsCurrent { get; set; }

        public virtual Driver Driver { get; set; }

        public virtual Vehicle Vehicle { get; set; }
    }
}
