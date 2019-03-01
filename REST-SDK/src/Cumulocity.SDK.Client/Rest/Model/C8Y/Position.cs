using Cumulocity.SDK.Client.Rest.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Cumulocity.SDK.Client.Rest.Model
{
#pragma warning disable 0472
	[PackageName("c8y_Position")]
	public class Position
	{
		private const long serialVersionUID = -8365376637780307348L;
		private decimal lat;
		private decimal lng;
		private decimal alt;

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		private long? accuracy;

		public Position()
		{
		}
		[JsonProperty("lat")]
		public virtual decimal Lat
		{
			get
			{
				return this.lat;
			}
			set
			{
				this.lat = value;
			}
		}
		[JsonProperty("lng")]
		public virtual decimal Lng
		{
			get
			{
				return this.lng;
			}
			set
			{
				this.lng = value;
			}
		}
		[JsonProperty("alt")]
		public virtual decimal Alt
		{
			get
			{
				return this.alt;
			}
			set
			{
				this.alt = value;
			}
		}

		public virtual long? getAccuracy()
		{
			return this.accuracy;
		}

		public virtual void setAccuracy(long accuracy)
		{
			this.accuracy = new long?(accuracy);
		}

		public virtual void setAccuracy(long? accuracy)
		{
			this.accuracy = accuracy;
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			else if (obj == this)
			{
				return true;
			}
			else if (!(obj is Position))
			{
				return false;
			}
			else
			{
				bool var10000;
				Position rhs;
				bool result;
				{
					{
						rhs = (Position)obj;
						result = this.lat == null ? rhs.lat == null : this.lat.Equals(rhs.lat);
						if (result)
						{
							if (this.lng == null)
							{
								if (rhs.lng == null)
								{
									goto label48Break;
								}
							}
							else if (this.lng.Equals(rhs.lng))
							{
								goto label48Break;
							}
						}

						var10000 = false;
						goto label49Break;
					}
					label48Break:

					var10000 = true;
				}
				label49Break:

				{
					{
						result = var10000;
						if (result)
						{
							if (this.alt == null)
							{
								if (rhs.alt == null)
								{
									goto label39Break;
								}
							}
							else if (this.alt.Equals(rhs.alt))
							{
								goto label39Break;
							}
						}

						var10000 = false;
						goto label40Break;
					}
					label39Break:

					var10000 = true;
				}
				label40Break:

				result = var10000;
				return result;
			}
		}

		public override int GetHashCode()
		{
			int result = this.lat == null ? 0 : this.lat.GetHashCode();
			result = 31 * result + (this.lng == null ? 0 : this.lng.GetHashCode());
			result = 31 * result + (this.alt == null ? 0 : this.alt.GetHashCode());
			return result;
		}

		public override string ToString()
		{
			return string.Format("Position [lat={0}, lng={1}, alt={2}, accuracy={3}]", this.lat, this.lng, this.alt, this.accuracy);
		}
	}
}