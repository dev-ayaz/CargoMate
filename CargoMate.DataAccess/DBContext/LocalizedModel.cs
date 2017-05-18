namespace CargoMate.DataAccess.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Localization.LocalizedModels")]
    public partial class LocalizedModel
    {
        public long Id { get; set; }

        [StringLength(500)]
        public string Name { get; set; }

        public long? ModelId { get; set; }

        [StringLength(10)]
        public string CultureCode { get; set; }

        public virtual Model Model { get; set; }
    }
}
