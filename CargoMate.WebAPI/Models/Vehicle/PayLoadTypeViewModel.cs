using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace CargoMate.WebAPI.Models.Vehicle
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