// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SpecialOrderCurrencyError`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using CommonServiceLocator;
using PX.Data;
using PX.Objects.CM.Extensions;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.SO.GraphExtensions;

public abstract class SpecialOrderCurrencyError<TGraph> : PXGraphExtension<TGraph> where TGraph : PXGraph
{
  public virtual string GetSpecialOrderCurrencyError(
    string currentCuryID,
    int? vendorID,
    int? branchID,
    bool onlyErrors)
  {
    if (currentCuryID == null)
      return (string) null;
    PX.Objects.AP.Vendor vendor = PX.Objects.AP.Vendor.PK.Find((PXGraph) this.Base, vendorID);
    string str = vendor?.CuryID;
    if (str == null)
    {
      branchID = PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>() ? branchID : new int?();
      str = this.GetBaseCuryID(branchID ?? this.Base.Accessinfo.BranchID);
    }
    string orderCurrencyError = (string) null;
    if (str != currentCuryID)
    {
      if ((vendor != null ? (!vendor.AllowOverrideCury.GetValueOrDefault() ? 1 : 0) : 1) != 0)
        orderCurrencyError = "The vendor with the currency that differs from the currency of the sales order is selected and it is not possible to override the currency in purchase orders. The purchase order for the special-order item cannot be created.";
      else if (!onlyErrors)
        orderCurrencyError = "The vendor with the currency that differs from the currency of the sales order is selected. A purchase order with the currency of the sales order will be created.";
    }
    return orderCurrencyError;
  }

  protected virtual string GetBaseCuryID(int? branchID)
  {
    return ServiceLocator.Current.GetInstance<Func<PXGraph, IPXCurrencyService>>()((PXGraph) this.Base).BaseCuryID(branchID);
  }
}
