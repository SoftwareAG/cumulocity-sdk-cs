using System;
using System.Linq;
using System.Reflection;

namespace Cumulocity.SDK.Client.Rest.Utils
{
    public static class TypeExtensions
    {
        public static MethodInfo GetMethod(this Type type, string name, bool generic, int paramsCount)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }
            return type.GetMethods()
                .FirstOrDefault(method =>method.GetParameters().Length == paramsCount & method.Name == name & method.IsGenericMethod == generic);
        }
    }
}