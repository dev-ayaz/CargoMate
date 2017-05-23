using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CargoMate.WebAPI.Models.Vehicle
{
    public class VehicleTypeViewModel
    {
        public long TypeId { get; set; }
        public string Name { get; set; }
        public string Descreption { get; set; }
        public string ImageUrl { get; set; }
        public bool? IsEquipment { get; set; }
    }
}