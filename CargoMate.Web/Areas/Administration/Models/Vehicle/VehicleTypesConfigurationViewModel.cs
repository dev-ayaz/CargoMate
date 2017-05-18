using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CargoMateSolution.Areas.Administration.Models.Vehicle
{
    public class VehicleTypeConfigurationsViewModel
    {
        public VehicleTypeConfigurationsViewModel()
        {
            VehicleTypesListItems = new List<SelectListItem>();
          Languges = new List<SelectListItem> {
                new SelectListItem {Text = "English US", Value = "en-US"},
                new SelectListItem {Text = "Arabic", Value = "ar-SA"}
            };
        }

        public List<SelectListItem> VehicleTypesListItems { get; set; } 

        [Key]
        public long Id { get; set; }

        public string Name { get; set; }
        public string Descreption { get; set; }
        public string ImageUrl { get; set; }

        [Display(Name = "Language")]
        public string CultureCode { get; set; }
        [Display(Name = "Vehicle Type")]
        public string VehicleTypeName { get; set; }

        [Display(Name = "Vehicle Type")]
        public long TypeId { get; set; }

        public List<SelectListItem> Languges { get; set; }

    }
    public class VehicleTypeConfigurationListModel
    {
        public VehicleTypeConfigurationListModel()
        {
            Languges = new List<SelectListItem>
            {
                new SelectListItem {Text = "English US", Value = "en-US"},
                new SelectListItem {Text = "Arabic", Value = "ar-SA"}
            };
        }
        [Key]
        public long Id { get; set; }

        public string Name { get; set; }
        public string Descreption { get; set; }
        public string ImageUrl { get; set; }

        [Display(Name = "Language")]
        public string CultureCode { get; set; }
        [Display(Name = "Vehicle Type")]
        public string VehicleTypeName { get; set; }

        [Display(Name = "Vehicle Type")]
        public long TypeId { get; set; }

        public List<SelectListItem> Languges { get; set; }

    }
}