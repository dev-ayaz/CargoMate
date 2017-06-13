namespace CargoMate.DataAccess.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("[Master.Localizations].LocalizedInsuranceCompanies")]
    public partial class LocalizedInsuranceCompany
    {
        public long Id { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Address { get; set; }

        [StringLength(20)]
        public string CultureCode { get; set; }

        public long? CompanyId { get; set; }

        public virtual InsuranceCompany InsuranceCompany { get; set; }
    }
}
