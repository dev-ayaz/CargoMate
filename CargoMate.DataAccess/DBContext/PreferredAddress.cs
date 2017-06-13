namespace CargoMate.DataAccess.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Master.PreferredAddress")]
    public partial class PreferredAddress
    {
        public long Id { get; set; }

        [StringLength(50)]
        public string PreferredCountry { get; set; }

        [StringLength(50)]
        public string PreferredLocality { get; set; }

        [StringLength(50)]
        public string PreferredSubLocality { get; set; }

        [StringLength(50)]
        public string Latitude { get; set; }

        [StringLength(50)]
        public string Longitude { get; set; }

        public long? DriverId { get; set; }
    }
}
