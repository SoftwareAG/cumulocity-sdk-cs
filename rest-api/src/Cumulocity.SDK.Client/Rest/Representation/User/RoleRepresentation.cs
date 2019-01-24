using System;

namespace Cumulocity.SDK.Client.Rest.Representation.User
{
	public class RoleRepresentation : BaseResourceRepresentation
	{
		//note that id is actually same as name, name is not used in model
		private string id;

		[Obsolete]
		private string name;

		// should not be used, there is only id in Authority which is also the name

		public virtual string Id
		{
			set
			{
				this.id = value;
			}
			get
			{
				return id;
			}
		}

		public virtual string Name
		{
			set
			{
				this.name = value;
			}
			get
			{
				return name;
			}
		}
	}
}