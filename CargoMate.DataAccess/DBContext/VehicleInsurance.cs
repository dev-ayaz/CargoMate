namespace CargoMate.DataAccess.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Vehicles.VehicleInsurance")]
    public partial class VehicleInsurance
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }

        public decimal? InsuranceAmount { get; set; }

        public long? PolicyNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime? InsuranceExpiryDate { get; set; }

        public long? InsuranceCompanyId { get; set; }

        public long? VehicleId { get; set; }

        public virtual InsuranceCompany InsuranceCompany { get; set; }

        public virtual Vehicle Vehicle { get; set; }
    }
}
