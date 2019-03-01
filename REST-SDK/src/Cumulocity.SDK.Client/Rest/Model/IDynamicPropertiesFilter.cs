using System;
using System.Collections.Generic;
using System.Text;

namespace Cumulocity.SDK.Client.Rest.Model
{
	public interface IDynamicPropertiesFilter
	{
		bool apply(string name);
	}
}
