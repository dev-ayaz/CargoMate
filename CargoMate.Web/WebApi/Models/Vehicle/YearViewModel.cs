using System.Collections.Generic;

namespace CargoMateSolution.WebApi.Models.Vehicle
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