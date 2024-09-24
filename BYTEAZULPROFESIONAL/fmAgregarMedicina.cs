using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BYTEAZULPROFESIONAL
{
    public partial class fmAgregarMedicina : Form
    {
        csMedicina Medicina;
        CsConexion conexion;
        CsMovimientos movimientos;
        int y;
        string ControladorActivo; // Variable que me va a controlar la modificacion de productos "Disponibles/NoDisponibles"
        public fmAgregarMedicina() // constructor que se activa cuando se entra a agregar una medicina
        {
            InitializeComponent();
            btnModificarMedicina.Enabled = false;
            btnModificarMedicina.Visible = false;
            cmbEstado.SelectedIndex = 0;
            cmbEstado.Enabled = false;
        }
        public fmAgregarMedicina(int z, string prv_nombre, string prv_categoria, string prv_IdProveedor, string prv_descripcion, string prv_precio, string prv_iva)
        {
            // constructor que se activa cuando se devuelve a este fm despues de la seleccion del proveedor
            InitializeComponent();
            this.Refresh();
            btnModificarMedicina.Enabled = false;
            btnModificarMedicina.Visible = false;
            txtNombreMedicina.Text = prv_nombre;
            cmbCategoria.Text = prv_categoria;
            MessageBox.Show(prv_IdProveedor);
            txtIDProveedor.Text = prv_IdProveedor.ToString();
            txtDescripcion.Text = prv_descripcion;
            txtPrecio.Text = prv_precio;
            cmbIva.Text = prv_iva;
            y = z;
        }
        string idMedicin;
        public fmAgregarMedicina(string IdMedicina, string NombreMedicina, string Categor, string Descripcion, int IdProveedor, string Iva, double Precio, string estadi)
        {
            // constructor que se activa cuando se va a modificar una medicina
            InitializeComponent();
            btnAgregarMedicina.Enabled = false;
            btnAgregarMedicina.Visible = false;
            idMedicin = IdMedicina;
            txtNombreMedicina.Text = NombreMedicina;
            cmbCategoria.Text = Categor;
            txtIDProveedor.Text = IdProveedor.ToString();
            txtDescripcion.Text = Descripcion;
            txtPrecio.Text = Precio.ToString().Replace(',', '.');
            cmbIva.Text = Iva;
            cmbEstado.Text = estadi;
            ControladorActivo = estadi;
            txtNombreMedicina.Enabled = false;
            btnBuscar.Enabled = false;
        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            fmGestionarProveedores VerProveedores = new fmGestionarProveedores(9, txtNombreMedicina.Text, cmbCategoria.Text, txtIDProveedor.Text, txtDescripcion.Text, txtPrecio.Text, cmbIva.Text);
            this.AddOwnedForm(VerProveedores); // Modificacion para que valiera el id de proveedor - se hizo publico el txt
            VerProveedores.ShowDialog();
        }
        // Agregar Medicina
        private void btnAgregarMedicina_Click(object sender, EventArgs e)
        {
            int idMedicin;
            conexion = new CsConexion();
            Medicina = new csMedicina();
            Medicina.NombreMedicina = txtNombreMedicina.Text;
            Medicina.Descripcion = txtDescripcion.Text;
            Medicina.Categoria = cmbCategoria.Text;
            Medicina.Estado = cmbEstado.Text;
            Medicina.Iva = cmbIva.Text;
            Medicina.IdMedicina = (conexion.Leer("Select * from Medicina").Rows.Count + 1).ToString();
            if (txtIDProveedor.Text == "")
                Medicina.IdProveedor = 0;
            else
                Medicina.IdProveedor = int.Parse(txtIDProveedor.Text);
            if (txtPrecio.Text == "")
                Medicina.Precio = 0;
            else
                Medicina.Precio = double.Parse(txtPrecio.Text.Replace('.', ','));
            //if (txtIva.Text == "")
            //    Medicina.Iva = -1;
            //else
            //    Medicina.Iva = double.Parse(txtIva.Text.Replace('.', ','));
            if (Medicina.AgregarMedicina())
            {
                movimientos = new CsMovimientos();
                string idempleado = movimientos.ID();
                idMedicin = int.Parse(Medicina.IdMedicina);
                MessageBox.Show("Producto Ingresado Exitosamente :D");
                movimientos.Accion("Se agrego una medicina", idempleado);
                CsMovimientos moviProdu = new CsMovimientos(idMedicin.ToString(), idempleado);
                moviProdu.Agg1Productos(cmbEstado.Text);
                this.Hide();
            }
        }
        // Control para que solo se ingresen numeros y el punto (decimal) para el Iva
        private void txtIva_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if ((e.KeyChar >= 32 && e.KeyChar <= 45) || (e.KeyChar == 47) || (e.KeyChar >= 58 && e.KeyChar <= 255))
            //{
                e.Handled = true;
            //}
        }
        //  Control para que solo se ingresen numeros y el punto (decimal) para el Precio
        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 && e.KeyChar <= 45) || (e.KeyChar == 47) || (e.KeyChar >= 58 && e.KeyChar <= 255))
            {
                e.Handled = true;
                return;
            }
        }
        // Modificacion de la medicina
        private void btnModificarMedicina_Click(object sender, EventArgs e)
        {
            // Control total de campos vacios para pasar los parametros a la clase Medicina y dicha clase 
            // procese correctamente su ejecucion
            int prove; double preci;
            if (txtIDProveedor.Text == "")
                prove = 0;
            else
                prove = int.Parse(txtIDProveedor.Text);
            if (txtPrecio.Text == "")
                preci = 0;
            else
                preci = double.Parse(txtPrecio.Text.Replace('.', ','));
            //if (txtIva.Text == "")
            //    iv = -1;
            //else
            //    iv = double.Parse(txtIva.Text.Replace('.', ','));
            Medicina = new csMedicina(txtNombreMedicina.Text, idMedicin, cmbCategoria.Text, txtDescripcion.Text, prove, preci, cmbIva.Text, cmbEstado.Text);
            if (Medicina.ModificarMedicina())
            {
                if(cmbEstado.Text != ControladorActivo)
                {
                    movimientos = new CsMovimientos();
                    string idempleado = movimientos.ID();
                    movimientos.Accion("Se modifico una medicina", idempleado);
                    CsMovimientos movimientosProdu = new CsMovimientos(idMedicin, idempleado);
                    if (cmbEstado.Text == "Disponible")
                        movimientosProdu.HabilitarProductos();
                    else if (cmbEstado.Text == "NoDisponible")
                        movimientosProdu.DeshabilitarProductos();
                }
                this.Hide();
            }
        }
        // En el load llenamos el combobox categoria con sus categorias obviamente
        private void fmAgregarMedicina_Load(object sender, EventArgs e)
        {
            Medicina = new csMedicina();
            DataTable Categorias = Medicina.ConsultaCategorias();
            for (int i = 0; i < Categorias.Rows.Count; i++)
                cmbCategoria.Items.Add(Categorias.Rows[i][0].ToString());
        }
        // control para que no se pueda escribir en el combobox y solo se tenga que seleccionar
        private void cmbCategoria_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
            return;
        }

        private void txtNombreMedicina_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void txtDescripcion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void cmbCategoria_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void txtPrecio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void cmbIva_KeyDown(object sender, KeyEventArgs e)
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
    }
}
