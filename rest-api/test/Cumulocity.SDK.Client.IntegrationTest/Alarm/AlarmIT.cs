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
using Cumulocity.SDK.Client.Rest.API.Alarm;
using Cumulocity.SDK.Client.Rest.Representation.Alarm;
using Cumulocity.SDK.Client.Rest.Representation.Builder;
using Cumulocity.SDK.Client.Rest.Representation.Inventory;
using Xunit;

namespace Cumulocity.SDK.Client.IntegrationTest.Alarm
{
	public class AlarmIT : IClassFixture<AlarmFixture>, IDisposable
	{
		private ManagedObjectRepresentation mo1;
		private ManagedObjectRepresentation mo2;
		private ManagedObjectRepresentation mo3;
		private int t = 0;

		public AlarmIT(AlarmFixture fixture)
		{
			AlarmApi = fixture.platform.AlarmApi;

			mo1 = fixture.platform.InventoryApi.create(aSampleMo().withName("MO" + 1).build());
			mo2 = fixture.platform.InventoryApi.create(aSampleMo().withName("MO" + 2).build());
			mo3 = fixture.platform.InventoryApi.create(aSampleMo().withName("MO" + 3).build());
		}

		public IAlarmApi AlarmApi { get; set; }

		public void Dispose()
		{
		}
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
				.withSeverity("major")
				.withSource(source)
				.withText("Alarm for mo")
				.withDateTime(DateTime.UtcNow).build();
		}

		[Fact]
		public void shouldHaveIdAfterCreateAlarm() 
		{
			// Given
			AlarmRepresentation rep = aSampleAlarm(mo1);
			// When
			AlarmRepresentation created = AlarmApi.create(rep);
			// Then
			Assert.NotNull(created.Id);
	}
}
}