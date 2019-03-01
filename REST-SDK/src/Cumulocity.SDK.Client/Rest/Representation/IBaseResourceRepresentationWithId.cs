using Cumulocity.SDK.Client.Rest.Model.Idtype;

namespace Cumulocity.SDK.Client.Rest.Representation
{
	public interface IBaseResourceRepresentationWithId : IResourceRepresentation
	{
		GId Id { set; }
	}
}