// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.GainLossSubAccountMaskAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR;
using PX.Objects.CS;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CM;

[PXDBString(30, IsUnicode = true, InputMask = "")]
[PXUIField]
public sealed class GainLossSubAccountMaskAttribute : PXEntityAttribute
{
  private const string _DimensionName = "SUBACCOUNT";
  private const string _MaskName = "CMSETUP";

  public GainLossSubAccountMaskAttribute()
  {
    PXDimensionMaskAttribute dimensionMaskAttribute = new PXDimensionMaskAttribute("SUBACCOUNT", "CMSETUP", "N", new GainLossAcctSubDefault.ListAttribute().AllowedValues, new GainLossAcctSubDefault.ListAttribute().AllowedLabels);
    dimensionMaskAttribute.ValidComboRequired = false;
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) dimensionMaskAttribute);
    this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1;
  }

  public static int? GetSubID<Field>(PXGraph graph, int? BranchID, Currency currency) where Field : IBqlField
  {
    return GainLossSubAccountMaskAttribute.GetSubIDInternal<Field, Currency>(graph, BranchID, currency);
  }

  public static int? GetSubID<Field>(PXGraph graph, int? BranchID, PX.Objects.CM.Extensions.Currency currency) where Field : IBqlField
  {
    return GainLossSubAccountMaskAttribute.GetSubIDInternal<Field, PX.Objects.CM.Extensions.Currency>(graph, BranchID, currency);
  }

  private static int? GetSubIDInternal<Field, T>(PXGraph graph, int? BranchID, T currency)
    where Field : IBqlField
    where T : class, IBqlTable, new()
  {
    int? subIdInternal1 = (int?) ((PXCache) GraphHelper.Caches<T>(graph)).GetValue<Field>((object) currency);
    CMSetup cmSetup = PXResultset<CMSetup>.op_Implicit(PXSelectBase<CMSetup, PXSelect<CMSetup>.Config>.Select(graph, Array.Empty<object>()));
    if (cmSetup == null || string.IsNullOrEmpty(cmSetup.GainLossSubMask))
      return subIdInternal1;
    PX.Objects.CR.Standalone.Location location = PXResultset<PX.Objects.CR.Standalone.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Standalone.Location, PXSelectJoin<PX.Objects.CR.Standalone.Location, InnerJoin<BAccountR, On<PX.Objects.CR.Standalone.Location.bAccountID, Equal<BAccountR.bAccountID>, And<PX.Objects.CR.Standalone.Location.locationID, Equal<BAccountR.defLocationID>>>, InnerJoin<PX.Objects.GL.Branch, On<BAccountR.bAccountID, Equal<PX.Objects.GL.Branch.bAccountID>>>>, Where<PX.Objects.GL.Branch.branchID, Equal<Required<PX.Objects.GL.Branch.branchID>>>>.Config>.Select(graph, new object[1]
    {
      (object) BranchID
    }));
    int? nullable = (int?) graph.Caches[typeof (PX.Objects.CR.Standalone.Location)].GetValue<PX.Objects.CR.Standalone.Location.cMPGainLossSubID>((object) location);
    object subIdInternal2;
    try
    {
      subIdInternal2 = (object) GainLossSubAccountMaskAttribute.MakeSub<CMSetup.gainLossSubMask>(graph, cmSetup.GainLossSubMask, new object[2]
      {
        (object) subIdInternal1,
        (object) nullable
      }, new System.Type[2]
      {
        typeof (Field),
        typeof (PX.Objects.CR.Location.cMPGainLossSubID)
      });
      ((PXCache) GraphHelper.Caches<T>(graph)).RaiseFieldUpdating<Field>((object) currency, ref subIdInternal2);
    }
    catch (PXException ex)
    {
      subIdInternal2 = (object) null;
    }
    return (int?) subIdInternal2;
  }

  private static string MakeSub<Field>(
    PXGraph graph,
    string mask,
    object[] sources,
    System.Type[] fields)
    where Field : IBqlField
  {
    try
    {
      return PXDimensionMaskAttribute.MakeSub<Field>(graph, mask, new GainLossAcctSubDefault.ListAttribute().AllowedValues, 0, sources);
    }
    catch (PXMaskArgumentException ex)
    {
      PXCache cach = graph.Caches[BqlCommand.GetItemType(fields[ex.SourceIdx])];
      string name = fields[ex.SourceIdx].Name;
      throw new PXMaskArgumentException(new object[2]
      {
        (object) new GainLossAcctSubDefault.ListAttribute().AllowedLabels[ex.SourceIdx],
        (object) PXUIFieldAttribute.GetDisplayName(cach, name)
      });
    }
  }
}
