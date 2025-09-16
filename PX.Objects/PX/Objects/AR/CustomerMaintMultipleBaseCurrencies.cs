// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CustomerMaintMultipleBaseCurrencies
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using CommonServiceLocator;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AP;
using PX.Objects.CA;
using PX.Objects.CM;
using PX.Objects.Common;
using PX.Objects.Common.Scopes;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.AR;

public class CustomerMaintMultipleBaseCurrencies : 
  PXGraphExtension<CustomerMaint.PaymentDetailsExt, CustomerMaint>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>();

  protected virtual void _(PX.Data.Events.RowSelected<Customer> e)
  {
    if (e.Row == null)
      return;
    Customer row = e.Row;
    PXCache cache = ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<Customer>>) e).Cache;
    bool? isBranch;
    int num1;
    if (PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>())
    {
      isBranch = e.Row.IsBranch;
      bool flag = false;
      num1 = isBranch.GetValueOrDefault() == flag & isBranch.HasValue ? 1 : 0;
    }
    else
      num1 = 0;
    PXUIFieldAttribute.SetRequired<Customer.cOrgBAccountID>(cache, num1 != 0);
    int num2;
    if (e.Row.BaseCuryID != null)
    {
      isBranch = e.Row.IsBranch;
      num2 = !isBranch.GetValueOrDefault() ? 1 : 0;
    }
    else
      num2 = 0;
    bool flag1 = num2 != 0;
    PXUIFieldAttribute.SetVisible<Customer.creditRule>(((PXSelectBase) ((PXGraphExtension<CustomerMaint>) this).Base.CurrentCustomer).Cache, (object) null, flag1);
    PXUIFieldAttribute.SetVisible<Customer.creditLimit>(((PXSelectBase) ((PXGraphExtension<CustomerMaint>) this).Base.CurrentCustomer).Cache, (object) null, flag1);
    PXUIFieldAttribute.SetVisible<Customer.creditDaysPastDue>(((PXSelectBase) ((PXGraphExtension<CustomerMaint>) this).Base.CurrentCustomer).Cache, (object) null, flag1);
    PXUIFieldAttribute.SetVisible<CustomerMaint.CustomerBalanceSummary.unreleasedBalance>(((PXSelectBase) ((PXGraphExtension<CustomerMaint>) this).Base.CustomerBalance).Cache, (object) null, flag1);
    PXUIFieldAttribute.SetVisible<CustomerMaint.CustomerBalanceSummary.openOrdersBalance>(((PXSelectBase) ((PXGraphExtension<CustomerMaint>) this).Base.CustomerBalance).Cache, (object) null, flag1);
    PXUIFieldAttribute.SetVisible<CustomerMaint.CustomerBalanceSummary.remainingCreditLimit>(((PXSelectBase) ((PXGraphExtension<CustomerMaint>) this).Base.CustomerBalance).Cache, (object) null, flag1);
    PXUIFieldAttribute.SetVisible<CustomerMaint.CustomerBalanceSummary.oldInvoiceDate>(((PXSelectBase) ((PXGraphExtension<CustomerMaint>) this).Base.CustomerBalance).Cache, (object) null, flag1);
    bool flag2 = true;
    if (e.Row.BaseCuryID == null)
      flag2 = ServiceLocator.Current.GetInstance<ICurrentUserInformationProvider>().GetAllBranches().ToList<BranchInfo>().Select<BranchInfo, string>((Func<BranchInfo, string>) (b => PXAccess.GetBranch(new int?(b.Id)).BaseCuryID)).Distinct<string>().ToList<string>().Count <= 1;
    bool flag3 = PXAccess.FeatureInstalled<FeaturesSet.parentChildAccount>() && CustomerMaint.HasChildren<PX.Objects.AR.Override.BAccount.parentBAccountID>((PXGraph) ((PXGraphExtension<CustomerMaint>) this).Base, e.Row.BAccountID);
    PXUIFieldAttribute.SetVisible<CustomerMaint.CustomerBalanceSummary.balance>(((PXSelectBase) ((PXGraphExtension<CustomerMaint>) this).Base.CustomerBalance).Cache, (object) null, flag2);
    PXUIFieldAttribute.SetVisible<CustomerMaint.CustomerBalanceSummary.consolidatedbalance>(((PXSelectBase) ((PXGraphExtension<CustomerMaint>) this).Base.CustomerBalance).Cache, (object) null, flag2 & flag3);
    PXUIFieldAttribute.SetVisible<CustomerMaint.CustomerBalanceSummary.signedDepositsBalance>(((PXSelectBase) ((PXGraphExtension<CustomerMaint>) this).Base.CustomerBalance).Cache, (object) null, flag2 && !flag3);
    PXUIFieldAttribute.SetVisible<CustomerMaint.CustomerBalanceSummary.retainageBalance>(((PXSelectBase) ((PXGraphExtension<CustomerMaint>) this).Base.CustomerBalance).Cache, (object) null, flag2);
    PXUIFieldAttribute.SetVisible<ARBalancesByBaseCuryID.consolidatedBalance>(((PXSelectBase) ((PXGraphExtension<CustomerMaint>) this).Base.Balances).Cache, (object) null, flag3);
    ((PXSelectBase) ((PXGraphExtension<CustomerMaint>) this).Base.Balances).AllowSelect = !flag2;
    if (((PXSelectBase) ((PXGraphExtension<CustomerMaint>) this).Base.Balances).AllowSelect)
    {
      IEnumerable<ARBalancesByBaseCuryID> source1 = GraphHelper.RowCast<ARBalancesByBaseCuryID>((IEnumerable) PXSelectBase<ARBalancesByBaseCuryID, PXViewOf<ARBalancesByBaseCuryID>.BasedOn<SelectFromBase<ARBalancesByBaseCuryID, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.AR.Override.BAccount>.On<BqlOperand<PX.Objects.AR.Override.BAccount.bAccountID, IBqlInt>.IsEqual<ARBalancesByBaseCuryID.customerID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.Override.BAccount.bAccountID, Equal<P.AsInt>>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.Override.BAccount.parentBAccountID, Equal<P.AsInt>>>>>.And<BqlOperand<PX.Objects.AR.Override.BAccount.consolidateToParent, IBqlBool>.IsEqual<True>>>>>.Config>.SelectMultiBound((PXGraph) ((PXGraphExtension<CustomerMaint>) this).Base, (object[]) null, new object[2]
      {
        (object) row.BAccountID,
        (object) row.BAccountID
      }));
      IEnumerable<CuryARHistory> source2 = GraphHelper.RowCast<CuryARHistory>((IEnumerable) PXSelectBase<CuryARHistory, PXSelectJoinGroupBy<CuryARHistory, InnerJoin<ARCustomerBalanceEnq.ARLatestHistory, On<ARCustomerBalanceEnq.ARLatestHistory.accountID, Equal<CuryARHistory.accountID>, And<ARCustomerBalanceEnq.ARLatestHistory.branchID, Equal<CuryARHistory.branchID>, And<ARCustomerBalanceEnq.ARLatestHistory.customerID, Equal<CuryARHistory.customerID>, And<ARCustomerBalanceEnq.ARLatestHistory.subID, Equal<CuryARHistory.subID>, And<ARCustomerBalanceEnq.ARLatestHistory.curyID, Equal<CuryARHistory.curyID>, And<ARCustomerBalanceEnq.ARLatestHistory.lastActivityPeriod, Equal<CuryARHistory.finPeriodID>>>>>>>>, Where<CuryARHistory.customerID, Equal<Current<Customer.bAccountID>>>, Aggregate<GroupBy<CuryARHistory.curyID, Sum<CuryARHistory.finYtdDeposits, Sum<CuryARHistory.finYtdRetainageWithheld, Sum<CuryARHistory.finYtdRetainageReleased>>>>>>.Config>.SelectMultiBound((PXGraph) ((PXGraphExtension<CustomerMaint>) this).Base, (object[]) null, Array.Empty<object>()));
      foreach (PXResult<ARBalancesByBaseCuryID> pxResult in ((PXSelectBase<ARBalancesByBaseCuryID>) ((PXGraphExtension<CustomerMaint>) this).Base.Balances).Select(Array.Empty<object>()))
      {
        ARBalancesByBaseCuryID arBalance = PXResult<ARBalancesByBaseCuryID>.op_Implicit(pxResult);
        arBalance.ConsolidatedBalance = new Decimal?(source1.Where<ARBalancesByBaseCuryID>((Func<ARBalancesByBaseCuryID, bool>) (a => a.BaseCuryID == arBalance.BaseCuryID)).Aggregate<ARBalancesByBaseCuryID, Decimal?>(new Decimal?(0M), (Func<Decimal?, ARBalancesByBaseCuryID, Decimal?>) ((current, bal) =>
        {
          Decimal? nullable = current;
          Decimal? currentBal = bal.CurrentBal;
          return !(nullable.HasValue & currentBal.HasValue) ? new Decimal?() : new Decimal?(nullable.GetValueOrDefault() + currentBal.GetValueOrDefault());
        })).GetValueOrDefault());
        arBalance.TotalPrepayments = source2.Where<CuryARHistory>((Func<CuryARHistory, bool>) (a => a.CuryID == arBalance.BaseCuryID)).Aggregate<CuryARHistory, Decimal?>(new Decimal?(0M), (Func<Decimal?, CuryARHistory, Decimal?>) ((current, bal) =>
        {
          Decimal? nullable = current;
          Decimal? finYtdDeposits = bal.FinYtdDeposits;
          return !(nullable.HasValue & finYtdDeposits.HasValue) ? new Decimal?() : new Decimal?(nullable.GetValueOrDefault() - finYtdDeposits.GetValueOrDefault());
        }));
        arBalance.RetainageBalance = source2.Where<CuryARHistory>((Func<CuryARHistory, bool>) (a => a.CuryID == arBalance.BaseCuryID)).Aggregate<CuryARHistory, Decimal?>(new Decimal?(0M), (Func<Decimal?, CuryARHistory, Decimal?>) ((current, bal) =>
        {
          Decimal? nullable1 = current;
          Decimal? retainageWithheld = bal.FinYtdRetainageWithheld;
          Decimal? nullable2 = nullable1.HasValue & retainageWithheld.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + retainageWithheld.GetValueOrDefault()) : new Decimal?();
          Decimal valueOrDefault;
          if (!nullable2.HasValue)
          {
            Decimal? retainageReleased = bal.FinYtdRetainageReleased;
            valueOrDefault = (retainageReleased.HasValue ? new Decimal?(0M - retainageReleased.GetValueOrDefault()) : new Decimal?()).GetValueOrDefault();
          }
          else
            valueOrDefault = nullable2.GetValueOrDefault();
          return new Decimal?(valueOrDefault);
        }));
      }
    }
    PXUIFieldAttribute.SetEnabled<Customer.cOrgBAccountID>(((PXSelectBase) ((PXGraphExtension<CustomerMaint>) this).Base.CurrentCustomer).Cache, (object) row, true);
    isBranch = row.IsBranch;
    if (!isBranch.GetValueOrDefault())
      return;
    using (new PXReadBranchRestrictedScope())
    {
      if (PXResultset<ARHistory>.op_Implicit(PXSelectBase<ARHistory, PXViewOf<ARHistory>.BasedOn<SelectFromBase<ARHistory, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.GL.Branch>.On<BqlOperand<PX.Objects.GL.Branch.branchID, IBqlInt>.IsEqual<ARHistory.branchID>>>, FbqlJoins.Inner<ARHistoryAlias>.On<BqlOperand<ARHistoryAlias.customerID, IBqlInt>.IsEqual<ARHistory.customerID>>>, FbqlJoins.Inner<BranchAlias>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<BranchAlias.branchID, Equal<ARHistoryAlias.branchID>>>>>.And<BqlOperand<BranchAlias.baseCuryID, IBqlString>.IsNotEqual<PX.Objects.GL.Branch.baseCuryID>>>>>.Where<BqlOperand<ARHistory.customerID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.SelectSingleBound((PXGraph) ((PXGraphExtension<CustomerMaint>) this).Base, (object[]) null, new object[1]
      {
        (object) row.BAccountID
      })) != null)
      {
        PXUIFieldAttribute.SetEnabled<Customer.cOrgBAccountID>(((PXSelectBase) ((PXGraphExtension<CustomerMaint>) this).Base.CurrentCustomer).Cache, (object) row, false);
      }
      else
      {
        if (!(row.Type == "VC"))
          return;
        if (PXResultset<APHistory>.op_Implicit(PXSelectBase<APHistory, PXViewOf<APHistory>.BasedOn<SelectFromBase<APHistory, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.GL.Branch>.On<BqlOperand<PX.Objects.GL.Branch.branchID, IBqlInt>.IsEqual<APHistory.branchID>>>, FbqlJoins.Inner<APHistoryAlias>.On<BqlOperand<APHistoryAlias.vendorID, IBqlInt>.IsEqual<APHistory.vendorID>>>, FbqlJoins.Inner<BranchAlias>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<BranchAlias.branchID, Equal<APHistoryAlias.branchID>>>>>.And<BqlOperand<BranchAlias.baseCuryID, IBqlString>.IsNotEqual<PX.Objects.GL.Branch.baseCuryID>>>>>.Where<BqlOperand<APHistory.vendorID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.SelectSingleBound((PXGraph) ((PXGraphExtension<CustomerMaint>) this).Base, (object[]) null, new object[1]
        {
          (object) row.BAccountID
        })) != null)
        {
          PXUIFieldAttribute.SetEnabled<Customer.cOrgBAccountID>(((PXSelectBase) ((PXGraphExtension<CustomerMaint>) this).Base.CurrentCustomer).Cache, (object) row, false);
        }
        else
        {
          if (PXResultset<ARHistory>.op_Implicit(PXSelectBase<ARHistory, PXViewOf<ARHistory>.BasedOn<SelectFromBase<ARHistory, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.GL.Branch>.On<BqlOperand<PX.Objects.GL.Branch.branchID, IBqlInt>.IsEqual<ARHistory.branchID>>>, FbqlJoins.Inner<APHistory>.On<BqlOperand<APHistory.vendorID, IBqlInt>.IsEqual<ARHistory.customerID>>>, FbqlJoins.Inner<BranchAlias>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<BranchAlias.branchID, Equal<APHistory.branchID>>>>>.And<BqlOperand<BranchAlias.baseCuryID, IBqlString>.IsNotEqual<PX.Objects.GL.Branch.baseCuryID>>>>>.Where<BqlOperand<ARHistory.customerID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.SelectSingleBound((PXGraph) ((PXGraphExtension<CustomerMaint>) this).Base, (object[]) null, new object[1]
          {
            (object) row.BAccountID
          })) == null)
            return;
          PXUIFieldAttribute.SetEnabled<Customer.cOrgBAccountID>(((PXSelectBase) ((PXGraphExtension<CustomerMaint>) this).Base.CurrentCustomer).Cache, (object) row, false);
        }
      }
    }
  }

  protected virtual void _(PX.Data.Events.RowPersisting<Customer> e)
  {
    if (e.Row == null || ((PXSelectBase) ((PXGraphExtension<CustomerMaint>) this).Base.CurrentCustomer).Cache.GetStatus((object) e.Row) != 1)
      return;
    object parentBaccountId = (object) e.Row.ParentBAccountID;
    try
    {
      ((PXSelectBase) ((PXGraphExtension<CustomerMaint>) this).Base.CurrentCustomer).Cache.RaiseFieldVerifying<Customer.parentBAccountID>((object) e.Row, ref parentBaccountId);
    }
    catch (PXSetPropertyException ex)
    {
      ((PXSelectBase) ((PXGraphExtension<CustomerMaint>) this).Base.CurrentCustomer).Cache.RaiseExceptionHandling<Customer.parentBAccountID>((object) e.Row, parentBaccountId, (Exception) ex);
    }
  }

  protected virtual void _(PX.Data.Events.RowDeleted<Customer> e)
  {
    if (e.Row == null || !(e.Row.Type == "CU") || !e.Row.IsBranch.GetValueOrDefault())
      return;
    BAccountItself baccountItself = ((PXSelectBase<BAccountItself>) ((PXGraphExtension<CustomerMaint>) this).Base.CurrentBAccountItself).SelectSingle(new object[1]
    {
      (object) e.Row.BAccountID
    });
    if (baccountItself == null)
      return;
    baccountItself.BaseCuryID = (string) null;
    baccountItself.COrgBAccountID = new int?(0);
    ((PXSelectBase<BAccountItself>) ((PXGraphExtension<CustomerMaint>) this).Base.CurrentBAccountItself).Update(baccountItself);
  }

  protected virtual void _(
    PX.Data.Events.RowSelected<CustomerMaint.CustomerBalanceSummary> e)
  {
    if (e.Row == null)
      return;
    ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CustomerMaint.CustomerBalanceSummary>>) e).Cache.GetAttributesReadonly((string) null).OfType<CurySymbolAttribute>().ToList<CurySymbolAttribute>().ForEach((Action<CurySymbolAttribute>) (attr => attr.SetSymbol((string) null)));
    if (!(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CustomerMaint.CustomerBalanceSummary>>) e).Cache.Graph.Caches[typeof (Customer)].Current is Customer current) || current.BaseCuryID != null)
      return;
    List<string> list = ServiceLocator.Current.GetInstance<ICurrentUserInformationProvider>().GetAllBranches().ToList<BranchInfo>().Select<BranchInfo, string>((Func<BranchInfo, string>) (b => PXAccess.GetBranch(new int?(b.Id)).BaseCuryID)).Distinct<string>().ToList<string>();
    if (list.Count > 1)
      return;
    PX.Objects.CM.Currency curr = CurrencyCollection.GetCurrency(list.FirstOrDefault<string>());
    ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CustomerMaint.CustomerBalanceSummary>>) e).Cache.GetAttributesReadonly((string) null).OfType<CurySymbolAttribute>().ToList<CurySymbolAttribute>().ForEach((Action<CurySymbolAttribute>) (attr => attr.SetSymbol(curr?.CurySymbol)));
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<Customer, Customer.cOrgBAccountID> e)
  {
    if (e.Row == null)
      return;
    int? newValue1 = (int?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<Customer, Customer.cOrgBAccountID>, Customer, object>) e).NewValue;
    int? oldValue = (int?) e.OldValue;
    if (newValue1.GetValueOrDefault() == oldValue.GetValueOrDefault() & newValue1.HasValue == oldValue.HasValue)
      return;
    Customer row = e.Row;
    string baseCuryId = PXOrgAccess.GetBaseCuryID(new int?((int) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<Customer, Customer.cOrgBAccountID>, Customer, object>) e).NewValue));
    if (row.BaseCuryID != baseCuryId)
    {
      ARHistory arHistory = (ARHistory) null;
      using (new PXReadBranchRestrictedScope())
        arHistory = PXResultset<ARHistory>.op_Implicit(PXSelectBase<ARHistory, PXViewOf<ARHistory>.BasedOn<SelectFromBase<ARHistory, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.GL.Branch>.On<BqlOperand<PX.Objects.GL.Branch.branchID, IBqlInt>.IsEqual<ARHistory.branchID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARHistory.customerID, Equal<P.AsInt>>>>>.And<BqlOperand<PX.Objects.GL.Branch.baseCuryID, IBqlString>.IsNotEqual<P.AsString>>>>.Config>.SelectSingleBound((PXGraph) ((PXGraphExtension<CustomerMaint>) this).Base, (object[]) null, new object[2]
        {
          (object) row.BAccountID,
          (object) baseCuryId
        }));
      if (arHistory != null)
      {
        ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<Customer, Customer.cOrgBAccountID>, Customer, object>) e).NewValue = (object) PXOrgAccess.GetCD(new int?((int) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<Customer, Customer.cOrgBAccountID>, Customer, object>) e).NewValue));
        throw new PXSetPropertyException("An entity with the base currency other than {0} cannot be associated with {1}, because there are AR documents for the customer.", (PXErrorLevel) 4, new object[2]
        {
          (object) PXAccess.GetBranch(arHistory.BranchID).BaseCuryID,
          (object) row.AcctCD.Trim()
        });
      }
      if (row.Type == "VC")
      {
        int? newValue2 = (int?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<Customer, Customer.cOrgBAccountID>, Customer, object>) e).NewValue;
        int? vorgBaccountId = row.VOrgBAccountID;
        if (!(newValue2.GetValueOrDefault() == vorgBaccountId.GetValueOrDefault() & newValue2.HasValue == vorgBaccountId.HasValue))
        {
          APHistory apHistory = (APHistory) null;
          using (new PXReadBranchRestrictedScope())
            apHistory = PXResultset<APHistory>.op_Implicit(PXSelectBase<APHistory, PXViewOf<APHistory>.BasedOn<SelectFromBase<APHistory, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.GL.Branch>.On<BqlOperand<PX.Objects.GL.Branch.branchID, IBqlInt>.IsEqual<APHistory.branchID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<APHistory.vendorID, Equal<P.AsInt>>>>>.And<BqlOperand<PX.Objects.GL.Branch.baseCuryID, IBqlString>.IsNotEqual<P.AsString>>>>.Config>.SelectSingleBound((PXGraph) ((PXGraphExtension<CustomerMaint>) this).Base, (object[]) null, new object[2]
            {
              (object) row.BAccountID,
              (object) baseCuryId
            }));
          if (apHistory != null)
          {
            ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<Customer, Customer.cOrgBAccountID>, Customer, object>) e).NewValue = (object) PXOrgAccess.GetCD(new int?((int) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<Customer, Customer.cOrgBAccountID>, Customer, object>) e).NewValue));
            PXAccess.MasterCollection.Branch branch1 = PXAccess.GetBranch(apHistory.BranchID);
            PXAccess.MasterCollection.Branch branch2 = !VisibilityRestriction.IsEmpty(row.COrgBAccountID) ? PXAccess.GetBranchByBAccountID(row.VOrgBAccountID) : throw new PXSetPropertyException("The entity with the base currency other than {0} cannot be associated with {1}, because the customer has been extended as a vendor and there are AP documents for the vendor.", (PXErrorLevel) 4, new object[2]
            {
              (object) branch1.BaseCuryID,
              (object) row.AcctCD.Trim()
            });
            PXAccess.MasterCollection.Organization organizationByBaccountId = PXAccess.GetOrganizationByBAccountID(row.VOrgBAccountID);
            throw new PXSetPropertyException("An entity with the base currency other than {0} cannot be associated with {1}, because the customer has been extended as a vendor whose visibility is limited to the {2} entity with the {3} base currency and there are AP documents for the vendor.", (PXErrorLevel) 4, new object[4]
            {
              (object) branch1.BaseCuryID,
              (object) row.AcctCD.Trim(),
              (object) (branch2?.BranchCD?.Trim() ?? ((PXAccess.Organization) organizationByBaccountId)?.OrganizationCD?.Trim()),
              (object) (branch2?.BaseCuryID ?? ((PXAccess.Organization) organizationByBaccountId)?.BaseCuryID)
            });
          }
          bool? isBranch;
          if (VisibilityRestriction.IsNotEmpty(row.VOrgBAccountID))
          {
            isBranch = row.IsBranch;
            if (isBranch.GetValueOrDefault() || VisibilityRestriction.IsNotEmpty((int?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<Customer, Customer.cOrgBAccountID>, Customer, object>) e).NewValue))
            {
              PXAccess.MasterCollection.Branch branchByBaccountId = PXAccess.GetBranchByBAccountID(row.VOrgBAccountID);
              PXAccess.MasterCollection.Organization organizationByBaccountId = PXAccess.GetOrganizationByBAccountID(row.VOrgBAccountID);
              if (((PXSelectBase) ((PXGraphExtension<CustomerMaint>) this).Base.BAccount).View.Ask(PXMessages.LocalizeFormatNoPrefix("The {0} customer has been extended as a vendor whose visibility is limited to the {1} entity with the {2} base currency. Do you want to change the visibility of both the customer and vendor accounts? To proceed, click Yes.", new object[3]
              {
                (object) row.AcctCD.Trim(),
                (object) (branchByBaccountId?.BranchCD?.Trim() ?? ((PXAccess.Organization) organizationByBaccountId)?.OrganizationCD?.Trim()),
                (object) (branchByBaccountId?.BaseCuryID ?? ((PXAccess.Organization) organizationByBaccountId)?.BaseCuryID)
              }), (MessageButtons) 4) == 6)
              {
                object newValue3 = ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<Customer, Customer.cOrgBAccountID>, Customer, object>) e).NewValue;
                ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<Customer, Customer.cOrgBAccountID>>) e).Cache.RaiseFieldVerifying<PX.Objects.CR.BAccount.vOrgBAccountID>((object) e.Row, ref newValue3);
                row.VOrgBAccountID = (int?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<Customer, Customer.cOrgBAccountID>, Customer, object>) e).NewValue;
                goto label_31;
              }
              ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<Customer, Customer.cOrgBAccountID>, Customer, object>) e).NewValue = e.OldValue;
              ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<Customer, Customer.cOrgBAccountID>>) e).Cancel = true;
              return;
            }
          }
          isBranch = row.IsBranch;
          if (isBranch.GetValueOrDefault() || VisibilityRestriction.IsNotEmpty((int?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<Customer, Customer.cOrgBAccountID>, Customer, object>) e).NewValue))
          {
            if (((PXSelectBase) ((PXGraphExtension<CustomerMaint>) this).Base.BAccount).View.Ask(PXMessages.LocalizeFormatNoPrefix(" The {0} customer has been extended as a vendor whose visibility is not limited to any entity. Do you want to change the visibility of both the customer and vendor accounts? To proceed, click Yes.", new object[1]
            {
              (object) row.AcctCD
            }), (MessageButtons) 4) == 6)
            {
              object newValue4 = ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<Customer, Customer.cOrgBAccountID>, Customer, object>) e).NewValue;
              ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<Customer, Customer.cOrgBAccountID>>) e).Cache.RaiseFieldVerifying<PX.Objects.CR.BAccount.vOrgBAccountID>((object) e.Row, ref newValue4);
              row.VOrgBAccountID = (int?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<Customer, Customer.cOrgBAccountID>, Customer, object>) e).NewValue;
            }
            else
            {
              ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<Customer, Customer.cOrgBAccountID>, Customer, object>) e).NewValue = e.OldValue;
              ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<Customer, Customer.cOrgBAccountID>>) e).Cancel = true;
              return;
            }
          }
        }
      }
    }
