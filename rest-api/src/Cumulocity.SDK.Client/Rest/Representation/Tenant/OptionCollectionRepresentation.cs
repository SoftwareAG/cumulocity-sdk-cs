using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Cumulocity.SDK.Client.Rest.Representation.Tenant
{
	[JsonObject]
	public class OptionCollectionRepresentation : BaseCollectionRepresentation<OptionRepresentation>
	{

		private IList<OptionRepresentation> options;

		public virtual IList<OptionRepresentation> Options
		{
			get
			{
				return options;
			}
			set
			{
				this.options = value;
			}
		}

		public override IEnumerator<OptionRepresentation> GetEnumerator()
		{
			return options.GetEnumerator();
		}
	}
}
