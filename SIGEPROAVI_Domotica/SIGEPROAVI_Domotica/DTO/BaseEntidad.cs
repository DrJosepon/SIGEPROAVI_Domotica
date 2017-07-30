using System;

namespace SIGEPROAVI_Domotica.DTO
{
    public class BaseEntidad
    {
        public bool Estado { get; set; }
        public string UsuarioCreador { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuarioModificador { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }
}