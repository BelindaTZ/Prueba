using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BYTEAZULPROFESIONAL
{
    public partial class fmGestionarProveedores : Form
    {
        CsConexion conexion;
        csProveedor Proveedores;
        fmAgregarProveedores Proveedor;
        int y;
        string prv_IdProveedor, prv_nombre, prv_celular, prv_servicios, prv_direccion, prv_email, prv_estado, prv_categoria, prv_descripcion, prv_precio, prv_iva;
        public fmGestionarProveedores(int z, string NombreMedicina, string Categoria, string IDProveedor, string Descripcion, string Precio, string Iva)
        {
            // constructor que se activa cuando quieren seleccionar un proveedor (esta enlazado con el fm AgregarMedicina)
            InitializeComponent();
            y = z;
            prv_nombre = NombreMedicina;
            prv_categoria = Categoria;
            prv_IdProveedor = IDProveedor;
            prv_descripcion = Descripcion;
            prv_precio = Precio;
            prv_iva = Iva;
        }
        public fmGestionarProveedores(int a)
        {
            InitializeComponent();
            y = a;
        }
        private void dgvVerProveedores_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Para seleccionar el proveedor (enlazado con el fm AgregarMedicina)
            if(y == 9)
            {
                int fila = dgvVerProveedores.CurrentCell.RowIndex;
                prv_IdProveedor = dgvVerProveedores.Rows[fila].Cells["Id"].Value.ToString();
                fmAgregarMedicina Medicina = Owner as fmAgregarMedicina; //(9, prv_nombre, prv_categoria, prv_IdProveedor, prv_descripcion, prv_precio, prv_iva);
                Medicina.txtIDProveedor.Text = prv_IdProveedor; // Codigo modificado para que valiera el id de proveedor al momento de agregar una medicina - el txt se hizo publico
                this.Hide();
            }
            else if (y == 2) //para compras
            {
                DataGridViewRow filaSeleccionada = dgvVerProveedores.SelectedRows[0];

                string id = filaSeleccionada.Cells["Id"].Value.ToString();
                string nombre = filaSeleccionada.Cells["Nombre"].Value.ToString();

                fmAgregarLotes lotes = Owner as fmAgregarLotes;
                lotes.txtProveedor.Text = id;
                lotes.NombreProvedor = nombre;
                this.Close();
            }
        }

        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Metodo para buscar con el enter sin necesidad de presionar el boton de busqueda
            if (e.KeyChar == (char)Keys.Enter)
            {
                Proveedores = new csProveedor();
                DataTable Proveedoris = Proveedores.Buscar(txtBuscar.Text);
                dgvVerProveedores.DataSource = Proveedoris;
                if (dgvVerProveedores.Columns.Count == 7)
                {
                    CsAuxiliar auxiliar = new CsAuxiliar();
                    fmAgregarLotes comprasimg = new fmAgregarLotes();
                    this.AddOwnedForm(comprasimg);
                    auxiliar.AgregarBoton(dgvVerProveedores, comprasimg.imlLogo.Images[2], "Editar");
                }
            }
        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            // Metodo para buscar con el boton de la lupita
            Proveedores = new csProveedor();
            DataTable Proveedoris = Proveedores.Buscar(txtBuscar.Text);
            dgvVerProveedores.DataSource = Proveedoris;
            if(dgvVerProveedores.Columns.Count == 7)
            {
                CsAuxiliar auxiliar = new CsAuxiliar();
                fmAgregarLotes comprasimg = new fmAgregarLotes();
                this.AddOwnedForm(comprasimg);
                auxiliar.AgregarBoton(dgvVerProveedores, comprasimg.imlLogo.Images[2], "Editar");
            }
        }

        public fmGestionarProveedores() // Constructor default
        {
            InitializeComponent();
        }

        private void fmGestionarProveedores_Load(object sender, EventArgs e)
        {
            // Para que se muestren la tabla apenas ingresar al fm
            conexion = new CsConexion();
            DataTable Proveedores = conexion.Insertinto("Select id_proveedor as Id, prv_nombre as Nombre, prv_celular as Celular, prv_servicios as Servicios, prv_direccion as Direccion, prv_email as Email, prv_estado as Estado from Proveedores");
            dgvVerProveedores.DataSource = Proveedores;

            CsAuxiliar auxiliar = new CsAuxiliar();
            fmAgregarLotes comprasimg = new fmAgregarLotes();
            this.AddOwnedForm(comprasimg);
            auxiliar.AgregarBoton(dgvVerProveedores, comprasimg.imlLogo.Images[2], "Editar");
        }

        private void dgvVerProveedores_CellContentClick(object sender, DataGridViewCellEventArgs e)
        { 
            // Metodo para el boton de modificar de la tabla
            if(y != 9 && y != 2)
            if (e.ColumnIndex == dgvVerProveedores.Columns["Editar"].Index && e.RowIndex >= 0)
            {
                int fila = dgvVerProveedores.CurrentCell.RowIndex;
                prv_IdProveedor = dgvVerProveedores.Rows[fila].Cells[1].Value.ToString();
                prv_nombre = dgvVerProveedores.Rows[fila].Cells[2].Value.ToString();
                prv_celular = dgvVerProveedores.Rows[fila].Cells[3].Value.ToString();
                prv_servicios = dgvVerProveedores.Rows[fila].Cells[4].Value.ToString();
                prv_direccion = dgvVerProveedores.Rows[fila].Cells[5].Value.ToString();
                prv_email = dgvVerProveedores.Rows[fila].Cells[6].Value.ToString();
                prv_estado = dgvVerProveedores.Rows[fila].Cells[7].Value.ToString();
                int select;
                if(prv_estado == "Activo")
                    select = 0;
                else
                    select = 1;
                Proveedor = new fmAgregarProveedores(prv_IdProveedor, prv_nombre, prv_celular, prv_servicios, prv_direccion, prv_email, select);
                Proveedor.ShowDialog();
                conexion = new CsConexion();
                DataTable Proveedores = conexion.Insertinto("Select id_proveedor as Id, prv_nombre as Nombre, prv_celular as Celular, prv_servicios as Servicios, prv_direccion as Direccion, prv_email as Email, prv_estado as Estado from Proveedores");
                dgvVerProveedores.DataSource = Proveedores;
            }
        }
    }
}
