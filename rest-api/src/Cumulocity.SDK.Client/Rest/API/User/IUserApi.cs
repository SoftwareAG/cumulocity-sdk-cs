using System;
using System.Collections.Generic;
using System.Text;
using Cumulocity.SDK.Client.Rest.Representation.User;

namespace Cumulocity.SDK.Client.Rest.API.User
{
	public interface IUserApi
	{
		CurrentUserRepresentation CurrentUser { get; }

		CurrentUserRepresentation updateCurrentUser(CurrentUserRepresentation currentUserRepresentation);

		UserRepresentation GetCurrentUserWithCustomProperties();
		UserRepresentation getUser(string tenant, string user);

		UserRepresentation create(string tenant, UserRepresentation userRepresentation);

		UserRepresentation update(string tenant, UserRepresentation userRepresentation);

		void delete(string tenant, string userName);
	}
}
