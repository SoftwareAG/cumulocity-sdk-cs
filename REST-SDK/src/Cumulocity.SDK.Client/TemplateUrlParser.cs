using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Cumulocity.SDK.Client
{
	public class TemplateUrlParser
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

		public virtual string replacePlaceholdersWithParams(string template, IDictionary<string, string> @params)
		{
			foreach (var entry in @params)
			{
				template = replaceAll(template, asPattern(entry.Key), encode(entry.Value));
			}
			return template;
		}

		private string replaceAll(string template, string key, string value)
		{
			string[] tokens = template.Split("\\?", true);
			bool hasQuery = tokens.Length > 1;
			return replaceAllInPath(tokens[0], key, value) + (hasQuery ? replaceAllInQuery(tokens[1], key, value) : "");
		}

		private string replaceAllInPath(string template, string key, string value)
		{
			return template.Replace(key, value.Replace("+", "%20"));
		}

		private string replaceAllInQuery(string template, string key, string value)
		{
			return "?" + template.Replace(key, value);
		}

		private string asPattern(string key)
		{
			return "{" + key + "}";
		}
	}
}
