// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.VendorMaintMultipleBaseCurrencies
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using CommonServiceLocator;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AR;
using PX.Objects.CM;
using PX.Objects.Common;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.AP;

public class VendorMaintMultipleBaseCurrencies : PXGraphExtension<VendorMaint>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.multipleBaseCurrencies>();

  protected virtual void _(PX.Data.Events.RowSelected<Vendor> e)
  {
    if (e.Row == null)
      return;
    Vendor row = e.Row;
    PXCache cache = e.Cache;
    bool? isBranch;
    int num;
    if (PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.multipleBaseCurrencies>())
    {
      isBranch = e.Row.IsBranch;
      bool flag = false;
      num = isBranch.GetValueOrDefault() == flag & isBranch.HasValue ? 1 : 0;
    }
    else
      num = 0;
    PXUIFieldAttribute.SetRequired<Vendor.vOrgBAccountID>(cache, num != 0);
    bool isVisible = true;
    if (e.Row.BaseCuryID == null)
      isVisible = ServiceLocator.Current.GetInstance<ICurrentUserInformationProvider>().GetAllBranches().ToList<BranchInfo>().Select<BranchInfo, string>((Func<BranchInfo, string>) (b => PXAccess.GetBranch(new int?(b.Id)).BaseCuryID)).Distinct<string>().ToList<string>().Count <= 1;
    PXUIFieldAttribute.SetVisible<VendorMaint.VendorBalanceSummary.balance>(this.Base.VendorBalance.Cache, (object) null, isVisible);
    PXUIFieldAttribute.SetVisible<VendorMaint.VendorBalanceSummary.depositsBalance>(this.Base.VendorBalance.Cache, (object) null, isVisible);
    PXUIFieldAttribute.SetVisible<VendorMaint.VendorBalanceSummary.retainageBalance>(this.Base.VendorBalance.Cache, (object) null, isVisible);
    this.Base.VendorBalanceByBaseCurrency.AllowSelect = !isVisible;
    PXUIFieldAttribute.SetEnabled<Vendor.vOrgBAccountID>(this.Base.CurrentVendor.Cache, (object) row, true);
    isBranch = row.IsBranch;
    if (!isBranch.GetValueOrDefault())
      return;
    using (new PXReadBranchRestrictedScope())
    {
      if ((APHistory) PXSelectBase<APHistory, PXViewOf<APHistory>.BasedOn<SelectFromBase<APHistory, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.GL.Branch>.On<BqlOperand<PX.Objects.GL.Branch.branchID, IBqlInt>.IsEqual<APHistory.branchID>>>, FbqlJoins.Inner<APHistoryAlias>.On<BqlOperand<APHistoryAlias.vendorID, IBqlInt>.IsEqual<APHistory.vendorID>>>, FbqlJoins.Inner<BranchAlias>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<BranchAlias.branchID, Equal<APHistoryAlias.branchID>>>>>.And<BqlOperand<BranchAlias.baseCuryID, IBqlString>.IsNotEqual<PX.Objects.GL.Branch.baseCuryID>>>>>.Where<BqlOperand<APHistory.vendorID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) null, (object) row.BAccountID) != null)
      {
        PXUIFieldAttribute.SetEnabled<Vendor.vOrgBAccountID>(this.Base.CurrentVendor.Cache, (object) row, false);
      }
      else
      {
        if (!(row.Type == "VC"))
          return;
        if ((ARHistory) PXSelectBase<ARHistory, PXViewOf<ARHistory>.BasedOn<SelectFromBase<ARHistory, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.GL.Branch>.On<BqlOperand<PX.Objects.GL.Branch.branchID, IBqlInt>.IsEqual<ARHistory.branchID>>>, FbqlJoins.Inner<ARHistoryAlias>.On<BqlOperand<ARHistoryAlias.customerID, IBqlInt>.IsEqual<ARHistory.customerID>>>, FbqlJoins.Inner<BranchAlias>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<BranchAlias.branchID, Equal<ARHistoryAlias.branchID>>>>>.And<BqlOperand<BranchAlias.baseCuryID, IBqlString>.IsNotEqual<PX.Objects.GL.Branch.baseCuryID>>>>>.Where<BqlOperand<ARHistory.customerID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) null, (object) row.BAccountID) != null)
        {
          PXUIFieldAttribute.SetEnabled<Vendor.vOrgBAccountID>(this.Base.CurrentVendor.Cache, (object) row, false);
        }
        else
        {
          if ((APHistory) PXSelectBase<APHistory, PXViewOf<APHistory>.BasedOn<SelectFromBase<APHistory, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.GL.Branch>.On<BqlOperand<PX.Objects.GL.Branch.branchID, IBqlInt>.IsEqual<APHistory.branchID>>>, FbqlJoins.Inner<ARHistory>.On<BqlOperand<ARHistory.customerID, IBqlInt>.IsEqual<APHistory.vendorID>>>, FbqlJoins.Inner<BranchAlias>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<BranchAlias.branchID, Equal<ARHistory.branchID>>>>>.And<BqlOperand<BranchAlias.baseCuryID, IBqlString>.IsNotEqual<PX.Objects.GL.Branch.baseCuryID>>>>>.Where<BqlOperand<APHistory.vendorID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) null, (object) row.BAccountID) == null)
            return;
          PXUIFieldAttribute.SetEnabled<Vendor.vOrgBAccountID>(this.Base.CurrentVendor.Cache, (object) row, false);
        }
      }
    }
  }

  protected virtual void _(PX.Data.Events.RowDeleted<Vendor> e)
  {
    if (e.Row == null || !(e.Row.Type == "VE") || !e.Row.IsBranch.GetValueOrDefault())
      return;
    BAccountItself baccountItself = this.Base.CurrentBAccountItself.SelectSingle((object) e.Row.BAccountID);
    if (baccountItself == null)
      return;
    baccountItself.BaseCuryID = (string) null;
    baccountItself.VOrgBAccountID = new int?(0);
    this.Base.CurrentBAccountItself.Update(baccountItself);
  }

  protected virtual void _(
    PX.Data.Events.RowSelected<VendorMaint.VendorBalanceSummary> e)
  {
    if (e.Row == null)
      return;
    e.Cache.GetAttributesReadonly((string) null).OfType<CurySymbolAttribute>().ToList<CurySymbolAttribute>().ForEach((System.Action<CurySymbolAttribute>) (attr => attr.SetSymbol((string) null)));
    if (!(e.Cache.Graph.Caches[typeof (Vendor)].Current is Vendor current) || current.BaseCuryID != null)
      return;
    List<string> list = ServiceLocator.Current.GetInstance<ICurrentUserInformationProvider>().GetAllBranches().ToList<BranchInfo>().Select<BranchInfo, string>((Func<BranchInfo, string>) (b => PXAccess.GetBranch(new int?(b.Id)).BaseCuryID)).Distinct<string>().ToList<string>();
    if (list.Count > 1)
      return;
    PX.Objects.CM.Currency curr = CurrencyCollection.GetCurrency(list.FirstOrDefault<string>());
    e.Cache.GetAttributesReadonly((string) null).OfType<CurySymbolAttribute>().ToList<CurySymbolAttribute>().ForEach((System.Action<CurySymbolAttribute>) (attr => attr.SetSymbol(curr?.CurySymbol)));
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<Vendor, Vendor.vOrgBAccountID> e)
  {
    if (e.Row == null)
      return;
    int? newValue1 = (int?) e.NewValue;
    int? oldValue = (int?) e.OldValue;
    if (newValue1.GetValueOrDefault() == oldValue.GetValueOrDefault() & newValue1.HasValue == oldValue.HasValue)
      return;
    Vendor row = e.Row;
    string baseCuryId = PXOrgAccess.GetBaseCuryID(new int?((int) e.NewValue));
    if (!(row.BaseCuryID != baseCuryId))
      return;
    APHistory apHistory = (APHistory) null;
    using (new PXReadBranchRestrictedScope())
      apHistory = (APHistory) PXSelectBase<APHistory, PXViewOf<APHistory>.BasedOn<SelectFromBase<APHistory, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.GL.Branch>.On<BqlOperand<PX.Objects.GL.Branch.branchID, IBqlInt>.IsEqual<APHistory.branchID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<APHistory.vendorID, Equal<P.AsInt>>>>>.And<BqlOperand<PX.Objects.GL.Branch.baseCuryID, IBqlString>.IsNotEqual<P.AsString>>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) null, (object) row.BAccountID, (object) baseCuryId);
    if (apHistory != null)
    {
      e.NewValue = (object) PXOrgAccess.GetCD(new int?((int) e.NewValue));
      throw new PXSetPropertyException("An entity with the base currency other than {0} cannot be associated with {1}, because there are AP documents for the vendor.", PXErrorLevel.Error, new object[2]
      {
        (object) PXAccess.GetBranch(apHistory.BranchID).BaseCuryID,
        (object) row.AcctCD.Trim()
      });
    }
    if (row.Type == "VC")
    {
      int? newValue2 = (int?) e.NewValue;
      int? corgBaccountId = row.COrgBAccountID;
      if (!(newValue2.GetValueOrDefault() == corgBaccountId.GetValueOrDefault() & newValue2.HasValue == corgBaccountId.HasValue))
      {
        ARHistory arHistory = (ARHistory) null;
        using (new PXReadBranchRestrictedScope())
          arHistory = (ARHistory) PXSelectBase<ARHistory, PXViewOf<ARHistory>.BasedOn<SelectFromBase<ARHistory, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.GL.Branch>.On<BqlOperand<PX.Objects.GL.Branch.branchID, IBqlInt>.IsEqual<ARHistory.branchID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<ARHistory.customerID, Equal<P.AsInt>>>>>.And<BqlOperand<PX.Objects.GL.Branch.baseCuryID, IBqlString>.IsNotEqual<P.AsString>>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) null, (object) row.BAccountID, (object) baseCuryId);
        if (arHistory != null)
        {
          e.NewValue = (object) PXOrgAccess.GetCD(new int?((int) e.NewValue));
          PXAccess.MasterCollection.Branch branch1 = PXAccess.GetBranch(arHistory.BranchID);
          PXAccess.MasterCollection.Branch branch2 = !VisibilityRestriction.IsEmpty(row.COrgBAccountID) ? PXAccess.GetBranchByBAccountID(row.COrgBAccountID) : throw new PXSetPropertyException("The entity with the base currency other than {0} cannot be associated with {1}, because the vendor has been extended as a customer and there are AR documents for the customer.", PXErrorLevel.Error, new object[2]
          {
            (object) branch1.BaseCuryID,
            (object) row.AcctCD.Trim()
          });
          PXAccess.MasterCollection.Organization organizationByBaccountId = PXAccess.GetOrganizationByBAccountID(row.COrgBAccountID);
          throw new PXSetPropertyException("An entity with the base currency other than {0} cannot be associated with {1}, because the vendor has been extended as a customer whose visibility is limited to the {2} entity with the {3} base currency and there are AR documents for the customer.", PXErrorLevel.Error, new object[4]
          {
            (object) branch1.BaseCuryID,
            (object) row.AcctCD.Trim(),
            (object) (branch2?.BranchCD?.Trim() ?? organizationByBaccountId?.OrganizationCD?.Trim()),
            (object) (branch2?.BaseCuryID ?? organizationByBaccountId?.BaseCuryID)
          });
        }
        bool? isBranch;
        if (VisibilityRestriction.IsNotEmpty(row.COrgBAccountID))
        {
          isBranch = row.IsBranch;
          if (isBranch.GetValueOrDefault() || VisibilityRestriction.IsNotEmpty((int?) e.NewValue))
          {
            PXAccess.MasterCollection.Branch branchByBaccountId = PXAccess.GetBranchByBAccountID(row.COrgBAccountID);
            PXAccess.MasterCollection.Organization organizationByBaccountId = PXAccess.GetOrganizationByBAccountID(row.COrgBAccountID);
            if (this.Base.BAccount.View.Ask("ChangeVisibilityForVendor", PXMessages.LocalizeFormatNoPrefix("The {0} vendor has been extended as a customer whose visibility is limited to the {1} entity with the {2} base currency. Do you want to change the visibility of both the vendor and customer accounts? To proceed, click Yes.", (object) row.AcctCD.Trim(), (object) (branchByBaccountId?.BranchCD?.Trim() ?? organizationByBaccountId?.OrganizationCD?.Trim()), (object) (branchByBaccountId?.BaseCuryID ?? organizationByBaccountId?.BaseCuryID)), MessageButtons.YesNo) == WebDialogResult.Yes)
            {
              object newValue3 = e.NewValue;
              e.Cache.RaiseFieldVerifying<PX.Objects.CR.BAccount.cOrgBAccountID>((object) e.Row, ref newValue3);
              row.COrgBAccountID = (int?) e.NewValue;
              goto label_31;
            }
            e.NewValue = e.OldValue;
            e.Cancel = true;
            return;
          }
        }
        isBranch = row.IsBranch;
        if (isBranch.GetValueOrDefault() || VisibilityRestriction.IsNotEmpty((int?) e.NewValue))
        {
          if (this.Base.BAccount.View.Ask("SetVisibilityForVendor", PXMessages.LocalizeFormatNoPrefix("The {0} vendor has been extended as a customer whose visibility is not limited to any entity. Do you want to change the visibility of both the customer and vendor accounts? To proceed, click Yes.", (object) row.AcctCD), MessageButtons.YesNo) == WebDialogResult.Yes)
          {
            object newValue4 = e.NewValue;
            e.Cache.RaiseFieldVerifying<PX.Objects.CR.BAccount.cOrgBAccountID>((object) e.Row, ref newValue4);
            row.COrgBAccountID = (int?) e.NewValue;
          }
          else
          {
            e.NewValue = e.OldValue;
            e.Cancel = true;
            return;
          }
        }
      }
    }
