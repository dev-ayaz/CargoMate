namespace CargoMate.DataAccess.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Localization.LocalizedDriverStatuses")]
    public partial class LocalizedDriverStatus
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public long? DriverStatusId { get; set; }

        [StringLength(20)]
        public string CultureCode { get; set; }

        public virtual DriverStatus DriverStatus { get; set; }
    }
}
