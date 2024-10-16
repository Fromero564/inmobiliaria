using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EjerInmobiliaria
{
    public partial class frmUsuarios : Form
    {
        public frmUsuarios()
        {
            InitializeComponent();
        }

        private void frmUsuarios_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
        }

        private void CargarGrilla()
        {
            string sql;
            sql = "select usuarios.*, grupo from usuarios inner join grupos on usuarios.idgrupo = grupos.idgrupo ";

            DataTable dt = new DataTable();

            dt = Libreria.Recuperar(sql);

            dgvUsuarios.DataSource = dt;

            dgvUsuarios.AllowUserToAddRows = false;
            dgvUsuarios.AllowUserToDeleteRows = false;
            dgvUsuarios.ReadOnly = true;
            dgvUsuarios.Columns["IdUsuario"].Visible = false;
            dgvUsuarios.Columns["IdGrupo"].Visible = false;
            dgvUsuarios.Columns["Pass"].Visible = false;
            dgvUsuarios.Columns["IntentosFallidos"].Visible = false;
            dgvUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void frmUsuarios_Activated(object sender, EventArgs e)
        {
            CargarGrilla();
        }

        private void mnuNuevo_Click(object sender, EventArgs e)
        {
            frmUsuario frm = new frmUsuario();
            frm.ShowDialog();
        }

        private void mnuModificar_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.CurrentRow == null)
            {
                MessageBox.Show("Debe seleccionar el Usuario a modificar.", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Warning); ;
            }
            else
            {
                frmUsuario frm = new frmUsuario();
                frm.IdSelec = int.Parse(dgvUsuarios.CurrentRow.Cells["IdUsuario"].Value.ToString());
                frm.ShowDialog();
            }
        }
    }
}
