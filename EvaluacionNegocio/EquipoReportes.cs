using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionNegocio
{
    public class EquipoReportes
    {

        // Retornan las cantidades de equipo masculinos y femeninos

        public int CantidadEquiposFemeninos()
        {
            return CommonBC.EquipoModelo.spObtenerCantidadEquiposFemeninos().First().Value;
        }
        public int CantidadEquiposMasculinos()
        {                        
            return CommonBC.EquipoModelo.spObtenerCantidadEquiposMasculinos().First().Value;
        }
    }
}
