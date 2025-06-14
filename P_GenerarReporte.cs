using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.Windows.Forms.DataVisualization.Charting;

namespace Administrador
{
    public partial class P_GenerarReporte : Form
    {
        public P_GenerarReporte()
        {
            InitializeComponent();
        }

        private void btnUbicacion_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Archivo PDF (*.pdf)|*.pdf";
                saveFileDialog.Title = "Seleccionar ubicación para guardar el PDF";
                saveFileDialog.DefaultExt = "pdf";
                saveFileDialog.AddExtension = true;
                saveFileDialog.FileName = "Reporte.pdf";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    lblUbicacion.Text = saveFileDialog.FileName;
                }
            }
        }

        private void chkTodas_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTodas.Checked)
            {
                cmbCategorias.Enabled = false;
            }
            else
            {
                cmbCategorias.Enabled = true;
                cmbCategorias.Text = " ";
            }
        }

        private void P_GenerarReporte_Load(object sender, EventArgs e)
        {
            try
            {
                string conexion = "Data Source=.;Initial Catalog=GestionFinanciera;Integrated Security=True;";
                using (SqlConnection conexionBD = new SqlConnection(conexion))
                {
                    conexionBD.Open();
                    string query = "SELECT nombre FROM Categoria WHERE activo = 1";
                    using (SqlCommand cmd = new SqlCommand(query, conexionBD))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            cmbCategorias.Items.Clear();
                            while (reader.Read())
                            {
                                cmbCategorias.Items.Add(reader["nombre"].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar categorías: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPDF_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(lblUbicacion.Text))
            {
                MessageBox.Show("Seleccione una ubicación para guardar el PDF.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                decimal totalIngresos = 0, totalGastos = 0, balance = 0, ahorroTotal = 0;
                DateTime fechaInicio = DateTime.Now, fechaFin = DateTime.Now;
                var transacciones = new List<Tuple<string, string, string, decimal, DateTime>>();

                string conexion = "Data Source=.;Initial Catalog=GestionFinanciera;Integrated Security=True;";
                using (SqlConnection conexionBD = new SqlConnection(conexion))
                {
                    conexionBD.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_GenerarReporte", conexionBD))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@usuarioId", UsuarioSesion.Id);
                        cmd.Parameters.AddWithValue("@fechaInicio", DateTime.Now.AddMonths(-1).Date);
                        cmd.Parameters.AddWithValue("@fechaFin", DateTime.Now.Date);

                        if (chkTodas.Checked)
                            cmd.Parameters.AddWithValue("@categoriaId", DBNull.Value);
                        else
                        {
                            if (cmbCategorias.SelectedItem == null)
                            {
                                MessageBox.Show("Seleccione una categoría.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            string categoriaNombre = cmbCategorias.SelectedItem.ToString();
                            int categoriaId = ObtenerIdCategoriaPorNombre(categoriaNombre);
                            cmd.Parameters.AddWithValue("@categoriaId", categoriaId);
                        }

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                totalIngresos = reader.GetDecimal(reader.GetOrdinal("TotalIngresos"));
                                totalGastos = reader.GetDecimal(reader.GetOrdinal("TotalGastos"));
                                balance = reader.GetDecimal(reader.GetOrdinal("Balance"));
                                ahorroTotal = reader.GetDecimal(reader.GetOrdinal("AhorroTotal"));
                                fechaInicio = reader.GetDateTime(reader.GetOrdinal("FechaInicio"));
                                fechaFin = reader.GetDateTime(reader.GetOrdinal("FechaFin"));
                            }

                            if (reader.NextResult())
                            {
                                while (reader.Read())
                                {
                                    string tipo = reader.GetString(0);
                                    string nombre = reader.GetString(1);
                                    string categoria = reader.GetString(2);
                                    decimal monto = reader.GetDecimal(3);
                                    DateTime fecha = reader.GetDateTime(4);
                                    transacciones.Add(Tuple.Create(tipo, nombre, categoria, monto, fecha));
                                }
                            }
                        }
                    }
                }

                Chart chartResumen = GenerarChartResumen(totalIngresos, totalGastos, ahorroTotal);
                MemoryStream ms = new MemoryStream();
                chartResumen.SaveImage(ms, ChartImageFormat.Png);
                ms.Position = 0;

                // 3. Crear el PDF con PDFsharp
                PdfDocument document = new PdfDocument();
                document.Info.Title = "Reporte Financiero";
                PdfPage page = document.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);
                XFont font = new XFont("Verdana", 10, XFontStyle.Regular);
                XFont fontBold = new XFont("Verdana", 12, XFontStyle.Bold);

                double y = 40;

                // Título y totales
                gfx.DrawString("Reporte Financiero", fontBold, XBrushes.Black, new XRect(0, y, page.Width, 20), XStringFormats.TopCenter);
                y += 30;
                gfx.DrawString($"Usuario: {UsuarioSesion.Nombre}", font, XBrushes.Black, 40, y); y += 20;
                gfx.DrawString($"Periodo: {fechaInicio.ToShortDateString()} - {fechaFin.ToShortDateString()}", font, XBrushes.Black, 40, y); y += 30;
                gfx.DrawString($"Total Ingresos: {totalIngresos:C2}", font, XBrushes.Black, 40, y); y += 20;
                gfx.DrawString($"Total Gastos: {totalGastos:C2}", font, XBrushes.Black, 40, y); y += 20;
                gfx.DrawString($"Balance: {balance:C2}", font, XBrushes.Black, 40, y); y += 20;
                gfx.DrawString($"Ahorro Total: {ahorroTotal:C2}", font, XBrushes.Black, 40, y); y += 30;

                // 4. Insertar el gráfico de barras
                XImage img = XImage.FromStream(ms);
                gfx.DrawImage(img, 40, y, 400, 200);
                y += 220;

                // 5. Encabezado de tabla de transacciones
                gfx.DrawString("Transacciones:", fontBold, XBrushes.Black, 40, y); y += 20;
                gfx.DrawString("Tipo", fontBold, XBrushes.Black, 40, y);
                gfx.DrawString("Nombre", fontBold, XBrushes.Black, 100, y);
                gfx.DrawString("Categoría", fontBold, XBrushes.Black, 220, y);
                gfx.DrawString("Monto", fontBold, XBrushes.Black, 340, y);
                gfx.DrawString("Fecha", fontBold, XBrushes.Black, 420, y);
                y += 20;

                // 6. Detalle de transacciones
                foreach (var t in transacciones)
                {
                    gfx.DrawString(t.Item1, font, XBrushes.Black, 40, y);
                    gfx.DrawString(t.Item2, font, XBrushes.Black, 100, y);
                    gfx.DrawString(t.Item3, font, XBrushes.Black, 220, y);
                    gfx.DrawString(t.Item4.ToString("C2"), font, XBrushes.Black, 340, y);
                    gfx.DrawString(t.Item5.ToShortDateString(), font, XBrushes.Black, 420, y);
                    y += 18;
                    if (y > page.Height - 40)
                    {
                        page = document.AddPage();
                        gfx = XGraphics.FromPdfPage(page);
                        y = 40;
                    }
                }

                document.Save(lblUbicacion.Text);
                document.Close();
                ms.Dispose();

                MessageBox.Show("Reporte PDF generado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el PDF:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Chart GenerarChartResumen(decimal totalIngresos, decimal totalGastos, decimal ahorroTotal)
        {
            Chart chart = new Chart();
            chart.Width = 600;
            chart.Height = 300;
            ChartArea area = new ChartArea();
            chart.ChartAreas.Add(area);

            Series serie = new Series
            {
                ChartType = SeriesChartType.Column
            };
            serie.Points.AddXY("Ingresos", totalIngresos);
            serie.Points.AddXY("Gastos", totalGastos);
            serie.Points.AddXY("Ahorro", ahorroTotal);

            chart.Series.Add(serie);
            return chart;
        }

        private int ObtenerIdCategoriaPorNombre(string nombreCategoria)
        {
            string conexion = "Data Source=.;Initial Catalog=GestionFinanciera;Integrated Security=True;";
            using (SqlConnection conexionBD = new SqlConnection(conexion))
            {
                conexionBD.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT id FROM Categoria WHERE nombre = @nombre", conexionBD))
                {
                    cmd.Parameters.AddWithValue("@nombre", nombreCategoria);
                    object result = cmd.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out int id))
                        return id;
                    throw new Exception("No se encontró la categoría seleccionada.");
                }
            }
        }
    }
}
