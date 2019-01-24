namespace Cumulocity.SDK.Client.Rest.API.Audit
{
	/// <summary>
	/// A filter to be used in audit record queries.
	/// The setter (by*) methods return the filter itself to provide chaining:
	/// {@code AuditRecordFilter filter = new AuditRecordFilter().ByUser(user).ByType(type);}
	/// </summary>
	public class AuditRecordFilter : Filter
	{
		private string user;

		private string type;

		private string application;

		/// <summary>
		/// Specifies the {@code user} query parameter.
		/// </summary>
		/// <param name="user"> the user associated with the audit record(s) </param>
		/// <returns> the audit record filter with {@code user} set </returns>
		public virtual AuditRecordFilter ByUser(string user)
		{
			this.user = user;
			return this;
		}

		/// <summary>
		/// Specifies the {@code type} query parameter.
		/// </summary>
		/// <param name="type"> the type of the audit record(s) </param>
		/// <returns> the audit record filter with {@code type} set </returns>
		public virtual AuditRecordFilter ByType(string type)
		{
			this.type = type;
			return this;
		}

		/// <summary>
		/// Specifies the {@code application} query parameter
		/// </summary>
		/// <param name="application"> the application associated with the audit record(s) </param>
		/// <returns> the audit record filter with {@code application} set </returns>
		public virtual AuditRecordFilter ByApplication(string application)
		{
			this.application = application;
			return this;
		}

		/// <returns> the {@code user} parameter of the query </returns>
		public virtual string User
		{
			get
			{
				return user;
			}
		}

		/// <returns> the {@code type} parameter of the query </returns>
		public virtual string Type
		{
			get
			{
				return type;
			}
		}

		/// <returns> the {@code application} parameter of the query </returns>
		public virtual string Application
		{
			get
			{
				return application;
			}
		}
	}
}