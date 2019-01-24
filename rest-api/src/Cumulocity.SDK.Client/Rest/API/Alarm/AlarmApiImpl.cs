using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cumulocity.SDK.Client.Rest.Model.Idtype;
using Cumulocity.SDK.Client.Rest.Representation.Alarm;

namespace Cumulocity.SDK.Client.Rest.API.Alarm
{
	public class AlarmApiImpl: IAlarmApi
	{
		private readonly RestConnector restConnector;

		private readonly int pageSize;

		private AlarmsApiRepresentation alarmsApiRepresentation;

		private UrlProcessor urlProcessor;

		public AlarmApiImpl(RestConnector restConnector, UrlProcessor urlProcessor, AlarmsApiRepresentation alarmsApiRepresentation, int pageSize)
		{
			this.restConnector = restConnector;
			this.urlProcessor = urlProcessor;
			this.alarmsApiRepresentation = alarmsApiRepresentation;
			this.pageSize = pageSize;
		}

		private AlarmsApiRepresentation AlarmsApiRepresentation
		{
			get
			{
				return alarmsApiRepresentation;
			}
		}

		public  AlarmRepresentation GetAlarm(GId alarmId)
		{
			string url = $"{SelfUri}/{alarmId.Value}";
			return restConnector.Get<AlarmRepresentation>(url, AlarmMediaType.ALARM, typeof(AlarmRepresentation));
		}

		[Obsolete]
		public  AlarmRepresentation UpdateAlarm(AlarmRepresentation alarmToUpdate)
		{
			return Update(alarmToUpdate);
		}

		public Task<AlarmRepresentation> CreateAsync(AlarmRepresentation alarm)
		{
			return restConnector.PostAsync(SelfUri, AlarmMediaType.ALARM, alarm);
		}

		public  AlarmRepresentation Update(AlarmRepresentation alarmToUpdate)
		{
			string url = $"{SelfUri}/{alarmToUpdate.Id.Value}";
			return restConnector.PutWithoutId(url, AlarmMediaType.ALARM, prepareForUpdate(alarmToUpdate));
		}

		public IAlarmCollection GetAlarms()
		{
				string url = SelfUri;
				return new AlarmCollectionImpl(restConnector, url, pageSize);			
		}

		private AlarmRepresentation prepareForUpdate(AlarmRepresentation alarm)
		{
			AlarmRepresentation updatable = new AlarmRepresentation();
			updatable.Status = alarm.Status;
			updatable.Severity = alarm.Severity;
			updatable.Source = alarm.Source;
			updatable.Text = alarm.Text;
			updatable.Attrs = alarm.Attrs;
			return updatable;
		}

		public  IAlarmCollection Alarms
		{
			get
			{
				string url = SelfUri;
				return new AlarmCollectionImpl(restConnector, url, pageSize);
			}
		}

		private string SelfUri
		{
			get
			{
				return AlarmsApiRepresentation.Alarms.Self;
			}
		}

		public  AlarmRepresentation Create(AlarmRepresentation representation)
		{
			return restConnector.Post(SelfUri, AlarmMediaType.ALARM, representation);
		}

		public Task<AlarmRepresentation> createAsync(AlarmRepresentation representation)
		{
			return restConnector.PostAsync(SelfUri, AlarmMediaType.ALARM, representation);
		}

		public IAlarmCollection GetAlarmsByFilter(AlarmFilter filter)
		{
			if (filter == null)
			{
				return Alarms;
			}
			IDictionary<string, string> @params = filter.QueryParams;
			return new AlarmCollectionImpl(restConnector, urlProcessor.replaceOrAddQueryParam(SelfUri, @params), pageSize);
		}

		public  void DeleteAlarmsByFilter(AlarmFilter filter)
		{

			if (filter == null)
			{
				throw new System.ArgumentException("Alarm filter is null");
			}
			else
			{
				IDictionary<string, string> @params = filter.QueryParams;
				restConnector.Delete(urlProcessor.replaceOrAddQueryParam(SelfUri, @params));
			}
		}

	}
}
