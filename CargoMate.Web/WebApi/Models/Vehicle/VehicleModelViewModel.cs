using System.Collections.Generic;

namespace CargoMateSolution.WebApi.Models.Vehicle
{
    public class VehicleModelViewModel
    {
        public List<VehicleModel> Items { get; set; }
    }

    public class VehicleModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public List<YearModel> Years { get; set; } 

    }
}