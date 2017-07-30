using RestSharp;
using SIGEPROAVI_Domotica.DTO;

namespace SIGEPROAVI_Domotica.Controlador
{
    public static class Gpr_Medicion_Diaria_Controlador
    {
        private static string rutaAPI = System.Configuration.ConfigurationManager.AppSettings["RutaAPI"].ToString();

        private static RestClient client = new RestClient(rutaAPI);

        public static Gpr_Medicion_DiariaDTO GuardarMedicionDiaria(Gpr_Medicion_DiariaDTO data)
        {
            var request = new RestRequest("Gpr_Medicion_Diaria", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Content-type", "application/json");
            request.AddJsonBody(data);
            //request.AddBody(data);

            //request.AddParameter("Gpr_Galpon", request.JsonSerializer.Serialize(gpr_Galpon));
            //request.AddBody(gpr_Galpon);

            var response = client.Execute<Gpr_Medicion_DiariaDTO>(request);

            //Gpr_Medicion_DiariaDTO medicionDiaria = Newtonsoft.Json.JsonConvert.DeserializeObject<Gpr_Medicion_DiariaDTO>(response.Content.ToString());

            return response.Data;
        }
    }
}