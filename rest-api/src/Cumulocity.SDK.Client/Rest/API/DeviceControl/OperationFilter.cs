using Cumulocity.SDK.Client.Rest.Model.Operation;
using Cumulocity.SDK.Client.Rest.Model.util;
using System;

namespace Cumulocity.SDK.Client.Rest.API.DeviceControl
{
	/// <summary>
	/// A filter to be used in operation queries.
	/// The setter (by*) methods return the filter itself to provide chaining:
	/// {@code OperationFilter filter = new OperationFilter().ByStatus(status).ByDevice(deviceId);}
	/// </summary>
	public class OperationFilter : Filter
	{
		private string fragmentType;

		private string status;

		private string deviceId;

		private string agentId;

		/// <summary>
		/// Specifies the {@code fragmentType} query parameter
		/// </summary>
		/// <param name="fragmentClass"> the class representation of the type of the operation(s) </param>
		/// <returns> the operation filter with {@code fragmentType} set </returns>
		public virtual OperationFilter ByFragmentType(Type fragmentClass)
		{
			this.fragmentType = ExtensibilityConverter.ClassToStringRepresentation(fragmentClass);
			return this;
		}

		public virtual OperationFilter ByFragmentType(string fragmentType)
		{
			this.fragmentType = fragmentType;
			return this;
		}

		/// <summary>
		/// Specifies the {@code status} query parameter
		/// </summary>
		/// <param name="status"> status of the operation(s) </param>
		/// <returns> the operation filter with {@code status} set </returns>
		public virtual OperationFilter ByStatus(OperationStatus status)
		{
			this.status = status.ToString();
			return this;
		}

		/// <summary>
		/// Specifies the {@code deviceId} query parameter
		/// </summary>
		/// <param name="deviceId"> id of the device associated with the the operations(s) </param>
		/// <returns> the operation filter with {@code deviceId} set </returns>
		public virtual OperationFilter ByDevice(string deviceId)
		{
			this.deviceId = deviceId;
			return this;
		}

		/// <summary>
		/// Specifies the {@code agentId} query parameter
		/// </summary>
		/// <param name="agentId"> id of the agent associated with the the operations(s) </param>
		/// <returns> the operation filter with {@code agentId} set </returns>
		public virtual OperationFilter ByAgent(string agentId)
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