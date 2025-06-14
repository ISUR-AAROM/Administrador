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
using System.Windows.Forms.DataVisualization.Charting;

namespace Administrador
{
    public partial class P_Dashboard : Form
    {
        public P_Dashboard()
        {
            InitializeComponent();
            dtpFechaInicio.ValueChanged += dtpFechaInicio_ValueChanged;
            dtpFechaFin.ValueChanged += dtpFechaFin_ValueChanged;
        }

        private void P_Dashboard_Load(object sender, EventArgs e)
        {
            CargarGraficosBarras();
        }

        private void dtpFechaInicio_ValueChanged(object sender, EventArgs e)
        {
            CargarGraficosBarras();
        }

        private void dtpFechaFin_ValueChanged(object sender, EventArgs e)
        {
            CargarGraficosBarras();
        }

        private void CargarGraficosBarras()
        {
            try
            {
                string conexion = "Data Source=.;Initial Catalog=GestionFinanciera;Integrated Security=True;";
                using (SqlConnection conexionBD = new SqlConnection(conexion))
                {
                    conexionBD.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_DatosGraficosBarras", conexionBD))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@usuarioId", UsuarioSesion.Id);
                        cmd.Parameters.AddWithValue("@fechaInicio", dtpFechaInicio.Value.Date);
                        cmd.Parameters.AddWithValue("@fechaFin", dtpFechaFin.Value.Date);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                decimal totalIngresos = reader.GetDecimal(reader.GetOrdinal("TotalIngresos"));
                                decimal totalGastos = reader.GetDecimal(reader.GetOrdinal("TotalGastos"));
                                decimal balance = reader.GetDecimal(reader.GetOrdinal("Balance"));
                                decimal montoAhorrable = reader.GetDecimal(reader.GetOrdinal("MontoAhorrable"));
                                decimal montoUtilizable = reader.GetDecimal(reader.GetOrdinal("MontoUtilizable"));

                                if (totalIngresos == 0 && totalGastos == 0 && balance == 0 && montoAhorrable == 0 && montoUtilizable == 0)
                                {
                                    chartBalance.Series.Clear();
                                    chartIngresos.Series.Clear();
                                    return;
                                }

                                chartBalance.Series.Clear();
                                Series serieBalance = new Series
                                {
                                    Name = "BalanceGeneral",
                                    ChartType = SeriesChartType.Column
                                };
                                serieBalance.Points.AddXY("Balance", balance);
                                serieBalance.Points.AddXY("Utilizable", montoUtilizable);
                                serieBalance.Points.AddXY("Ahorrable", montoAhorrable);
                                serieBalance.Points[0].Color = Color.MediumSeaGreen;
                                serieBalance.Points[1].Color = Color.Goldenrod;
                                serieBalance.Points[2].Color = Color.CornflowerBlue;

                                chartBalance.Series.Add(serieBalance);

                                chartIngresos.Series.Clear();
                                Series serieIngresos = new Series
                                {
                                    Name = "IngresosYGastos",
                                    ChartType = SeriesChartType.Column
                                };
                                serieIngresos.Points.AddXY("Ingresos", totalIngresos);
                                serieIngresos.Points.AddXY("Gastos", totalGastos);
                                serieIngresos.Points[0].Color = Color.MediumSeaGreen;
                                serieIngresos.Points[1].Color = Color.Crimson;
                                chartIngresos.Series.Add(serieIngresos);

                            }
                            else
                            {
                                chartBalance.Series.Clear();
                                chartIngresos.Series.Clear();
                                MessageBox.Show("No hay datos para el rango seleccionado.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al cargar los gráficos:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
