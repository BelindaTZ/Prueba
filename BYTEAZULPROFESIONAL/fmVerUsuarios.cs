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
    public partial class fmVerUsuarios : Form
    {
        csEmpleados empleados;
        public fmVerUsuarios()
        {
            InitializeComponent();
        }
        private void BUSCAR()
        {
            empleados = new csEmpleados();
            if (txtBuscar.Text.Length > 0)
            {
                DataTable dt = empleados.BuscarUsuario(txtBuscar.Text);
                dgvVerUsuarios.DataSource = dt;
            }
            else
            {
                DataTable dt = empleados.Buscar();
                dgvVerUsuarios.DataSource = dt;
            }
            dgvVerUsuarios.ReadOnly = true;
            dgvVerUsuarios.AllowUserToAddRows = false;
            dgvVerUsuarios.RowHeadersVisible = false;

            dgvVerUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvVerUsuarios.MultiSelect = false;
            dgvVerUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        private void fmVerUsuarios_Load(object sender, EventArgs e)
        {
            BUSCAR(); 
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            BUSCAR();
        }
        private void txtBuscar_KeyUp(object sender, KeyEventArgs e)
        {
            BUSCAR();
        }
    }
}
