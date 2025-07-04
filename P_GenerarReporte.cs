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
using iTextSharp.text;
using iTextSharp.text.pdf;

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
                cmbCategorias.Text = string.Empty;
            }
            else
            {
                cmbCategorias.Enabled = true;
                cmbCategorias.Text = string.Empty;
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
                        cmd.Parameters.AddWithValue("@fechaInicio", dtpInicio.Value);
                        cmd.Parameters.AddWithValue("@fechaFin", dtpFin.Value);

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

                using (FileStream fs = new FileStream(lblUbicacion.Text, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    using (Document document = new Document(PageSize.A4, 40, 40, 40, 40))
                    {
                        PdfWriter writer = PdfWriter.GetInstance(document, fs);
                        document.Open();

                        var fontTitle = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16);
                        var fontBold = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
                        var font = FontFactory.GetFont(FontFactory.HELVETICA, 10);

                        document.Add(new Paragraph("Reporte Financiero", fontTitle) { Alignment = Element.ALIGN_CENTER });
                        document.Add(new Paragraph($"Usuario: {UsuarioSesion.Nombre}", font));
                        document.Add(new Paragraph($"Periodo: {fechaInicio.ToShortDateString()} - {fechaFin.ToShortDateString()}", font));
                        document.Add(new Paragraph($"Total Ingresos: {totalIngresos:C2}", font));
                        document.Add(new Paragraph($"Total Gastos: {totalGastos:C2}", font));
                        document.Add(new Paragraph($"Balance: {balance:C2}", font));
                        document.Add(new Paragraph($"Ahorro Total: {ahorroTotal:C2}", font));
                        document.Add(new Paragraph(" "));

                        iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(ms.ToArray());
                        img.ScaleToFit(400f, 200f);
                        img.Alignment = Element.ALIGN_CENTER;
                        document.Add(img);

                        document.Add(new Paragraph(" "));

                        document.Add(new Paragraph("Transacciones:", fontBold));
                        document.Add(new Paragraph(" "));
                        PdfPTable table = new PdfPTable(5);
                        table.WidthPercentage = 100;
                        table.SetWidths(new float[] { 1.2f, 2f, 2f, 1.2f, 1.5f });

                        table.AddCell(new PdfPCell(new Phrase("Tipo", fontBold)));
                        table.AddCell(new PdfPCell(new Phrase("Nombre", fontBold)));
                        table.AddCell(new PdfPCell(new Phrase("Categoría", fontBold)));
                        table.AddCell(new PdfPCell(new Phrase("Monto", fontBold)));
                        table.AddCell(new PdfPCell(new Phrase("Fecha", fontBold)));

                        foreach (var t in transacciones)
                        {
                            table.AddCell(new PdfPCell(new Phrase(t.Item1, font)));
                            table.AddCell(new PdfPCell(new Phrase(t.Item2, font)));
                            table.AddCell(new PdfPCell(new Phrase(t.Item3, font)));
                            table.AddCell(new PdfPCell(new Phrase(t.Item4.ToString("C2"), font)));
                            table.AddCell(new PdfPCell(new Phrase(t.Item5.ToShortDateString(), font)));
                        }

                        document.Add(table);

                        document.Close();
                        writer.Close();
                    }
                }
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
