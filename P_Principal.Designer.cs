namespace Administrador
{
    partial class P_Principal
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panelUsuario = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblNombre = new System.Windows.Forms.Label();
            this.btnDashboard = new System.Windows.Forms.Button();
            this.btnRIngreso = new System.Windows.Forms.Button();
            this.btnRGasto = new System.Windows.Forms.Button();
            this.btnTransacciones = new System.Windows.Forms.Button();
            this.btnMeta = new System.Windows.Forms.Button();
            this.btnReporte = new System.Windows.Forms.Button();
            this.panelTitulo = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.panelFormulario = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1.SuspendLayout();
            this.panelUsuario.SuspendLayout();
            this.panelTitulo.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.DarkSlateGray;
            this.flowLayoutPanel1.Controls.Add(this.panelUsuario);
            this.flowLayoutPanel1.Controls.Add(this.btnDashboard);
            this.flowLayoutPanel1.Controls.Add(this.btnRIngreso);
            this.flowLayoutPanel1.Controls.Add(this.btnRGasto);
            this.flowLayoutPanel1.Controls.Add(this.btnTransacciones);
            this.flowLayoutPanel1.Controls.Add(this.btnMeta);
            this.flowLayoutPanel1.Controls.Add(this.btnReporte);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(200, 450);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // panelUsuario
            // 
            this.panelUsuario.Controls.Add(this.button1);
            this.panelUsuario.Controls.Add(this.label1);
            this.panelUsuario.Controls.Add(this.lblNombre);
            this.panelUsuario.Location = new System.Drawing.Point(3, 3);
            this.panelUsuario.Name = "panelUsuario";
            this.panelUsuario.Size = new System.Drawing.Size(197, 97);
            this.panelUsuario.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.DarkSlateGray;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(3, 58);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(188, 36);
            this.button1.TabIndex = 2;
            this.button1.Text = "Cerrar sesión";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.LightSeaGreen;
            this.label1.Location = new System.Drawing.Point(10, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Bienvenido";
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombre.ForeColor = System.Drawing.Color.White;
            this.lblNombre.Location = new System.Drawing.Point(9, 23);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(160, 20);
            this.lblNombre.TabIndex = 0;
            this.lblNombre.Text = "@Nombre del usuario";
            // 
            // btnDashboard
            // 
            this.btnDashboard.FlatAppearance.BorderColor = System.Drawing.Color.DarkSlateGray;
            this.btnDashboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDashboard.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDashboard.ForeColor = System.Drawing.Color.White;
            this.btnDashboard.Location = new System.Drawing.Point(3, 106);
            this.btnDashboard.Name = "btnDashboard";
            this.btnDashboard.Size = new System.Drawing.Size(197, 50);
            this.btnDashboard.TabIndex = 0;
            this.btnDashboard.Text = "Dashboard";
            this.btnDashboard.UseVisualStyleBackColor = true;
            this.btnDashboard.Click += new System.EventHandler(this.btnDashboard_Click);
            // 
            // btnRIngreso
            // 
            this.btnRIngreso.FlatAppearance.BorderColor = System.Drawing.Color.DarkSlateGray;
            this.btnRIngreso.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRIngreso.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRIngreso.ForeColor = System.Drawing.Color.White;
            this.btnRIngreso.Location = new System.Drawing.Point(3, 162);
            this.btnRIngreso.Name = "btnRIngreso";
            this.btnRIngreso.Size = new System.Drawing.Size(197, 50);
            this.btnRIngreso.TabIndex = 2;
            this.btnRIngreso.Text = "Registrar ingreso";
            this.btnRIngreso.UseVisualStyleBackColor = true;
            this.btnRIngreso.Click += new System.EventHandler(this.btnRIngreso_Click);
            // 
            // btnRGasto
            // 
            this.btnRGasto.FlatAppearance.BorderColor = System.Drawing.Color.DarkSlateGray;
            this.btnRGasto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRGasto.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRGasto.ForeColor = System.Drawing.Color.White;
            this.btnRGasto.Location = new System.Drawing.Point(3, 218);
            this.btnRGasto.Name = "btnRGasto";
            this.btnRGasto.Size = new System.Drawing.Size(197, 50);
            this.btnRGasto.TabIndex = 3;
            this.btnRGasto.Text = "Registrar gasto";
            this.btnRGasto.UseVisualStyleBackColor = true;
            this.btnRGasto.Click += new System.EventHandler(this.btnRGasto_Click);
            // 
            // btnTransacciones
            // 
            this.btnTransacciones.FlatAppearance.BorderColor = System.Drawing.Color.DarkSlateGray;
            this.btnTransacciones.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTransacciones.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTransacciones.ForeColor = System.Drawing.Color.White;
            this.btnTransacciones.Location = new System.Drawing.Point(3, 274);
            this.btnTransacciones.Name = "btnTransacciones";
            this.btnTransacciones.Size = new System.Drawing.Size(197, 50);
            this.btnTransacciones.TabIndex = 4;
            this.btnTransacciones.Text = "Ver transacciones";
            this.btnTransacciones.UseVisualStyleBackColor = true;
            // 
            // btnMeta
            // 
            this.btnMeta.FlatAppearance.BorderColor = System.Drawing.Color.DarkSlateGray;
            this.btnMeta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMeta.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMeta.ForeColor = System.Drawing.Color.White;
            this.btnMeta.Location = new System.Drawing.Point(3, 330);
            this.btnMeta.Name = "btnMeta";
            this.btnMeta.Size = new System.Drawing.Size(197, 50);
            this.btnMeta.TabIndex = 5;
            this.btnMeta.Text = "Meta financiera";
            this.btnMeta.UseVisualStyleBackColor = true;
            // 
            // btnReporte
            // 
            this.btnReporte.FlatAppearance.BorderColor = System.Drawing.Color.DarkSlateGray;
            this.btnReporte.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReporte.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReporte.ForeColor = System.Drawing.Color.White;
            this.btnReporte.Location = new System.Drawing.Point(3, 386);
            this.btnReporte.Name = "btnReporte";
            this.btnReporte.Size = new System.Drawing.Size(197, 50);
            this.btnReporte.TabIndex = 6;
            this.btnReporte.Text = "Generar reporte PDF";
            this.btnReporte.UseVisualStyleBackColor = true;
            // 
            // panelTitulo
            // 
            this.panelTitulo.BackColor = System.Drawing.Color.CadetBlue;
            this.panelTitulo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panelTitulo.Controls.Add(this.lblTitulo);
            this.panelTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitulo.ForeColor = System.Drawing.Color.CadetBlue;
            this.panelTitulo.Location = new System.Drawing.Point(200, 0);
            this.panelTitulo.Name = "panelTitulo";
            this.panelTitulo.Size = new System.Drawing.Size(600, 100);
            this.panelTitulo.TabIndex = 1;
            // 
            // lblTitulo
            // 
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(114, 26);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(400, 50);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Dashboard";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelFormulario
            // 
            this.panelFormulario.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFormulario.Location = new System.Drawing.Point(200, 100);
            this.panelFormulario.Name = "panelFormulario";
            this.panelFormulario.Size = new System.Drawing.Size(600, 350);
            this.panelFormulario.TabIndex = 2;
            this.panelFormulario.Paint += new System.Windows.Forms.PaintEventHandler(this.panelFormulario_Paint);
            // 
            // P_Principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panelFormulario);
            this.Controls.Add(this.panelTitulo);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "P_Principal";
            this.Text = "Administrador financiero";
            this.Load += new System.EventHandler(this.P_Principal_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.panelUsuario.ResumeLayout(false);
            this.panelUsuario.PerformLayout();
            this.panelTitulo.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnDashboard;
        private System.Windows.Forms.Panel panelUsuario;
        private System.Windows.Forms.Button btnRIngreso;
        private System.Windows.Forms.Button btnRGasto;
        private System.Windows.Forms.Button btnTransacciones;
        private System.Windows.Forms.Button btnMeta;
        private System.Windows.Forms.Button btnReporte;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.Panel panelTitulo;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelFormulario;
    }
}