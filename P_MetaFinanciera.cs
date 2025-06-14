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
    public partial class P_MetaFinanciera : Form
    {
        public P_MetaFinanciera()
        {
            InitializeComponent();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Form principal = this.MdiParent ?? this.Owner;
            if (principal == null)
                principal = Application.OpenForms.OfType<P_Principal>().FirstOrDefault();

            if (principal is P_Principal pPrincipal)
            {
                pPrincipal.AbrirFormularioEnPanel(new P_EditarMeta());
            }
            else
            {
                MessageBox.Show("No se pudo encontrar el formulario principal.");
            }
        }

        private void P_MetaFinanciera_Load(object sender, EventArgs e)
        {
            try
            {
                string conexion = "Data Source=.;Initial Catalog=GestionFinanciera;Integrated Security=True;";
                using (SqlConnection conexionBD = new SqlConnection(conexion))
                {
                    conexionBD.Open();

                    using (SqlCommand cmd = new SqlCommand("sp_ActualizarEstadoMetaFinanciera", conexionBD))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@usuarioId", UsuarioSesion.Id);
                        cmd.ExecuteNonQuery();
                    }

                    using (SqlCommand cmd = new SqlCommand("sp_ObtenerMetaFinancieraUsuario", conexionBD))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@usuarioId", UsuarioSesion.Id);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                lblIngreso.Text = reader["montoIngreso"].ToString();
                                lblAhorro.Text = reader["montoAhorro"].ToString();
                                lblGasto.Text = reader["limiteGasto"].ToString();
                                lblEstado.Text = reader["estado"].ToString();
                                DateTime fechaFin = Convert.ToDateTime(reader["fechaFin"]);
                                lblFechaFin.Text = fechaFin.ToShortDateString();

                                int diasRestantes = (fechaFin - DateTime.Today).Days;
                                lblDiasRestantes.Text = diasRestantes >= 0 ? diasRestantes.ToString() : "0";
                            }
                            else
                            {
                                lblIngreso.Text = "-";
                                lblAhorro.Text = "-";
                                lblGasto.Text = "-";
                                lblEstado.Text = "-";
                                lblFechaFin.Text = "-";
                                lblDiasRestantes.Text = "-";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar la meta financiera:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show(
        "¿Está seguro que desea eliminar la meta financiera?",
        "Confirmar eliminación",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Warning);

            if (confirmResult != DialogResult.Yes)
                return;

            try
            {
                string conexion = "Data Source=.;Initial Catalog=GestionFinanciera;Integrated Security=True;";
                using (SqlConnection conexionBD = new SqlConnection(conexion))
                {
                    conexionBD.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_EliminarMetaFinanciera", conexionBD))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@usuarioId", UsuarioSesion.Id);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Meta financiera eliminada correctamente.");
                lblIngreso.Text = "-";
                lblAhorro.Text = "-";
                lblGasto.Text = "-";
                lblEstado.Text = "-";
                lblFechaFin.Text = "-";
                lblDiasRestantes.Text = "-";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar la meta financiera:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
