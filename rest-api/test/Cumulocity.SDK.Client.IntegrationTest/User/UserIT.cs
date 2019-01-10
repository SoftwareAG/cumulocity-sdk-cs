using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Cumulocity.SDK.Client.Rest;
using Cumulocity.SDK.Client.Rest.API.User;
using Cumulocity.SDK.Client.Rest.Representation.User;
using Xunit;

namespace Cumulocity.SDK.Client.IntegrationTest.User
{
	public class UserIT: IClassFixture<UserFixture>, IDisposable
	{
		private readonly PlatformImpl _fixture;
		private IUserApi userApi;
		private string userName = "test";
		private string tenant = "piotr";

		public UserIT(UserFixture userFixture)
		{
			_fixture = userFixture.platform;
			userApi = userFixture.platform.UserApi;
		}
		[Fact]
		public void GetCurrentUser()
		{
			var cu = userApi.GetCurrentUserWithCustomProperties();
			Assert.NotNull(cu);
			Assert.Equal("admin", cu.UserName);

		}
		[Fact]
		public void DeleteUser()
		{
			
			// Given
			UserRepresentation userRepresentation = CreateUserRepresentation();
			var u1 = userApi.create(tenant, userRepresentation);
			Assert.NotNull(u1);

			// When
			userApi.delete(tenant, userRepresentation.UserName);
			var ex = Record.Exception(() => userApi.getUser(tenant, userRepresentation.UserName));

			// Then
			Assert.NotNull(ex);
			Assert.IsType<SDKException>(ex);
			Assert.Equal((int)HttpStatusCode.NotFound, ((SDKException)ex).HttpStatus);

		}

		[Fact]
		public void CreateUserAndCheckProperties()
		{
			// Given
			UserRepresentation ur = CreateUserRepresentation();
			var u1 = userApi.create(tenant, ur);
			// When
			var u2 = userApi.getUser(tenant, ur.UserName);
			userApi.delete(tenant, ur.UserName);
			//Then
			Assert.NotNull(u2);
			Assert.Equal(ur.UserName, u2.UserName);
		}

		[Fact]
		public void CreateUserAndUpdateProperties()
		{
			// Given
			UserRepresentation ur = CreateUserRepresentation();
			var u1 = userApi.create(tenant, ur);
			UserRepresentation userRepresentation = new UserRepresentation();
			u1.UserName = userName;
			u1.LastName = "lastName";
			// When
			var u2 = userApi.update(tenant, u1);
			var u3 = userApi.getUser(tenant, u2.UserName);
			//Then
			Assert.NotNull(u2);
			Assert.Equal(u1.LastName, u3.LastName);
			userApi.delete(tenant, u3.UserName);
		}

		private  UserRepresentation CreateUserRepresentation()
		{
			UserRepresentation userRepresentation = new UserRepresentation();
			userRepresentation.Enabled = true;
			userRepresentation.Password = "test3333";
			userRepresentation.UserName = userName;
			userRepresentation.LastName = "test";
			userRepresentation.Roles = new RoleReferenceCollectionRepresentation();
			userRepresentation.Roles.References.Add(new RoleReferenceRepresentation() { Role = new RoleRepresentation() { Id = "ROLE_USER_MANAGEMENT_OWN_READ", Name = "ROLE_USER_MANAGEMENT_OWN_READ" } });
			return userRepresentation;
		}

		public void Dispose()
		{
			try
			{
				userApi.delete(tenant, userName);
			}
			catch(SDKException ex){}
		}
		
	}
}
