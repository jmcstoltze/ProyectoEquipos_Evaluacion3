using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionNegocio
{
    public interface IPersistente
    {
        bool Create();
        bool Read(int EquipoId);
        bool Update(int Equipo);
        bool Delete(int EquipoId);
    }
}
