using System;
using System.Collections.Generic;
using System.Text;

namespace Cumulocity.SDK.Client.Rest.Representation.Application
{
	public class ApplicationReferenceRepresentation : BaseResourceRepresentation
	{

		private ApplicationRepresentation application;

		/// <returns> the application </returns>
		public virtual ApplicationRepresentation Application
		{
			get
			{
				return application;
			}
			set
			{
				this.application = value;
			}
		}

	}

}
