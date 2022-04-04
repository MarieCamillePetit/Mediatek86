using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediatek86.metier
{
    /// <summary>
    /// Class DVD
    /// </summary>
    public class Dvd : LivreDvd
    {

        private readonly int duree;
        private readonly string realisateur;
        private readonly string synopsis;
        /// <summary>
        /// Valorisation des propriété de la classe
        /// </summary>
        /// <param name="id"></param>
        /// <param name="titre"></param>
        /// <param name="image"></param>
        /// <param name="duree"></param>
        /// <param name="realisateur"></param>
        /// <param name="synopsis"></param>
        /// <param name="idGenre"></param>
        /// <param name="genre"></param>
        /// <param name="idPublic"></param>
        /// <param name="lePublic"></param>
        /// <param name="idRayon"></param>
        /// <param name="rayon"></param>
        public Dvd(string id, string titre, string image, int duree, string realisateur, string synopsis,
            string idGenre, string genre, string idPublic, string lePublic, string idRayon, string rayon)
            : base(id, titre, image, idGenre, genre, idPublic, lePublic, idRayon, rayon)
        {
            this.duree = duree;
            this.realisateur = realisateur;
            this.synopsis = synopsis;
        }
        /// <summary>
        /// get durée
        /// </summary>
        public int Duree { get => duree; }
        /// <summary>
        /// get Réalisateur
        /// </summary>
        public string Realisateur { get => realisateur; }
        /// <summary>
        /// get Synopsis
        /// </summary>
        public string Synopsis { get => synopsis; }

    }
}
