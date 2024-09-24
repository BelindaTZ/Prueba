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
    public partial class fmGestionarClientes : Form
    {
        CsConexion conexion;
        csClientes clientes = new csClientes();
        int num;
        public fmGestionarClientes()
        {
            InitializeComponent();
        }
        public fmGestionarClientes(int sal)
        {
            InitializeComponent();
            num = sal;
        }
        private void fmGestionarClientes_Load(object sender, EventArgs e)
        {
            Mostrar();
            CsAuxiliar auxiliar = new CsAuxiliar();
            fmAgregarLotes comprasimg = new fmAgregarLotes();
            this.AddOwnedForm(comprasimg);
            auxiliar.AgregarBoton(dgvVerClientes, comprasimg.imlLogo.Images[2], "Editar");
        }
        private void Mostrar()
        {
            DataTable ds = clientes.TablaClientes();

            dgvVerClientes.DataSource = ds;
            dgvVerClientes.AllowUserToAddRows = false;
            dgvVerClientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        private void dgvVerClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(num!=1)
            if (e.ColumnIndex == dgvVerClientes.Columns["Editar"].Index && e.RowIndex >= 0)
            {
                DialogResult res = MessageBox.Show("¿Esta seguro de que desea modificar los datos el cliente?", "Modificar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    DataGridViewRow filaSeleccionada = dgvVerClientes.SelectedRows[0];

                    string nombre = filaSeleccionada.Cells["Nombres"].Value.ToString();
                    string apellido = filaSeleccionada.Cells["Apellidos"].Value.ToString();
                    string identificacion = filaSeleccionada.Cells["Identificación"].Value.ToString();
                    string correo = filaSeleccionada.Cells["Email"].Value.ToString();
                    string direccion = filaSeleccionada.Cells["Dirección"].Value.ToString();
                    string celular = filaSeleccionada.Cells["Celular"].Value.ToString();
                    string estado = filaSeleccionada.Cells["Estado"].Value.ToString();

                    fmAgregarClientes clientes = new fmAgregarClientes(nombre, apellido, identificacion, correo, direccion, celular, estado);
                    clientes.ShowDialog();
                    Mostrar();
                }
            }
        }
        void Buscar()
        {
            clientes = new csClientes();
            if (!string.IsNullOrWhiteSpace(txtBuscar.Text))
            {
                DataTable ds = clientes.TablaClientes(txtBuscar.Text);

                dgvVerClientes.DataSource = ds;
                dgvVerClientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            else
                Mostrar();
        }

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                Buscar();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Buscar();
        }

        private void dgvVerClientes_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int fila = dgvVerClientes.CurrentCell.RowIndex;
            if (num == 1)
            {
                if (dgvVerClientes.Rows[fila].Cells["Estado"].Value.ToString().Trim() == "Activo")
                {
                    fmCaja caja = Owner as fmCaja;
                    caja.txtIdCliente.Text = dgvVerClientes.Rows[fila].Cells["ID Cliente"].Value.ToString();
                    caja.txtIdCliente.Enabled = false;
                    this.Hide();
                }
                else MessageBox.Show("Este cliente no se encuentra disponible");
            }
        }
    }
}
