using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EjerInmobiliaria
{
    public partial class frmLogueo : Form
    {
        public frmLogueo()
        {
            InitializeComponent();
        }

        private void frmLogueo_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            string cadenaSQL;
            DataTable tabla;

            if (txtUsuario.Text.Trim() != string.Empty && txtUsuario.Text.Trim().Length >= 5)
            {

                if (txtPass.Text.Trim() != string.Empty && txtPass.Text.Trim().Length >= 5)
                {
                    //la sig. linea define la sentencia SQL necesaria para recuperar los datos del usuario ingresado
                    cadenaSQL = $"select * from usuarios where Usuario = '{txtUsuario.Text}'";
                    tabla = new DataTable();
                    tabla = Libreria.Recuperar(cadenaSQL);
                    //el sig. condicional verifica si se encontro algun usuario que cumpla con la sentencia especificada
                    if (tabla.Rows.Count == 1)
                    {
                        if (int.Parse(tabla.Rows[0]["IntentosFallidos"].ToString()) < 3)
                        {
                            //el sig. condicional verifica si el pass ingresado coincide con el pass del usuario
                            if (Libreria.GenerarHash256(txtPass.Text) == tabla.Rows[0]["Pass"].ToString())
                            {
                                //el sig. condicional verifica si el usuario ingresado se encuentra activo
                                if (bool.Parse(tabla.Rows[0]["Activo"].ToString()))
                                {
                                    //las sig. lineas almacenan en las propiedades IdUsuarioLog y Acceso 
                                    //la informacion correspondiente al usuario logueado.
                                    //esta informacion sera de utilidad dentro de la aplicacion para poder determinar 
                                    //en cualquier momento el usuario que esta realizando la accion y si esta autorizado a realizarla.
                                    string sql;
                                    sql = $"update usuarios set intentosfallidos = 0 " +
                                    $"where idusuario = {tabla.Rows[0]["IdUsuario"].ToString()}";
                                    Libreria.Ejecutar(sql);

                                    Libreria.UsuarioLog = tabla.Rows[0]["Usuario"].ToString();

                                    Libreria.IdUsuarioLog = int.Parse(tabla.Rows[0]["IdUsuario"].ToString());
                                    cadenaSQL = $"select * from grupos where IdGrupo = {tabla.Rows[0]["IdGrupo"]}";
                                    tabla = new DataTable();
                                    tabla = Libreria.Recuperar(cadenaSQL);
                                    Libreria.GrupoLog = tabla.Rows[0]["Grupo"].ToString();
                                    
                                    this.Hide();
                                    FrmMenu frm = new FrmMenu();
                                    frm.Show();
                                    //el esquema de logueo y acceso presentado es simple, el objetivo es dar 
                                    //a conocer al alumno las bases de una aplicacion en la que participan diferentes categorias de usuarios.
                                    //Muchas acciones aqui presentadas podrian ampliarse/mejorarse, por ejemplo:
                                    //obligar al usuario modificar su pass en el primer acceso,
                                    //bloquear al usuario luego de una cierta cantidad de intentos fallidos, 
                                    //modificar el algoritmo de logueo por otro tipo de herramientas/metodologias/funciones mas complejas/seguras,
                                    //permitir al usuario modificar su pass,
                                    //poder comunicarse con el administrador en caso de inconvenientes con el acceso,
                                    //llevar un control de accesos al sistema,
                                    //organizar el acceso o visualizacion de opciones a las que tiene acceso un grupo 
                                    //de una manera diferente (ej: desde la base de datos), etc.
                                }
                                else
                                {
                                    MessageBox.Show("El Usuario no se encuentra Activo.", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                            else
                            {
                                string sql;

                                sql = $"update usuarios set intentosfallidos = intentosfallidos + 1 " +
                                    $"where idusuario = {tabla.Rows[0]["IdUsuario"].ToString()}";
                                Libreria.Ejecutar(sql);

                                MessageBox.Show("El logueo es incorrecto.", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Ha superado la cantidad de intentos, comuniquese con el administrador.", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("El logueo es incorrecto.", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("El Password debe contener 5 o mas caracteres.", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("El usuario debe contener 5 o mas caracteres.", "Atencion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
