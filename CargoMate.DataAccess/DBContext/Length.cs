namespace CargoMate.DataAccess.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Configuration.Length")]
    public partial class Length
    {
        public long Id { get; set; }

        public bool? IsMetric { get; set; }

        public decimal? LengthMultiple { get; set; }
    }
}