label_31:
    if (((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<Customer, Customer.cOrgBAccountID>, Customer, object>) e).NewValue == null || (int) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<Customer, Customer.cOrgBAccountID>, Customer, object>) e).NewValue == 0)
      return;
    Customer customer = PXResultset<Customer>.op_Implicit(PXSelectBase<Customer, PXViewOf<Customer>.BasedOn<SelectFromBase<Customer, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Customer.parentBAccountID, Equal<P.AsInt>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Customer.consolidateToParent, Equal<True>>>>>.And<BqlOperand<Customer.baseCuryID, IBqlString>.IsNotEqual<P.AsString>>>>>.Config>.SelectSingleBound((PXGraph) ((PXGraphExtension<CustomerMaint>) this).Base, (object[]) null, new object[2]
    {
      (object) row.BAccountID,
      (object) baseCuryId
    }));
    if (customer != null)
    {
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<Customer, Customer.cOrgBAccountID>, Customer, object>) e).NewValue = (object) PXOrgAccess.GetCD(new int?((int) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<Customer, Customer.cOrgBAccountID>, Customer, object>) e).NewValue));
      throw new PXSetPropertyException("An entity with the base currency other than {0} cannot be associated with the customer, because it is the parent account for the {1} customer whose usage is limited to {2} with the {3} base currency.", (PXErrorLevel) 4, new object[4]
      {
        (object) e.Row.BaseCuryID,
        (object) customer.AcctCD,
        (object) PXOrgAccess.GetCD(customer.COrgBAccountID),
        (object) PXOrgAccess.GetBaseCuryID(customer.COrgBAccountID)
      });
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<Customer, Customer.cOrgBAccountID> e)
  {
    int? oldValue = (int?) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<Customer, Customer.cOrgBAccountID>, Customer, object>) e).OldValue;
    int? newValue1 = (int?) e.NewValue;
    if (oldValue.GetValueOrDefault() == newValue1.GetValueOrDefault() & oldValue.HasValue == newValue1.HasValue)
      return;
    int? newValue2 = (int?) e.NewValue;
    int num1 = 0;
    if (newValue2.GetValueOrDefault() == num1 & newValue2.HasValue && e.Row.Type == "VC")
    {
      int? vorgBaccountId = e.Row.VOrgBAccountID;
      int num2 = 0;
      if (!(vorgBaccountId.GetValueOrDefault() == num2 & vorgBaccountId.HasValue))
        goto label_5;
    }
    e.Row.BaseCuryID = PXOrgAccess.GetBaseCuryID(e.Row.COrgBAccountID) ?? (e.Row.IsBranch.GetValueOrDefault() ? (string) null : ((PXGraph) ((PXGraphExtension<CustomerMaint>) this).Base).Accessinfo.BaseCuryID);
