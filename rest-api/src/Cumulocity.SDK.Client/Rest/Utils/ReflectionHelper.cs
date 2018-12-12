using System;
using System.Collections.Generic;
using System.Reflection;

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
                throw new ArgumentOutOfRangeException("fieldName",
                    string.Format("Couldn't find field {0} in type {1}", fieldName, objType.FullName));
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