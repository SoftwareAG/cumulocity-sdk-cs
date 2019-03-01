using System;
using System.Collections.Generic;
using System.Text;

namespace Cumulocity.SDK.Client.Rest.Model.Measurement
{
	/// <summary>
	/// The state of a measurement.
	/// Can be the original reading, a validated reading or an interpolated value.
	/// 
	/// @author pitchfor
	/// 
	/// </summary>
	public enum StateType
	{
		ORIGINAL,
		VALIDATED,
		INTERPOLATED
	}

}
