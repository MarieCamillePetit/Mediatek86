using System;


namespace Mediatek86.metier
{
    /// <summary>
    /// Class Abonnement
    /// </summary>
    public class Abonnement : Commande
    {

        private readonly DateTime dateFinAbonnement;
        private readonly string idRevue;

        /// <summary>
        /// Valorisation des propriété de la classe
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dateCommande"></param>
        /// <param name="montant"></param>
        /// <param name="dateFinAbonnement"></param>
        /// <param name="idRevue"></param>
        public Abonnement(string id, DateTime dateCommande, double montant, DateTime dateFinAbonnement, string idRevue) : base(id, dateCommande, montant)
        {
            this.dateFinAbonnement = dateFinAbonnement;
            this.idRevue = idRevue;
        }

        /// <summary>
        /// getter sur la date de fin d'abonnement
        /// </summary>
        public DateTime DateFinAbonnement { get => dateFinAbonnement; }
        /// <summary>
        /// Getter sur l'idRevue
        /// </summary>
        public string IdRevue { get => idRevue; }
    }
}