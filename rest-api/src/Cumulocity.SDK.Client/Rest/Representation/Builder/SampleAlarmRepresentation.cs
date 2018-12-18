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
using static System.DateTime;

namespace Cumulocity.SDK.Client.Rest.Representation.Builder
{
	public abstract class SampleAlarmRepresentation : AbstractSampleAlarmRepresentation
	{
		public const string ALARM_SEVERITY = "Alarm severity #";

		public const string ALARM_STATUS = "Alarm status #";

		public const string ALARM_TYPE = "Alarm Type #";

		public const string ALARM_TEXT = "Alarm Text #";

		static SampleAlarmRepresentation()
		{
			ALARM_REPRESENTATION = () =>
				RestRepresentationObjectBuilder.anAlarmRepresentation()
					.withSeverity(ALARM_SEVERITY)
					.withStatus(ALARM_STATUS)
					.withType(ALARM_TYPE)
					.withText(ALARM_TEXT)
					.withDateTime(UtcNow);
		}

		public static Func<AlarmRepresentationBuilder> ALARM_REPRESENTATION { get; }
	}
}