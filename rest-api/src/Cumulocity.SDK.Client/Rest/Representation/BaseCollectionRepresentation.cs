using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Cumulocity.SDK.Client.Rest.Representation
{
    public abstract class BaseCollectionRepresentation<T> : BaseResourceRepresentation ,IEnumerable<T>
    {
        //ORIGINAL LINE: @JSONProperty(value = "statistics", ignoreIfNull = true) public PageStatisticsRepresentation getPageStatistics()
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore,PropertyName =  "statistics")]
        public virtual PageStatisticsRepresentation PageStatistics { get; set; }

       //ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public String getPrev()
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public virtual string Prev { get; set; }

        //ORIGINAL LINE: @JSONProperty(ignoreIfNull = true) public String getNext()
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public virtual string Next { get; set; }
       
        public abstract IEnumerator<T> GetEnumerator();
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }
}