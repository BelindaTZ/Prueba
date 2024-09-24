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
    public partial class fmGestionarMedicina : Form
    {
        CsConexion conexion;
        csMedicina Medicina;
        int num, select;
        public fmGestionarMedicina()
        {
            InitializeComponent();
        }
        public fmGestionarMedicina(int y, int z)
        {
            InitializeComponent();
            select = y;
        }
        public fmGestionarMedicina(int sal)
        {
            InitializeComponent();
            num = sal;
        }
        private void fmGestionarMedicina_Load(object sender, EventArgs e)
        {
            // Para que se muestren la tabla apenas ingresar al fm
            conexion = new CsConexion();
            DataTable VerMedicina = conexion.Insertinto("Select M.id_producto Id, M.md_producto Medicina, M.md_categoria Categoria, M.id_proveedor [Id Proveedor], M.id_iva Iva, M.md_descripcion Descripcion, M.md_precio_unidad [Precio unitario], M.md_estado Estado, sum(l.lt_cantidad) Cantidad from Medicina M left join Lotes L on M.id_producto=L.id_producto group by M.id_producto, M.md_producto, M.md_categoria, M.id_proveedor, M.id_iva, M.md_descripcion, M.md_precio_unidad, M.md_estado");
            dgvVerMedicina.DataSource = VerMedicina;
            
            CsAuxiliar auxiliar = new CsAuxiliar();
            fmAgregarLotes comprasimg = new fmAgregarLotes();
            this.AddOwnedForm(comprasimg);
            auxiliar.AgregarBoton(dgvVerMedicina, comprasimg.imlLogo.Images[2], "Editar");
        }
       

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Medicina = new csMedicina();
            DataTable verMedicina = Medicina.Buscar(txtBuscar.Text);
            dgvVerMedicina.DataSource = verMedicina;
            if(dgvVerMedicina.Columns.Count == 8)
            {
                CsAuxiliar auxiliar = new CsAuxiliar();
                fmAgregarLotes comprasimg = new fmAgregarLotes();
                this.AddOwnedForm(comprasimg);
                auxiliar.AgregarBoton(dgvVerMedicina, comprasimg.imlLogo.Images[2], "Editar");
            }
        }

        private void dgvVerMedicina_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string IdMedicina, NombreMedicina, Categor, Descripcion, Estado, Iva;
            int IdProveedor;
            double Precio;
            Medicina = new csMedicina();
            if(num!= 1 && num!=2)
            if (e.ColumnIndex == dgvVerMedicina.Columns["Editar"].Index && e.RowIndex >= 0)
            {
                int fila = dgvVerMedicina.CurrentCell.RowIndex;
                IdMedicina = dgvVerMedicina.Rows[fila].Cells[1].Value.ToString();
                NombreMedicina = dgvVerMedicina.Rows[fila].Cells[2].Value.ToString();
                Categor = dgvVerMedicina.Rows[fila].Cells[3].Value.ToString();
                IdProveedor = int.Parse(dgvVerMedicina.Rows[fila].Cells[4].Value.ToString());
                Iva = dgvVerMedicina.Rows[fila].Cells[5].Value.ToString();
                Descripcion = dgvVerMedicina.Rows[fila].Cells[6].Value.ToString();
                Precio = double.Parse(dgvVerMedicina.Rows[fila].Cells[7].Value.ToString());
                Estado = dgvVerMedicina.Rows[fila].Cells[8].Value.ToString();
                fmAgregarMedicina AgregarMedicina = new fmAgregarMedicina(IdMedicina, NombreMedicina, Categor, Descripcion, IdProveedor, Iva, Precio, Estado);
                AgregarMedicina.ShowDialog();
                conexion = new CsConexion();
                DataTable VerMedicina = conexion.Insertinto("Select M.id_producto Id, M.md_producto Medicina, M.md_categoria Categoria, M.id_proveedor [Id Proveedor], M.id_iva Iva, M.md_descripcion Descripcion, M.md_precio_unidad [Precio unitario], M.md_estado Estado, sum(l.lt_cantidad) Cantidad from Medicina M left join Lotes L on M.id_producto=L.id_producto group by M.id_producto, M.md_producto, M.md_categoria, M.id_proveedor, M.id_iva, M.md_descripcion, M.md_precio_unidad, M.md_estado");
                dgvVerMedicina.DataSource = VerMedicina;
            }
        }

        private void dgvVerMedicina_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int fila = dgvVerMedicina.CurrentCell.RowIndex;
                if (num == 1)
                {                   
                    if (dgvVerMedicina.Rows[fila].Cells["Estado"].Value.ToString().Trim() == "Disponible")
                    {
                        fmCaja caja = Owner as fmCaja;
                        caja.txtIdProducto.Text = dgvVerMedicina.Rows[fila].Cells["Id"].Value.ToString();
                        caja.txtNombreProducto.Text = dgvVerMedicina.Rows[fila].Cells["Medicina"].Value.ToString();
                        caja.txtPrecio.Text = dgvVerMedicina.Rows[fila].Cells["Precio unitario"].Value.ToString();
                        caja.txtIdProducto.Enabled = false;
                        caja.txtNombreProducto.Enabled = false;
                        caja.txtPrecio.Enabled = false;
                        this.Hide();
                    }
                    else MessageBox.Show("Este producto no se encuentra disponible");

                }
                else if (num == 2)
                {
                    if (dgvVerMedicina.Rows[fila].Cells["Estado"].Value.ToString().Trim() == "Disponible")
                    {
                        DataGridViewRow filaSeleccionada = dgvVerMedicina.SelectedRows[0];
                        Medicina = new csMedicina();

                        string id = filaSeleccionada.Cells["Id"].Value.ToString();
                        string nombremedicina = filaSeleccionada.Cells["Medicina"].Value.ToString();
                        string precio = filaSeleccionada.Cells["Precio unitario"].Value.ToString();
                        string iva = filaSeleccionada.Cells["Iva"].Value.ToString();

                        fmAgregarLotes lotes = Owner as fmAgregarLotes;
                        lotes.txtIDProducto.Text = id;
                        lotes.txtNombreProducto.Text = nombremedicina;
                        lotes.txtPrecio.Text = precio;
                        lotes.txtIVA.Text = Medicina.SacarIVA(iva);
                        this.Close();
                    }
                    else MessageBox.Show("Este producto no se encuentra disponible");
                }
                else if (select == 2)
                {
                    if (dgvVerMedicina.Rows[fila].Cells["Estado"].Value.ToString().Trim() == "Disponible")
                    {
                        DataGridViewRow filaSeleccionada = dgvVerMedicina.SelectedRows[0];
                        fmMovimientos MoviProdu = Owner as fmMovimientos;
                        MoviProdu.YY = int.Parse(filaSeleccionada.Cells["Id"].Value.ToString()); ;
                        this.Hide();
                    }
                    else MessageBox.Show("Este producto no se encuentra disponible");
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
