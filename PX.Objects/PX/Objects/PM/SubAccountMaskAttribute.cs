// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.SubAccountMaskAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.PM;

/// <summary>
/// Attribute for Subaccount field. Aggregates PXFieldAttribute, PXUIFieldAttribute and DimensionSelector without any restriction.
/// Used in PM Billing to create a subaccount mask for Expenses.
/// </summary>
[PXDBString(30, IsUnicode = true, InputMask = "")]
[PXUIField]
public sealed class SubAccountMaskAttribute : PXEntityAttribute
{
  private const string _DimensionName = "SUBACCOUNT";
  private const string _MaskName = "PMSETUP";

  public SubAccountMaskAttribute()
  {
    PXDimensionMaskAttribute dimensionMaskAttribute = new PXDimensionMaskAttribute("SUBACCOUNT", "PMSETUP", "I", new AcctSubDefault.SubListAttribute().AllowedValues, new AcctSubDefault.SubListAttribute().AllowedLabels);
    dimensionMaskAttribute.ValidComboRequired = false;
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) dimensionMaskAttribute);
    this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1;
  }

  public static string MakeSub<Field>(PXGraph graph, string mask, object[] sources, Type[] fields) where Field : IBqlField
  {
    try
    {
      return PXDimensionMaskAttribute.MakeSub<Field>(graph, mask, new AcctSubDefault.SubListAttribute().AllowedValues, 0, sources);
    }
    catch (PXMaskArgumentException ex)
    {
      PXCache cach = graph.Caches[BqlCommand.GetItemType(fields[ex.SourceIdx])];
      string name = fields[ex.SourceIdx].Name;
      throw new PXMaskArgumentException(new object[2]
      {
        (object) new AcctSubDefault.SubListAttribute().AllowedLabels[ex.SourceIdx],
        (object) PXUIFieldAttribute.GetDisplayName(cach, name)
      });
    }
  }
}
