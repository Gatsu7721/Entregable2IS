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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Persona persona = new Persona();
            persona.nombre = txtNombre.Text;
            persona.edad = Convert.ToInt32(txtEdad.Text);
            persona.celular = txtCelular.Text;



            if (dataGridView1.SelectedRows.Count == 1)
            {
                int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["id"].Value);
                if (id != null)
                {
                    persona.id = id;
                    int result = PersonaDAL.ModificarPersona(persona);
                    if (result > 0)
                    {
                        MessageBox.Show("Exito al modificar");
                    }
                    else
                    {
                        MessageBox.Show("Error al modificar");
                    }
                }
            }
            else
            {
                int result = PersonaDAL.AgregarPersona(persona);

                if (result > 0)
                {
                    MessageBox.Show("Exito al guardar");
                }
                else
                {
                    MessageBox.Show("Error al guardar");
                }
            }

            refressPantalla();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            refressPantalla();
            txtId.Enabled = false;
        }

        public void refressPantalla()
        {
            dataGridView1.DataSource = PersonaDAL.PresentarRegistro();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            txtId.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["id"].Value);
            txtNombre.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["nombre"].Value);
            txtEdad.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["edad"].Value);
            txtCelular.Text = Convert.ToString(dataGridView1.CurrentRow.Cells["celular"].Value);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            txtId.Clear();
            txtNombre.Clear();
            txtEdad.Clear();
            txtCelular.Clear();
            dataGridView1.CurrentCell = null;

            dataGridView1.ClearSelection();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["id"].Value);
                int resultado = PersonaDAL.EliminarPersona(id);
                if (resultado > 0)
                {
                    MessageBox.Show("Eliminado con éxito");
                }
                else
                {
                    MessageBox.Show("Error al eliminar");
                }
            }
            refressPantalla();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FormAsistencias formAsistencias = new FormAsistencias();
            formAsistencias.ShowDialog(); 
        }
    }
}
