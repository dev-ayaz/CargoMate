namespace CargoMate.DataAccess.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Localization.LocalizedWeight")]
    public partial class LocalizedWeight
    {
        public long Id { get; set; }

        [StringLength(100)]
        public string ShortName { get; set; }

        [StringLength(500)]
        public string FullName { get; set; }

        public long? WeightId { get; set; }

        [StringLength(10)]
        public string CultureCode { get; set; }

        public virtual Weight Weight { get; set; }
    }
}
