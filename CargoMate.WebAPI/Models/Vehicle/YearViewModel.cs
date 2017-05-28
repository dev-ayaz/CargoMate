using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CargoMate.WebAPI.Models.Vehicle
{
    public class YearViewModel
    {
        public List<YearModel> Items { get; set; }
    }

    public class YearModel
    {
        public long Id { get; set; }

        public string Name { get; set; }
    }
}