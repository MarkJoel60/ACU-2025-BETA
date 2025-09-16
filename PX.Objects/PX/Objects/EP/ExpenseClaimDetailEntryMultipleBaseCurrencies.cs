// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.ExpenseClaimDetailEntryMultipleBaseCurrencies
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
namespace PX.Objects.EP;

public sealed class ExpenseClaimDetailEntryMultipleBaseCurrencies : 
  PXGraphExtension<ExpenseClaimDetailEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>();

  protected void _(
    PX.Data.Events.FieldVerifying<EPExpenseClaimDetails, EPExpenseClaimDetails.branchID> e)
  {
    if (this.Base.ClaimDetails.Current == null)
      return;
    int? nullable1 = this.Base.ClaimDetails.Current.EmployeeID;
    if (!nullable1.HasValue)
      return;
    int? newValue = e.NewValue as int?;
    nullable1 = newValue;
    int num = 0;
    if (!(nullable1.GetValueOrDefault() > num & nullable1.HasValue))
      return;
    PXUIFieldAttribute.SetError<EPExpenseClaimDetails.branchID>(this.Base.ClaimDetails.Cache, (object) this.Base.ClaimDetails.Current, (string) null);
    PXAccess.MasterCollection.Branch branch = PXAccess.GetBranch(newValue);
    ExpenseClaimDetailEntry graph = this.Base;
    EPExpenseClaimDetails current = this.Base.ClaimDetails.Current;
    int? nullable2;
    if (current == null)
    {
      nullable1 = new int?();
      nullable2 = nullable1;
    }
    else
      nullable2 = current.EmployeeID;
    // ISSUE: variable of a boxed type
    __Boxed<int?> field0 = (ValueType) nullable2;
    object[] objArray = Array.Empty<object>();
    EPEmployee epEmployee = (EPEmployee) PXSelectBase<EPEmployee, PXSelect<EPEmployee>.Config>.Search<EPEmployee.bAccountID>((PXGraph) graph, (object) field0, objArray);
    if (epEmployee.BaseCuryID != branch.BaseCuryID)
    {
      e.NewValue = (object) branch.BranchCD;
      throw new PXSetPropertyException("The base currency of the selected branch differs from the base currency of the {0} employee.", new object[1]
      {
        (object) epEmployee.AcctCD
      });
    }
  }

  protected void _(
    PX.Data.Events.FieldUpdated<EPExpenseClaimDetails.branchID> e)
  {
    EPExpenseClaimDetails row = e.Row as EPExpenseClaimDetails;
    if (!row.BranchID.HasValue)
      return;
    PXAccess.MasterCollection.Branch branch = PXAccess.GetBranch(row.BranchID);
    if (row.CuryID == branch.BaseCuryID)
    {
      PX.Objects.CM.CurrencyInfo currencyInfo = PX.Objects.CM.CurrencyInfoAttribute.SetDefaults<EPExpenseClaim.curyInfoID>(e.Cache, (object) row);
      if (currencyInfo == null)
        return;
      row.CuryID = currencyInfo.CuryID;
    }
    else
    {
      EPEmployee employee = (EPEmployee) PXSelectBase<EPEmployee, PXSelect<EPEmployee>.Config>.Search<EPEmployee.bAccountID>((PXGraph) this.Base, (object) (int?) this.Base.ClaimDetails.Current?.EmployeeID);
      PX.Objects.CM.CurrencyInfo rate = ExpenseClaimDetailEntryMultipleBaseCurrencies.CreateRate((PXGraph) this.Base, row.CuryID, branch.BaseCuryID, row.ExpenseDate, employee);
      if (rate == null)
        return;
      row.CuryID = rate.CuryID;
      row.CuryInfoID = rate.CuryInfoID;
    }
  }

  protected void _(
    PX.Data.Events.FieldUpdated<EPExpenseClaimDetails.corpCardID> e)
  {
    EPExpenseClaimDetails row = e.Row as EPExpenseClaimDetails;
    this.CalculateCorpCurrencyInfo(e.Cache, row);
  }

  protected void _(
    PX.Data.Events.FieldUpdated<EPExpenseClaimDetails.employeeID> e)
  {
    EPExpenseClaimDetails row = e.Row as EPExpenseClaimDetails;
    this.CalculateCorpCurrencyInfo(e.Cache, row);
  }

  protected void _(
    PX.Data.Events.FieldUpdated<EPExpenseClaimDetails.billable> e)
  {
    EPExpenseClaimDetails row = e.Row as EPExpenseClaimDetails;
    if (!(e.NewValue as bool?).GetValueOrDefault() || !row.CustomerID.HasValue)
      return;
    PXAccess.MasterCollection.Branch branch = PXAccess.GetBranch(row.BranchID);
    PX.Objects.AR.Customer customer = PX.Objects.AR.Customer.PK.Find((PXGraph) this.Base, row.CustomerID);
    if (!(customer.BaseCuryID != branch.BaseCuryID))
      return;
    PXUIFieldAttribute.SetError<EPExpenseClaim.customerID>(e.Cache, e.Row, $"The base currency of the expense receipt differs from the base currency of the entity associated with the {customer.AcctCD} account.", customer.AcctCD);
  }

  protected void _(
    PX.Data.Events.FieldVerifying<EPExpenseClaimDetails, EPExpenseClaimDetails.customerID> e)
  {
    if (e.Row == null)
      return;
    EPExpenseClaimDetails row = e.Row;
    int? newValue = e.NewValue as int?;
    int? nullable = newValue;
    int num = 0;
    if (!(nullable.GetValueOrDefault() > num & nullable.HasValue))
      return;
    PXUIFieldAttribute.SetError<EPExpenseClaim.customerID>(this.Base.ClaimDetails.Cache, (object) this.Base.ClaimDetails.Current, (string) null);
    if (!row.Billable.GetValueOrDefault())
      return;
    PXAccess.MasterCollection.Branch branch = PXAccess.GetBranch(row.BranchID);
    PX.Objects.AR.Customer customer = PX.Objects.AR.Customer.PK.Find((PXGraph) this.Base, newValue);
    if (!(customer.BaseCuryID != branch.BaseCuryID))
      return;
    e.NewValue = (object) customer.AcctCD;
    e.Cancel = true;
    PXUIFieldAttribute.SetError<EPExpenseClaim.customerID>(e.Cache, (object) e.Row, $"The base currency of the expense receipt differs from the base currency of the entity associated with the {customer.AcctCD} account.", customer.AcctCD);
  }

  private static PX.Objects.CM.CurrencyInfo CreateRate(
    PXGraph graph,
    string curyID,
    string baseCuryID,
    DateTime? date,
    EPEmployee employee)
  {
    IPXCurrencyService pxCurrencyService = ServiceLocator.Current.GetInstance<Func<PXGraph, IPXCurrencyService>>()(graph);
    PX.Objects.CM.CurrencyInfo currencyInfo = new PX.Objects.CM.CurrencyInfo();
    currencyInfo.ModuleCode = "EP";
    currencyInfo.BaseCuryID = baseCuryID;
    currencyInfo.CuryID = curyID;
    currencyInfo.CuryRateTypeID = employee != null ? employee.CuryRateTypeID ?? pxCurrencyService.DefaultRateTypeID("CA") : pxCurrencyService.DefaultRateTypeID("CA");
    currencyInfo.CuryEffDate = date;
    IPXCurrencyRate rate = pxCurrencyService.GetRate(currencyInfo.CuryID, currencyInfo.BaseCuryID, currencyInfo.CuryRateTypeID, currencyInfo.CuryEffDate);
    if (rate == null)
    {
      PX.Objects.CM.CurrencyInfo data = new PX.Objects.CM.CurrencyInfo();
      graph.Caches[typeof (PX.Objects.CM.CurrencyInfo)].SetDefaultExt<PX.Objects.CM.CurrencyInfo.curyRate>((object) data);
      graph.Caches[typeof (PX.Objects.CM.CurrencyInfo)].SetDefaultExt<PX.Objects.CM.CurrencyInfo.curyMultDiv>((object) data);
      graph.Caches[typeof (PX.Objects.CM.CurrencyInfo)].SetDefaultExt<PX.Objects.CM.CurrencyInfo.recipRate>((object) data);
      currencyInfo.CuryRate = new Decimal?(Math.Round(data.CuryRate.Value, 8));
      currencyInfo.CuryMultDiv = data.CuryMultDiv;
      currencyInfo.RecipRate = new Decimal?(Math.Round(data.RecipRate.Value, 8));
    }
    else
    {
      currencyInfo.CuryRate = rate.CuryRate;
      currencyInfo.CuryMultDiv = rate.CuryMultDiv;
      currencyInfo.RecipRate = rate.RateReciprocal;
    }
    return (PX.Objects.CM.CurrencyInfo) graph.Caches[typeof (PX.Objects.CM.CurrencyInfo)].Insert((object) currencyInfo);
  }

  private void CalculateCorpCurrencyInfo(PXCache cache, EPExpenseClaimDetails row)
  {
    if (!row.BranchID.HasValue || row.CardCuryID == null)
      return;
    PXAccess.MasterCollection.Branch branch = PXAccess.GetBranch(row.BranchID);
    EPEmployee employee = (EPEmployee) PXSelectBase<EPEmployee, PXSelect<EPEmployee>.Config>.Search<EPEmployee.bAccountID>((PXGraph) this.Base, (object) (int?) this.Base.ClaimDetails.Current?.EmployeeID);
    PX.Objects.CM.CurrencyInfo rate = ExpenseClaimDetailEntryMultipleBaseCurrencies.CreateRate((PXGraph) this.Base, row.CardCuryID, branch.BaseCuryID, row.ExpenseDate, employee);
    if (rate == null)
      return;
    cache.SetValueExt<EPExpenseClaimDetails.cardCuryID>((object) row, (object) rate.CuryID);
    cache.SetValueExt<EPExpenseClaimDetails.cardCuryInfoID>((object) row, (object) rate.CuryInfoID);
    if (!row.IsPaidWithCard)
      return;
    cache.SetValueExt<EPExpenseClaimDetails.claimCuryInfoID>((object) row, (object) rate.CuryInfoID);
  }
}
