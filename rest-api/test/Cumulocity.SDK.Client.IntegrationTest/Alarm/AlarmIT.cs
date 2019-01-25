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

#endregion Cumulocity GmbH

using Cumulocity.SDK.Client.Rest;
using Cumulocity.SDK.Client.Rest.API.Alarm;
using Cumulocity.SDK.Client.Rest.Model.Event;
using Cumulocity.SDK.Client.Rest.Representation.Alarm;
using Cumulocity.SDK.Client.Rest.Representation.Builder;
using Cumulocity.SDK.Client.Rest.Representation.Inventory;
using System;
using System.Collections.Generic;
using System.Net;
using Cumulocity.SDK.Client.Rest.Model.Idtype;
using Microsoft.DotNet.PlatformAbstractions;
using Xunit;

namespace Cumulocity.SDK.Client.IntegrationTest.Alarm
{
#pragma warning disable 0612
	public class AlarmIT : IClassFixture<AlarmFixture>, IDisposable
	{
		public AlarmIT(AlarmFixture fixture)
		{
			AlarmApi = fixture.platform.AlarmApi;
			Platform = fixture.platform;
			mo1 = fixture.platform.InventoryApi.Create(aSampleMo().WithName("MO" + 1).build());
			mo2 = fixture.platform.InventoryApi.Create(aSampleMo().WithName("MO" + 2).build());
			mo3 = fixture.platform.InventoryApi.Create(aSampleMo().WithName("MO" + 3).build());
		}

		public void Dispose()
		{
			var inventory = Platform.InventoryApi;
			List<GId> lst = new List<GId>();
			foreach (var item in inventory.ManagedObjects.GetFirstPage().AllPages())
			{
				lst.Add(item.Id);
			}

			foreach (var id in lst)
			{
#pragma warning disable 0612
				var mo = inventory.GetManagedObject(id);
				mo.Delete();
			}
		}

		private readonly ManagedObjectRepresentation mo1;
		private ManagedObjectRepresentation mo2;
		private ManagedObjectRepresentation mo3;
		private int t;

		public IAlarmApi AlarmApi { get; set; }
		public  PlatformImpl Platform { get; set; }

		private static ManagedObjectRepresentationBuilder aSampleMo()
		{
			return RestRepresentationObjectMother.anMoRepresentationLike(SampleManagedObjectRepresentation
				.MO_REPRESENTATION);
		}

		private AlarmRepresentation aSampleAlarm(ManagedObjectRepresentation source)
		{
			return RestRepresentationObjectMother.anAlarmRepresentationLike(
					SampleAlarmRepresentation.ALARM_REPRESENTATION)
				.withType("com_nsn_bts_TrxFaulty" + t++)
				.withStatus("ACTIVE")
				.withSeverity("MAJOR")
				.withSource(source)
				.withText("Alarm for mo")
				.withDateTime(DateTime.UtcNow).build();
		}

		[Fact]
		public void shouldHaveIdAfterCreateAlarm()
		{
			// Given
			var rep = aSampleAlarm(mo1);
			// When
			var created = AlarmApi.Create(rep);
			// Then
			Assert.NotNull(created.Id);
		}

		[Fact]
		public void createAlarmWithoutText()
		{
			// Given
			AlarmRepresentation alarm = RestRepresentationObjectMother.anAlarmRepresentationLike(
					SampleAlarmRepresentation.ALARM_REPRESENTATION)
			.withSource(mo1).withText(null).build();

			// When
			var ex = Record.Exception(() => AlarmApi.Create(alarm));

			// Then
			Assert.NotNull(ex);
			Assert.IsType<SDKException>(ex);
			Assert.Equal((int)HttpStatusCode.UnprocessableEntity, ((SDKException)ex).HttpStatus);
		}

		[Fact]
		public void createAlarmsWithoutSeverity()
		{
			// Given
			AlarmRepresentation alarm = RestRepresentationObjectMother.anAlarmRepresentationLike(
				   SampleAlarmRepresentation.ALARM_REPRESENTATION)
			   .withSource(mo1).withSeverity(null).build();

			// When
			var ex = Record.Exception(() => AlarmApi.Create(alarm));

			// Then
			Assert.NotNull(ex);
			Assert.IsType<SDKException>(ex);
			Assert.Equal((int)HttpStatusCode.UnprocessableEntity, ((SDKException)ex).HttpStatus);
		}

		[Fact]
		public void shouldReturnAllCreatedAlarms()
		{
			// Given
			ManagedObjectRepresentation source = mo1;

			for (int i = 0; i < 10; i++)
			{
				AlarmApi.Create(aSampleAlarm(source));
			}

			int resultNumber = 0;
			var pager = AlarmApi.GetAlarmsByFilter(new AlarmFilter().BySource(source.Id)).GetFirstPage().AllPages();
			foreach (AlarmRepresentation alarm in pager)
			{
				resultNumber++;
			}

			Assert.Equal( 10, resultNumber);
		}

