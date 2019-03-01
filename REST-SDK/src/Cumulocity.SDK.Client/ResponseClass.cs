using System;
using Cumulocity.SDK.Client.Rest.Representation;

namespace Cumulocity.SDK.Client
{
    public abstract class ResponseClass<T> where T : BaseCollectionRepresentation<T>
    {
        public abstract T GetResponseClass { get; set; }
        public abstract bool EqualsResponseClass(T response);
        public abstract int GetHashCodeResponseClass();
    }
}