using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Mediatek86.metier;
using Mediatek86.controleur;
using System.Drawing;
using System.Linq;
using System.Globalization;

namespace Mediatek86.vue
{
    public partial class FrmMediatek : Form
    {

        #region Variables globales

        private readonly Controle controle;
        const string ETATNEUF = "00001";

        private readonly BindingSource bdgLivresListe = new BindingSource();
        private readonly BindingSource bdgDvdListe = new BindingSource();
        private readonly BindingSource bdgGenres = new BindingSource();
        private readonly BindingSource bdgPublics = new BindingSource();
        private readonly BindingSource bdgRayons = new BindingSource();
        private readonly BindingSource bdgRevuesListe = new BindingSource();
        private readonly BindingSource bdgExemplairesListe = new BindingSource();
        private List<Livre> lesLivres = new List<Livre>();
        private List<Dvd> lesDvd = new List<Dvd>();
        private List<Revue> lesRevues = new List<Revue>();
        private List<Exemplaire> lesExemplaires = new List<Exemplaire>();

        private List<CommandeDocument> lesCommandeDocument = new List<CommandeDocument>();
        private readonly BindingSource bdgCommandesLivresListe = new BindingSource();
        private List<Suivi> lesSuivis = new List<Suivi>();
        private readonly BindingSource bdgCommandesDvdListe = new BindingSource();

        #endregion


        internal FrmMediatek(Controle controle)
        {
            InitializeComponent();
            this.controle = controle;
        }


        #region modules communs

