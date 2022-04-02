using System;


namespace Mediatek86.metier
{
    public class Suivi
    {
        private readonly int id;
        private readonly string libelle;

        public Suivi(int id, string libelle)
        {
            this.id = id;
            this.libelle = libelle;
        }

        public int Id { get => id; }
        public string Libelle { get => libelle; }
    }
}