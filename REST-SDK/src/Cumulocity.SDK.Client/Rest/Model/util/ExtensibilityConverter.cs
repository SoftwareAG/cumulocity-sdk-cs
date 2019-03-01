using Cumulocity.SDK.Client.Rest.Utils;
using System;
using System.Reflection;

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
			return type.GetCustomAttribute<PackageName>().Name;
		}

		public static string ClassToStringRepresentation<T>(T type)
		{
			return typeof(T).GetCustomAttribute<PackageName>().Name;
		}
	}
}