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
using Xunit;

namespace Cumulocity.SDK.Client.IntegrationTest.Alarm
{
	public class AlarmIT : IClassFixture<AlarmFixture>, IDisposable
	{
		public AlarmIT(AlarmFixture fixture)
		{
			AlarmApi = fixture.platform.AlarmApi;

			mo1 = fixture.platform.InventoryApi.create(aSampleMo().withName("MO" + 1).build());
			mo2 = fixture.platform.InventoryApi.create(aSampleMo().withName("MO" + 2).build());
			mo3 = fixture.platform.InventoryApi.create(aSampleMo().withName("MO" + 3).build());
		}

		public void Dispose()
		{
		}

		private readonly ManagedObjectRepresentation mo1;
		private ManagedObjectRepresentation mo2;
		private ManagedObjectRepresentation mo3;
		private int t;

		public IAlarmApi AlarmApi { get; set; }

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
			var created = AlarmApi.create(rep);
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
			var ex = Record.Exception(() => AlarmApi.create(alarm));

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
			var ex = Record.Exception(() => AlarmApi.create(alarm));

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
				AlarmApi.create(aSampleAlarm(source));
			}

			int resultNumber = 0;
			var pager = AlarmApi.getAlarmsByFilter(new AlarmFilter().bySource(source.Id)).get().allPages();
			foreach (AlarmRepresentation alarm in pager)
			{
				resultNumber++;
			}

			Assert.Equal(resultNumber, 10);
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
			var pager = AlarmApi.getAlarmsByFilter(new AlarmFilter().bySource(source.Id)).get().allPages();
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
			AlarmApi.create(aSampleAlarm(mo1));

			// When
			AlarmFilter filter = new AlarmFilter().bySource(mo3);
			AlarmCollectionRepresentation bySource = AlarmApi.getAlarmsByFilter(filter).get();

