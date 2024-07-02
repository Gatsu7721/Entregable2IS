using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Asistencia
{
    public partial class FormAsistencias : Form
    {
        public FormAsistencias()
        {
            InitializeComponent();
        }

        private void FormAsistencias_Load(object sender, EventArgs e)
        {
            CargarUsuarios();
        }
        private void CargarUsuarios()
        {
            List<Persona> usuarios = PersonaDAL.PresentarRegistro();

            verUsuariosCreados.Items.Clear();

            verUsuariosCreados.DisplayMember = "nombre"; 

            foreach (Persona usuario in usuarios)
            {
                verUsuariosCreados.Items.Add(usuario);
            }

            if (verUsuariosCreados.Items.Count > 0)
            {
                verUsuariosCreados.SelectedIndex = 0;
            }
        }

        private void btnRegistrarAsistencia_Click(object sender, EventArgs e)
        {
            DateTime fechaHoraActual = DateTime.Now;

            Persona usuarioSeleccionado = (Persona)verUsuariosCreados.SelectedItem;

            int result = PersonaDAL.RegistrarAsistencia(usuarioSeleccionado.id, fechaHoraActual);

            if (result > 0)
            {
                MessageBox.Show($"Se registró la asistencia para {usuarioSeleccionado.nombre} a las {fechaHoraActual}");

                List<Asistencia> asistenciasActualizadas = PersonaDAL.ObtenerAsistenciasPorIdPersona(usuarioSeleccionado.id);
                dataGridViewAsistencias.DataSource = asistenciasActualizadas;
            }
            else
            {
                MessageBox.Show("Error al registrar la asistencia");
            }
        }
        private void verUsuariosCreados_SelectedIndexChanged(object sender, EventArgs e)
        {
            Persona usuarioSeleccionado = (Persona)verUsuariosCreados.SelectedItem;

            List<Asistencia> asistencias = PersonaDAL.ObtenerAsistenciasPorIdPersona(usuarioSeleccionado.id);

            dataGridViewAsistencias.DataSource = asistencias;
        }

        private void btnVerAsistencias_Click(object sender, EventArgs e)
        {
            Persona usuarioSeleccionado = (Persona)verUsuariosCreados.SelectedItem;

            List<Asistencia> asistencias = PersonaDAL.ObtenerAsistenciasPorIdPersona(usuarioSeleccionado.id);

            dataGridViewAsistencias.DataSource = asistencias;
        }
    }
}
