namespace Mediatek86.vue
{
    partial class FrmAlerteFinAbonnements
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvAlerteFinAbonnements = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAlerteFinAbonnements = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlerteFinAbonnements)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvAlerteFinAbonnements
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAlerteFinAbonnements.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvAlerteFinAbonnements.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAlerteFinAbonnements.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvAlerteFinAbonnements.Location = new System.Drawing.Point(16, 87);
            this.dgvAlerteFinAbonnements.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvAlerteFinAbonnements.Name = "dgvAlerteFinAbonnements";
            this.dgvAlerteFinAbonnements.RowHeadersVisible = false;
            this.dgvAlerteFinAbonnements.RowHeadersWidth = 51;
            this.dgvAlerteFinAbonnements.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAlerteFinAbonnements.Size = new System.Drawing.Size(557, 185);
            this.dgvAlerteFinAbonnements.TabIndex = 0;
            this.dgvAlerteFinAbonnements.TabStop = false;
            this.dgvAlerteFinAbonnements.SelectionChanged += new System.EventHandler(this.dgvAlerteFinAbonnements_SelectionChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(252, 28);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Attention !";
            // 
            // btnAlerteFinAbonnements
            // 
            this.btnAlerteFinAbonnements.Location = new System.Drawing.Point(16, 279);
            this.btnAlerteFinAbonnements.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAlerteFinAbonnements.Name = "btnAlerteFinAbonnements";
            this.btnAlerteFinAbonnements.Size = new System.Drawing.Size(557, 36);
            this.btnAlerteFinAbonnements.TabIndex = 1;
            this.btnAlerteFinAbonnements.Text = "Continuer";
            this.btnAlerteFinAbonnements.UseVisualStyleBackColor = true;
            this.btnAlerteFinAbonnements.Click += new System.EventHandler(this.btnAlerteFinAbonnements_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(127, 44);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(362, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Les abonnements suivants expirent dans moins de 30 jours :";
            // 
            // FrmAlerteFinAbonnements
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(589, 332);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnAlerteFinAbonnements);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvAlerteFinAbonnements);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FrmAlerteFinAbonnements";
            this.Text = "Alerte abonnements";
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlerteFinAbonnements)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvAlerteFinAbonnements;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAlerteFinAbonnements;
        private System.Windows.Forms.Label label2;
    }
}
