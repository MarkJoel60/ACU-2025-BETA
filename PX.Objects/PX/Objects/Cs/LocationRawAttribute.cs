// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.LocationRawAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CS;

[PXDBString(InputMask = "", IsUnicode = true, PadSpaced = true)]
[PXUIField]
public sealed class LocationRawAttribute : PXEntityAttribute, IPXRowPersistedSubscriber
{
  public string DimensionName = "LOCATION";

  public LocationRawAttribute(System.Type WhereType, System.Type SubstituteKey = null)
  {
    System.Type type1 = BqlCommand.Compose(new System.Type[3]
    {
      typeof (Search<,>),
      typeof (Location.locationCD),
      WhereType
    });
    PXAggregateAttribute.AggregatedAttributesCollection attributes = ((PXAggregateAttribute) this)._Attributes;
    string dimensionName = this.DimensionName;
    System.Type type2 = type1;
    System.Type type3 = SubstituteKey;
    if ((object) type3 == null)
      type3 = typeof (Location.locationCD);
    System.Type[] typeArray = new System.Type[2]
    {
      typeof (Location.locationCD),
      typeof (Location.descr)
    };
    PXDimensionSelectorAttribute selectorAttribute = new PXDimensionSelectorAttribute(dimensionName, type2, type3, typeArray);
    attributes.Add((PXEventSubscriberAttribute) selectorAttribute);
    this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1;
    ((PXDimensionSelectorAttribute) ((PXAggregateAttribute) this)._Attributes[this._SelAttrIndex]).CacheGlobal = true;
  }

  void IPXRowPersistedSubscriber.RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    if (e.TranStatus != 1)
      return;
    PXSelectorAttribute.ClearGlobalCache<Location>();
  }
}
