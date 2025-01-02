// Decompiled with JetBrains decompiler
// Type: logic.SeguridadPortalRH.SeguridadPortalRHSoapClient
// Assembly: logic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85DFC992-2B9B-4392-94C7-6A4DF6BD2C2F
// Assembly location: C:\Users\admin\Desktop\Think Solutions\Clientes\Rapid\compiled 20190511\bin\logic.dll

using System.CodeDom.Compiler;
using System.Data;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;

namespace logic.SeguridadPortalRH
{
    [DebuggerStepThrough]
    [GeneratedCode("System.ServiceModel", "4.0.0.0")]
    public class SeguridadPortalRHSoapClient : ClientBase<SeguridadPortalRHSoap>, SeguridadPortalRHSoap
    {
        public SeguridadPortalRHSoapClient()
        {
        }

        public SeguridadPortalRHSoapClient(string endpointConfigurationName)
          : base(endpointConfigurationName)
        {
        }

        public SeguridadPortalRHSoapClient(string endpointConfigurationName, string remoteAddress)
          : base(endpointConfigurationName, remoteAddress)
        {
        }

        public SeguridadPortalRHSoapClient(
          string endpointConfigurationName,
          EndpointAddress remoteAddress)
          : base(endpointConfigurationName, remoteAddress)
        {
        }

        public SeguridadPortalRHSoapClient(Binding binding, EndpointAddress remoteAddress)
          : base(binding, remoteAddress)
        {
        }

        public DataTable ObtieneMenuServicio(int servicioID, int socioID) => this.Channel.ObtieneMenuServicio(servicioID, socioID);

        public Task<DataTable> ObtieneMenuServicioAsync(int servicioID, int socioID) => this.Channel.ObtieneMenuServicioAsync(servicioID, socioID);

        public DataTable ObtieneDivisionesPermitidas(
          int servicioID,
          int socioID,
          int moduloID)
        {
            return this.Channel.ObtieneDivisionesPermitidas(servicioID, socioID, moduloID);
        }

        public Task<DataTable> ObtieneDivisionesPermitidasAsync(
          int servicioID,
          int socioID,
          int moduloID)
        {
            return this.Channel.ObtieneDivisionesPermitidasAsync(servicioID, socioID, moduloID);
        }

        public DataTable ObtienePermisosPantalla(int servicioID, int socioID, int moduloID) => this.Channel.ObtienePermisosPantalla(servicioID, socioID, moduloID);

        public Task<DataTable> ObtienePermisosPantallaAsync(
          int servicioID,
          int socioID,
          int moduloID)
        {
            return this.Channel.ObtienePermisosPantallaAsync(servicioID, socioID, moduloID);
        }

        public ResponseWSInfo ObtieneLogoSuperiorIzquierdo() => this.Channel.ObtieneLogoSuperiorIzquierdo();

        public Task<ResponseWSInfo> ObtieneLogoSuperiorIzquierdoAsync() => this.Channel.ObtieneLogoSuperiorIzquierdoAsync();

        public ResponseWSInfo ObtieneLogoSuperiorCentral() => this.Channel.ObtieneLogoSuperiorCentral();

        public Task<ResponseWSInfo> ObtieneLogoSuperiorCentralAsync() => this.Channel.ObtieneLogoSuperiorCentralAsync();
    }
}
