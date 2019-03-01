using System.Collections.Generic;

namespace Cumulocity.SDK.Client.Rest.Representation
{
	public class BaseMediaType
	{
		/// <summary>
		///     The value of a type or subtype wildcard: "*"
		/// </summary>
		public const string MEDIA_TYPE_WILDCARD = "*";

		public BaseMediaType(string type, string subtype, IDictionary<string, string> parameters)
		{
			Type = ReferenceEquals(type, null) ? MEDIA_TYPE_WILDCARD : type;
			Subtype = ReferenceEquals(subtype, null) ? MEDIA_TYPE_WILDCARD : subtype;
			if (parameters == null)
				Parameters = new Dictionary<string, string>();
			else
				Parameters = parameters;
		}

		public BaseMediaType(string type, string subtype) : this(type, subtype, new Dictionary<string, string>())
		{
		}

		/// <summary>
		///     Creates a new instance of MediaType, both type and subtype are wildcards.
		///     Consider using the constant <seealso cref="#WILDCARD_TYPE" /> instead.
		/// </summary>
		public BaseMediaType() : this(MEDIA_TYPE_WILDCARD, MEDIA_TYPE_WILDCARD)
		{
		}

		public virtual string Type { get; }

		public virtual string Subtype { get; }

		public virtual IDictionary<string, string> Parameters { get; }
	}

	internal static class HashMapHelperClass
	{
		internal static HashSet<KeyValuePair<TKey, TValue>> SetOfKeyValuePairs<TKey, TValue>(
			this IDictionary<TKey, TValue> dictionary)
		{
			var entries = new HashSet<KeyValuePair<TKey, TValue>>();
			foreach (var keyValuePair in dictionary) entries.Add(keyValuePair);
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