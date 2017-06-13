using System.Collections.Generic;

namespace CargoMateSolution.WebApi.Models.Vehicle
{
    public class VehicleTypes
    {
        public List<VehicleTypeViewModel> Items { get; set; } 
    }
    public class VehicleTypeViewModel
    {
        public long TypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public bool? IsEquipment { get; set; }

        public List<CapacityViewModel> VehicleCapacities { get;set ;}

        public List<ConfigurationsViewModel> Configurations { get; set; } 
    }
}