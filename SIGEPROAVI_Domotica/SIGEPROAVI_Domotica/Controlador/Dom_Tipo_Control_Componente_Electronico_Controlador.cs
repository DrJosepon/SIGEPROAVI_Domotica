using RestSharp;
using SIGEPROAVI_Domotica.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGEPROAVI_Domotica.Controlador
{
    public static class Dom_Tipo_Control_Componente_Electronico_Controlador
    {
        private static string rutaAPI = System.Configuration.ConfigurationManager.AppSettings["RutaAPI"].ToString();

        private static RestClient client = new RestClient(rutaAPI);

        public static List<Dom_Tipo_Control_Componente_ElectronicoDTO> ListarTipoControlComponenteElectronico()
        {
            var request = new RestRequest("Dom_Tipo_Control_Componente_Electronico", Method.GET);
            request.RequestFormat = DataFormat.Json;

            //request.AddParameter("Gpr_Galpon", request.JsonSerializer.Serialize(gpr_Galpon));
            //request.AddBody(gpr_Galpon);

            var response = client.Execute<List<Dom_Tipo_Control_Componente_ElectronicoDTO>>(request);

            return response.Data;
        }
    }
}