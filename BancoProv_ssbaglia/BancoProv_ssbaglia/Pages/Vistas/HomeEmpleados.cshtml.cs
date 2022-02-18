using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmpleadosModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BancoProv_ssbaglia.Pages.Vistas
{
    public class HomeEmpleadosModel : PageModel
    {
        [BindProperty]
        public string ErrorMessage { get; set; }


        [BindProperty]
        public Empleado empleado { get; set; }


      

        [BindProperty]
        public List<Empleado> Empleado { get; set; }



        public ActionResult OnGet()
        {
           Empleado = DB.GetEmpleado();
            
            return Page();
        }


        public ActionResult OnPostAltaEmpleado()
        {
            return RedirectToPage("/Vistas/AgregarEmpleado");
        }

        public ActionResult OnPostEditar(int idEmpleado)
        {
            return RedirectToPage("/Vistas/Editar", new { IdEmpleado = idEmpleado });
        }

        public ActionResult OnPostEliminar(int idEmpleado)
        {
            empleado.Id = idEmpleado;

            DB.EliminarEmpleado(empleado);
            ModelState.AddModelError("MensajeOk", "Empleado eliminada Correctamente.");
            Empleado = DB.GetEmpleado();
            return Page();

        }

    }
}
