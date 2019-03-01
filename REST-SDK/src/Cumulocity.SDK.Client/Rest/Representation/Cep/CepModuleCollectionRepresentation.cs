using Newtonsoft.Json;
using System.Collections.Generic;

namespace Cumulocity.SDK.Client.Rest.Representation.Cep
{
	[JsonObject]
	public class CepModuleCollectionRepresentation : BaseCollectionRepresentation<CepModuleRepresentation>
	{
		private IList<CepModuleRepresentation> modules;

		public virtual IList<CepModuleRepresentation> Modules
		{
			get
			{
				return modules;
			}
			set
			{
				this.modules = value;
			}
		}

		public override IEnumerator<CepModuleRepresentation> GetEnumerator()
		{
			return modules.GetEnumerator();
		}
	}
}