using System;
using System.Collections.Generic;
using System.Text;

namespace LMG.FAB.Util
{
    public class AppSettings
    {
        /// <summary>
        /// En seconds, c'est la durée de rafraichissement du lock coté client
        /// </summary>
        public int LockRefreshDelay { get; set; }

        /// <summary>
        /// En seconds, si le lock n'est pas rafraichi apres LockExpireDuration, il est supprimé
        /// </summary>
        public int LockExpireDuration { get; set; }
    }
}
