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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BYTEAZULPROFESIONAL
{
    public partial class fmLogin : Form
    {
        CsConexion conexion = new CsConexion();
        csEncriptarMd5 md5 = new csEncriptarMd5();
        csEmpleados empleados;
        public fmLogin()
        {
            InitializeComponent();
        }
        private void ingresar()
        {
            empleados = new csEmpleados();

            try
            {
                string clavecirp = md5.Encriptar(txtPassword.Text, txtUsuario.Text);
                char[] aux = clavecirp.ToCharArray();
                Array.Reverse(aux);
                //Cambiar new string (aux) POR txtPassword.Text para ingresar un usuario con clave cifrada
                if (empleados.Ingreso(txtUsuario.Text, new string (aux)))
                {
                    FmMenu menu = new FmMenu();
                    if (empleados.Estado == "Activo")
                    {
                        if (empleados.Cargo == "Administrador")
                        { }
                        else if (empleados.Cargo == "Gerente")
                        {
                            menu.btnAdministracion.Enabled = false;
                            menu.btnAdministracion.Visible = false;
                        }
                        else
                        {
                            menu.btnEmpleados.Enabled = false;
                            menu.btnEmpleados.Visible = false;
                            menu.btnLotes.Enabled = false;
                            menu.btnLotes.Visible = false;
                            menu.btnMedicinas.Enabled = false;
                            menu.btnMedicinas.Visible = false;
                            menu.btnProveedores.Enabled = false;
                            menu.btnProveedores.Visible = false;
                            menu.btnReportes.Enabled = false;
                            menu.btnReportes.Visible = false;
                            menu.btnAdministracion.Enabled = false;
                            menu.btnAdministracion.Visible = false;
                        }
                        CsMovimientos movimientos = new CsMovimientos();
                        movimientos.Accion("Inicio sesion", empleados.IdEmpleado.Trim());
                        this.Hide();
                        menu.ShowDialog();
                    }
                    else MessageBox.Show("El usuario no tiene acceso");
                }
                else { MessageBox.Show("Credenciales incorrectas"); }
            }
            catch (Exception ex) { }
        }
        private void btnIngresar_Click(object sender, EventArgs e)
        {
            ingresar();  
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Enter) {txtPassword.Focus();}
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) ingresar();
        }

        private void btnVer_Click(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = false;
            btnOcultar.Visible = true;
            btnVer.Visible = false;
        }

        private void btnOcultar_Click(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar=true;
            btnVer.Visible = true;
            btnOcultar.Visible = false;
        }
    }
}
