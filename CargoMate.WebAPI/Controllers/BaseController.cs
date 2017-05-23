using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CargoMate.DataAccess.DBContext;

namespace CargoMate.WebAPI.Controllers
{
    public class BaseController : ApiController
    {
        public DBContext DbContext;
        public BaseController()
        {
            DbContext = new DBContext();

        }
    }
}
