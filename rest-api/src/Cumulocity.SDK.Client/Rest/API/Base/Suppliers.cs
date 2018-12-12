namespace Cumulocity.SDK.Client.Rest.API.Base
{
    public class Suppliers
    {

        //ORIGINAL LINE: public static <T> Supplier<T> ofInstance(final T instance)
        public static ISupplier<T> ofInstance<T>(T instance)
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

            public virtual T get()
            {
                return instance;
            }
        }
    }
}