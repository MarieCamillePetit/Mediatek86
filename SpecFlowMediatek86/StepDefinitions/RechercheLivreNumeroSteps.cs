using Mediatek86.vue;
using System.Windows.Forms;
using TechTalk.SpecFlow;
using Mediatek86.controleur;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SpecFlowMediatek86.Steps
{
    [Binding]
    public class RechercheLivreNumeroSteps
    {
        private readonly FrmMediatek frmMediatek = new FrmMediatek(new Controle());

        [Given(@"Je saisis la valeur (.*)")]
        public void GivenJeSaisisLaValeur(string valeur)
        {
            TextBox TxtValeur = (TextBox)frmMediatek.Controls["tabOngletsApplication"].Controls["tabLivres"].Controls["grpLivresRecherche"].Controls["txbLivresNumRecherche"];
            frmMediatek.Visible = true;
            TxtValeur.Text = valeur;
        }

        [When(@"Je clic sur le bouton Rechercher")]
        public void WhenJeClicSurLeBoutonRechercher()
        {
            Button BtnRechercher = (Button)frmMediatek.Controls["tabOngletsApplication"].Controls["tabLivres"].Controls["grpLivresRecherche"].Controls["btnLivresNumRecherche"];
            frmMediatek.Visible = true;
            BtnRechercher.PerformClick();
        }

        [Then(@"Les informations détaillées doivent afficher le titre (.*)")]
        public void ThenLesInformationsDetailleesDoiventAfficherLeTitre(string titreAttendu)
        {
            TextBox TxtTitre = (TextBox)frmMediatek.Controls["tabOngletsApplication"].Controls["tabLivres"].Controls["grpLivresInfos"].Controls["txbLivresTitre"];
            string titreObtenu = TxtTitre.Text;
            Assert.AreEqual(titreAttendu, titreObtenu);
        }
    }
}