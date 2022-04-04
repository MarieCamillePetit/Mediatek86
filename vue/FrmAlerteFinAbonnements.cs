using Mediatek86.controleur;
using Mediatek86.metier;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Mediatek86.vue
{
    /// <summary>
    /// Class pour l'alerte
    /// </summary>
    public partial class FrmAlerteFinAbonnements : Form
    {

        private readonly BindingSource bdgAlerteAbonnements = new BindingSource();

        private readonly List<FinAbonnement> lesFinAbonnement;

        /// <summary>
        /// Affichage des abonnements dans la fenêtre d'alerte
        /// </summary>
        /// <param name="controle"></param>
        internal FrmAlerteFinAbonnements(Controle controle)
        {
            InitializeComponent();
            lesFinAbonnement = controle.GetFinAbonnement();
            bdgAlerteAbonnements.DataSource = lesFinAbonnement;
            dgvAlerteFinAbonnements.DataSource = bdgAlerteAbonnements;
            dgvAlerteFinAbonnements.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvAlerteFinAbonnements.Columns["idRevue"].DisplayIndex = 2;
            dgvAlerteFinAbonnements.Columns[0].HeaderCell.Value = "Date fin d'abonnement";
            dgvAlerteFinAbonnements.Columns[1].HeaderCell.Value = "Identifiant Revue";
            dgvAlerteFinAbonnements.Columns[2].HeaderCell.Value = "Revue";
            btnAlerteFinAbonnements.Focus();
        }

        /// <summary>
        /// Selection d'un abonnement
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvAlerteFinAbonnements_SelectionChanged(object sender, EventArgs e)
        {
            dgvAlerteFinAbonnements.ClearSelection();
        }
        /// <summary>
        /// Bouton pour quitter la fenêtre d'alerte
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAlerteFinAbonnements_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}