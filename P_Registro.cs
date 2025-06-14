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
using System.Security.Cryptography;

namespace Administrador
{
    public partial class P_Registro : Form
    {
        public P_Registro()
        {
            InitializeComponent();
            txtContrasena.TextChanged += txtContrasena_TextChanged;
            txtConfContrasena.TextChanged += txtConfContrasena_TextChanged;
            btnRegistro.Enabled = false;
        }

        private void ValidarContrasenas()
        {
            bool coinciden = txtContrasena.Text == txtConfContrasena.Text && !string.IsNullOrWhiteSpace(txtContrasena.Text);
            btnRegistro.Enabled = coinciden;
        }

        private void txtContrasena_TextChanged(object sender, EventArgs e)
        {
            ValidarContrasenas();
        }

        private void txtConfContrasena_TextChanged(object sender, EventArgs e)
        {
            ValidarContrasenas();
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

        private void label6_Click(object sender, EventArgs e)
        {
            P_InicarSesion p_InicarSesion = new P_InicarSesion();
            p_InicarSesion.Show();
            this.Hide();
        }

        private void btnRegistro_Click(object sender, EventArgs e)
        {
            string conexion = "Data Source=.;Initial Catalog=GestionFinanciera;Integrated Security=True;";
            using (SqlConnection conexionBD = new SqlConnection(conexion))
            {
                try
                {
                    conexionBD.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_AgregarUsuario", conexionBD))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@nombre", txtNombre.Text.Trim());
                        cmd.Parameters.AddWithValue("@correo", txtCorreo.Text.Trim());
                        cmd.Parameters.AddWithValue("@contrasena", HashPassword(txtContrasena.Text));

                        object result = cmd.ExecuteScalar();
                        MessageBox.Show("Usuario registrado correctamente. ¡Bienvenido " + UsuarioSesion.Nombre + "!");
                        new P_Principal().Show();
                        this.Hide();
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Error al registrar usuario: " + ex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error inesperado: " + ex.Message);
                }
            }
        }
    }
}
