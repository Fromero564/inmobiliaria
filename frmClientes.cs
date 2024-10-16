using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EjerInmobiliaria
{
    public partial class frmClientes : Form
    {
        public frmClientes()
        {
            InitializeComponent();
        }

        private void frmClientes_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();

            chkTodos.Checked = true;

            DataTable tabla = new DataTable();
            string sql = $"select * from localidades order by localidad asc";
            tabla = Libreria.Recuperar(sql);
            cmbLocalidad.DataSource = tabla;
            cmbLocalidad.ValueMember = "idlocalidad";
            cmbLocalidad.DisplayMember = "localidad";
            cmbLocalidad.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbLocalidad.SelectedIndex = 0;

        }

        private void CargarGrilla()
        {
            string sql;
            if (chkTodos.Checked == true)
            {
                sql = "select clientes_1.*, tipodoc from (select clientes.*, " +
                    "localidad from clientes inner join localidades " +
                    "on clientes.idlocalidad = localidades.idlocalidad) AS clientes_1 " +
                    "inner join tiposdocs " +
                    "on clientes_1.idtipodoc = tiposdocs.idtipodoc order by apellido, nombre asc";
            }
            else
            {
                sql = $"select clientes_1.*, tipodoc from (select clientes.*, localidad from clientes inner join localidades on clientes.idlocalidad = localidades.idlocalidad) AS clientes_1 inner join tiposdocs on clientes_1.idtipodoc = tiposdocs.idtipodoc where clientes_1.idlocalidad = {cmbLocalidad.SelectedValue} order by apellido, nombre asc";
            }
            DataTable dt = new DataTable();
            dt = Libreria.Recuperar(sql);

            dgvClientes.DataSource = dt;

            dgvClientes.AllowUserToAddRows = false;
            dgvClientes.AllowUserToDeleteRows = false;
            dgvClientes.ReadOnly = true;
            dgvClientes.Columns["IdCliente"].Visible = false;
            dgvClientes.Columns["IdLocalidad"].Visible = false;
            dgvClientes.Columns["IdTipoDoc"].Visible = false;
            dgvClientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

        }

        private void chkTodos_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTodos.Checked)
            {
                cmbLocalidad.Enabled = false;
            }
            else
            { cmbLocalidad.Enabled = true; }

            CargarGrilla();
        }

        private void cmbLocalidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarGrilla();
        }

        private void mnuNuevo_Click(object sender, EventArgs e)
        {
            frmCliente frm = new frmCliente();
            frm.ShowDialog();
        }

        private void frmClientes_Activated(object sender, EventArgs e)
        {
            CargarGrilla();
        }

        private void mnuModificar_Click(object sender, EventArgs e)
        {
            if (dgvClientes.CurrentRow == null)
            {
                MessageBox.Show("Debe seleccionar la Cliente a modificar.", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Warning); ;
            }
            else
            {
                frmCliente frm = new frmCliente();
                frm.IdClienteSelec = int.Parse(dgvClientes.CurrentRow.Cells["IdCliente"].Value.ToString());
                frm.ShowDialog();
            }
        }
    }
}
