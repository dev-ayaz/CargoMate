namespace CargoMate.DataAccess.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FrontEnd.Companies")]
    public partial class Company
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Company()
        {
            Customers = new HashSet<Customer>();
        }

        public long Id { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(200)]
        public string Location { get; set; }

        [StringLength(20)]
        public string PhoneNumber { get; set; }

        public long? CrNumber { get; set; }

        [StringLength(500)]
        public string PoBox { get; set; }

        public string Logo { get; set; }

        [StringLength(250)]
        public string WebSiteUrl { get; set; }

        public long? CountryId { get; set; }

        [StringLength(50)]
        public string PostalCode { get; set; }

        public string AdministrativeAreaLevel1 { get; set; }

        public string AdministrativeAreaLevel2 { get; set; }

        public string Locality { get; set; }

        public string SubLocality { get; set; }

        public string Route { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Customer> Customers { get; set; }
    }
}
