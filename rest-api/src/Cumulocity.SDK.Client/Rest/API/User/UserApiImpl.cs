using System;
using System.Collections.Generic;
using System.Text;
using Cumulocity.SDK.Client.Rest.Representation.User;

namespace Cumulocity.SDK.Client.Rest.API.User
{
	public class UserApiImpl : IUserApi
	{

		private const string REALM = "realm";
		private const string USER_NAME = "userName";

		private readonly RestConnector restConnector;
		private readonly TemplateUrlParser templateUrlParser;
		private readonly UsersApiRepresentation usersApiRepresentation;

		public UserApiImpl(RestConnector restConnector, TemplateUrlParser templateUrlParser, UsersApiRepresentation usersApiRepresentation)
		{
			this.restConnector = restConnector;
			this.templateUrlParser = templateUrlParser;
			this.usersApiRepresentation = usersApiRepresentation;
		}

		public  CurrentUserRepresentation CurrentUser
		{
			get
			{
				string url = usersApiRepresentation.CurrentUser;
				return restConnector.Get<CurrentUserRepresentation>(url, UserMediaType.CURRENT_USER, typeof(CurrentUserRepresentation));
			}
		}

		public  CurrentUserRepresentation updateCurrentUser(CurrentUserRepresentation currentUserRepresentation)
		{
			string url = usersApiRepresentation.CurrentUser;
			return restConnector.PutWithoutId(url, UserMediaType.CURRENT_USER, currentUserRepresentation);
		}

		public UserRepresentation GetCurrentUserWithCustomProperties()
		{
			string url = usersApiRepresentation.CurrentUser;
			return restConnector.Get<UserRepresentation>(url, UserMediaType.CURRENT_USER, typeof(CurrentUserRepresentation));
		}

		public  UserRepresentation CurrentUserWithCustomProperties
		{
			get
			{
				string url = usersApiRepresentation.CurrentUser;
				return restConnector.Get<UserRepresentation>(url, UserMediaType.USER, typeof(UserRepresentation));
			}
		}

		public  UserRepresentation getUser(string tenant, string user)
		{
			IDictionary<string, string> @params = new Dictionary<string, string>();
			@params[REALM] = tenant;
			@params[USER_NAME] = user;
			string url = templateUrlParser.replacePlaceholdersWithParams(usersApiRepresentation.UserByName, @params);
			Console.WriteLine(url);
			return restConnector.Get<UserRepresentation>(url, UserMediaType.USER, typeof(UserRepresentation));
		}

		public UserRepresentation create(string tenant, UserRepresentation userRepresentation)
		{
			IDictionary<string, string> @params = new Dictionary<string, string>();
			@params[REALM] = tenant;
			string url = templateUrlParser.replacePlaceholdersWithParams(usersApiRepresentation.Users, @params);
			return restConnector.Post(url, UserMediaType.USER, userRepresentation);
		}

		public UserRepresentation update(string tenant, UserRepresentation userRepresentation)
		{
			IDictionary<string, string> @params = new Dictionary<string, string>();
			@params[REALM] = tenant;
			string url = templateUrlParser.replacePlaceholdersWithParams(usersApiRepresentation.Users + '/' + userRepresentation.UserName, @params);
			userRepresentation.UserName = null;
			return restConnector.PutWithoutId(url, UserMediaType.USER, userRepresentation);
		}

		public void delete(string tenant, string userName)
		{
			IDictionary<string, string> @params = new Dictionary<string, string>();
			@params[REALM] = tenant;
			string url = templateUrlParser.replacePlaceholdersWithParams(usersApiRepresentation.Users + '/' + userName, @params);
			restConnector.Delete(url);
		}
	}
}
