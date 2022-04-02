using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Mediatek86.controleur.Tests
{
    [TestClass()]
    public class ControleTests
    {
        private readonly DateTime beforeDate = new DateTime(2022, 1, 1);
        private readonly DateTime btwDate = new DateTime(2022, 6, 1);
        private readonly DateTime afterDate = new DateTime(2022, 12, 1);

        [TestMethod()]

        public bool ParutionDansAbonnement(DateTime dateCommande, DateTime dateFinAbonnement, DateTime dateParution)
        {
            return (DateTime.Compare(dateCommande, dateParution) < 0 && DateTime.Compare(dateParution, dateFinAbonnement) < 0);
        }

        public void ParutionDansAbonnementTest()
        {
            bool resultat1 = ParutionDansAbonnement(beforeDate, afterDate, btwDate);
            Assert.AreEqual(true, resultat1, "Succès test : dateparution COMPRISE entre date abonnement et fin abonnement => TRUE");

            bool resultat2 = ParutionDansAbonnement(beforeDate, afterDate, beforeDate);
            Assert.AreEqual(false, resultat2, "Succès test : dateparution EGALE à date abonnement => FALSE");

            bool resultat3 = ParutionDansAbonnement(beforeDate, afterDate, afterDate);
            Assert.AreEqual(false, resultat3, "Succès test : dateparution EGALE à date fin abonnement => FALSE");

            // Date parution en dehors des bornes
            bool resultat4 = ParutionDansAbonnement(btwDate, afterDate, beforeDate);
            Assert.AreEqual(false, resultat4, "Succès test : dateparution AVANT date fin abonnement => FALSE");

            bool resultat5 = ParutionDansAbonnement(beforeDate, btwDate, afterDate);
            Assert.AreEqual(false, resultat5, "Succès test : dateparution APRES date fin abonnement => FALSE");
        }
    }
}