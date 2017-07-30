using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGEPROAVI_Domotica.DTO
{
    public class Gpr_Costo_ServicioDTO : BaseEntidad
    {
        public int IdGprCostoServicio { get; set; }

        public decimal Costo { get; set; }

        public DateTime Fecha { get; set; }

        public int IdGprServicio { get; set; }
    }
}