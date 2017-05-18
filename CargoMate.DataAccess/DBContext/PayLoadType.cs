namespace CargoMate.DataAccess.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Configuration.PayLoadTypes")]
    public partial class PayLoadType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PayLoadType()
        {
            LocalizedPayLoadTypes = new HashSet<LocalizedPayLoadType>();
        }

        public long Id { get; set; }

        public long? TypeId { get; set; }

        public string ImageUrl { get; set; }

        public bool? IsActive { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LocalizedPayLoadType> LocalizedPayLoadTypes { get; set; }

        public virtual VehicleType VehicleType { get; set; }
    }
}
