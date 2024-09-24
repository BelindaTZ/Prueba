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
    public partial class fmVentas : Form
    {
        CsConexion conexion;
        CsCaja Caja;
        string idventa = "";
        public fmVentas()
        {
            InitializeComponent();
        }
        public fmVentas(string venta)
        {
            InitializeComponent();
            idventa = venta;
        }
        private void AgregarColumnas(string name,string text)
        {
            // Agregar columna con botones
            DataGridViewButtonColumn btnColumn = new DataGridViewButtonColumn();
            btnColumn.Name = name;
            btnColumn.HeaderText = name;
            btnColumn.Text = text;  // Texto del botón
            btnColumn.UseColumnTextForButtonValue = true; // Usar el mismo texto para todos los botones
            // Agregar la columna de botones al DataGridView
            dgvVentas.Columns.Add(btnColumn);
        }
        private void fmVentas_Load(object sender, EventArgs e)
        {
            BUSCAR();
            dgvVentas.ReadOnly = true;
            dgvVentas.AllowUserToAddRows = false;
            dgvVentas.RowHeadersVisible = false;

            dgvVentas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvVentas.MultiSelect = false;
            dgvVentas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            if (idventa == "") AgregarColumnas("Detalles Ventas", "Detalles");
            else
            {
                btnRegresar.Enabled = true;
                btnRegresar.Visible = true;
                AgregarColumnas("Devolucion", "Devolver");
            }             
        }
       
        private void BUSCAR()
        {
            Caja = new CsCaja();
            if(idventa=="")
            if(txtBuscar.Text.Length > 0 )
            {
                DataTable dt= Caja.BuscarVentas(txtBuscar.Text);
                dgvVentas.DataSource = dt;
            }
            else
            {
                DataTable dt = Caja.Buscar();
                dgvVentas.DataSource = dt;
            }
            else if(txtBuscar.Text.Length>0)
            {
                DataTable dt = Caja.BuscarDetallesVentas(idventa,txtBuscar.Text);
                dgvVentas.DataSource = dt;
            }
            else
            {
                DataTable dt = Caja.BuscarDetalles(idventa);
                dgvVentas.DataSource = dt;
            }
        }
        private void btnBuscar_Click(object sender, EventArgs e) { BUSCAR(); }
        
        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e) { BUSCAR(); }

        private void dgvVentas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (idventa == "")
            {
                if (e.ColumnIndex == dgvVentas.Columns["Detalles Ventas"].Index && e.RowIndex >= 0)
                {
                    DialogResult res = MessageBox.Show("¿Esta seguro de que desea ver los detalles de esta venta?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (res == DialogResult.Yes)
                    {
                        try
                        {
                            fmVentas ventas = new fmVentas(dgvVentas["ID", e.RowIndex].Value.ToString().Trim());
                            ventas.ShowDialog();
                        }
                        catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
                    }
                }
            }
            else
            {
                if (e.ColumnIndex == dgvVentas.Columns["Devolucion"].Index && e.RowIndex >= 0)
                {
                    if (dgvVentas["Estado", e.RowIndex].Value.ToString() != "Devuelto")
                    { 
                        DialogResult res = MessageBox.Show("¿Esta seguro de que devolver esta venta?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (res == DialogResult.Yes)
                    {
                        try
                        {
                            
                                Caja = new CsCaja(int.Parse(dgvVentas["Cantidad", e.RowIndex].Value.ToString()), dgvVentas["ID Lote", e.RowIndex].Value.ToString());
                                if (Caja.DevolverProducto(dgvVentas["ID", e.RowIndex].Value.ToString())) MessageBox.Show("Producto devuelto");
                           
                            BUSCAR();
                        }
                        catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
                    }
                }
                else MessageBox.Show("Este producto ya se ah devuelto");
            }
            }
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            btnRegresar.Enabled = false;
            btnRegresar.Visible = false;
            this.Hide();
        }
    }
}
