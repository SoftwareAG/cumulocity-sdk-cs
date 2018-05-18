using Cumulocity.MQTT;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Cumulocity.MQTT.Model;
using Xunit;
using static Cumulocity.MQTT.Client;

namespace Cumulocity.MQTT.XUnitTest
{
	[Collection("Client collection")]
	public class Ws_SmartRest_Tests 
	{
		readonly Client cl;

		public Ws_SmartRest_Tests(ClientFixture fixture)
		{
			this.cl = fixture.cl;
		}

		[Fact]
		public void ClientTest_WsConnection_CheckTemplateCollectionExists_CreateGetInventoryDataAsync()
		{
			var res2 = Task.Run(() => cl.CustomSmartRest.CreateTemplateDataAsync("GetTemplate4",
																	 new List<Request> {
																		new InventoryGetRequest("9999",null, String.Empty, true),
																		new InventoryGetRequest("9998",null, "c8y_Serial", false)
																	 },
																	 new List<Response> {
																		 new Response("8889",
																		 String.Empty,
																		 "c8y_IsDevice",
																		 new List<string> { "type", "c8y_MQTTDevice", "c8y_Mobile.cellId" }),
																		 new Response("8888",
																		 String.Empty,
																		 "c8y_IsDevice",
																		 new List<string> { "type", "c8y_MQTTDevice", "c8y_Mobile.cellId" })
																	 })).Result;
			Assert.True(res2);
		}

		[Fact]
		//[Ignore("Firewall")]
		public void ClientTest_WsConnection_CheckTemplateCollectionExists_GetInventoryDataAsync()
		{
			//var res1 = Task.Run(() => cl.CustomSmartRest.SubscribeSmartRestAsync("GetTemplate4")).Result;
			var res2 = Task.Run(() => cl.CustomSmartRest.SendRequestDataAsync("GetTemplate4", "9998", new List<string> { "4927468bdd4b4171a23e31476ff82674" })).Result;
			Assert.True(res2);
		}

		[Fact]
		public void ClientTest_WsConnection_CreatePostDataAsync_MeasurementRequest()
		{
			//(string messageId, bool? response, string type, string time, IList<CustomValue> customValues)
			var res2 = Task.Run(() => cl.CustomSmartRest.CreateTemplateDataAsync("PostTemplateMeasurement",
																	 new List<Request> {
																		new MeasurementRequest("7777",
																		null,
																		"c8y_CustomMeasurement",
																		String.Empty,
																		new List<CustomValue>{
																			new CustomValue {Path = "c8y_MyMeasurement.M.value",
																				Type = Cumulocity.MQTT.Enums.CustomValueType.NUMBER,
																				Value = String.Empty
																			}
																		},Cumulocity.MQTT.Enums.HttpMethods.POST)
																	 },
																	 new List<Response> {
																		 new Response("8889",
																		 String.Empty,
																		 "c8y_IsDevice",
																		 new List<string> { "type", "c8y_MQTTDevice", "c8y_Mobile.cellId" }),
																		 new Response("8888",
																		 String.Empty,
																		 "c8y_IsDevice",
																		 new List<string> { "type", "c8y_MQTTDevice", "c8y_Mobile.cellId" })
																	 })).Result;
			Assert.True(res2);
		}

		[Fact]
		public void ClientTest_WsConnection_CreatePostDataAsync_AlarmRequest()
		{
			var res2 = Task.Run(() => cl.CustomSmartRest.CreateTemplateDataAsync("PostTemplateAlarm",
																	 new List<Request> {
																		new AlarmRequest("6666",
																		null,
																		"c8y_CustomAlarm",
																		"CustomAlarm",
																		"ACTIVE",
																		"MAJOR",
																		String.Empty,
																		new List<CustomValue>{
																			new CustomValue {
																				Path = "c8y_CustomAlarm.M.value",
																				Type = Cumulocity.MQTT.Enums.CustomValueType.NUMBER,
																				Value = String.Empty
																			}
																		},Cumulocity.MQTT.Enums.HttpMethods.POST)
																	 },
																	 new List<Response> {
																		 new Response("8889",
																		 String.Empty,
																		 "c8y_IsDevice",
																		 new List<string> {
																			 "type",
																			 "c8y_MQTTDevice",
																			 "c8y_Mobile.cellId" }),
																		 new Response("8888",
																		 String.Empty,
																		 "c8y_IsDevice",
																		 new List<string> {
																			 "type",
																			 "c8y_MQTTDevice",
																			 "c8y_Mobile.cellId" })
																	 })).Result;
			Assert.True(res2);
		}

