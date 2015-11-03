using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlMapper
{
    public class MapUtility
    {
        public T RunMap<T>(T item, string Xml) where T : GenericContent
        {
            var mapper = new Mapper.XmlMapper();
            mapper.ProcessXml<T>(Xml, ref item);
            return item;
        }
    }
}
