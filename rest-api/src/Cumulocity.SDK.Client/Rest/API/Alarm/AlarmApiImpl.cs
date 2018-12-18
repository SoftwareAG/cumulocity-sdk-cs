using System;
using System.Collections.Generic;
using System.Text;
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

		//ORIGINAL LINE: private AlarmsApiRepresentation getAlarmsApiRepresentation() throws SDKException
		private AlarmsApiRepresentation AlarmsApiRepresentation
		{
			get
			{
				return alarmsApiRepresentation;
			}
		}

		//ORIGINAL LINE: @Override public AlarmRepresentation getAlarm(GId alarmId) throws SDKException
		public  AlarmRepresentation getAlarm(GId alarmId)
		{
			string url = SelfUri + "/" + alarmId.Value;
			return restConnector.Get<AlarmRepresentation>(url, AlarmMediaType.ALARM, typeof(AlarmRepresentation));
		}


		//ORIGINAL LINE: @Override @Deprecated public AlarmRepresentation updateAlarm(AlarmRepresentation alarmToUpdate) throws SDKException
		[Obsolete]
		public  AlarmRepresentation updateAlarm(AlarmRepresentation alarmToUpdate)
		{
			return update(alarmToUpdate);
		}

		//ORIGINAL LINE: @Override public AlarmRepresentation update(AlarmRepresentation alarmToUpdate) throws SDKException
		public  AlarmRepresentation update(AlarmRepresentation alarmToUpdate)
		{
			string url = SelfUri + "/" + alarmToUpdate.Id.Value;
			return restConnector.PutWithoutId(url, AlarmMediaType.ALARM, prepareForUpdate(alarmToUpdate));
		}

		public IAlarmCollection alarms { get; }

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

		//ORIGINAL LINE: @Override public AlarmCollection getAlarms() throws SDKException
		public  IAlarmCollection Alarms
		{
			get
			{
				string url = SelfUri;
				return new AlarmCollectionImpl(restConnector, url, pageSize);
			}
		}

		//ORIGINAL LINE: private String getSelfUri() throws SDKException
		private string SelfUri
		{
			get
			{
				return AlarmsApiRepresentation.Alarms.Self;
			}
		}

		//ORIGINAL LINE: @Override public AlarmRepresentation create(AlarmRepresentation representation) throws SDKException
		public  AlarmRepresentation create(AlarmRepresentation representation)
		{
			return restConnector.Post(SelfUri, AlarmMediaType.ALARM, representation);
		}

		//ORIGINAL LINE: @Override public Future createAsync(AlarmRepresentation representation) throws SDKException
		//public override Future createAsync(AlarmRepresentation representation)
		//{
		//	return restConnector.postAsync(SelfUri, AlarmMediaType.ALARM, representation);
		//}

		//ORIGINAL LINE: @Override public AlarmCollection getAlarmsByFilter(AlarmFilter filter) throws SDKException
		public IAlarmCollection getAlarmsByFilter(AlarmFilter filter)
		{
			if (filter == null)
			{
				return Alarms;
			}
			IDictionary<string, string> @params = filter.QueryParams;
			return new AlarmCollectionImpl(restConnector, urlProcessor.replaceOrAddQueryParam(SelfUri, @params), pageSize);
		}

		//ORIGINAL LINE: @Override public void deleteAlarmsByFilter(AlarmFilter filter) throws IllegalArgumentException, SDKException
		public  void deleteAlarmsByFilter(AlarmFilter filter)
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
