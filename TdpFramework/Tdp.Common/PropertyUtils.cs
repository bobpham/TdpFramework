using System;


namespace Tdp.Common
{
    public class PropertyUtils
    {
        public static bool HasProperty(object obj, string propertyName)
        {
            if (obj == null)
                throw new ArgumentNullException("obj");

            if ((propertyName == null) || (propertyName == String.Empty) || (propertyName.Length == 0))
                throw new System.ArgumentException("Property name cannot be empty or null.");
            return obj.GetType().GetProperty(propertyName) != null;
        }
              

        public static Type GetPropertyTypeFromClass<T>(string propertyName)
        {
            Type t = typeof(T);

            var output = t.GetProperty(propertyName)?.PropertyType;

            return output;
        }
    }
}
