using System;
using System.Collections.Generic;
using System.Text;

namespace Cumulocity.SDK.Client.Rest.Representation.User
{
	public class RoleReferenceRepresentation : BaseResourceRepresentation, IReferenceRepresentation
	{

		//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
		//ORIGINAL LINE: @NotNull(operation = {Command.CREATE, Command.UPDATE}) private RoleRepresentation role;
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