label_31:
    Vendor vendor = (Vendor) PXSelectBase<Vendor, PXSelect<Vendor, Where<Vendor.payToVendorID, Equal<Current<Vendor.bAccountID>>, And<Vendor.bAccountID, NotEqual<Current<Vendor.bAccountID>>>>, PX.Data.OrderBy<Asc<Vendor.bAccountID>>>.Config>.SelectSingleBound((PXGraph) this.Base, new object[1]
    {
      (object) row
    });
    if (vendor != null)
    {
      e.NewValue = (object) PXOrgAccess.GetCD(e.NewValue as int?);
      throw new PXSetPropertyException("An entity with the base currency other than {0} cannot be associated with the vendor because it is assigned as a pay-to vendor for {1} whose usage is limited to {2} with the {3} base currency.", new object[4]
      {
        (object) row.BaseCuryID,
        (object) vendor.AcctCD,
        (object) PXOrgAccess.GetCD(vendor.VOrgBAccountID),
        (object) vendor.BaseCuryID
      });
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<Vendor, Vendor.vOrgBAccountID> e)
  {
    int? oldValue = (int?) e.OldValue;
    int? newValue1 = (int?) e.NewValue;
    if (oldValue.GetValueOrDefault() == newValue1.GetValueOrDefault() & oldValue.HasValue == newValue1.HasValue)
      return;
    int? newValue2 = (int?) e.NewValue;
    int num1 = 0;
    if (newValue2.GetValueOrDefault() == num1 & newValue2.HasValue && e.Row.Type == "VC")
    {
      int? corgBaccountId = e.Row.COrgBAccountID;
      int num2 = 0;
      if (!(corgBaccountId.GetValueOrDefault() == num2 & corgBaccountId.HasValue))
        goto label_4;
    }
    e.Row.BaseCuryID = PXOrgAccess.GetBaseCuryID(e.Row.VOrgBAccountID) ?? (e.Row.IsBranch.GetValueOrDefault() ? (string) null : this.Base.Accessinfo.BaseCuryID);
label_4:
    object payToVendorId = (object) e.Row.PayToVendorID;
    try
    {
      e.Cache.RaiseFieldVerifying<Vendor.payToVendorID>((object) e.Row, ref payToVendorId);
    }
    catch (PXSetPropertyException ex)
    {
      e.Cache.RaiseExceptionHandling<Vendor.payToVendorID>((object) e.Row, payToVendorId, (Exception) ex);
    }
  }

  public void _(PX.Data.Events.RowUpdated<Vendor> e)
  {
    if (!(e.OldRow.BaseCuryID != e.Row.BaseCuryID))
      return;
    foreach (PXResult<PX.Objects.CR.Standalone.Location> pxResult in this.Base.GetExtension<VendorMaint.LocationDetailsExt>().Locations.Select())
    {
      PX.Objects.CR.Standalone.Location row = (PX.Objects.CR.Standalone.Location) pxResult;
      row.VCashAccountID = new int?();
      if (this.Base.Caches<PX.Objects.CR.Standalone.Location>().GetStatus(row) == PXEntryStatus.Notchanged)
        this.Base.Caches<PX.Objects.CR.Standalone.Location>().MarkUpdated((object) row);
    }
  }

  protected virtual void _(PX.Data.Events.FieldVerifying<Vendor.payToVendorID> e)
  {
    if (e.Row == null || e.NewValue == null)
      return;
    Vendor row = e.Row as Vendor;
    PX.Objects.CR.BAccount baccount1 = PXSelectorAttribute.Select<Vendor.payToVendorID>(e.Cache, e.Row, e.NewValue) as PX.Objects.CR.BAccount;
    PX.Objects.CR.BAccount baccount2 = PXSelectorAttribute.Select<Vendor.vOrgBAccountID>(e.Cache, e.Row) as PX.Objects.CR.BAccount;
    if (row != null && baccount1 != null && baccount2 != null && baccount1.BaseCuryID != row.BaseCuryID)
    {
      e.NewValue = (object) baccount1.AcctCD;
      throw new PXSetPropertyException("The {0} account cannot be used as a pay-to vendor, because its usage is limited to the entity whose base currency differs from the base currency of {1} associated with the {2} account.", new object[3]
      {
        (object) baccount1.AcctCD,
        (object) baccount2.AcctCD,
        (object) row.AcctCD
      });
    }
  }

  [PXOverride]
  public IEnumerable vendorBalanceByBaseCurrency(
    VendorMaintMultipleBaseCurrencies.vendorBalanceByBaseCurrencyDelegate baseMethod)
  {
    Vendor current = (Vendor) this.Base.BAccountAccessor.Current;
    List<VendorBalanceSummaryByBaseCurrency> summaryByBaseCurrencyList = new List<VendorBalanceSummaryByBaseCurrency>(1);
    if (this.Base.BAccountAccessor.Cache.GetStatus((object) current) != PXEntryStatus.Inserted)
    {
      foreach (PXResult<APVendorBalanceEnq.APLatestHistory, CuryAPHistory> pxResult in new PXSelectJoinGroupBy<APVendorBalanceEnq.APLatestHistory, LeftJoin<CuryAPHistory, On<APVendorBalanceEnq.APLatestHistory.branchID, Equal<CuryAPHistory.branchID>, And<APVendorBalanceEnq.APLatestHistory.accountID, Equal<CuryAPHistory.accountID>, And<APVendorBalanceEnq.APLatestHistory.vendorID, Equal<CuryAPHistory.vendorID>, And<APVendorBalanceEnq.APLatestHistory.subID, Equal<CuryAPHistory.subID>, And<APVendorBalanceEnq.APLatestHistory.curyID, Equal<CuryAPHistory.curyID>, And<APVendorBalanceEnq.APLatestHistory.lastActivityPeriod, Equal<CuryAPHistory.finPeriodID>>>>>>>, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.branchID, Equal<CuryAPHistory.branchID>>>>, Where<APVendorBalanceEnq.APLatestHistory.vendorID, Equal<Current<Vendor.bAccountID>>>, PX.Data.Aggregate<GroupBy<PX.Objects.GL.Branch.baseCuryID, Sum<CuryAPHistory.finYtdBalance, Sum<CuryAPHistory.finYtdDeposits, Sum<CuryAPHistory.finYtdRetainageWithheld, Sum<CuryAPHistory.finYtdRetainageReleased>>>>>>>((PXGraph) this.Base).Select())
      {
        CuryAPHistory aSrc = (CuryAPHistory) pxResult;
        this.Aggregate(summaryByBaseCurrencyList, aSrc);
      }
    }
    List<string> foundCurrencies = summaryByBaseCurrencyList.Select<VendorBalanceSummaryByBaseCurrency, string>((Func<VendorBalanceSummaryByBaseCurrency, string>) (_ => _.BaseCuryID)).Distinct<string>().ToList<string>();
    foreach (string str in ServiceLocator.Current.GetInstance<ICurrentUserInformationProvider>().GetAllBranches().ToList<BranchInfo>().Select<BranchInfo, string>((Func<BranchInfo, string>) (b => PXAccess.GetBranch(new int?(b.Id)).BaseCuryID)).Distinct<string>().ToList<string>().Where<string>((Func<string, bool>) (c => !foundCurrencies.Contains(c))))
      summaryByBaseCurrencyList.Add(new VendorBalanceSummaryByBaseCurrency()
      {
        VendorID = this.Base.BAccount.Current.BAccountID,
        BaseCuryID = str,
        Balance = new Decimal?(0M),
        DepositsBalance = new Decimal?(0M),
        RetainageBalance = new Decimal?(0M)
      });
    return (IEnumerable) summaryByBaseCurrencyList.OrderBy<VendorBalanceSummaryByBaseCurrency, string>((Func<VendorBalanceSummaryByBaseCurrency, string>) (_ => _.BaseCuryID));
  }

  protected virtual void Aggregate(
    List<VendorBalanceSummaryByBaseCurrency> aRes,
    CuryAPHistory aSrc)
  {
    string baseCuryID = PXAccess.GetBranch(aSrc.BranchID)?.BaseCuryID;
    VendorBalanceSummaryByBaseCurrency summaryByBaseCurrency1 = aRes.Where<VendorBalanceSummaryByBaseCurrency>((Func<VendorBalanceSummaryByBaseCurrency, bool>) (_ => _.BaseCuryID == baseCuryID)).FirstOrDefault<VendorBalanceSummaryByBaseCurrency>();
    Decimal? nullable1;
    if (summaryByBaseCurrency1 == null)
    {
      summaryByBaseCurrency1 = new VendorBalanceSummaryByBaseCurrency();
      nullable1 = summaryByBaseCurrency1.Balance;
      if (!nullable1.HasValue)
        summaryByBaseCurrency1.Balance = new Decimal?(0M);
      nullable1 = summaryByBaseCurrency1.DepositsBalance;
      if (!nullable1.HasValue)
        summaryByBaseCurrency1.DepositsBalance = new Decimal?(0M);
      nullable1 = summaryByBaseCurrency1.RetainageBalance;
      if (!nullable1.HasValue)
        summaryByBaseCurrency1.RetainageBalance = new Decimal?(0M);
      summaryByBaseCurrency1.BaseCuryID = baseCuryID;
      aRes.Add(summaryByBaseCurrency1);
    }
    summaryByBaseCurrency1.VendorID = aSrc.VendorID;
    VendorBalanceSummaryByBaseCurrency summaryByBaseCurrency2 = summaryByBaseCurrency1;
    nullable1 = summaryByBaseCurrency2.Balance;
    Decimal valueOrDefault1 = aSrc.FinYtdBalance.GetValueOrDefault();
    summaryByBaseCurrency2.Balance = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault1) : new Decimal?();
    VendorBalanceSummaryByBaseCurrency summaryByBaseCurrency3 = summaryByBaseCurrency1;
    nullable1 = summaryByBaseCurrency3.DepositsBalance;
    Decimal valueOrDefault2 = aSrc.FinYtdDeposits.GetValueOrDefault();
    summaryByBaseCurrency3.DepositsBalance = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + valueOrDefault2) : new Decimal?();
    VendorBalanceSummaryByBaseCurrency summaryByBaseCurrency4 = summaryByBaseCurrency1;
    nullable1 = summaryByBaseCurrency4.RetainageBalance;
    Decimal? retainageWithheld = aSrc.FinYtdRetainageWithheld;
    Decimal? nullable2 = aSrc.FinYtdRetainageReleased;
    Decimal? nullable3 = retainageWithheld.HasValue & nullable2.HasValue ? new Decimal?(retainageWithheld.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable4;
    if (!(nullable1.HasValue & nullable3.HasValue))
    {
      nullable2 = new Decimal?();
      nullable4 = nullable2;
    }
    else
      nullable4 = new Decimal?(nullable1.GetValueOrDefault() + nullable3.GetValueOrDefault());
    summaryByBaseCurrency4.RetainageBalance = nullable4;
  }

  public delegate IEnumerable vendorBalanceByBaseCurrencyDelegate();
}
