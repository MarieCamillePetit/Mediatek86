
namespace Mediatek86.vue
{
    partial class FrmAuthentification
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnConnexion = new System.Windows.Forms.Button();
            this.label89 = new System.Windows.Forms.Label();
            this.label100 = new System.Windows.Forms.Label();
            this.txbUser = new System.Windows.Forms.TextBox();
            this.txbMDP = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnConnexion
            // 
            this.btnConnexion.Location = new System.Drawing.Point(88, 133);
            this.btnConnexion.Name = "btnConnexion";
            this.btnConnexion.Size = new System.Drawing.Size(229, 41);
            this.btnConnexion.TabIndex = 0;
            this.btnConnexion.Text = "Se connecter";
            this.btnConnexion.UseVisualStyleBackColor = true;
            this.btnConnexion.Click += new System.EventHandler(this.btnConnexion_Click);
            this.btnConnexion.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnConnexion_KeyDown);
            // 
            // label89
            // 
            this.label89.AutoSize = true;
            this.label89.Location = new System.Drawing.Point(23, 38);
            this.label89.Name = "label89";
            this.label89.Size = new System.Drawing.Size(72, 16);
            this.label89.TabIndex = 1;
            this.label89.Text = "Utilisateur :";
            // 
            // label100
            // 
            this.label100.AutoSize = true;
            this.label100.Location = new System.Drawing.Point(23, 89);
            this.label100.Name = "label100";
            this.label100.Size = new System.Drawing.Size(95, 16);
            this.label100.TabIndex = 2;
            this.label100.Text = "Mot de passe :";
            // 
            // txbUser
            // 
            this.txbUser.Location = new System.Drawing.Point(139, 32);
            this.txbUser.Name = "txbUser";
            this.txbUser.Size = new System.Drawing.Size(223, 22);
            this.txbUser.TabIndex = 3;
            // 
            // txbMDP
            // 
            this.txbMDP.Location = new System.Drawing.Point(139, 83);
            this.txbMDP.Name = "txbMDP";
            this.txbMDP.PasswordChar = '*';
            this.txbMDP.Size = new System.Drawing.Size(223, 22);
            this.txbMDP.TabIndex = 4;
            // 
            // FrmAuthentification
            // 
            this.ClientSize = new System.Drawing.Size(385, 186);
            this.Controls.Add(this.txbMDP);
            this.Controls.Add(this.txbUser);
            this.Controls.Add(this.label100);
            this.Controls.Add(this.label89);
            this.Controls.Add(this.btnConnexion);
            this.Name = "FrmAuthentification";
            this.Text = "Connexion";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

       
        private System.Windows.Forms.Button btnConnexion;
        private System.Windows.Forms.Label label89;
        private System.Windows.Forms.Label label100;
        private System.Windows.Forms.TextBox txbUser;
        private System.Windows.Forms.TextBox txbMDP;
    }
}

