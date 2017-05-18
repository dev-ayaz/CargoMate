namespace CargoMate.DataAccess.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Localization.LocalizedVehicleTypes")]
    public partial class LocalizedVehicleType
    {
        public int Id { get; set; }

        [StringLength(500)]
        public string Name { get; set; }

        public string Descreption { get; set; }

        [StringLength(10)]
        public string CultureCode { get; set; }

        public long? VehicleTypeId { get; set; }

        public virtual VehicleType VehicleType { get; set; }
    }
}
