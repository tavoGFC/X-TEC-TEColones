using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace X_TEC.TEColones.Models.AdminModels
{
    public class NewAdminSCM
    {

        /// <summary>
        /// Get or Set FirstName User
        /// </summary>
        [Required(ErrorMessage = "Campo Requerido")]
        [DisplayName("Nombre:")]
        public string FirstName { get; set; }

        /// <summary>
        /// Get or Set LastName User
        /// </summary>
        [Required(ErrorMessage = "Campo Requerido")]
        [DisplayName("Apellidos:")]
        public string LastName { get; set; }

        /// <summary>
        /// Get or Set Email User
        /// </summary>
        [Required(ErrorMessage = "Campo Requerido")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Formato de correo incorrecto")]
        [DisplayName("Correo Electronico:")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        /// <summary>
        /// Get or Set Identidication User
        /// </summary>
        [DisplayName("Numero de Cedula:")]
        [Required(ErrorMessage = "Campo Requerido")]
        public string Identification { get; set; }

        /// <summary>
        /// Get or Set University User
        /// </summary>
        [DisplayName("Universidad:")]
        [DisplayFormat(NullDisplayText = "X-Tecnologico de Costa Rica")]
        [DefaultValue("X-Tecnologico de Costa Rica")]
        public string University { get; set; }

        /// <summary>
        /// Get or Set Headquarter User
        /// </summary>
        [DisplayName("Sede:")]
        [Required(ErrorMessage = "Campo Requerido")]
        public string Headquarter { get; set; }

        /// <summary>
        /// Get or Set Department
        /// </summary>
        [DisplayName("Departamento:")]
        [Required(ErrorMessage = "Campo Requerido")]
        public string Department { get; set; }


        /// <summary>
        /// Get or Set Password User
        /// </summary>
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Campo Requerido")]
        [DisplayName("Contraseña:")]
        [StringLength(12, ErrorMessage = ":Contraseña debe ser menor o igual a 12 caracteres")]
        [RegularExpression(@"^[\w\s]*$", ErrorMessage = "Caracteres especiales no permitidos")]
        public string Password { get; set; }

        /// <summary>
        /// Get or Set PhoneNumber
        /// </summary>
        [Required(ErrorMessage = "Campo Requerido")]
        [DisplayName("Numero telefonico:")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
       


        public void SetValues()
        {
            University = "X-Tecnologico de Costa Rica";
            Password = Identification;
        }
    }
}
