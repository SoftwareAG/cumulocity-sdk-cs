using System;

namespace Cumulocity.SDK.Client
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public sealed class ParamSourceAttribute : System.Attribute
    {
        internal string value;

        public ParamSourceAttribute(String value = "")
        {
            this.value = value;
        }
     }
 }