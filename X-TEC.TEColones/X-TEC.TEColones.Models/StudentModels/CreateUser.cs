using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;

namespace X_TEC.TEColones.Models.StudentModels
{
    public class CreateUser
    {

        public CreateUser()
        {
            Description = " ";
            Photo = " ";
            ImageByte = new byte[] { };
        }

        

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
        [DisplayName("Numero de Carnet:")]
        [Required(ErrorMessage = "Campo Requerido")]
        public string Identification { get; set; }

        public int Id { get; set; }

        /// <summary>
        /// Get or Set University User
        /// </summary>
        [DisplayName("Universidad:")]
        [Required(ErrorMessage = "Campo Requerido")]
        [DisplayFormat(NullDisplayText ="X-Tecnologico de Costa Rica")]
        public string University { get; set; }

        /// <summary>
        /// Get or Set Headquarter User
        /// </summary>
        [DisplayName("Sede:")]
        [Required(ErrorMessage = "Campo Requerido")]
        public string Headquarter { get; set; }
        
        /// <summary>
        /// Get or Set PhoneNumber User
        /// </summary>
        [DataType(DataType.PhoneNumber)]
        [DisplayName("Numero Telefono:")]
        [Required(ErrorMessage = "Campo Requerido")]
        public string PhoneNumber { get; set; }

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
        /// Get or Set PasswordVerify User
        /// </summary>
        [DisplayName("Confirmar Contraseña:")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="Las contraseñas no coinciden")]
        public string PasswordVerify { get; set; }

        /// <summary>
        /// Get or Set Description User
        /// </summary>
        [DisplayName("Descripcion(*):")]
        [DataType(DataType.MultilineText)]
        [StringLength(300, ErrorMessage = "Maximo cantidad de caracteres 300")]
        public string Description { get; set; }

        /// <summary>
        /// Get or Set Skills User
        /// </summary>
        [DisplayName("Habilidades:")]
        [Required(ErrorMessage = "Campo Requerido")]
        [DataType(DataType.MultilineText)]
        [StringLength(300, ErrorMessage = "Maximo cantidad de caracteres 300")]
        public string Skills { get; set; }

        /// <summary>
        /// Get or Set Photo User
        /// </summary>
        //[DataType(DataType.ImageUrl)]
        [DisplayName("Subir Foto(*):")]
        //[FileExtensions(Extensions = "jpg,jpeg,png")]
        public string Photo { get; set; }


        //[FileExtensions(Extensions = "jpg,jpeg,png")]
        public HttpPostedFileBase PhotoFile { get; set; }

        public byte[] ImageByte { get; set; }
                          
    }
}