		[Fact]
		public void ClientTest_WsConnection_CreatePostDataAsync_EventRequest()
		{
			var res2 = Task.Run(() => cl.CustomSmartRest.CreateTemplateDataAsync("PostTemplateEvent",
																	 new List<Request> {
																		new EventRequest("5555",
																		null,
																		"c8y_CustomEvent",
																		"CustomEvent",
																		String.Empty,
																		new List<CustomValue>{
																			new CustomValue {Path = "c8y_CustomEvent.M.value",
																				Type = Cumulocity.MQTT.Enums.CustomValueType.NUMBER,
																				Value = String.Empty
																			}
																		})
																	 },
																	 new List<Response> {
																		 new Response("8889",
																		 String.Empty,
																		 "c8y_IsDevice",
																		 new List<string> {
																			 "type",
																			 "c8y_MQTTDevice",
																			 "c8y_Mobile.cellId" })
																	 })).Result;
			Assert.True(res2);
		}

		[Fact]
		public void ClientTest_WsConnection_CreatePostDataAsync_InventoryRequest()
		{
			var res2 = Task.Run(() => cl.CustomSmartRest.CreateTemplateDataAsync("PostTemplateInventory",
																	 new List<Request> {
																		new InventoryRequest("4444",
																		null,
																		"c8y_MySerial",
																		new List<CustomValue>{
																			new CustomValue {Path = "c8y_CustomInventory.M.value",
																				Type = Cumulocity.MQTT.Enums.CustomValueType.NUMBER,
																				Value = String.Empty
																			}
																		},Cumulocity.MQTT.Enums.HttpMethods.POST)
																	 },
																	 new List<Response> {
																		 new Response("8889",
																		 String.Empty,
																		 "c8y_IsDevice",
																		 new List<string> { "type", "c8y_MQTTDevice", "c8y_Mobile.cellId" })
																	 })).Result;
			Assert.True(res2);
		}

		[Fact]
		public void ClientTest_WsConnection_AddUpdateDataAsync_InventoryRequest()
		{
			var res2 = Task.Run(() => cl.CustomSmartRest.CreateTemplateDataAsync("UpdateTemplateInventory",
																	 new List<Request> {
																		new InventoryRequest("3333",
																		null,
																		"c8y_MySerial",
																		new List<CustomValue>{
																			new CustomValue {Path = "c8y_CustomInventory.M.value",
																				Type = Cumulocity.MQTT.Enums.CustomValueType.NUMBER,
																				Value = String.Empty
																			}
																		},Cumulocity.MQTT.Enums.HttpMethods.PUT)
																	 },
																	 new List<Response> {
																		 new Response("8889",
																		 String.Empty,
																		 "c8y_IsDevice",
																		 new List<string> { "type", "c8y_MQTTDevice", "c8y_Mobile.cellId" })
																	 })).Result;
			Assert.True(res2);
		}

		[Fact]
		public void ClientTest_WsConnection_AddUpdateDataAsync_AlarmRequest()
		{
			var res2 = Task.Run(() => cl.CustomSmartRest.CreateTemplateDataAsync("UpdateTemplateAlarm",
																	 new List<Request> {
																		new AlarmUpdateRequest("2222",
																		null,
																		"c8y_CustomAlarm",
																		new AlarmFragment("status",null),
																		new List<CustomValue>{
																		})
																	 },
																	 new List<Response> {
																		 new Response("8889",
																		 String.Empty,
																		 "c8y_IsDevice",
																		 new List<string> { "type", "c8y_MQTTDevice", "c8y_Mobile.cellId" })
																	 })).Result;
			Assert.True(res2);
		}


		[Fact]
		public void ClientTest_WsConnection_AddUpdateDataAsync_ClearingAlarmRequest()
		{
			var res2 = Task.Run(() => cl.CustomSmartRest.CreateTemplateDataAsync("UpdateTemplateClearingAlarm ",
																	 new List<Request> {
																		new AlarmUpdateRequest("0000",
																		null,
																		"c8y_CustomAlarm",
																		new AlarmFragment("status",Cumulocity.MQTT.Enums.AlarmStatus.CLEARED),
																		new List<CustomValue>{
																		   new CustomValue {
																				Path = "c8y_CustomFragment",
																				Type = Cumulocity.MQTT.Enums.CustomValueType.STRING,
																				Value = String.Empty
																			},

																		})
																	 },
																	 new List<Response> {
																		 new Response("8889",
																		 String.Empty,
																		 "c8y_IsDevice",
																		 new List<string> { "type", "c8y_MQTTDevice", "c8y_Mobile.cellId" })
																	 })).Result;
			Assert.True(res2);
		}

