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
    public partial class P_Transacciones : Form
    {
        public P_Transacciones()
        {
            InitializeComponent();
            CargarTransacciones();
        }

        private void CargarTransacciones()
        {
            string conexion = "Data Source=.;Initial Catalog=GestionFinanciera;Integrated Security=True;";
            using (SqlConnection conexionBD = new SqlConnection(conexion))
            {
                using (SqlCommand cmd = new SqlCommand("sp_TransaccionesPorUsuario", conexionBD))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@usuarioId", UsuarioSesion.Id);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                    {
                        if (row["Gasto/Ingreso"].ToString() == "Gasto")
                        {
                            row["Ahorrable/Utilizable"] = string.Empty;
                        }
                    }
                    dgvTransacciones.DataSource = dt;
                    dgvTransacciones.Columns["Id"].Visible = false;
                    dgvTransacciones.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvTransacciones.CurrentRow == null)
            {
                MessageBox.Show("Seleccione una transacción para eliminar.");
                return;
            }

            var confirmResult = MessageBox.Show(
                "¿Está seguro que desea eliminar la transacción seleccionada?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmResult != DialogResult.Yes)
                return;

            try
            {
                int id = Convert.ToInt32(dgvTransacciones.CurrentRow.Cells["Id"].Value);
                string tipo = dgvTransacciones.CurrentRow.Cells["Gasto/Ingreso"].Value.ToString();

                string conexion = "Data Source=.;Initial Catalog=GestionFinanciera;Integrated Security=True;";
                using (SqlConnection conexionBD = new SqlConnection(conexion))
                {
                    conexionBD.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_EliminarTransaccion", conexionBD))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@tipo", tipo);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Transacción eliminada correctamente.");
                CargarTransacciones();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al eliminar la transacción:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void P_Transacciones_Load(object sender, EventArgs e)
        {

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvTransacciones.CurrentRow == null)
            {
                MessageBox.Show("Seleccione una transacción para editar.");
                return;
            }

            int id = Convert.ToInt32(dgvTransacciones.CurrentRow.Cells["Id"].Value);
            string tipo = dgvTransacciones.CurrentRow.Cells["Gasto/Ingreso"].Value.ToString();

            if (tipo == "Ingreso")
            {
                var frm = new P_RegistrarIngreso();
                frm.CargarIngresoPorId(id);
                frm.ShowDialog();
            }
            else if (tipo == "Gasto")
            {
                var frm = new P_RegistrarGasto();
                frm.CargarGastoPorId(id);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Tipo de transacción desconocido.");
            }
        }
    }
}
