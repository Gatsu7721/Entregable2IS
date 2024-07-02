using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asistencia
{
    public class BDGeneral
    {
        public static SqlConnection Obtenerconexion()
        {
            SqlConnection conexion = new SqlConnection("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=BDtrabajador;Data Source=DESKTOP-469EI7L\\SQLEXPRESS");
            conexion.Open();

            return conexion;
        }

    }
}
