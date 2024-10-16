using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EjerInmobiliaria
{
    public partial class frmLocalidad : Form
    {
        public int IdLocalidadSelec { get; set; }
        public frmLocalidad()
        {
            InitializeComponent();
            this.IdLocalidadSelec = 0;

        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                try
                {
                    string sql;
                    if (this.IdLocalidadSelec == 0)
                    {
                        sql = $"insert into localidades (codigopostal, localidad) " +
                            $"values ('{txtCodPostal.Text}','{txtLocalidad.Text}') ";
                    }
                    else
                    {
                        sql = $"update localidades set codigopostal = '{txtCodPostal.Text}', " +
                            $"localidad = '{txtLocalidad.Text}' " +
                            $"where idlocalidad = {this.IdLocalidadSelec}";
                    }
                    Libreria.Ejecutar(sql);

                    MessageBox.Show("Localidad Registrada!", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.Close();
                }
                catch (Exception ex) {
                    MessageBox.Show($"Error: {ex.Message}", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void frmLocalidad_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();

            if (this.IdLocalidadSelec != 0)
            {
                string sql;
                DataTable dt = new DataTable();
                sql = $"select * from localidades where idlocalidad = {this.IdLocalidadSelec}";
                dt = Libreria.Recuperar(sql);
                txtCodPostal.Text = dt.Rows[0]["CodigoPostal"].ToString();
                txtLocalidad.Text = dt.Rows[0]["Localidad"].ToString();

            }

        }

        private bool validar()
        {
            bool ok = false;

            if (txtCodPostal.Text.Trim() != string.Empty)
            {
                if (txtLocalidad.Text.Trim() != string.Empty)
                {
                    string sql;
                    if (this.IdLocalidadSelec == 0)
                    {
                        sql = $"select* from localidades where codigopostal = '{txtCodPostal.Text}'";
                    }
                    else
                    {
                        sql = $"select* from localidades where codigopostal = '{txtCodPostal.Text}' " +
                            $"and idlocalidad <> {this.IdLocalidadSelec}" ;
                    }

                    DataTable dt = Libreria.Recuperar(sql);
                    if (dt.Rows.Count == 0)
                    {
                        ok = true;
                    }
                    else
                    {
                        MessageBox.Show($"El codigo postal ya existe ({dt.Rows[0]["Localidad"].ToString()}).", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    MessageBox.Show("Debe completar Localidad.", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("Debe completar el codigo postal.", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            return ok;

        }
    }
}
