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

		//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public String getId()
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

		//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public String getUserName()
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

		//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public String getPassword()
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

		//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public String getFirstName()
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

		//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public String getLastName()
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

		//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public String getPhone()
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

		//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public String getEmail()
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

		//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
		//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public List<RoleRepresentation> getEffectiveRoles()
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

		//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public System.Nullable<bool> getShouldResetPassword()
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

		//ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public System.Nullable<bool> getSupportUserEnabled()
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

		//ORIGINAL LINE: @JSONProperty(value = "deprecated_LastPasswordChange", ignore = true) @Deprecated public Date getLastPasswordChange()
		//[Obsolete]
		//public virtual DateTime LastPasswordChange
		//{
		//	get
		//	{
		//		return lastPasswordChange == null ? null : lastPasswordChange.toDate();
		//	}
		//	set
		//	{
		//		this.lastPasswordChange = value == null ? null : newUTC(value);
		//	}
		//}

		//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
		//ORIGINAL LINE: @JSONProperty(value = "lastPasswordChange", ignoreIfNull = true) @JSONConverter(type = DateTimeConverter.class) public DateTime getLastPasswordChangeDateTime()
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