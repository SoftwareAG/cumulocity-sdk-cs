using Cumulocity.SDK.Client.Rest.Model.util;
using System;
using System.Collections.Generic;

namespace Cumulocity.SDK.Client.Rest.Model
{
	/// <summary>
	/// Represents the common elements of any modeled entity. Can serialize into and
	/// be deserialized from JSON.
	/// </summary>
	/// TODO: CleanUp !
	public abstract class Document<T> : JSONBase where T : ID
	{
		public static DynamicPropertiesFilter acceptAll = new DynamicPropertiesFilter();

		public class DynamicPropertiesFilter : IDynamicPropertiesFilter
		{
			public bool apply(string name)
			{
				return true;
			}
		}

		public static E copyDynamicProperties<E>(object source, E target, DynamicPropertiesFilter filter)
		{
			throw new NotImplementedException();
		}

		public static E copyDynamicProperties<E>(object source, E target)
		{
			throw new NotImplementedException();
		}

		public static E deepCopyDynamicProperties<E>(object source, E target)
		{
			throw new NotImplementedException();
		}

		private T id;

		[Obsolete]
		private string internalId;

		/// <summary>
		/// The document revision in couchDB. revision will no longer be a
		/// strong-typed property - it is not applicable in the Agent Perspective
		/// (See note on
		/// https://startups.jira.com/wiki/display/MTM/Extensibility+model)
		/// </summary>

		//ORIGINAL LINE: @Deprecated @SkipFieldInChangeScanner private String revision;
		[Obsolete]
		private string revision;

		protected internal Document()
		{
		}

		protected internal Document(T id) : this(id, (string)null)
		{
		}

		protected internal Document(T id, IDictionary<string, object> fragments) : this(id, null, null, fragments)
		{
		}

		protected internal Document(T id, string internalId) : this(id, internalId, null)
		{
		}

		protected internal Document(T id, string internalId, string revision) : this(id, internalId, revision, null)
		{
		}

		protected internal Document(T id, string internalId, string revision, IDictionary<string, object> fragments)
		{
			this.id = id;
			this.internalId = internalId;
			this.revision = revision;
			add(fragments);
		}

		//ORIGINAL LINE: @JSONProperty(value = "id", ignoreIfNull = true) @JSONConverter(type = IDTypeConverter.class) public T getId()
		public virtual T Id
		{
			get
			{
				return this.id;
			}
			set
			{
				this.id = value;
			}
		}

		//Not use in current repository implementation, to remove
		[Obsolete]
		public virtual string InternalId
		{
			get
			{
				return internalId;
			}
			set
			{
				this.internalId = value;
			}
		}

		//Not use in current repository implementation, to remove

		//Not use in current repository implementation, to remove
		[Obsolete]
		public virtual string Revision
		{
			get
			{
				return this.revision;
			}
			set
			{
				this.revision = value;
			}
		}

		public static DynamicPropertiesFilter AcceptAll
		{
			get { return acceptAll; }
			set { acceptAll = value; }
		}

		//Not use in current repository implementation, to remove

		/// <summary>
		/// Sets a property referring to the given object. The name of the property
		/// will be the fully qualified class name with dots replaced by underscores.<br>
		/// For example, if the object is of type:<br>
		/// <ul>
		/// com.cumulocity.model.Coordinate
		/// </ul>
		/// then the property name will be:<br>
		/// <ul>
		/// "com_cumulocity_model_Coordinate"
		/// </ul>
		/// </summary>
		/// <param name="object"> </param>
		//ORIGINAL LINE: @JSONProperty(ignore = true) public void set(Object object)
		public virtual void set(object @object)
		{
			//set(@object, @object.GetType().Name);
		}

		/// <summary>
		/// Sets a property referring to the given object, using an arbitrary
		/// property name.
		/// </summary>
		/// <param name="object"> </param>
		/// <param name="propertyName"> </param>
		//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
		//ORIGINAL LINE: @JSONProperty(ignore = true) public void set(Object object, String propertyName)
		public virtual void set(object @object, string propertyName)
		{
			//setProperty(propertyName, @object);
		}

