// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMRecurentBillSubAccountMaskAttribute
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

[PXDBString(30, IsUnicode = true, InputMask = "")]
[PXUIField]
public sealed class PMRecurentBillSubAccountMaskAttribute : PXEntityAttribute
{
  private const string _DimensionName = "SUBACCOUNT";
  private string _MaskName = "PMRECBILL";

  public PMRecurentBillSubAccountMaskAttribute()
  {
    PXDimensionMaskAttribute dimensionMaskAttribute = new PXDimensionMaskAttribute("SUBACCOUNT", this._MaskName, "B", new AcctSubDefault.RecurentBillingSubListAttribute().AllowedValues, new AcctSubDefault.RecurentBillingSubListAttribute().AllowedLabels);
    dimensionMaskAttribute.ValidComboRequired = false;
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) dimensionMaskAttribute);
    this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1;
  }

  public static string MakeSub<Field>(PXGraph graph, string mask, object[] sources, Type[] fields) where Field : IBqlField
  {
    try
    {
      return PXDimensionMaskAttribute.MakeSub<Field>(graph, mask, new AcctSubDefault.RecurentBillingSubListAttribute().AllowedValues, 0, sources);
    }
    catch (PXMaskArgumentException ex)
    {
      PXCache cach = graph.Caches[fields[ex.SourceIdx].DeclaringType];
      string name = fields[ex.SourceIdx].Name;
      throw new PXMaskArgumentException(new object[2]
      {
        (object) new AcctSubDefault.RecurentBillingSubListAttribute().AllowedLabels[ex.SourceIdx],
        (object) PXUIFieldAttribute.GetDisplayName(cach, name)
      });
    }
  }
}
