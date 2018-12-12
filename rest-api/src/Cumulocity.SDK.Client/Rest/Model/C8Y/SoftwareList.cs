using System.Collections.Generic;
using Cumulocity.SDK.Client.Rest.Utils;

namespace Cumulocity.SDK.Client.Rest.Model.C8Y
{
    [PackageName("c8y_SoftwareList")]
    public class SoftwareList : List<SoftwareListEntry>
    {
        private const long serialVersionUID = -2808870422985393963L;

////JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
////ORIGINAL LINE: @SuppressWarnings({ "unchecked", "rawtypes" }) @Override public SoftwareListEntry get(int index)
//        public override SoftwareListEntry get(int index)
//        {
//            object o = base[index];
//            if (o is System.Collections.IDictionary)
//            {
//                return new SoftwareListEntry((System.Collections.IDictionary) base[index]);
//            }
//
//            return (SoftwareListEntry) o;
//        }
//
//        public override IEnumerator<SoftwareListEntry> iterator()
//        {
//            return new SoftwareListIterator(this, base.GetEnumerator());
//        }
//
//        private class SoftwareListIterator : IEnumerator<SoftwareListEntry>
//        {
//            private readonly SoftwareList outerInstance;
//
//
//            internal IEnumerator<SoftwareListEntry> iterator;
//
//            public SoftwareListIterator(SoftwareList outerInstance, IEnumerator<SoftwareListEntry> iterator)
//            {
//                this.outerInstance = outerInstance;
//                this.iterator = iterator;
//            }
//
//            public override bool hasNext()
//            {
////JAVA TO C# CONVERTER TODO TASK: Java iterators are only converted within the context of 'while' and 'for' loops:
//                return iterator.hasNext();
//            }
//
//            public override SoftwareListEntry next()
//            {
////JAVA TO C# CONVERTER TODO TASK: Java iterators are only converted within the context of 'while' and 'for' loops:
//                object o = iterator.next();
//                if (o is System.Collections.IDictionary)
//                {
//                    return new SoftwareListEntry((System.Collections.IDictionary) o);
//                }
//
//                return (SoftwareListEntry) o;
//
//            }
//
//            public override void remove()
//            {
//                //JAVA TO C# CONVERTER TODO TASK: .NET enumerators are read-only:
//                iterator.remove();
//            }
    }
}