using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfServiceExp
{
    public class Service1 : IWcfOrdenService
    {
        public RespuestaOrden RegistrarOrden(OrdenContrato orden)
        {
            try
            {
                Console.WriteLine($"WCF - Procesando orden para cliente: {orden.Cliente}");

                if (orden.Detalles == null || !orden.Detalles.Any())
                {
                    return new RespuestaOrden
                    {
                        Exito = false,
                        Mensaje = "La orden debe tener al menos un detalle"
                    };
                }

                var ordenId = new Random().Next(1000, 9999);

                return new RespuestaOrden
                {
                    Exito = true,
                    Mensaje = "Orden registrada exitosamente",
                    OrdenId = ordenId
                };
            }
            catch (Exception ex)
            {
                return new RespuestaOrden
                {
                    Exito = false,
                    Mensaje = $"Error en servicio WCF: {ex.Message}"
                };
            }
        }
    }
}
