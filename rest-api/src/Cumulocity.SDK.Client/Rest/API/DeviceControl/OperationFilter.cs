using System;
using System.Collections.Generic;
using System.Text;
using Cumulocity.SDK.Client.Rest.Model.Operation;
using Cumulocity.SDK.Client.Rest.Model.util;

namespace Cumulocity.SDK.Client.Rest.API.DeviceControl
{
	/// <summary>
	/// A filter to be used in operation queries.
	/// The setter (by*) methods return the filter itself to provide chaining:
	/// {@code OperationFilter filter = new OperationFilter().byStatus(status).byDevice(deviceId);}
	/// </summary>
	public class OperationFilter : Filter
	{

		//ORIGINAL LINE: @ParamSource private String fragmentType;
		private string fragmentType;

		//ORIGINAL LINE: @ParamSource private String status;
		private string status;

		//ORIGINAL LINE: @ParamSource private String deviceId;
		private string deviceId;

		//ORIGINAL LINE: @ParamSource private String agentId;
		private string agentId;

		/// <summary>
		/// Specifies the {@code fragmentType} query parameter
		/// </summary>
		/// <param name="fragmentClass"> the class representation of the type of the operation(s) </param>
		/// <returns> the operation filter with {@code fragmentType} set </returns>
		public virtual OperationFilter byFragmentType(Type fragmentClass)
		{
			this.fragmentType = ExtensibilityConverter.ClassToStringRepresentation(fragmentClass);
			return this;
		}

		public virtual OperationFilter byFragmentType(string fragmentType)
		{
			this.fragmentType = fragmentType;
			return this;
		}

		/// <summary>
		/// Specifies the {@code status} query parameter
		/// </summary>
		/// <param name="status"> status of the operation(s) </param>
		/// <returns> the operation filter with {@code status} set </returns>
		public virtual OperationFilter byStatus(OperationStatus status)
		{
			this.status = status.ToString();
			return this;
		}

		/// <summary>
		/// Specifies the {@code deviceId} query parameter
		/// </summary>
		/// <param name="deviceId"> id of the device associated with the the operations(s) </param>
		/// <returns> the operation filter with {@code deviceId} set </returns>
		public virtual OperationFilter byDevice(string deviceId)
		{
			this.deviceId = deviceId;
			return this;
		}

		/// <summary>
		/// Specifies the {@code agentId} query parameter
		/// </summary>
		/// <param name="agentId"> id of the agent associated with the the operations(s) </param>
		/// <returns> the operation filter with {@code agentId} set </returns>
		public virtual OperationFilter byAgent(string agentId)
		{
			this.agentId = agentId;
			return this;
		}

		/// <returns> the {@code status} parameter of the query </returns>
		public virtual string Status
		{
			get
			{
				return status;
			}
		}

		/// <returns> the {@code deviceId} parameter of the query </returns>
		public virtual string Device
		{
			get
			{
				return deviceId;
			}
		}

		/// <returns> the {@code agentId} parameter of the query </returns>
		public virtual string Agent
		{
			get
			{
				return agentId;
			}
		}

		/// <returns> the {@code fragmentType} parameter of the query </returns>
		public virtual string FragmentType
		{
			get
			{
				return fragmentType;
			}
		}
	}
}
