using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using X_TEC.TEColones.Models;

namespace X_TEC.TEColones.Controllers.StorageCenterManager
{
    public class SCMHomeController : Controller
    {

        SCMModel empleado = new SCMModel();

        // GET: SCMHome
        public ActionResult Home()
        {
            
            empleado.Name = "Rebeca Beaker";
            return View("~/Views/StorageCenterManager/SCMHome/SCMHome.cshtml",empleado);
        }

        [HttpPost]
        public ActionResult Insertar(int InputCarnet, int InputPyC = 0, int InputPlastico = 0, int InputTetrapack = 0, int InputVidrio = 0) //LOS NOMBRES DE LOS PARAMETROS TIENEN QUE SER LOS NOMBRES DE LOS INPUT EN EL HTML
        {

            System.Diagnostics.Debug.WriteLine("CARNET INGRESADO: " + InputCarnet);
            System.Diagnostics.Debug.WriteLine("PYC INGRESADO: " + InputPyC);
            System.Diagnostics.Debug.WriteLine("PLASTICO INGRESADO: " + InputPlastico);
            System.Diagnostics.Debug.WriteLine("TETRAPACK INGRESADO: " + InputTetrapack);
            System.Diagnostics.Debug.WriteLine("VIDRIO INGRESADO: " + InputVidrio);

            return Home();
        }
    }
}