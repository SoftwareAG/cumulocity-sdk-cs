using System.Linq;
using Cumulocity.SDK.Client.Rest.Model.Idtype;

namespace Cumulocity.SDK.Client.Rest.API.Inventory
{
using System;
using System.Collections.Generic;
using System.Text;

public class InventoryFilter : Filter
{

	private string fragmentType;

	private string type;

	private string owner;

	private string text;

	private string ids;

	private string childAssetId;

	private string childDeviceId;

	private string childAdditionId;

	public static InventoryFilter searchInventory()
	{
		return new InventoryFilter();
	}

	/// <summary>
	/// Specifies the {@code fragmentType} query parameter
	/// </summary>
	/// <param name="fragmentClass"> the class representation of the type of the managed object(s) </param>
	/// <returns> the managed object filter with {@code fragmentType} Set </returns>
	public virtual InventoryFilter ByFragmentType(Type fragmentClass)
	{
		this.fragmentType = Cumulocity.SDK.Client.Rest.Model.util.ExtensibilityConverter.ClassToStringRepresentation(fragmentClass);
		return this;
	}

	/// <summary>
	/// Specifies the {@code fragmentType} query parameter
	/// </summary>
	/// <param name="fragmentType"> the string representation of the type of the managed object(s) </param>
	/// <returns> the managed object filter with {@code fragmentType} Set </returns>
	public virtual InventoryFilter ByFragmentType(string fragmentType)
	{
		this.fragmentType = fragmentType;
		return this;
	}

	/// <returns> the {@code fragmentType} parameter of the query </returns>
	public virtual string FragmentType
	{
		get
		{
			return fragmentType;
		}
	}

	/// <summary>
	/// Specifies the {@code type} query parameter
	/// </summary>
	/// <param name="type"> the type of the managed object(s) </param>
	/// <returns> the managed object filter with {@code type} Set </returns>
	public virtual InventoryFilter ByType(string type)
	{
		this.type = type;
		return this;
	}

	/// <returns> the {@code type} parameter of the query </returns>
	public virtual string Type
	{
		get
		{
			return type;
		}
	}

	/// <summary>
	/// Specifies the {@code owner} query parameter
	/// </summary>
	/// <param name="owner"> the owner of the managed object(s) </param>
	/// <returns> the managed object filter with {@code owner} Set </returns>
	public virtual InventoryFilter ByOwner(string owner)
	{
		this.owner = owner;
		return this;
	}

	/// <returns> the {@code owner} parameter of the query </returns>
	public virtual string Owner
	{
		get
		{
			return owner;
		}
	}

	/// <summary>
	/// Specifies the {@code text} query parameter
	/// </summary>
	/// <param name="text"> the text of the managed object(s) </param>
	/// <returns> the managed object filter with {@code text} Set </returns>
	public virtual InventoryFilter ByText(string text)
	{
		this.text = text;
		return this;
	}

	/// <returns> the {@code text} parameter of the query </returns>
	public virtual string Text
	{
		get
		{
			return text;
		}
	}

	/// <summary>
	/// Specifies the {@code ids} query parameter
	/// </summary>
	/// <param name="ids"> the ids of the managed object(s) </param>
	/// <returns> the managed object filter with {@code ids} Set </returns>
	public virtual InventoryFilter ByIds(IList<GId> ids)
	{
		this.ids = CreateCommaSeparatedStringFromGids(ids);
		return this;
	}

	/// <summary>
	/// Specifies the {@code ids} query parameter
	/// </summary>
	/// <param name="ids"> the ids of the managed object(s) </param>
	/// <returns> the managed object filter with {@code ids} Set </returns>
	public virtual InventoryFilter ByIds(params GId[] ids)
	{
		this.ids = CreateCommaSeparatedStringFromGids(ids.ToList());
		return this;
	}

	/// <summary>
	/// Specifies the {@code childAssetId} query parameter
	/// </summary>
	/// <param name="childAssetId"> the child asset of the managed object(s) </param>
	/// <returns> the managed object filter with {@code childAssetId} Set </returns>
	public virtual InventoryFilter ByChildAssetId(GId childAssetId)
	{
		this.childAssetId = childAssetId.Value;
		return this;
	}

	public virtual string ChildAssetId
	{
		get
		{
			return childAssetId;
		}
	}

	/// <summary>
	/// Specifies the {@code childDeviceId} query parameter
	/// </summary>
	/// <param name="childDeviceId"> the child asset of the managed object(s) </param>
	/// <returns> the managed object filter with {@code childDeviceId} Set </returns>
	public virtual InventoryFilter ByChildDeviceId(GId childDeviceId)
	{
		this.childDeviceId = childDeviceId.Value;
		return this;
	}

	public virtual string ChildDeviceId
	{
		get
		{
			return childDeviceId;
		}
	}

	/// <summary>
	/// Specifies the {@code childAdditionId} query parameter
	/// </summary>
	/// <param name="childAdditionId"> the child addition of the managed object(s) </param>
	/// <returns> the managed object filter with {@code childAdditionId} Set </returns>
	public virtual InventoryFilter ByChildAdditionId(GId childAdditionId)
	{
		this.childAdditionId = childAdditionId.Value;
		return this;
	}

	public virtual string ChildAdditionId
	{
		get
		{
			return childAdditionId;
		}
	}

	private string CreateCommaSeparatedStringFromGids(IList<GId> ids)
	{
		bool atLeastOneItemProcessed = false;
		StringBuilder builder = new StringBuilder();

		foreach (GId gid in ids)
		{
			atLeastOneItemProcessed = true;
			builder.Append(gid.Value);
			builder.Append(",");
		}

		// remove last comma if needed
		if (atLeastOneItemProcessed)
		{
			builder.Remove(builder.Length - 1, 1);
		}

		return builder.ToString();
	}

	/// <returns> the {@code ids} parameter of the query </returns>
	public virtual string Ids
	{
		get
		{
			return ids;
		}
	}

}

}