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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LocalizedLength> LocalizedLengths { get; set; }
    }
}
