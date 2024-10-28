using DevExpress.Xpo.Metadata;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NqlDotNet.DevExpress
{
 
    public class XpoUtilities
    {
        public static EntityPropertiesWrapper GetEntityProperties(Assembly assembly)
        {
            List<EntityProperties> entityPropertiesList = new List<EntityProperties>();

            foreach (Type type in assembly.GetTypes())
            {
                if (typeof(XPObject).IsAssignableFrom(type))
                {
                    XPClassInfo classInfo = XpoDefault.Session.GetClassInfo(type);
                    EntityProperties entityProperties = new EntityProperties
                    {
                        EntityName = type.Name,
                        Properties = new List<Property>()
                    };

                    foreach (XPMemberInfo memberInfo in classInfo.PersistentProperties)
                    {
                        Property entityProperty = new Property
                        {
                            PropertyName = memberInfo.Name,
                            Type = memberInfo.MemberType.Name,
                            Key = memberInfo.IsKey ? "primary" : (memberInfo.ReferenceType != null ? "foreign" : null),
                            Description = memberInfo.IsKey ? "Unique identifier for each " + type.Name : null,
                            References = memberInfo.ReferenceType != null ? memberInfo.ReferenceType.FullName + "." + memberInfo.ReferenceType.KeyProperty.Name : null
                        };

                        entityProperties.Properties.Add(entityProperty);
                    }

                    entityPropertiesList.Add(entityProperties);
                }
            }
            var Wrapper = new EntityPropertiesWrapper();
            Wrapper.EntityProperties = entityPropertiesList;
            return Wrapper;
        }

    }
}

