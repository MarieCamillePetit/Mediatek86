

namespace Mediatek86.metier
{
    /// <summary>
    /// Class Livre
    /// </summary>
    public class Livre : LivreDvd
    {

        private readonly string isbn;
        private readonly string auteur;
        private readonly string collection;

        /// <summary>
        /// Valorisation des propriété de la classe
        /// </summary>
        /// <param name="id"></param>
        /// <param name="titre"></param>
        /// <param name="image"></param>
        /// <param name="isbn"></param>
        /// <param name="auteur"></param>
        /// <param name="collection"></param>
        /// <param name="idGenre"></param>
        /// <param name="genre"></param>
        /// <param name="idPublic"></param>
        /// <param name="lePublic"></param>
        /// <param name="idRayon"></param>
        /// <param name="rayon"></param>
        public Livre(string id, string titre, string image, string isbn, string auteur, string collection, 
            string idGenre, string genre, string idPublic, string lePublic, string idRayon, string rayon)
            :base(id, titre, image, idGenre, genre, idPublic, lePublic, idRayon, rayon)
        {
            this.isbn = isbn;
            this.auteur = auteur;
            this.collection = collection;
        }
        /// <summary>
        /// Get ISBN
        /// </summary>
        public string Isbn { get => isbn; }
        /// <summary>
        /// Get Auteur
        /// </summary>
        public string Auteur { get => auteur; }
        /// <summary>
        /// Get Collection
        /// </summary>
        public string Collection { get => collection; }

    }
}
