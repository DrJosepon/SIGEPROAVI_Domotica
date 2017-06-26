using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGEPROAVI_Domotica.DTO
{
    public class Gpr_GalponDTO : BaseEntidad
    {
        public int IdGprGalpon { get; set; }

        public int CantidadAves { get; set; }

        public string Descripcion { get; set; }
    }
}