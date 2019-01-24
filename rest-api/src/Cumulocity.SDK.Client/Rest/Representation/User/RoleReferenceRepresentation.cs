using System;
using System.Collections.Generic;
using System.Text;

namespace Cumulocity.SDK.Client.Rest.Representation.User
{
	public class RoleReferenceRepresentation : BaseResourceRepresentation, IReferenceRepresentation
	{

		private RoleRepresentation role;

		public virtual RoleRepresentation Role
		{
			set
			{
				this.role = value;
			}
			get
			{
				return role;
			}
		}

	}
}
