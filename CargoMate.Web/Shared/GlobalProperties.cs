using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CargoMateSolution.Shared
{
    public static class GlobalProperties
    {
        public static List<SelectListItem> Languges = new List<SelectListItem>
            {
                new SelectListItem {Text = "English US", Value = "en-US"},
                new SelectListItem {Text = "Arabic", Value = "ar-SA"}
            }; 
    }
}