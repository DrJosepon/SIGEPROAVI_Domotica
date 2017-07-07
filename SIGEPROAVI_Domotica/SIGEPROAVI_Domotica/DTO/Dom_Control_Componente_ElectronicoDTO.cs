using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGEPROAVI_Domotica.DTO
{
    public class Dom_Control_Componente_ElectronicoDTO
    {
        public int IdDomControlComponenteElectronico { get; set; }
        public string Inicio { get; set; }
        public string Fin { get; set; }

        public int IdDomTipoControlComponenteElectronico { get; set; }
        public int IdDomComponenteElectronico { get; set; }
    }
}