using System;


namespace Mediatek86.metier
{

    public class Abonnement : Commande
    {

        private readonly DateTime dateFinAbonnement;
        private readonly string idRevue;

        public Abonnement(string id, DateTime dateCommande, double montant, DateTime dateFinAbonnement, string idRevue) : base(id, dateCommande, montant)
        {
            this.dateFinAbonnement = dateFinAbonnement;
            this.idRevue = idRevue;
        }


        public DateTime DateFinAbonnement { get => dateFinAbonnement; }

        public string IdRevue { get => idRevue; }
    }
}