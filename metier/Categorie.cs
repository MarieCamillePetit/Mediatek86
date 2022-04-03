

namespace Mediatek86.metier
{
    /// <summary>
    /// Class Categorie
    /// </summary>
    public abstract class Categorie
    {
        private readonly string id;
        private readonly string libelle;
        /// <summary>
        /// Valorisation des propriété de la classe
        /// </summary>
        /// <param name="id"></param>
        /// <param name="libelle"></param>
        protected Categorie(string id, string libelle)
        {
            this.id = id;
            this.libelle = libelle;
        }
        /// <summary>
        /// Get Id
        /// </summary>
        public string Id { get => id; }
        /// <summary>
        /// Get Libelle
        /// </summary>
        public string Libelle { get => libelle; }

        /// <summary>
        /// Récupération du libellé pour l'affichage dans les combos
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.libelle;
        }

    }
}
