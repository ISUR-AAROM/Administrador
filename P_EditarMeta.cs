﻿using System;
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
    public partial class P_EditarMeta : Form
    {
        public P_EditarMeta()
        {
            InitializeComponent();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Form principal = this.MdiParent ?? this.Owner;
            if (principal == null)
                principal = Application.OpenForms.OfType<P_Principal>().FirstOrDefault();

            if (principal is P_Principal pPrincipal)
            {
                pPrincipal.AbrirFormularioEnPanel(new P_MetaFinanciera());
            }
            else
            {
                MessageBox.Show("No se pudo encontrar el formulario principal.");
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                decimal montoIngreso = decimal.Parse(txtIngreso.Text);
                decimal montoAhorro = decimal.Parse(txtAhorros.Text);
                decimal limiteGasto = decimal.Parse(txtGastos.Text);
                DateTime fechaFin = dtpFechaLim.Value.Date;
                DateTime fechaInicio = dtpFechaInicio.Value.Date;

                string conexion = "Data Source=.;Initial Catalog=GestionFinanciera;Integrated Security=True;";
                using (SqlConnection conexionBD = new SqlConnection(conexion))
                {
                    conexionBD.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_GuardarMetaFinanciera", conexionBD))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@usuarioId", UsuarioSesion.Id);
                        cmd.Parameters.AddWithValue("@montoIngreso", montoIngreso);
                        cmd.Parameters.AddWithValue("@montoAhorro", montoAhorro);
                        cmd.Parameters.AddWithValue("@limiteGasto", limiteGasto);
                        cmd.Parameters.AddWithValue("@fechaInicio", fechaInicio);
                        cmd.Parameters.AddWithValue("@fechaFin", fechaFin);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                MessageBox.Show(
                                    $"Meta guardada correctamente.\nEstado calculado: {reader["EstadoCalculado"]}",
                                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar la meta financiera:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
