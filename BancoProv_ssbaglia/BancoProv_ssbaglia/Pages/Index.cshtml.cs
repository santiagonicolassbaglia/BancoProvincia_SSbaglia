using EmpleadosModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BancoProv_ssbaglia.Pages
{
    public class IndexModel : PageModel
    {
        

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }


        [BindProperty]
        public string ErrorMessage { get; set; }


        [BindProperty]
        public Empleado empleado { get; set; }

        

        public ActionResult OnPostBtnIngresar_Click()
        {
             

            if (!ValidarString(empleado.Nombre))
            {
                empleado = DB.validaUsuario(empleado.Nombre, empleado.NumDocumento);

                if (!(empleado is null))
                {
                    
                    
                        return RedirectToPage("/Vistas/HomeEmpleados");
                  
                }

                else
                {
                    ModelState.AddModelError("ErrorMessage", "El empleado es incorrecto.");
                    return Page();
                }

            }

            return Page();

        }


        public ActionResult OnPostAltaEmpleado_Click()
        {
            return RedirectToPage("Vistas/AgregarEmpleado");
        }



        public static bool ValidarString(string auxString)
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
