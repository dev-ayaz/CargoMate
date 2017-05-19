namespace CargoMate.DataAccess.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Configuration.Weight")]
    public partial class Weight
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Weight()
        {
            LocalizedWeights = new HashSet<LocalizedWeight>();
        }

        public long Id { get; set; }

        public bool? IsMetric { get; set; }

        public decimal? WeightMultiple { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LocalizedWeight> LocalizedWeights { get; set; }
    }
}
