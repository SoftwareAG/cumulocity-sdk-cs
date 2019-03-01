using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Cumulocity.SDK.Client.Rest.Representation.DeviceBootstrap
{

	//ORIGINAL LINE: @Getter @Setter public class NewDeviceRequestRepresentation extends CustomPropertiesMapRepresentation
	public class NewDeviceRequestRepresentation : CustomPropertiesMapRepresentation
	{

		//ORIGINAL LINE: @NotNull(operation = {CREATE}) @Null(operation = {UPDATE}) private String id;
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string Id { get; set; }


		//ORIGINAL LINE: @Null(operation = {CREATE}) @NotNull(operation = {UPDATE}) private String status;
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string Status { get; set; }

		//ORIGINAL LINE: @Null(operation = {UPDATE}) private String tenantId;
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string TenantId { get; set; }


		//ORIGINAL LINE: @Null(operation = {UPDATE}) @Getter(onMethod = @__(@JSONProperty(ignoreIfNull = true))) private String groupId;
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string GroupId { get; set; }

		//ORIGINAL LINE: @Getter(onMethod = @__(@JSONProperty(ignoreIfNull = true))) private String type;
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string Type { get; set; }

		//ORIGINAL LINE: @Null(operation = {CREATE, UPDATE}) @Getter(onMethod = @__(@JSONProperty(ignoreIfNull = true))) private String owner;
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string Owner { get; set; }

		//ORIGINAL LINE: @Null(operation = {CREATE, UPDATE}) @Getter(onMethod = @__({@JSONProperty(ignoreIfNull = true), @JSONConverter(type = DateTimeConverter.class)})) private DateTime creationTime;
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public DateTime? CreationTime { get; set; }

	}
}
