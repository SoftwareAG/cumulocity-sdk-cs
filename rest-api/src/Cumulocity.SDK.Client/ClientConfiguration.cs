using Cumulocity.SDK.Client.Rest.Model.Buffering;

namespace Cumulocity.SDK.Client
{
	public class ClientConfiguration
	{
		public ClientConfiguration() : this(new MemoryBasedPersistentProvider(), true)
		{
		}

		public ClientConfiguration(PersistentProvider persistentProvider) : this(persistentProvider, true)
		{
		}

		public ClientConfiguration(PersistentProvider persistentProvider, bool asyncEnabled)
		{
			AsyncEnabled = asyncEnabled;
			PersistentProvider = persistentProvider;
		}

		public virtual PersistentProvider PersistentProvider { get; }

		public virtual bool AsyncEnabled { get; }
	}
}