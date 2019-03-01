namespace Cumulocity.SDK.Client.Rest.API.Base
{
	public class Suppliers
	{
		public static ISupplier<T> OfInstance<T>(T instance)
		{
			return new SupplierAnonymousInnerClass<T>(instance);
		}

		private class SupplierAnonymousInnerClass<T> : ISupplier<T>
		{
			private readonly T instance;

			public SupplierAnonymousInnerClass(T instance)
			{
				this.instance = instance;
			}

			public virtual T Get()
			{
				return instance;
			}
		}
	}
}