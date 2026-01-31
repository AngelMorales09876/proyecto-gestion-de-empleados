using System;
using System.IO;
using System.Windows.Forms;

namespace GestionEmpleados
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cboGenero.Items.Add("Masculino");
            cboGenero.Items.Add("Femenino");
        }

        // 🔹 MÉTODO PARA LIMPIAR CAMPOS
        private void LimpiarCampos()
        {
            txtID.Clear();
            txtNombre.Clear();
            txtApellidos.Clear();
            txtDireccion.Clear();
            txtTelefono.Clear();
            txtEmail.Clear();
            txtCargo.Clear();
            txtSalario.Clear();

            cboGenero.SelectedIndex = -1;
            dtpFechaIngreso.Value = DateTime.Now;

            txtID.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // VALIDACIÓN
            if (string.IsNullOrWhiteSpace(txtID.Text) ||
                string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtApellidos.Text) ||
                string.IsNullOrWhiteSpace(txtDireccion.Text) ||
                string.IsNullOrWhiteSpace(txtTelefono.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtCargo.Text) ||
                string.IsNullOrWhiteSpace(txtSalario.Text) ||
                cboGenero.SelectedIndex == -1)
            {
                MessageBox.Show("Debe completar todos los campos",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            // GUARDAR ARCHIVO
            SaveFileDialog guardar = new SaveFileDialog();
            guardar.Filter = "Archivos de texto|*.txt";
            guardar.Title = "Guardar datos del empleado";

            if (guardar.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(guardar.FileName, true))
                {
                    sw.WriteLine(
                        $"{txtID.Text}\t" +
                        $"{txtNombre.Text}\t" +
                        $"{txtApellidos.Text}\t" +
                        $"{txtDireccion.Text}\t" +
                        $"{txtTelefono.Text}\t" +
                        $"{txtEmail.Text}\t" +
                        $"{txtCargo.Text}\t" +
                        $"{txtSalario.Text}\t" +
                        $"{dtpFechaIngreso.Value.ToShortDateString()}\t" +
                        $"{cboGenero.Text}"
                    );
                }

                MessageBox.Show("Archivo guardado correctamente",
                    "Éxito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                // 🔹 LIMPIAR CAMPOS DESPUÉS DE GUARDAR
                LimpiarCampos();
            }
        }

        private void btnAbrir_Click(object sender, EventArgs e)
        {
            OpenFileDialog abrir = new OpenFileDialog();
            abrir.Filter = "Archivos de texto|*.txt";

            if (abrir.ShowDialog() == DialogResult.OK)
            {
                string contenido = File.ReadAllText(abrir.FileName);

                MessageBox.Show(
                    contenido,
                    "Datos del Empleado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show(
                "¿Desea salir de la aplicación?",
                "Confirmar salida",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (r == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
