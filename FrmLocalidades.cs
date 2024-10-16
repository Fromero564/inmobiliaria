using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EjerInmobiliaria
{
    public partial class FrmLocalidades : Form
    {
        public FrmLocalidades()
        {
            InitializeComponent();
        }

        private void FrmLocalidades_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();

            
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarGrilla();
        }

        private void CargarGrilla()
        {
            string sql;
            if (txtLocalidad.Text.Trim() == string.Empty)
            {
                sql = "select * from localidades order by localidad asc";
            }
            else
            {
                sql = $"select * from localidades where localidad like '{txtLocalidad.Text.Trim()}%' order by localidad asc";
            }

            DataTable dt = new DataTable();

            dt = Libreria.Recuperar(sql);

            dgvLocalidades.DataSource = dt;

            dgvLocalidades.AllowUserToAddRows = false;
            dgvLocalidades.AllowUserToDeleteRows = false;
            dgvLocalidades.ReadOnly = true;
            dgvLocalidades.Columns["IdLocalidad"].Visible = false;
            dgvLocalidades.Columns["CodigoPostal"].HeaderText = "C.P.";
            dgvLocalidades.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void mnuNueva_Click(object sender, EventArgs e)
        {
            frmLocalidad frm = new frmLocalidad();
            frm.ShowDialog();
        }

        private void FrmLocalidades_Activated(object sender, EventArgs e)
        {
            CargarGrilla();
        }

        private void mnuExportar_Click(object sender, EventArgs e)
        {
            string cadena;
            PB.Minimum = 0;
            PB.Maximum = dgvLocalidades.Rows.Count;
            PB.Step = 1;
            FileStream archivo = new FileStream("Expo.csv", FileMode.Create);
            StreamWriter contenido = new StreamWriter(archivo);
            foreach (DataGridViewRow fila in dgvLocalidades.Rows)
            {
                cadena = string.Empty;
                foreach (DataGridViewCell celda in fila.Cells)
                {
                    cadena += celda.Value.ToString() + ";";
                }
                contenido.WriteLine(cadena);
                PB.PerformStep();
            }
            contenido.Close();
            archivo.Close();

            MessageBox.Show("Proceso Finalizado.", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void mnuModificar_Click(object sender, EventArgs e)
        {
            if (dgvLocalidades.CurrentRow == null)
            {
                MessageBox.Show("Debe seleccionar la Localidad a modificar.", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Warning); ;
            }
            else
            {
                frmLocalidad frm = new frmLocalidad();
                frm.IdLocalidadSelec = int.Parse(dgvLocalidades.CurrentRow.Cells["IdLocalidad"].Value.ToString());
                frm.ShowDialog();
            }
        }
    }
}
