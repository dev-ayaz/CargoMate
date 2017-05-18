using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CargoMateSolution.Shared;

namespace CargoMateSolution.Areas.Administration.Models.Vehicle
{
    public class MakeViewModel
    {
        public MakeModel MakeModel { get; set; }

        public List<MakeModel> MakeModelsList { get; set; } 
    }

    public class MakeModel
    {
        public MakeModel()
        {
            Languges = GlobalProperties.Languges;

        }
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Display(Name = "Image")]
        public string ImageUrl { get; set; }
 
        public string CountryName { get; set; }
        [Display(Name = "Country")]

        [Required]
        public long CountryId { get; set; }
        [Display(Name = "Language")]
        public string CultureCode { get; set; }
        public List<SelectListItem> Languges { get; set; }
        public List<SelectListItem> Countries { get; set; }
        
    }
}