// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.BaseInventoryAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.IN;

/// <summary>
/// Provides a base selector for the Inventory Items. The list is filtered by the user access rights.
/// </summary>
[PXDBInt]
[PXUIField]
public abstract class BaseInventoryAttribute : PXEntityAttribute
{
  public const 
  #nullable disable
  string DimensionName = "INVENTORY";

  public BaseInventoryAttribute()
    : this(typeof (Search<InventoryItem.inventoryID, Where<Match<Current<AccessInfo.userName>>>>), typeof (InventoryItem.inventoryCD), typeof (InventoryItem.descr))
  {
  }

  public BaseInventoryAttribute(Type SearchType, Type SubstituteKey, Type DescriptionField)
  {
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new PXDimensionSelectorAttribute("INVENTORY", SearchType, SubstituteKey)
    {
      CacheGlobal = true,
      DescriptionField = DescriptionField
    });
    this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1;
  }

  public BaseInventoryAttribute(
    Type SearchType,
    Type SubstituteKey,
    Type DescriptionField,
    Type[] fields)
  {
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new PXDimensionSelectorAttribute("INVENTORY", SearchType, SubstituteKey, fields)
    {
      CacheGlobal = true,
      DescriptionField = DescriptionField
    });
    this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1;
  }

  public class dimensionName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    BaseInventoryAttribute.dimensionName>
  {
    public dimensionName()
      : base("INVENTORY")
    {
    }
  }
}
