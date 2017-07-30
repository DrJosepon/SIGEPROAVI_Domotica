namespace SIGEPROAVI_Domotica.DTO
{
    public class Gpr_Medicion_DiariaDTO
    {
        public int IdGprMedicionDiaria { get; set; }

        public decimal Medicion { get; set; }

        public string Fecha { get; set; }

        public int IdGprServicio { get; set; }
        public int IdGprGalpon { get; set; }
    }
}