		[Fact]
		public void shouldReturnAllCreatedAsyncAlarms()
		{
			// Given
			ManagedObjectRepresentation source = mo1;

			for (int i = 0; i < 10; i++)
			{
				var alarmRepresentation = AlarmApi.CreateAsync(aSampleAlarm(source)).Result;
			}

			int resultNumber = 0;
			var pager = AlarmApi.GetAlarmsByFilter(new AlarmFilter().BySource(source.Id)).GetFirstPage().AllPages();
			foreach (AlarmRepresentation alarm in pager)
			{
				resultNumber++;
			}

			Assert.Equal(10, resultNumber);
		}

		[Fact]
		public void shouldReturnNoAlarmWithUnmatchedFilter()
		{
			// Given
			AlarmApi.Create(aSampleAlarm(mo1));

			// When
			AlarmFilter filter = new AlarmFilter().BySource(mo3);
			AlarmCollectionRepresentation bySource = AlarmApi.GetAlarmsByFilter(filter).GetFirstPage();

			// Then
			var alarms = bySource.Alarms;
			Assert.Equal(0, alarms.Count);
		}

		[Fact]
		public void shouldReturnMultipleAlarmsWithMatchedFilter()
		{
			// Given
			AlarmApi.Create(aSampleAlarm(mo1));
			AlarmApi.Create(aSampleAlarm(mo1));

			// When
			AlarmFilter filter = new AlarmFilter().BySource(mo1);
			AlarmCollectionRepresentation bySource = AlarmApi.GetAlarmsByFilter(filter).GetFirstPage();

			// Then
			var alarms = bySource.Alarms;
			Assert.Equal(2, alarms.Count);
		}

		[Fact]
		public void shouldReturnFilterBySource()
		{
			// Given
			AlarmApi.Create(aSampleAlarm(mo1));
			AlarmApi.Create(aSampleAlarm(mo2));

			// When
			AlarmFilter filter = new AlarmFilter().BySource(mo1);
			AlarmCollectionRepresentation bySource = AlarmApi.GetAlarmsByFilter(filter).GetFirstPage();

			// Then
			var alarms = bySource.Alarms;
			Assert.Equal(1, alarms.Count);
			Assert.Equal(alarms[0].Source.Id, mo1.Id);
		}

		[Fact]
		public void getAlarmCollectionByStatus()
		{
			// Given
			AlarmApi.Create(RestRepresentationObjectMother.anAlarmRepresentationLike(
					SampleAlarmRepresentation.ALARM_REPRESENTATION)
					.withType("com_nsn_bts_TrxFaulty" + t++)
					.withStatus("ACTIVE")
					.withSource(mo1).build());

			AlarmApi.Create(RestRepresentationObjectMother.anAlarmRepresentationLike(
					SampleAlarmRepresentation.ALARM_REPRESENTATION)
					.withType("com_nsn_bts_TrxFaulty" + t++)
					.withStatus("ACKNOWLEDGED")
					.withSource(mo1).build());

			// When
			AlarmFilter acknowledgedFilter = new AlarmFilter().ByStatus(CumulocityAlarmStatuses.valueOf("ACKNOWLEDGED"));

			// Then
			foreach (AlarmRepresentation result in AlarmApi.GetAlarmsByFilter(acknowledgedFilter).GetFirstPage().AllPages())
			{
				Assert.Equal("ACKNOWLEDGED", result.Status);
			}
		}

		[Fact]
		public void getAlarmCollectionByStatusAndSource()
		{
			// Given
			AlarmApi.Create(RestRepresentationObjectMother.anAlarmRepresentationLike(
					SampleAlarmRepresentation.ALARM_REPRESENTATION)
						.withType("com_nsn_bts_TrxFaulty" + t++)
						.withStatus("CLEARED")
						.withSource(mo1).build());

			AlarmApi.Create(RestRepresentationObjectMother.anAlarmRepresentationLike(
					SampleAlarmRepresentation.ALARM_REPRESENTATION)
						.withType("com_nsn_bts_TrxFaulty" + t++)
						.withStatus("ACKNOWLEDGED")
						.withSource(mo1).build());

			AlarmApi.Create(RestRepresentationObjectMother.anAlarmRepresentationLike(
					SampleAlarmRepresentation.ALARM_REPRESENTATION)
						.withType("com_nsn_bts_TrxFaulty" + t++)
						.withStatus("CUSTOM")
						.withSource(mo2).build());

			// When
			AlarmFilter acknowledgedFilter = new AlarmFilter()
				.ByStatus(CumulocityAlarmStatuses.valueOf("ACKNOWLEDGED")).BySource(mo1);
			AlarmCollectionRepresentation acknowledgedAlarms = AlarmApi.GetAlarmsByFilter(acknowledgedFilter).GetFirstPage();

			// Then
			var alarms = acknowledgedAlarms.Alarms;
			Assert.Equal(1, alarms.Count);
			Assert.Equal("ACKNOWLEDGED", alarms[0].Status);
			Assert.Equal(mo1.Id, alarms[0].Source.Id);
		}

