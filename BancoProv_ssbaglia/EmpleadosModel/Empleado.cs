using System;

namespace EmpleadosModel
{
    public class Empleado
    {
        public Empleado()
        {
        }

        public Empleado(int id, string codigo, string apellido, string nombre, DateTime fechaAlta, int idTipoDto, int numDocumento)
        {
            Id = id;
            Codigo = codigo;
            Apellido = apellido;
            Nombre = nombre;
            FechaAlta = fechaAlta;
            IdTipoDto = idTipoDto;
            NumDocumento = numDocumento;
        }

        public int Id { get; set; }
        public string  Codigo { get; set; }
        public string Apellido { get; set; }
        public string Nombre{ get; set; }
        public DateTime FechaAlta{ get; set; }
        public int IdTipoDto{ get; set; }
        public int NumDocumento{ get; set; }
      
      
    }









}
