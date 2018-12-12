using System;
using System.Reflection;
using Cumulocity.SDK.Client.Rest.Utils;

namespace Cumulocity.SDK.Client.Rest.Model.util
{
    public class ExtensibilityConverter
    {
        private static readonly Lazy<ExtensibilityConverter> lazy =
            new Lazy<ExtensibilityConverter>(() => new ExtensibilityConverter());

        private ExtensibilityConverter()
        {
        }

        public static ExtensibilityConverter Instance => lazy.Value;

        public static string ClassToStringRepresentation(Type type)
        {
            //return type.FullName.Replace(".", "_");
            return type.GetCustomAttribute<PackageName>().Name;
        }

        public static string ClassToStringRepresentation<T>(T type)
        {
            //return typeof(T).Name.Replace(".", "_");
            return typeof(T).GetCustomAttribute<PackageName>().Name;
        }
    }
}