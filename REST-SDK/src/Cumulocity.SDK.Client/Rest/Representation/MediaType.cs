using System;
using System.Collections;
using System.Collections.Generic;

namespace Cumulocity.SDK.Client.Rest.Representation
{
#pragma warning disable CS0169
    public class MediaType
    {
        public const string CHARSET_PARAMETER = "charset";
        public const string MEDIA_TYPE_WILDCARD = "*";
        public const string WILDCARD = "*/*";
        public const string APPLICATION_XML = "application/xml";
        public const string APPLICATION_ATOM_XML = "application/atom+xml";
        public const string APPLICATION_XHTML_XML = "application/xhtml+xml";
        public const string APPLICATION_SVG_XML = "application/svg+xml";
        public const string APPLICATION_JSON = "application/json";
        public const string APPLICATION_FORM_URLENCODED = "application/x-www-form-urlencoded";

        public const string MULTIPART_FORM_DATA = "multipart/form-data";
        public const string APPLICATION_OCTET_STREAM = "application/octet-stream";
        public const string TEXT_PLAIN = "text/plain";
        public const string TEXT_XML = "text/xml";
        public const string TEXT_HTML = "text/html";
        public static readonly MediaType WILDCARD_TYPE = new MediaType();
        public static readonly MediaType APPLICATION_XML_TYPE = new MediaType("application", "xml");
        public static readonly MediaType APPLICATION_ATOM_XML_TYPE = new MediaType("application", "atom+xml");
        public static readonly MediaType APPLICATION_XHTML_XML_TYPE = new MediaType("application", "xhtml+xml");
        public static readonly MediaType APPLICATION_SVG_XML_TYPE = new MediaType("application", "svg+xml");
        public static readonly MediaType APPLICATION_JSON_TYPE = new MediaType("application", "json");

        public static readonly MediaType APPLICATION_FORM_URLENCODED_TYPE =
            new MediaType("application", "x-www-form-urlencoded");

        public static readonly MediaType MULTIPART_FORM_DATA_TYPE = new MediaType("multipart", "form-data");
        public static readonly MediaType APPLICATION_OCTET_STREAM_TYPE = new MediaType("application", "octet-stream");
        public static readonly MediaType TEXT_PLAIN_TYPE = new MediaType("text", "plain");
        public static readonly MediaType TEXT_XML_TYPE = new MediaType("text", "xml");
        public static readonly MediaType TEXT_HTML_TYPE = new MediaType("text", "html");
        private readonly IDictionary<string, string> parameters;
        private readonly string subtype;
        private readonly string type;

        public MediaType(string type, string subtype, IDictionary<string, string> parameters) :
            this(type, subtype, null, createParametersMap(parameters))
        {
        }

        public MediaType(string type, string subtype) : this(type, subtype, null,
            null)
        {
        }

        public MediaType(string type, string subtype, string charset) : this(type, subtype, charset,
            null)
        {
        }

        public MediaType() : this("*", "*", null, null)
        {
        }

        private MediaType(string type, string subtype, string charset, SortedDictionary<string, string> parameterMap)
        {
            this.type = type ?? "*";
            this.subtype = subtype ?? "*";
            if (parameterMap == null)
                parameterMap = new SortedDictionary<string, string>(new ComparatorSortedDictionary());

            if (!ReferenceEquals(charset, null) && charset.Length > 0)
                ((IDictionary) parameterMap)["charset"] = charset;

            parameters = parameterMap;
        }

        public virtual string Type => type;

        public virtual bool WildcardType => Type.Equals("*");

        public virtual string Subtype => subtype;

        public string TypeString => $"{Type}/{Subtype}";

        public virtual bool WildcardSubtype => Subtype.Equals("*");

        public virtual IDictionary<string, string> Parameters => parameters;

        public static SortedDictionary<TKey, TValue> ConvertToSortedDictionary<TKey, TValue>(
            IDictionary<TKey, TValue> map)
        {
            return new SortedDictionary<TKey, TValue>(map);
        }

        private static SortedDictionary<T1, T2> createParametersMap<T1, T2>(IDictionary<T1, T2> initialValues)
        {
            return ConvertToSortedDictionary(initialValues);
        }

        public virtual MediaType withCharset(string charset)
        {
            return new MediaType(type, subtype, charset, createParametersMap(parameters));
        }

        public virtual bool isCompatible(MediaType other)
        {
            return other != null && (type.Equals("*") || other.type.Equals("*") ||
                                     type.Equals(other.type, StringComparison.CurrentCultureIgnoreCase) &&
                                     (subtype.Equals("*") || other.subtype.Equals("*")) ||
                                     type.Equals(other.type, StringComparison.CurrentCultureIgnoreCase) &&
                                     subtype.Equals(other.subtype, StringComparison.CurrentCultureIgnoreCase));
        }

        public override bool Equals(object obj)
        {
            if (!(obj is MediaType)) return false;

            var other = (MediaType) obj;
            return type.Equals(other.type, StringComparison.InvariantCultureIgnoreCase) &&
                   subtype.Equals(other.subtype, StringComparison.InvariantCultureIgnoreCase) &&
                   parameters.Equals(other.parameters);
        }

        public override int GetHashCode()
        {
            return (type.ToLower() + subtype.ToLower()).GetHashCode() + parameters.GetHashCode();
        }

        private class ComparatorSortedDictionary : IComparer<string>
        {
            private readonly MediaType outerInstance;

            public int Compare(string o1, string o2)
            {
                return string.Compare(o1, o2, StringComparison.OrdinalIgnoreCase);
            }
        }

    }
}