using Cumulocity.SDK.Client.Rest.Representation;

namespace Cumulocity.SDK.Client.Rest.Model.Idtype
{
    public interface IResourceRepresentationWithId: IResourceRepresentation
    {
        GId Id {set;}
    }
}