using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGEPROAVI_Domotica.DTO
{
    public class Gpr_ServicioDTO : BaseEntidad
    {
        public int IdGprServicio { get; set; }
        public string Descripcion { get; set; }
        public int IdGprTipoServicio { get; set; }
    }
}