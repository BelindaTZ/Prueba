using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BYTEAZULPROFESIONAL
{
    public partial class fmAgregarLotes : Form
    {
        CsLotes lotes;
        CsCompras compras;
        CsCaja caja;
        public string NombreProvedor;
        static decimal precio, cantidad, totalConIVA, totalSinIVA, iva;
        int ClientexPag = 40, Bandera = 0;
        public fmAgregarLotes()
        {
            InitializeComponent();
            lotes = new CsLotes();
            compras = new CsCompras();
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void btnBuscarIDProducto_Click(object sender, EventArgs e)
        {
            fmGestionarMedicina medicinas = new fmGestionarMedicina(2);
            this.AddOwnedForm(medicinas);
            medicinas.ShowDialog();
        }

        private void btnBuscarIDProveedor_Click(object sender, EventArgs e)
        {
            fmGestionarProveedores prove = new fmGestionarProveedores(2);
            this.AddOwnedForm(prove);
            prove.ShowDialog();
            
        }

        private void btnConfirmarCompra_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtPrecioSubtotal.Text) || !string.IsNullOrWhiteSpace(txtTotalIva.Text))
            {
                caja = new CsCaja();
                //if para verificar el saldo actual y no permitir una egreso q supere el saldo de caja
                decimal saldoactual = caja.ObtenerSaldoIncial(caja.IDCaja()) + caja.ObtenerTotalIngresos(caja.IDCaja());
                decimal totalPagar;
                if (!decimal.TryParse(txtTotalPagar.Text, out totalPagar))
                    return;

                if (saldoactual > totalPagar)
                {
                    if(compras.IngresarCompra(txtProveedor.Text, txtEmpleado.Text, decimal.Parse(txtTotalPagar.Text)))
                    foreach (DataGridViewRow row in dgvCompra.Rows)
                    {
                        lotes.IngresarLotes(row.Cells[0].Value.ToString(), row.Cells[2].Value.ToString(), DateTime.Parse(row.Cells[3].Value.ToString()), int.Parse(row.Cells[4].Value.ToString()), decimal.Parse(row.Cells[5].Value.ToString()), "Compra", "Disponible");
                        compras.IngresarDetallesCompra(compras.IDCompra(), row.Cells[0].Value.ToString(), int.Parse(row.Cells[4].Value.ToString()), decimal.Parse(row.Cells[5].Value.ToString()), decimal.Parse(row.Cells[7].Value.ToString()));
                        caja.InsertarEgresoCaja(row.Cells[7].Value.ToString());
                    }
                    MessageBox.Show("El/Los nuevos lotes se ingresaron correctamente");
                    prtImprimir = new PrintDocument();
                    PrinterSettings ps = new PrinterSettings();
                    prtImprimir.PrinterSettings = ps;
                    prtImprimir.PrintPage += Imprimir;
                    prtImprimir.Print();
                    limpiar2(); dgvCompra.Rows.Clear();
                }
                else
                {
                    MessageBox.Show("Valor de la compra mayor al saldo actual de caja", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Limpiar();
                    limpiar2(); dgvCompra.Rows.Clear();
                }
            }
        }
        void limpiar2()
        {
            txtPrecioSubtotal.Clear();
            txtTotalIva.Clear();
            txtTotalPagar.Clear();
            txtProveedor.Clear();
        }
        private void fmAgregarLotes_Load(object sender, EventArgs e)
        {
            CsMovimientos movimientos = new CsMovimientos();
            CsAuxiliar auxiliar = new CsAuxiliar();
            txtEmpleado.Text = movimientos.ID();
            dgvCompra.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtpFechaCaducidad.Value = DateTime.Now.AddMonths(6);

            auxiliar.AgregarBoton(dgvCompra, imlLogo.Images[1], "Borrar");
        }

        private void dtpFechaCaducidad_ValueChanged(object sender, EventArgs e)
        {
            if (dtpFechaCaducidad.Value < DateTime.Now.AddMonths(6))
            {
                MessageBox.Show("La fecha de caducidad debe ser al menos 6 meses después de la fecha de entrada.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpFechaCaducidad.Value = DateTime.Now.AddMonths(6);
            }
        }

        private void dgvCompra_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvCompra.Columns["Borrar"].Index && e.RowIndex >= 0)
            {
                DialogResult res = MessageBox.Show("¿Estás seguro de que deseas eliminar este producto de la compra?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (res == DialogResult.Yes)
                    dgvCompra.Rows.RemoveAt(e.RowIndex);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if(Validar())
            {
                precio = decimal.Parse(txtPrecio.Text);
                cantidad = decimal.Parse(txtCantidad.Text);
                iva = decimal.Parse(txtIVA.Text);

                totalConIVA = precio * cantidad * (1 + iva);
                totalSinIVA = precio * cantidad; // Total sin IVA

                dgvCompra.Rows.Add(txtIDProducto.Text, txtNombreProducto.Text, txtProveedor.Text, dtpFechaCaducidad.Value, txtCantidad.Text, txtPrecio.Text ,totalSinIVA.ToString(), totalConIVA.ToString("F2"));
                Limpiar();
            }
        }
        public void Limpiar()
        {
            txtIDProducto.Clear();
            txtCantidad.Clear();
            txtNombreProducto.Clear();
            txtPrecio.Clear();
            txtIVA.Clear();
        }
        public bool Validar()
        {
            if (!string.IsNullOrWhiteSpace(txtIDProducto.Text) && !string.IsNullOrWhiteSpace(txtProveedor.Text) && !string.IsNullOrWhiteSpace(txtCantidad.Text))
                return true;
            else
            {
                MessageBox.Show("No debe haber campos vacios", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        private void btnSubtotal_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvCompra.Rows.Count > 0)
                {
                    decimal subtotalSinIVA = 0;
                    decimal subtotalConIVA = 0;

                    decimal totalSinIVA;
                    decimal totalConIVA;

                    foreach (DataGridViewRow row in dgvCompra.Rows)
                    {
                        if (decimal.TryParse(row.Cells[6].Value.ToString(), out totalSinIVA) &&
                                decimal.TryParse(row.Cells[7].Value.ToString(), out totalConIVA))
                        {
                            subtotalSinIVA += totalSinIVA;
                            subtotalConIVA += totalConIVA;
                        }
                    }
                    txtPrecioSubtotal.Text = subtotalSinIVA.ToString();
                    txtTotalIva.Text = (subtotalConIVA - subtotalSinIVA).ToString();
                    txtTotalPagar.Text = subtotalConIVA.ToString();
                }
                else
                    MessageBox.Show("No hay valores de compra", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void Imprimir(object sender, PrintPageEventArgs e)
        {
            int y = 260;
            string nombreProveedor = NombreProvedor;

            e.Graphics.DrawImage(imlLogo.Images[0], new RectangleF(100, 0, imlLogo.Images[0].Width / 2, imlLogo.Images[0].Height / 2));
            Font fuente = new Font("Arial", 20, FontStyle.Bold);
            e.Graphics.DrawString("BYTE AZUL", fuente, Brushes.BlueViolet, new RectangleF(300, 20, 300, 40));
            fuente = new Font("Arial", 16, FontStyle.Bold);
            e.Graphics.DrawString("FACTURA DE BYTE AZUL", fuente, Brushes.Red, new RectangleF(300, 70, 300, 35));
            fuente = new Font("Arial", 12, FontStyle.Bold);
            e.Graphics.DrawString("Fecha: " + DateTime.Now.ToString("dd/MM/yyyy"), fuente, Brushes.Black, new RectangleF(500, 130, 160, 20)); // Moved to the right
            e.Graphics.DrawString("Hora: " + DateTime.Now.ToString("HH:mm:ss"), fuente, Brushes.Black, new RectangleF(500, 150, 160, 20)); // Moved to the right
            e.Graphics.DrawString("Sucursal: MATRIZ BYTE AZUL", fuente, Brushes.Black, new RectangleF(0, 130, 300, 20));
            e.Graphics.DrawString("Número de factura: " + compras.NumFactura(), fuente, Brushes.Black, new RectangleF(0, 150, 300, 20)); // Adjusted position
            e.Graphics.DrawString("Empleado: " + txtEmpleado.Text.Trim(), fuente, Brushes.Black, new RectangleF(0, 170, 300, 20)); // Adjusted position
            e.Graphics.DrawString("Proveedor: " + nombreProveedor, fuente, Brushes.Black, new RectangleF(0, 190, 300, 20)); // Adjusted position

            e.Graphics.DrawString("Num.", fuente, Brushes.Black, new RectangleF(0, 230, 40, 20));
            e.Graphics.DrawString("ID Producto", fuente, Brushes.Black, new RectangleF(60, 230, 75, 20));
            e.Graphics.DrawString("Nombre Producto", fuente, Brushes.Black, new RectangleF(200, 230, 300, 20));
            e.Graphics.DrawString("Cantidad", fuente, Brushes.Black, new RectangleF(360, 230, 150, 20));
            e.Graphics.DrawString("Precio Unitario", fuente, Brushes.Black, new RectangleF(500, 230, 150, 20));
            e.Graphics.DrawString("Precio Total", fuente, Brushes.Black, new RectangleF(669, 230, 150, 20));

            e.Graphics.DrawLine(new Pen(Color.Black, 1), 0, 250, 800, 250);

            fuente = new Font("Arial", 12, FontStyle.Regular);
            for (int i = 0; Bandera < dgvCompra.Rows.Count && i < ClientexPag; i++, Bandera++)
            {
                e.Graphics.DrawString((Bandera + 1).ToString(), fuente, Brushes.Black, new RectangleF(0, y, 40, 20));
                e.Graphics.DrawString(dgvCompra[0, Bandera].Value.ToString().Trim(), fuente, Brushes.Black, new RectangleF(60, y, 75, 20));
                e.Graphics.DrawString(dgvCompra[1, Bandera].Value.ToString().Trim(), fuente, Brushes.Black, new RectangleF(200, y, 300, 20));
                e.Graphics.DrawString(dgvCompra[4, Bandera].Value.ToString().Trim(), fuente, Brushes.Black, new RectangleF(380, y, 150, 20));
                e.Graphics.DrawString(dgvCompra[5, Bandera].Value.ToString().Trim(), fuente, Brushes.Black, new RectangleF(515, y, 150, 20));
                e.Graphics.DrawString(dgvCompra[7, Bandera].Value.ToString().Trim(), fuente, Brushes.Black, new RectangleF(684, y, 150, 20));
                y += 20;
            }

            e.HasMorePages = Bandera < dgvCompra.Rows.Count;

            e.Graphics.DrawString("Subtotal: " + txtPrecioSubtotal.Text, fuente, Brushes.Black, new RectangleF(500, y + 20, 150, 20));
            e.Graphics.DrawString("IVA: " + txtTotalIva.Text, fuente, Brushes.Black, new RectangleF(500, y + 40, 150, 20));
            e.Graphics.DrawString("Total a pagar: $" + txtTotalPagar.Text, fuente, Brushes.Black, new RectangleF(500, y + 60, 150, 20));
        }
    }
}
