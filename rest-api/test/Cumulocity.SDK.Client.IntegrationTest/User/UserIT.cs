using System;
using System.Collections.Generic;
using System.Text;
using Cumulocity.SDK.Client.Rest;
using Cumulocity.SDK.Client.Rest.API.User;
using Xunit;

namespace Cumulocity.SDK.Client.IntegrationTest.User
{
	public class UserIT: IClassFixture<UserFixture>, IDisposable
	{
		private readonly PlatformImpl _fixture;
		private IUserApi _userApi;

		public UserIT(UserFixture userFixture)
		{
			_fixture = userFixture.platform;
			_userApi = userFixture.platform.UserApi;
		}

		[Fact]
		public void GetUserByTenantAndUserName()
		{
			var u = _userApi.getUser("piotr", "admin");
			Assert.NotNull(u);
		}

		public void Dispose()
		{
		}
		
	}
}
