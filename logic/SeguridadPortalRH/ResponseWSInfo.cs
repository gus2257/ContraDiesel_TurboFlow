// Decompiled with JetBrains decompiler
// Type: logic.SeguridadPortalRH.ResponseWSInfo
// Assembly: logic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85DFC992-2B9B-4392-94C7-6A4DF6BD2C2F
// Assembly location: C:\Users\admin\Desktop\Think Solutions\Clientes\Rapid\compiled 20190511\bin\logic.dll

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace logic.SeguridadPortalRH
{
    [GeneratedCode("System.Xml", "4.0.30319.34234")]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(Namespace = "http://tempuri.org/")]
    [Serializable]
    public class ResponseWSInfo : INotifyPropertyChanged
    {
        private int errorCodeField;
        private string errorMessageField;
        private string returnInfoField;
        private AnexoInfo anexoField;
        private object[] returnArrayInfoField;

        [XmlElement(Order = 0)]
        public int errorCode
        {
            get => this.errorCodeField;
            set
            {
                this.errorCodeField = value;
                this.RaisePropertyChanged(nameof(errorCode));
            }
        }

        [XmlElement(Order = 1)]
        public string errorMessage
        {
            get => this.errorMessageField;
            set
            {
                this.errorMessageField = value;
                this.RaisePropertyChanged(nameof(errorMessage));
            }
        }

        [XmlElement(Order = 2)]
        public string returnInfo
        {
            get => this.returnInfoField;
            set
            {
                this.returnInfoField = value;
                this.RaisePropertyChanged(nameof(returnInfo));
            }
        }

        [XmlElement(Order = 3)]
        public AnexoInfo Anexo
        {
            get => this.anexoField;
            set
            {
                this.anexoField = value;
                this.RaisePropertyChanged(nameof(Anexo));
            }
        }

        [XmlArray(Order = 4)]
        public object[] returnArrayInfo
        {
            get => this.returnArrayInfoField;
            set
            {
                this.returnArrayInfoField = value;
                this.RaisePropertyChanged(nameof(returnArrayInfo));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if (propertyChanged == null)
                return;
            propertyChanged((object)this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
