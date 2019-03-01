using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;

namespace Cumulocity.SDK.Client.Rest.Representation
{
	public abstract class BaseCollectionRepresentation<T> : BaseResourceRepresentation, IEnumerable<T>
	{
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "statistics")]
		public virtual PageStatisticsRepresentation PageStatistics { get; set; }

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual string Prev { get; set; }

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual string Next { get; set; }

		public abstract IEnumerator<T> GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}