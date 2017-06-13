using System.Collections.Generic;

namespace CargoMateSolution.WebApi.Models.Vehicle
{
    public class PayLoadTypeViewModel
    {
        public List<PayLoadType> Items { get; set; } 
    }

    public class PayLoadType
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }
    }
}