using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediatek86.metier
{
    public class Service
    {
        private readonly int id;
        private readonly string libelle;

        public Service(int id, string libelle)
        {
            this.id = id;
            this.libelle = libelle;
        }

        public int Id => id;
        public string Libelle => libelle;
    }
}