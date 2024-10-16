using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace EjerInmobiliaria
{
    public partial class frmCliente : Form
    {
        public int IdClienteSelec { get; set; }
        public frmCliente()
        {
            InitializeComponent();
            this.IdClienteSelec = 0;
        }

        private void frmCliente_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();

            DataTable tabla = new DataTable();
            string sql = $"select * from localidades order by localidad asc";
            tabla = Libreria.Recuperar(sql);
            cmbLocalidad.DataSource = tabla;
            cmbLocalidad.ValueMember = "idlocalidad";
            cmbLocalidad.DisplayMember = "localidad";
            cmbLocalidad.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbLocalidad.SelectedIndex = 0;

            tabla = new DataTable();
            sql = $"select * from tiposdocs order by tipodoc asc";
            tabla = Libreria.Recuperar(sql);
            cmbTDoc.DataSource = tabla;
            cmbTDoc.ValueMember = "idtipodoc";
            cmbTDoc.DisplayMember = "tipodoc";
            cmbTDoc.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTDoc.SelectedIndex = 0;

            chkActivo.Checked = true; 
            
            if (this.IdClienteSelec != 0)
            {
                tabla = new DataTable();
                tabla = Libreria.Recuperar($"select * from clientes where idcliente = {this.IdClienteSelec}");
                cmbTDoc.SelectedValue = tabla.Rows[0]["IdTipoDoc"].ToString();
                txtNDoc.Text = tabla.Rows[0]["NDoc"].ToString();
                txtApellido.Text = tabla.Rows[0]["Apellido"].ToString();
                txtNombre.Text = tabla.Rows[0]["Nombre"].ToString();
                dtpfechanac.Value = DateTime.Parse(tabla.Rows[0]["FechaNac"].ToString());
                cmbLocalidad.SelectedValue = tabla.Rows[0]["IdLocalidad"].ToString();
                txtDireccion.Text = tabla.Rows[0]["Direccion"].ToString();
                txtCel.Text = tabla.Rows[0]["Cel"].ToString();
                txtEmail.Text = tabla.Rows[0]["Email"].ToString();
                chkActivo.Checked = bool.Parse(tabla.Rows[0]["Activo"].ToString());
                txtObs.Text = tabla.Rows[0]["Observaciones"].ToString();
            }
        }

        private bool validar()
        {
            bool ok = false;
            int ndoc;

            if (txtApellido.Text.Trim() != String.Empty)
            {
                if (txtNombre.Text.Trim() != String.Empty)
                {
                    if (txtNDoc.Text.Trim() != String.Empty)
                    {
                        if (int.TryParse(txtNDoc.Text, out ndoc) && ndoc > 0)
                        {
                            if (txtCel.Text.Trim() != String.Empty)
                            {
                                if (txtDireccion.Text.Trim() != String.Empty)
                                {
                                    if (txtEmail.Text.Trim() == String.Empty)
                                    {
                                        ok = true;
                                    }
                                    else
                                    {
                                        Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                                        if (regex.Match(txtEmail.Text).Success)
                                        {
                                            ok = true;
                                        }
                                        else
                                        {
                                            MessageBox.Show("Email no es válido.", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                        }
                                    }

                                    if (ok == true)
                                    {
                                        string sql;

                                        if (this.IdClienteSelec == 0)
                                        {
                                            sql = $"select * from clientes where idtipodoc = {cmbTDoc.SelectedValue}" +
                                                $" and ndoc = {txtNDoc.Text}";
                                        }
                                        else
                                        {
                                            sql = $"select * from clientes where idtipodoc = {cmbTDoc.SelectedValue} " +
                                                $"and ndoc = {txtNDoc.Text} " +
                                                $"and idcliente <> {this.IdClienteSelec}" ;
                                        }

                                        DataTable dt = new DataTable();
                                        dt = Libreria.Recuperar(sql);

                                        if (dt.Rows.Count > 0)
                                        {
                                            ok = false;
                                            MessageBox.Show("El Tdoc y Ndoc se encuentran asignados a otro cliente.", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                        }
                                        else
                                        {
                                            ok = true;
                                        }

                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Debe completar la direccion.", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Debe completar el Cel.", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                        }
                        else
                        {
                            MessageBox.Show("El Nro. Doc no es válido.", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Debe completar el Nro. Doc.", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    MessageBox.Show("Debe completar el Nombre.", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("Debe completar el apellido.", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            return ok;
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                try
                {
                    string email;
                    if (txtEmail.Text.Trim() == String.Empty)
                    {
                        email = "NULL";
                    }
                    else
                    {
                        email = $"'{txtEmail.Text.Trim()}'";
                    }

                    string obs;
                    if (txtObs.Text.Trim() == String.Empty)
                    {
                        obs = "NULL";
                    }
                    else
                    {
                        obs = $"'{txtObs.Text.Trim()}'";
                    }

                    string activo;

                    if (chkActivo.Checked)
                        activo = "1";
                    else
                        activo = "0";

                    string sql;

                    if (this.IdClienteSelec == 0)
                    {
                        sql = $"insert into clientes (Apellido,Nombre,IdTipoDoc,NDoc,FechaNac,Direccion,IdLocalidad,Cel,Email,Activo,Observaciones) values ('{txtApellido.Text}','{txtNombre.Text}',{cmbTDoc.SelectedValue},{txtNDoc.Text},'{dtpfechanac.Value.ToShortDateString()}','{txtDireccion.Text}',{cmbLocalidad.SelectedValue},'{txtCel.Text}',{email},{activo},{obs})";
                    }
                    else
                    {
                        sql = $"update clientes set Apellido = '{txtApellido.Text}', Nombre = '{txtNombre.Text}'," +
                           $"IdTipoDoc = {cmbTDoc.SelectedValue}, NDoc = {txtNDoc.Text}," +
                           $"FechaNac = '{dtpfechanac.Value.ToShortDateString()}'," +
                           $"IdLocalidad = {cmbLocalidad.SelectedValue}, Direccion = '{txtDireccion.Text}'," +
                           $"Cel = '{txtCel.Text}', Email = {email}, Observaciones = {obs}, Activo = {activo}" +
                           $"where IdCliente = {this.IdClienteSelec}";
                    }
                    Libreria.Ejecutar(sql);

                    MessageBox.Show("Cliente Registrado!", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.Close();
                }
                catch (Exception er)
                {
                    MessageBox.Show(er.Message, "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}
