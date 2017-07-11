using RestSharp;
using SIGEPROAVI_Domotica.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGEPROAVI_Domotica.Controlador
{
    public static class Gpr_Medicion_Diaria_Controlador
    {
        private static string rutaAPI = System.Configuration.ConfigurationManager.AppSettings["RutaAPI"].ToString();

        private static RestClient client = new RestClient(rutaAPI);

        public static void GuardarMedicionDiaria(Gpr_Medicion_DiariaDTO data)
        {
            var request = new RestRequest("Gpr_Medicion_Diaria", Method.POST);
            request.AddHeader("Content-type", "application/json");
            request.AddJsonBody(data);
            //request.AddBody(data);

            //request.AddParameter("Gpr_Galpon", request.JsonSerializer.Serialize(gpr_Galpon));
            //request.AddBody(gpr_Galpon);

            var response = client.Execute<object>(request);

            //return response.Data;
        }
    }
}