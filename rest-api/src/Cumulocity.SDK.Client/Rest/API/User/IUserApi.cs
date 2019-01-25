using Cumulocity.SDK.Client.Rest.Representation.User;

namespace Cumulocity.SDK.Client.Rest.API.User
{
	public interface IUserApi
	{
		CurrentUserRepresentation CurrentUser { get; }

		CurrentUserRepresentation UpdateCurrentUser(CurrentUserRepresentation currentUserRepresentation);

		UserRepresentation GetCurrentUserWithCustomProperties();

		UserRepresentation GetUser(string tenant, string user);

		UserRepresentation Create(string tenant, UserRepresentation userRepresentation);

		UserRepresentation Update(string tenant, UserRepresentation userRepresentation);

		void Delete(string tenant, string userName);
	}
}