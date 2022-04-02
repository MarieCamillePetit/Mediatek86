using System;


namespace Mediatek86.metier
{
    public class FinAbonnement
    {
        private readonly DateTime dateFinAbonnement;
        private readonly string idRevue;
        private readonly string titreRevue;

        public FinAbonnement(DateTime dateFinAbonnement, string idRevue, string titreRevue)
        {
            this.dateFinAbonnement = dateFinAbonnement;
            this.idRevue = idRevue;
            this.titreRevue = titreRevue;
        }

        public DateTime DateFinAbonnement => dateFinAbonnement;
        public string IdRevue => idRevue;
        public string TitreRevue => titreRevue;
    }
}