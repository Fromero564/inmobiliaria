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
    public partial class frmUsuario : Form
    {
        private int _IdSelec;
        public int IdSelec
        {
            get { return this._IdSelec; }
            set { this._IdSelec = value; }
        }
        public frmUsuario()
        {
            InitializeComponent();
            this.IdSelec = 0;
        }

        private void frmUsuario_Load(object sender, EventArgs e)
        {
            DataTable tabla;
            string cadenaSQL;

            this.CenterToScreen();

            cadenaSQL = "select * from grupos order by acceso asc";
            tabla = new DataTable();
            tabla = Libreria.Recuperar(cadenaSQL);
            cmbGrupo.DataSource = tabla;
            cmbGrupo.DisplayMember = "Grupo";
            cmbGrupo.ValueMember = "IdGrupo";
            cmbGrupo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbGrupo.SelectedIndex = 0;

            if (this.IdSelec == 0)
            {
                //las sig. lineas ocultan los controles relacionados con el estado del usuario.
                //En caso de un Alta no tiene sentido dar la posibilidad de registrarlo NO ACTIVO
                chkActivo.Visible = false;
                lblActivo.Visible = false;
            }
            else
            {
                cadenaSQL = $"select * from Usuarios where IdUsuario = {this.IdSelec}";
                tabla = new DataTable();
                tabla = Libreria.Recuperar(cadenaSQL);
                txtUsuario.Text = tabla.Rows[0]["Usuario"].ToString();
                txtApellido.Text = tabla.Rows[0]["Apellido"].ToString();
                txtNombre.Text = tabla.Rows[0]["Nombre"].ToString();
                cmbGrupo.SelectedValue = int.Parse(tabla.Rows[0]["IdGrupo"].ToString());
                chkActivo.Checked = bool.Parse(tabla.Rows[0]["Activo"].ToString());
                //las sig. lineas presentan los controles relacionados con el estado del usuario.
                chkActivo.Visible = true;
                lblActivo.Visible = true;
                //las sig. lineas ocultan los controles relacionados con el password.
                //En caso de una Modificacion no tiene sentido obligar a pisar el pass 
                //del usuario al cual solo se le deben modificar datos
                lblPass.Visible = false;
                txtPass.Visible = false;
                lblConfPass.Visible = false;
                txtConfPass.Visible = false;
            }
        }

        private bool Validar()
        {
            string cadenaSQL;
            DataTable tabla;
            bool TodoOk;

            TodoOk = false;
            if (txtUsuario.Text.Trim() != string.Empty && txtUsuario.Text.Trim().Length >= 5)
            {
                if (txtApellido.Text.Trim() != string.Empty)
                {
                    if (txtNombre.Text.Trim() != string.Empty)
                    {
                        if (this.IdSelec == 0)
                        {
                            //si la operacion es ALTA, se deberan verificar las cajas de texto asociadas al password
                            if (txtPass.Text.Trim() != string.Empty && txtPass.Text.Trim().Length >= 5)
                            {
                                if (txtPass.Text.Trim() == txtConfPass.Text.Trim())
                                {
                                    cadenaSQL = $"select * from Usuarios where Usuario = '{txtUsuario.Text.Trim()}'";
                                    tabla = new DataTable();
                                    tabla = Libreria.Recuperar(cadenaSQL);
                                    if (tabla.Rows.Count == 0)
                                    {
                                        TodoOk = true;
                                    }
                                    else
                                    {
                                        MessageBox.Show("El Usuario ingresado se encuentra asignado.", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("El Password ingresado y su Confirmacion no coinciden.", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                            else
                            {
                                MessageBox.Show("El Password debe contener 5 o mas caracteres.", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            //si la operacion es MODIFICAR, no se deberan verificar las cajas de texto asociadas al password
                            //ya que se encuentran ocultas
                            cadenaSQL = $"select * from Usuarios where Usuario = '{txtUsuario.Text.Trim()}' and IdUsuario <> {this.IdSelec}";
                            tabla = new DataTable();
                            tabla = Libreria.Recuperar(cadenaSQL);
                            if (tabla.Rows.Count == 0)
                            {
                                TodoOk = true;
                            }
                            else
                            {
                                MessageBox.Show("El Usuario ingresado se encuentra asignado.", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Debe ingresar el Nombre.", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Debe ingresar el Apellido.", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("El Usuario debe contener 5 o mas caracteres.", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return TodoOk;
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            string cadenaSQL;

            if (Validar())
            {
                //el sig. condicional determina la cadena sql a ejecutar en funcion de la operacion seleccionada
                //nota: nunca se debe almacenar un password tal cual fue ingresado, es una norma basica de seguridad,
                //por ello se recurre a alguna funcion de encriptacion, en este caso hash256
                if (this.IdSelec == 0)
                {
                    cadenaSQL = "INSERT INTO Usuarios (Apellido,Nombre,IdGrupo,Activo,Usuario,Pass,IntentosFallidos) ";
                    cadenaSQL = cadenaSQL + $" VALUES('{txtApellido.Text.Trim()}','{txtNombre.Text.Trim()}',";
                    cadenaSQL = cadenaSQL + cmbGrupo.SelectedValue + $",1,'{txtUsuario.Text.Trim()}',";
                    cadenaSQL = cadenaSQL + $"'{Libreria.GenerarHash256(txtPass.Text)}',0)";
                }
                else
                {
                    cadenaSQL = "UPDATE Usuarios ";
                    cadenaSQL = cadenaSQL + $" SET Apellido = '{txtApellido.Text.Trim()}',";
                    cadenaSQL = cadenaSQL + $" Nombre = '{txtNombre.Text.Trim()} ',";
                    cadenaSQL = cadenaSQL + $" IdGrupo = {cmbGrupo.SelectedValue},";
                    if (chkActivo.Checked)
                    {
                        cadenaSQL = cadenaSQL + " Activo = 1 ";
                    }
                    else
                    {
                        cadenaSQL = cadenaSQL + " Activo = 0 ";
                    }
                    cadenaSQL = cadenaSQL + " where IdUsuario = " + this.IdSelec;
                }

                try
                {
                    Libreria.Ejecutar(cadenaSQL);
                    MessageBox.Show("Operacion Realizada.", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("La operacion fallida: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

    }
}
