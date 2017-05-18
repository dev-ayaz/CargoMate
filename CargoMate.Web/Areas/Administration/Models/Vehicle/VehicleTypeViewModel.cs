using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CargoMateSolution.Areas.Administration.Models.Vehicle
{
    public class VehicleTypeViewModel
    {
        public VehicleTypeViewModel()
        {
            Languges = new List<SelectListItem>
            {
                new SelectListItem {Text = "English US", Value = "en-US"},
                new SelectListItem {Text = "Arabic", Value = "ar-SA"}
            };
        }

        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "Please enter Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter Descreption")]
        public string Descreption { get; set; }

        public string ImageUrl { get; set; }

        [Display(Name = "Language")]
        [Required(ErrorMessage = "Please select a language")]
        public string CultureCode { get; set; }
        public bool IsEquipment { get; set; }
        public List<SelectListItem> Languges { get; set; } 
        public List<VehicleTypeConfigurationsViewModel> Configurations { get; set; }
        public List<VehicleCapicityViewModel> Capicities { get; set; }

    }
}