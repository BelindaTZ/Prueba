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
    public partial class fmCambiarContrasena : Form
    {
        csEmpleados empleados;
        CsMovimientos movimientos;
        csEncriptarMd5 md5; 
        string idempleado;

        public fmCambiarContrasena()
        {
            InitializeComponent();
            empleados = new csEmpleados();
            md5 = new csEncriptarMd5();
            movimientos = new CsMovimientos();
        }

        private void btnCancelar_Click(object sender, EventArgs e) { this.Close(); }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            CambiarContra();
        }
        void VerificarContra()
        {
            idempleado = movimientos.ID();
            if (idempleado != null)
            {
                if (empleados.ConfirmarContraActual(idempleado, EncriptarVoltear(txtContraActual.Text)))
                {
                    txtNuevaContra.Enabled = true; txtNuevaContra.BorderStyle = BorderStyle.Fixed3D;
                    txtConfirmarContra.Enabled = true; txtConfirmarContra.BorderStyle = BorderStyle.Fixed3D;
                    btnConfirmar.Enabled = true; txtNuevaContra.Focus();
                }
                else { MessageBox.Show("Contraseña Actual Incorrecta", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
        }
        void CambiarContra()
        {
            if (!string.IsNullOrWhiteSpace(txtNuevaContra.Text) && !string.IsNullOrWhiteSpace(txtConfirmarContra.Text) && txtNuevaContra.Text.Length >= 5)
            {
                if (txtNuevaContra.Text == txtConfirmarContra.Text)
                {
                    empleados.CambiarContraseña(idempleado, EncriptarVoltear(txtNuevaContra.Text));
                    limpiar(); this.Close();
                }
                else
                    MessageBox.Show("Las contraseñas difieren", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else { MessageBox.Show("La nueva contraseña no puede ser menor a 5 caracteres", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }
        void limpiar()
        {
            txtNuevaContra.Clear();
            txtConfirmarContra.Clear();
            txtContraActual.Clear();
        }

        string EncriptarVoltear(string clave)
        {
            string clavecirp = md5.Encriptar(clave, empleados.SacarIdentificador());
            char[] aux = clavecirp.ToCharArray();
            Array.Reverse(aux);
            return new string(aux);
        }

        private void fmCambiarContrasena_Load(object sender, EventArgs e)
        {
            txtNuevaContra.Enabled = false;
            txtConfirmarContra.Enabled = false;
            btnConfirmar.Enabled = false;
        }

        private void txtContraActual_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                VerificarContra();
        }

        private void txtNuevaContra_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
                this.SelectNextControl((Control)sender, true, true, true, true);
        }

        private void txtConfirmarContra_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                CambiarContra();
        }
    }
}
