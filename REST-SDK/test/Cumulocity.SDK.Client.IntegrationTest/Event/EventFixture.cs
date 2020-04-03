using Cumulocity.SDK.Client.HelperTest;
using Cumulocity.SDK.Client.Rest;
using Cumulocity.SDK.Client.Rest.Model.Authentication;
using System;
using System.Collections.Generic;
using Cumulocity.SDK.Client.Rest.Representation.Event;

namespace Cumulocity.SDK.Client.IntegrationTest.Event
{
	public class EventFixture : IDisposable
	{
		public PlatformImpl platform;

		public EventFixture()
		{
			var secretRevealer = TestHelper.GetApplicationConfiguration(Environment.CurrentDirectory);

			PlatformImpl platform = new PlatformImpl(secretRevealer.Reveal().platformurl,
				new CumulocityCredentials(secretRevealer.Reveal().user, secretRevealer.Reveal().pass));

			this.platform = platform;
		}

		public void Dispose()
		{
			IList<EventRepresentation> eventsOn1stPage = getEventsFrom1stPage();
			while (eventsOn1stPage.Count > 0)
			{
				deleteMOs(eventsOn1stPage);
				eventsOn1stPage = getEventsFrom1stPage();
			}
		}
		private void deleteMOs(IList<EventRepresentation> mosOn1stPage)
		{
			foreach (EventRepresentation e in mosOn1stPage)
			{
				this.platform.EventApi.Delete(e);
			}
		}

		private IList<EventRepresentation> getEventsFrom1stPage()
		{
			return this.platform.EventApi.Events.GetFirstPage().Events;
		}
	}
}