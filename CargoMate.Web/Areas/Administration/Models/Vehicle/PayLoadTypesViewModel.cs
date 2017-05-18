using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CargoMateSolution.Shared;

namespace CargoMateSolution.Areas.Administration.Models.Vehicle
{
    public class PayLoadTypesViewModel
    {
        public PayLoadModel PayLoadModel { get; set; }
        public List<PayLoadModel> PayLoadModelList { get; set; }
    }

    public class PayLoadModel
    {
        public PayLoadModel()
        {
            Languges = GlobalProperties.Languges;
            VehicleTypesList = new List<SelectListItem>();
        }
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

         [Display(Name = "Vehicle Type")]
        public string TypeName { get; set; }

        [Display(Name = "Type Name")]
        [Required]
        public long TypeId { get; set; }
        [Display(Name = "Language")]
        public string CultureCode { get; set; }
        public List<SelectListItem> Languges { get; set; }

        public List<SelectListItem> VehicleTypesList { get; set; }
    }
}