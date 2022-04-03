
namespace Mediatek86.metier
{
    /// <summary>
    /// class document
    /// </summary>
    public class Document
    {

        private readonly string id;
        private readonly string titre;
        private readonly string image;
        private readonly string idGenre;
        private readonly string genre;
        private readonly string idPublic;
        private readonly string lePublic;
        private readonly string idRayon;
        private readonly string rayon;

        /// <summary>
        /// Valorisation des propriété de la classe
        /// </summary>
        /// <param name="id"></param>
        /// <param name="titre"></param>
        /// <param name="image"></param>
        /// <param name="idGenre"></param>
        /// <param name="genre"></param>
        /// <param name="idPublic"></param>
        /// <param name="lePublic"></param>
        /// <param name="idRayon"></param>
        /// <param name="rayon"></param>
        public Document(string id, string titre, string image, string idGenre, string genre, 
            string idPublic, string lePublic, string idRayon, string rayon)
        {
            this.id = id;
            this.titre = titre;
            this.image = image;
            this.idGenre = idGenre;
            this.genre = genre;
            this.idPublic = idPublic;
            this.lePublic = lePublic;
            this.idRayon = idRayon;
            this.rayon = rayon;
        }
        /// <summary>
        /// get Id
        /// </summary>
        public string Id { get => id; }
        /// <summary>
        /// get Titre
        /// </summary>
        public string Titre { get => titre; }
        /// <summary>
        /// get Image
        /// </summary>
        public string Image { get => image; }
        /// <summary>
        /// Get IdGenre
        /// </summary>
        public string IdGenre { get => idGenre; }
        /// <summary>
        /// Get Genre
        /// </summary>
        public string Genre { get => genre; }
        /// <summary>
        /// Get IdPublic
        /// </summary>
        public string IdPublic { get => idPublic; }
        /// <summary>
        /// Get Public
        /// </summary>
        public string Public { get => lePublic; }
        /// <summary>
        /// Get IdRayon
        /// </summary>
        public string IdRayon { get => idRayon; }
        /// <summary>
        /// Get Rayon
        /// </summary>
        public string Rayon { get => rayon; }

    }


}
