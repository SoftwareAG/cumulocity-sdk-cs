namespace Cumulocity.SDK.Client.Rest.Representation.User
{
	public class UserReferenceRepresentation : BaseResourceRepresentation, IReferenceRepresentation
	{
		private UserRepresentation user;

		public virtual UserRepresentation User
		{
			get
			{
				return user;
			}
			set
			{
				this.user = value;
			}
		}
	}
}