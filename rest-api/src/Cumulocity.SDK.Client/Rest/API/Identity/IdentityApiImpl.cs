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

#endregion Cumulocity GmbH

using Cumulocity.SDK.Client.Rest.Model;
using Cumulocity.SDK.Client.Rest.Model.Idtype;
using Cumulocity.SDK.Client.Rest.Representation.Identity;
using System.Collections.Generic;

namespace Cumulocity.SDK.Client.Rest.API.Identity
{
	public class IdentityApiImpl : IIdentityApi
	{
		private const string GLOBAL_ID = "globalId";

		private const string EXTERNAL_ID = "externaId";

		private const string TYPE = "type";

		private readonly int pageSize;

		private readonly RestConnector restConnector;

		private readonly TemplateUrlParser templateUrlParser;

		public IdentityApiImpl(RestConnector restConnector, TemplateUrlParser templateUrlParser,
			IdentityRepresentation identityRepresentation, int pageSize)
		{
			this.restConnector = restConnector;
			this.templateUrlParser = templateUrlParser;
			IdentityRepresentation = identityRepresentation;
			this.pageSize = pageSize;
		}
		private IdentityRepresentation IdentityRepresentation { get; }

		public ExternalIDRepresentation create(ExternalIDRepresentation representation)
		{
			if (representation == null || representation.ManagedObject == null ||
				representation.ManagedObject.Id == null) throw new SDKException("Cannot determine global id value");

			IDictionary<string, string> filter = new Dictionary<string, string>();
			filter[GLOBAL_ID] = representation.ManagedObject.Id.Value;
			var path = templateUrlParser.replacePlaceholdersWithParams(IdentityRepresentation.ExternalIdsOfGlobalId,
				filter);
			return restConnector.Post(path, IdentityMediaType.EXTERNAL_ID, representation);
		}

		public ExternalIDRepresentation getExternalId(ID extId)
		{
			if (extId == null || extId.Value == null || extId.Type == null)
				throw new SDKException("XtId without value/type or null");

			IDictionary<string, string> filter = new Dictionary<string, string>();
			filter[TYPE] = extId.Type;
			filter[EXTERNAL_ID] = extId.Value;
			var extIdUrl = templateUrlParser.replacePlaceholdersWithParams(IdentityRepresentation.ExternalId, filter);
			return restConnector.Get<ExternalIDRepresentation>(extIdUrl, IdentityMediaType.EXTERNAL_ID,
				typeof(ExternalIDRepresentation));
		}

		public IExternalIDCollection getExternalIdsOfGlobalId(GId gid)
		{
			if (gid == null || gid.Value == null)
			{
				throw new SDKException("Cannot determine global id value");
			}

			IDictionary<string, string> filter = new Dictionary<string, string>();
			filter[GLOBAL_ID] = gid.Value;
			string uri = templateUrlParser.replacePlaceholdersWithParams(IdentityRepresentation.ExternalIdsOfGlobalId, filter);
			return new ExternalIDCollectionImpl(restConnector, uri, pageSize);
		}

		public void deleteExternalId(ExternalIDRepresentation externalId)
		{
			IDictionary<string, string> filter = new Dictionary<string, string>();
			filter[TYPE] = externalId.Type;
			filter[EXTERNAL_ID] = externalId.ExternalId;
			var extIdUrl = templateUrlParser.replacePlaceholdersWithParams(IdentityRepresentation.ExternalId, filter);

			restConnector.Delete(extIdUrl);
		}
	}
}