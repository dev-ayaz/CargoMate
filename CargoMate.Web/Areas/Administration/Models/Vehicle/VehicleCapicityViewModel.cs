﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using CargoMateSolution.Shared;

namespace CargoMateSolution.Areas.Administration.Models.Vehicle
{
    public class VehicleCapacityViewModel
    {
        public VehicleCapacityViewModel()
        {
            VehicleTypesListItems = new List<SelectListItem>();
            Languges = GlobalProperties.Languges;
        }
        [Key]
        public long Id { get; set; }
        [Required(ErrorMessage = "Please enter name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter Capacity")]
        public int Capacity { get; set; }

        [Required(ErrorMessage = "Please enter length")]
        public int Length { get; set; }

        [Required(ErrorMessage = "Please enter Pallet number")]
        public int PalletNumber { get; set; }

        [Required(ErrorMessage = "Please select language")]
        public string CultureCode { get; set; }
        public string VehicleType { get; set; }

        [Required(ErrorMessage = "Please select vehicle type")]
        public long VehicleTypeId { get; set; }
        public List<SelectListItem> Languges { get; set; }
        public List<SelectListItem> VehicleTypesListItems { get; set; }
    }

    public class VehicleCapacityListModel
    {


        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public int Length { get; set; }
        public int PalletNumber { get; set; }
        public string CultureCode { get; set; }
        public string VehicleType { get; set; }
    }
}