// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.LocationIDBaseAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CS;

public abstract class LocationIDBaseAttribute : PXEntityAttribute
{
  public const string DimensionName = "LOCATION";
  protected const string CS_VIEW_NAME = "_Location_Contact_Address_";

  public LocationIDBaseAttribute()
    : this(typeof (Where<boolTrue, Equal<boolTrue>>))
  {
  }

  public LocationIDBaseAttribute(System.Type WhereType)
  {
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) this.GetSelectorAttribute(BqlCommand.Compose(new System.Type[3]
    {
      typeof (Search<,>),
      typeof (Location.locationID),
      WhereType
    })));
    this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1;
  }

  public LocationIDBaseAttribute(System.Type WhereType, System.Type JoinType)
  {
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) this.GetSelectorAttribute(BqlCommand.Compose(new System.Type[4]
    {
      typeof (Search2<,,>),
      typeof (Location.locationID),
      JoinType,
      WhereType
    })));
    this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1;
  }

  /// <summary>Defines columns set of the selector</summary>
  protected virtual PXDimensionSelectorAttribute GetSelectorAttribute(System.Type searchType)
  {
    return new PXDimensionSelectorAttribute("LOCATION", searchType, typeof (Location.locationCD), new System.Type[2]
    {
      typeof (Location.locationCD),
      typeof (Location.descr)
    });
  }

  public virtual bool DirtyRead
  {
    get
    {
      return this._SelAttrIndex != -1 && ((PXDimensionSelectorAttribute) ((PXAggregateAttribute) this)._Attributes[this._SelAttrIndex]).DirtyRead;
    }
    set
    {
      if (this._SelAttrIndex == -1)
        return;
      ((PXDimensionSelectorAttribute) ((PXAggregateAttribute) this)._Attributes[this._SelAttrIndex]).DirtyRead = value;
    }
  }
}
