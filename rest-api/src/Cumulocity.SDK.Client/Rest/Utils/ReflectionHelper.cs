using System;
using System.Collections.Generic;
using System.Reflection;
using Cumulocity.SDK.Client.Rest.Model.Idtype;

namespace Cumulocity.SDK.Client.Rest.Utils
{
    public static class ReflectionHelper
    {
        private static FieldInfo GetFieldInfo(Type type, string fieldName)
        {
            FieldInfo fieldInfo;
            do
            {
                fieldInfo = type.GetField(fieldName,
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.IgnoreCase);
                type = type.BaseType;
            } while (fieldInfo == null && type != null);

            return fieldInfo;
        }

        public static object GetFieldValue(this object obj, string fieldName)
        {
            if (obj == null)
                throw new ArgumentNullException("obj");
            var objType = obj.GetType();
            var fieldInfo = GetFieldInfo(objType, fieldName);
            if (fieldInfo == null)
                throw new ArgumentOutOfRangeException("fieldName",
                    string.Format("Couldn't find field {0} in type {1}", fieldName, objType.FullName));
            return fieldInfo.GetValue(obj);
        }

        public static void SetFieldValue(this object obj, string fieldName, object val)
        {
            if (obj == null)
                throw new ArgumentNullException("obj");
            var objType = obj.GetType();
            var fieldInfo = GetFieldInfo(objType, fieldName);
            if (fieldInfo == null)
                throw new ArgumentOutOfRangeException("fieldName",
                    string.Format("Couldn't find field {0} in type {1}", fieldName, objType.FullName));
            fieldInfo.SetValue(obj, val);
        }


        private static PropertyInfo GetPropertyInfo(Type type, string fieldName)
        {
            PropertyInfo fieldInfo;
            do
            {
                fieldInfo = type.GetProperty(fieldName,
                    BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic |
                    BindingFlags.GetProperty);
                type = type.BaseType;
            } while (fieldInfo == null && type != null);

            return fieldInfo;
        }

        public static object GetPropertyValue(this object obj, string fieldName)
        {
            if (obj == null)
                throw new ArgumentNullException("obj");
            var objType = obj.GetType();
            var propertyInfo = GetPropertyInfo(objType, fieldName);
            if (propertyInfo == null)
                throw new ArgumentOutOfRangeException("fieldName",
                    string.Format("Couldn't find field {0} in type {1}", fieldName, objType.FullName));
            return propertyInfo.GetValue(obj);
        }

        public static void SetPropertyValue(this object obj, string fieldName, object val)
        {
            if (obj == null)
                throw new ArgumentNullException("obj");
            var objType = obj.GetType();
            var propertyInfo = GetPropertyInfo(objType, fieldName);
	        if (propertyInfo == null)
	        {
		        propertyInfo = GetPropertyInfo(objType, "Attrs");
			}
			if (propertyInfo == null)
				throw new ArgumentOutOfRangeException("fieldName",
					string.Format("Couldn't find field {0} in type {1}", fieldName, objType.FullName));

			System.Type propertyType = propertyInfo.PropertyType;
	        // Get the type code so we can switch
	        System.TypeCode typeCode = System.Type.GetTypeCode(propertyType);
	        try
	        {

		        switch (typeCode)
		        {
			        case TypeCode.Int32:
				        propertyInfo.SetValue(obj, Convert.ToInt32(val), null);
				        break;
			        case TypeCode.Int64:
				        propertyInfo.SetValue(obj, Convert.ToInt64(val), null);
				        break;
			        case TypeCode.String:
				        propertyInfo.SetValue(obj, val, null);
				        break;
			        case TypeCode.Object:
				        if (propertyType == typeof(GId) )
				        {
					        var valType = val.GetType();
					        if (val.GetType() == typeof(GId))
					        {
						        propertyInfo.SetValue(obj, (GId) val, null);
						        return;
							}
					        else
					        {
						        propertyInfo.SetValue(obj, new GId(val.ToString()), null);
						        return;
					        }
				        }
				        else
				        {
							propertyInfo.SetValue(obj, val, null);
						}
				        break;
			        default:
				        propertyInfo.SetValue(obj, val, null);
				        break;
		        }

		        return;
	        }
	        catch (Exception ex)
	        {
		        throw new Exception("Failed to set property value for our Foreign Key");
	        }
			propertyInfo.SetValue(obj, val);
        }
        
        public static IEnumerable<Type> GetTypesWithPackageName() {
            Assembly assembly = Assembly.GetExecutingAssembly();
            foreach(Type type in assembly.GetTypes()) {
                if (type.GetCustomAttributes(typeof(PackageName), true).Length > 0) {
                    yield return type;
                }
            }
        }
    }
}