        /// <summary>
        /// Rempli un des 3 combo (genre, public, rayon)
        /// </summary>
        /// <param name="lesCategories"></param>
        /// <param name="bdg"></param>
        /// <param name="cbx"></param>
        public void RemplirComboCategorie(List<Categorie> lesCategories, BindingSource bdg, ComboBox cbx)
        {
            bdg.DataSource = lesCategories;
            cbx.DataSource = bdg;
            if (cbx.Items.Count > 0)
            {
                cbx.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Demande la confirmation de la suppression
        /// </summary>
        private bool ConfirmationSupprCommande()
        {
            return (MessageBox.Show("Voulez-vous vraiment supprimer cette commande ?", "Suppression", MessageBoxButtons.YesNo) == DialogResult.Yes);
        }

        /// <summary>
        /// Demande la confirmation de la modification d'un statut de suivi
        /// </summary>
        /// <param name="libelleSuivi">Nouvel état de suivi</param>
        private bool ConfirmationModifSuiviCommande(string libelleSuivi)
        {
            return (MessageBox.Show("Etes-vous sûr de vouloir changer l'état de cette commande à : " + libelleSuivi + " ?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes);
        }

        /// <summary>
        /// Demande la confirmation de l'annulation de la commande
        /// </summary>
        private bool ConfirmationAnnulationCommande()
        {
            return (MessageBox.Show("Etes-vous sûr de vouloir annuler votre saisie ?", "Confirmation d'annulation", MessageBoxButtons.YesNo) == DialogResult.Yes);
        }

        #endregion


        #region Revues
        //-----------------------------------------------------------
        // ONGLET "Revues"
        //------------------------------------------------------------

        /// <summary>
        /// Ouverture de l'onglet Revues : 
        /// appel des méthodes pour remplir le datagrid des revues et des combos (genre, rayon, public)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabRevues_Enter(object sender, EventArgs e)
        {
            lesRevues = controle.GetAllRevues();
            RemplirComboCategorie(controle.GetAllGenres(), bdgGenres, cbxRevuesGenres);
            RemplirComboCategorie(controle.GetAllPublics(), bdgPublics, cbxRevuesPublics);
            RemplirComboCategorie(controle.GetAllRayons(), bdgRayons, cbxRevuesRayons);
            RemplirRevuesListeComplete();
        }

        /// <summary>
        /// Remplit le dategrid avec la liste reçue en paramètre
        /// </summary>
        private void RemplirRevuesListe(List<Revue> revues)
        {
            bdgRevuesListe.DataSource = revues;
            dgvRevuesListe.DataSource = bdgRevuesListe;
            dgvRevuesListe.Columns["empruntable"].Visible = false;
            dgvRevuesListe.Columns["idRayon"].Visible = false;
            dgvRevuesListe.Columns["idGenre"].Visible = false;
            dgvRevuesListe.Columns["idPublic"].Visible = false;
            dgvRevuesListe.Columns["image"].Visible = false;
            dgvRevuesListe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvRevuesListe.Columns["id"].DisplayIndex = 0;
            dgvRevuesListe.Columns["titre"].DisplayIndex = 1;
        }

        /// <summary>
        /// Recherche et affichage de la revue dont on a saisi le numéro.
        /// Si non trouvé, affichage d'un MessageBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRevuesNumRecherche_Click(object sender, EventArgs e)
        {
            if (!txbRevuesNumRecherche.Text.Equals(""))
            {
                txbRevuesTitreRecherche.Text = "";
                cbxRevuesGenres.SelectedIndex = -1;
                cbxRevuesRayons.SelectedIndex = -1;
                cbxRevuesPublics.SelectedIndex = -1;
                Revue revue = lesRevues.Find(x => x.Id.Equals(txbRevuesNumRecherche.Text));
                if (revue != null)
                {
                    List<Revue> revues = new List<Revue>();
                    revues.Add(revue);
                    RemplirRevuesListe(revues);
                }
                else
                {
                    MessageBox.Show("numéro introuvable");
                    RemplirRevuesListeComplete();
                }
            }
            else
            {
                RemplirRevuesListeComplete();
            }
        }

        /// <summary>
        /// Recherche et affichage des revues dont le titre matche acec la saisie.
        /// Cette procédure est exécutée à chaque ajout ou suppression de caractère
        /// dans le textBox de saisie.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txbRevuesTitreRecherche_TextChanged(object sender, EventArgs e)
        {
            if (!txbRevuesTitreRecherche.Text.Equals(""))
            {
                cbxRevuesGenres.SelectedIndex = -1;
                cbxRevuesRayons.SelectedIndex = -1;
                cbxRevuesPublics.SelectedIndex = -1;
                txbRevuesNumRecherche.Text = "";
                List<Revue> lesRevuesParTitre;
                lesRevuesParTitre = lesRevues.FindAll(x => x.Titre.ToLower().Contains(txbRevuesTitreRecherche.Text.ToLower()));
                RemplirRevuesListe(lesRevuesParTitre);
            }
            else
            {
                // si la zone de saisie est vide et aucun élément combo sélectionné, réaffichage de la liste complète
                if (cbxRevuesGenres.SelectedIndex < 0 && cbxRevuesPublics.SelectedIndex < 0 && cbxRevuesRayons.SelectedIndex < 0
                    && txbRevuesNumRecherche.Text.Equals(""))
                {
                    RemplirRevuesListeComplete();
                }
            }
        }

        /// <summary>
        /// Affichage des informations de la revue sélectionné
        /// </summary>
        /// <param name="revue"></param>
        private void AfficheRevuesInfos(Revue revue)
        {
            txbRevuesPeriodicite.Text = revue.Periodicite;
            chkRevuesEmpruntable.Checked = revue.Empruntable;
            txbRevuesImage.Text = revue.Image;
            txbRevuesDateMiseADispo.Text = revue.DelaiMiseADispo.ToString();
            txbRevuesNumero.Text = revue.Id;
            txbRevuesGenre.Text = revue.Genre;
            txbRevuesPublic.Text = revue.Public;
            txbRevuesRayon.Text = revue.Rayon;
            txbRevuesTitre.Text = revue.Titre;
            string image = revue.Image;
            try
            {
                pcbRevuesImage.Image = Image.FromFile(image);
            }
            catch
            {
                pcbRevuesImage.Image = null;
            }
        }

        /// <summary>
        /// Vide les zones d'affichage des informations de la reuve
        /// </summary>
        private void VideRevuesInfos()
        {
            txbRevuesPeriodicite.Text = "";
            chkRevuesEmpruntable.Checked = false;
            txbRevuesImage.Text = "";
            txbRevuesDateMiseADispo.Text = "";
            txbRevuesNumero.Text = "";
            txbRevuesGenre.Text = "";
            txbRevuesPublic.Text = "";
            txbRevuesRayon.Text = "";
            txbRevuesTitre.Text = "";
            pcbRevuesImage.Image = null;
        }

        /// <summary>
        /// Filtre sur le genre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxRevuesGenres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxRevuesGenres.SelectedIndex >= 0)
            {
                txbRevuesTitreRecherche.Text = "";
                txbRevuesNumRecherche.Text = "";
                Genre genre = (Genre)cbxRevuesGenres.SelectedItem;
                List<Revue> revues = lesRevues.FindAll(x => x.Genre.Equals(genre.Libelle));
                RemplirRevuesListe(revues);
                cbxRevuesRayons.SelectedIndex = -1;
                cbxRevuesPublics.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Filtre sur la catégorie de public
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxRevuesPublics_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxRevuesPublics.SelectedIndex >= 0)
            {
                txbRevuesTitreRecherche.Text = "";
                txbRevuesNumRecherche.Text = "";
                Public lePublic = (Public)cbxRevuesPublics.SelectedItem;
                List<Revue> revues = lesRevues.FindAll(x => x.Public.Equals(lePublic.Libelle));
                RemplirRevuesListe(revues);
                cbxRevuesRayons.SelectedIndex = -1;
                cbxRevuesGenres.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Filtre sur le rayon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxRevuesRayons_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxRevuesRayons.SelectedIndex >= 0)
            {
                txbRevuesTitreRecherche.Text = "";
                txbRevuesNumRecherche.Text = "";
                Rayon rayon = (Rayon)cbxRevuesRayons.SelectedItem;
                List<Revue> revues = lesRevues.FindAll(x => x.Rayon.Equals(rayon.Libelle));
                RemplirRevuesListe(revues);
                cbxRevuesGenres.SelectedIndex = -1;
                cbxRevuesPublics.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Sur la sélection d'une ligne ou cellule dans le grid
        /// affichage des informations de la revue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvRevuesListe_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvRevuesListe.CurrentCell != null)
            {
                try
                {
                    Revue revue = (Revue)bdgRevuesListe.List[bdgRevuesListe.Position];
                    AfficheRevuesInfos(revue);
                }
                catch
                {
                    VideRevuesZones();
                }
            }
            else
            {
                VideRevuesInfos();
            }
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des revues
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRevuesAnnulPublics_Click(object sender, EventArgs e)
        {
            RemplirRevuesListeComplete();
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des revues
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRevuesAnnulRayons_Click(object sender, EventArgs e)
        {
            RemplirRevuesListeComplete();
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des revues
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRevuesAnnulGenres_Click(object sender, EventArgs e)
        {
            RemplirRevuesListeComplete();
        }

        /// <summary>
        /// Affichage de la liste complète des revues
        /// et annulation de toutes les recherches et filtres
        /// </summary>
        private void RemplirRevuesListeComplete()
        {
            RemplirRevuesListe(lesRevues);
            VideRevuesZones();
        }

        /// <summary>
        /// vide les zones de recherche et de filtre
        /// </summary>
        private void VideRevuesZones()
        {
            cbxRevuesGenres.SelectedIndex = -1;
            cbxRevuesRayons.SelectedIndex = -1;
            cbxRevuesPublics.SelectedIndex = -1;
            txbRevuesNumRecherche.Text = "";
            txbRevuesTitreRecherche.Text = "";
        }

        /// <summary>
        /// Tri sur les colonnes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvRevuesListe_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            VideRevuesZones();
            string titreColonne = dgvRevuesListe.Columns[e.ColumnIndex].HeaderText;
            List<Revue> sortedList = new List<Revue>();
            switch (titreColonne)
            {
                case "Id":
                    sortedList = lesRevues.OrderBy(o => o.Id).ToList();
                    break;
                case "Titre":
                    sortedList = lesRevues.OrderBy(o => o.Titre).ToList();
                    break;
                case "Periodicite":
                    sortedList = lesRevues.OrderBy(o => o.Periodicite).ToList();
                    break;
                case "DelaiMiseADispo":
                    sortedList = lesRevues.OrderBy(o => o.DelaiMiseADispo).ToList();
                    break;
                case "Genre":
                    sortedList = lesRevues.OrderBy(o => o.Genre).ToList();
                    break;
                case "Public":
                    sortedList = lesRevues.OrderBy(o => o.Public).ToList();
                    break;
                case "Rayon":
                    sortedList = lesRevues.OrderBy(o => o.Rayon).ToList();
                    break;
            }
            RemplirRevuesListe(sortedList);
        }

        #endregion


        #region Livres

        //-----------------------------------------------------------
        // ONGLET "LIVRES"
        //-----------------------------------------------------------

        /// <summary>
        /// Ouverture de l'onglet Livres : 
        /// appel des méthodes pour remplir le datagrid des livres et des combos (genre, rayon, public)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabLivres_Enter(object sender, EventArgs e)
        {
            lesLivres = controle.GetAllLivres();
            RemplirComboCategorie(controle.GetAllGenres(), bdgGenres, cbxLivresGenres);
            RemplirComboCategorie(controle.GetAllPublics(), bdgPublics, cbxLivresPublics);
            RemplirComboCategorie(controle.GetAllRayons(), bdgRayons, cbxLivresRayons);
            RemplirLivresListeComplete();
        }

        /// <summary>
        /// Remplit le dategrid avec la liste reçue en paramètre
        /// </summary>
        private void RemplirLivresListe(List<Livre> livres)
        {
            bdgLivresListe.DataSource = livres;
            dgvLivresListe.DataSource = bdgLivresListe;
            dgvLivresListe.Columns["isbn"].Visible = false;
            dgvLivresListe.Columns["idRayon"].Visible = false;
            dgvLivresListe.Columns["idGenre"].Visible = false;
            dgvLivresListe.Columns["idPublic"].Visible = false;
            dgvLivresListe.Columns["image"].Visible = false;
            dgvLivresListe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvLivresListe.Columns["id"].DisplayIndex = 0;
            dgvLivresListe.Columns["titre"].DisplayIndex = 1;
        }

        /// <summary>
        /// Recherche et affichage du livre dont on a saisi le numéro.
        /// Si non trouvé, affichage d'un MessageBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLivresNumRecherche_Click(object sender, EventArgs e)
        {
            if (!txbLivresNumRecherche.Text.Equals(""))
            {
                txbLivresTitreRecherche.Text = "";
                cbxLivresGenres.SelectedIndex = -1;
                cbxLivresRayons.SelectedIndex = -1;
                cbxLivresPublics.SelectedIndex = -1;
                Livre livre = lesLivres.Find(x => x.Id.Equals(txbLivresNumRecherche.Text));
                if (livre != null)
                {
                    List<Livre> livres = new List<Livre>();
                    livres.Add(livre);
                    RemplirLivresListe(livres);
                }
                else
                {
                    MessageBox.Show("numéro introuvable");
                    RemplirLivresListeComplete();
                }
            }
            else
            {
                RemplirLivresListeComplete();
            }
        }

        /// <summary>
        /// Recherche et affichage des livres dont le titre matche acec la saisie.
        /// Cette procédure est exécutée à chaque ajout ou suppression de caractère
        /// dans le textBox de saisie.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxbLivresTitreRecherche_TextChanged(object sender, EventArgs e)
        {
            if (!txbLivresTitreRecherche.Text.Equals(""))
            {
                cbxLivresGenres.SelectedIndex = -1;
                cbxLivresRayons.SelectedIndex = -1;
                cbxLivresPublics.SelectedIndex = -1;
                txbLivresNumRecherche.Text = "";
                List<Livre> lesLivresParTitre;
                lesLivresParTitre = lesLivres.FindAll(x => x.Titre.ToLower().Contains(txbLivresTitreRecherche.Text.ToLower()));
                RemplirLivresListe(lesLivresParTitre);
            }
            else
            {
                // si la zone de saisie est vide et aucun élément combo sélectionné, réaffichage de la liste complète
                if (cbxLivresGenres.SelectedIndex < 0 && cbxLivresPublics.SelectedIndex < 0 && cbxLivresRayons.SelectedIndex < 0
                    && txbLivresNumRecherche.Text.Equals(""))
                {
                    RemplirLivresListeComplete();
                }
            }
        }

        /// <summary>
        /// Affichage des informations du livre sélectionné
        /// </summary>
        /// <param name="livre"></param>
        private void AfficheLivresInfos(Livre livre)
        {
            txbLivresAuteur.Text = livre.Auteur;
            txbLivresCollection.Text = livre.Collection;
            txbLivresImage.Text = livre.Image;
            txbLivresIsbn.Text = livre.Isbn;
            txbLivresNumero.Text = livre.Id;
            txbLivresGenre.Text = livre.Genre;
            txbLivresPublic.Text = livre.Public;
            txbLivresRayon.Text = livre.Rayon;
            txbLivresTitre.Text = livre.Titre;
            string image = livre.Image;
            try
            {
                pcbLivresImage.Image = Image.FromFile(image);
            }
            catch
            {
                pcbLivresImage.Image = null;
            }
        }

        /// <summary>
        /// Vide les zones d'affichage des informations du livre
        /// </summary>
        private void VideLivresInfos()
        {
            txbLivresAuteur.Text = "";
            txbLivresCollection.Text = "";
            txbLivresImage.Text = "";
            txbLivresIsbn.Text = "";
            txbLivresNumero.Text = "";
            txbLivresGenre.Text = "";
            txbLivresPublic.Text = "";
            txbLivresRayon.Text = "";
            txbLivresTitre.Text = "";
            pcbLivresImage.Image = null;
        }

        /// <summary>
        /// Filtre sur le genre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbxLivresGenres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxLivresGenres.SelectedIndex >= 0)
            {
                txbLivresTitreRecherche.Text = "";
                txbLivresNumRecherche.Text = "";
                Genre genre = (Genre)cbxLivresGenres.SelectedItem;
                List<Livre> livres = lesLivres.FindAll(x => x.Genre.Equals(genre.Libelle));
                RemplirLivresListe(livres);
                cbxLivresRayons.SelectedIndex = -1;
                cbxLivresPublics.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Filtre sur la catégorie de public
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbxLivresPublics_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxLivresPublics.SelectedIndex >= 0)
            {
                txbLivresTitreRecherche.Text = "";
                txbLivresNumRecherche.Text = "";
                Public lePublic = (Public)cbxLivresPublics.SelectedItem;
                List<Livre> livres = lesLivres.FindAll(x => x.Public.Equals(lePublic.Libelle));
                RemplirLivresListe(livres);
                cbxLivresRayons.SelectedIndex = -1;
                cbxLivresGenres.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Filtre sur le rayon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbxLivresRayons_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxLivresRayons.SelectedIndex >= 0)
            {
                txbLivresTitreRecherche.Text = "";
                txbLivresNumRecherche.Text = "";
                Rayon rayon = (Rayon)cbxLivresRayons.SelectedItem;
                List<Livre> livres = lesLivres.FindAll(x => x.Rayon.Equals(rayon.Libelle));
                RemplirLivresListe(livres);
                cbxLivresGenres.SelectedIndex = -1;
                cbxLivresPublics.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Sur la sélection d'une ligne ou cellule dans le grid
        /// affichage des informations du livre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvLivresListe_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvLivresListe.CurrentCell != null)
            {
                try
                {
                    Livre livre = (Livre)bdgLivresListe.List[bdgLivresListe.Position];
                    AfficheLivresInfos(livre);
                }
                catch
                {
                    VideLivresZones();
                }
            }
            else
            {
                VideLivresInfos();
            }
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des livres
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLivresAnnulPublics_Click(object sender, EventArgs e)
        {
            RemplirLivresListeComplete();
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des livres
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLivresAnnulRayons_Click(object sender, EventArgs e)
        {
            RemplirLivresListeComplete();
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des livres
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLivresAnnulGenres_Click(object sender, EventArgs e)
        {
            RemplirLivresListeComplete();
        }

        /// <summary>
        /// Affichage de la liste complète des livres
        /// et annulation de toutes les recherches et filtres
        /// </summary>
        private void RemplirLivresListeComplete()
        {
            RemplirLivresListe(lesLivres);
            VideLivresZones();
        }

        /// <summary>
        /// vide les zones de recherche et de filtre
        /// </summary>
        private void VideLivresZones()
        {
            cbxLivresGenres.SelectedIndex = -1;
            cbxLivresRayons.SelectedIndex = -1;
            cbxLivresPublics.SelectedIndex = -1;
            txbLivresNumRecherche.Text = "";
            txbLivresTitreRecherche.Text = "";
        }

        /// <summary>
        /// Tri sur les colonnes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvLivresListe_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            VideLivresZones();
            string titreColonne = dgvLivresListe.Columns[e.ColumnIndex].HeaderText;
            List<Livre> sortedList = new List<Livre>();
            switch (titreColonne)
            {
                case "Id":
                    sortedList = lesLivres.OrderBy(o => o.Id).ToList();
                    break;
                case "Titre":
                    sortedList = lesLivres.OrderBy(o => o.Titre).ToList();
                    break;
                case "Collection":
                    sortedList = lesLivres.OrderBy(o => o.Collection).ToList();
                    break;
                case "Auteur":
                    sortedList = lesLivres.OrderBy(o => o.Auteur).ToList();
                    break;
                case "Genre":
                    sortedList = lesLivres.OrderBy(o => o.Genre).ToList();
                    break;
                case "Public":
                    sortedList = lesLivres.OrderBy(o => o.Public).ToList();
                    break;
                case "Rayon":
                    sortedList = lesLivres.OrderBy(o => o.Rayon).ToList();
                    break;
            }
            RemplirLivresListe(sortedList);
        }

        #endregion


        #region Dvd
        //-----------------------------------------------------------
        // ONGLET "DVD"
        //-----------------------------------------------------------

        /// <summary>
        /// Ouverture de l'onglet Dvds : 
        /// appel des méthodes pour remplir le datagrid des dvd et des combos (genre, rayon, public)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabDvd_Enter(object sender, EventArgs e)
        {
            lesDvd = controle.GetAllDvd();
            RemplirComboCategorie(controle.GetAllGenres(), bdgGenres, cbxDvdGenres);
            RemplirComboCategorie(controle.GetAllPublics(), bdgPublics, cbxDvdPublics);
            RemplirComboCategorie(controle.GetAllRayons(), bdgRayons, cbxDvdRayons);
            RemplirDvdListeComplete();
        }

        /// <summary>
        /// Remplit le dategrid avec la liste reçue en paramètre
        /// </summary>
        private void RemplirDvdListe(List<Dvd> Dvds)
        {
            bdgDvdListe.DataSource = Dvds;
            dgvDvdListe.DataSource = bdgDvdListe;
            dgvDvdListe.Columns["idRayon"].Visible = false;
            dgvDvdListe.Columns["idGenre"].Visible = false;
            dgvDvdListe.Columns["idPublic"].Visible = false;
            dgvDvdListe.Columns["image"].Visible = false;
            dgvDvdListe.Columns["synopsis"].Visible = false;
            dgvDvdListe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvDvdListe.Columns["id"].DisplayIndex = 0;
            dgvDvdListe.Columns["titre"].DisplayIndex = 1;
        }

        /// <summary>
        /// Recherche et affichage du Dvd dont on a saisi le numéro.
        /// Si non trouvé, affichage d'un MessageBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDvdNumRecherche_Click(object sender, EventArgs e)
        {
            if (!txbDvdNumRecherche.Text.Equals(""))
            {
                txbDvdTitreRecherche.Text = "";
                cbxDvdGenres.SelectedIndex = -1;
                cbxDvdRayons.SelectedIndex = -1;
                cbxDvdPublics.SelectedIndex = -1;
                Dvd dvd = lesDvd.Find(x => x.Id.Equals(txbDvdNumRecherche.Text));
                if (dvd != null)
                {
                    List<Dvd> Dvd = new List<Dvd>();
                    Dvd.Add(dvd);
                    RemplirDvdListe(Dvd);
                }
                else
                {
                    MessageBox.Show("numéro introuvable");
                    RemplirDvdListeComplete();
                }
            }
            else
            {
                RemplirDvdListeComplete();
            }
        }

        /// <summary>
        /// Recherche et affichage des Dvd dont le titre matche acec la saisie.
        /// Cette procédure est exécutée à chaque ajout ou suppression de caractère
        /// dans le textBox de saisie.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txbDvdTitreRecherche_TextChanged(object sender, EventArgs e)
        {
            if (!txbDvdTitreRecherche.Text.Equals(""))
            {
                cbxDvdGenres.SelectedIndex = -1;
                cbxDvdRayons.SelectedIndex = -1;
                cbxDvdPublics.SelectedIndex = -1;
                txbDvdNumRecherche.Text = "";
                List<Dvd> lesDvdParTitre;
                lesDvdParTitre = lesDvd.FindAll(x => x.Titre.ToLower().Contains(txbDvdTitreRecherche.Text.ToLower()));
                RemplirDvdListe(lesDvdParTitre);
            }
            else
            {
                // si la zone de saisie est vide et aucun élément combo sélectionné, réaffichage de la liste complète
                if (cbxDvdGenres.SelectedIndex < 0 && cbxDvdPublics.SelectedIndex < 0 && cbxDvdRayons.SelectedIndex < 0
                    && txbDvdNumRecherche.Text.Equals(""))
                {
                    RemplirDvdListeComplete();
                }
            }
        }

        /// <summary>
        /// Affichage des informations du dvd sélectionné
        /// </summary>
        /// <param name="dvd"></param>
        private void AfficheDvdInfos(Dvd dvd)
        {
            txbDvdRealisateur.Text = dvd.Realisateur;
            txbDvdSynopsis.Text = dvd.Synopsis;
            txbDvdImage.Text = dvd.Image;
            txbDvdDuree.Text = dvd.Duree.ToString();
            txbDvdNumero.Text = dvd.Id;
            txbDvdGenre.Text = dvd.Genre;
            txbDvdPublic.Text = dvd.Public;
            txbDvdRayon.Text = dvd.Rayon;
            txbDvdTitre.Text = dvd.Titre;
            string image = dvd.Image;
            try
            {
                pcbDvdImage.Image = Image.FromFile(image);
            }
            catch
            {
                pcbDvdImage.Image = null;
            }
        }

        /// <summary>
        /// Vide les zones d'affichage des informations du dvd
        /// </summary>
        private void VideDvdInfos()
        {
            txbDvdRealisateur.Text = "";
            txbDvdSynopsis.Text = "";
            txbDvdImage.Text = "";
            txbDvdDuree.Text = "";
            txbDvdNumero.Text = "";
            txbDvdGenre.Text = "";
            txbDvdPublic.Text = "";
            txbDvdRayon.Text = "";
            txbDvdTitre.Text = "";
            pcbDvdImage.Image = null;
        }

        /// <summary>
        /// Filtre sur le genre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxDvdGenres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxDvdGenres.SelectedIndex >= 0)
            {
                txbDvdTitreRecherche.Text = "";
                txbDvdNumRecherche.Text = "";
                Genre genre = (Genre)cbxDvdGenres.SelectedItem;
                List<Dvd> Dvd = lesDvd.FindAll(x => x.Genre.Equals(genre.Libelle));
                RemplirDvdListe(Dvd);
                cbxDvdRayons.SelectedIndex = -1;
                cbxDvdPublics.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Filtre sur la catégorie de public
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxDvdPublics_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxDvdPublics.SelectedIndex >= 0)
            {
                txbDvdTitreRecherche.Text = "";
                txbDvdNumRecherche.Text = "";
                Public lePublic = (Public)cbxDvdPublics.SelectedItem;
                List<Dvd> Dvd = lesDvd.FindAll(x => x.Public.Equals(lePublic.Libelle));
                RemplirDvdListe(Dvd);
                cbxDvdRayons.SelectedIndex = -1;
                cbxDvdGenres.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Filtre sur le rayon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxDvdRayons_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxDvdRayons.SelectedIndex >= 0)
            {
                txbDvdTitreRecherche.Text = "";
                txbDvdNumRecherche.Text = "";
                Rayon rayon = (Rayon)cbxDvdRayons.SelectedItem;
                List<Dvd> Dvd = lesDvd.FindAll(x => x.Rayon.Equals(rayon.Libelle));
                RemplirDvdListe(Dvd);
                cbxDvdGenres.SelectedIndex = -1;
                cbxDvdPublics.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Sur la sélection d'une ligne ou cellule dans le grid
        /// affichage des informations du dvd
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvDvdListe_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDvdListe.CurrentCell != null)
            {
                try
                {
                    Dvd dvd = (Dvd)bdgDvdListe.List[bdgDvdListe.Position];
                    AfficheDvdInfos(dvd);
                }
                catch
                {
                    VideDvdZones();
                }
            }
            else
            {
                VideDvdInfos();
            }
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des Dvd
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDvdAnnulPublics_Click(object sender, EventArgs e)
        {
            RemplirDvdListeComplete();
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des Dvd
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDvdAnnulRayons_Click(object sender, EventArgs e)
        {
            RemplirDvdListeComplete();
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des Dvd
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDvdAnnulGenres_Click(object sender, EventArgs e)
        {
            RemplirDvdListeComplete();
        }

        /// <summary>
        /// Affichage de la liste complète des Dvd
        /// et annulation de toutes les recherches et filtres
        /// </summary>
        private void RemplirDvdListeComplete()
        {
            RemplirDvdListe(lesDvd);
            VideDvdZones();
        }

        /// <summary>
        /// vide les zones de recherche et de filtre
        /// </summary>
        private void VideDvdZones()
        {
            cbxDvdGenres.SelectedIndex = -1;
            cbxDvdRayons.SelectedIndex = -1;
            cbxDvdPublics.SelectedIndex = -1;
            txbDvdNumRecherche.Text = "";
            txbDvdTitreRecherche.Text = "";
        }

        /// <summary>
        /// Tri sur les colonnes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvDvdListe_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            VideDvdZones();
            string titreColonne = dgvDvdListe.Columns[e.ColumnIndex].HeaderText;
            List<Dvd> sortedList = new List<Dvd>();
            switch (titreColonne)
            {
                case "Id":
                    sortedList = lesDvd.OrderBy(o => o.Id).ToList();
                    break;
                case "Titre":
                    sortedList = lesDvd.OrderBy(o => o.Titre).ToList();
                    break;
                case "Duree":
                    sortedList = lesDvd.OrderBy(o => o.Duree).ToList();
                    break;
                case "Realisateur":
                    sortedList = lesDvd.OrderBy(o => o.Realisateur).ToList();
                    break;
                case "Genre":
                    sortedList = lesDvd.OrderBy(o => o.Genre).ToList();
                    break;
                case "Public":
                    sortedList = lesDvd.OrderBy(o => o.Public).ToList();
                    break;
                case "Rayon":
                    sortedList = lesDvd.OrderBy(o => o.Rayon).ToList();
                    break;
            }
            RemplirDvdListe(sortedList);
        }

        #endregion


        #region Réception Exemplaire de presse
        //-----------------------------------------------------------
        // ONGLET "RECEPTION DE REVUES"
        //-----------------------------------------------------------

        /// <summary>
        /// Ouverture de l'onglet : blocage en saisie des champs de saisie des infos de l'exemplaire
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabReceptionRevue_Enter(object sender, EventArgs e)
        {
            lesRevues = controle.GetAllRevues();
            accesReceptionExemplaireGroupBox(false);
        }

        /// <summary>
        /// Remplit le dategrid avec la liste reçue en paramètre
        /// </summary>
        private void RemplirReceptionExemplairesListe(List<Exemplaire> exemplaires)
        {
            bdgExemplairesListe.DataSource = exemplaires;
            dgvReceptionExemplairesListe.DataSource = bdgExemplairesListe;
            dgvReceptionExemplairesListe.Columns["idEtat"].Visible = false;
            dgvReceptionExemplairesListe.Columns["idDocument"].Visible = false;
            dgvReceptionExemplairesListe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvReceptionExemplairesListe.Columns["numero"].DisplayIndex = 0;
            dgvReceptionExemplairesListe.Columns["dateAchat"].DisplayIndex = 1;
        }

        /// <summary>
        /// Recherche d'un numéro de revue et affiche ses informations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReceptionRechercher_Click(object sender, EventArgs e)
        {
            if (!txbReceptionRevueNumero.Text.Equals(""))
            {
                Revue revue = lesRevues.Find(x => x.Id.Equals(txbReceptionRevueNumero.Text));
                if (revue != null)
                {
                    AfficheReceptionRevueInfos(revue);
                }
                else
                {
                    MessageBox.Show("numéro introuvable");
                    VideReceptionRevueInfos();
                }
            }
            else
            {
                VideReceptionRevueInfos();
            }
        }

        /// <summary>
        /// Si le numéro de revue est modifié, la zone de l'exemplaire est vidée et inactive
        /// les informations de la revue son aussi effacées
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txbReceptionRevueNumero_TextChanged(object sender, EventArgs e)
        {
            accesReceptionExemplaireGroupBox(false);
            VideReceptionRevueInfos();
        }

        /// <summary>
        /// Affichage des informations de la revue sélectionnée et les exemplaires
        /// </summary>
        /// <param name="revue"></param>
        private void AfficheReceptionRevueInfos(Revue revue)
        {
            // informations sur la revue
            txbReceptionRevuePeriodicite.Text = revue.Periodicite;
            chkReceptionRevueEmpruntable.Checked = revue.Empruntable;
            txbReceptionRevueImage.Text = revue.Image;
            txbReceptionRevueDelaiMiseADispo.Text = revue.DelaiMiseADispo.ToString();
            txbReceptionRevueNumero.Text = revue.Id;
            txbReceptionRevueGenre.Text = revue.Genre;
            txbReceptionRevuePublic.Text = revue.Public;
            txbReceptionRevueRayon.Text = revue.Rayon;
            txbReceptionRevueTitre.Text = revue.Titre;
            string image = revue.Image;
            try
            {
                pcbReceptionRevueImage.Image = Image.FromFile(image);
            }
            catch
            {
                pcbReceptionRevueImage.Image = null;
            }
            // affiche la liste des exemplaires de la revue
            afficheReceptionExemplairesRevue();
            // accès à la zone d'ajout d'un exemplaire
            accesReceptionExemplaireGroupBox(true);
        }

        private void afficheReceptionExemplairesRevue()
        {
            string idDocuement = txbReceptionRevueNumero.Text;
            lesExemplaires = controle.GetExemplairesRevue(idDocuement);
            RemplirReceptionExemplairesListe(lesExemplaires);
        }

        /// <summary>
        /// Vide les zones d'affchage des informations de la revue
        /// </summary>
        private void VideReceptionRevueInfos()
        {
            txbReceptionRevuePeriodicite.Text = "";
            chkReceptionRevueEmpruntable.Checked = false;
            txbReceptionRevueImage.Text = "";
            txbReceptionRevueDelaiMiseADispo.Text = "";
            txbReceptionRevueGenre.Text = "";
            txbReceptionRevuePublic.Text = "";
            txbReceptionRevueRayon.Text = "";
            txbReceptionRevueTitre.Text = "";
            pcbReceptionRevueImage.Image = null;
            lesExemplaires = new List<Exemplaire>();
            RemplirReceptionExemplairesListe(lesExemplaires);
            accesReceptionExemplaireGroupBox(false);
        }

        /// <summary>
        /// Vide les zones d'affichage des informations de l'exemplaire
        /// </summary>
        private void VideReceptionExemplaireInfos()
        {
            txbReceptionExemplaireImage.Text = "";
            txbReceptionExemplaireNumero.Text = "";
            pcbReceptionExemplaireImage.Image = null;
            dtpReceptionExemplaireDate.Value = DateTime.Now;
        }

        /// <summary>
        /// Permet ou interdit l'accès à la gestion de la réception d'un exemplaire
        /// et vide les objets graphiques
        /// </summary>
        /// <param name="acces"></param>
        private void accesReceptionExemplaireGroupBox(bool acces)
        {
            VideReceptionExemplaireInfos();
            grpReceptionExemplaire.Enabled = acces;
        }

        /// <summary>
        /// Recherche image sur disque (pour l'exemplaire)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReceptionExemplaireImage_Click(object sender, EventArgs e)
        {
            string filePath = "";
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "Files|*.jpg;*.bmp;*.jpeg;*.png;*.gif";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog.FileName;
            }
            txbReceptionExemplaireImage.Text = filePath;
            try
            {
                pcbReceptionExemplaireImage.Image = Image.FromFile(filePath);
            }
            catch
            {
                pcbReceptionExemplaireImage.Image = null;
            }
        }

        /// <summary>
        /// Enregistrement du nouvel exemplaire
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReceptionExemplaireValider_Click(object sender, EventArgs e)
        {
            if (!txbReceptionExemplaireNumero.Text.Equals(""))
            {
                try
                {
                    int numero = int.Parse(txbReceptionExemplaireNumero.Text);
                    DateTime dateAchat = dtpReceptionExemplaireDate.Value;
                    string photo = txbReceptionExemplaireImage.Text;
                    string idEtat = ETATNEUF;
                    string idDocument = txbReceptionRevueNumero.Text;
                    Exemplaire exemplaire = new Exemplaire(numero, dateAchat, photo, idEtat, idDocument);
                    if (controle.CreerExemplaire(exemplaire))
                    {
                        VideReceptionExemplaireInfos();
                        afficheReceptionExemplairesRevue();
                    }
                    else
                    {
                        MessageBox.Show("numéro de publication déjà existant", "Erreur");
                    }
                }
                catch
                {
                    MessageBox.Show("le numéro de parution doit être numérique", "Information");
                    txbReceptionExemplaireNumero.Text = "";
                    txbReceptionExemplaireNumero.Focus();
                }
            }
            else
            {
                MessageBox.Show("numéro de parution obligatoire", "Information");
            }
        }

        /// <summary>
        /// Tri sur une colonne
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvExemplairesListe_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string titreColonne = dgvReceptionExemplairesListe.Columns[e.ColumnIndex].HeaderText;
            List<Exemplaire> sortedList = new List<Exemplaire>();
            switch (titreColonne)
            {
                case "Numero":
                    sortedList = lesExemplaires.OrderBy(o => o.Numero).Reverse().ToList();
                    break;
                case "DateAchat":
                    sortedList = lesExemplaires.OrderBy(o => o.DateAchat).Reverse().ToList();
                    break;
                case "Photo":
                    sortedList = lesExemplaires.OrderBy(o => o.Photo).ToList();
                    break;
            }
            RemplirReceptionExemplairesListe(sortedList);
        }

        /// <summary>
        /// Sélection d'une ligne complète et affichage de l'image sz l'exemplaire
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvReceptionExemplairesListe_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvReceptionExemplairesListe.CurrentCell != null)
            {
                Exemplaire exemplaire = (Exemplaire)bdgExemplairesListe.List[bdgExemplairesListe.Position];
                string image = exemplaire.Photo;
                try
                {
                    pcbReceptionExemplaireRevueImage.Image = Image.FromFile(image);
                }
                catch
                {
                    pcbReceptionExemplaireRevueImage.Image = null;
                }
            }
            else
            {
                pcbReceptionExemplaireRevueImage.Image = null;
            }
        }

        #endregion

        #region Commande de livre

        //-----------------------------------------------------------
        // ONGLET "COMMANDES DE LIVRES"
        //-----------------------------------------------------------



        /// <summary>
        /// Ouverture de l'onglet Commande de livres : 
        /// Récupération des livres pour pouvoir afficher les différentes informatios sur le livre 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabCommandeLivres_Enter(object sender, EventArgs e)
        {
            lesLivres = controle.GetAllLivres();
            lesSuivis = controle.GetAllSuivis();
            AccesGestionCommandeLivres(false);
            txbCommandeLivreNumero.Text = "";
            VideCommandeLivresInfos();
            VideDetailsCommandeLivres();
        }

        /// <summary>
        /// Lors du clic sur le bouton "Rechercher" on obtient les informations du livre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCommandeLivreRechercher_Click(object sender, EventArgs e)
        {
            CommandeLivresRechercher();
        }

        /// <summary>
        /// Quand l'on appuie sur le bouton "Entrer" on obtient les informations du livre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txbCommandeLivreNumero_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnCommandeLivreRechercher_Click(sender, e);
            }
        }


        /// <summary>
        /// Recherche d'un livre à partir du numéro et affichage les informations
        /// </summary>txbCommandeLivresNumeroCommande
        private void CommandeLivresRechercher()
        {
            if (!txbCommandeLivreNumero.Text.Equals(""))
            {
                Livre livre = lesLivres.Find(x => x.Id.Equals(txbCommandeLivreNumero.Text.Trim()));
                if (livre != null)
                {
                    AfficheCommandeLivresInfos(livre);
                }
                else
                {
                    MessageBox.Show("Numéro introuvable");
                    txbCommandeLivreNumero.Text = "";
                    txbCommandeLivreNumero.Focus();
                    VideCommandeLivresInfos();
                }
            }
            else
            {
                VideCommandeLivresInfos();
            }
        }

        /// <summary>
        /// Affichage les informations du livre
        /// </summary>
        /// <param name="livre">Le livre sélectionné</param>
        private void AfficheCommandeLivresInfos(Livre livre)
        {
            txbCommandeLivresTitre.Text = livre.Titre;
            txbCommandeLivresAuteur.Text = livre.Auteur;
            txbCommandeLivresCollection.Text = livre.Collection;
            txbCommandeLivresGenre.Text = livre.Genre;
            txbCommandeLivresPublic.Text = livre.Public;
            txbCommandeLivresRayon.Text = livre.Rayon;
            txbCommandeLivresImage.Text = livre.Image;
            txbCommandeLivresISBN.Text = livre.Isbn;
            string image = livre.Image;
            try
            {
                pcbCommandeLivresImage.Image = Image.FromFile(image);
            }
            catch
            {
                pcbCommandeLivresImage.Image = null;
            }
            AfficheCommandeDocumentLivre();
            AccesGestionCommandeLivres(true);
        }

        /// <summary>
        /// Vide les zones d'affchage des informations du livre
        /// </summary>
        private void VideCommandeLivresInfos()
        {
            txbCommandeLivresTitre.Text = "";
            txbCommandeLivresAuteur.Text = "";
            txbCommandeLivresCollection.Text = "";
            txbCommandeLivresGenre.Text = "";
            txbCommandeLivresPublic.Text = "";
            txbCommandeLivresRayon.Text = "";
            txbCommandeLivresImage.Text = "";
            txbCommandeLivresISBN.Text = "";
            pcbCommandeLivresImage.Image = null;
        }

        /// <summary>
        /// Vide les zones d'affichage des détails de commande.
        /// </summary>
        private void VideDetailsCommandeLivres()
        {
            txbCommandeLivresNumeroCommande.Text = "";
            datepickCommandeLivresDateCommande.Value = DateTime.Now;
            numCommandeLivresExemplaires.Value = 1;
            txbCommandeLivresMontant.Text = "";
        }


        /// <summary>
        /// Affiche les détails d'une commande de livre
        /// </summary>
        /// <param name="commandeDocument">Commande concernée</param>
        private void AfficheCommandeLivresDetails(CommandeDocument commandeDocument)
        {
            txbCommandeLivresNumeroCommande.Text = commandeDocument.Id;
            datepickCommandeLivresDateCommande.Value = commandeDocument.DateCommande;
            numCommandeLivresExemplaires.Value = commandeDocument.NbExemplaires;
            txbCommandeLivresMontant.Text = commandeDocument.Montant.ToString("C2", CultureInfo.CreateSpecificCulture("fr-FR"));

        }

        /// <summary>
        /// Quand le numéro de document est modifié, les information sont effacées
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txbCommandeLivreNumero_TextChanged(object sender, EventArgs e)
        {
            AccesGestionCommandeLivres(false);
            VideCommandeLivresInfos();
        }

        /// <summary>
        /// Zone de gestion des commande
        /// </summary>
        /// <param name="acces">true autorise l'accès</param>
        private void AccesGestionCommandeLivres(bool acces)
        {
            grpGestionCommandeLivres.Enabled = acces;
            btnCommandeLivresAjouter.Enabled = acces;

        }

        /// <summary>
        /// Affichage des commandes d'un livre
        /// </summary>
        private void AfficheCommandeDocumentLivre()
        {
            string idDocument = txbCommandeLivreNumero.Text.Trim();
            lesCommandeDocument = controle.GetCommandeDocument(idDocument);
            RemplirCommandeLivresListe(lesCommandeDocument);
        }

        /// <summary>
        /// Remplissage du dategrid avec la collection CommandeDocument
        /// </summary>
        /// <param name="lesCommandeDocument">Collection de CommandeDocument</param>
        private void RemplirCommandeLivresListe(List<CommandeDocument> lesCommandeDocument)
        {
            bdgCommandesLivresListe.DataSource = lesCommandeDocument;
            dgvCommandeLivresListe.DataSource = bdgCommandesLivresListe;
            dgvCommandeLivresListe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvCommandeLivresListe.Columns["Id"].Visible = false;
            dgvCommandeLivresListe.Columns["IdSuivi"].Visible = false;
            dgvCommandeLivresListe.Columns["IdLivreDvd"].Visible = false;
            dgvCommandeLivresListe.Columns["DateCommande"].DisplayIndex = 0;
            dgvCommandeLivresListe.Columns[5].HeaderCell.Value = "Date";
            dgvCommandeLivresListe.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvCommandeLivresListe.Columns["Montant"].DisplayIndex = 1;
            dgvCommandeLivresListe.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvCommandeLivresListe.Columns[6].DefaultCellStyle.Format = "c2";
            dgvCommandeLivresListe.Columns[6].DefaultCellStyle.FormatProvider = CultureInfo.GetCultureInfo("fr-FR");
            dgvCommandeLivresListe.Columns[0].HeaderCell.Value = "Exemplaires";
            dgvCommandeLivresListe.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvCommandeLivresListe.Columns[2].HeaderCell.Value = "Etat";
            dgvCommandeLivresListe.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        /// <summary>
        /// Permet de trier en fonction des colonnes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvLivresListeCommande_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string titreColonne = dgvCommandeLivresListe.Columns[e.ColumnIndex].HeaderText;
            List<CommandeDocument> sortedList = new List<CommandeDocument>();
            switch (titreColonne)
            {
                case "Date":
                    sortedList = lesCommandeDocument.OrderBy(o => o.DateCommande).Reverse().ToList();
                    break;
                case "Montant":
                    sortedList = lesCommandeDocument.OrderBy(o => o.Montant).Reverse().ToList();
                    break;
                case "Exemplaires":
                    sortedList = lesCommandeDocument.OrderBy(o => o.NbExemplaires).Reverse().ToList();
                    break;
                case "Etat":
                    sortedList = lesCommandeDocument.OrderBy(o => o.IdSuivi).ToList();
                    break;
            }
            RemplirCommandeLivresListe(sortedList);
        }
        /// <summary>
        /// Evénement clic sur le bouton d'ajout de commande
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCommandeLivresAjouter_Click(object sender, EventArgs e)
        {
            AccesDetailsCommandeLivres(true);
            AccesModificationCommandeLivres(true);
        }

        /// <summary>
        /// Activation de la zone détails d'une nouvellecommande
        /// </summary>
        /// <param name="acces"></param>
        private void AccesDetailsCommandeLivres(bool acces)
        {
            VideDetailsCommandeLivres();
            grpCommandeLivres.Enabled = acces;
            txbCommandeLivresNumeroCommande.Enabled = acces;
            datepickCommandeLivresDateCommande.Enabled = acces;
            numCommandeLivresExemplaires.Enabled = acces;
            txbCommandeLivresMontant.Enabled = acces;
            btnCommandeLivresEnregistrer.Enabled = acces;
            btnCommandeLivresAnnuler.Enabled = acces;
            btnCommandeLivresAjouter.Enabled = !acces;
        }

        /// <summary>
        /// Active/Désactive les boutons de gestion de commande (sauf ajout)
        /// </summary>
        private void AccesModificationCommandeLivres(bool acces)
        {
            btnCommandeLivresRelancer.Enabled = acces;
            btnCommandeLivresConfirmer.Enabled = acces;
            btnCommandeLivresRegler.Enabled = acces;
            btnCommandeLivresSupprimer.Enabled = acces;
        }

        /// <summary>
        /// Evénement clic sur le bouton 'enregistrer'
        /// Création d'une nouvelle commande
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCommandeLivresEnregistrer_Click(object sender, EventArgs e)
        {
            if (txbCommandeLivresNumeroCommande.Text == "" || txbCommandeLivresMontant.Text == "")
            {
                MessageBox.Show("Vous devez remplir tous les champs.", "Information");
                return;
            }

            String id = txbCommandeLivresNumeroCommande.Text;
            DateTime dateCommande = datepickCommandeLivresDateCommande.Value;
            int nbExemplaires = (int)numCommandeLivresExemplaires.Value;
            string idLivreDvd = txbCommandeLivreNumero.Text.Trim();
            int idSuivi = lesSuivis[0].Id;
            string libelleSuivi = lesSuivis[0].Libelle;
            String montantSaisie = txbCommandeLivresMontant.Text.Replace(',', '.');

            // validation du champ montant
            if (!Double.TryParse(montantSaisie, out double montant))
            {
                MessageBox.Show("Le montant doit être numérique.", "Attention");
                txbCommandeLivresMontant.Text = "";
                txbCommandeLivresMontant.Focus();
                return;
            }
            CommandeDocument laCommandeDocument = new CommandeDocument(id, dateCommande, montant, nbExemplaires, idLivreDvd, idSuivi, libelleSuivi);
            if (controle.CreerCommandeDocument(laCommandeDocument))
            {
                AfficheCommandeDocumentLivre();
                int addedRowIndex = -1;
                DataGridViewRow row = dgvCommandeLivresListe.Rows
                    .Cast<DataGridViewRow>()
                    .First(r => r.Cells["id"].Value.ToString().Equals(id));
                addedRowIndex = row.Index;
                dgvCommandeLivresListe.Rows[addedRowIndex].Selected = true;
            }
            else
            {
                MessageBox.Show("Le numéro de commande existe déjà.", "Erreur");
                txbCommandeLivresNumeroCommande.Text = "";
                txbCommandeLivresNumeroCommande.Focus();
            }
            AccesDetailsCommandeLivres(false);
            AccesGestionCommandeLivres(true);
        }

        /// <summary>
        /// Annulation d'une commande
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCommandeLivresAnnuler_Click(object sender, EventArgs e)
        {
            if (!(txbCommandeLivresNumeroCommande.Text == "" && txbCommandeLivresMontant.Text == ""))
            {
                if (ConfirmationAnnulationCommande())
                {
                    AccesDetailsCommandeLivres(false);
                    AccesGestionCommandeLivres(true);
                }
            }
            else
            {
                AccesDetailsCommandeLivres(false);
                AccesGestionCommandeLivres(true);
            }
        }


        /// <summary>
        /// Suppression d'une commande
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCommandeLivresSupprimer_Click(object sender, EventArgs e)
        {
            if (ConfirmationSupprCommande())
            {
                CommandeDocument commandeDocument = (CommandeDocument)bdgCommandesLivresListe.Current;
                if (controle.SupprCommandeDocument(commandeDocument.Id))
                {
                    AfficheCommandeDocumentLivre();
                }
                else
                {
                    MessageBox.Show("Une erreur s'est produite.", "Erreur");
                }
            }
        }

        /// <summary>
        /// Modifie l'état de la commande à : rélancée
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCommandeLivresRelancer_Click(object sender, EventArgs e)
        {
            CommandeDocument commandeDocument = (CommandeDocument)bdgCommandesLivresListe.List[bdgCommandesLivresListe.Position];
            Suivi nouveauSuivi = lesSuivis.Find(suivi => suivi.Libelle == "Relancée");
            ModifEtatSuiviCommandeDocumentLivre(commandeDocument.Id, nouveauSuivi);
        }

        /// <summary>
        /// Modifie l'état de la commande à : livrée
        /// Notifie la création des exemplaires
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCommandeLivresConfirmerLivraison_Click(object sender, EventArgs e)
        {
            CommandeDocument commandeDocument = (CommandeDocument)bdgCommandesLivresListe.List[bdgCommandesLivresListe.Position];
            Suivi nouveauSuivi = lesSuivis.Find(suivi => suivi.Libelle == "Livrée");
            if (ModifEtatSuiviCommandeDocumentLivre(commandeDocument.Id, nouveauSuivi))
            {
                MessageBox.Show("Les exemplaires ont été ajoutés dans la base de données.", "Information");
            }
        }

        /// <summary>
        /// Modifie l'état de la commande à : réglée
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCommandeLivresRegler_Click(object sender, EventArgs e)
        {
            CommandeDocument commandeDocument = (CommandeDocument)bdgCommandesLivresListe.List[bdgCommandesLivresListe.Position];
            Suivi nouveauSuivi = lesSuivis.Find(suivi => suivi.Libelle == "Réglée");
            ModifEtatSuiviCommandeDocumentLivre(commandeDocument.Id, nouveauSuivi);
        }

        /// <summary>
        /// Demande de modification de l'état de suivi au contrôleur après validation utilisateur
        /// </summary>
        /// <param name="idCommandeDocument">identifiant du document concerné</param>
        /// <param name="nouveauSuivi">nouvel état de suivi</param>
        /// <returns>True si modification a réussi</returns>
        private bool ModifEtatSuiviCommandeDocumentLivre(string idCommandeDocument, Suivi nouveauSuivi)
        {
            if (ConfirmationModifSuiviCommande(nouveauSuivi.Libelle))
            {
                if (controle.ModifSuiviCommandeDocument(idCommandeDocument, nouveauSuivi.Id))
                {
                    AfficheCommandeDocumentLivre();
                    return true;
                }
                else
                {
                    MessageBox.Show("Une erreur s'est produite.", "Erreur");
                    return false;
                }
            }
            return false;
        }

        /// <summary>
        /// Affiche le détail de la commande sélectionnée
        /// </summary>
        private void AfficheCommandeLivresDetailSelect()
        {
            if (dgvCommandeLivresListe.CurrentCell != null)
            {
                CommandeDocument commandeDocument = (CommandeDocument)bdgCommandesLivresListe.List[bdgCommandesLivresListe.Position];
                AfficheCommandeLivresDetails(commandeDocument);
                AccesBtnModificationCommandeLivres(commandeDocument);
            }
            else
            {
                AccesGestionCommandeLivres(false);
                VideDetailsCommandeLivres();
            }
        }

        private void dgvCommandeLivresListe_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCommandeLivresListe.CurrentCell != null)
            {
                AfficheCommandeLivresDetailSelect();
            }
        }

        /// <summary>
        /// Active/Désactive les boutons de gestion de commande en fonction de l'état de suivi
        /// </summary>
        /// <param name="commandeDocument">CommandeDocument concernée</param>
        private void AccesBtnModificationCommandeLivres(CommandeDocument commandeDocument)
        {
            string etatSuivi = commandeDocument.LibelleSuivi;
            switch (etatSuivi)
            {
                case "En cours":
                case "Relancée":
                    btnCommandeLivresRelancer.Enabled = true;
                    btnCommandeLivresConfirmer.Enabled = true;
                    btnCommandeLivresRegler.Enabled = false;
                    btnCommandeLivresSupprimer.Enabled = true;
                    break;
                case "Livrée":
                    btnCommandeLivresRelancer.Enabled = false;
                    btnCommandeLivresConfirmer.Enabled = false;
                    btnCommandeLivresRegler.Enabled = true;
                    btnCommandeLivresSupprimer.Enabled = false;
                    break;
                case "Réglée":
                    AccesModificationCommandeLivres(false);
                    break;
            }
        }
        #endregion

        #region Commande DVD

        //-----------------------------------------------------------
        // ONGLET "DVD"
        //-----------------------------------------------------------

        /// <summary>
        /// Onglet DVD
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabCommandeDVD_Enter(object sender, EventArgs e)
        {
            lesDvd = controle.GetAllDvd();
            lesSuivis = controle.GetAllSuivis();
            AccesGestionCommandeDvd(false);
            txbCommandeDVDNumero.Text = "";
            VideCommandeDvdInfos();
            VideDetailsCommandeDvd();
            dgvCommandeDVDListe.DataSource = null;
        }

        /// <summary>
        /// Recherche d'un DVD à partir du numéro et affichage les informations
        /// </summary>
        private void CommandeDvdRechercher()
        {
            if (!txbCommandeDVDNumero.Text.Equals(""))
            {
                Dvd dvd = lesDvd.Find(x => x.Id.Equals(txbCommandeDVDNumero.Text.Trim()));
                if (dvd != null)
                {
                    AfficheCommandeDvdInfos(dvd);
                }
                else
                {
                    MessageBox.Show("Numéro introuvable");
                    txbCommandeDVDNumero.Text = "";
                    txbCommandeDVDNumero.Focus();
                    VideCommandeDvdInfos();
                }
            }
            else
            {
                VideCommandeDvdInfos();
            }
        }

        /// <summary>
        /// Affiche les informations du DVD
        /// </summary>
        /// <param name="dvd">Le DVD sélectionné</param>
        private void AfficheCommandeDvdInfos(Dvd dvd)
        {
            // affiche les informations
            txbCommandeDVDTitre.Text = dvd.Titre;
            txbCommandeDVDReali.Text = dvd.Realisateur;
            txbCommandeDVDSynopsis.Text = dvd.Synopsis;
            txbCommandeDVDGenre.Text = dvd.Genre;
            txbCommandeDVDPublic.Text = dvd.Public;
            txbCommandeDVDRayon.Text = dvd.Rayon;
            txbCommandeDVDImage.Text = dvd.Image;
            txbCommandeDureeDVD.Text = dvd.Duree.ToString();
            string image = dvd.Image;
            try
            {
                pcbCommandeDVDImage.Image = Image.FromFile(image);
            }
            catch
            {
                pcbCommandeDVDImage.Image = null;
            }
            // affiche la liste des commandes
            AfficheCommandeDocumentDvd();

            // active la zone de gestion des commandes
            AccesGestionCommandeDvd(true);
        }

        /// <summary>
        /// Récupère, affiche les commandes d'un DVD
        /// </summary>
        private void AfficheCommandeDocumentDvd()
        {
            string idDocument = txbCommandeDVDNumero.Text.Trim();
            lesCommandeDocument = controle.GetCommandeDocument(idDocument);
            RemplirCommandeDvdListe(lesCommandeDocument);
            AfficheCommandeDvdDetailSelect();
        }

        /// <summary>
        /// Affiche le détail de la commande sélectionnée
        /// </summary>
        private void AfficheCommandeDvdDetailSelect()
        {
            if (dgvCommandeDVDListe.CurrentCell != null)
            {
                CommandeDocument commandeDocument = (CommandeDocument)bdgCommandesDvdListe.List[bdgCommandesDvdListe.Position];
                AfficheCommandeDvdDetails(commandeDocument);
                AccesBtnModificationCommandeDvd(commandeDocument);
            }
            else
            {
                AccesGestionCommandeDvd(false);
                VideDetailsCommandeDvd();
            }
        }

        /// <summary>
        /// Affiche les détails d'une commande de DVD
        /// </summary>
        /// <param name="commandeDocument">Commande concernée</param>
        private void AfficheCommandeDvdDetails(CommandeDocument commandeDocument)
        {
            txbCommandeDVDNumeroCommande.Text = commandeDocument.Id;
            datepickCommandeDVDDateCommande.Value = commandeDocument.DateCommande;
            numCommandeDVDExemplaires.Value = commandeDocument.NbExemplaires;
            txbCommandeDVDMontant.Text = commandeDocument.Montant.ToString("C2", CultureInfo.CreateSpecificCulture("fr-FR"));
        }

        /// <summary>
        /// Remplit le dategrid avec la collection reçue en paramètre
        /// </summary>
        /// <param name="lesCommandeDocument">Collection de CommandeDocument</param>
        private void RemplirCommandeDvdListe(List<CommandeDocument> lesCommandeDocument)
        {
            bdgCommandesDvdListe.DataSource = lesCommandeDocument;
            dgvCommandeDVDListe.DataSource = bdgCommandesDvdListe;
            dgvCommandeDVDListe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvCommandeDVDListe.Columns["id"].Visible = false;
            dgvCommandeDVDListe.Columns["idSuivi"].Visible = false;
            dgvCommandeDVDListe.Columns["idLivreDvd"].Visible = false;
            dgvCommandeDVDListe.Columns["dateCommande"].DisplayIndex = 0;
            dgvCommandeDVDListe.Columns[5].HeaderCell.Value = "Date";
            dgvCommandeDVDListe.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvCommandeDVDListe.Columns["montant"].DisplayIndex = 1;
            dgvCommandeDVDListe.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvCommandeDVDListe.Columns[6].DefaultCellStyle.Format = "c2";
            dgvCommandeDVDListe.Columns[6].DefaultCellStyle.FormatProvider = CultureInfo.GetCultureInfo("fr-FR");
            dgvCommandeDVDListe.Columns[0].HeaderCell.Value = "Exemplaires";
            dgvCommandeDVDListe.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvCommandeDVDListe.Columns[2].HeaderCell.Value = "Etat";
            dgvCommandeDVDListe.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        /// <summary>
        /// Evénement clic sur le bouton de recherche de DVD
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCommandeDvdRechercher_Click(object sender, EventArgs e)
        {
            CommandeDvdRechercher();
        }

        /// <summary>
        /// Evénement sur la touche entrer déclenche la recherche
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txbCommandeDvdNumero_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnCommandeDvdRechercher_Click(sender, e);
            }
        }

