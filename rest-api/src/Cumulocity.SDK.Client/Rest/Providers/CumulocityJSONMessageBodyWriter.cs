namespace Cumulocity.SDK.Client.Rest.Providers
{
    public class CumulocityJSONMessageBodyWriter
    {
    }


//using System;
//
////JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
////ORIGINAL LINE: @Provider public class CumulocityJSONMessageBodyWriter implements MessageBodyWriter<BaseResourceRepresentation>
//public class CumulocityJSONMessageBodyWriter : MessageBodyWriter<BaseResourceRepresentation>
//{
//	private static readonly Charset a = Charset.forName("UTF-8");
//	private readonly JSON b;
//
//	public CumulocityJSONMessageBodyWriter()
//	{
//		LoggerFactory.getLogger(typeof(CumulocityJSONMessageBodyWriter));
//		this.b = JSONBase.JSONGenerator;
//	}
//
//	public virtual bool isWriteable(Type type, Type genericType, Annotation[] annotations, MediaType mediaType)
//	{
//		return type.IsSubclassOf(typeof(BaseResourceRepresentation));
//	}
//
//	public virtual long getSize(BaseResourceRepresentation t, Type type, Type genericType, Annotation[] annotations, MediaType mediaType)
//	{
//		return -1L;
//	}
//
////JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
////ORIGINAL LINE: public void writeTo(BaseResourceRepresentation representation, Class type, Type genericType, Annotation[] annotations, MediaType mediaType, MultivaluedMap<String, Object> httpHeaders, OutputStream entityStream) throws IOException, WebApplicationException
//	public virtual void writeTo(BaseResourceRepresentation representation, Type type, Type genericType, Annotation[] annotations, MediaType mediaType, MultivaluedMap<string, object> httpHeaders, OutputStream entityStream)
//	{
//		if (!mediaType.isCompatible(CumulocityMediaType.APPLICATION_JSON_STREAM))
//		{
//			string type2 = this.b.forValue(representation);
//			entityStream.WriteByte(type2.GetBytes(a));
//		}
//		else
//		{
//			BufferedWriter type1 = new System.IO.StreamWriter(entityStream, a);
//			Exception genericType1 = null;
//			bool var11 = false;
//
//			try
//			{
//				var11 = true;
//				this.b.dumpObject(new WriterSink(type1), representation);
//				var11 = false;
//			}
//			catch (Exception var13)
//			{
//				genericType1 = var13;
//				throw var13;
//			}
//			finally
//			{
//				if (var11)
//				{
//					if (genericType1 != null)
//					{
//						try
//						{
//							type1.Close();
//						}
//						catch (Exception var12)
//						{
//							genericType1.addSuppressed(var12);
//						}
//					}
//					else
//					{
//						type1.Close();
//					}
//
//				}
//			}
//
//			type1.Close();
//		}
//	}
//}
//
////-------------------------------------------------------------------------------------------
////	Copyright © 2007 - 2017 Tangible Software Solutions Inc.
////	This class can be used by anyone provided that the copyright notice remains intact.
////
////	This class is used to convert some aspects of the Java String class.
////-------------------------------------------------------------------------------------------
//internal static class StringHelperClass
//{
//	//----------------------------------------------------------------------------------
//	//	This method replaces the Java String.substring method when 'start' is a
//	//	method call or calculated value to ensure that 'start' is obtained just once.
//	//----------------------------------------------------------------------------------
//	internal static string SubstringSpecial(this string self, int start, int end)
//	{
//		return self.Substring(start, end - start);
//	}
//
//	//------------------------------------------------------------------------------------
//	//	This method is used to replace calls to the 2-arg Java String.startsWith method.
//	//------------------------------------------------------------------------------------
//	internal static bool StartsWith(this string self, string prefix, int toffset)
//	{
//		return self.IndexOf(prefix, toffset, System.StringComparison.Ordinal) == toffset;
//	}
//
//	//------------------------------------------------------------------------------
//	//	This method is used to replace most calls to the Java String.split method.
//	//------------------------------------------------------------------------------
//	internal static string[] Split(this string self, string regexDelimiter, bool trimTrailingEmptyStrings)
//	{
//		string[] splitArray = System.Text.RegularExpressions.Regex.Split(self, regexDelimiter);
//
//		if (trimTrailingEmptyStrings)
//		{
//			if (splitArray.Length > 1)
//			{
//				for (int i = splitArray.Length; i > 0; i--)
//				{
//					if (splitArray[i - 1].Length > 0)
//					{
//						if (i < splitArray.Length)
//							System.Array.Resize(ref splitArray, i);
//
//						break;
//					}
//				}
//			}
//		}
//
//		return splitArray;
//	}
//
//	//-----------------------------------------------------------------------------
//	//	These methods are used to replace calls to some Java String constructors.
//	//-----------------------------------------------------------------------------
//	internal static string NewString(sbyte[] bytes)
//	{
//		return NewString(bytes, 0, bytes.Length);
//	}
//	internal static string NewString(sbyte[] bytes, int index, int count)
//	{
//		return System.Text.Encoding.UTF8.GetString((byte[])(object)bytes, index, count);
//	}
//	internal static string NewString(sbyte[] bytes, string encoding)
//	{
//		return NewString(bytes, 0, bytes.Length, encoding);
//	}
//	internal static string NewString(sbyte[] bytes, int index, int count, string encoding)
//	{
//		return System.Text.Encoding.GetEncoding(encoding).GetString((byte[])(object)bytes, index, count);
//	}
//
//	//--------------------------------------------------------------------------------
//	//	These methods are used to replace calls to the Java String.getBytes methods.
//	//--------------------------------------------------------------------------------
//	internal static sbyte[] GetBytes(this string self)
//	{
//		return GetSBytesForEncoding(System.Text.Encoding.UTF8, self);
//	}
//	internal static sbyte[] GetBytes(this string self, System.Text.Encoding encoding)
//	{
//		return GetSBytesForEncoding(encoding, self);
//	}
//	internal static sbyte[] GetBytes(this string self, string encoding)
//	{
//		return GetSBytesForEncoding(System.Text.Encoding.GetEncoding(encoding), self);
//	}
//	private static sbyte[] GetSBytesForEncoding(System.Text.Encoding encoding, string s)
//	{
//		sbyte[] sbytes = new sbyte[encoding.GetByteCount(s)];
//		encoding.GetBytes(s, 0, s.Length, (byte[])(object)sbytes, 0);
//		return sbytes;
//	}
//}
}