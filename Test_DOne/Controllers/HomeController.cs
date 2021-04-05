using System;
using System.Web.Mvc;
using Test_DOne.Models;
using Test_DOne.Models.Extensions;

namespace Test_DOne.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult getGridBounds(string value)
        {
            GridBounds result = new GridBounds();
            result = value.ToGridBounds();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult getCoordinate(string value)
        {
            Coordinate result = new Coordinate();
            result = value.ToCoordinate();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult doWork(string value)
        {
            // Split the value input in order to get first the grid bounds
            string[] vals = value.Split(new[] { Environment.NewLine, "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            // Init the model result with the grid bounds.
            Script scr = new Script(vals[0]);
            for (int i = 1; i < vals.Length; i += 2)
            { 
                // After the grid bounds each two lines are the definition for a rover
                scr.rovers.Add(new Rover(vals[i], vals[i + 1], (scr.rovers.Count + 1).ToString(), scr.gridBounds));
            }
            return Json(scr, JsonRequestBehavior.AllowGet);
        }
    }
}