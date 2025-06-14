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

namespace Administrador
{
    public partial class P_InicarSesion : Form
    {
        public P_InicarSesion()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            P_Registro registro = new P_Registro();
            registro.Show();
            this.Hide();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            string conexion = "Data Source=.;Initial Catalog=GestionFinanciera;Integrated Security=True;";
            using (SqlConnection conexionBD = new SqlConnection(conexion))
            {
                try
                {
                    conexionBD.Open();
                    string query = "SELECT id, nombre FROM Usuario WHERE correo = @correo AND contrasena = @contrasena";
                    using (SqlCommand cmd = new SqlCommand(query, conexionBD))
                    {
                        cmd.Parameters.AddWithValue("@correo", txtCorreo.Text.Trim());
                        cmd.Parameters.AddWithValue("@contrasena", HashPassword(txtContrasena.Text));

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                UsuarioSesion.Id = Convert.ToInt32(reader["id"]);
                                UsuarioSesion.Nombre = reader["nombre"].ToString();

                                MessageBox.Show("¡Bienvenido " + UsuarioSesion.Nombre + "!");
                                new P_Principal().Show();
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("Correo o contraseña incorrectos.");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error inesperado: " + ex.Message);
                }
            }
        }
        
        private string HashPassword(string password)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
