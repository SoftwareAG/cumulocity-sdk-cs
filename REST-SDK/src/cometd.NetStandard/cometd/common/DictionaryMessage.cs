using Cometd.Bayeux;
using Cometd.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Cometd.Common
{
	[Serializable]
	public class DictionaryMessage : Dictionary<String, Object>, IMutableMessage
	{
		private const long serialVersionUID = 4318697940670212190L;
		private static readonly ILog LOG = LogProvider.For<DictionaryMessage>();

		public DictionaryMessage()
		{
		}

		public DictionaryMessage(IDictionary<String, Object> message)
		{
			foreach (KeyValuePair<String, Object> kvp in message)
			{
				this.Add(kvp.Key, kvp.Value);
			}
		}

		public IDictionary<String, Object> Advice
		{
			get
			{
				Object advice;
				this.TryGetValue(Message_Fields.ADVICE_FIELD, out advice);
				if (advice is String)
				{
					advice = JsonConvert.DeserializeObject<IDictionary<String, Object>>(advice as String);
					this[Message_Fields.ADVICE_FIELD] = advice;
				}
				//return (IDictionary<String, Object>)advice;
				return advice == null ? null : ((JObject)advice).ToObject<Dictionary<string, object>>();
			}
		}

		public String Channel
		{
			get
			{
				Object obj;
				this.TryGetValue(Message_Fields.CHANNEL_FIELD, out obj);
				return (String)obj;
			}
			set
			{
				this[Message_Fields.CHANNEL_FIELD] = value;
			}
		}

		public ChannelId ChannelId
		{
			get
			{
				return new ChannelId(Channel);
			}
		}

		public String ClientId
		{
			get
			{
				Object obj;
				this.TryGetValue(Message_Fields.CLIENT_ID_FIELD, out obj);
				return (String)obj;
			}
			set
			{
				this[Message_Fields.CLIENT_ID_FIELD] = value;
			}
		}

		public Object Data
		{
			get
			{
				Object obj;
				this.TryGetValue(Message_Fields.DATA_FIELD, out obj);
				return obj;
			}
			set
			{
				this[Message_Fields.DATA_FIELD] = value;
			}
		}

		public IDictionary<String, Object> DataAsDictionary
		{
			get
			{
				Object data;
				this.TryGetValue(Message_Fields.DATA_FIELD, out data);
				if (data is String)
				{
					data = JsonConvert.DeserializeObject<Dictionary<String, Object>>(data as String);
					this[Message_Fields.DATA_FIELD] = data;
				}
				return (Dictionary<String, Object>)data;
			}
		}

		public IDictionary<String, Object> Ext
		{
			get
			{
				Object ext;
				this.TryGetValue(Message_Fields.EXT_FIELD, out ext);

				//TODO: There was if (ext is String)
				if (ext != null)
				{
					ext = JObject.FromObject(ext).ToObject<Dictionary<string, object>>();
					this[Message_Fields.EXT_FIELD] = ext;
				}
				return (Dictionary<String, Object>)ext;
			}
		}

		public String Id
		{
			get
			{
				Object obj;
				this.TryGetValue(Message_Fields.ID_FIELD, out obj);
				return (String)obj;
			}
			set
			{
				this[Message_Fields.ID_FIELD] = value;
			}
		}

		public String JSON
		{
			get
			{
				return JsonConvert.SerializeObject(this as IDictionary<String, Object>);
			}
		}

		public IDictionary<String, Object> getAdvice(bool create)
		{
			IDictionary<String, Object> advice = Advice;
			if (create && advice == null)
			{
				advice = new Dictionary<String, Object>();
				this[Message_Fields.ADVICE_FIELD] = advice;
			}
			return advice;
		}

		public IDictionary<String, Object> getDataAsDictionary(bool create)
		{
			IDictionary<String, Object> data = DataAsDictionary;
			if (create && data == null)
			{
				data = new Dictionary<String, Object>();
				this[Message_Fields.DATA_FIELD] = data;
			}
			return data;
		}

		public IDictionary<String, Object> getExt(bool create)
		{
			IDictionary<String, Object> ext = Ext;
			if (create && ext == null)
			{
				ext = new Dictionary<String, Object>();
				this[Message_Fields.EXT_FIELD] = ext;
			}
			return ext;
		}

		public bool Meta
		{
			get
			{
				return ChannelId.isMeta(Channel);
			}
		}

		public bool Successful
		{
			get
			{
				Object obj;
				this.TryGetValue(Message_Fields.SUCCESSFUL_FIELD, out obj);
				return ObjectConverter.ToBoolean(obj, false);
			}
			set
			{
				this[Message_Fields.SUCCESSFUL_FIELD] = value;
			}
		}

		public override String ToString()
		{
			return JSON;
		}

		public static IList<IMutableMessage> parseMessages(String content)
		{
			IList<IDictionary<String, Object>> dictionaryList = null;
			try
			{
				dictionaryList = JsonConvert.DeserializeObject<IList<IDictionary<String, Object>>>(content);
			}
			catch (Exception e)
			{
				LOG.Debug("Exception when parsing json {0}", e);
			}

			IList<IMutableMessage> messages = new List<IMutableMessage>();
			if (dictionaryList == null)
			{
				return messages;
			}

			foreach (IDictionary<String, Object> message in dictionaryList)
			{
				if (message != null)
					messages.Add(new DictionaryMessage(message));
			}

			return messages;
		}

	}
}