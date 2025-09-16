// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.ExternalTaxBase`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CS.Contracts.Interfaces;
using PX.Data;
using PX.Objects.CS;
using PX.TaxProvider;
using System;

#nullable disable
namespace PX.Objects.TX;

public abstract class ExternalTaxBase<TGraph> : PXGraphExtension<TGraph> where TGraph : PXGraph
{
  public static Func<PXGraph, string, ITaxProvider> TaxProviderFactory = (Func<PXGraph, string, ITaxProvider>) ((graph, taxZoneID) =>
  {
    TaxZone taxZone = PXResultset<TaxZone>.op_Implicit(PXSelectBase<TaxZone, PXSelect<TaxZone, Where<TaxZone.taxZoneID, Equal<Required<TaxZone.taxZoneID>>>>.Config>.Select(graph, new object[1]
    {
      (object) taxZoneID
    }));
    return taxZone.IsExternal.GetValueOrDefault() && PXAccess.FeatureInstalled<FeaturesSet.avalaraTax>() && !string.IsNullOrEmpty(taxZone.TaxPluginID) ? TaxPluginMaint.CreateTaxProvider(graph, taxZone.TaxPluginID) : (ITaxProvider) null;
  });

  public static bool IsExternalTax(PXGraph graph, string taxZoneID)
  {
    if (string.IsNullOrEmpty(taxZoneID) || !PXAccess.FeatureInstalled<FeaturesSet.avalaraTax>())
      return false;
    TaxZone taxZone = PXResultset<TaxZone>.op_Implicit(PXSelectBase<TaxZone, PXSelect<TaxZone, Where<TaxZone.taxZoneID, Equal<Required<TaxZone.taxZoneID>>>>.Config>.Select(graph, new object[1]
    {
      (object) taxZoneID
    }));
    return taxZone != null && (taxZone.IsExternal ?? false) && !string.IsNullOrEmpty(taxZone.TaxPluginID);
  }

  public static bool IsEmptyAddress(IAddressLocation address)
  {
    int num1 = !string.IsNullOrEmpty(((IAddressBase) address)?.PostalCode) ? 0 : (string.IsNullOrEmpty(((IAddressBase) address)?.AddressLine1) ? 1 : 0);
    Decimal? nullable;
    int num2;
    if (address == null)
    {
      num2 = 1;
    }
    else
    {
      nullable = address.Latitude;
      num2 = !nullable.HasValue ? 1 : 0;
    }
    int num3;
    if (num2 == 0)
    {
      if (address != null)
      {
        nullable = address.Latitude;
        Decimal num4 = 0M;
        if (nullable.GetValueOrDefault() == num4 & nullable.HasValue)
          goto label_13;
      }
      int num5;
      if (address == null)
      {
        num5 = 1;
      }
      else
      {
        nullable = address.Longitude;
        num5 = !nullable.HasValue ? 1 : 0;
      }
      if (num5 == 0)
      {
        if (address == null)
        {
          num3 = 0;
          goto label_14;
        }
        nullable = address.Longitude;
        Decimal num6 = 0M;
        num3 = nullable.GetValueOrDefault() == num6 & nullable.HasValue ? 1 : 0;
        goto label_14;
      }
    }
label_13:
    num3 = 1;
label_14:
    int num7 = num3 != 0 ? 1 : 0;
    return (num1 & num7) != 0;
  }

  public static string CompanyCodeFromBranch(PXGraph graph, string taxZoneID, int? branchID)
  {
    TaxPluginMapping taxPluginMapping = PXResultset<TaxPluginMapping>.op_Implicit(PXSelectBase<TaxPluginMapping, PXSelectJoin<TaxPluginMapping, InnerJoin<TaxZone, On<TaxPluginMapping.taxPluginID, Equal<TaxZone.taxPluginID>>>, Where<TaxPluginMapping.branchID, Equal<Required<TaxPluginMapping.branchID>>, And<TaxZone.taxZoneID, Equal<Required<TaxZone.taxZoneID>>>>>.Config>.Select(graph, new object[2]
    {
      (object) branchID,
      (object) taxZoneID
    }));
    if (taxPluginMapping != null)
      return taxPluginMapping.CompanyCode;
    if (PXResultset<TaxPlugin>.op_Implicit(PXSelectBase<TaxPlugin, PXSelectJoin<TaxPlugin, InnerJoin<TaxZone, On<TaxPlugin.taxPluginID, Equal<TaxZone.taxPluginID>>>, Where<TaxZone.taxZoneID, Equal<Required<TaxZone.taxZoneID>>>>.Config>.Select(graph, new object[1]
    {
      (object) taxZoneID
    })) == null)
      throw new PXSetPropertyException("External tax provider is not configured.");
    throw new PXException("The Branch to Company Code mapping is not specified in the configuration of the external tax provider.");
  }

  public static string GetTaxID(TaxDetail taxDetail)
  {
    return $"{taxDetail.TaxName.ToUpperInvariant()} {taxDetail.Rate * 100M:G6}";
  }
}
