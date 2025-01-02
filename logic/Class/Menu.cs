using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Xml.Serialization;

namespace logic.Class
{
    [XmlRoot("menu")]
    [Serializable]
    public class Menu
    {
        public Menu() => this.items = new List<MenuItem>();

        [XmlElement(typeof(MenuItem), ElementName = "menuItem")]
        public List<MenuItem> items { get; set; }
    }
}
