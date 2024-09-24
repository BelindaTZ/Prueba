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
    public partial class fmAbrirCaja : Form
    {
        CsMovimientos movimientos;
        CsCaja caja;
        public fmAbrirCaja()
        {
            InitializeComponent();
            movimientos = new CsMovimientos();
            caja = new CsCaja();
        }

        private void btnCancelar_Click(object sender, EventArgs e) { this.Close(); }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            Confirmar();
        }
        void Confirmar()
        {
            FmMenu menu = Owner as FmMenu;

            if (!string.IsNullOrWhiteSpace(txtSueldoInicial.Text) && decimal.TryParse(txtSueldoInicial.Text, out decimal sueldo) && sueldo > 0)
            {
                caja.AbrirCaja(txtSueldoInicial.Text, movimientos.ID());
                menu.AbrirCaja = true;
                this.Close();
            }
            else
                MessageBox.Show("valor no valido", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void txtSueldoInicial_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir solo números, el punto decimal, y la tecla Backspace
            if ((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == '.' || e.KeyChar == (char)Keys.Back)
            {
                if (e.KeyChar == '.' && ((TextBox)sender).Text.Contains('.'))
                    e.Handled = true;
            }
            else
                e.Handled = true;
        }

        private void txtSueldoInicial_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Text.Contains('.'))
            {
                string[] partes = textBox.Text.Split('.');
                if (partes.Length > 1 && partes[1].Length > 2)
                {
                    textBox.Text = partes[0] + "." + partes[1].Substring(0, 2);
                    textBox.SelectionStart = textBox.Text.Length;
                }
            }
        }

        private void txtSueldoInicial_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Confirmar();
            }
        }
    }
}
