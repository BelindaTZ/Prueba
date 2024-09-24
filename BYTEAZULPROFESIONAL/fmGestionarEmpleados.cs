using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BYTEAZULPROFESIONAL
{
    public partial class fmGestionarEmpleados : Form
    {
        string id;
        CsConexion conexion;
        CsMovimientos movimientos;
        csEmpleados empleados = new csEmpleados();
        public fmGestionarEmpleados()
        {
            InitializeComponent();
        }

        private void fmGestionarEmpleados_Load(object sender, EventArgs e)
        {
            Mostrar();
            CsAuxiliar auxiliar = new CsAuxiliar();
            fmAgregarLotes comprasimg = new fmAgregarLotes();
            this.AddOwnedForm(comprasimg);
            auxiliar.AgregarBoton(dgvVerEmpleados, comprasimg.imlLogo.Images[2], "Editar");
        }
        private void Mostrar()
        {
            DataTable dt = empleados.TablaEmpleados();
            dgvVerEmpleados.DataSource = dt;

            dgvVerEmpleados.AllowUserToAddRows = false;
            dgvVerEmpleados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void dgvVerEmpleados_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvVerEmpleados.Columns["Editar"].Index && e.RowIndex >= 0)
            {
                DialogResult res = MessageBox.Show("¿Esta seguro de que desea modificar los datos del empleado?", "Modificar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    DataGridViewRow filaSeleccionada = dgvVerEmpleados.SelectedRows[0];

                    string nombre = filaSeleccionada.Cells["Nombres"].Value.ToString();
                    string apellido = filaSeleccionada.Cells["Apellidos"].Value.ToString();
                    string identificadcion = filaSeleccionada.Cells["Identificación"].Value.ToString();
                    DateTime fechanacimiento = (DateTime)filaSeleccionada.Cells["Fecha de Nacimiento"].Value;
                    string genero = filaSeleccionada.Cells["Género"].Value.ToString();
                    string cargo = filaSeleccionada.Cells["Cargo"].Value.ToString();
                    DateTime fechaingreso = (DateTime)filaSeleccionada.Cells["Fecha de Ingreso"].Value;
                    string correo = filaSeleccionada.Cells["Email"].Value.ToString();
                    string direccion = filaSeleccionada.Cells["Dirección"].Value.ToString();
                    string celular = filaSeleccionada.Cells["Celular"].Value.ToString();
                    string estado = filaSeleccionada.Cells["Estado"].Value.ToString();

                    fmAgregarEmpleados empleados = new fmAgregarEmpleados(nombre, apellido, identificadcion, fechanacimiento, genero, cargo, fechaingreso, correo, direccion, celular, estado);
                    empleados.ShowDialog();
                    Mostrar();
                }
            }
        }
        void Buscar()
        {
            if (!string.IsNullOrWhiteSpace(txtBuscar.Text))
            {
                DataTable dt = empleados.TablaEmpleados(txtBuscar.Text);

                dgvVerEmpleados.DataSource = dt;
                dgvVerEmpleados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            else
                Mostrar();

            //Restaurar la posición de la columna "Acciones" al final
            if (dgvVerEmpleados.Columns["Editar"] != null)
            {
                dgvVerEmpleados.Columns["Editar"].DisplayIndex = dgvVerEmpleados.Columns.Count - 1;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Buscar();
        }

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                Buscar();
            }
        }
    }
}
