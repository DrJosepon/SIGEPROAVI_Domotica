using RestSharp;
using SIGEPROAVI_Domotica.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGEPROAVI_Domotica.Controlador
{
    public static class Gpr_Galpon_Controlador
    {
        private static RestClient client = new RestClient("http://localhost:53578/api/");

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