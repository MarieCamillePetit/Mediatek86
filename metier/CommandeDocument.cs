using System;

namespace Mediatek86.metier
{
    /// <summary>
    /// Class commandeDocument
    /// </summary>
    public class CommandeDocument : Commande
    {
        private readonly int nbExemplaires;
        private readonly int idSuivi;
        private readonly string libelleSuivi;
        private readonly string idLivreDvd;

        /// <summary>
        /// Valorisation des propriété de la classe
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dateCommande"></param>
        /// <param name="montant"></param>
        /// <param name="nbExemplaires"></param>
        /// <param name="idLivreDvd"></param>
        /// <param name="idSuivi"></param>
        /// <param name="libelleSuivi"></param>
        public CommandeDocument(string id, DateTime dateCommande, double montant, int nbExemplaires, string idLivreDvd, int idSuivi, string libelleSuivi) : base(id, dateCommande, montant)
        {
            this.nbExemplaires = nbExemplaires;
            this.idSuivi = idSuivi;
            this.libelleSuivi = libelleSuivi;
            this.idLivreDvd = idLivreDvd;
        }
        /// <summary>
        /// Get nbExemplaires
        /// </summary>
        public int NbExemplaires { get => nbExemplaires; }
        /// <summary>
        /// get IdSuivi
        /// </summary>
        public int IdSuivi { get => idSuivi; }
        /// <summary>
        /// Get LibelleSuivi
        /// </summary>
        public string LibelleSuivi { get => libelleSuivi; }
        /// <summary>
        /// get IdLivreDVD
        /// </summary>
        public string IdLivreDvd { get => idLivreDvd; }
    }
}