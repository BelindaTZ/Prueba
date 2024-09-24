using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BYTEAZULPROFESIONAL
{
    public partial class fmReportesVentas : Form
    {
        csReportes reportes;
        CsConexion conexion = new CsConexion();
        public fmReportesVentas()
        {
            InitializeComponent();
            dtpFechaInicio.Value = DateTime.Now;
            dtpFechafin.Value = DateTime.Now;
        }       
        private void btnGenerarReportes_Click(object sender, EventArgs e)
        {
            if (cbTipodeReportes.Text.Length > 0)
            {
                if (dtpFechafin.Value >= dtpFechaInicio.Value)
                {
                    reportes = new csReportes(dtpFechaInicio.Value, dtpFechafin.Value);
                    ReportParameter parametro = new ReportParameter();
                    rptReporte.Visible = true;
                    reportes.Reportes(rptReporte, reportes.caden(cbTipodeReportes.Text.Trim()), reportes.Informe(cbTipodeReportes.Text.Trim()), reportes.Tabla(cbTipodeReportes.Text.Trim()));
                    if (cbTipodeReportes.Text != "Reporte de Productos en Existencia" && cbTipodeReportes.Text != "Reporte de Listado de Clientes")
                    {
                        parametro = new ReportParameter(reportes.Parametros(cbTipodeReportes.Text), reportes.rellenar(cbTipodeReportes.Text));
                        this.rptReporte.LocalReport.SetParameters(parametro);
                    }
                    this.rptReporte.RefreshReport();
                }
                else { MessageBox.Show("La fecha fin debe ser mayor o igual a la fecha inicial"); }
            }
            else MessageBox.Show("Primero seleccione el tipo de reporte");
        }
        private void cbTipodeReportes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbTipodeReportes.Text != "Reporte de Productos en Existencia" && cbTipodeReportes.Text != "Reportes Graficos de Ventas Mensuales" && cbTipodeReportes.Text != "Reporte de Listado de Clientes" && cbTipodeReportes.Text != "Reportes de Ventas de Empleados")
            {
                dtpFechaInicio.Visible = true;
                dtpFechafin.Visible = true;
            }
            else if (cbTipodeReportes.Text == "Reportes Graficos de Ventas Mensuales" || cbTipodeReportes.Text == "Reportes de Ventas de Empleados")
            {
                dtpFechaInicio.Visible = true;
                dtpFechafin.Visible = false;
            }
            else
            {
                dtpFechaInicio.Visible = false;
                dtpFechafin.Visible = false;
            }
        }
    }
}
