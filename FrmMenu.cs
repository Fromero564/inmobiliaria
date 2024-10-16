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
    public partial class FrmMenu : Form
    {
        public FrmMenu()
        {
            InitializeComponent();
        }

        private void mnuLocalidades_Click(object sender, EventArgs e)
        {
            FrmLocalidades frm = new FrmLocalidades();
            frm.ShowDialog();
        }

        private void mnuClientes_Click(object sender, EventArgs e)
        {
            frmClientes frm = new frmClientes();    
            frm.ShowDialog();
        }

        private void FrmMenu_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();

            lblUsuario.Text = Libreria.UsuarioLog;

            if (Libreria.GrupoLog == "Administrador")
            {
                mnuUsuario.Visible = true;
            }
            else
            {
                mnuUsuario.Visible = false;
            }
        }

        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUsuarios frm = new frmUsuarios();
            frm.ShowDialog();
        }

        private void mnuSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmMenu_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
