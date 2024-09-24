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
    public partial class fmAgregarClientes : Form
    {
        csClientes cliente;
        CsConexion conexion;
        bool confir; 
        public fmAgregarClientes()
        {
            InitializeComponent();
            cmbEstado.SelectedIndex = 0;
        }
        public fmAgregarClientes(string nombre, string apellido, string identificacion, string correo, string direccion, string celular, string estado)
        {
            InitializeComponent();
            txtNombres.Text = nombre;
            txtApellidos.Text = apellido;
            txtIdentificacion.Text = identificacion;
            txtEmail.Text = correo;
            txtDireccion.Text = direccion;
            txtCelular.Text = celular;

            if (estado == "Activo")
                cmbEstado.SelectedIndex = 0;
            else
                cmbEstado.SelectedIndex = 1;

            txtNombres.Enabled = false;
            txtApellidos.Enabled = false; 
            txtIdentificacion.Enabled = false;

            confir = true;
        }

        private void btnAgregarClientes_Click(object sender, EventArgs e)
        {
            if (validar() && validarIdentificador(txtIdentificacion.Text))
            {
                cliente = new csClientes();

                if (cliente.VerificarIdentBD(txtIdentificacion.Text))
                {
                    if (cliente.IngresarCliente(txtNombres.Text.Trim(), txtApellidos.Text.Trim(), txtIdentificacion.Text.Trim(), txtCelular.Text.Trim(), txtDireccion.Text.Trim(), txtEmail.Text.Trim(), cmbEstado.Text))
                        Limpiar();
                }
                else
                    MessageBox.Show("Identificacion en uso", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnModificarClientes_Click(object sender, EventArgs e)
        {
            if (confir && validar())
            {
                cliente = new csClientes();

                if (cliente.ModificarCliente(txtCelular.Text.Trim(), txtDireccion.Text.Trim(), txtEmail.Text.Trim(), txtIdentificacion.Text.Trim(), cmbEstado.Text))
                {
                    confir = false;
                    Limpiar();
                }

                txtNombres.Enabled = true;
                txtApellidos.Enabled = true;
                txtIdentificacion.Enabled = true; this.Close();
            }
        }
        void Limpiar()
        {
            txtNombres.Clear();
            txtApellidos.Clear();
            txtIdentificacion.Clear();
            txtCelular.Clear();
            txtDireccion.Clear();
            txtEmail.Clear();
            cmbEstado.SelectedIndex = -1;
        }
        bool validar()
        {
            if (string.IsNullOrWhiteSpace(txtNombres.Text) || string.IsNullOrWhiteSpace(txtApellidos.Text) || string.IsNullOrWhiteSpace(txtIdentificacion.Text)
                || string.IsNullOrWhiteSpace(txtCelular.Text) || string.IsNullOrWhiteSpace(txtDireccion.Text) || string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Ingrese todos los valores", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!EsCorreoValido(txtEmail.Text))
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
        public bool ValidarRUC(string identificador)
        {
            if (ValidarRUCNatural(identificador))
                return true;
            else if (ValidarRucSociedad(identificador))
                return true;
            else
                return false;
        }
        public bool ValidarRUCNatural(string ruc)
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
        public bool ValidarRucSociedad(string ruc)
        {
            // Verificar que tenga 13 dígitos
            if (ruc.Length != 13)
                return false;

            // Verificar que los dos primeros dígitos correspondan a una provincia válida (01-24)
            int provincia = int.Parse(ruc.Substring(0, 2));
            if (provincia < 1 || provincia > 24)
                return false;

            // Verificar que el noveno dígito sea el correcto usando un algoritmo de verificación
            int[] coeficientes = { 4, 3, 2, 7, 6, 5, 4, 3, 2 };
            int suma = 0;
            for (int i = 0; i < coeficientes.Length; i++)
            {
                suma += coeficientes[i] * int.Parse(ruc[i].ToString());
            }

            int residuo = suma % 11;
            int verificador = residuo == 0 ? 0 : 11 - residuo;

            // Comparar el noveno dígito
            if (int.Parse(ruc[9].ToString()) != verificador)
                return false;

            // Verificar que los últimos tres dígitos sean "001"
            if (ruc.Substring(10, 3) != "001")
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


        private void txtCelular_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void txtCelular_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtIdentificacion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
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

        private void txtEmail_KeyDown(object sender, KeyEventArgs e)
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
    }
}
