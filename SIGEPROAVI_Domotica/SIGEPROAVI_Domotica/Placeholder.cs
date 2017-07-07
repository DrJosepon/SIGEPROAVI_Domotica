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
        public static List<Gpr_ServicioDTO> servicios = new List<Gpr_ServicioDTO>();
        public static List<Dom_Componente_ElectronicoDTO> componenteselectronicos = new List<Dom_Componente_ElectronicoDTO>();
        public static List<Dom_Tipo_Control_Componente_ElectronicoDTO> tipocontrolescomponenteselectronicos = new List<Dom_Tipo_Control_Componente_ElectronicoDTO>();
        public static List<Gpr_Tipo_ServicioDTO> tiposervicios = new List<Gpr_Tipo_ServicioDTO>();
        public static List<Dom_Control_Componente_ElectronicoDTO> contolcomponenteelectronico = new List<Dom_Control_Componente_ElectronicoDTO>();

        public static int flip = 0;
        public static int flipMinuto = 0;

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
            servicios = Gpr_Servicio_Controlador.ListarServicioes();
            componenteselectronicos = Dom_Componente_Electronico_Controlador.ListarTipoControlComponenteElectronico();
            tipocontrolescomponenteselectronicos = Dom_Tipo_Control_Componente_Electronico_Controlador.ListarTipoControlComponenteElectronico();
            tiposervicios = Gpr_Tipo_Servicio_Controlador.ListarTipoServicioes();
            contolcomponenteelectronico = Dom_Control_Componente_Electronico_Controlador.ListarControlComponenteElectronico();
        }

        private static void mtdCargarTopics()
        {
            client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
            byte code = client.Connect(Guid.NewGuid().ToString());
            //client.Subscribe(new string[] { "event" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
            //client.Subscribe(new string[] { "temp" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
            //client.Subscribe(new string[] { "hum" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
            foreach (Dom_Componente_ElectronicoDTO componenteelectronico in componenteselectronicos)
            {
                client.Subscribe(new string[] { componenteelectronico.Topic }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
            }
        }

        public static void mtdProcesarInformacion(string Topic, string Mensaje)
        {
            foreach (Gpr_GalponDTO galpon in galpones)
            {
                //foreach (Gpr_Tipo_ServicioDTO tiposervicio in tiposervicios)
                //{
                //    if (tiposervicio.IdGprTipoServicio == 2)
                //    {
                //        foreach (Dom_Componente_ElectronicoDTO componenteelectronico in componenteselectronicos.Where(X => X.IdGprServicio == tiposervicio.IdGprTipoServicio && X.IdGprGalpon == galpon.IdGprGalpon))
                //        {
                //            if (Topic == componenteelectronico.Topic)
                //            {
                //                Console.WriteLine("Servicio: " + tiposervicio.Descripcion);
                //                Console.WriteLine(Mensaje);
                //            }
                //        }
                //    }
                //}
                foreach (Gpr_ServicioDTO servicio in servicios)
                {
                    if (servicio.IdGprTipoServicio == 1)
                    {
                        foreach (Dom_Componente_ElectronicoDTO componenteelectronico in componenteselectronicos.Where(X => X.IdGprServicio == servicio.IdGprServicio && X.IdGprGalpon == galpon.IdGprGalpon))
                        {
                            if (Topic == componenteelectronico.Topic)
                            {
                                Console.WriteLine(galpon.Descripcion + ":");

                                Console.WriteLine("Servicio: " + servicio.Descripcion);
                                Console.WriteLine(Mensaje);
                            }
                        }
                    }
                }
            }
        }

        public static void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            //foreach (Gpr_ServicioDTO servicio in servicios)
            //{
            //    if(galpon.IdGprGalpon == servicio.)
            //}

            //if (e.Topic == "temp")
            //{
            //    //Debug.WriteLine("Received = " + Encoding.UTF8.GetString(e.Message) + " on topic " + e.Topic);

            //    Console.WriteLine(Encoding.UTF8.GetString(e.Message) + "°C");
            //}

            //if (e.Topic == "hum")
            //{
            //    //Debug.WriteLine("Received = " + Encoding.UTF8.GetString(e.Message) + " on topic " + e.Topic);

            //    Console.WriteLine(Encoding.UTF8.GetString(e.Message) + "%");
            //}

            foreach (Gpr_GalponDTO galpon in galpones)
            {
                foreach (Gpr_ServicioDTO servicio in servicios)
                {
                    foreach (Gpr_Tipo_ServicioDTO tiposervicio in tiposervicios)
                    {
                        if (tiposervicio.Descripcion == "Control")
                        {
                            foreach (Dom_Componente_ElectronicoDTO componenteelectronico in componenteselectronicos.Where(X => X.IdGprServicio == servicio.IdGprServicio))
                            {
                                foreach (Dom_Control_Componente_ElectronicoDTO controlcomponente in contolcomponenteelectronico.Where(X => X.IdDomComponenteElectronico == componenteelectronico.IdDomComponenteElectronico))
                                {
                                    if (controlcomponente.IdDomTipoControlComponenteElectronico == 3)
                                    {
                                        DateTime horaInicio = Convert.ToDateTime(controlcomponente.Inicio);
                                        DateTime horaFin = Convert.ToDateTime(controlcomponente.Fin);

                                        if (e.Topic == componenteelectronico.Topic)
                                        {
                                            if (horaInicio < DateTime.Now && horaFin.Hour >= DateTime.Now.Hour && horaFin.Minute > DateTime.Now.Minute)
                                            {
                                                if (Encoding.UTF8.GetString(e.Message) == "0")
                                                {
                                                    client.Publish(componenteelectronico.Topic, Encoding.UTF8.GetBytes("1"));
                                                }
                                            }
                                            else if (horaFin.Hour <= DateTime.Now.Hour && horaFin.Minute <= DateTime.Now.Minute)
                                            {
                                                if (Encoding.UTF8.GetString(e.Message) == "1")
                                                {
                                                    client.Publish(componenteelectronico.Topic, Encoding.UTF8.GetBytes("0"));
                                                }
                                            }
                                        }
                                    }
                                }

                                //if (Topic == componenteelectronico.Topic)
                                //{
                                //    Console.WriteLine(galpon.Descripcion + ":");

                                //    Console.WriteLine("Servicio: " + servicio.Descripcion);
                                //    Console.WriteLine(Mensaje);
                                //}
                            }
                        }
                    }
                }
            }


            mtdProcesarInformacion(e.Topic, Encoding.UTF8.GetString(e.Message));

            Console.WriteLine(DateTime.Now.Second);

            if ((DateTime.Now.Second >= 0 && DateTime.Now.Second < 10) && flipMinuto == 0)
            {
                mtdCargarInfo();
                flipMinuto = 1;
            }
            else if (DateTime.Now.Second >= 10)
            {
                flipMinuto = 0;
            }
        }
    }
}