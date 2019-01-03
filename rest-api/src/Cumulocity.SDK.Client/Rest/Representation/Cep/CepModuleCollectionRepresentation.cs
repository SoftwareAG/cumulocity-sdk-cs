using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Cumulocity.SDK.Client.Rest.Representation.Cep
{
	[JsonObject]
	public class CepModuleCollectionRepresentation : BaseCollectionRepresentation<CepModuleRepresentation>
	{
		private IList<CepModuleRepresentation> modules;

		//ORIGINAL LINE: @JSONTypeHint(CepModuleRepresentation.class) @JSONProperty(ignoreIfNull = true) public List<CepModuleRepresentation> getModules()
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

		//ORIGINAL LINE: @Override @JSONProperty(ignore = true) public Iterator<CepModuleRepresentation> iterator()
		public  IEnumerator<CepModuleRepresentation> iterator()
		{
			return modules.GetEnumerator();
		}
		public override IEnumerator<CepModuleRepresentation> GetEnumerator()
		{
			return modules.GetEnumerator();
		}
	}
}
