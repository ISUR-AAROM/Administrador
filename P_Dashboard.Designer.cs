namespace Administrador
{
    partial class P_Dashboard
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chartBalance = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartIngresos = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dtpFechaInicio = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaFin = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chartBalance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartIngresos)).BeginInit();
            this.SuspendLayout();
            // 
            // chartBalance
            // 
            this.chartBalance.BackColor = System.Drawing.Color.Transparent;
            this.chartBalance.BorderlineColor = System.Drawing.Color.Transparent;
            chartArea1.Name = "ChartArea1";
            this.chartBalance.ChartAreas.Add(chartArea1);
            this.chartBalance.Location = new System.Drawing.Point(47, 114);
            this.chartBalance.Name = "chartBalance";
            series1.ChartArea = "ChartArea1";
            series1.Name = "Series1";
            this.chartBalance.Series.Add(series1);
            this.chartBalance.Size = new System.Drawing.Size(240, 163);
            this.chartBalance.TabIndex = 0;
            this.chartBalance.Text = "chart1";
            // 
            // chartIngresos
            // 
            this.chartIngresos.BackColor = System.Drawing.Color.Transparent;
            this.chartIngresos.BorderlineColor = System.Drawing.Color.Transparent;
            chartArea2.Name = "ChartArea1";
            this.chartIngresos.ChartAreas.Add(chartArea2);
            this.chartIngresos.Location = new System.Drawing.Point(293, 114);
            this.chartIngresos.Name = "chartIngresos";
            series2.ChartArea = "ChartArea1";
            series2.Name = "Series1";
            this.chartIngresos.Series.Add(series2);
            this.chartIngresos.Size = new System.Drawing.Size(240, 163);
            this.chartIngresos.TabIndex = 3;
            this.chartIngresos.Text = "chart3";
            // 
            // dtpFechaInicio
            // 
            this.dtpFechaInicio.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaInicio.Location = new System.Drawing.Point(73, 53);
            this.dtpFechaInicio.Name = "dtpFechaInicio";
            this.dtpFechaInicio.Size = new System.Drawing.Size(213, 23);
            this.dtpFechaInicio.TabIndex = 4;
            // 
            // dtpFechaFin
            // 
            this.dtpFechaFin.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaFin.Location = new System.Drawing.Point(309, 53);
            this.dtpFechaFin.Name = "dtpFechaFin";
            this.dtpFechaFin.Size = new System.Drawing.Size(213, 23);
            this.dtpFechaFin.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(70, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "Desde:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(306, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 15);
            this.label2.TabIndex = 7;
            this.label2.Text = "Hasta:";
            // 
            // P_Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 311);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpFechaFin);
            this.Controls.Add(this.dtpFechaInicio);
            this.Controls.Add(this.chartIngresos);
            this.Controls.Add(this.chartBalance);
            this.Name = "P_Dashboard";
            this.Text = "P_Dashboard";
            this.Load += new System.EventHandler(this.P_Dashboard_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chartBalance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartIngresos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartBalance;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartIngresos;
        private System.Windows.Forms.DateTimePicker dtpFechaInicio;
        private System.Windows.Forms.DateTimePicker dtpFechaFin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}