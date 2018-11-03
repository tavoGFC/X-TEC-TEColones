using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace X_TEC.TEColones.Controllers.Administrator
{
    public class PromotionController : Controller
    {
        // GET: Promotion
        public ActionResult ViewPromotion()
        {
            return View("~/Views/Administrator/Promotion/ViewPromotion.cshtml");
        }

        public ActionResult CreatePromotion()
        {
            return View("~/Views/Administrator/Promotion/CreatePromotion.cshtml");
        }
    }
}