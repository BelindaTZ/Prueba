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
    public partial class fmAgregarProveedores : Form
    {
        csProveedor Proveedor;
        CsMovimientos movimientos;
        public fmAgregarProveedores() // Constructor default para agregar proveedor
        {
            InitializeComponent();
            cmbEstado.SelectedIndex = 0;
            btnModificarProveedor.Enabled = false;
            btnModificarProveedor.Visible = false;
        }
        int y;
        string id_prove;
        CsConexion conexion;
        public fmAgregarProveedores(string prv_IdProveedor, string prv_nombre, string prv_celular, string prv_servicios, string prv_direccion, string prv_email, int prv_estado)
        {
            // Constructor que se activa al momento de querer modificar un proveedor
            InitializeComponent();
            id_prove = prv_IdProveedor;
            txtNombre.Text = prv_nombre;
            txtCelular.Text = prv_celular;
            txtDireccion.Text = prv_direccion;
            txtEmail.Text = prv_email;
            cmbServicios.Text = prv_servicios;
            cmbEstado.SelectedIndex = prv_estado;
            txtNombre.Enabled = false;
            cmbServicios.Enabled = false;
            btnAgregarProveedor.Enabled = false;
            btnAgregarProveedor.Visible = false;
            
        }
        // Agregacion de un proveedor
        private void btnAgregarProveedor_Click(object sender, EventArgs e)
        {
            Proveedor = new csProveedor();
            Proveedor.Nombre = txtNombre.Text;
            Proveedor.Email = txtEmail.Text;
            Proveedor.Servicios = cmbServicios.Text;
            Proveedor.Estado = cmbEstado.Text;
            Proveedor.Celular = txtCelular.Text;
            Proveedor.Direccion = txtDireccion.Text;
            if (!Proveedor.AgregarProveedor()) MessageBox.Show("A ocurrido un error");
            else
            {
                txtNombre.Clear();
                txtEmail.Clear();
                cmbServicios.SelectedIndex = -1;
                cmbEstado.Items.Clear();
                txtCelular.Clear();
                txtDireccion.Clear();
                movimientos = new CsMovimientos();
                string idEmple = movimientos.ID();
                movimientos.Accion("Se agrego un proveedor", idEmple);
            }          
        }
        // Modificacion de un proveedor
        private void btnModificarProveedor_Click(object sender, EventArgs e)
        {
            Proveedor = new csProveedor(int.Parse(id_prove), txtNombre.Text, txtCelular.Text, cmbServicios.Text, txtDireccion.Text, txtEmail.Text, cmbEstado.Text);
            if (Proveedor.ModificarProveedor())
            {
                movimientos = new CsMovimientos();
                string idEmple = movimientos.ID();
                movimientos.Accion("Se modifico un proveedor", idEmple);
                this.Hide();
            }
        }
        // Bloquea el ingreso de informacion para que solo seleccione
        private void cmbEstado_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
            return;
        }

        private void txtNombre_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void txtCelular_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void txtServicios_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void txtDireccion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void txtEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void cmbEstado_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void fmAgregarProveedores_Load(object sender, EventArgs e)
        {
            Proveedor = new csProveedor();
            DataTable Service = Proveedor.ConsultaServicios();
            for (int i = 0; i < Service.Rows.Count; i++)
                cmbServicios.Items.Add(Service.Rows[i][0].ToString());
        }

        private void cmbServicios_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
    }
}
