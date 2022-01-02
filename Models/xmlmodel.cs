using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
namespace IntergrationA.Models
{
    [XmlRoot(ElementName = "string", Namespace = "http://tempuri.org/")]
    public class xmlmodel
    {
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

}


