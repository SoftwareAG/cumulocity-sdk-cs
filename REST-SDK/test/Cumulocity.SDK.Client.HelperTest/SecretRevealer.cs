﻿#region Cumulocity GmbH

// /*
//  * Copyright (C) 2015-2018
//  *
//  * Permission is hereby granted, free of charge, to any person obtaining a copy of
//  * this software and associated documentation files (the "Software"),
//  * to deal in the Software without restriction, including without limitation the rights to use,
//  * copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software,
//  * and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
//  *
//  * The above copyright notice and this permission notice shall be
//  * included in all copies or substantial portions of the Software.
//  *
//  * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
//  * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
//  * OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
//  * IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
//  * DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
//  * ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//  */

#endregion

using System;
using Microsoft.Extensions.Options;

namespace Cumulocity.SDK.Client.HelperTest
{
	public class SecretRevealer : ISecretRevealer
	{
		private readonly SecretPlatform _secrets;

		public SecretRevealer(IOptions<SecretPlatform> secrets)
		{
			_secrets = secrets.Value ?? throw new ArgumentNullException(nameof(secrets));
		}

		public (string user, string pass, string userbootstrap, string passbootstrap, string platformurl) Reveal()
		{
			return (_secrets.UserAdmin, _secrets.PasswordAdmin, _secrets.UserBootstrap, _secrets.PasswordBootstrap, _secrets.PlatformUrl);
		}
	}
}