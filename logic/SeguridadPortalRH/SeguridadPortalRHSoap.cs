
using System.CodeDom.Compiler;
using System.Data;
using System.ServiceModel;
using System.Threading.Tasks;

namespace logic.SeguridadPortalRH
{
    [GeneratedCode("System.ServiceModel", "4.0.0.0")]
    [ServiceContract(ConfigurationName = "SeguridadPortalRH.SeguridadPortalRHSoap")]
    public interface SeguridadPortalRHSoap
    {
        [OperationContract(Action = "http://tempuri.org/ObtieneMenuServicio", ReplyAction = "*")]
        [XmlSerializerFormat(SupportFaults = true)]
        DataTable ObtieneMenuServicio(int servicioID, int socioID);

        [OperationContract(Action = "http://tempuri.org/ObtieneMenuServicio", ReplyAction = "*")]
        Task<DataTable> ObtieneMenuServicioAsync(int servicioID, int socioID);

        [OperationContract(Action = "http://tempuri.org/ObtieneDivisionesPermitidas", ReplyAction = "*")]
        [XmlSerializerFormat(SupportFaults = true)]
        DataTable ObtieneDivisionesPermitidas(int servicioID, int socioID, int moduloID);

        [OperationContract(Action = "http://tempuri.org/ObtieneDivisionesPermitidas", ReplyAction = "*")]
        Task<DataTable> ObtieneDivisionesPermitidasAsync(
          int servicioID,
          int socioID,
          int moduloID);

        [OperationContract(Action = "http://tempuri.org/ObtienePermisosPantalla", ReplyAction = "*")]
        [XmlSerializerFormat(SupportFaults = true)]
        DataTable ObtienePermisosPantalla(int servicioID, int socioID, int moduloID);

        [OperationContract(Action = "http://tempuri.org/ObtienePermisosPantalla", ReplyAction = "*")]
        Task<DataTable> ObtienePermisosPantallaAsync(
          int servicioID,
          int socioID,
          int moduloID);

        [OperationContract(Action = "http://tempuri.org/ObtieneLogoSuperiorIzquierdo", ReplyAction = "*")]
        [XmlSerializerFormat(SupportFaults = true)]
        [ServiceKnownType(typeof(object[]))]
        ResponseWSInfo ObtieneLogoSuperiorIzquierdo();

        [OperationContract(Action = "http://tempuri.org/ObtieneLogoSuperiorIzquierdo", ReplyAction = "*")]
        Task<ResponseWSInfo> ObtieneLogoSuperiorIzquierdoAsync();

        [OperationContract(Action = "http://tempuri.org/ObtieneLogoSuperiorCentral", ReplyAction = "*")]
        [XmlSerializerFormat(SupportFaults = true)]
        [ServiceKnownType(typeof(object[]))]
        ResponseWSInfo ObtieneLogoSuperiorCentral();

        [OperationContract(Action = "http://tempuri.org/ObtieneLogoSuperiorCentral", ReplyAction = "*")]
        Task<ResponseWSInfo> ObtieneLogoSuperiorCentralAsync();
    }
}
