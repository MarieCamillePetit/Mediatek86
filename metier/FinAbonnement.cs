using System;


namespace Mediatek86.metier
{
    /// <summary>
    /// Class FinAbonnement
    /// </summary>
    public class FinAbonnement
    {
        private readonly DateTime dateFinAbonnement;
        private readonly string idRevue;
        private readonly string titreRevue;

        /// <summary>
        /// Valorisation des propriété de la classe
        /// </summary>
        /// <param name="dateFinAbonnement"></param>
        /// <param name="idRevue"></param>
        /// <param name="titreRevue"></param>
        public FinAbonnement(DateTime dateFinAbonnement, string idRevue, string titreRevue)
        {
            this.dateFinAbonnement = dateFinAbonnement;
            this.idRevue = idRevue;
            this.titreRevue = titreRevue;
        }

        /// <summary>
        /// Get dateFinAbonnement
        /// </summary>
        public DateTime DateFinAbonnement => dateFinAbonnement;
        /// <summary>
        /// get IdRevue
        /// </summary>
        public string IdRevue => idRevue;
        /// <summary>
        /// Get TitreRevue
        /// </summary>
        public string TitreRevue => titreRevue;
    }
}