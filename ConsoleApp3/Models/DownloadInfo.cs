using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ConsoleApp3
{ 
    //XML модель для удобства)
    [XmlRoot(ElementName = "download-info")]
    public class Downloadinfo
    {

        [XmlElement(ElementName = "host")]
        public string Host { get; set; }

        [XmlElement(ElementName = "path")]
        public string Path { get; set; }

        [XmlElement(ElementName = "ts")]
        public string Ts { get; set; }

        [XmlElement(ElementName = "region")]
        public int Region { get; set; }

        [XmlElement(ElementName = "s")]
        public string S { get; set; }
    }
}
