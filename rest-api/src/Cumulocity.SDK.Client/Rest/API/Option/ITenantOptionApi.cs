using Cumulocity.SDK.Client.Rest.Model.Option;
using Cumulocity.SDK.Client.Rest.Representation.Tenant;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cumulocity.SDK.Client.Rest.API.Option
{
	/// <summary>
	/// API for creating, updating and retrieving options from the platform.
	/// </summary>
	public interface ITenantOptionApi
	{
		/// <summary>
		/// Gets an option by id
		/// Requires role ROLE_OPTION_MANAGEMENT_READ
		/// </summary>
		/// <param name="optionPK"> id of the option to search for </param>
		/// <returns> the option with the given id </returns>
		/// <exception cref="SDKException"> if the option is not found or if the query failed </exception>
		OptionRepresentation getOption(OptionPK optionPK);

		/// <summary>
		/// Gets all options from the platform
		/// Requires role ROLE_OPTION_MANAGEMENT_READ
		/// </summary>
		/// <returns> collection of options with paging functionality </returns>
		/// <exception cref="SDKException"> if the query failed </exception>
		ITenantOptionCollection Options { get; }

		/// <summary>
		/// Creates or updates an option in the platform.
		/// Requires role ROLE_OPTION_MANAGEMENT_ADMIN
		/// </summary>
		/// <param name="option"> option to be created </param>
		/// <returns> the created option with the generated id </returns>
		/// <exception cref="SDKException"> if the option could not be created </exception>
		OptionRepresentation save(OptionRepresentation option);

		/// <summary>
		/// Creates or updates an option in the platform. Immediate response is available through the Future object.
		/// In case of lost connection, buffers data in persistence provider.
		/// Requires role ROLE_OPTION_MANAGEMENT_ADMIN
		/// </summary>
		/// <param name="option"> option to be created </param>
		/// <returns> the created option with the generated id </returns>
		/// <exception cref="SDKException"> if the option could not be created </exception>
		Task<OptionRepresentation> saveAsync(OptionRepresentation option);

		/// <summary>
		/// Deletes option from the platform.
		/// Requires role ROLE_OPTION_MANAGEMENT_ADMIN
		/// </summary>
		/// <param name="optionPK"> to be deleted </param>
		/// <exception cref="SDKException"> if the measurement could not be deleted </exception>
		void delete(OptionPK optionPK);

		/// <summary>
		/// Gets all options from the platform for the specific category
		/// Requires role ROLE_OPTION_MANAGEMENT_READ
		/// </summary>
		/// <returns> collection of options </returns>
		/// <exception cref="SDKException"> if the query failed </exception>
		IList<OptionRepresentation> getAllOptionsForCategory(string category);
	}
}