using RestSharp;
using SIGEPROAVI_Domotica.DTO;
using System.Collections.Generic;

namespace SIGEPROAVI_Domotica.Controlador
{
    public static class Dom_Componente_Electronico_Controlador
    {
        private static string rutaAPI = System.Configuration.ConfigurationManager.AppSettings["RutaAPI"].ToString();

        private static RestClient client = new RestClient(rutaAPI);

        public static List<Dom_Componente_ElectronicoDTO> ListarComponenteElectronico()
        {
            var request = new RestRequest("Dom_Componente_Electronico", Method.GET);
            request.RequestFormat = DataFormat.Json;

            var response = client.Execute<List<Dom_Componente_ElectronicoDTO>>(request);

            return response.Data;
        }
    }
}