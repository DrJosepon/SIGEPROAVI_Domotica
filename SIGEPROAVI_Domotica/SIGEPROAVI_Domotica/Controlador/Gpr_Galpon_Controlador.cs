using RestSharp;
using SIGEPROAVI_Domotica.DTO;
using System.Collections.Generic;

namespace SIGEPROAVI_Domotica.Controlador
{
    public static class Gpr_Galpon_Controlador
    {
        private static string rutaAPI = System.Configuration.ConfigurationManager.AppSettings["RutaAPI"].ToString();

        private static RestClient client = new RestClient(rutaAPI);

        public static List<Gpr_GalponDTO> ListarGalpones()
        {
            var request = new RestRequest("Gpr_Galpon", Method.GET);
            request.RequestFormat = DataFormat.Json;

            //request.AddParameter("Gpr_Galpon", request.JsonSerializer.Serialize(gpr_Galpon));
            //request.AddBody(gpr_Galpon);

            var response = client.Execute<List<Gpr_GalponDTO>>(request);

            return response.Data;
        }
    }
}