using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CargoMate.WebAPI.Models.Vehicle
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