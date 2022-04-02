using System;

namespace Mediatek86.metier
{
    public class CommandeDocument : Commande
    {
        private readonly int nbExemplaires;
        private readonly int idSuivi;
        private readonly string libelleSuivi;
        private readonly string idLivreDvd;

        public CommandeDocument(string id, DateTime dateCommande, double montant, int nbExemplaires, string idLivreDvd, int idSuivi, string libelleSuivi) : base(id, dateCommande, montant)
        {
            this.nbExemplaires = nbExemplaires;
            this.idSuivi = idSuivi;
            this.libelleSuivi = libelleSuivi;
            this.idLivreDvd = idLivreDvd;
        }

        public int NbExemplaires { get => nbExemplaires; }
        public int IdSuivi { get => idSuivi; }
        public string LibelleSuivi { get => libelleSuivi; }
        public string IdLivreDvd { get => idLivreDvd; }
    }
}