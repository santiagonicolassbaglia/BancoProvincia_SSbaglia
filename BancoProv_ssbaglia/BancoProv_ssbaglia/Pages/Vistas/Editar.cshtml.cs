using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmpleadosModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BancoProv_ssbaglia.Pages.Vistas
{
    public class EditarModel : PageModel
    {

        [BindProperty]
        public string ErrorMessage { get; set; }

        [BindProperty]
        public string MensajeOk { get; set; }


        [BindProperty]
        public Empleado empleado { get; set; } = new Empleado();

          
        [BindProperty]
        public TiposDocumento tiposDocumento { get; set; } = new TiposDocumento();
        public void OnGet(int idEmpleado)
        {
            empleado.Id = idEmpleado;
            empleado = DB.GetEmpleadoPorId(idEmpleado);
            
        }


        public ActionResult OnPostEditarEmpleado()
        {

            if (ValidarEmpleado())
            {
                empleado.IdTipoDto = tiposDocumento.Id;
                ModelState.AddModelError("MensajeOk", "Empleado editado Correctamente.");
                DB.EditarEmpleados(empleado);
                return Page();
            }


            else
            {
                ModelState.AddModelError("ErrorMessage", "El empleado es incorrecta.");
                return Page();
            }
             
            return Page();

        }




        public bool ValidarEmpleado()
        {
            bool isValid = true;

            if (empleado.Nombre == null || empleado.Apellido == null || empleado.Codigo == null || empleado.NumDocumento < 100)
            {
                if (empleado.Codigo.Length > 5)
                    ModelState.AddModelError("ErrorMessage", "El código no puede contener mas de 5 números");
                isValid = false;

            }
            return isValid;
        }

    }
}
