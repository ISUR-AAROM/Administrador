using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Administrador
{
    public partial class P_RegistrarIngreso : Form
    {
        private int transaccionId = 0;
        public P_RegistrarIngreso()
        {
            InitializeComponent();
        }

        private void P_RegistrarIngreso_Load(object sender, EventArgs e)
        {
            string conexion = "Data Source=.;Initial Catalog=GestionFinanciera;Integrated Security=True;";
            using (SqlConnection conexionBD = new SqlConnection(conexion))
            {
                try
                {
                    cmbCategoria.Items.Clear();
                    using (SqlCommand cmd = new SqlCommand("SELECT id, nombre FROM Categoria WHERE TipoCategoria = 1", conexionBD))
                    {
                        conexionBD.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string nombre = reader.GetString(1);
                            cmbCategoria.Items.Add(new KeyValuePair<int, string>(id, nombre));
                        }
                        reader.Close();
                    }
                    cmbCategoria.DisplayMember = "Value";
                    cmbCategoria.ValueMember = "Key";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar categorías: " + ex.Message);
                }
            }
        }

        public void CargarIngresoPorId(int ingresoId)
        {
            string conexion = "Data Source=.;Initial Catalog=GestionFinanciera;Integrated Security=True;";
            using (SqlConnection conexionBD = new SqlConnection(conexion))
            {
                conexionBD.Open();
                using (SqlCommand cmd = new SqlCommand("sp_ObtenerIngresoPorId", conexionBD))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", ingresoId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            transaccionId = reader.GetInt32(reader.GetOrdinal("Id"));
                            txtNombre.Text = reader["Nombre"].ToString();
                            txtMonto.Text = Convert.ToDecimal(reader["Monto"]).ToString("F2");
                            dtpFecha.Value = Convert.ToDateTime(reader["Fecha"]);
                            chkRecurrente.Checked = Convert.ToBoolean(reader["Recurrencia"]);
                            cmbTipoIngreso.SelectedItem = reader["TipoIngreso"].ToString();
                            txtPorcentaje.Text = reader["PorcentajeAhorro"].ToString();

                            int categoriaIdBD = Convert.ToInt32(reader["CategoriaId"]);
                            for (int i = 0; i < cmbCategoria.Items.Count; i++)
                            {
                                var item = (KeyValuePair<int, string>)cmbCategoria.Items[i];
                                if (item.Key == categoriaIdBD)
                                {
                                    cmbCategoria.SelectedIndex = i;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void cmbTipoIngreso_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTipoIngreso.Text == "Ahorrable")
            {
                txtPorcentaje.Enabled = true;
                label7.Enabled = true;
            }
            else
            {
                txtPorcentaje.Enabled = false;
                label7.Enabled = false;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            decimal porcentajeAhorro = 0;
            if (!string.IsNullOrWhiteSpace(txtPorcentaje.Text))
            {
                if (!decimal.TryParse(txtPorcentaje.Text, out porcentajeAhorro))
                {
                    MessageBox.Show("El porcentaje de ahorro debe ser un número válido.");
                    return;
                }
                porcentajeAhorro = porcentajeAhorro / 100m;
            }

            int categoriaId = ((KeyValuePair<int, string>)cmbCategoria.SelectedItem).Key;
            string conexion = "Data Source=.;Initial Catalog=GestionFinanciera;Integrated Security=True;";
            using (SqlConnection conexionBD = new SqlConnection(conexion))
            {
                try
                {
                    conexionBD.Open();

                    SqlCommand cmd;
                    if (transaccionId != 0)
                    {
                        cmd = new SqlCommand("sp_EditarIngreso", conexionBD);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", transaccionId);
                    }
                    else
                    {
                        cmd = new SqlCommand("sp_RegistrarIngreso", conexionBD);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    }

                    int usuarioId = UsuarioSesion.Id;

                    cmd.Parameters.AddWithValue("@nombre", txtNombre.Text);
                    cmd.Parameters.AddWithValue("@monto", decimal.Parse(txtMonto.Text));
                    cmd.Parameters.AddWithValue("@fecha", dtpFecha.Value.Date);
                    cmd.Parameters.AddWithValue("@recurrencia", chkRecurrente.Checked ? 1 : 0);
                    cmd.Parameters.AddWithValue("@categoriaId", categoriaId);
                    cmd.Parameters.AddWithValue("@usuarioId", usuarioId);
                    cmd.Parameters.AddWithValue("@tipoIngreso", cmbTipoIngreso.SelectedItem?.ToString() ?? "");
                    cmd.Parameters.AddWithValue("@porcentajeAhorro", porcentajeAhorro);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show(transaccionId != 0 ? "Ingreso editado correctamente." : "Ingreso registrado correctamente.");

                    txtMonto.Clear();
                    txtNombre.Clear();
                    dtpFecha.Value = DateTime.Now;
                    chkRecurrente.Checked = false;
                    cmbCategoria.SelectedIndex = -1;
                    cmbTipoIngreso.SelectedIndex = -1;
                    txtPorcentaje.Clear();
                    transaccionId = 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al registrar/editar ingreso: " + ex.Message);
                }
            }
        }

        private void txtMonto_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPorcentaje_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPorcentaje_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            if (e.KeyChar == '.' && (sender as TextBox).Text.Contains("."))
            {
                e.Handled = true;
            }
        }

        private void txtMonto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            // Permite solo un punto decimal
            if (e.KeyChar == '.' && (sender as TextBox).Text.Contains("."))
            {
                e.Handled = true;
            }
        }
    }
}
