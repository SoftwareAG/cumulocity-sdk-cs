using System;

namespace Cumulocity.SDK.Client
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public class ParamSource : System.Attribute
    {
        internal string value;

        public ParamSource(String value = "")
        {
            this.value = value;
        }
     }
 }