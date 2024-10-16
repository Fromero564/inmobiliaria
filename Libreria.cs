using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace EjerInmobiliaria
{
    public static class Libreria
    {
        private static int _IdUsuarioLog;
        private static string _GrupoLog;
        private static string _UsuarioLog;

        public static int IdUsuarioLog
        {
            get { return _IdUsuarioLog; }
            set { _IdUsuarioLog = value; }
        }
        public static string GrupoLog
        {
            get { return _GrupoLog; }
            set { _GrupoLog = value; }
        }

        public static string UsuarioLog
        {
            get { return _UsuarioLog; }
            set { _UsuarioLog = value; }
        }

        public static void Ejecutar(string sql)
        {
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.ConnectionStrings["mibase"].ConnectionString;
            conexion.Open();
            SqlCommand comando = new SqlCommand();
            comando.Connection = conexion;
            comando.CommandType = CommandType.Text;
            comando.CommandText = sql;
            comando.ExecuteNonQuery();
            conexion.Close();
        }

        public static DataTable Recuperar(string sql)
        {
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.ConnectionStrings["mibase"].ConnectionString;
            conexion.Open();
            SqlCommand comando = new SqlCommand();
            comando.Connection = conexion;
            comando.CommandType = CommandType.Text;
            comando.CommandText = sql;
            DataTable tabla = new DataTable();
            tabla.Load(comando.ExecuteReader());

            return tabla;
        }

        public static string GenerarHash256(string Cadena)
        {
            System.Security.Cryptography.SHA256CryptoServiceProvider AlgoritmoHash = new System.Security.Cryptography.SHA256CryptoServiceProvider();
            byte[] inputBytes;
            byte[] hashBytes;
            string Salida;

            inputBytes = System.Text.Encoding.UTF8.GetBytes(Cadena);
            hashBytes = AlgoritmoHash.ComputeHash(inputBytes);

            Salida = "";
            foreach (byte b in hashBytes)
            {
                Salida = Salida + b.ToString("x2");
            }

            return Salida;
        }
    }
}
