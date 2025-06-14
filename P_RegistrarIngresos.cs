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
    public partial class P_RegistrarIngreso : Form
    {
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
            string conexion = "Data Source=.;Initial Catalog=GestionFinanciera;Integrated Security=True;";
            using (SqlConnection conexionBD = new SqlConnection(conexion))
            {
                try
                {
                    conexionBD.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_RegistrarIngreso", conexionBD))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        int usuarioId = UsuarioSesion.Id;
                        decimal porcentajeAhorro = 0.0000m;
                        if (cmbTipoIngreso.Text == "Ahorrable" && !string.IsNullOrWhiteSpace(txtPorcentaje.Text))
                        {
                            if (decimal.TryParse(txtPorcentaje.Text, out decimal porcentajeEntero) && porcentajeEntero >= 0 && porcentajeEntero <= 100)
                            {
                                porcentajeAhorro = porcentajeEntero / 100.0m;
                            }
                            else
                            {
                                MessageBox.Show("El porcentaje de ahorro debe ser un número entre 0 y 100.");
                                return;
                            }
                        }

                        cmd.Parameters.AddWithValue("@nombre", txtNombre.Text);
                        cmd.Parameters.AddWithValue("@monto", decimal.Parse(txtMonto.Text));
                        cmd.Parameters.AddWithValue("@fecha", dtpFecha.Value.Date);
                        cmd.Parameters.AddWithValue("@recurrencia", chkRecurrente.Checked ? 1 : 0);
                        cmd.Parameters.AddWithValue("@tipoIngreso", cmbTipoIngreso.Text);
                        cmd.Parameters.AddWithValue("@porcentajeAhorro", porcentajeAhorro);
                        cmd.Parameters.AddWithValue("@categoriaId", cmbCategoria.SelectedIndex + 1);
                        cmd.Parameters.AddWithValue("@usuarioId", usuarioId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                MessageBox.Show(
                                    $"Ingreso registrado correctamente.\nID: {reader["IngresoId"]}\n" +
                                    $"Porcentaje ahorro decimal: {reader["PorcentajeAhorroDecimal"]}\n" +
                                    $"Monto ahorro calculado: {reader["MontoAhorroCalculado"]}"
                                );
                            }
                        }

                        txtMonto.Clear();
                        txtNombre.Clear();
                        dtpFecha.Value = DateTime.Now;
                        chkRecurrente.Checked = false;
                        cmbTipoIngreso.SelectedIndex = -1;
                        cmbCategoria.SelectedIndex = -1;
                        txtPorcentaje.Clear();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al registrar ingreso: " + ex.Message);
                }
            }
        }
    }
}
