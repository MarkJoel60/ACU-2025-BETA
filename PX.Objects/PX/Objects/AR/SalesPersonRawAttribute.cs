// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.SalesPersonRawAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.AR;

[PXDBString(15, IsUnicode = true, InputMask = "", PadSpaced = true)]
[PXUIField]
public sealed class SalesPersonRawAttribute : PXEntityAttribute
{
  public string DimensionName = "SALESPER";

  public SalesPersonRawAttribute()
  {
    Type type1 = typeof (Search<SalesPerson.salesPersonCD, Where<True, Equal<True>>>);
    PXAggregateAttribute.AggregatedAttributesCollection attributes = ((PXAggregateAttribute) this)._Attributes;
    string dimensionName = this.DimensionName;
    Type type2 = type1;
    Type type3 = typeof (SalesPerson.salesPersonCD);
    Type[] typeArray = new Type[2]
    {
      typeof (SalesPerson.salesPersonCD),
      typeof (SalesPerson.descr)
    };
    PXDimensionSelectorAttribute selectorAttribute1;
    PXDimensionSelectorAttribute selectorAttribute2 = selectorAttribute1 = new PXDimensionSelectorAttribute(dimensionName, type2, type3, typeArray);
    attributes.Add((PXEventSubscriberAttribute) selectorAttribute1);
    selectorAttribute2.DescriptionField = typeof (SalesPerson.descr);
    this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1;
    ((PXDimensionSelectorAttribute) ((PXAggregateAttribute) this)._Attributes[this._SelAttrIndex]).CacheGlobal = true;
  }
}
