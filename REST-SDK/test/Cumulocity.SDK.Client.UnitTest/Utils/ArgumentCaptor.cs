using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cumulocity.SDK.Client.UnitTest.Utils
{
    public class ArgumentCaptor<T>
    {
        public T Capture()
        {
            return It.Is<T>(t => SaveValue(t));
        }

        private bool SaveValue(T t)
        {
            Value = t;
            return true;
        }

        public T Value { get; private set; }
    }
}
