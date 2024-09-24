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
    public partial class fmGestionarLotes : Form
    {
        CsLotes lotes = new CsLotes();
        public fmGestionarLotes()
        {
            InitializeComponent();
        }

        private void fmGestionarLotes_Load(object sender, EventArgs e)
        {
            lotes.ActualizarDisponiblidad();
            Mostrar();
            CsAuxiliar auxiliar = new CsAuxiliar();
            fmAgregarLotes comprasimg = new fmAgregarLotes();
            this.AddOwnedForm(comprasimg);
            auxiliar.AgregarBoton(dgvVerLotes, comprasimg.imlLogo.Images[3], "Available");
        }
        private void Mostrar()
        {
            DataTable ds = lotes.TablaLotes();
            dgvVerLotes.DataSource = ds;
            dgvVerLotes.AllowUserToAddRows = false;
            dgvVerLotes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        void Buscar()
        {
            lotes = new CsLotes();
            if (!string.IsNullOrWhiteSpace(txtBuscar.Text))
            {
                DataTable ds = lotes.TablaLotes(txtBuscar.Text.Trim());

                dgvVerLotes.DataSource = ds;
                dgvVerLotes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            else
                Mostrar();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Buscar();
        }

        private void dgvVerLotes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                Buscar();
            }
        }

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                Buscar();
            }
        }

        private void dgvVerLotes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvVerLotes.Columns["Available"].Index && e.RowIndex >= 0)
            {
                DataGridViewRow filaSeleccionada = dgvVerLotes.SelectedRows[0];
                string idlote = filaSeleccionada.Cells["ID del Lote"].Value.ToString();

                if (filaSeleccionada.Cells["Estado"].Value.ToString() == "Disponible")
                {
                    DialogResult res = MessageBox.Show("¿Esta seguro de que desea modificar la disponibilidad del lote?", "Modificar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (res == DialogResult.Yes)
                    {
                        if (lotes.CambiarDisponiblidad(idlote))
                            Mostrar();
                    }
                }
                else
                    MessageBox.Show("El lote ya no se encuentra Disponible");
            }
        }
    }
}
