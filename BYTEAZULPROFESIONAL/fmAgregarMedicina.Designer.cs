namespace BYTEAZULPROFESIONAL
{
    partial class fmAgregarMedicina
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fmAgregarMedicina));
            this.txtNombreMedicina = new System.Windows.Forms.TextBox();
            this.txtIDProveedor = new System.Windows.Forms.TextBox();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.txtPrecio = new System.Windows.Forms.TextBox();
            this.btnAgregarMedicina = new System.Windows.Forms.Button();
            this.btnModificarMedicina = new System.Windows.Forms.Button();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.cmbCategoria = new System.Windows.Forms.ComboBox();
            this.cmbEstado = new System.Windows.Forms.ComboBox();
            this.cmbIva = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // txtNombreMedicina
            // 
            this.txtNombreMedicina.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(214)))), ((int)(((byte)(216)))));
            this.txtNombreMedicina.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNombreMedicina.Location = new System.Drawing.Point(205, 122);
            this.txtNombreMedicina.Margin = new System.Windows.Forms.Padding(2);
            this.txtNombreMedicina.Name = "txtNombreMedicina";
            this.txtNombreMedicina.Size = new System.Drawing.Size(180, 13);
            this.txtNombreMedicina.TabIndex = 0;
            this.txtNombreMedicina.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtNombreMedicina.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNombreMedicina_KeyDown);
            // 
            // txtIDProveedor
            // 
            this.txtIDProveedor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(214)))), ((int)(((byte)(216)))));
            this.txtIDProveedor.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtIDProveedor.Enabled = false;
            this.txtIDProveedor.Location = new System.Drawing.Point(498, 122);
            this.txtIDProveedor.Margin = new System.Windows.Forms.Padding(2);
            this.txtIDProveedor.Name = "txtIDProveedor";
            this.txtIDProveedor.Size = new System.Drawing.Size(134, 13);
            this.txtIDProveedor.TabIndex = 3;
            this.txtIDProveedor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(214)))), ((int)(((byte)(216)))));
            this.txtDescripcion.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDescripcion.Location = new System.Drawing.Point(86, 232);
            this.txtDescripcion.Margin = new System.Windows.Forms.Padding(2);
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(180, 13);
            this.txtDescripcion.TabIndex = 3;
            this.txtDescripcion.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtDescripcion.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDescripcion_KeyDown);
            // 
            // txtPrecio
            // 
            this.txtPrecio.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(214)))), ((int)(((byte)(216)))));
            this.txtPrecio.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPrecio.Location = new System.Drawing.Point(598, 229);
            this.txtPrecio.Margin = new System.Windows.Forms.Padding(2);
            this.txtPrecio.Name = "txtPrecio";
            this.txtPrecio.Size = new System.Drawing.Size(180, 13);
            this.txtPrecio.TabIndex = 5;
            this.txtPrecio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPrecio.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPrecio_KeyDown);
            this.txtPrecio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPrecio_KeyPress);
            // 
            // btnAgregarMedicina
            // 
            this.btnAgregarMedicina.BackColor = System.Drawing.Color.Transparent;
            this.btnAgregarMedicina.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAgregarMedicina.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregarMedicina.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(92)))), ((int)(((byte)(185)))));
            this.btnAgregarMedicina.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(92)))), ((int)(((byte)(185)))));
            this.btnAgregarMedicina.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(92)))), ((int)(((byte)(185)))));
            this.btnAgregarMedicina.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgregarMedicina.ForeColor = System.Drawing.Color.Transparent;
            this.btnAgregarMedicina.Location = new System.Drawing.Point(130, 429);
            this.btnAgregarMedicina.Name = "btnAgregarMedicina";
            this.btnAgregarMedicina.Size = new System.Drawing.Size(258, 60);
            this.btnAgregarMedicina.TabIndex = 8;
            this.btnAgregarMedicina.TabStop = false;
            this.btnAgregarMedicina.UseVisualStyleBackColor = false;
            this.btnAgregarMedicina.Click += new System.EventHandler(this.btnAgregarMedicina_Click);
            // 
            // btnModificarMedicina
            // 
            this.btnModificarMedicina.BackColor = System.Drawing.Color.Transparent;
            this.btnModificarMedicina.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnModificarMedicina.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnModificarMedicina.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(92)))), ((int)(((byte)(185)))));
            this.btnModificarMedicina.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(92)))), ((int)(((byte)(185)))));
            this.btnModificarMedicina.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(92)))), ((int)(((byte)(185)))));
            this.btnModificarMedicina.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnModificarMedicina.ForeColor = System.Drawing.Color.Transparent;
            this.btnModificarMedicina.Location = new System.Drawing.Point(466, 427);
            this.btnModificarMedicina.Name = "btnModificarMedicina";
            this.btnModificarMedicina.Size = new System.Drawing.Size(258, 63);
            this.btnModificarMedicina.TabIndex = 9;
            this.btnModificarMedicina.TabStop = false;
            this.btnModificarMedicina.UseVisualStyleBackColor = false;
            this.btnModificarMedicina.Click += new System.EventHandler(this.btnModificarMedicina_Click);
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.Transparent;
            this.btnBuscar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBuscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(92)))), ((int)(((byte)(185)))));
            this.btnBuscar.FlatAppearance.BorderSize = 0;
            this.btnBuscar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(92)))), ((int)(((byte)(185)))));
            this.btnBuscar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(92)))), ((int)(((byte)(185)))));
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.ForeColor = System.Drawing.Color.Transparent;
            this.btnBuscar.Location = new System.Drawing.Point(635, 113);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(30, 31);
            this.btnBuscar.TabIndex = 2;
            this.btnBuscar.TabStop = false;
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // cmbCategoria
            // 
            this.cmbCategoria.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(214)))), ((int)(((byte)(216)))));
            this.cmbCategoria.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbCategoria.FormattingEnabled = true;
            this.cmbCategoria.Location = new System.Drawing.Point(340, 226);
            this.cmbCategoria.Margin = new System.Windows.Forms.Padding(2);
            this.cmbCategoria.Name = "cmbCategoria";
            this.cmbCategoria.Size = new System.Drawing.Size(175, 21);
            this.cmbCategoria.TabIndex = 4;
            this.cmbCategoria.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbCategoria_KeyDown);
            this.cmbCategoria.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbCategoria_KeyPress);
            // 
            // cmbEstado
            // 
            this.cmbEstado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(214)))), ((int)(((byte)(216)))));
            this.cmbEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEstado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbEstado.FormattingEnabled = true;
            this.cmbEstado.Items.AddRange(new object[] {
            "Disponible",
            "NoDisponible"});
            this.cmbEstado.Location = new System.Drawing.Point(479, 327);
            this.cmbEstado.Margin = new System.Windows.Forms.Padding(2);
            this.cmbEstado.Name = "cmbEstado";
            this.cmbEstado.Size = new System.Drawing.Size(175, 21);
            this.cmbEstado.TabIndex = 7;
            this.cmbEstado.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbEstado_KeyDown);
            // 
            // cmbIva
            // 
            this.cmbIva.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(214)))), ((int)(((byte)(216)))));
            this.cmbIva.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbIva.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbIva.FormattingEnabled = true;
            this.cmbIva.Items.AddRange(new object[] {
            "A",
            "B",
            "C"});
            this.cmbIva.Location = new System.Drawing.Point(191, 326);
            this.cmbIva.Margin = new System.Windows.Forms.Padding(2);
            this.cmbIva.Name = "cmbIva";
            this.cmbIva.Size = new System.Drawing.Size(175, 21);
            this.cmbIva.TabIndex = 6;
            this.cmbIva.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbIva_KeyDown);
            // 
            // fmAgregarMedicina
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(831, 544);
            this.Controls.Add(this.cmbIva);
            this.Controls.Add(this.cmbEstado);
            this.Controls.Add(this.cmbCategoria);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.btnModificarMedicina);
            this.Controls.Add(this.btnAgregarMedicina);
            this.Controls.Add(this.txtPrecio);
            this.Controls.Add(this.txtDescripcion);
            this.Controls.Add(this.txtIDProveedor);
            this.Controls.Add(this.txtNombreMedicina);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "fmAgregarMedicina";
            this.Text = "fmAgregarMedicina";
            this.Load += new System.EventHandler(this.fmAgregarMedicina_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtNombreMedicina;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.TextBox txtPrecio;
        private System.Windows.Forms.Button btnAgregarMedicina;
        private System.Windows.Forms.Button btnModificarMedicina;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.ComboBox cmbCategoria;
        private System.Windows.Forms.ComboBox cmbEstado;
        public System.Windows.Forms.TextBox txtIDProveedor;
        private System.Windows.Forms.ComboBox cmbIva;
    }
}