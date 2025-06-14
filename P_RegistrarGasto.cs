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
    public partial class P_RegistrarGasto : Form
    {
        public P_RegistrarGasto()
        {
            InitializeComponent();
        }

        private void P_RegistrarGasto_Load(object sender, EventArgs e)
        {
            string conexion = "Data Source=.;Initial Catalog=GestionFinanciera;Integrated Security=True;";
            using (SqlConnection conexionBD = new SqlConnection(conexion))
            {
                try
                {
                    conexionBD.Open();
                    string query = "SELECT nombre FROM Categoria";
                    SqlCommand cmd = new SqlCommand(query, conexionBD);
                    SqlDataReader reader = cmd.ExecuteReader();

                    cmbCategoria.Items.Clear();
                    while (reader.Read())
                    {
                        cmbCategoria.Items.Add(reader["nombre"].ToString());
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar categorías: " + ex.Message);
                }
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string conexion = "Data Source=.;Initial Catalog=GestionFinanciera;Integrated Security=True;";
            using (SqlConnection conexionBD = new SqlConnection(conexion))
            {
                try
                {
                    conexionBD.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_RegistrarGasto", conexionBD))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        int usuarioId = UsuarioSesion.Id;

                        cmd.Parameters.AddWithValue("@nombre", txtNombre.Text);
                        cmd.Parameters.AddWithValue("@monto", decimal.Parse(txtMonto.Text));
                        cmd.Parameters.AddWithValue("@fecha", dtpFecha.Value.Date);
                        cmd.Parameters.AddWithValue("@recurrencia", chkRecurrente.Checked ? 1 : 0);
                        cmd.Parameters.AddWithValue("@categoriaId", cmbCategoria.SelectedIndex + 1);
                        cmd.Parameters.AddWithValue("@usuarioId", usuarioId);

                        object result = cmd.ExecuteScalar();
                        MessageBox.Show("Gasto registrado correctamente.");

                        txtMonto.Clear();
                        txtNombre.Clear();
                        dtpFecha.Value = DateTime.Now;
                        chkRecurrente.Checked = false;
                        cmbCategoria.SelectedIndex = -1;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al registrar ingreso: " + ex.Message);
                }
            }
        }

        private void btnMCategorias_Click(object sender, EventArgs e)
        {
            Form principal = this.MdiParent ?? this.Owner;
            if (principal == null)
                principal = Application.OpenForms.OfType<P_Principal>().FirstOrDefault();

            if (principal is P_Principal pPrincipal)
            {
                pPrincipal.AbrirFormularioEnPanel(new P_Categorias());
            }
            else
            {
                MessageBox.Show("No se pudo encontrar el formulario principal.");
            }
        }
    }
}
