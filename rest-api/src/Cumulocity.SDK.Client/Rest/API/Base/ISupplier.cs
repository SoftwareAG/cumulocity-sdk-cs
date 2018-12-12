namespace Cumulocity.SDK.Client.Rest.API.Base
{
    public interface ISupplier<out T>
    {
        T get();
    }
}