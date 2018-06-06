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
using System.IO;
using System.Linq;
using Cumulocity.MQTT.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Cumulocity.MQTT.XUnitTest
{
	public class ClientFixture : IDisposable
	{
		
		public ClientFixture()
		{
			var path = @"Properties\launchSettings.json";

			if (File.Exists(path))
			{
				using (var file = File.OpenText(path))
				{
					var reader = new JsonTextReader(file);
					var jObject = JObject.Load(reader);

					var variables = jObject
						.GetValue("profiles")
						//select a proper profile here
						.SelectMany(profiles => profiles.Children())
						.SelectMany(profile => profile.Children<JProperty>())
						.Where(prop => prop.Name == "environmentVariables")
						.SelectMany(prop => prop.Value.Children<JProperty>())
						.ToList();

					foreach (var variable in variables)
					{
						Environment.SetEnvironmentVariable(variable.Name, variable.Value.ToString());
					}
				}
			}

			var cnf = new Configuration()
			{
				Server = String.IsNullOrEmpty(Environment.GetEnvironmentVariable("TESTCSDEVICESDK_WsServer")) ? ConfigData.Instance.WsServer : Environment.GetEnvironmentVariable("TESTCSDEVICESDK_WsServer"),
				UserName = String.IsNullOrEmpty(Environment.GetEnvironmentVariable("TESTCSDEVICESDK_UserName")) ?  ConfigData.Instance.UserName : Environment.GetEnvironmentVariable("TESTCSDEVICESDK_UserName"),
				Password = String.IsNullOrEmpty(Environment.GetEnvironmentVariable("TESTCSDEVICESDK_Password")) ? ConfigData.Instance.Password : Environment.GetEnvironmentVariable("TESTCSDEVICESDK_Password"),
				ClientId = String.IsNullOrEmpty(Environment.GetEnvironmentVariable("TESTCSDEVICESDK_ClientId")) ?  ConfigData.Instance.ClientId : Environment.GetEnvironmentVariable("TESTCSDEVICESDK_ClientId"),
				Port = String.IsNullOrEmpty(Environment.GetEnvironmentVariable("TESTCSDEVICESDK_WsPort")) ? ConfigData.Instance.WsPort : Environment.GetEnvironmentVariable("TESTCSDEVICESDK_WsPort"),
				ConnectionType = "WS"
			};

			cl = new Client(cnf);
			var item = cl.ConnectAsync().Result;
		}

		public void Dispose()
		{
			cl.DisconnectAsync();
		}

		public Client cl { get; set; }
	}
}