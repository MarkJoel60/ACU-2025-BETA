// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.LocationValidator
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.Common;
using PX.Objects.GL.Helpers;
using System;

#nullable disable
namespace PX.Objects.CR;

public class LocationValidator
{
  public virtual bool ValidateCustomerLocation(
    PXCache cache,
    BAccount baccount,
    ILocation location)
  {
    bool flag1 = this.ValidateCommon(cache, baccount, location);
    AccountAndSubValidationHelper validationHelper = new AccountAndSubValidationHelper(cache, (object) location);
    bool flag2 = flag1 & validationHelper.SetErrorEmptyIfNull<Location.cSalesAcctID>((object) location.CSalesAcctID).SetErrorEmptyIfNull<Location.cSalesSubID>((object) location.CSalesSubID).SetErrorIfInactiveAccount<Location.cARAccountID>((object) location.CARAccountID).SetErrorIfInactiveSubAccount<Location.cARSubID>((object) location.CARSubID).SetErrorIfInactiveAccount<Location.cSalesAcctID>((object) location.CSalesAcctID).SetErrorIfInactiveSubAccount<Location.cSalesSubID>((object) location.CSalesSubID).SetErrorIfInactiveAccount<Location.cDiscountAcctID>((object) location.CDiscountAcctID).SetErrorIfInactiveSubAccount<Location.cDiscountSubID>((object) location.CDiscountSubID).SetErrorIfInactiveAccount<Location.cFreightAcctID>((object) location.CFreightAcctID).SetErrorIfInactiveSubAccount<Location.cFreightSubID>((object) location.CFreightSubID).IsValid;
    if (!location.IsARAccountSameAsMain.GetValueOrDefault())
      flag2 &= validationHelper.SetErrorEmptyIfNull<Location.cARAccountID>((object) location.CARAccountID).SetErrorEmptyIfNull<Location.cARSubID>((object) location.CARSubID).IsValid;
    return flag2;
  }

  public virtual bool ValidateVendorLocation(PXCache cache, Vendor vendor, ILocation location)
  {
    bool flag = this.ValidateCommon(cache, (BAccount) vendor, location);
    ValidationHelper validationHelper = new ValidationHelper(cache, (object) location);
    if (vendor.TaxAgency.GetValueOrDefault())
      flag &= validationHelper.SetErrorEmptyIfNull<Location.vExpenseAcctID>((object) location.VExpenseAcctID).SetErrorEmptyIfNull<Location.vExpenseSubID>((object) location.VExpenseSubID).IsValid;
    if (!location.IsAPAccountSameAsMain.GetValueOrDefault())
      flag &= validationHelper.SetErrorEmptyIfNull<Location.vAPAccountID>((object) location.VAPAccountID).SetErrorEmptyIfNull<Location.vAPSubID>((object) location.VAPSubID).IsValid;
    return flag;
  }

  protected virtual bool ValidateCommon(PXCache cache, BAccount acct, ILocation loc)
  {
    int? locationId = loc.LocationID;
    int? defLocationId = acct.DefLocationID;
    if (!(locationId.GetValueOrDefault() == defLocationId.GetValueOrDefault() & locationId.HasValue == defLocationId.HasValue) || loc.IsActive.GetValueOrDefault())
      return true;
    cache.RaiseExceptionHandling<Location.isActive>((object) loc, (object) null, (Exception) new PXSetPropertyException("Default location can not be inactive.", new object[1]
    {
      (object) "[isActive]"
    }));
    return false;
  }
}
