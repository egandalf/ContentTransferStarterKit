using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Text;
using System.Threading.Tasks;
using Models;
using System.Reflection;

namespace XmlMapper.Mapper
{
    internal class XmlMapper
    {
        private oPropertyManager propMan = new oPropertyManager();

        public void ProcessXml<T>(string xml, ref T item) where T : GenericContent
        {
            var sourceDoc = XDocument.Parse(xml);
            var rootEl = sourceDoc.Root;
            var propertiesList = propMan.GetPropertiesList(typeof(T));
            object value;
            foreach (PropertyInfo p in propertiesList)
            {
                value = MapXmlToProperty(p, rootEl);
                propMan.SetPropertyValue<T>(p, value, ref item);
            }
        }

        private object MapXmlToProperty(PropertyInfo p, XElement node)
        {
            string xpath = propMan.GetAttributeXPath(p);
            bool isRich = propMan.GetAttributeRichText(p);
            object o = MapXmlToType(p.PropertyType, xpath, node, isRich);
            return o;
        }

        private object MapElementToComplexType(XElement node, Type t)
        {
            var propertiesList = propMan.GetPropertiesList(t) ?? new PropertyInfo[0];
            object tmp = Activator.CreateInstance(t);            
            object o;
            foreach (PropertyInfo p in propertiesList)
            {
                o = MapXmlToProperty(p, node);
                p.SetValue(tmp, o);
            }
            return tmp;
        }

        private object MapXmlToType(Type t, string xpath, XElement node, bool IsRichText = false)
        {
            object o = null;
            if (t.IsValueType || t == typeof(string))
            {
                if (IsRichText)
                {
                    var reader = node.XPathSelectElement(xpath).CreateReader();
                    reader.MoveToContent();
                    o = reader.ReadInnerXml();
                }
                else
                {
                    o = node.XPathSelectElement(xpath).Value;
                }
            }
            else if (typeof(IList).IsAssignableFrom(t))
            {
                var els = node.XPathSelectElements(xpath);
                var lType = typeof(List<>);
                Type arrayType = t.GetGenericArguments()[0];
                var constructedType = lType.MakeGenericType(arrayType);
                var tmp = (IList)Activator.CreateInstance(constructedType);
                
                foreach (var el in els)
                {
                    if (arrayType.IsValueType || arrayType == typeof(string))
                    {
                        tmp.Add(Convert.ChangeType(el.Value, arrayType));
                    }
                    else // complex type
                    {
                        tmp.Add(MapElementToComplexType(el, arrayType));
                    }
                }
                o = tmp;
            }
            else // complex type
            {
                var el = node.XPathSelectElement(xpath);
                o = MapElementToComplexType(el, t);
            }

            return o;
        }
    }
}