		/// <summary>
		/// Sets a property referring to the given object. The name of the property
		/// will be the fully qualified class name of the given class, with dots
		/// replaced by underscores.<br>
		/// This can be useful if you want to name the property after the base class
		/// rather than the actual class of object.<br>
		/// For example, if clazz is of type:<br>
		/// <ul>
		/// com.cumulocity.model.Coordinate
		/// </ul>
		/// then the property name will be:<br>
		/// <ul>
		/// "com_cumulocity_model_Coordinate"
		/// </ul>
		/// </summary>
		/// <param name="object"> </param>
		/// <param name="clazz"> </param>
		//ORIGINAL LINE: @JSONProperty(ignore = true) public <C> void set(Object object, Class<C> clazz)
		public virtual void set<C>(object @object, Type clazz)
		{
			//setProperty(ExtensibilityConverter.ClassToStringRepresentation(clazz), @object);
		}

		//ORIGINAL LINE: @JSONProperty(ignore = true) public void add(Map<String, Object> fragments)
		public virtual void add(IDictionary<string, object> fragments)
		{
			if (fragments == null)
			{
				return;
			}
			foreach (var fragment in fragments.SetOfKeyValuePairs())
			{
				//setProperty(fragment.Key, fragment.Value);
			}
		}

		/// <summary>
		/// Returns the object whose parameter name is given by clazz, or null if no
		/// such property exists.
		/// </summary>
		/// <seealso cref= #set(Object) </seealso>
		/// <param name="clazz">
		/// @return </param>
		//ORIGINAL LINE: @SuppressWarnings("unchecked") public <C> C get(Class<C> clazz)
		public virtual C get<C>(Type clazz)
		{
			//return (C)getProperty(ExtensibilityConverter.ClassToStringRepresentation(clazz));
			return default(C);
		}

		//ORIGINAL LINE: public <C> C get(final String propertyName, final Class<C> asClass)
		public virtual C get<C>(string propertyName, Type asClass)
		{
			object o = get(propertyName);
			if (o == null)
			{
				return default(C);
			}
			return (C) o;
		}

		/// <summary>
		/// Remove the fragment whose name is given by clazz if exists
		/// </summary>
		/// <param name="clazz">
		/// @return </param>
		public virtual void remove(Type clazz)
		{
			//removeProperty(ExtensibilityConverter.ClassToStringRepresentation(clazz));
		}

		/// <summary>
		/// Returns the object associated with the given property name, or null if no
		/// such property exists.
		/// </summary>
		/// <param name="name">
		/// @return </param>
		public virtual object get(string name)
		{
			//return getProperty(name);
			return null;
		}

		/// <summary>
		/// Returns the object associated with the given property name, or null if no
		/// such property exists.
		/// Such an accessor is required when bean naming conventions are used to
		/// discover available properties, eg in Esper.
		///
		/// Same as a call to <seealso cref="#get(String name)"/>
		/// </summary>
		/// <param name="name">
		/// @return </param>
		public virtual object getFragment(string name)
		{
			return get(name);
		}

		public override int GetHashCode()
		{
			return Id != null ? Id.GetHashCode() : 0;
		}

		public override bool Equals(object o)
		{
			if (this == o)
			{
				return true;
			}
			if (!(o is Document<T>))
			{
				return false;
			}

			Document<T> document = (Document<T>)o;

			if (Id != null ? !Id.Equals(document.Id) : document.Id != null)
			{
				return false;
			}

			return true;
		}

		public override string ToString()
		{
			return String.Empty;
		}

	}

	internal static class HashMapHelperClass
	{
		internal static HashSet<KeyValuePair<TKey, TValue>> SetOfKeyValuePairs<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
		{
			HashSet<KeyValuePair<TKey, TValue>> entries = new HashSet<KeyValuePair<TKey, TValue>>();
			foreach (KeyValuePair<TKey, TValue> keyValuePair in dictionary)
			{
				entries.Add(keyValuePair);
			}
			return entries;
		}

		internal static TValue GetValueOrNull<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
		{
			TValue ret;
			dictionary.TryGetValue(key, out ret);
			return ret;
		}
	}
}