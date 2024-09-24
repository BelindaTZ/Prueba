using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BYTEAZULPROFESIONAL
{
    public partial class fmCrearCuentas : Form
    {
        csEmpleados empleados;
        csEncriptarMd5 md5;
        string idEmpleado, clavecirp;
        public fmCrearCuentas()
        {
            InitializeComponent();
            txtContraseña.Enabled = false;
            txtConfirmarContraseña.Enabled = false;
        }

        void Limpiar()
        {
            txtContraseña.Enabled = false;
            txtContraseña.Clear();
            txtConfirmarContraseña.Enabled = false;
            txtConfirmarContraseña.Clear();
            txtusuario.Clear();
        }

        private void txtusuario_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                empleados = new csEmpleados();
                idEmpleado = empleados.VerificarUsuario(txtusuario.Text);
                if (idEmpleado != null)
                {
                    if (!empleados.VerificarCuenta(idEmpleado))
                    {
                        txtContraseña.Enabled = true;
                        txtConfirmarContraseña.Enabled = true;
                        txtContraseña.BorderStyle = BorderStyle.Fixed3D;
                        txtConfirmarContraseña.BorderStyle = BorderStyle.Fixed3D;
                    }
                    else { MessageBox.Show("Ya tiene una cuenta registrada", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning); Limpiar(); }

                    this.SelectNextControl((Control)sender, true, true, true, true);
                }
                else
                    MessageBox.Show("No se encuentra registrado en el sistema o credenciales incorrectas.");
            }
        }

        private void btnCrearCuentaEmpleado_Click(object sender, EventArgs e)
        {
            crearcuenta();
        }

        private void txtConfirmarContraseña_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                crearcuenta();
        }

        private void txtContraseña_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }

        }

        public void crearcuenta()
        {
            empleados = new csEmpleados();
            md5 = new csEncriptarMd5();

            if (!string.IsNullOrWhiteSpace(txtContraseña.Text) && txtContraseña.Text.Length >= 5)
            {
                if(txtContraseña.Text == txtConfirmarContraseña.Text)
                {
                    clavecirp = md5.Encriptar(txtContraseña.Text, txtusuario.Text);
                    char[] aux = clavecirp.ToCharArray();
                    Array.Reverse(aux);
                    //MessageBox.Show(new string(aux));

                    if (empleados.InsertarAcceso(idEmpleado, (new string(aux))))
                    {
                        MessageBox.Show("Cuenta creada exitosamente");
                        Limpiar();
                        this.Close();
                    }
                } 
                else
                    MessageBox.Show("Las contraseñas difieren","Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning); 
            }
            else
                MessageBox.Show("El campo contraseña no puede estar vacio ni ser menor a 5 caracteres");
        }
    }
}
