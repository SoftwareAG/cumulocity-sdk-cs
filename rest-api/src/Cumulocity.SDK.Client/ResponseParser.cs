using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Cumulocity.SDK.Client.Rest;
using Cumulocity.SDK.Client.Rest.Model.Idtype;
using Cumulocity.SDK.Client.Rest.Representation;
using Cumulocity.SDK.Client.Rest.Representation.Platform;
using Newtonsoft.Json;

namespace Cumulocity.SDK.Client
{
    public class ResponseParser
    {
        public const string NO_ERROR_REPRESENTATION = "Something went wrong. Failed to parse error message.";
        //private static readonly Logger LOG = LoggerFactory.getLogger(typeof(ResponseParser));

        //JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        public virtual T parse<T>(HttpResponseMessage response, Type type, params int[] expectedStatusCode)
        {
            checkStatus(response, expectedStatusCode);
            var r = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<T>(r);
            return result; 
        }

	    public virtual async Task<T> ParseAsync<T>(HttpResponseMessage response, int expectedStatusCode, Type type)
	    {
		    checkStatus(response, expectedStatusCode);
		    var r = await response.Content.ReadAsStringAsync();
		    var result = JsonConvert.DeserializeObject<T>(r);
		    return result;
	    }


		//ORIGINAL LINE: public <T> T parseObject(ClientResponse response, int expectedStatusCode, Class<T> type) throws SDKException
		public virtual T parseObject<T>(HttpResponseMessage response, int expectedStatusCode, T type)
        {
            checkStatus(response, expectedStatusCode);
            //return response.getEntity(type);
            return (T) new object();
        }


        //ORIGINAL LINE: public void checkStatus(ClientResponse response, int... expectedStatusCodes) throws SDKException
        public virtual void checkStatus(HttpResponseMessage response, params int[] expectedStatusCodes)
        {
	            var status = (int) response.StatusCode;
            var arr = expectedStatusCodes;
            var len = expectedStatusCodes.Length;

            for (var i = 0; i < len; ++i)
            {
                var expectedStatusCode = arr[i];
                if (status == expectedStatusCode) return;
            }

            throw new SDKException(status, getErrorMessage(response, status));
        }

        protected internal virtual string getErrorMessage(HttpResponseMessage response, int status)
        {
            var errorMessage = "Http status code: " + status;
            //if (response.hasEntity())
            if (response != null)
            {
                var errorRepresentation = getErrorRepresentation(response);
                if (ReferenceEquals(errorRepresentation, null))
                    errorRepresentation = "Something went wrong. Failed to parse error message.";

                errorMessage = errorMessage + "\n" + errorRepresentation;
            }

            return errorMessage;
        }

        protected internal virtual string getErrorRepresentation(HttpResponseMessage response)
        {
            try
            {
                if (isJsonResponse(response)) return "ErrorMessageRepresentation";

                //LOG.error("Failed to parse error message to json. Getting error string... ");
                //LOG.error((string) response.getEntity(typeof(string)));
            }
            catch (Exception var3)
            {
                //LOG.error("Failed to parse error message", var3);
            }

            return null;
        }

        protected internal virtual bool isJsonResponse(HttpResponseMessage response)
        {
//			MediaType contentType = response.Type;
//			if (contentType == null)
//			{
//				return false;
//			}
//			else
//			{
//				return contentType.Type.contains("application") && contentType.Subtype.contains("json");
//			}
            return true;
        }

        public virtual GId parseIdFromLocation(HttpResponseMessage response)
        {
            //string path = response.Location.Path;
            var path = "";
            var pathParts = path.Split("\\/", true);
            var gid = pathParts[pathParts.Length - 1];
            return new GId(gid);
        }
    }

    public static class StringHelperClass
    {
        //----------------------------------------------------------------------------------
        //	This method replaces the Java String.substring method when 'start' is a
        //	method call or calculated value to ensure that 'start' is obtained just once.
        //----------------------------------------------------------------------------------
        internal static string SubstringSpecial(this string self, int start, int end)
        {
            return self.Substring(start, end - start);
        }

        //------------------------------------------------------------------------------------
        //	This method is used to replace calls to the 2-arg Java String.startsWith method.
        //------------------------------------------------------------------------------------
        internal static bool StartsWith(this string self, string prefix, int toffset)
        {
            return self.IndexOf(prefix, toffset, StringComparison.Ordinal) == toffset;
        }

        //------------------------------------------------------------------------------
        //	This method is used to replace most calls to the Java String.split method.
        //------------------------------------------------------------------------------
        internal static string[] Split(this string self, string regexDelimiter, bool trimTrailingEmptyStrings)
        {
            var splitArray = Regex.Split(self, regexDelimiter);

            if (trimTrailingEmptyStrings)
                if (splitArray.Length > 1)
                    for (var i = splitArray.Length; i > 0; i--)
                        if (splitArray[i - 1].Length > 0)
                        {
                            if (i < splitArray.Length)
                                Array.Resize(ref splitArray, i);

                            break;
                        }

            return splitArray;
        }

        //-----------------------------------------------------------------------------
        //	These methods are used to replace calls to some Java String constructors.
        //-----------------------------------------------------------------------------
        internal static string NewString(sbyte[] bytes)
        {
            return NewString(bytes, 0, bytes.Length);
        }

        internal static string NewString(sbyte[] bytes, int index, int count)
        {
            return Encoding.UTF8.GetString((byte[]) (object) bytes, index, count);
        }

        internal static string NewString(sbyte[] bytes, string encoding)
        {
            return NewString(bytes, 0, bytes.Length, encoding);
        }

        internal static string NewString(sbyte[] bytes, int index, int count, string encoding)
        {
            return Encoding.GetEncoding(encoding).GetString((byte[]) (object) bytes, index, count);
        }

        //--------------------------------------------------------------------------------
        //	These methods are used to replace calls to the Java String.getBytes methods.
        //--------------------------------------------------------------------------------
        internal static sbyte[] GetBytes(this string self)
        {
            return GetSBytesForEncoding(Encoding.UTF8, self);
        }

        internal static sbyte[] GetBytes(this string self, Encoding encoding)
        {
            return GetSBytesForEncoding(encoding, self);
        }

        internal static sbyte[] GetBytes(this string self, string encoding)
        {
            return GetSBytesForEncoding(Encoding.GetEncoding(encoding), self);
        }

        private static sbyte[] GetSBytesForEncoding(Encoding encoding, string s)
        {
            var sbytes = new sbyte[encoding.GetByteCount(s)];
            encoding.GetBytes(s, 0, s.Length, (byte[]) (object) sbytes, 0);
            return sbytes;
        }
    }
}