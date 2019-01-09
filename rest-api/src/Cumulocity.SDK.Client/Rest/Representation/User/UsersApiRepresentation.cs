using System;
using System.Collections.Generic;
using System.Text;

namespace Cumulocity.SDK.Client.Rest.Representation.User
{
	public class UsersApiRepresentation : BaseResourceRepresentation
	{

		private string users;

		private string userByName;

		private string currentUser;

		private string groups;

		private string groupByName;

		private string roles;

		public virtual string Users
		{
			get
			{
				return users;
			}
			set
			{
				this.users = value;
			}
		}


		public virtual string Groups
		{
			get
			{
				return groups;
			}
			set
			{
				this.groups = value;
			}
		}


		/// <summary>
		/// Contains a placeholder name and point to a resource of Type User.
		/// </summary>
		public virtual string UserByName
		{
			get
			{
				return userByName;
			}
			set
			{
				this.userByName = value;
			}
		}


		public virtual string CurrentUser
		{
			get
			{
				return currentUser;
			}
			set
			{
				this.currentUser = value;
			}
		}


		public virtual string GroupByName
		{
			set
			{
				this.groupByName = value;
			}
			get
			{
				return groupByName;
			}
		}


		public virtual string Roles
		{
			set
			{
				this.roles = value;
			}
			get
			{
				return roles;
			}
		}

	}

}
