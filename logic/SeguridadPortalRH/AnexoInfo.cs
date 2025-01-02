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
    public class AnexoInfo : INotifyPropertyChanged
    {
        private string _fileNameField;
        private string _contentTypeField;
        private byte[] _sourceField;
        private int _fileIdField;
        private string extensionField;

        [XmlElement(Order = 0)]
        public string _fileName
        {
            get => this._fileNameField;
            set
            {
                this._fileNameField = value;
                this.RaisePropertyChanged(nameof(_fileName));
            }
        }

        [XmlElement(Order = 1)]
        public string _contentType
        {
            get => this._contentTypeField;
            set
            {
                this._contentTypeField = value;
                this.RaisePropertyChanged(nameof(_contentType));
            }
        }

        [XmlElement(DataType = "base64Binary", Order = 2)]
        public byte[] _source
        {
            get => this._sourceField;
            set
            {
                this._sourceField = value;
                this.RaisePropertyChanged(nameof(_source));
            }
        }

        [XmlElement(Order = 3)]
        public int _fileId
        {
            get => this._fileIdField;
            set
            {
                this._fileIdField = value;
                this.RaisePropertyChanged(nameof(_fileId));
            }
        }

        [XmlElement(Order = 4)]
        public string Extension
        {
            get => this.extensionField;
            set
            {
                this.extensionField = value;
                this.RaisePropertyChanged(nameof(Extension));
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
