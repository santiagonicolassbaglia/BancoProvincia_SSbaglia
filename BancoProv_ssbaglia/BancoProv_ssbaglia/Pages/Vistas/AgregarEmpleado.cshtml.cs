using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmpleadosModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BancoProv_ssbaglia.Pages.Vistas
{
    public class AgregarEmpleadoModel : PageModel
    {

        [BindProperty]
        public string ErrorMessage { get; set; }


        [BindProperty]
        public Empleado empleado { get; set; } = new Empleado();

        [BindProperty]
        public TiposDocumento tiposDocumento { get; set; } = new TiposDocumento();
        public void OnGet()
        {
          
        }



        public ActionResult OnPostAltaAdministrador()
        {
            Empleado registrarUsuario = new Empleado();

            empleado.IdTipoDto = tiposDocumento.Id;
            empleado.FechaAlta = DateTime.Now; 

            if (ValidarEmpleado())
            {
                DB.InsertarEmpleado(empleado);
            }


            else
            {
                ModelState.AddModelError("ErrorMessage", "El usuario es incorrecto.");
              
            }


            return Page();


        }



        public bool ValidarEmpleado()
         {
            bool isValid = true;

            if (empleado.Nombre == null || empleado.Apellido == null || empleado.Codigo == null || empleado.NumDocumento <100 )
            {
            if (empleado.Codigo.Length > 5  )
                    ModelState.AddModelError("ErrorMessage", "El código no puede contener mas de 5 números");

                isValid = false;
            
            }
            return isValid;
        }



        public bool ValidarString(string auxString)
        {
            if (String.IsNullOrEmpty(auxString) || auxString.Length < 2)
            {
                return true;

            }
            else
            {
                return false;
            }
        }
    }
}
