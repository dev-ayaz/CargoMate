using System.Web.Mvc;
using CargoMate.DataAccess.DBContext;

namespace CargoMateSolution.Areas.Administration.Controllers
{
    public class BaseController : Controller
    {
           public DBContext DbContext;
           public BaseController()
        {
            DbContext = new DBContext();
           
        }
    }
}