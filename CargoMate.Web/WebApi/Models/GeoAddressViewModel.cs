using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CargoMateSolution.WebApi.Models
{
    public class GeoAddressViewModel
    {
        public long Id { get; set; }

        public string Country { get; set; }


        public string PostalCode { get; set; }

        public string AdministrativeAreaLevel1 { get; set; }

        public string AdministrativeAreaLevel2 { get; set; }

        public string Locality { get; set; }

        public string SubLocality { get; set; }

        public string Route { get; set; }
    }
}