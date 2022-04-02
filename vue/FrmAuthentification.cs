using System;

using System.Windows.Forms;
using Mediatek86.controleur;
using Mediatek86.metier;

namespace Mediatek86.vue
{
    public partial class FrmAuthentification : Form
    {
        private readonly Controle controle;

        /// <summary>
        /// Bool si la connexion réussie
        /// </summary>
        public bool onSuccessAuth { get; private set; }


        public FrmAuthentification(Controle controle)
        {
            InitializeComponent();
            this.controle = controle;

        }

        /// <summary>
        /// Connexion et renvoie un message
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConnexion_Click(object sender, EventArgs e)
        {
            string utilisateur = txbUser.Text.Trim();
            string mdp = txbMDP.Text.Trim();
            Service userService = controle.Authentification(utilisateur, mdp);

            if (userService != null)
            {
                if (userService.Libelle == "culture")
                {
                    MessageBox.Show("Accès réservé aux services administratifs et au service de prêt.", "Information");
                    VideTbxConnexion();
                }
                else
                {
                    onSuccessAuth = true;
                    Close();
                }
            }
            else
            {
                MessageBox.Show("Vérifiez vos identifiants de connexion, ils sont incorrects..", "Information");
                VideTbxConnexion();
            }
        }

        /// <summary>
        /// Touche "enter" = connexion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConnexion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnConnexion_Click(sender, e);
            }
        }

        /// <summary>
        /// Vide les txb de connexion
        /// </summary>
        private void VideTbxConnexion()
        {
            txbUser.Text = "";
            txbMDP.Text = "";
            txbUser.Focus();
        }
    }
}