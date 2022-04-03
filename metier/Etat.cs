

namespace Mediatek86.metier
{
    /// <summary>
    /// Class Etat
    /// </summary>
    public class Etat
    {
        /// <summary>
        /// Valorisation des propriété de la classe
        /// </summary>
        /// <param name="id"></param>
        /// <param name="libelle"></param>
        public Etat(string id, string libelle)
        {
            this.Id = id;
            this.Libelle = libelle;
        }
        /// <summary>
        /// get et set Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Get et set Libelle
        /// </summary>
        public string Libelle { get; set; }
    }
}