        /// <summary>
        /// Vide les zones d'affichage des informations du DVD
        /// </summary>
        private void VideCommandeDvdInfos()
        {
            txbCommandeDVDTitre.Text = "";
            txbCommandeDVDReali.Text = "";
            txbCommandeDVDSynopsis.Text = "";
            txbCommandeDVDGenre.Text = "";
            txbCommandeDVDPublic.Text = "";
            txbCommandeDVDRayon.Text = "";
            txbCommandeDVDImage.Text = "";
            txbCommandeDureeDVD.Text = "";
            pcbCommandeDVDImage.Image = null;
        }


        /// <summary>
        /// Vide les zones d'affichage des détails de commande
        /// </summary>
        private void VideDetailsCommandeDvd()
        {
            txbCommandeDVDNumeroCommande.Text = "";
            datepickCommandeDVDDateCommande.Value = DateTime.Now;
            numCommandeDVDExemplaires.Value = 1;
            txbCommandeDVDMontant.Text = "";
        }

        /// <summary>
        /// Active/Désactive la zone de gestion des commandes et bouton ajouter
        /// </summary>
        /// <param name="acces">true autorise l'accès</param>
        private void AccesGestionCommandeDvd(bool acces)
        {
            grpGestionCommandeDVD.Enabled = acces;
            btnCommandeDVDEnregistrer.Enabled = acces;
        }

