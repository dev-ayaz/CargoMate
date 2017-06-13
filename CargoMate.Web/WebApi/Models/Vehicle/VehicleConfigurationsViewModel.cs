using System.Collections.Generic;

namespace CargoMateSolution.WebApi.Models.Vehicle
{
    public class VehicleConfigurationsViewModel
    {
        public List<ConfigurationsViewModel> Items { get; set; } 
    }

    public class ConfigurationsViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}