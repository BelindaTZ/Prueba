using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BYTEAZULPROFESIONAL
{
    public partial class fmCaja : Form
    {
        CsCaja caja;
        CsMovimientos movimientos = new CsMovimientos();
        public fmCaja()
        {
            InitializeComponent();
        }
        private void llenartxttex()
        {
            double subtotal = 0, iva = 0;
            for (int i = 0; i < dgvDetallesVentas.Rows.Count; i++)
            {
                caja = new CsCaja();
                caja.IdProducto = dgvDetallesVentas[1, i].Value.ToString();
                subtotal += double.Parse(dgvDetallesVentas[5, i].Value.ToString().Replace('.', ','));
                iva += double.Parse(dgvDetallesVentas[5, i].Value.ToString()) * caja.iva();
            }
            txtSubtotal.Text = "Subtotal: " + subtotal.ToString("F2");
            txtIva.Text = "Iva: " + iva.ToString("F2");
            txtTotal.Text = "Total: " + (subtotal + iva).ToString("F2");
        }
        private bool com()
        {
            try
            {
                if (double.Parse(txtTotal.Text.Split(' ')[1].Replace('.', ',')) > double.Parse(txtPago.Text))
                { MessageBox.Show("El pago debe ser mayor o igual al total"); return false; }
                else { txtCambio.Text = "Cambio: " + (double.Parse(txtPago.Text) - double.Parse(txtTotal.Text.Split(' ')[1].Replace('.', ','))).ToString("F2"); return true; }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return false; }
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (int.Parse(txtCantidad.Text) > 0)
                {                 
                    caja = new CsCaja(int.Parse(txtCantidad.Text), txtIdProducto.Text);
                    string idlote = caja.descontar();
                    double total = int.Parse(txtCantidad.Text) * double.Parse(txtPrecio.Text.Replace('.', ','));
                    if (idlote!=null)
                    {
                        movimientos.AccionForms("Registro una venta");
                        dgvDetallesVentas.Rows.Add(idlote,txtIdProducto.Text.Trim(), txtNombreProducto.Text.Trim(), txtCantidad.Text.Trim(), txtPrecio.Text.Replace(',', '.').Trim(), total);
                        txtIdProducto.Clear();
                        txtNombreProducto.Clear();
                        txtPrecio.Clear();
                        txtCantidad.Clear();
                        llenartxttex();
                    }
                }
                else MessageBox.Show("Ingrese una cantidad mayor a 0");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
        }
        private void btnBuscarProducto_Click(object sender, EventArgs e)
        {
            fmGestionarMedicina medicina = new fmGestionarMedicina(1);
            this.AddOwnedForm(medicina);
            medicina.ShowDialog();
        }
        private void btnCliente_Click(object sender, EventArgs e)
        {
            fmGestionarClientes Clientes = new fmGestionarClientes(1);
            this.AddOwnedForm(Clientes);
            Clientes.ShowDialog();
        }
        private void fmCaja_Load(object sender, EventArgs e)
        {
            txtFecha.Text = DateTime.Now.ToString().Split(' ')[0];
            txtidEmpleado.Text = movimientos.ID();
            txtFecha.Enabled = false;
            txtidEmpleado.Enabled = false;
            txtIdProducto.Enabled = false;
            txtNombreProducto.Enabled = false;
            txtPrecio.Enabled = false;
            txtSubtotal.Enabled = false;
            txtIva.Enabled = false;
            txtTotal.Enabled = false;
            txtCambio.Enabled = false;          
        }
        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 && e.KeyChar <= 47) || (e.KeyChar >= 58 && e.KeyChar <= 255)) e.Handled = true;
        }
        private void btnGenerarFactura_Click(object sender, EventArgs e)
        {
            if (com())
            {
                caja = new CsCaja(txtIdCliente.Text, double.Parse(txtTotal.Text.Split(' ')[1]));
                if (caja.Generarfactura(dgvDetallesVentas))
                {
                    MessageBox.Show("Factura realizada");
                    movimientos.AccionForms("Se realizo una factura");
                    dgvDetallesVentas.Rows.Clear();
                    caja.InsertarIngresoCaja(txtTotal.Text.Split(' ')[1]);
                    txtSubtotal.Clear();txtIva.Clear(); txtTotal.Clear(); txtCambio.Clear(); txtPago.Clear();
                }
                else MessageBox.Show("A ocurrido un error");
            }
        }     
        private void btnSubtotal_Click(object sender, EventArgs e)
        {
            if (dgvDetallesVentas.Rows.Count > 0)
                if (txtPago.Text.Length > 0) com();
                else MessageBox.Show("Ingrese el pago del cliente");
            else MessageBox.Show("Primero genere una venta");
        }
        private void txtPago_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 && e.KeyChar <= 47) || (e.KeyChar >= 58 && e.KeyChar <= 255)) e.Handled = true;
        }
        private void btnCerrarCaja_Click(object sender, EventArgs e)
        {
            caja = new CsCaja();
            caja.CerrarCaja();
            this.Close();
        }
        private void dgvDetallesVentas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            if(txtCambio.Text.Length == 0)
            if (dgvDetallesVentas.Columns[e.ColumnIndex]== dgvDetallesVentas.Columns[dgvDetallesVentas.Columns.Count-1])
            {
                DialogResult res = MessageBox.Show("¿Esta seguro de que desea eliminar esta venta?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    caja = new CsCaja(int.Parse(dgvDetallesVentas[3, e.RowIndex].Value.ToString()), dgvDetallesVentas[0, e.RowIndex].Value.ToString());
                    caja.DevolverProducto("NO");                    
                    dgvDetallesVentas.Rows.Remove(dgvDetallesVentas.CurrentRow);
                    movimientos.AccionForms("Se elimino una venta");
                    llenartxttex();
                }
            }
        }
    }
}
