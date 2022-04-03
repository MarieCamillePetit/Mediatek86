using System;

namespace Mediatek86.metier
{
    /// <summary>
    /// Class Exemplaire
    /// </summary>
    public class Exemplaire
    {
        /// <summary>
        /// Valorisation des propriété de la classe
        /// </summary>
        /// <param name="numero"></param>
        /// <param name="dateAchat"></param>
        /// <param name="photo"></param>
        /// <param name="idEtat"></param>
        /// <param name="idDocument"></param>
        public Exemplaire(int numero, DateTime dateAchat, string photo,string idEtat, string idDocument)
        {
            this.Numero = numero;
            this.DateAchat = dateAchat;
            this.Photo = photo;
            this.IdEtat = idEtat;
            this.IdDocument = idDocument;
        }

        /// <summary>
        /// Get et Set Numero
        /// </summary>
        public int Numero { get; set; }
        /// <summary>
        /// get et set Photo
        /// </summary>
        public string Photo { get; set; }
        /// <summary>
        /// get et set DateAchat
        /// </summary>
        public DateTime DateAchat { get; set; }
        /// <summary>
        /// get et set IdEtat
        /// </summary>
        public string IdEtat { get; set; }
        /// <summary>
        /// get et set IdDocument
        /// </summary>
        public string IdDocument { get; set; }
    }
}
