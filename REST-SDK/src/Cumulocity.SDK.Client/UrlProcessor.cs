namespace Cumulocity.SDK.Client
{
	using System.Collections.Generic;
	using System.Text;

	public class UrlProcessor
	{
		public UrlProcessor()
		{
		}

		public virtual string removeQueryParam(string url, ICollection<string> @params)
		{
			UrlProcessor.URLParts urlParts = UrlProcessor.URLParts.asParts(url);
			IDictionary<string, string> queryParams = urlParts.QueryParams;
			System.Collections.IEnumerator i = @params.GetEnumerator();

			while (i.MoveNext())
			{
				string param = (string)i.Current;
				queryParams.Remove(param);
			}

			return this.buildUrl(urlParts.PartOfUrlWithoutQueryParams, queryParams);
		}

		public virtual string replaceOrAddQueryParam(string url, IDictionary<string, string> newParams)
		{
			UrlProcessor.URLParts urlParts = UrlProcessor.URLParts.asParts(url);
			IDictionary<string, string> queryParams = urlParts.QueryParams;

			foreach (var param in newParams)
			{
				if (!queryParams.TryGetValue(param.Key, out var val))
				{
					queryParams.Add(param.Key, param.Value);
				}
				else
				{
					queryParams[param.Key] = param.Value;
				}
			}

			return this.buildUrl(urlParts.PartOfUrlWithoutQueryParams, queryParams);
		}

		private string buildUrl(string urlBeginning, IDictionary<string, string> queryParams)
		{
			StringBuilder builder = new StringBuilder(urlBeginning);
			if (queryParams.Count > 0)
			{
				builder.Append("?");
				bool addingfirstPair = true;

				for (System.Collections.IEnumerator i = queryParams.SetOfKeyValuePairs().GetEnumerator(); i.MoveNext(); addingfirstPair = false)
				{
					KeyValuePair<string, string> entry = (KeyValuePair<string, string>)i.Current;
					if (!addingfirstPair)
					{
						builder.Append("&");
					}

					builder.Append((string)entry.Key).Append("=").Append((string)entry.Value);
				}
			}

			return builder.ToString();
		}

		private class URLParts
		{
			internal readonly string partOfUrlWithoutQueryParams;
			internal readonly IDictionary<string, string> queryParams;

			internal static UrlProcessor.URLParts asParts(string url)
			{
				string[] urlParts = url.Split("\\?", true);
				string partOfUrlWithoutQueryParams = urlParts[0];
				string queryParamsString = urlParts.Length == 2 ? urlParts[1] : "";
				IDictionary<string, string> queryParams = parseQueryParams(queryParamsString);
				return new UrlProcessor.URLParts(partOfUrlWithoutQueryParams, queryParams);
			}

			internal URLParts(string partOfUrlWithoutQueryParams, IDictionary<string, string> queryParams)
			{
				this.partOfUrlWithoutQueryParams = partOfUrlWithoutQueryParams;
				this.queryParams = queryParams;
			}

			public virtual string PartOfUrlWithoutQueryParams
			{
				get
				{
					return this.partOfUrlWithoutQueryParams;
				}
			}

			public virtual IDictionary<string, string> QueryParams
			{
				get
				{
					return this.queryParams;
				}
			}

			internal static IDictionary<string, string> parseQueryParams(string queryParams)
			{
				IDictionary<string, string> result = new Dictionary<string, string>();
				string[] pairs = queryParams.Split("&", true);
				string[] arr = pairs;
				int len = pairs.Length;

				for (int i = 0; i < len; ++i)
				{
					string pair = arr[i];
					string[] items = pair.Split("=", true);
					if (items.Length == 2)
					{
						result[items[0]] = items[1];
					}
				}

				return result;
			}
		}
	}

	internal static class HashMapHelperClass
	{
		internal static HashSet<KeyValuePair<TKey, TValue>> SetOfKeyValuePairs<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
		{
			HashSet<KeyValuePair<TKey, TValue>> entries = new HashSet<KeyValuePair<TKey, TValue>>();
			foreach (KeyValuePair<TKey, TValue> keyValuePair in dictionary)
			{
				entries.Add(keyValuePair);
			}
			return entries;
		}

		internal static TValue GetValueOrNull<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
		{
			TValue ret;
			dictionary.TryGetValue(key, out ret);
			return ret;
		}
	}
}