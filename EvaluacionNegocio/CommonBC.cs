using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionNegocio
{
    public class CommonBC
    {
        private static EvaluacionDALC.PCEEntities _equipoModelo;

        public static EvaluacionDALC.PCEEntities EquipoModelo
        {
            get
            {
                if (_equipoModelo == null)
                {
                    _equipoModelo = new EvaluacionDALC.PCEEntities();
                }
                return _equipoModelo;
            }

        }
    }
}