		[Fact]
		public void ClientTest_WsConnection_AddUpdateDataAsync_OperationRequest()
		{
			var res2 = Task.Run(() => cl.CustomSmartRest.CreateTemplateDataAsync("UpdateTemplateOperation2",
																	 new List<Request> {
																		new OperationRequest("11112",
																		null,
																		"com_cumulocity_model_WebCamDevice",
																		new OperationFragment(
																			"status",
																			 Cumulocity.MQTT.Enums.OperationStatus.SUCCESSFUL),
																		  new List<CustomValue>{
																			new CustomValue {
																				Path = "c8y_Fragment.val",
																				Type = Cumulocity.MQTT.Enums.CustomValueType.NUMBER,
																				Value = String.Empty
																			} })
																	 },
																	 new List<Response> {
																		 new Response("88892",
																		 String.Empty,
																		 "c8y_IsDevice",
																		 new List<string> { "type", "c8y_MQTTDevice", "c8y_Mobile.cellId" })
																	 })).Result;
			Assert.True(res2);
		}

		[Fact]
		public void ClientTest_WsConnection_PostDataAsync_SendMeasurement()
		{
			//var res1 = Task.Run(() => cl.CustomSmartRest.SubscribeSmartRestAsync("PostTemplate")).Result;
			var res2 = Task.Run(() => cl.CustomSmartRest.SendRequestDataAsync("PostTemplate", "7777", new List<string> { "", "25" })).Result;
			Assert.True(res2);
		}

		[Fact]
		public void ClientTest_WsConnection_PostDataAsync_SendAlarm()
		{
			//var res1 = Task.Run(() => cl.CustomSmartRest.SubscribeSmartRestAsync("PostTemplateAlarm")).Result;
			var res2 = Task.Run(() => cl.CustomSmartRest.SendRequestDataAsync("PostTemplateAlarm", "6666", new List<string> { "ACTIVE", "MAJOR", "2017-09-20T09:03:14.000+02:00", "100" })).Result;
			Assert.True(res2);
		}

		[Fact]
		public void ClientTest_WsConnection_PostDataAsync_SendEvent()
		{
			//var res1 = Task.Run(() => cl.CustomSmartRest.SubscribeSmartRestAsync("PostTemplateEvent")).Result;
			var res2 = Task.Run(() => cl.CustomSmartRest.SendRequestDataAsync("PostTemplateEvent", "5555", new List<string> { "", "100" })).Result;
			Assert.True(res2);
		}

		[Fact]
		public void ClientTest_WsConnection_PostDataAsync_AddToInventory()
		{
			//var res1 = Task.Run(() => cl.CustomSmartRest.SubscribeSmartRestAsync("PostTemplateInventory")).Result;
			var res2 = Task.Run(() => cl.CustomSmartRest.SendRequestDataAsync("PostTemplateInventory", "4444", new List<string> { "myImei300", "2" })).Result;
			Assert.True(res2);
		}

		[Fact]
		public void ClientTest_WsConnection_UpdateDataAsync_Inventory()
		{
			//var res1 = Task.Run(() => cl.CustomSmartRest.SubscribeSmartRestAsync("UpdateTemplateInventory")).Result;
			var res2 = Task.Run(() => cl.CustomSmartRest.SendRequestDataAsync("UpdateTemplateInventory", "3333", new List<string> { "myImei300", "2" })).Result;
			Assert.True(res2);
		}

		[Fact]
		public void ClientTest_WsConnection_UpdateDataAsync_Alarm()
		{
			//var res1 = Task.Run(() => cl.CustomSmartRest.SubscribeSmartRestAsync("UpdateTemplateAlarm")).Result;
			var res2 = Task.Run(() => cl.CustomSmartRest.SendRequestDataAsync("UpdateTemplateAlarm", "2222", new List<string> { "ACKNOWLEDGED" })).Result;
			Assert.True(res2);
		}


		[Fact]
		public void ClientTest_WsConnection_UpdateDataAsync_Operation()
		{
			//var res1 = Task.Run(() => cl.CustomSmartRest.SubscribeSmartRestAsync("UpdateTemplateOperation")).Result;
			var res2 = Task.Run(() => cl.CustomSmartRest.SendRequestDataAsync("UpdateTemplateOperation", "1111", new List<string> { "\"Take a picture!\"" })).Result;
			//TestContext.WriteLine(res1.ToString());
			Assert.True(res2);
		}
	}
}
