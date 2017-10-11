using RestSharp;
using SIGEPROAVI_Domotica.DTO;
using System.Collections.Generic;

namespace SIGEPROAVI_Domotica.Controlador
{
    public static class Gpr_Tipo_Servicio_Controlador
    {
        private static string rutaAPI = System.Configuration.ConfigurationManager.AppSettings["RutaAPI"].ToString();

        private static RestClient client = new RestClient(rutaAPI);

        public static List<Gpr_Tipo_ServicioDTO> ListarTipoServicio()
        {
            var request = new RestRequest("Gpr_Tipo_Servicio", Method.GET);
            request.RequestFormat = DataFormat.Json;

            var response = client.Execute<List<Gpr_Tipo_ServicioDTO>>(request);

            return response.Data;
        }
    }
}