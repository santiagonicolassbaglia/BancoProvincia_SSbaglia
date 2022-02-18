using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpleadosModel
{
   public class DB
    {


        static SqlConnection conectarAlDB;



        static DB()
        {
            conectarAlDB = new SqlConnection("Server=(local)\\sqlexpress;Database=Test_Bp_Ssbaglia;Trusted_Connection=True;");
        }

        //USUARIO
        public static Empleado validaUsuario(string nombre, int documento)
        {
            SqlCommand commandd;
            commandd = new SqlCommand();
            SqlConnection sqlConnection;
            string connectionString = "Server=(local)\\sqlexpress;Database=Test_Bp_Ssbaglia;Trusted_Connection=True;";
            sqlConnection = new SqlConnection(connectionString);

            string sql = "select * from  Empleados  where  Nombre = @nombre and NumDocumento = @documento";
            commandd.CommandText = sql;
            commandd.Parameters.Clear();
            commandd.Parameters.Add(new SqlParameter("@nombre", nombre));
            commandd.Parameters.Add(new SqlParameter("@documento", documento));

            commandd.Connection = sqlConnection;
            try
            {
                sqlConnection.Open();
                SqlDataReader dr = commandd.ExecuteReader();
                if (dr.Read())
                {

                    Empleado user = new Empleado();
                    user.Codigo = (dr["Codigo"].ToString());
                    user.Apellido = (dr["Apellido"].ToString());
                    user.Nombre = nombre;
                    user.FechaAlta = DateTime.Parse(dr["FechaAlta"].ToString());
                    user.IdTipoDto = int.Parse(dr["IdTipoDto"].ToString());
                    user.NumDocumento = documento;


                    return user;
                }
                else
                    return null;


            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                sqlConnection.Close();
            }

        }



        public static void InsertarEmpleado(Empleado empleado)
        {
            try
            {


                string command = "INSERT INTO Empleados (Codigo,Apellido,Nombre,FechaAlta,IdTipoDto,NumDocumento) VALUES(@codigo,@apellido,@nombre,@fechaAlta,@idTipoDto,@numDocumento)";

                SqlCommand sqlCommand = new SqlCommand(command, conectarAlDB);


                sqlCommand.Parameters.AddWithValue("@codigo", empleado.Codigo);
                sqlCommand.Parameters.AddWithValue("@apellido", empleado.Apellido);
                sqlCommand.Parameters.AddWithValue("@nombre", empleado.Nombre);
                sqlCommand.Parameters.AddWithValue("@fechaAlta", empleado.FechaAlta);
                sqlCommand.Parameters.AddWithValue("@idTipoDto", empleado.IdTipoDto);
                sqlCommand.Parameters.AddWithValue("@numDocumento", empleado.NumDocumento);




                conectarAlDB.Open(); //abrir la coneccion 
                sqlCommand.ExecuteNonQuery();// nos devuelve la cantidad de filas
            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {
                if (conectarAlDB.State == System.Data.ConnectionState.Open)
                {
                    conectarAlDB.Close();
                }
            }



        }




        public static List<Empleado> GetEmpleado()
        {

            //   SqlConnection sqlConnection;
            List<Empleado> auxPel = new List<Empleado>();
            try
            {

                SqlCommand comandito = new SqlCommand();

                comandito.Connection = conectarAlDB;
                comandito.CommandType = CommandType.Text;
                comandito.CommandText = "SELECT * FROM Empleados";

                if (conectarAlDB.State != ConnectionState.Open)
                    conectarAlDB.Open();


                SqlDataReader datosDevueltos = comandito.ExecuteReader();


                while (datosDevueltos.Read())
                {


                    auxPel.Add(new Empleado(int.Parse(datosDevueltos["Id"].ToString()),
                                                datosDevueltos["Codigo"].ToString(),
                                              datosDevueltos["Apellido"].ToString(),
                                             datosDevueltos["Nombre"].ToString(),
                                             DateTime.Parse(datosDevueltos["FechaAlta"].ToString()),
                                            int.Parse(datosDevueltos["IdTipoDto"].ToString()),
                                             int.Parse(datosDevueltos["NumDocumento"].ToString())));
                }


                return auxPel;
            }

            catch (Exception ex)
            {
                return null;
            }

            finally
            {
                conectarAlDB.Close();
            }
        }






        public static Empleado GetEmpleadoPorId(int id)

        {
            string connectionString = "Server=(local)\\sqlexpress;Database=Test_Bp_Ssbaglia;Trusted_Connection=True;";
            //SqlCommand comandito = new SqlCommand();

            Empleado empleado = new Empleado();

            try
            {
                string sql = "SELECT * FROM Empleados WHERE Id=" + id;

                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                SqlDataReader reader;
                con.Open();

                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    empleado.Id = (int)reader[0];
                    empleado.Codigo = reader[1].ToString();
                    empleado.Apellido = reader[2].ToString();
                    empleado.Nombre = reader[3].ToString(); 
                    empleado.FechaAlta = (DateTime)reader[4];
                    empleado.IdTipoDto = (int)reader[5];
                    empleado.NumDocumento = (int)reader[6];
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                conectarAlDB.Close();
            }

            return empleado;
        }




        public static void EditarEmpleados(Empleado empleado)
        {


            conectarAlDB.Open();
            string sql = "Update Empleados Set Codigo = @codigo, " +
                "Apellido = @Apellido, Nombre = @nombre, FechaAlta = @fechaAlta , IdTipoDto = @idTipoDto , NumDocumento = @numDocumento where Id = @id";
            SqlCommand comando = new SqlCommand(sql, conectarAlDB);

            comando.Parameters.Add(new SqlParameter("@codigo", empleado.Codigo));
            comando.Parameters.Add(new SqlParameter("@apellido", empleado.Apellido));
            comando.Parameters.Add(new SqlParameter("@nombre", empleado.Nombre));
            comando.Parameters.Add(new SqlParameter("@fechaAlta", empleado.FechaAlta));
            comando.Parameters.Add(new SqlParameter("@idTipoDto", empleado.IdTipoDto));
            comando.Parameters.Add(new SqlParameter("@numDocumento", empleado.NumDocumento));
            comando.Parameters.Add(new SqlParameter("@id", empleado.Id));

            comando.ExecuteNonQuery();
            conectarAlDB.Close();
        }




        public static void EliminarEmpleado(Empleado empleado)
        {


            conectarAlDB.Open();
            string sql = "Delete from Empleados  where Id = @auxID";
            SqlCommand comando = new SqlCommand(sql, conectarAlDB);

            comando.Parameters.Add(new SqlParameter("@auxID", empleado.Id));

            comando.ExecuteNonQuery();
            conectarAlDB.Close();
        }






    }
}
