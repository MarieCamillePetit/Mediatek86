

namespace Mediatek86.metier
{
    public abstract class Categorie
    {
        private readonly string id;
        private readonly string libelle;

        protected Categorie(string id, string libelle)
        {
            this.id = id;
            this.libelle = libelle;
        }

        public string Id { get => id; }
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
