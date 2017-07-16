using SIGEPROAVI_Domotica.DTO;
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

        public static List<Gpr_Medicion_DiariaDTO> medicionDiaria = new List<Gpr_Medicion_DiariaDTO>();
        public static List<Gpr_Medicion_HorariaDTO> medicionHorariaTemporal = new List<Gpr_Medicion_HorariaDTO>();
        public static List<Gpr_Medicion_HorariaDTO> medicionHoraria = new List<Gpr_Medicion_HorariaDTO>();

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

                                Gpr_Medicion_HorariaDTO medicionTemporal = new Gpr_Medicion_HorariaDTO();
                                try
                                {
                                    medicionTemporal.IdGprGalpon = galpon.IdGprGalpon;
                                    medicionTemporal.IdGprServicio = servicio.IdGprServicio;
                                    medicionTemporal.Medicion = Convert.ToDecimal(Mensaje);
                                }
                                catch
                                {
                                }

                                if (medicionTemporal.Medicion != 0)
                                {
                                    medicionHorariaTemporal.Add(medicionTemporal);
                                }
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
                                            else if (horaFin.Hour <= DateTime.Now.Hour && horaFin.Minute <= DateTime.Now.Minute || horaInicio > DateTime.Now)
                                            {
                                                if (Encoding.UTF8.GetString(e.Message) == "1")
                                                {
                                                    client.Publish(componenteelectronico.Topic, Encoding.UTF8.GetBytes("0"));
                                                }
                                            }
                                        }
                                    }
                                    //Pendiente
                                    else if (controlcomponente.IdDomTipoControlComponenteElectronico == 2)
                                    {
                                        decimal Promedio = 0;
                                        int contador = 0;
                                        foreach (Gpr_Medicion_HorariaDTO medicion in medicionHorariaTemporal)
                                        {
                                            if (medicion.IdGprGalpon == galpon.IdGprGalpon && medicion.IdGprServicio == 1)
                                            {
                                                Promedio = Promedio + medicion.Medicion;
                                                contador++;
                                            }
                                        }

                                        if (Promedio > 0)
                                        {
                                            Promedio = Promedio / contador;

                                            if (e.Topic == componenteelectronico.Topic)
                                            {
                                                if (Convert.ToInt32(controlcomponente.Inicio) <= Promedio)
                                                {
                                                    if (Encoding.UTF8.GetString(e.Message) == "0")
                                                    {
                                                        client.Publish(componenteelectronico.Topic, Encoding.UTF8.GetBytes("1"));
                                                    }
                                                }
                                                else if (Promedio <= Convert.ToInt32(controlcomponente.Fin))
                                                {
                                                    if (Encoding.UTF8.GetString(e.Message) == "1")
                                                    {
                                                        client.Publish(componenteelectronico.Topic, Encoding.UTF8.GetBytes("0"));
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else if (controlcomponente.IdDomTipoControlComponenteElectronico == 1)
                                    {
                                        decimal Promedio = 0;
                                        int contador = 0;
                                        foreach (Gpr_Medicion_HorariaDTO medicion in medicionHorariaTemporal)
                                        {
                                            if (medicion.IdGprGalpon == galpon.IdGprGalpon && medicion.IdGprServicio == 2)
                                            {
                                                Promedio = Promedio + medicion.Medicion;
                                                contador++;
                                            }
                                        }

                                        if (Promedio > 0)
                                        {
                                            Promedio = Promedio / contador;

                                            if (e.Topic == componenteelectronico.Topic)
                                            {
                                                if (Convert.ToInt32(controlcomponente.Inicio) <= Promedio)
                                                {
                                                    if (Encoding.UTF8.GetString(e.Message) == "0")
                                                    {
                                                        client.Publish(componenteelectronico.Topic, Encoding.UTF8.GetBytes("1"));
                                                    }
                                                }
                                                else if (Promedio <= Convert.ToInt32(controlcomponente.Fin))
                                                {
                                                    if (Encoding.UTF8.GetString(e.Message) == "1")
                                                    {
                                                        client.Publish(componenteelectronico.Topic, Encoding.UTF8.GetBytes("0"));
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            mtdProcesarInformacion(e.Topic, Encoding.UTF8.GetString(e.Message));

            //Console.WriteLine(DateTime.Now.Second);

            DateTime hoy = DateTime.Now;
            DateTime ayer = DateTime.Now.AddDays(-1);
            int minuto = hoy.Minute;
            int hora = int.Parse(hoy.ToString("HH"));

            Console.WriteLine(hora.ToString() + ":" + minuto.ToString());
            Console.WriteLine(hoy);

            //flip asegura que solo se ejecute el metodo 1 vez en la hora que se indique
            if (minuto == 0 && hora != 0 && flip == 0)
            {
                foreach (Gpr_GalponDTO galpon in galpones)
                {
                    foreach (Gpr_ServicioDTO servicio in servicios.Where(X => X.Descripcion != "Control"))
                    {
                        decimal PromedioHorario = 0;
                        int contador = 0;
                        foreach (Gpr_Medicion_HorariaDTO medicion in medicionHorariaTemporal)
                        {
                            if (medicion.IdGprGalpon == galpon.IdGprGalpon && servicio.IdGprServicio == medicion.IdGprServicio)
                            {
                                PromedioHorario = PromedioHorario + medicion.Medicion;
                                contador++;
                            }
                        }

                        if (PromedioHorario > 0)
                        {
                            PromedioHorario = PromedioHorario / contador;

                            //ejecutar el metodo para guardar lo almacenado por hora

                            Gpr_Medicion_HorariaDTO medicionH = new Gpr_Medicion_HorariaDTO();
                            medicionH.Fecha = hoy.ToString("dd-MM-yyyy");
                            medicionH.Hora = hora;
                            medicionH.IdGprGalpon = galpon.IdGprGalpon;
                            medicionH.IdGprServicio = servicio.IdGprServicio;
                            medicionH.Medicion = PromedioHorario;

                            medicionHoraria.Add(medicionH);

                            Gpr_Medicion_Horaria_Controlador.GuardarMedicionHoraria(medicionH);
                        }
                    }
                }

                // se cancela el ciclo
                flip = 1;
                medicionHorariaTemporal.Clear();
                Console.WriteLine("IF 1");
            }
            else if (minuto == 0 && hora == 0 && flip == 0)
            {
                //ejecutar el metodo para guardar lo almacenado por hora a las 12
                foreach (Gpr_GalponDTO galpon in galpones)
                {
                    foreach (Gpr_ServicioDTO servicio in servicios.Where(X => X.Descripcion != "Control"))
                    {
                        decimal PromedioHorario = 0;
                        int contador = 0;
                        foreach (Gpr_Medicion_HorariaDTO medicion in medicionHorariaTemporal)
                        {
                            if (medicion.IdGprGalpon == galpon.IdGprGalpon && servicio.IdGprServicio == medicion.IdGprServicio)
                            {
                                PromedioHorario = PromedioHorario + medicion.Medicion;
                                contador++;
                            }
                        }

                        if (PromedioHorario > 0)
                        {
                            PromedioHorario = PromedioHorario / contador;

                            //ejecutar el metodo para guardar lo almacenado por hora

                            Gpr_Medicion_HorariaDTO medicionH = new Gpr_Medicion_HorariaDTO();
                            medicionH.Fecha = ayer.ToString("dd-MM-yyyy"); ;
                            medicionH.Hora = 24;
                            medicionH.IdGprGalpon = galpon.IdGprGalpon;
                            medicionH.IdGprServicio = servicio.IdGprServicio;
                            medicionH.Medicion = PromedioHorario;

                            medicionHoraria.Add(medicionH);

                            Gpr_Medicion_Horaria_Controlador.GuardarMedicionHoraria(medicionH);
                        }
                    }
                }

                //foreach (DataRow drComboGalpon in dtComboGalpon.Rows)
                //{
                //    foreach (DataRow drServicio in dtServicio.Rows)
                //    {
                //        float PromedioDiario = 0;

                //        int contador = 0;

                //        foreach (cls_gprmedicionhorariaBN medicionPlaceholder in Medicion)
                //        {
                //            if (!float.IsNaN(medicionPlaceholder.Medicion) && medicionPlaceholder.IdGprGalpon == Convert.ToInt32(drComboGalpon["IdGprGalpon"].ToString()) && medicionPlaceholder.IdGprServicio == Convert.ToInt32(drServicio["IdGprServicio"].ToString()))
                //            {
                //                PromedioDiario = PromedioDiario + medicionPlaceholder.Medicion;
                //                contador++;
                //            }
                //        }

                //        PromedioDiario = PromedioDiario / contador;

                //        //ejecutar el metodo para guardar lo almacenado por hora
                //        var _with1 = cls_gprmedicionhorariaBUS;
                //        _with1.IdGprMedicionHoraria = 0;
                //        _with1.IdGprGalpon = Convert.ToInt32(drComboGalpon["IdGprGalpon"].ToString()); ;
                //        _with1.IdGprServicio = Convert.ToInt32(drServicio["IdGprServicio"].ToString()); ;
                //        _with1.Hora = 24;
                //        _with1.Fecha = ayer;
                //        _with1.Medicion = PromedioDiario;

                //        int resultado = cls_gprmedicionhorariaBUS.fnc_I_MedicionHorariaBUS();
                //    }
                //}

                foreach (Gpr_GalponDTO galpon in galpones)
                {
                    foreach (Gpr_ServicioDTO servicio in servicios.Where(X => X.Descripcion != "Control"))
                    {
                        decimal PromedioDiario = 0;
                        int contador = 0;
                        foreach (Gpr_Medicion_HorariaDTO medicion in medicionHoraria)
                        {
                            if (medicion.IdGprGalpon == galpon.IdGprGalpon && servicio.IdGprServicio == medicion.IdGprServicio)
                            {
                                PromedioDiario = PromedioDiario + medicion.Medicion;
                                contador++;
                            }
                        }

                        if (PromedioDiario > 0)
                        {
                            PromedioDiario = PromedioDiario / contador;

                            //ejecutar el metodo para guardar lo almacenado por dia

                            Gpr_Medicion_DiariaDTO medicionD = new Gpr_Medicion_DiariaDTO();
                            medicionD.Fecha = ayer.ToString("dd-MM-yyyy"); ;
                            medicionD.IdGprGalpon = galpon.IdGprGalpon;
                            medicionD.IdGprServicio = servicio.IdGprServicio;
                            medicionD.Medicion = PromedioDiario;

                            medicionDiaria.Add(medicionD);

                            Gpr_Medicion_Diaria_Controlador.GuardarMedicionDiaria(medicionD);
                        }
                    }
                }

                //DataTable dtMedicionDiaria = new DataTable();
                //dtMedicionDiaria = cls_gprmedicionhorariaBUS.fncusp_S_ListarMedicionHorariaXFechaBUS(ayer.ToString("dd-MM-yyyy"));
                //foreach (DataRow drComboGalpon in dtComboGalpon.Rows)
                //{
                //    foreach (DataRow drServicio in dtServicio.Rows)
                //    {
                //        float PromedioDiario = 0;

                //        int contador = 0;

                //        foreach (DataRow drMedicionDiaria in dtMedicionDiaria.Rows)
                //        {
                //            if (Convert.ToInt32(drMedicionDiaria["IdGprGalpon"].ToString()) == Convert.ToInt32(drComboGalpon["IdGprGalpon"].ToString()) && Convert.ToInt32(drMedicionDiaria["IdGprServicio"].ToString()) == Convert.ToInt32(drServicio["IdGprServicio"].ToString()))
                //            {
                //                PromedioDiario = PromedioDiario + (float)Convert.ToDouble(drMedicionDiaria["Medicion"].ToString());
                //                contador++;
                //            }
                //        }

                //        PromedioDiario = PromedioDiario / contador;

                //        //ejecutar el metodo para guardar lo almacenado por hora
                //        var _with1 = cls_gprmediciondiariaBUS;
                //        _with1.IdGprMedicionDiaria = 0;
                //        _with1.IdGprGalpon = Convert.ToInt32(drComboGalpon["IdGprGalpon"].ToString()); ;
                //        _with1.IdGprServicio = Convert.ToInt32(drServicio["IdGprServicio"].ToString()); ;
                //        _with1.Fecha = ayer;
                //        _with1.Medicion = PromedioDiario;

                //        int resultado = cls_gprmediciondiariaBUS.fnc_I_MedicionDiariaBUS();
                //    }
                //}

                // se cancela el ciclo
                flip = 1;
                medicionHoraria.Clear();
                Console.WriteLine("IF 2");
            }
            else if (minuto == 1)
            {
                flip = 0;
                Console.WriteLine("IF 3");
            }
            else
            {
                Console.WriteLine("EN ESPERA");
            }

            //Refresco de información

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