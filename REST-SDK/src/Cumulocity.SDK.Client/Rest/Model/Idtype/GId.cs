using Cumulocity.SDK.Client.Rest.Model.Interfaces;
using Newtonsoft.Json;
using System;

namespace Cumulocity.SDK.Client.Rest.Model.Idtype
{
	public class GId : ID, JSONable
	{
		public GId()
		{
		}

		public GId([JsonProperty(PropertyName = "id")]string id) : base(id)
		{
		}

		public GId(string type, string value, string name) : base(type, value, name)
		{
		}

		public virtual long? Long
		{
			get
			{
				try
				{
					if (Value == null) return null;
					return long.Parse(Value);
				}
				catch (Exception)
				{
					return null;
				}
			}
		}

		/// <summary>
		///     GId type returns just its value when converting to JSON
		/// </summary>
		public string toJSON()
		{
			return JsonConvert.SerializeObject(base.Value).Replace(@"\", "").Replace("\"", "");
		}

		public static GId asGId(object id)
		{
			if (id is GId)
				return (GId)id;
			if (id is ID)
				return new GId(((ID)id).Value);
			return asGId(id.ToString());
		}

		public static GId asGId(string id)
		{
			return ReferenceEquals(id, null) ? null : new GId(id);
		}

		public static GId asGId(long? id)
		{
			return id == null ? null : new GId(id.ToString());
		}

		public static Func<T, GId> AsGId<T>()
		{
			return input => asGId(input);
		}
	}
}