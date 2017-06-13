using System.Collections.Generic;

namespace CargoMateSolution.WebApi.Models.Vehicle
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