
namespace Mediatek86.metier
{
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

        public string Id { get => id; }
        public string Titre { get => titre; }
        public string Image { get => image; }
        public string IdGenre { get => idGenre; }
        public string Genre { get => genre; }
        public string IdPublic { get => idPublic; }
        public string Public { get => lePublic; }
        public string IdRayon { get => idRayon; }
        public string Rayon { get => rayon; }

    }


}
