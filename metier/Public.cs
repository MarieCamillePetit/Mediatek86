using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediatek86.metier
{
    /// <summary>
    /// Class public
    /// </summary>
    public class Public : Categorie
    {

        /// <summary>
        /// Valorisation des propriété de la classe
        /// </summary>
        /// <param name="id"></param>
        /// <param name="libelle"></param>
        public Public(string id, string libelle):base(id, libelle)
        {
        }

    }
}