			// Then
			var alarms = bySource.Alarms;
			Assert.Equal(0, alarms.Count);
		}

		[Fact]
		public void shouldReturnMultipleAlarmsWithMatchedFilter()
		{
			// Given
			AlarmApi.create(aSampleAlarm(mo1));
			AlarmApi.create(aSampleAlarm(mo1));

			// When
			AlarmFilter filter = new AlarmFilter().bySource(mo1);
			AlarmCollectionRepresentation bySource = AlarmApi.getAlarmsByFilter(filter).get();

			// Then
			var alarms = bySource.Alarms;
			Assert.Equal(2, alarms.Count);
		}

		[Fact]
		public void shouldReturnFilterBySource()
		{
			// Given
			AlarmApi.create(aSampleAlarm(mo1));
			AlarmApi.create(aSampleAlarm(mo2));

			// When
			AlarmFilter filter = new AlarmFilter().bySource(mo1);
			AlarmCollectionRepresentation bySource = AlarmApi.getAlarmsByFilter(filter).get();

			// Then
			var alarms = bySource.Alarms;
			Assert.Equal(1, alarms.Count);
			Assert.Equal(alarms[0].Source.Id, mo1.Id);
		}

		[Fact]
		public void getAlarmCollectionByStatus()
		{
			// Given
			AlarmApi.create(RestRepresentationObjectMother.anAlarmRepresentationLike(
					SampleAlarmRepresentation.ALARM_REPRESENTATION)
					.withType("com_nsn_bts_TrxFaulty" + t++)
					.withStatus("ACTIVE")
					.withSource(mo1).build());

			AlarmApi.create(RestRepresentationObjectMother.anAlarmRepresentationLike(
					SampleAlarmRepresentation.ALARM_REPRESENTATION)
					.withType("com_nsn_bts_TrxFaulty" + t++)
					.withStatus("ACKNOWLEDGED")
					.withSource(mo1).build());

			// When
			AlarmFilter acknowledgedFilter = new AlarmFilter().byStatus(CumulocityAlarmStatuses.valueOf("ACKNOWLEDGED"));

			// Then
			foreach (AlarmRepresentation result in AlarmApi.getAlarmsByFilter(acknowledgedFilter).get().allPages())
			{
				Assert.Equal("ACKNOWLEDGED", result.Status);
			}
		}

		[Fact]
		public void getAlarmCollectionByStatusAndSource()
		{
			// Given
			AlarmApi.create(RestRepresentationObjectMother.anAlarmRepresentationLike(
					SampleAlarmRepresentation.ALARM_REPRESENTATION)
						.withType("com_nsn_bts_TrxFaulty" + t++)
						.withStatus("CLEARED")
						.withSource(mo1).build());

			AlarmApi.create(RestRepresentationObjectMother.anAlarmRepresentationLike(
					SampleAlarmRepresentation.ALARM_REPRESENTATION)
						.withType("com_nsn_bts_TrxFaulty" + t++)
						.withStatus("ACKNOWLEDGED")
						.withSource(mo1).build());

			AlarmApi.create(RestRepresentationObjectMother.anAlarmRepresentationLike(
					SampleAlarmRepresentation.ALARM_REPRESENTATION)
						.withType("com_nsn_bts_TrxFaulty" + t++)
						.withStatus("CUSTOM")
						.withSource(mo2).build());

			// When
			AlarmFilter acknowledgedFilter = new AlarmFilter()
				.byStatus(CumulocityAlarmStatuses.valueOf("ACKNOWLEDGED")).bySource(mo1);
			AlarmCollectionRepresentation acknowledgedAlarms = AlarmApi.getAlarmsByFilter(acknowledgedFilter).get();

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
			AlarmRepresentation created = AlarmApi.create(RestRepresentationObjectMother.anAlarmRepresentationLike(
					SampleAlarmRepresentation.ALARM_REPRESENTATION)
				.withStatus("ACTIVE")
				.withSource(mo1).build());

			AlarmRepresentation returned = AlarmApi.getAlarm(created.Id);

			Assert.Equal("ACTIVE", returned.Status);
			Assert.Equal(mo1.Id, returned.Source.Id);
		}

		[Fact]
		public void shouldReturnTheUpdatedAlarm()
		{
			// Given
			AlarmRepresentation created = AlarmApi.create(RestRepresentationObjectMother.anAlarmRepresentationLike(
					SampleAlarmRepresentation.ALARM_REPRESENTATION)
					.withStatus("ACTIVE")
					.withSource(mo1).build());

			// When
			AlarmRepresentation alarm = new AlarmRepresentation();
			alarm.Status = "ACKNOWLEDGED";
			alarm.Id = created.Id;
			AlarmRepresentation updated = AlarmApi.updateAlarm(alarm);

			// Then
			Assert.Equal("ACKNOWLEDGED", updated.Status);
			Assert.Equal(mo1.Id, updated.Source.Id);
		}

		[Fact]
		public void shouldUpdateAlarm()
		{
			// Given
			AlarmRepresentation created = AlarmApi.create(RestRepresentationObjectMother.anAlarmRepresentationLike(
					SampleAlarmRepresentation.ALARM_REPRESENTATION)
						.withStatus("ACTIVE")
						.withSource(mo1).build());

			// When
			AlarmRepresentation alarm = new AlarmRepresentation();
			alarm.Status ="ACKNOWLEDGED";
			alarm.Id = created.Id;
			AlarmApi.updateAlarm(alarm);

			// Then
			AlarmRepresentation returned = AlarmApi.getAlarm(created.Id);

			Assert.Equal("ACKNOWLEDGED", returned.Status);
			Assert.Equal(mo1.Id, returned.Source.Id);
		}

		[Fact]
		public void shouldDeleteAlarmCollectionByEmptyFilter()
		{
			// Given
			for (int i = 0; i < 5; i++)
			{
				AlarmApi.create(aSampleAlarm(mo1));
				AlarmApi.create(aSampleAlarm(mo2));
				AlarmApi.create(aSampleAlarm(mo3));
			}

			AlarmFilter emptyFilter = new AlarmFilter();

			//When
			AlarmApi.deleteAlarmsByFilter(emptyFilter);

			//Then
			int resultNumber = 0;
			var pager = AlarmApi.getAlarms().get().allPages();
			foreach (AlarmRepresentation alarm in pager)
			{
				resultNumber++;
			}

			Assert.Equal(0,resultNumber);
		}

		[Fact]
		public void shouldDeleteByFilterStatus()
		{
			// Given
			AlarmApi.create(RestRepresentationObjectMother.anAlarmRepresentationLike(
					SampleAlarmRepresentation.ALARM_REPRESENTATION)
						.withType("com_nsn_bts_TrxFaulty" + t++)
						.withStatus("ACTIVE")
						.withSource(mo1).build());

			AlarmApi.create(RestRepresentationObjectMother.anAlarmRepresentationLike(
					SampleAlarmRepresentation.ALARM_REPRESENTATION)
						.withType("com_nsn_bts_TrxFaulty" + t++)
						.withStatus("ACKNOWLEDGED")
						.withSource(mo1).build());

			AlarmApi.create(RestRepresentationObjectMother.anAlarmRepresentationLike(
					SampleAlarmRepresentation.ALARM_REPRESENTATION)
						.withType("com_nsn_bts_TrxFaulty" + t++)
						.withStatus("ACKNOWLEDGED")
						.withSource(mo2).build());

			AlarmApi.create(RestRepresentationObjectMother.anAlarmRepresentationLike(
					SampleAlarmRepresentation.ALARM_REPRESENTATION)
						.withType("com_nsn_bts_TrxFaulty" + t++)
						.withStatus("CLEARED")
						.withSource(mo2).build());

			AlarmFilter alarmFilter = new AlarmFilter().byStatus(CumulocityAlarmStatuses.valueOf("ACKNOWLEDGED"));

			// When
			AlarmApi.deleteAlarmsByFilter(alarmFilter);

			// Then
			var allAlarms = AlarmApi.getAlarms().get().Alarms;

			Assert.Equal(2, allAlarms.Count);
			List<string> wantedStatus = new List<string>();
			wantedStatus.Add("ACTIVE");
			wantedStatus.Add("CLEARED");
			foreach (AlarmRepresentation alarm in allAlarms)
			{
				Assert.True(wantedStatus.Contains(alarm.Status));
			}
		}
	}
}