using RestSharp;
using SIGEPROAVI_Domotica.DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGEPROAVI_Domotica.Controlador
{
    public static class Gpr_Servicio_Controlador
    {
        private static string rutaAPI = System.Configuration.ConfigurationManager.AppSettings["RutaAPI"].ToString();

        private static RestClient client = new RestClient(rutaAPI);

        public static List<Gpr_ServicioDTO> ListarServicioes()
        {
            var request = new RestRequest("Gpr_Servicio", Method.GET);
            request.RequestFormat = DataFormat.Json;

            var response = client.Execute<List<Gpr_ServicioDTO>>(request);

            return response.Data;
        }
    }
}