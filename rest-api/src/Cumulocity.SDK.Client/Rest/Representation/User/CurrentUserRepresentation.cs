using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Cumulocity.SDK.Client.Rest.Representation.User
{
	/// <summary>
	/// This class intentionally is not extending <seealso cref="UserRepresentation"/>, although
	/// <seealso cref="com.cumulocity.model.user.CurrentUser CurrentUser"/> extends <seealso cref="com.cumulocity.model.user.User User"/>.
	/// Current user resource provides information required by client (application) for normal operation.
	/// One example is effective list of roles. Without building this list on server side, client would have to explicitly go
	/// through associated roles/groups, possibly with pagination operations. It is both difficult, and not possible if
	/// ROLE_USER_MANAGEMENT_READ role is not present.
	/// </summary>
	public class CurrentUserRepresentation : BaseResourceRepresentation
	{
		private string id;

		private string userName;

		private string password;

		private bool? shouldResetPassword;

		private DateTime lastPasswordChange;

		private string firstName;

		private string lastName;

		private string phone;

		private string email;

		private IList<RoleRepresentation> effectiveRoles;

		private bool? supportUserEnabled;

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual string Id
		{
			get
			{
				return id;
			}
			set
			{
				this.id = value;
			}
		}

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual string UserName
		{
			get
			{
				return userName;
			}
			set
			{
				this.userName = value;
			}
		}

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual string Password
		{
			get
			{
				return password;
			}
			set
			{
				this.password = value;
			}
		}

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual string FirstName
		{
			get
			{
				return firstName;
			}
			set
			{
				this.firstName = value;
			}
		}

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual string LastName
		{
			get
			{
				return lastName;
			}
			set
			{
				this.lastName = value;
			}
		}

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual string Phone
		{
			get
			{
				return phone;
			}
			set
			{
				this.phone = value;
			}
		}

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual string Email
		{
			get
			{
				return email;
			}
			set
			{
				this.email = value;
			}
		}

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual IList<RoleRepresentation> EffectiveRoles
		{
			get
			{
				return effectiveRoles;
			}
			set
			{
				this.effectiveRoles = value;
			}
		}

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual bool? ShouldResetPassword
		{
			get
			{
				return shouldResetPassword;
			}
			set
			{
				this.shouldResetPassword = value;
			}
		}

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public virtual bool? SupportUserEnabled
		{
			get
			{
				return supportUserEnabled;
			}
			set
			{
				this.supportUserEnabled = value;
			}
		}

		[JsonProperty("lastPasswordChange", NullValueHandling = NullValueHandling.Ignore)]
		public virtual DateTime LastPasswordChangeDateTime
		{
			get
			{
				return lastPasswordChange;
			}
			set
			{
				this.lastPasswordChange = value;
			}
		}
	}
}