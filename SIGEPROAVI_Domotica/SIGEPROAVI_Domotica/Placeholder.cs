﻿using SIGEPROAVI_Domotica.DTO;
using SIGEPROAVI_Domotica.Controlador;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace SIGEPROAVI_Domotica
{
    internal class Placeholder
    {
        private static MqttClient client = new MqttClient("192.168.1.36");
        private SerialPort Puerto = new SerialPort();
        public static List<Gpr_GalponDTO> galpones = new List<Gpr_GalponDTO>();

        private static void Main(string[] args)
        {
            Thread t = new Thread(new ThreadStart(Servicio_SIGEPROAVI));
            t.Start();
        }

        public static void Servicio_SIGEPROAVI()
        {
            mtdCargarInfo();
            mtdCargarTopics();
        }

        private static void mtdCargarInfo()
        {
            galpones = Gpr_Galpon_Controlador.ListarGalpones();
        }

        private static void mtdCargarTopics()
        {
            client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
            byte code = client.Connect(Guid.NewGuid().ToString());
            client.Subscribe(new string[] { "event" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
            client.Subscribe(new string[] { "temp" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
            client.Subscribe(new string[] { "hum" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
        }

        public static void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            foreach (Gpr_GalponDTO galpon in galpones)
            {
                Console.WriteLine(galpon.Descripcion + ":");

                if (e.Topic == "temp")
                {
                    //Debug.WriteLine("Received = " + Encoding.UTF8.GetString(e.Message) + " on topic " + e.Topic);

                    Console.WriteLine(Encoding.UTF8.GetString(e.Message) + "°C");
                }

                if (e.Topic == "hum")
                {
                    //Debug.WriteLine("Received = " + Encoding.UTF8.GetString(e.Message) + " on topic " + e.Topic);

                    Console.WriteLine(Encoding.UTF8.GetString(e.Message) + "%");
                }
            }
        }
    }
}