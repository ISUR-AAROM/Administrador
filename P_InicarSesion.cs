using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
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

                                if (chkRecordarInicio.Checked)
                                {
                                    Properties.Settings.Default.UsuarioId = UsuarioSesion.Id;
                                    Properties.Settings.Default.RecordarHasta = DateTime.Now.AddMinutes(10);
                                    Properties.Settings.Default.Save();
                                }
                                else
                                {
                                    Properties.Settings.Default.UsuarioId = 0;
                                    Properties.Settings.Default.RecordarHasta = DateTime.MinValue;
                                    Properties.Settings.Default.Save();
                                }

                                MessageBox.Show("¡Bienvenido " + UsuarioSesion.Nombre + "!");
                                this.Hide();
                                using (var principal = new P_Principal())
                                {
                                    principal.ShowDialog();
                                }
                                this.Close();
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
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha256.ComputeHash(bytes);
                StringBuilder builder = new StringBuilder();
                foreach (byte b in hash)
                    builder.Append(b.ToString("x2"));
                return builder.ToString();
            }
        }

        private void chkRecordarInicio_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void P_InicarSesion_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.UsuarioId > 0 && Properties.Settings.Default.RecordarHasta > DateTime.Now)
            {
                UsuarioSesion.Id = Properties.Settings.Default.UsuarioId;

                string conexion = "Data Source=.;Initial Catalog=GestionFinanciera;Integrated Security=True;";
                using (SqlConnection conexionBD = new SqlConnection(conexion))
                {
                    conexionBD.Open();
                    string query = "SELECT nombre FROM Usuario WHERE id = @id";
                    using (SqlCommand cmd = new SqlCommand(query, conexionBD))
                    {
                        cmd.Parameters.AddWithValue("@id", UsuarioSesion.Id);
                        object result = cmd.ExecuteScalar();
                        if (result != null)
                            UsuarioSesion.Nombre = result.ToString();
                        else
                            UsuarioSesion.Nombre = "";
                    }
                }

                this.Hide();
                using (var principal = new P_Principal())
                {
                    principal.ShowDialog();
                }
                this.Close();
            }
        }
    }
}
