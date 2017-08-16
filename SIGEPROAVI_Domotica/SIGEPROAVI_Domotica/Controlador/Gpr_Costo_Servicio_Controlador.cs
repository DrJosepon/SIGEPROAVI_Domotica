using RestSharp;
using SIGEPROAVI_Domotica.DTO;
using System.Collections.Generic;

namespace SIGEPROAVI_Domotica.Controlador
{
    public static class Gpr_Costo_Servicio_Controlador
    {
        private static string rutaAPI = System.Configuration.ConfigurationManager.AppSettings["RutaAPI"].ToString();

        private static RestClient client = new RestClient(rutaAPI);

        public static List<Gpr_Costo_ServicioDTO> ListarCostoServicios()
        {
            var request = new RestRequest("Gpr_Costo_Servicio/Activo", Method.GET);
            request.RequestFormat = DataFormat.Json;

            //request.AddParameter("Gpr_Costo_Servicio", request.JsonSerializer.Serialize(gpr_Galpon));
            //request.AddBody(gpr_Galpon);

            var response = client.Execute<List<Gpr_Costo_ServicioDTO>>(request);

            return response.Data;
        }
    }
}