label_5:
    if (!e.Row.ParentBAccountID.HasValue)
      return;
    object parentBaccountId = (object) e.Row.ParentBAccountID;
    try
    {
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<Customer, Customer.cOrgBAccountID>>) e).Cache.RaiseFieldVerifying<Customer.parentBAccountID>((object) e.Row, ref parentBaccountId);
    }
    catch (PXSetPropertyException ex)
    {
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<Customer, Customer.cOrgBAccountID>>) e).Cache.RaiseExceptionHandling<Customer.parentBAccountID>((object) e.Row, parentBaccountId, (Exception) ex);
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<Customer, Customer.parentBAccountID> e)
  {
    if (((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<Customer, Customer.parentBAccountID>, Customer, object>) e).NewValue != null && PXSelectorAttribute.Select<Customer.parentBAccountID>(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<Customer, Customer.parentBAccountID>>) e).Cache, (object) e.Row, (object) (int) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<Customer, Customer.parentBAccountID>, Customer, object>) e).NewValue) is BAccountR baccountR && baccountR.BaseCuryID != null && baccountR.BaseCuryID != e.Row.BaseCuryID && e.Row.ConsolidateToParent.GetValueOrDefault())
    {
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<Customer, Customer.parentBAccountID>, Customer, object>) e).NewValue = (object) baccountR.AcctCD;
      throw new PXSetPropertyException("The {0} account cannot be used as a parent account, because its usage is limited to the entity whose base currency differs from the base currency of {1} associated with the {2} customer.", (PXErrorLevel) 4, new object[3]
      {
        (object) baccountR.AcctCD,
        (object) PXOrgAccess.GetCD(e.Row.COrgBAccountID),
        (object) e.Row.AcctCD
      });
    }
  }

  protected virtual void _(PX.Data.Events.RowUpdated<Customer> e)
  {
    if (!(e.OldRow.BaseCuryID != e.Row.BaseCuryID))
      return;
    foreach (PXResult<CustomerPaymentMethod> pxResult in PXSelectBase<CustomerPaymentMethod, PXViewOf<CustomerPaymentMethod>.BasedOn<SelectFromBase<CustomerPaymentMethod, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<CustomerPaymentMethod.bAccountID, IBqlInt>.IsEqual<BqlField<Customer.bAccountID, IBqlInt>.FromCurrent>>>.Config>.SelectMultiBound((PXGraph) ((PXGraphExtension<CustomerMaint>) this).Base, new object[1]
    {
      (object) e.Row
    }, (object[]) null))
    {
      CustomerPaymentMethod customerPaymentMethod = PXResult<CustomerPaymentMethod>.op_Implicit(pxResult);
      customerPaymentMethod.CashAccountID = new int?();
      if (GraphHelper.Caches<CustomerPaymentMethod>((PXGraph) ((PXGraphExtension<CustomerMaint>) this).Base).GetStatus(customerPaymentMethod) == null)
        GraphHelper.MarkUpdated((PXCache) GraphHelper.Caches<CustomerPaymentMethod>((PXGraph) ((PXGraphExtension<CustomerMaint>) this).Base), (object) customerPaymentMethod);
    }
    PXResultset<CustomerPaymentMethodInfo> source = ((PXSelectBase<CustomerPaymentMethodInfo>) ((PXGraph) ((PXGraphExtension<CustomerMaint>) this).Base).GetExtension<CustomerMaint.PaymentDetailsExt>().PaymentMethods).Select(Array.Empty<object>());
    Expression<Func<PXResult<CustomerPaymentMethodInfo>, bool>> predicate = (Expression<Func<PXResult<CustomerPaymentMethodInfo>, bool>>) (m => ((CustomerPaymentMethodInfo) m).BAccountID != new int?());
    foreach (PXResult<CustomerPaymentMethodInfo> pxResult in (IEnumerable<PXResult<CustomerPaymentMethodInfo>>) ((IQueryable<PXResult<CustomerPaymentMethodInfo>>) source).Where<PXResult<CustomerPaymentMethodInfo>>(predicate))
      PXResult<CustomerPaymentMethodInfo>.op_Implicit(pxResult).CashAccountID = new int?();
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<Customer, Customer.consolidateToParent> e)
  {
    if (!(bool) e.NewValue)
      return;
    object parentBaccountId = (object) e.Row.ParentBAccountID;
    try
    {
      ((PXSelectBase) ((PXGraphExtension<CustomerMaint>) this).Base.CurrentCustomer).Cache.RaiseFieldVerifying<Customer.parentBAccountID>((object) e.Row, ref parentBaccountId);
    }
    catch (PXSetPropertyException ex)
    {
      ((PXSelectBase) ((PXGraphExtension<CustomerMaint>) this).Base.CurrentCustomer).Cache.RaiseExceptionHandling<Customer.parentBAccountID>((object) e.Row, parentBaccountId, (Exception) ex);
    }
  }

  [PXMergeAttributes]
  [PXRestrictor(typeof (Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<BAccountR.baseCuryID, Equal<BqlField<Customer.baseCuryID, IBqlString>.FromCurrent>>>>, Or<BqlOperand<Current<Customer.consolidateToParent>, IBqlBool>.IsEqual<False>>>>.Or<BqlOperand<BAccountR.baseCuryID, IBqlString>.IsNull>>), "Cannot be used as a parent account.", new System.Type[] {})]
  protected void Customer_ParentBAccountID_CacheAttached(PXCache sender)
  {
  }

  [CashAccount(null, typeof (Search2<PX.Objects.CA.CashAccount.cashAccountID, InnerJoin<PaymentMethodAccount, On<PaymentMethodAccount.cashAccountID, Equal<PX.Objects.CA.CashAccount.cashAccountID>, And<PaymentMethodAccount.useForAR, Equal<True>, And<PaymentMethodAccount.paymentMethodID, Equal<Current<CustomerPaymentMethod.paymentMethodID>>>>>>, Where2<Match<Current<AccessInfo.userName>>, And<Where<PX.Objects.CA.CashAccount.baseCuryID, Equal<Current<Customer.baseCuryID>>>>>>))]
  [PXDefault(typeof (Search<PX.Objects.CA.PaymentMethod.defaultCashAccountID, Where<PX.Objects.CA.PaymentMethod.paymentMethodID, Equal<Current<CustomerPaymentMethod.paymentMethodID>>>>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<CustomerPaymentMethod.cashAccountID> e)
  {
  }

  [PXOverride]
  public IEnumerable CustomerStatement(
    PXAdapter adapter,
    CustomerMaintMultipleBaseCurrencies.CustomerStatementDelegate baseMethod)
  {
    Customer current = ((PXSelectBase<Customer>) ((PXGraphExtension<CustomerMaint>) this).Base.CurrentCustomer).Current;
    if (current == null)
      return adapter.Get();
    if (PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>() && ((PXSelectBase<ARSetup>) ((PXGraphExtension<CustomerMaint>) this).Base.ARSetup).Current.PrepareStatements.Equals("A") && current.BaseCuryID == null)
      throw new PXException("The statements cannot be printed because it is not allowed to consolidate statements for all companies if the Multiple Base Currency feature is enabled. Select a different option in the Prepare Statements box on the Accounts Receivable Preferences (AR101000) form.");
    return baseMethod(adapter);
  }

  [PXOverride]
  public IEnumerable customerBalance(
    CustomerMaintMultipleBaseCurrencies.customerBalanceDelegate baseMethod)
  {
    using (new ForceUseBranchRestrictionsScope())
      return baseMethod();
  }

  protected virtual IEnumerable balances()
  {
    List<ARBalancesByBaseCuryID> source = new List<ARBalancesByBaseCuryID>();
    foreach (PXResult<ARBalancesByBaseCuryID> pxResult in PXSelectBase<ARBalancesByBaseCuryID, PXSelect<ARBalancesByBaseCuryID, Where<ARBalancesByBaseCuryID.customerID, Equal<Current<Customer.bAccountID>>>>.Config>.Select((PXGraph) ((PXGraphExtension<CustomerMaint>) this).Base, Array.Empty<object>()))
      source.Add(PXResult<ARBalancesByBaseCuryID>.op_Implicit(pxResult));
    List<string> foundCurrencies = source.Select<ARBalancesByBaseCuryID, string>((Func<ARBalancesByBaseCuryID, string>) (_ => _.BaseCuryID)).Distinct<string>().ToList<string>();
    List<string> list = ServiceLocator.Current.GetInstance<ICurrentUserInformationProvider>().GetAllBranches().ToList<BranchInfo>().Select<BranchInfo, string>((Func<BranchInfo, string>) (b => PXAccess.GetBranch(new int?(b.Id)).BaseCuryID)).Distinct<string>().ToList<string>();
    Dictionary<string, Decimal> dictionary = new Dictionary<string, Decimal>();
    if (PXAccess.FeatureInstalled<FeaturesSet.parentChildAccount>())
    {
      Customer current = ((PXGraphExtension<CustomerMaint>) this).Base.BAccountAccessor.Current;
      dictionary = GraphHelper.RowCast<CustomerMaint.CustomerBalances>((IEnumerable) PXSelectBase<CustomerMaint.CustomerBalances, PXSelectJoinGroupBy<CustomerMaint.CustomerBalances, InnerJoin<PX.Objects.AR.Override.BAccount, On<PX.Objects.AR.Override.BAccount.bAccountID, Equal<CustomerMaint.CustomerBalances.customerID>>>, Where2<Where<PX.Objects.AR.Override.BAccount.consolidateToParent, Equal<True>, And<PX.Objects.AR.Override.BAccount.parentBAccountID, Equal<Required<PX.Objects.AR.Override.Customer.bAccountID>>>>, Or<PX.Objects.AR.Override.BAccount.bAccountID, Equal<Required<Customer.bAccountID>>>>, Aggregate<GroupBy<CustomerMaint.CustomerBalances.baseCuryID, Sum<CustomerMaint.CustomerBalances.balance, Sum<CustomerMaint.CustomerBalances.unreleasedBalance, Sum<CustomerMaint.CustomerBalances.openOrdersBalance, Min<CustomerMaint.CustomerBalances.oldInvoiceDate>>>>>>>.Config>.Select((PXGraph) ((PXGraphExtension<CustomerMaint>) this).Base, new object[2]
      {
        (object) current.BAccountID,
        (object) current.BAccountID
      })).ToDictionary<CustomerMaint.CustomerBalances, string, Decimal>((Func<CustomerMaint.CustomerBalances, string>) (_ => _.BaseCuryID), (Func<CustomerMaint.CustomerBalances, Decimal>) (_ => _.Balance.GetValueOrDefault()));
    }
    foreach (string key in list.Where<string>((Func<string, bool>) (c => !foundCurrencies.Contains(c))))
      source.Add(new ARBalancesByBaseCuryID()
      {
        CustomerID = ((PXSelectBase<Customer>) ((PXGraphExtension<CustomerMaint>) this).Base.BAccount).Current.BAccountID,
        BaseCuryID = key,
        ConsolidatedBalance = new Decimal?(dictionary.ContainsKey(key) ? dictionary[key] : 0M),
        CurrentBal = new Decimal?(0M),
        RetainageBalance = new Decimal?(0M),
        TotalPrepayments = new Decimal?(0M),
        UnreleasedBal = new Decimal?(0M)
      });
    return (IEnumerable) source.OrderBy<ARBalancesByBaseCuryID, string>((Func<ARBalancesByBaseCuryID, string>) (_ => _.BaseCuryID));
  }

  protected virtual void _(
    PX.Data.Events.RowSelecting<CustomerMaint.ChildCustomerBalanceSummary> e)
  {
    CustomerMaint.ChildCustomerBalanceSummary row = e.Row;
    if (row == null || row.BaseCuryID != null)
      return;
    row.BaseCuryID = PXAccess.GetBranchByBAccountID(row.CustomerID)?.BaseCuryID;
  }

  public delegate IEnumerable CustomerStatementDelegate(PXAdapter adapter);

  public delegate IEnumerable customerBalanceDelegate();
}
