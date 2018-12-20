using System;
using System.Collections.Generic;
using System.Text;

namespace Cumulocity.SDK.Client.Rest.Representation.DeviceBootstrap
{

	//ORIGINAL LINE: @Getter @Setter public class NewDeviceRequestRepresentation extends CustomPropertiesMapRepresentation
	public class NewDeviceRequestRepresentation : CustomPropertiesMapRepresentation
	{

		//ORIGINAL LINE: @NotNull(operation = {CREATE}) @Null(operation = {UPDATE}) private String id;
		public string id;


		//ORIGINAL LINE: @Null(operation = {CREATE}) @NotNull(operation = {UPDATE}) private String status;
		public string status;

		//ORIGINAL LINE: @Null(operation = {UPDATE}) private String tenantId;
		public string tenantId;


		//ORIGINAL LINE: @Null(operation = {UPDATE}) @Getter(onMethod = @__(@JSONProperty(ignoreIfNull = true))) private String groupId;
		public string groupId;

		//ORIGINAL LINE: @Getter(onMethod = @__(@JSONProperty(ignoreIfNull = true))) private String type;
		private string type;


		//ORIGINAL LINE: @Null(operation = {CREATE, UPDATE}) @Getter(onMethod = @__(@JSONProperty(ignoreIfNull = true))) private String owner;
		public string owner;

		//ORIGINAL LINE: @Null(operation = {CREATE, UPDATE}) @Getter(onMethod = @__({@JSONProperty(ignoreIfNull = true), @JSONConverter(type = DateTimeConverter.class)})) private DateTime creationTime;
		public DateTime creationTime;

	}
}
