#region Cumulocity GmbH

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
using System.Linq;

namespace Cumulocity.MQTT.XUnitTest
{
	public sealed class ConfigData
	{
		private static readonly ConfigData instance = new ConfigData();

		static ConfigData()
		{
		}

		private ConfigData()
		{
			TcpServer = "daria.staging7.c8y.io";
			TlsServer = "daria.staging7.c8y.io";
			WsServer = "ws://daria.staging7.c8y.io/mqtt";
			WssServer = "wss://daria.staging7.c8y.io/mqtt";
			UserName = @"daria/admin";
			Password = @"test1234";
			ClientId = "4927468bdd4b4171a23e31476ff82674";
			TlsPort = "8883";
			TcpPort = "1883";
			WsPort = "80";
			WssPort = "443";
		}

		public static ConfigData Instance
		{
			get
			{
				return instance;
			}
		}

		private static Random random = new Random();

		public static string RandomString(int length)
		{
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
			return new string(Enumerable.Repeat(chars, length)
				.Select(s => s[random.Next(s.Length)]).ToArray());
		}

		public string TcpServer { get; }
		public string TlsServer { get; }
		public string WsServer { get; }
		public string WssServer { get; }
		public string UserName { get; }
		public string Password { get; }
		public string TlsPort { get; }
		public string TcpPort { get; }
		public string WsPort { get; }
		public string WssPort { get; }
		public string ClientId { get; }
	}
}