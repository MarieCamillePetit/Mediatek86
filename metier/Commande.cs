using System;

namespace Mediatek86.metier
{
    public class Commande
    {
        private readonly string id;
        private readonly DateTime dateCommande;
        private readonly Double montant;

        public Commande(string id, DateTime dateCommande, double montant)
        {
            this.id = id;
            this.dateCommande = dateCommande;
            this.montant = montant;
        }

        public string Id { get => id; }
        public DateTime DateCommande { get => dateCommande; }
        public Double Montant { get => montant; }
    }
}