        /// <summary>
        /// Active/Désactive la zone détails d'une commande et les boutons (valider, annuler, ajouter)
        /// </summary>
        /// <param name="acces">True active les boutons Valider et Annuler, désactive le bouton Ajouter, dévérouille les champs</param>
        private void AccesDetailsCommandeDvd(bool acces)
        {
            VideDetailsCommandeDvd();
            grpCommandeDVD.Enabled = acces;
            txbCommandeDVDNumeroCommande.Enabled = acces;
            datepickCommandeDVDDateCommande.Enabled = acces;
            numCommandeDVDExemplaires.Enabled = acces;
            txbCommandeDVDMontant.Enabled = acces;
            btnCommandeDVDEnregistrer.Enabled = acces;
            btnCommandeDVDAnnuler.Enabled = acces;
            btnCommandeDVDEnregistrer.Enabled = !acces;
        }

        /// <summary>
        /// Active/Désactive les boutons de gestion de commande (sauf ajout)
        /// </summary>
        private void AccesModificationCommandeDvd(bool acces)
        {
            btnCommandeDVDRelancer.Enabled = acces;
            btnCommandeDVDConfirmation.Enabled = acces;
            btnCommandeDVDRegler.Enabled = acces;
            btnCommandeDVDSupprimer.Enabled = acces;
        }

