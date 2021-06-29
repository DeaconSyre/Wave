using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region
//searches: todo, refactor, next
//notes:
//-----------
//---------------
#endregion

namespace Wave
{
    public class ProgramSettings
    {
        #region Singleton
        private static ProgramSettings _Instance;

        public static ProgramSettings Instance
        {
            get
            {
                if(_Instance == null)
                {
                    _Instance = new ProgramSettings();
                }

                return _Instance;
            }
            private set { }
        }

        private ProgramSettings() { }
        #endregion


    }
}
