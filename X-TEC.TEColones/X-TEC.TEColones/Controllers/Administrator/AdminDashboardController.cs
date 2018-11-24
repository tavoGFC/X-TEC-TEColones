using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using X_TEC.TEColones.Models.StudentModels;
using X_TEC.TEColones.Models.AdminModels;

namespace X_TEC.TEColones.Controllers.Administrator
{
    public class AdminDashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Dashboard()
        {

            AdminModel adminChang = new AdminModel
            {

                Email = "holi@wawa.com",
                Department = "Dep de pendejadas",
                FirstName = "miNombre",
                LastName = "miApellido",
                University = "LA u",
                Headquarter = "El Alamo",
                Id = 7654
            };

            //SEDES  >:(

            //Sede:Cant/Sede:Cant/Sede:Cant

            //string pares = "Sede1:111/Sede2:222/Sede3:333";
            //adminChang.DashboardModel.TxS = pares;

            
            
            //TONELADAS POR MES
            adminChang.DashboardModel.ToneladasXmes = new List<float>();
            adminChang.DashboardModel.ToneladasXmes.Add(11f);
            adminChang.DashboardModel.ToneladasXmes.Add(22f);
            adminChang.DashboardModel.ToneladasXmes.Add(33f);
            adminChang.DashboardModel.ToneladasXmes.Add(44f);
            adminChang.DashboardModel.ToneladasXmes.Add(55f);
            adminChang.DashboardModel.ToneladasXmes.Add(66f);
            adminChang.DashboardModel.ToneladasXmes.Add(7f);
            adminChang.DashboardModel.ToneladasXmes.Add(8f);
            adminChang.DashboardModel.ToneladasXmes.Add(9f);
            adminChang.DashboardModel.ToneladasXmes.Add(10f);
            adminChang.DashboardModel.ToneladasXmes.Add(1f);
            adminChang.DashboardModel.ToneladasXmes.Add(2f);

            //USUARIOS POR MES
            adminChang.DashboardModel.UsuariosXmes = new List<int>();
            adminChang.DashboardModel.UsuariosXmes.Add(7);
            adminChang.DashboardModel.UsuariosXmes.Add(14);
            adminChang.DashboardModel.UsuariosXmes.Add(21);
            adminChang.DashboardModel.UsuariosXmes.Add(28);
            adminChang.DashboardModel.UsuariosXmes.Add(35);
            adminChang.DashboardModel.UsuariosXmes.Add(42);
            adminChang.DashboardModel.UsuariosXmes.Add(49);
            adminChang.DashboardModel.UsuariosXmes.Add(56);
            adminChang.DashboardModel.UsuariosXmes.Add(63);
            adminChang.DashboardModel.UsuariosXmes.Add(70);
            adminChang.DashboardModel.UsuariosXmes.Add(77);
            adminChang.DashboardModel.UsuariosXmes.Add(84);

            //DINERO BENEFICIO
            adminChang.DashboardModel.DineroXbeneficio = new List<float>();
            adminChang.DashboardModel.DineroXbeneficio.Add(1.005f);
            adminChang.DashboardModel.DineroXbeneficio.Add(2.4f);
            adminChang.DashboardModel.DineroXbeneficio.Add(6.66f);
            adminChang.DashboardModel.DineroXbeneficio.Add(14.75f);
            adminChang.DashboardModel.DineroXbeneficio.Add(7.6f);
            adminChang.DashboardModel.DineroXbeneficio.Add(8.9f);
            adminChang.DashboardModel.DineroXbeneficio.Add(2.45f);
            adminChang.DashboardModel.DineroXbeneficio.Add(7.5f);
            adminChang.DashboardModel.DineroXbeneficio.Add(2.23f);
            adminChang.DashboardModel.DineroXbeneficio.Add(5.62f);
            adminChang.DashboardModel.DineroXbeneficio.Add(4.42f);
            adminChang.DashboardModel.DineroXbeneficio.Add(10.36f);

            //VELOCIMETRO
            adminChang.DashboardModel.ToneladasAnuales = 3.5f;

            //TOP10
            adminChang.DashboardModel.Top10 = new List<StudentModel>();
            adminChang.DashboardModel.Top10.Add( new StudentModel() { FirstName = "Kanye", LastName = "West", Id = 98765432, kgRecicled = 166 } );
            adminChang.DashboardModel.Top10.Add(new StudentModel() { FirstName = "Jay", LastName = "Z", Id = 864364894, kgRecicled = 146 });
            adminChang.DashboardModel.Top10.Add(new StudentModel() { FirstName = "Drake", LastName = "", Id = 45312354, kgRecicled = 121 });
            adminChang.DashboardModel.Top10.Add(new StudentModel() { FirstName = "Eminem", LastName = "", Id = 453134, kgRecicled = 106 });
            adminChang.DashboardModel.Top10.Add(new StudentModel() { FirstName = "Tupac", LastName = "Shakur", Id = 21346684, kgRecicled = 98 });
            adminChang.DashboardModel.Top10.Add(new StudentModel() { FirstName = "The Notorius", LastName = "B.I.G", Id = 768455, kgRecicled = 75 });
            adminChang.DashboardModel.Top10.Add(new StudentModel() { FirstName = "Kendrick", LastName = "Lamar", Id = 97456784, kgRecicled = 66 });
            adminChang.DashboardModel.Top10.Add(new StudentModel() { FirstName = "Lil", LastName = "Wayne", Id = 77845, kgRecicled = 53 });
            adminChang.DashboardModel.Top10.Add(new StudentModel() { FirstName = "Dr.", LastName = "Dre", Id = 34353, kgRecicled = 49 });
            adminChang.DashboardModel.Top10.Add(new StudentModel() { FirstName = "Snoop", LastName = "Dog", Id = 420, kgRecicled = 42 });

            //aqui iria el tempdata[admin]

            return View("~/Views/Administrator/AdminDashboard/Dashboard.cshtml", adminChang);
            
        }
    }
}