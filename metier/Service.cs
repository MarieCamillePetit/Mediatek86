using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediatek86.metier
{
    /// <summary>
    /// Class Service
    /// </summary>
    public class Service
    {
        private readonly int id;
        private readonly string libelle;
        /// <summary>
        /// Valorisation des propriété de la classe
        /// </summary>
        /// <param name="id"></param>
        /// <param name="libelle"></param>
        public Service(int id, string libelle)
        {
            this.id = id;
            this.libelle = libelle;
        }
        /// <summary>
        /// get Id
        /// </summary>
        public int Id => id;
        /// <summary>
        /// get Libelle
        /// </summary>
        public string Libelle => libelle;
    }
}