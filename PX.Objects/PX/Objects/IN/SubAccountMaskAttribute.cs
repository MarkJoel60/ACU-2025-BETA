// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.SubAccountMaskAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.IN;

[PXDBString(30, InputMask = "")]
[PXUIField]
public sealed class SubAccountMaskAttribute : PXEntityAttribute
{
  private const string _DimensionName = "SUBACCOUNT";
  private const string _MaskName = "ZZZZZZZZZZ";

  public SubAccountMaskAttribute()
  {
    PXDimensionMaskAttribute dimensionMaskAttribute = new PXDimensionMaskAttribute("SUBACCOUNT", "ZZZZZZZZZZ", "I", new INAcctSubDefault.ClassListAttribute().AllowedValues, new INAcctSubDefault.ClassListAttribute().AllowedLabels);
    dimensionMaskAttribute.ValidComboRequired = false;
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) dimensionMaskAttribute);
    this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1;
  }

  public static string MakeSub<Field>(PXGraph graph, string mask, params object[] sources) where Field : IBqlField
  {
    return PXDimensionMaskAttribute.MakeSub<Field>(graph, mask, new INAcctSubDefault.ClassListAttribute().AllowedValues, sources);
  }

  public static string MakeSub<Field>(PXGraph graph, string mask, object[] sources, Type[] fields) where Field : IBqlField
  {
    if (string.IsNullOrEmpty(mask))
    {
      object obj;
      graph.Caches[BqlCommand.GetItemType(typeof (Field))].RaiseFieldDefaulting<Field>((object) null, ref obj);
      mask = (string) obj;
    }
    try
    {
      return PXDimensionMaskAttribute.MakeSub<Field>(graph, mask, new INAcctSubDefault.ClassListAttribute().AllowedValues, sources);
    }
    catch (PXMaskArgumentException ex)
    {
      PXCache cach = graph.Caches[BqlCommand.GetItemType(fields[ex.SourceIdx])];
      string name = fields[ex.SourceIdx].Name;
      throw new PXMaskArgumentException(ex, new object[2]
      {
        (object) new INAcctSubDefault.ClassListAttribute().AllowedLabels[ex.SourceIdx],
        (object) PXUIFieldAttribute.GetDisplayName(cach, name)
      });
    }
  }

  public static string MakeSub<Field>(
    PXGraph graph,
    string mask,
    bool? stkItem,
    object[] sources,
    Type[] fields)
    where Field : IBqlField
  {
    if (string.IsNullOrEmpty(mask))
    {
      object obj;
      graph.Caches[BqlCommand.GetItemType(typeof (Field))].RaiseFieldDefaulting<Field>((object) null, ref obj);
      mask = (string) obj;
    }
    try
    {
      return PXDimensionMaskAttribute.MakeSub<Field>(graph, mask, stkItem, new INAcctSubDefault.ClassListAttribute().AllowedValues, sources);
    }
    catch (PXMaskArgumentException ex)
    {
      PXCache cach = graph.Caches[BqlCommand.GetItemType(fields[ex.SourceIdx])];
      string name = fields[ex.SourceIdx].Name;
      throw new PXMaskArgumentException(ex, new object[2]
      {
        (object) new INAcctSubDefault.ClassListAttribute().AllowedLabels[ex.SourceIdx],
        (object) PXUIFieldAttribute.GetDisplayName(cach, name)
      });
    }
  }
}
