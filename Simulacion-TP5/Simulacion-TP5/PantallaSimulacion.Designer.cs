namespace Simulacion_TP5
{
    partial class PantallaSimulacion
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgv_simulacion = new System.Windows.Forms.DataGridView();
            this.lbl_gondola = new System.Windows.Forms.Label();
            this.lbl_caja3 = new System.Windows.Forms.Label();
            this.lbl_caja2 = new System.Windows.Forms.Label();
            this.lbl_cajaRapida = new System.Windows.Forms.Label();
            this.lbl_panaderia = new System.Windows.Forms.Label();
            this.lbl_carniceria = new System.Windows.Forms.Label();
            this.lbl_verduleria = new System.Windows.Forms.Label();
            this.btn_generar = new System.Windows.Forms.Button();
            this.txt_horas = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txt_hasta = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_desde = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_resultado = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_simulacion)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv_simulacion
            // 
            this.dgv_simulacion.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_simulacion.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_simulacion.Location = new System.Drawing.Point(219, 27);
            this.dgv_simulacion.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgv_simulacion.Name = "dgv_simulacion";
            this.dgv_simulacion.Size = new System.Drawing.Size(1082, 616);
            this.dgv_simulacion.TabIndex = 0;
            // 
            // lbl_gondola
            // 
            this.lbl_gondola.BackColor = System.Drawing.Color.Gainsboro;
            this.lbl_gondola.Location = new System.Drawing.Point(14, 496);
            this.lbl_gondola.Name = "lbl_gondola";
            this.lbl_gondola.Size = new System.Drawing.Size(199, 34);
            this.lbl_gondola.TabIndex = 33;
            this.lbl_gondola.Text = "Góndola";
            this.lbl_gondola.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_gondola.Visible = false;
            // 
            // lbl_caja3
            // 
            this.lbl_caja3.BackColor = System.Drawing.Color.Gainsboro;
            this.lbl_caja3.Location = new System.Drawing.Point(14, 609);
            this.lbl_caja3.Name = "lbl_caja3";
            this.lbl_caja3.Size = new System.Drawing.Size(199, 34);
            this.lbl_caja3.TabIndex = 32;
            this.lbl_caja3.Text = "Caja 3";
            this.lbl_caja3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_caja3.Visible = false;
            // 
            // lbl_caja2
            // 
            this.lbl_caja2.BackColor = System.Drawing.Color.Gainsboro;
            this.lbl_caja2.Location = new System.Drawing.Point(14, 571);
            this.lbl_caja2.Name = "lbl_caja2";
            this.lbl_caja2.Size = new System.Drawing.Size(199, 34);
            this.lbl_caja2.TabIndex = 31;
            this.lbl_caja2.Text = "Caja 2";
            this.lbl_caja2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_caja2.Visible = false;
            // 
            // lbl_cajaRapida
            // 
            this.lbl_cajaRapida.BackColor = System.Drawing.Color.Gainsboro;
            this.lbl_cajaRapida.Location = new System.Drawing.Point(14, 534);
            this.lbl_cajaRapida.Name = "lbl_cajaRapida";
            this.lbl_cajaRapida.Size = new System.Drawing.Size(199, 34);
            this.lbl_cajaRapida.TabIndex = 30;
            this.lbl_cajaRapida.Text = "Caja Rápida";
            this.lbl_cajaRapida.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_cajaRapida.Visible = false;
            // 
            // lbl_panaderia
            // 
            this.lbl_panaderia.BackColor = System.Drawing.Color.Gainsboro;
            this.lbl_panaderia.Location = new System.Drawing.Point(14, 458);
            this.lbl_panaderia.Name = "lbl_panaderia";
            this.lbl_panaderia.Size = new System.Drawing.Size(199, 34);
            this.lbl_panaderia.TabIndex = 29;
            this.lbl_panaderia.Text = "Panadería";
            this.lbl_panaderia.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_panaderia.Visible = false;
            // 
            // lbl_carniceria
            // 
            this.lbl_carniceria.BackColor = System.Drawing.Color.Gainsboro;
            this.lbl_carniceria.Location = new System.Drawing.Point(14, 420);
            this.lbl_carniceria.Name = "lbl_carniceria";
            this.lbl_carniceria.Size = new System.Drawing.Size(199, 34);
            this.lbl_carniceria.TabIndex = 28;
            this.lbl_carniceria.Text = "Carnicería";
            this.lbl_carniceria.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_carniceria.Visible = false;
            // 
            // lbl_verduleria
            // 
            this.lbl_verduleria.BackColor = System.Drawing.Color.Gainsboro;
            this.lbl_verduleria.Location = new System.Drawing.Point(14, 382);
            this.lbl_verduleria.Name = "lbl_verduleria";
            this.lbl_verduleria.Size = new System.Drawing.Size(199, 34);
            this.lbl_verduleria.TabIndex = 27;
            this.lbl_verduleria.Text = "Verduleria";
            this.lbl_verduleria.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_verduleria.Visible = false;
            // 
            // btn_generar
            // 
            this.btn_generar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_generar.Location = new System.Drawing.Point(14, 194);
            this.btn_generar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_generar.Name = "btn_generar";
            this.btn_generar.Size = new System.Drawing.Size(199, 38);
            this.btn_generar.TabIndex = 26;
            this.btn_generar.Text = "Generar simulación";
            this.btn_generar.UseVisualStyleBackColor = true;
            this.btn_generar.Click += new System.EventHandler(this.btn_generar_Click_1);
            // 
            // txt_horas
            // 
            this.txt_horas.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txt_horas.Location = new System.Drawing.Point(99, 155);
            this.txt_horas.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt_horas.Name = "txt_horas";
            this.txt_horas.Size = new System.Drawing.Size(91, 25);
            this.txt_horas.TabIndex = 25;
            this.txt_horas.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.label3.Location = new System.Drawing.Point(14, 158);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 17);
            this.label3.TabIndex = 24;
            this.label3.Text = "Cant. horas";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txt_hasta);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txt_desde);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(14, 27);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(199, 120);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Rango a mostrar";
            // 
            // txt_hasta
            // 
            this.txt_hasta.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txt_hasta.Location = new System.Drawing.Point(85, 75);
            this.txt_hasta.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt_hasta.Name = "txt_hasta";
            this.txt_hasta.Size = new System.Drawing.Size(91, 25);
            this.txt_hasta.TabIndex = 3;
            this.txt_hasta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.label2.Location = new System.Drawing.Point(38, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Hasta";
            // 
            // txt_desde
            // 
            this.txt_desde.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txt_desde.Location = new System.Drawing.Point(85, 42);
            this.txt_desde.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txt_desde.Name = "txt_desde";
            this.txt_desde.Size = new System.Drawing.Size(91, 25);
            this.txt_desde.TabIndex = 1;
            this.txt_desde.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.label1.Location = new System.Drawing.Point(34, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Desde";
            // 
            // lbl_resultado
            // 
            this.lbl_resultado.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbl_resultado.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lbl_resultado.Location = new System.Drawing.Point(0, 647);
            this.lbl_resultado.Name = "lbl_resultado";
            this.lbl_resultado.Size = new System.Drawing.Size(1313, 46);
            this.lbl_resultado.TabIndex = 34;
            this.lbl_resultado.Text = " Resultados de la simulación";
            this.lbl_resultado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PantallaSimulacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(1313, 693);
            this.Controls.Add(this.lbl_resultado);
            this.Controls.Add(this.lbl_gondola);
            this.Controls.Add(this.lbl_caja3);
            this.Controls.Add(this.lbl_caja2);
            this.Controls.Add(this.lbl_cajaRapida);
            this.Controls.Add(this.lbl_panaderia);
            this.Controls.Add(this.lbl_carniceria);
            this.Controls.Add(this.lbl_verduleria);
            this.Controls.Add(this.btn_generar);
            this.Controls.Add(this.txt_horas);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgv_simulacion);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "PantallaSimulacion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Supermercado";
            this.Load += new System.EventHandler(this.PantallaSimulacion_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_simulacion)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_simulacion;
        private System.Windows.Forms.Label lbl_gondola;
        private System.Windows.Forms.Label lbl_caja3;
        private System.Windows.Forms.Label lbl_caja2;
        private System.Windows.Forms.Label lbl_cajaRapida;
        private System.Windows.Forms.Label lbl_panaderia;
        private System.Windows.Forms.Label lbl_carniceria;
        private System.Windows.Forms.Label lbl_verduleria;
        private System.Windows.Forms.Button btn_generar;
        private System.Windows.Forms.TextBox txt_horas;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txt_hasta;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_desde;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_resultado;
    }
}

