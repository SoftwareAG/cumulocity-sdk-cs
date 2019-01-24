using System.Linq;
using System.Text;
using System.Web;

namespace Cumulocity.SDK.Client
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public abstract class Filter
    {

        public static string encode(string value)
        {
            try
            {
                return HttpUtility.UrlEncode(value, Encoding.UTF8);
            }
            catch (Exception e)
            {
                throw new Exception("UTF-8 is not supported!?", e);
            }
        }

        public virtual IDictionary<string, string> QueryParams
        {
            get
            {
                IDictionary<string, string> @params = new Dictionary<string, string>();
                Type clazz = this.GetType();
                foreach (var field in clazz.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance))
                {
                    if (isTechnicalProxy(field))
                    {
                        continue;
                    }
                    
                    //field.Accessible = true; // => BindingFlags.DeclaredOnly
                    string value = (string) safelyGetFieldValue(field, this);
                    if (!string.ReferenceEquals(value, null))
                    {
                        @params[getParamName(field)] = encode(value);
                    }
                }
                return @params;
            }
        }

        private string getParamName(FieldInfo field)
        {
            var attrs = (ParamSourceAttribute[])field.GetCustomAttributes
                (typeof(ParamSourceAttribute), false);
            
            if (attrs.Length == 0)
            {
                return field.Name;
            }

            return attrs.First().value;
        }

        private object safelyGetFieldValue(FieldInfo field, Filter filter)
        {
            try
            {
                return field.GetValue(filter);
            }
            catch (System.ArgumentException e)
            {
                throw new Exception(e.Message);
            }
            catch (AccessViolationException e)
            {
                throw new Exception(e.Message);
            }
        }

        private bool isTechnicalProxy(FieldInfo field)
        {
            return field.Name.Equals("$jacocoData");
        }
    }
}