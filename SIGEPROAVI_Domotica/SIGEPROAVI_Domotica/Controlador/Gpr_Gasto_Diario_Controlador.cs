using RestSharp;
using SIGEPROAVI_Domotica.DTO;

namespace SIGEPROAVI_Domotica.Controlador
{
    public static class Gpr_Gasto_Diario_Controlador
    {
        private static string rutaAPI = System.Configuration.ConfigurationManager.AppSettings["RutaAPI"].ToString();

        private static RestClient client = new RestClient(rutaAPI);

        public static void GuardarGastoDiario(Gpr_Gasto_DiarioDTO data)
        {
            var request = new RestRequest("Gpr_Gasto_Diario", Method.POST);
            request.RequestFormat = DataFormat.Json;
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