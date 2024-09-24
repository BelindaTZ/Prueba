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
    public partial class fmMovimientos : Form
    {
        CsMovimientos movimientos;
        int y = 0, yy = 0;
        public fmMovimientos()
        {
            InitializeComponent();
        }
        public fmMovimientos(int z, int cc)
        {
            InitializeComponent();
            y = z;
        }
        //public int Y { get { return y; } set { y = value; } }
        public int YY { get { return yy; } set { yy = value; } }
        public fmMovimientos(int a)
        {
            InitializeComponent();
            y = a;
        }
        private void BUSCAR()
        {
            if (y == 1)
            {
                movimientos = new CsMovimientos();
            
                if (txtBuscar.Text.Length > 0)
                {
                    DataTable MoviP = movimientos.BuscarProduMovi(txtBuscar.Text);
                    dgvMovimientos.DataSource = MoviP;
                }
                else
                {
                    DataTable MoviP = movimientos.BuscarProduMovi("1");
                    dgvMovimientos.DataSource = MoviP;
                }
            }
            else if(y == 2)
            {
                imgMovimientosCaja.Visible = true;
                movimientos = new CsMovimientos();
                if (txtBuscar.Text.Length > 0)
                {
                    DataTable dt = movimientos.TablaCaja(txtBuscar.Text);
                    dgvMovimientos.DataSource = dt;
                }
                else
                {
                    DataTable dt = movimientos.TablaCaja();
                    dgvMovimientos.DataSource = dt;
                }
            }
            else if(y == 3)
            {
                imgTransaccionesCja.Visible = true;
                movimientos = new CsMovimientos();
                if (txtBuscar.Text.Length > 0)
                {
                    DataTable dt = movimientos.TablaTransacciones(txtBuscar.Text);
                    dgvMovimientos.DataSource = dt;
                }
                else
                {
                    DataTable dt = movimientos.TablaTransacciones();
                    dgvMovimientos.DataSource = dt;
                }
            }
            else
            {
                movimientos = new CsMovimientos();
                if (txtBuscar.Text.Length > 0)
                {
                    DataTable dt = movimientos.BuscarMovimientos(txtBuscar.Text);
                    dgvMovimientos.DataSource = dt;
                }
                else
                {
                    DataTable dt = movimientos.Buscar();
                    dgvMovimientos.DataSource = dt;
                }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            fmGestionarMedicina SelectProdu = new fmGestionarMedicina(2,2);
            this.AddOwnedForm(SelectProdu);
            SelectProdu.ShowDialog();
            movimientos = new CsMovimientos();
            DataTable MoviP = movimientos.BuscarProduMoviSelector(yy.ToString());
            dgvMovimientos.DataSource = MoviP;
        }

        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            BUSCAR();
        }

        private void fmMovimientos_Load(object sender, EventArgs e)
        {
            BUSCAR();
            dgvMovimientos.ReadOnly = true;
            dgvMovimientos.AllowUserToAddRows = false;
            dgvMovimientos.RowHeadersVisible = false;

            dgvMovimientos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMovimientos.MultiSelect = false;
            dgvMovimientos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
    }
}
