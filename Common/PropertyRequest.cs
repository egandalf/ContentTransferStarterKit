using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class PropertyRequest
    {
        public static long GetEktronDefinition(Type t)
        {
            var atts = t.GetCustomAttributes(typeof(EktronDefinition), false);
            if (atts != null && atts.Any())
            {
                return ((EktronDefinition)atts[0]).XmlConfigId;
            }
            return -1; // Return invalid finding
        }

        public static string GetEPiServerDefinition(Type t)
        {
            var atts = t.GetCustomAttributes(typeof(EPiServerDefinition), false);
            if (atts != null && atts.Any())
            {
                return ((EPiServerDefinition)atts[0]).ContentTypeName;
            }
            return string.Empty; // Return invalid finding
        }

        public static bool GetEktronIsItemReference(PropertyInfo p)
        {
            return Attribute.IsDefined(p, typeof(EktronItemReference));
        }

        public static Type GetTypeByEktronDefinition(long XmlConfigId)
        {
            var types = from a in AppDomain.CurrentDomain.GetAssemblies()
                        from t in a.GetTypes()
                        where t.IsDefined(typeof(EktronDefinition), false)
                        where t.GetCustomAttribute<EktronDefinition>().XmlConfigId == XmlConfigId
                        select t;

            if (types.Any()) return types.First();
            return null;
        }
    }
}
