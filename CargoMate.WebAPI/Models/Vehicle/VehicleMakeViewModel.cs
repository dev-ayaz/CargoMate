using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace CargoMate.WebAPI.Models.Vehicle
{
    public class VehicleMakeViewModel
    {
        public List<VehicleMakeModel> Items { get; set; }
    }

    public class VehicleMakeModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public CountryModel Country { get; set; }

        public List<VehicleModel> Models { get; set; } 
        
    }
}