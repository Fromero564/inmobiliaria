namespace EjerInmobiliaria
{
    partial class FrmLocalidades
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
            this.components = new System.ComponentModel.Container();
            this.dgvLocalidades = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuNueva = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExportar = new System.Windows.Forms.ToolStripMenuItem();
            this.txtLocalidad = new System.Windows.Forms.TextBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.PB = new System.Windows.Forms.ProgressBar();
            this.mnuModificar = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocalidades)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvLocalidades
            // 
            this.dgvLocalidades.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLocalidades.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvLocalidades.Location = new System.Drawing.Point(12, 38);
            this.dgvLocalidades.Name = "dgvLocalidades";
            this.dgvLocalidades.Size = new System.Drawing.Size(396, 310);
            this.dgvLocalidades.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuNueva,
            this.mnuModificar,
            this.mnuExportar});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(181, 92);
            // 
            // mnuNueva
            // 
            this.mnuNueva.Name = "mnuNueva";
            this.mnuNueva.Size = new System.Drawing.Size(180, 22);
            this.mnuNueva.Text = "Nueva";
            this.mnuNueva.Click += new System.EventHandler(this.mnuNueva_Click);
            // 
            // mnuExportar
            // 
            this.mnuExportar.Name = "mnuExportar";
            this.mnuExportar.Size = new System.Drawing.Size(180, 22);
            this.mnuExportar.Text = "Exportar";
            this.mnuExportar.Click += new System.EventHandler(this.mnuExportar_Click);
            // 
            // txtLocalidad
            // 
            this.txtLocalidad.Location = new System.Drawing.Point(13, 13);
            this.txtLocalidad.Name = "txtLocalidad";
            this.txtLocalidad.Size = new System.Drawing.Size(220, 20);
            this.txtLocalidad.TabIndex = 1;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(240, 12);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(75, 23);
            this.btnBuscar.TabIndex = 2;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // PB
            // 
            this.PB.Location = new System.Drawing.Point(13, 358);
            this.PB.Name = "PB";
            this.PB.Size = new System.Drawing.Size(395, 23);
            this.PB.TabIndex = 3;
            // 
            // mnuModificar
            // 
            this.mnuModificar.Name = "mnuModificar";
            this.mnuModificar.Size = new System.Drawing.Size(180, 22);
            this.mnuModificar.Text = "Modificar";
            this.mnuModificar.Click += new System.EventHandler(this.mnuModificar_Click);
            // 
            // FrmLocalidades
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 393);
            this.Controls.Add(this.PB);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.txtLocalidad);
            this.Controls.Add(this.dgvLocalidades);
            this.Name = "FrmLocalidades";
            this.Text = "Listado de Localidades";
            this.Activated += new System.EventHandler(this.FrmLocalidades_Activated);
            this.Load += new System.EventHandler(this.FrmLocalidades_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocalidades)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvLocalidades;
        private System.Windows.Forms.TextBox txtLocalidad;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuNueva;
        private System.Windows.Forms.ToolStripMenuItem mnuExportar;
        private System.Windows.Forms.ProgressBar PB;
        private System.Windows.Forms.ToolStripMenuItem mnuModificar;
    }
}