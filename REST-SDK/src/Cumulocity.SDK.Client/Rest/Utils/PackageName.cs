using System;

namespace Cumulocity.SDK.Client.Rest.Utils
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PackageName: System.Attribute
    {
        public string Name { get; set; }

        public PackageName(string name){
            Name = name;
        }
    }
}