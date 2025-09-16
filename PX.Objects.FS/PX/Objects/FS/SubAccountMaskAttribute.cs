// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.SubAccountMaskAttribute
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.CS;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.FS;

[PXDBString(30, IsUnicode = true, InputMask = "")]
[PXUIField]
public sealed class SubAccountMaskAttribute : PXEntityAttribute
{
  private const string DIMENSION_NAME = "SUBACCOUNT";
  private const string MASK_NAME = "FSSrvOrdType";
  private const string CONTRACT_MASK_NAME = "ServiceContract";

  public SubAccountMaskAttribute()
  {
    FSDimensionMaskAttribute dimensionMaskAttribute = new FSDimensionMaskAttribute("SUBACCOUNT", "FSSrvOrdType", "L", new FSAcctSubDefault.ClassListAttribute().AllowedValues, new FSAcctSubDefault.ClassListAttribute().AllowedLabels);
    dimensionMaskAttribute.ValidComboRequired = false;
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) dimensionMaskAttribute);
    this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1;
  }

  public SubAccountMaskAttribute(bool ignoreSrvOrdType)
  {
    FSDimensionMaskAttribute dimensionMaskAttribute = new FSDimensionMaskAttribute("SUBACCOUNT", "ServiceContract", "L", new FSAcctSubDefault.ClassListAttribute(true).AllowedValues, new FSAcctSubDefault.ClassListAttribute(true).AllowedLabels);
    dimensionMaskAttribute.ValidComboRequired = false;
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) dimensionMaskAttribute);
    this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1;
  }

  public static string MakeSub<Field>(
    PXGraph graph,
    string mask,
    object[] sources,
    Type[] fields,
    bool contract = false)
    where Field : IBqlField
  {
    try
    {
      string[] allowedValues = !contract ? new FSAcctSubDefault.ClassListAttribute().AllowedValues : new FSAcctSubDefault.ClassListAttribute(true).AllowedValues;
      return PXDimensionMaskAttribute.MakeSub<Field>(graph, mask, allowedValues, 0, sources);
    }
    catch (PXMaskArgumentException ex)
    {
      PXCache cach = graph.Caches[BqlCommand.GetItemType(fields[ex.SourceIdx])];
      string name = fields[ex.SourceIdx].Name;
      throw new PXMaskArgumentException(new object[2]
      {
        (object) new FSAcctSubDefault.ClassListAttribute().AllowedLabels[ex.SourceIdx],
        (object) PXUIFieldAttribute.GetDisplayName(cach, name)
      });
    }
  }
}
