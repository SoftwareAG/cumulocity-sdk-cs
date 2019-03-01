using System;
using System.Collections.Generic;
using System.Reflection;
using Cumulocity.SDK.Client.Rest.Utils;

namespace Cumulocity.SDK.Client.Rest.API.Notification
{
	public static class Helpers
	{
		public static bool IsNullOrDefault<T>(T argument)
		{
			// deal with normal scenarios
			if (argument == null) return true;
			if (object.Equals(argument, default(T))) return true;

			// deal with non-null nullables
			Type methodType = typeof(T);
			if (Nullable.GetUnderlyingType(methodType) != null) return false;

			// deal with boxed value types
			Type argumentType = argument.GetType();
			if (argumentType.IsValueType && argumentType != methodType)
			{
				object obj = Activator.CreateInstance(argument.GetType());
				return obj.Equals(argument);
			}

			return false;
		}

		public static T GetObject<T>(Dictionary<string, object> dict)
		{
			Type type = typeof(T);
			var obj = Activator.CreateInstance(type);

			foreach (var kv in dict)
			{
				//type.GetProperty(kv.Key).SetValue(obj, kv.Value);
				SetProperty(obj, kv.Key, kv.Value);
			}
			return (T)obj;
		}

		private static void SetProperty(Object R, string propertyName, object value)
		{
			//Type type = R.GetType();
			//object result;
			//result = type.InvokeMember(
			//	propertyName,
			//	BindingFlags.SetProperty |
			//	BindingFlags.IgnoreCase |
			//	BindingFlags.Public |
			//	BindingFlags.Instance,
			//	null,
			//	R,
			//	new object[] { value });
			R.SetPropertyValue(propertyName, value);
		}
	}
}