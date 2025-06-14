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
    public partial class P_Categorias : Form
    {
        public P_Categorias()
        {
            InitializeComponent();
        }

        private void P_Categorias_Load(object sender, EventArgs e)
        {
            //string conexion = "Data Source=.;Initial Catalog=GestionFinanciera;Integrated Security=True;";
            //using (SqlConnection conexionBD = new SqlConnection(conexion))
            //{
            //    using (SqlCommand cmd = new SqlCommand("sp_TransaccionesPorUsuario", conexionBD))
            //    {
            //        cmd.CommandType = CommandType.StoredProcedure;
            //        cmd.Parameters.AddWithValue("@usuarioId", UsuarioSesion.Id);
            //        SqlDataAdapter da = new SqlDataAdapter(cmd);
            //        DataTable dt = new DataTable();
            //        da.Fill(dt);
            //        dgvCategorias.DataSource = dt;
            //        dgvCategorias.Columns["Id"].Visible = false;
            //        dgvCategorias.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //    }
            //}
        }
    }
}
