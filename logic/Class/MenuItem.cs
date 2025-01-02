using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace logic.Class
{
    [XmlRoot("menuItem")]
    [Serializable]
    public class MenuItem
    {
        public MenuItem() => this.items = new List<MenuItem>();

        [XmlElement("Titulo")]
        public string Titulo { get; set; }

        [XmlElement("url")]
        public string url { get; set; }

        [XmlElement("ModuloID")]
        public int ModuloID { get; set; }

        [XmlElement("EsMostrarEnMenu")]
        public int EsMostrarEnMenu { get; set; }

        [XmlElement(typeof(MenuItem), ElementName = "menuItem")]
        public List<MenuItem> items { get; set; }
    }
}