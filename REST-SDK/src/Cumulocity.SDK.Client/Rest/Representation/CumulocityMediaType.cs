using Cumulocity.SDK.Client.Rest.Model;
using System;
using System.Collections.Generic;

namespace Cumulocity.SDK.Client.Rest.Representation
{
	public class CumulocityMediaType : MediaType
	{
		protected const string VND_COM_NSN_CUMULOCITY = "vnd.com.nsn.cumulocity.";

		public const string APPLICATION_JSON_STREAM_TYPE = "application/json-stream";

		public static readonly string APPLICATION_VND_COM_NSN_CUMULOCITY = "application/" + VND_COM_NSN_CUMULOCITY;

		public static readonly MediaType APPLICATION_JSON_STREAM = new MediaType("application", "json-stream");

		public static readonly CumulocityMediaType ERROR_MESSAGE = new CumulocityMediaType("error");

		public static readonly string VND_COM_NSN_CUMULOCITY_CHARSET = "charset=" + CumulocityCharset_Fields.CHARSET;

		public static readonly string VND_COM_NSN_CUMULOCITY_VERSION =
			"ver=" + CumulocityVersionParameter_Fields.VERSION;

		public static readonly string VND_COM_NSN_CUMULOCITY_PARAMS =
			VND_COM_NSN_CUMULOCITY_CHARSET + ";" + VND_COM_NSN_CUMULOCITY_VERSION;

		public static readonly string ERROR_MESSAGE_TYPE =
			APPLICATION_VND_COM_NSN_CUMULOCITY + "error+json;" + VND_COM_NSN_CUMULOCITY_PARAMS;

		[Obsolete]
		private static readonly Dictionary<string, string> MediaTypeParams =
			new Dictionary<string, string> { { "ver", "0.9" }, { "charset", CumulocityCharset_Fields.CHARSET } };

		private MediaType parametrizedMediaTypeObject;

		public CumulocityMediaType()
		{
		}

		public CumulocityMediaType(string type, string subtype) : base(type, subtype)
		{
		}

		public CumulocityMediaType(string entity) : base("application",
			VND_COM_NSN_CUMULOCITY + entity + "+json;" + VND_COM_NSN_CUMULOCITY_PARAMS)
		{
		}

		public CumulocityMediaType(string type, string subtype, IDictionary<string, string> parameters) : base(type,
			subtype, parameters)
		{
		}

		[Obsolete]
		public MediaType withParams()
		{
			if (parametrizedMediaTypeObject == null)
				parametrizedMediaTypeObject = new MediaType(Type, Subtype, MediaTypeParams);

			return parametrizedMediaTypeObject;
		}
	}
}