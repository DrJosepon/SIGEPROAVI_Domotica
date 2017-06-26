using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGEPROAVI_Domotica.DTO
{
    public interface IBaseEntidad
    {
        bool Estado { get; set; }
        string UsuarioCreador { get; set; }
        DateTime FechaCreacion { get; set; }
        string UsuarioModificador { get; set; }
        DateTime? FechaModificacion { get; set; }
    }
}