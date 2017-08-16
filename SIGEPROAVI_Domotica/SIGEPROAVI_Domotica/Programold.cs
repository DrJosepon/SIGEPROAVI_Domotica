using System;
using System.IO.Ports;
using System.Text;
using System.Threading;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace SIGEPROAVI_Domotica
{
    internal class Programold
    {
        private static MqttClient client = new MqttClient("192.168.1.36");
        private SerialPort Puerto = new SerialPort();

        private static void Maina(string[] args)
        {
            Thread t = new Thread(new ThreadStart(Servicio_SIGEPROAVI));
            t.Start();
        }

        public static void Servicio_SIGEPROAVI()
        {
            mtdCargarTopics();
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