		[Fact]
		public void shouldGetAlarmById()
		{
			// Given
			AlarmRepresentation created = AlarmApi.Create(RestRepresentationObjectMother.anAlarmRepresentationLike(
					SampleAlarmRepresentation.ALARM_REPRESENTATION)
				.withStatus("ACTIVE")
				.withSource(mo1).build());

			AlarmRepresentation returned = AlarmApi.GetAlarm(created.Id);

			Assert.Equal("ACTIVE", returned.Status);
			Assert.Equal(mo1.Id, returned.Source.Id);
		}

		[Fact]
		public void shouldReturnTheUpdatedAlarm()
		{
			// Given
			AlarmRepresentation created = AlarmApi.Create(RestRepresentationObjectMother.anAlarmRepresentationLike(
					SampleAlarmRepresentation.ALARM_REPRESENTATION)
					.withStatus("ACTIVE")
					.withSource(mo1).build());

			// When
			AlarmRepresentation alarm = new AlarmRepresentation();
			alarm.Status = "ACKNOWLEDGED";
			alarm.Id = created.Id;
			AlarmRepresentation updated = AlarmApi.UpdateAlarm(alarm);

			// Then
			Assert.Equal("ACKNOWLEDGED", updated.Status);
			Assert.Equal(mo1.Id, updated.Source.Id);
		}

		[Fact]
		public void shouldUpdateAlarm()
		{
			// Given
			AlarmRepresentation created = AlarmApi.Create(RestRepresentationObjectMother.anAlarmRepresentationLike(
					SampleAlarmRepresentation.ALARM_REPRESENTATION)
						.withStatus("ACTIVE")
						.withSource(mo1).build());

			// When
			AlarmRepresentation alarm = new AlarmRepresentation();
			alarm.Status ="ACKNOWLEDGED";
			alarm.Id = created.Id;
			AlarmApi.UpdateAlarm(alarm);

			// Then
			AlarmRepresentation returned = AlarmApi.GetAlarm(created.Id);

			Assert.Equal("ACKNOWLEDGED", returned.Status);
			Assert.Equal(mo1.Id, returned.Source.Id);
		}

		[Fact]
		public void shouldDeleteAlarmCollectionByEmptyFilter()
		{
			// Given
			for (int i = 0; i < 5; i++)
			{
				AlarmApi.Create(aSampleAlarm(mo1));
				AlarmApi.Create(aSampleAlarm(mo2));
				AlarmApi.Create(aSampleAlarm(mo3));
			}

			AlarmFilter emptyFilter = new AlarmFilter();

			//When
			AlarmApi.DeleteAlarmsByFilter(emptyFilter);

			//Then
			int resultNumber = 0;
			var pager = AlarmApi.GetAlarms().GetFirstPage().AllPages();
			foreach (AlarmRepresentation alarm in pager)
			{
				resultNumber++;
			}

			Assert.Equal(0,resultNumber);
		}

		[Fact]
		public void shouldDeleteByFilterStatus()
		{
			var allAlarmsT = AlarmApi.GetAlarms().GetFirstPage().Alarms;
			// Given
			AlarmApi.Create(RestRepresentationObjectMother.anAlarmRepresentationLike(
					SampleAlarmRepresentation.ALARM_REPRESENTATION)
						.withType("com_nsn_bts_TrxFaulty" + t++)
						.withStatus("ACTIVE")
						.withSource(mo1).build());

			AlarmApi.Create(RestRepresentationObjectMother.anAlarmRepresentationLike(
					SampleAlarmRepresentation.ALARM_REPRESENTATION)
						.withType("com_nsn_bts_TrxFaulty" + t++)
						.withStatus("ACKNOWLEDGED")
						.withSource(mo1).build());

			AlarmApi.Create(RestRepresentationObjectMother.anAlarmRepresentationLike(
					SampleAlarmRepresentation.ALARM_REPRESENTATION)
						.withType("com_nsn_bts_TrxFaulty" + t++)
						.withStatus("ACKNOWLEDGED")
						.withSource(mo2).build());

			AlarmApi.Create(RestRepresentationObjectMother.anAlarmRepresentationLike(
					SampleAlarmRepresentation.ALARM_REPRESENTATION)
						.withType("com_nsn_bts_TrxFaulty" + t++)
						.withStatus("CLEARED")
						.withSource(mo2).build());

			AlarmFilter alarmFilter = new AlarmFilter().ByStatus(CumulocityAlarmStatuses.valueOf("ACKNOWLEDGED"));

			// When
			AlarmApi.DeleteAlarmsByFilter(alarmFilter);

			// Then
			var allAlarms = AlarmApi.GetAlarms().GetFirstPage().Alarms;

			Assert.Equal(2, allAlarms.Count);
			List<string> wantedStatus = new List<string>();
			wantedStatus.Add("ACTIVE");
			wantedStatus.Add("CLEARED");
			foreach (AlarmRepresentation alarm in allAlarms)
			{
				Assert.Contains(alarm.Status, wantedStatus);
			}
		}
	}
}