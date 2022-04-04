using System;

namespace Mediatek86.metier
{
    /// <summary>
    /// Classe commande
    /// </summary>
    public class Commande
    {
        private readonly string id;
        private readonly DateTime dateCommande;
        private readonly Double montant;
        /// <summary>
        /// Valorisation des propriété de la classe
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dateCommande"></param>
        /// <param name="montant"></param>
        public Commande(string id, DateTime dateCommande, double montant)
        {
            this.id = id;
            this.dateCommande = dateCommande;
            this.montant = montant;
        }
        /// <summary>
        /// Get Id
        /// </summary>
        public string Id { get => id; }
        /// <summary>
        /// Get DateCommande
        /// </summary>
        public DateTime DateCommande { get => dateCommande; }
        /// <summary>
        /// Get Montant
        /// </summary>
        public Double Montant { get => montant; }
    }
}