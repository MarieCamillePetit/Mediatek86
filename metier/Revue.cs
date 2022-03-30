
namespace Mediatek86.metier
{
    public class Revue : Document
    {
        public Revue(string id, string titre, string image, string idGenre, string genre,
            string idPublic, string lePublic, string idRayon, string rayon, 
            bool empruntable, string periodicite, int delaiMiseADispo)
             : base(id, titre, image, idGenre, genre, idPublic, lePublic, idRayon, rayon)
        {
            Periodicite = periodicite;
            Empruntable = empruntable;
            DelaiMiseADispo = delaiMiseADispo;
        }


        public string Periodicite { get; set; }
        public bool Empruntable { get; set; }
        public int DelaiMiseADispo { get; set; }
    }
}
