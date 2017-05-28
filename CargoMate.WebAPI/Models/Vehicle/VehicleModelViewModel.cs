using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CargoMate.WebAPI.Models.Vehicle
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

    }
}