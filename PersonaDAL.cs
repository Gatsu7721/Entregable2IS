using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistencia
{
    public class PersonaDAL
    {
        public static int AgregarPersona(Persona persona)
        {
            int retorna = 0;

            using (SqlConnection conexion = BDGeneral.Obtenerconexion())
            {
                string query = "insert into persona (nombre,edad,celular) values('" + persona.nombre + "' , " + persona.edad + ", '" + persona.celular + "')";
                SqlCommand comando = new SqlCommand(query, conexion);

                retorna = comando.ExecuteNonQuery();
            }

            return retorna;
        }

        public static List<Persona> PresentarRegistro()
        {
            List<Persona> Lista = new List<Persona>();

            using (SqlConnection conexion = BDGeneral.Obtenerconexion())
            {
                string query = "select * from persona";
                SqlCommand comando = new SqlCommand(query, conexion);

                using (SqlDataReader reader = comando.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Persona persona = new Persona
                        {
                            id = reader.GetInt32(0),
                            nombre = reader.GetString(1),
                            edad = reader.GetInt32(2),
                            celular = reader.GetString(3)
                        };
                        Lista.Add(persona);
                    }
                }
            }

            return Lista;
        }

        public static int ModificarPersona(Persona persona)
        {
            int result = 0;
            using (SqlConnection conexion = BDGeneral.Obtenerconexion())
            {
                string query = "update persona set nombre = '" + persona.nombre + "', edad =" + persona.edad + ", celular ='" + persona.celular + "' where id = " + persona.id + "";
                SqlCommand comando = new SqlCommand(query, conexion);

                result = comando.ExecuteNonQuery();
                conexion.Close();
            }
            return result;
        }
        public static int EliminarPersona(int id)
        {
            int retorna = 0;

            using (SqlConnection conexion = BDGeneral.Obtenerconexion())
            {
                string query = "delete from persona where id = " + id + " ";
                SqlCommand comando = new SqlCommand(query, conexion);

                retorna = comando.ExecuteNonQuery();
            }

            return retorna;
        }
        public static int RegistrarAsistencia(int idPersona, DateTime fechaHora)
        {
            int result = 0;

            using (SqlConnection conexion = BDGeneral.Obtenerconexion())
            {
                string query = "INSERT INTO Asistencia (idPersona, fechaHora) VALUES (@idPersona, @fechaHora)";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@idPersona", idPersona);
                comando.Parameters.AddWithValue("@fechaHora", fechaHora);

                result = comando.ExecuteNonQuery();
            }

            return result;
        }
        public static List<Asistencia> ObtenerAsistenciasPorIdPersona(int idPersona)
        {
            List<Asistencia> listaAsistencias = new List<Asistencia>();

            using (SqlConnection conexion = BDGeneral.Obtenerconexion())
            {
                string query = "SELECT id, idPersona, fechaHora FROM Asistencia WHERE idPersona = @idPersona";
                SqlCommand comando = new SqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@idPersona", idPersona);

                SqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    Asistencia asistencia = new Asistencia
                    {
                        Id = reader.GetInt32(0),
                        IdPersona = reader.GetInt32(1),
                        FechaHora = reader.GetDateTime(2)
                    };
                    listaAsistencias.Add(asistencia);
                }
            }

            return listaAsistencias;
        }
    }
}
