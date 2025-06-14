using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Administrador
{
    public partial class P_Principal : Form
    {
        public P_Principal()
        {
            InitializeComponent();
            btnRIngreso.Click += btnRIngreso_Click;
            this.Load += P_Principal_Load;
        }

        public void AbrirFormularioEnPanel(Form formulario)
        {
            panelFormulario.Controls.Clear();
            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;
            panelFormulario.Controls.Add(formulario);
            formulario.Show();
        }

        private void panelFormulario_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnRIngreso_Click(object sender, EventArgs e)
        {
            AbrirFormularioEnPanel(new P_RegistrarIngreso());
            lblTitulo.Text = "Registrar Ingreso";

        }

        private void btnRGasto_Click(object sender, EventArgs e)
        {
            AbrirFormularioEnPanel(new P_RegistrarGasto());
            lblTitulo.Text = "Registrar Gasto";
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            AbrirFormularioEnPanel(new P_Dashboard());
            lblTitulo.Text = "Dashboard";
        }

        private void P_Principal_Load(object sender, EventArgs e)
        {
            AbrirFormularioEnPanel(new P_Dashboard());
            lblNombre.Text = UsuarioSesion.Nombre;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
