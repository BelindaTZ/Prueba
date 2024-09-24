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
    public partial class fmAgregarEmpleados : Form
    {
        CsConexion conexion;
        csEmpleados empleados;
        string id;
        bool modificar;
        public fmAgregarEmpleados()
        {
            InitializeComponent();
            cmbEstado.SelectedIndex = 0;
        }
        public fmAgregarEmpleados(string nombre, string apellido, string identificacion, DateTime fechanacimiento, string genero, string cargoo,
            DateTime fechaingreso, string correo, string direccion, string celular, string estado)
        {
            InitializeComponent();

            txtNombres.Text = nombre;
            txtApellidos.Text = apellido;
            txtIdentificacion.Text = identificacion;
            dtpFechaNacimiento.Value = fechanacimiento;
            dtpFechaIngreso.Value = fechaingreso;
            txtCelular.Text = celular;
            txtCorreo.Text = correo;
            txtDireccion.Text = direccion;

            if (cargoo == "Administrador")
                cmbCargo.SelectedIndex = 0;
            else if (cargoo == "Gerente")
                cmbCargo.SelectedIndex = 1;
            else if (cargoo == "Empleado")
                cmbCargo.SelectedIndex = 2;
            else
                cmbCargo.SelectedIndex = 3;

            if (genero == "Femenino")
                cmbGenero.SelectedIndex = 0;
            else if (genero == "Masculino")
                cmbGenero.SelectedIndex = 1;
            else
                cmbGenero.SelectedIndex = 2;

            if (estado == "Activo")
                cmbEstado.SelectedIndex = 0;
            else
                cmbEstado.SelectedIndex = 1;

            dtpFechaIngreso.Enabled = false;
            txtNombres.Enabled = false;
            txtIdentificacion.Enabled = false;
            txtApellidos.Enabled = false;
            dtpFechaIngreso.Enabled = false;
            dtpFechaNacimiento.Enabled = false;
            cmbGenero.Enabled = false;

            modificar = true;
        }

        private void fmAgregarEmpleados_Load(object sender, EventArgs e)
        {
            dtpFechaIngreso.Value = DateTime.Now;
            dtpFechaIngreso.Enabled = false;
            dtpFechaNacimiento.MaxDate  = DateTime.Now.AddYears(-18);
            dtpFechaNacimiento.MinDate = DateTime.Now.AddYears(-68);
        }

        private void btnAgregarEmpleados_Click(object sender, EventArgs e)
        {
            if (Validar() && validarIdentificador(txtIdentificacion.Text))
            {
                empleados = new csEmpleados();

                if (empleados.VerificarIdentBD(txtIdentificacion.Text))
                {
                    if (empleados.InsertarEmpleado(txtNombres.Text.Trim(), txtApellidos.Text.Trim(), txtIdentificacion.Text.Trim(), dtpFechaNacimiento.Value, cmbGenero.Text.Trim(), cmbCargo.Text,
                        dtpFechaIngreso.Value, txtCorreo.Text.Trim(), txtDireccion.Text.Trim(), txtCelular.Text.Trim(), cmbEstado.Text))

                    Limpiar();
                }
                else
                    MessageBox.Show("Identificación en uso", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnModificarEmpleados_Click(object sender, EventArgs e)
        {
            if (modificar && Validar())
            {
                empleados = new csEmpleados();

                if (empleados.ModificarEmpleado(txtDireccion.Text.Trim(), cmbCargo.Text, txtCelular.Text.Trim(), txtCorreo.Text.Trim(), cmbEstado.Text, txtIdentificacion.Text))
                {
                    modificar = false;
                    Limpiar();

                    txtNombres.Enabled = true; txtApellidos.Enabled = true; txtIdentificacion.Enabled = true; txtCorreo.Enabled = true; txtCelular.Enabled = true; cmbCargo.Enabled = true;
                    cmbGenero.Enabled = true; cmbEstado.Enabled = true; dtpFechaNacimiento.Enabled = true; txtDireccion.Enabled = true; this.Close();
                }
            }
        }
        void Limpiar()
        {
            txtNombres.Clear();
            txtApellidos.Clear();
            txtIdentificacion.Clear();
            dtpFechaNacimiento.Value = new DateTime(2006, 5, 20);
            dtpFechaIngreso.Value = dtpFechaIngreso.MaxDate;
            txtCelular.Clear();
            txtCorreo.Clear();
            txtDireccion.Clear();

            cmbCargo.SelectedIndex = -1;
            cmbGenero.SelectedIndex = -1;
            cmbEstado.SelectedIndex = -1;
        }
        bool Validar()
        {
            if (string.IsNullOrWhiteSpace(txtNombres.Text) || string.IsNullOrWhiteSpace(txtApellidos.Text) || string.IsNullOrWhiteSpace(txtIdentificacion.Text)
                || string.IsNullOrWhiteSpace(txtCelular.Text) || string.IsNullOrWhiteSpace(txtCorreo.Text) || string.IsNullOrWhiteSpace(txtDireccion.Text)
                || string.IsNullOrWhiteSpace(cmbGenero.Text) || string.IsNullOrWhiteSpace(cmbCargo.Text) || string.IsNullOrWhiteSpace(cmbEstado.Text))
            {
                MessageBox.Show("Ingrese todos los valores", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!EsCorreoValido(txtCorreo.Text))
            {
                MessageBox.Show("Correo no valido", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        bool validarIdentificador(string identificador)
        {
            if (EsCedulaValida(identificador))
                return true;
            else if (ValidarPasaporte(identificador))
                return true;
            else if (ValidarRUC(identificador))
                return true;
            else
            {
                MessageBox.Show("Cédula, Ruc o Pasaporte no valido", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }
        bool EsCorreoValido(string correo)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(correo);
                return addr.Address == correo;
            }
            catch
            {
                return false;
            }
        }
        public bool ValidarPasaporte(string pasaporte)
        {
            if (pasaporte.Length < 6 || pasaporte.Length > 9)
                return false;

            foreach (char c in pasaporte)
            {
                if (!char.IsLetterOrDigit(c))
                    return false;
            }

            return true;
        }
        public bool ValidarRUC(string ruc)
        {
            if (ruc.Length != 13)
                return false;

            string cedula = ruc.Substring(0, 10);

            if (!EsCedulaValida(cedula))
                return false;

            string establecimiento = ruc.Substring(10, 3);
            if (establecimiento != "001")
                return false;

            return true;
        }

        private bool EsCedulaValida(string cedula)
        {
            if (cedula.Length != 10 || !cedula.All(char.IsDigit))
            {
                return false;
            }

            int[] digitos = cedula.Take(9).Select(c => int.Parse(c.ToString())).ToArray();

            int sumaPares = 0;
            int sumaImpares = 0;

            for (int i = 0; i < digitos.Length; i++)
            {
                if (i % 2 == 0)
                {
                    int resultado = digitos[i] * 2;
                    sumaImpares += resultado > 9 ? resultado - 9 : resultado;
                }
                else
                {
                    sumaPares += digitos[i];
                }
            }

            int sumaTotal = sumaPares + sumaImpares;

            int verificadorCalculado = (sumaTotal % 10 == 0) ? 0 : 10 - (sumaTotal % 10);

            int verificadorCedula = int.Parse(cedula.Substring(9, 1));

            if (verificadorCedula != verificadorCalculado)
            {
                return false;
            }

            return true;
        }


        private void txtCelular_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtNombres_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void txtApellidos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void txtIdentificacion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void txtCorreo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void txtCelular_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void txtDireccion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void dtpFechaNacimiento_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void cmbGenero_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void cmbCargo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtCorreo.Focus();
            }
        }

        private void dtpFechaIngreso_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void cmbEstado_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void txtNombres_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsLetter(e.KeyChar) && !Char.IsControl(e.KeyChar) && e.KeyChar != ' ')
            {
                e.Handled = true; // Ignorar la entrada del usuario
            }
        }

        private void txtApellidos_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsLetter(e.KeyChar) && !Char.IsControl(e.KeyChar) && e.KeyChar != ' ')
            {
                e.Handled = true; // Ignorar la entrada del usuario
            }
        }
    }
}
