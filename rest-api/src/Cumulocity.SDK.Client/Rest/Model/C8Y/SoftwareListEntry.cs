using Cumulocity.SDK.Client.Rest.Utils;
using System.Collections.Generic;

namespace Cumulocity.SDK.Client.Rest.Model.C8Y
{
	[PackageName("c8y_SoftwareListEntry")]
	public class SoftwareListEntry
	{
		private const long serialVersionUID = 196308789576755511L;

		private string name;

		private string url;

		private string version;

		public SoftwareListEntry()
		{
		}

		public SoftwareListEntry(IDictionary<string, string> map)
		{
			name = map["name"];
			version = map["version"];
			url = map["url"];
		}

		public SoftwareListEntry(string name, string version, string url)
		{
			this.name = name;
			this.version = version;
			this.url = url;
		}

		public virtual string Name
		{
			get => name;
			set => name = value;
		}

		public virtual string Version
		{
			get => version;
			set => version = value;
		}

		public virtual string Url
		{
			get => url;
			set => url = value;
		}

		public override int GetHashCode()
		{
			const int prime = 31;
			var result = 1;
			result = prime * result + (ReferenceEquals(name, null) ? 0 : name.GetHashCode());
			result = prime * result + (ReferenceEquals(url, null) ? 0 : url.GetHashCode());
			result = prime * result + (ReferenceEquals(version, null) ? 0 : version.GetHashCode());
			return result;
		}

		public override bool Equals(object obj)
		{
			if (this == obj) return true;
			if (obj == null) return false;
			if (GetType() != obj.GetType()) return false;
			var other = (SoftwareListEntry)obj;
			if (ReferenceEquals(name, null))
			{
				if (!ReferenceEquals(other.name, null)) return false;
			}
			else if (!name.Equals(other.name))
			{
				return false;
			}

			if (ReferenceEquals(url, null))
			{
				if (!ReferenceEquals(other.url, null)) return false;
			}
			else if (!url.Equals(other.url))
			{
				return false;
			}

			if (ReferenceEquals(version, null))
			{
				if (!ReferenceEquals(other.version, null)) return false;
			}
			else if (!version.Equals(other.version))
			{
				return false;
			}

			return true;
		}

		public override string ToString()
		{
			return "SoftwareListEntry [name=" + name + ", version=" + version + ", url=" + url + "]";
		}
	}
}