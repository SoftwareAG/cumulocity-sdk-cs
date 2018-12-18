using System;
using System.Collections.Generic;


namespace Cumulocity.SDK.Client.Rest.Representation.Alarm
{
	public class AlarmsApiRepresentation : AbstractExtensibleRepresentation
	{

		private AlarmCollectionRepresentation alarms;

		private string alarmsForStatus;

		private string alarmsForSource;

		private string alarmsForSourceAndStatus;

		private string alarmsForTime;

		private string alarmsForStatusAndTime;

		private string alarmsForSourceAndTime;

		private string alarmsForSourceAndStatusAndTime;

		public virtual AlarmCollectionRepresentation Alarms
		{
			get
			{
				return alarms;
			}
			set
			{
				this.alarms = value;
			}
		}


		public virtual string AlarmsForStatus
		{
			get
			{
				return alarmsForStatus;
			}
			set
			{
				this.alarmsForStatus = value;
			}
		}


		public virtual string AlarmsForSource
		{
			get
			{
				return alarmsForSource;
			}
			set
			{
				this.alarmsForSource = value;
			}
		}


		public virtual string AlarmsForSourceAndStatus
		{
			get
			{
				return alarmsForSourceAndStatus;
			}
			set
			{
				this.alarmsForSourceAndStatus = value;
			}
		}


		public virtual string AlarmsForTime
		{
			get
			{
				return alarmsForTime;
			}
			set
			{
				this.alarmsForTime = value;
			}
		}


		public virtual string AlarmsForStatusAndTime
		{
			get
			{
				return alarmsForStatusAndTime;
			}
			set
			{
				this.alarmsForStatusAndTime = value;
			}
		}


		public virtual string AlarmsForSourceAndTime
		{
			get
			{
				return alarmsForSourceAndTime;
			}
			set
			{
				this.alarmsForSourceAndTime = value;
			}
		}


		public virtual string AlarmsForSourceAndStatusAndTime
		{
			get
			{
				return alarmsForSourceAndStatusAndTime;
			}
			set
			{
				this.alarmsForSourceAndStatusAndTime = value;
			}
		}

		//ORIGINAL LINE: @JSONProperty(ignore = true) public List<String> getURITemplates()
		public virtual IList<string> URITemplates
		{
			get
			{
				IList<string> uriTemplates = new List<string>();
				uriTemplates.Add(this.AlarmsForSource);
				uriTemplates.Add(this.AlarmsForSourceAndStatus);
				uriTemplates.Add(this.AlarmsForSourceAndStatusAndTime);
				uriTemplates.Add(this.AlarmsForSourceAndTime);
				uriTemplates.Add(this.AlarmsForStatus);
				uriTemplates.Add(this.AlarmsForStatusAndTime);
				uriTemplates.Add(this.AlarmsForTime);
				return uriTemplates;
			}
		}

	}
}
