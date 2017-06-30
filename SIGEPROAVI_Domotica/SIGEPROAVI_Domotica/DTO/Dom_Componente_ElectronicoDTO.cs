using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGEPROAVI_Domotica.DTO
{
    public class Dom_Componente_ElectronicoDTO
    {
        public int IdDomComponenteElectronico { get; set; }

        public string Topic { get; set; }

        public int IdDomTipoComponenteElectronico { get; set; }
        public int IdGprGalpon { get; set; }
        public int IdGprServicio { get; set; }
    }
}