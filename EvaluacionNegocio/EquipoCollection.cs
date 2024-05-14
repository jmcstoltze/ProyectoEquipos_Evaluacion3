using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionNegocio
{
    public class EquipoCollection
    {
        public List<Equipo> ReadAll()
        {
            var equipos = CommonBC.EquipoModelo.VwGetEquipos;
            return ObtenerEquipos(equipos.ToList());
        }

        private List<Equipo> ObtenerEquipos(List<EvaluacionDALC.VwGetEquipos> equiposDALC)
        {
            List<Equipo> equipoList = new List<Equipo>();
            foreach (EvaluacionDALC.VwGetEquipos equipo in equiposDALC)
            {

                Equipo eq = new Equipo();
                eq.EquipoId = equipo.EquipoId;
                eq.NombreEquipo = AES_Helper.DecryptString(equipo.NombreEquipo);
                eq.CantidadJugadores = equipo.CantidadJugadores;
                eq.NombreDT = AES_Helper.DecryptString(equipo.NombreDT);
                eq.TipoEquipo = AES_Helper.DecryptString(equipo.TipoEquipo);
                eq.CapitanEquipo = AES_Helper.DecryptString(equipo.CapitanEquipo);
                eq.TieneSub21 = equipo.TieneSub21;
           
                equipoList.Add(eq);
            }
            return equipoList;
        }
    }
}
