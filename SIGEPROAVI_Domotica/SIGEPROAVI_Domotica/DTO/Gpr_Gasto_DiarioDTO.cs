using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGEPROAVI_Domotica.DTO
{
    public class Gpr_Gasto_DiarioDTO
    {
        public int IdGprGastoDiario { get; set; }

        public decimal Gasto { get; set; }

        public int IdGprMedicionDiaria { get; set; }
        public int IdGprCostoServicio { get; set; }
    }
}