        /// <summary>
        /// Active/Désactive les boutons de gestion de commande en fonction de l'état de suivi
        /// </summary>
        /// <param name="commandeDocument">CommandeDocument concernée</param>
        private void AccesBtnModificationCommandeDvd(CommandeDocument commandeDocument)
        {
            string etatSuivi = commandeDocument.LibelleSuivi;
            switch (etatSuivi)
            {
                case "En cours":
                case "Relancée":
                    btnCommandeDVDRelancer.Enabled = true;
                    btnCommandeDVDConfirmation.Enabled = true;
                    btnCommandeDVDRegler.Enabled = false;
                    btnCommandeDVDSupprimer.Enabled = true;
                    break;
                case "Livrée":
                    btnCommandeDVDRelancer.Enabled = false;
                    btnCommandeDVDConfirmation.Enabled = false;
                    btnCommandeDVDRegler.Enabled = true;
                    btnCommandeDVDSupprimer.Enabled = false;
                    break;
                case "Réglée":
                    AccesModificationCommandeDvd(false);
                    break;
            }
        }

        /// <summary>
        /// Permet de trier en fonction des colonnes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvCommandeDVDListe_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string titreColonne = dgvCommandeDVDListe.Columns[e.ColumnIndex].HeaderText;
            List<CommandeDocument> sortedList = new List<CommandeDocument>();
            switch (titreColonne)
            {
                case "Date":
                    sortedList = lesCommandeDocument.OrderBy(o => o.DateCommande).Reverse().ToList();
                    break;
                case "Montant":
                    sortedList = lesCommandeDocument.OrderBy(o => o.Montant).Reverse().ToList();
                    break;
                case "Exemplaires":
                    sortedList = lesCommandeDocument.OrderBy(o => o.NbExemplaires).Reverse().ToList();
                    break;
                case "Etat":
                    sortedList = lesCommandeDocument.OrderBy(o => o.IdSuivi).ToList();
                    break;
            }
            RemplirCommandeDvdListe(sortedList);
        }


        #endregion
    }
}