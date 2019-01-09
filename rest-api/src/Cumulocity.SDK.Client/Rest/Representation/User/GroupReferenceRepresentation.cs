using System;
using System.Collections.Generic;
using System.Text;

namespace Cumulocity.SDK.Client.Rest.Representation.User
{
	public class GroupReferenceRepresentation : BaseResourceRepresentation, IReferenceRepresentation
	{

		private GroupRepresentation group;

		public virtual GroupRepresentation Group
		{
			get
			{
				return group;
			}
			set
			{
				this.group = value;
			}
		}


	}
}
