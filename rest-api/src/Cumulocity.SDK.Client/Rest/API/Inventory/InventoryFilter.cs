using System.Linq;
using Cumulocity.SDK.Client.Rest.Model.Idtype;

namespace Cumulocity.SDK.Client.Rest.API.Inventory
{
using System;
using System.Collections.Generic;
using System.Text;

public class InventoryFilter : Filter
{

//ORIGINAL LINE: @ParamSource private String fragmentType;
	private string fragmentType;

//ORIGINAL LINE: @ParamSource private String type;
	private string type;

//ORIGINAL LINE: @ParamSource private String owner;
	private string owner;

//ORIGINAL LINE: @ParamSource private String text;
	private string text;

//ORIGINAL LINE: @ParamSource private String ids;
	private string ids;

//ORIGINAL LINE: @ParamSource private String childAssetId;
	private string childAssetId;

//ORIGINAL LINE: @ParamSource private String childDeviceId;
	private string childDeviceId;

//ORIGINAL LINE: @ParamSource private String childAdditionId;
	private string childAdditionId;

	public static InventoryFilter searchInventory()
	{
		return new InventoryFilter();
	}

	/// <summary>
	/// Specifies the {@code fragmentType} query parameter
	/// </summary>
	/// <param name="fragmentClass"> the class representation of the type of the managed object(s) </param>
	/// <returns> the managed object filter with {@code fragmentType} set </returns>
	public virtual InventoryFilter byFragmentType(Type fragmentClass)
	{
		this.fragmentType = Cumulocity.SDK.Client.Rest.Model.util.ExtensibilityConverter.ClassToStringRepresentation(fragmentClass);
		return this;
	}

	/// <summary>
	/// Specifies the {@code fragmentType} query parameter
	/// </summary>
	/// <param name="fragmentType"> the string representation of the type of the managed object(s) </param>
	/// <returns> the managed object filter with {@code fragmentType} set </returns>
	public virtual InventoryFilter byFragmentType(string fragmentType)
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
	/// <returns> the managed object filter with {@code type} set </returns>
	public virtual InventoryFilter byType(string type)
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
	/// <returns> the managed object filter with {@code owner} set </returns>
	public virtual InventoryFilter byOwner(string owner)
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
	/// <returns> the managed object filter with {@code text} set </returns>
	public virtual InventoryFilter byText(string text)
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
	/// <returns> the managed object filter with {@code ids} set </returns>
	public virtual InventoryFilter byIds(IList<GId> ids)
	{
		this.ids = createCommaSeparatedStringFromGids(ids);
		return this;
	}

	/// <summary>
	/// Specifies the {@code ids} query parameter
	/// </summary>
	/// <param name="ids"> the ids of the managed object(s) </param>
	/// <returns> the managed object filter with {@code ids} set </returns>
	public virtual InventoryFilter byIds(params GId[] ids)
	{
		this.ids = createCommaSeparatedStringFromGids(ids.ToList());
		return this;
	}

	/// <summary>
	/// Specifies the {@code childAssetId} query parameter
	/// </summary>
	/// <param name="childAssetId"> the child asset of the managed object(s) </param>
	/// <returns> the managed object filter with {@code childAssetId} set </returns>
	public virtual InventoryFilter byChildAssetId(GId childAssetId)
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
	/// <returns> the managed object filter with {@code childDeviceId} set </returns>
	public virtual InventoryFilter byChildDeviceId(GId childDeviceId)
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
	/// <returns> the managed object filter with {@code childAdditionId} set </returns>
	public virtual InventoryFilter byChildAdditionId(GId childAdditionId)
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

	private string createCommaSeparatedStringFromGids(IList<GId> ids)
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