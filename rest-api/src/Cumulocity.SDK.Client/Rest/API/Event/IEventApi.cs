﻿#region Cumulocity GmbH

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

using System.Threading.Tasks;
using Cumulocity.SDK.Client.Rest.Model.Idtype;
using Cumulocity.SDK.Client.Rest.Representation.Event;

namespace Cumulocity.SDK.Client.Rest.API.Event
{
	/// <summary>
	/// API for creating, deleting and retrieving events from the platform.
	/// </summary>
	public interface IEventApi
	{

		/// <summary>
		/// Gets event by id
		/// </summary>
		/// <param name="gid"> id of the event to search for </param>
		/// <returns> the event with the given id </returns>
		/// <exception cref="SDKException"> if the event is not found or if the query failed </exception>
		EventRepresentation GetEvent(GId gid);

		/// <summary>
		/// Creates event in the platform. The id of the event must not be Set, since it will be generated by the platform
		/// </summary>
		/// <param name="event"> event to be created </param>
		/// <returns> the created event with the generated id </returns>
		/// <exception cref="SDKException"> if the event could not be created </exception>
		EventRepresentation Create(EventRepresentation @event);

		/// <summary>
		/// Creates event in the platform. Immediate response is available through the Future object. 
		/// In case of lost connection, buffers data in persistence provider. 
		/// </summary>
		/// <param name="event"> event to be created </param>
		/// <returns> the created event with the generated id </returns>
		/// <exception cref="SDKException"> if the event could not be created </exception>
		Task<EventRepresentation> CreateAsync(EventRepresentation @event);

		/// <summary>
		/// Deletes event from the platform.
		/// The event to be deleted is identified by the id within the given event.
		/// </summary>
		/// <param name="event"> to be deleted </param>
		/// <exception cref="SDKException"> if the event could not be deleted </exception>
		void Delete(EventRepresentation @event);

		/// <summary>
		/// Deletes events from the platform based on the specified filter
		/// </summary>
		/// <param name="filter"> the filter criteria(s) </param>
		/// <exception cref="IllegalArgumentException"> </exception>
		/// <exception cref="SDKException"> if the event(s) could not be deleted </exception>
		void DeleteEventsByFilter(EventFilter filter);

		/// <summary>
		/// Gets the all the event in the platform
		/// </summary>
		/// <returns> collection of events with paging functionality </returns>
		/// <exception cref="SDKException"> if the query failed </exception>
		IEventCollection Events { get; }

		/// <summary>
		/// Gets the events from the platform based on specified filter
		/// </summary>
		/// <param name="filter"> the filter criteria(s) </param>
		/// <returns> collection of events matched by the filter with paging functionality </returns>
		/// <exception cref="SDKException"> if the query failed </exception>
		IEventCollection GetEventsByFilter(EventFilter filter);

		/// <summary>
		/// This Update the event in the platform. Cannot Update the ID.
		/// </summary>
		/// <param name="eventRepresentation"> </param>
		/// <returns> The created event </returns>
		/// <exception cref="SDKException"> if the event could not be updated </exception>
		EventRepresentation Update(EventRepresentation eventRepresentation);
	}

}