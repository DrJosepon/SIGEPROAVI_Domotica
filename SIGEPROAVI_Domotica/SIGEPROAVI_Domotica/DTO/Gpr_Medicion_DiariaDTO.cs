using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGEPROAVI_Domotica.DTO
{
    public class Gpr_Medicion_DiariaDTO
    {
        public int IdGprMedicionDiaria { get; set; }

        public decimal Medicion { get; set; }

        public DateTime Fecha { get; set; }

        public int IdGprServicio { get; set; }
        public int IdGprGalpon { get; set; }
    }
}