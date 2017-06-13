using System.Collections.Generic;

namespace CargoMateSolution.WebApi.Models.Vehicle
{
    public class VehicleCapacityViewModel
    {
        public List<CapacityViewModel> Items { get; set; } 
    }

    public class CapacityViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public int Capacity { get; set; }

        public int Length { get; set; }

        public int PalletNumber { get; set; }

    }
}