﻿using Cumulocity.SDK.Client.Rest.Model.Idtype;
using Cumulocity.SDK.Client.Rest.Representation.Alarm;
using System;
using System.Threading.Tasks;

namespace Cumulocity.SDK.Client.Rest.API.Alarm
{
	/// <summary>
	///     API for creating, updating and retrieving alarms from the platform.
	/// </summary>
	public interface IAlarmApi
	{
		/// <summary>
		///     Gets an alarm by id
		/// </summary>
		/// <param name="gid"> id of the alarm to search for </param>
		/// <returns> the alarm with the given id </returns>
		/// <exception cref="SDKException"> if the alarm is not found or if the query failed </exception>
		AlarmRepresentation GetAlarm(GId gid);

		/// <summary>
		///     Creates an alarm in the platform. The id of the alarm must not be Set, since it will be generated by the platform
		/// </summary>
		/// <param name="alarm"> alarm to be created </param>
		/// <returns> the created alarm with the generated id </returns>
		/// <exception cref="SDKException"> if the alarm could not be created </exception>
		AlarmRepresentation Create(AlarmRepresentation alarm);

		/// <summary>
		/// Creates an alarm in the platform. Immediate response is available through the Task object.
		/// In case of lost connection, buffers data in persistence provider.
		/// </summary>
		/// <param name="alarm"> alarm to be created </param>
		/// <returns> the created alarm with the generated id </returns>
		/// <exception cref="SDKException"> if the alarm could not be created </exception>
		Task<AlarmRepresentation> CreateAsync(AlarmRepresentation alarm);

		/// <summary>
		/// Updates an alarm in the platform.
		/// The alarm to be updated is identified by the id within the given alarm.
		/// </summary>
		/// <param name="alarm"> to be updated </param>
		/// <returns> the updated alarm </returns>
		/// <exception cref="SDKException"> if the alarm could not be updated </exception>
		AlarmRepresentation Update(AlarmRepresentation alarm);

		/// <summary>
		/// Gets all alarms from the platform
		/// </summary>
		/// <returns> collection of alarms with paging functionality </returns>
		/// <exception cref="SDKException"> if the query failed </exception>
		IAlarmCollection GetAlarms();

		/// <summary>
		/// Gets alarms from the platform based on the specified filter
		/// </summary>
		/// <param name="filter"> the filter criteria(s) </param>
		/// <returns> collection of alarms matched by the filter with paging functionality </returns>
		/// <exception cref="SDKException"> if the query failed </exception>
		IAlarmCollection GetAlarmsByFilter(AlarmFilter filter);

		/// <summary>
		/// Delete alarms from the platform based on the specified filter
		/// </summary>
		/// <param name="filter"> the filter criteria(s) </param>
		/// <exception cref="SDKException"> if the query failed </exception>
		void DeleteAlarmsByFilter(AlarmFilter filter);

		/// <summary>
		/// Updates an alarm in the platform.
		/// The alarm to be updated is identified by the id within the given alarm.
		/// </summary>
		/// <param name="alarm"> to be updated </param>
		/// <returns> the updated alarm </returns>
		/// <exception cref="SDKException"> if the alarm could not be updated </exception>

		[Obsolete]
		AlarmRepresentation UpdateAlarm(AlarmRepresentation alarm);
	}
}