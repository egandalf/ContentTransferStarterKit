using Common;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace XmlMapper.Mapper
{
    internal class oPropertyManager
    {
        public PropertyInfo[] GetPropertiesList(Type t)
        {
            var properties = t.GetProperties().Where(p => Attribute.IsDefined(p, typeof(EktronXml))).ToArray();
            if (properties.Any()) return properties;
            return null;
        }

        public void SetPropertyValue<T>(PropertyInfo p, object value, ref T item)
        {
            var convertedVal = value;
            if (p.PropertyType.IsValueType)
            {
                convertedVal = Convert.ChangeType(value, p.PropertyType);
            }
            else if(p.PropertyType == typeof(DateTime))
            {
                convertedVal = DateTime.Parse(value.ToString());
            }
            p.SetValue(item, convertedVal);
        }

        public string GetAttributeXPath(PropertyInfo p)
        {
            return p.GetCustomAttribute<EktronXml>().Xpath;
        }

        public bool GetAttributeRichText(PropertyInfo p)
        {
            return Attribute.IsDefined(p, typeof(EktronRich));// p.GetCustomAttribute<EktronRich>() != null;
        }
    }
}
