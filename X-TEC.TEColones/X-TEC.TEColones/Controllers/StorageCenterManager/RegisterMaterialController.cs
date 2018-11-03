using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace X_TEC.TEColones.Controllers.StorageCenterManager
{
    public class RegisterMaterialController : Controller
    {
        // GET: RegisterMaterial
        public ActionResult Index()
        {
            return View("~/Views/StorageCenterManager/RegisterMaterial/RegisterMaterial.cshtml");
        }
    }
}