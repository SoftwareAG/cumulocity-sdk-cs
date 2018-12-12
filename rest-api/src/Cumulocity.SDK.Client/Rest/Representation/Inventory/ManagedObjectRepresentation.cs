using System;
using Cumulocity.SDK.Client.Rest.Model.Idtype;
using Cumulocity.SDK.Client.Rest.Utils;
using Newtonsoft.Json;

namespace Cumulocity.SDK.Client.Rest.Representation.Inventory
{
    public class ManagedObjectRepresentation : AbstractExtensibleRepresentation, IBaseResourceRepresentationWithId
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public virtual string Type { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public virtual string Name { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public virtual string Owner { get; set; }
        
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public virtual DateTime? CreationTime { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public virtual DateTime? LastUpdated { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public virtual ManagedObjectReferenceCollectionRepresentation ChildDevices { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public virtual ManagedObjectReferenceCollectionRepresentation ChildAssets { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public virtual ManagedObjectReferenceCollectionRepresentation ChildAdditions { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public virtual ManagedObjectReferenceCollectionRepresentation DeviceParents { get; set; }
        
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public virtual ManagedObjectReferenceCollectionRepresentation AssetParents { get; set; }
        
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public virtual ManagedObjectReferenceCollectionRepresentation AdditionParents { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(GidConverter))]
        public virtual GId Id { get; set; }
    }
}