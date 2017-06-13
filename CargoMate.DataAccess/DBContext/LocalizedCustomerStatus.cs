namespace CargoMate.DataAccess.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("[FrontEnd.Localizations].LocalizedCustomerStatuses")]
    public partial class LocalizedCustomerStatus
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public long? CustomerStatusId { get; set; }

        [StringLength(20)]
        public string CultureCode { get; set; }

        public virtual CustomerStatus CustomerStatus { get; set; }
    }
}
