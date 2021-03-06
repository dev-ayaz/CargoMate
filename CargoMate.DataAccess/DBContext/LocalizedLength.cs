namespace CargoMate.DataAccess.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Localization.LocalizedLength")]
    public partial class LocalizedLength
    {
        public long Id { get; set; }

        [StringLength(100)]
        public string ShortName { get; set; }

        [StringLength(500)]
        public string FullName { get; set; }

        [StringLength(10)]
        public string CultureCode { get; set; }

        public long? LengthId { get; set; }

        public virtual Length Length { get; set; }
    }
}
