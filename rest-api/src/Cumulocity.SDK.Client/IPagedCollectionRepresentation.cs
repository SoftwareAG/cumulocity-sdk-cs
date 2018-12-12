using System.Collections.Generic;

namespace Cumulocity.SDK.Client
{
    public interface IPagedCollectionRepresentation<T>
    {
        IEnumerable<T> allPages();

        IEnumerable<T> elements(int var1);
    }

}