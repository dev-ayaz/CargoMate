namespace CargoMate.DataAccess.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Configuration.ModelYearCombinations")]
    public partial class ModelYearCombination
    {
        public long Id { get; set; }

        public long? ModelId { get; set; }

        public long? YearId { get; set; }

        [StringLength(500)]
        public string ImageUrl { get; set; }

        public virtual Model Model { get; set; }

        public virtual Year Year { get; set; }
    }
}
