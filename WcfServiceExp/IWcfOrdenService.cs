using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfServiceExp
{
    [ServiceContract]
    public interface IWcfOrdenService
    {
        [OperationContract]
        RespuestaOrden RegistrarOrden(OrdenContrato orden);
    }


    [DataContract]
    public class OrdenContrato
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Cliente { get; set; }

        [DataMember]
        public decimal Total { get; set; }

        [DataMember]
        public System.DateTime Fecha { get; set; }

        [DataMember]
        public List<DetalleOrdenContrato> Detalles { get; set; }
    }

    [DataContract]
    public class DetalleOrdenContrato
    {
        [DataMember]
        public string Producto { get; set; }

        [DataMember]
        public int Cantidad { get; set; }

        [DataMember]
        public decimal PrecioUnitario { get; set; }
    }

    [DataContract]
    public class RespuestaOrden
    {
        [DataMember]
        public bool Exito { get; set; }

        [DataMember]
        public string Mensaje { get; set; }

        [DataMember]
        public int OrdenId { get; set; }
    }
}
