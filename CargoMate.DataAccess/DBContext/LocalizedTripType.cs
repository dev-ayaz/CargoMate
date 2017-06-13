namespace CargoMate.DataAccess.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("[CodeDecode.Localizations].LocalizedTripTypes")]
    public partial class LocalizedTripType
    {
        public long Id { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        public long? TripTypeId { get; set; }

        [StringLength(20)]
        public string CultureCode { get; set; }

        public virtual TripType TripType { get; set; }
    }
}
