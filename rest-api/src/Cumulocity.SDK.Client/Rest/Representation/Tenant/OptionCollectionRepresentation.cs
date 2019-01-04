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

		//ORIGINAL LINE: @Override @JSONProperty(ignore = true) public Iterator<OptionRepresentation> iterator()
		public  IEnumerator<OptionRepresentation> iterator()
		{
			return options.GetEnumerator();
		}

		public override IEnumerator<OptionRepresentation> GetEnumerator()
		{
			return options.GetEnumerator();
		}
	}
}
