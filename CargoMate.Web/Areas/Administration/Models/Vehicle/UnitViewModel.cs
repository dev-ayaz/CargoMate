using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CargoMateSolution.Shared;

namespace CargoMateSolution.Areas.Administration.Models.Vehicle
{
    public class UnitViewModel
    {
        public WeightModel WeightModel { get; set; }

        public List<WeightModel> WeightModelList { get; set; }

        public LengthModel LengthModel { get; set; }

        public List<LengthModel> LengthModelList { get; set; } 

    }

    public class WeightModel
    {
        public WeightModel()
        {
            Languges = GlobalProperties.Languges;
        }
        [Key]
        public long Id { get; set; }

        [StringLength(10)]
        public string CultureCode { get; set; }

        public string ShortName { get; set; }

        [StringLength(500)]
        public string FullName { get; set; }

        public decimal? WeightMultiple { get; set; }

        public bool IsMetric { get; set; }

        public List<SelectListItem> Languges { get; set; }


        
    }

    public class LengthModel
    {
        public LengthModel()
        {
            Languges = GlobalProperties.Languges;
        }
        [Key]
        public long Id { get; set; }

        [StringLength(10)]
        public string CultureCode { get; set; }

        public string ShortName { get; set; }

        [StringLength(500)]
        public string FullName { get; set; }

        public decimal? LengthMultiple { get; set; }

        public bool IsMetric { get; set; }

        public List<SelectListItem> Languges { get; set; }



    }
}