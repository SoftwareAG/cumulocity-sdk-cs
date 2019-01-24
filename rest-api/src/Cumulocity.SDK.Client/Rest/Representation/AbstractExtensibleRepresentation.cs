using Cumulocity.SDK.Client.Rest.Model.util;
using Cumulocity.SDK.Client.Rest.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;

namespace Cumulocity.SDK.Client.Rest.Representation
{
	public class AbstractExtensibleRepresentation : BaseResourceRepresentation
	{
		private IDictionary<string, Newtonsoft.Json.Linq.JToken> attrs = new Dictionary<string, Newtonsoft.Json.Linq.JToken>();

		[OnDeserialized]
		private void OnDeserialized(StreamingContext context)
		{
		}

		[JsonExtensionData]
		public IDictionary<string, Newtonsoft.Json.Linq.JToken> Attrs
		{
			get => attrs;
			set => attrs = value;
		}

		public virtual object getProperty(string name)
		{
			foreach (var type in ReflectionHelper.GetTypesWithPackageName())
			{
				System.Attribute[] refAttrs = System.Attribute.GetCustomAttributes(type);
				foreach (System.Attribute attr in refAttrs)
				{
					if (attr is PackageName p)
					{
						if (p.Name.Equals(name))
						{
							var item = attrs[name];
							var t = item.GetType();
							var m = t.GetMethod("ToObject", true, 0);
							return m.MakeGenericMethod(type).Invoke(attrs[name], null);
						}
					}
				}
			}

			throw new ArgumentException(nameof(name));
		}

		public virtual void setProperty(string name, object value)
		{
			attrs[name] = JToken.FromObject(value);
		}

		public virtual object removeProperty(string name)
		{
			return attrs.Remove(name);
		}

		public virtual bool hasProperty(string name)
		{
			return attrs.ContainsKey(name);
		}

		public virtual ICollection<string> propertyNames()
		{
			return attrs.Keys;
		}

		/// <summary>
		///     Sets a property referring to the given object.
		///     The name of the property will be the fully qualified class name
		///     with dots replaced by underscores.
		///     <br>
		///         For example, if the object is of type:
		///         <br>
		///             <ul>com.cumulocity.model.Coordinate</ul>
		///             then the property name will be:
		///             <br>
		///                 <ul>"com_cumulocity_model_Coordinate"</ul>
		/// </summary>
		/// <param name="object"> </param>
		public virtual void set(object @object)
		{
			set(@object, @object.GetType().GetCustomAttribute<PackageName>().Name);
		}

		/// <summary>
		///     Sets a property referring to the given object,
		///     using an arbitrary property name.
		/// </summary>
		/// <param name="object"> </param>
		/// <param name="propertyName"> </param>
		public virtual void set(object @object, string propertyName)
		{
			setProperty(propertyName, @object);
		}

		/// <summary>
		///     Sets a property referring to the given object.
		///     The name of the property will be the fully qualified class name
		///     of the given class, with dots replaced by underscores.
		///     <br>
		///         This can be useful if you want to name the property after the base class
		///         rather than the actual class of object.
		///         <br>
		///             For example, if clazz is of type:
		///             <br>
		///                 <ul>com.cumulocity.model.Coordinate</ul>
		///                 then the property name will be:
		///                 <br>
		///                     <ul> "com_cumulocity_model_Coordinate"</ul>
		/// </summary>
		/// <param name="object"> </param>
		/// <param name="clazz"> </param>
		public virtual void set<T>(object @object, Type clazz)
		{
			setProperty(ExtensibilityConverter.ClassToStringRepresentation(clazz), @object);
		}

		/// <summary>
		///     Returns the object whose parameter name is given by clazz, or null
		///     if no such property exists.
		/// </summary>
		/// <seealso cref= # set( Object
		/// )
		/// </seealso>
		/// <param name="clazz">
		///     @return
		/// </param>
		public virtual T get<T>(T clazz)
		{
			var claz = typeof(T);
			return (T)getProperty(ExtensibilityConverter.ClassToStringRepresentation(claz));
		}

		public virtual T get<T>()
		{
			var clazz = typeof(T);
			return (T)getProperty(ExtensibilityConverter.ClassToStringRepresentation(clazz));
		}

		/// <summary>
		///     Returns the object associated with the given property name,
		///     or null if no such property exists.
		/// </summary>
		/// <param name="name">
		///     @return
		/// </param>
		public virtual object get(string name)
		{
			return getProperty(name);
		}
	}

	[System.AttributeUsage(System.AttributeTargets.Property, AllowMultiple = false)]
	public class GetOnlyJsonPropertyAttribute : Attribute
	{
	}

	public class GetOnlyContractResolver : DefaultContractResolver
	{
		protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
		{
			var property = base.CreateProperty(member, memberSerialization);
			if (property != null && property.Writable)
			{
				var attributes = property.AttributeProvider.GetAttributes(typeof(GetOnlyJsonPropertyAttribute), true);
				if (attributes != null && attributes.Count > 0)
					property.Writable = false;
			}
			return property;
		}
	}
}