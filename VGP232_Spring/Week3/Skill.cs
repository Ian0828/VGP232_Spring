using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Week3
{
    [Serializable]
    [XmlRoot("Skills")]
    public class Skill
    {
        [XmlAttribute("Name Of Character")]
        public string Name { get; set; }
        [XmlElement("cost")]
        public int Cost { get; set; }
        public int Modifier { get; set; }
        public int Damage { get; set; }
        public override string ToString()
        {
            return string.Format("Name: {0}   Cost: {1}   Modifier: {2}   Damage: {3}", Name, Cost, Modifier, Damage);
        }
    }
}
