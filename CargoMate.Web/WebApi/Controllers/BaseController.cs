using System.Web.Http;
using CargoMate.DataAccess.DBContext;

namespace CargoMateSolution.WebApi.Controllers
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
