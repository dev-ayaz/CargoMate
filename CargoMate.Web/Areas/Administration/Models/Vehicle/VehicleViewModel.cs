﻿using System.Collections.Generic;
using System.Web.Mvc;

namespace CargoMateSolution.Areas.Administration.Models.Vehicle
{
    public class VehicleViewModel
    {
        public List<VehicleCapacityListModel> VehicleCapcitiesList { get; set; }

        public VehicleCapacityViewModel CapicityViewModel { get; set; }

        public List<VehicleTypeViewModel> VehicleTypesList { get; set; }

        public List<VehicleTypeConfigurationListModel> ConfigurationsList { get; set; }
        public VehicleTypeConfigurationsViewModel ConfigurationViewModel { get; set; }
           
    }
}