using System.Collections.Generic;

namespace Cumulocity.SDK.Client
{
    public interface IPagedCollectionRepresentation<T>
    {
        IEnumerable<T> AllPages();

        IEnumerable<T> Elements(int var1);
    }

}