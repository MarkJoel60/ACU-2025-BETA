// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.ExpenseClaimEntryMultipleBaseCurrencies
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.EP;

public sealed class ExpenseClaimEntryMultipleBaseCurrencies : PXGraphExtension<ExpenseClaimEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>();

  protected void _(
    PX.Data.Events.FieldVerifying<EPExpenseClaim, EPExpenseClaim.branchID> e)
  {
    if (this.Base.ExpenseClaimCurrent.Current == null)
      return;
    int? nullable = this.Base.ExpenseClaimCurrent.Current.EmployeeID;
    if (!nullable.HasValue)
      return;
    int? newValue = e.NewValue as int?;
    nullable = newValue;
    int num = 0;
    if (!(nullable.GetValueOrDefault() > num & nullable.HasValue))
      return;
    PXUIFieldAttribute.SetError<EPExpenseClaim.branchID>(this.Base.ExpenseClaimCurrent.Cache, (object) this.Base.ExpenseClaimCurrent.Current, (string) null);
    PXAccess.MasterCollection.Branch branch = PXAccess.GetBranch(newValue);
    EPEmployee epEmployee = (EPEmployee) PXSelectBase<EPEmployee, PXSelect<EPEmployee>.Config>.Search<EPEmployee.bAccountID>((PXGraph) this.Base, (object) this.Base.ExpenseClaimCurrent.Current.EmployeeID);
    if (epEmployee.BaseCuryID != branch.BaseCuryID)
    {
      e.NewValue = (object) branch.BranchCD;
      e.Cancel = true;
      throw new PXSetPropertyException("The base currency of the selected branch differs from the base currency of the {0} employee.", new object[1]
      {
        (object) epEmployee.AcctCD
      });
    }
  }

  protected void _(
    PX.Data.Events.FieldVerifying<EPExpenseClaim, EPExpenseClaim.customerID> e)
  {
    EPExpenseClaim row = e.Row;
    int? newValue = e.NewValue as int?;
    int? nullable = newValue;
    int num = 0;
    if (!(nullable.GetValueOrDefault() > num & nullable.HasValue))
      return;
    PXUIFieldAttribute.SetError<EPExpenseClaim.customerID>(this.Base.ExpenseClaimCurrent.Cache, (object) this.Base.ExpenseClaimCurrent.Current, (string) null);
    if (!((IEnumerable<EPExpenseClaimDetails>) this.Base.ExpenseClaimDetails.Select<EPExpenseClaimDetails>()).AsEnumerable<EPExpenseClaimDetails>().Any<EPExpenseClaimDetails>((Func<EPExpenseClaimDetails, bool>) (a => a.Billable.GetValueOrDefault())))
      return;
    PXAccess.MasterCollection.Branch branch = PXAccess.GetBranch(row.BranchID);
    PX.Objects.AR.Customer customer = PX.Objects.AR.Customer.PK.Find((PXGraph) this.Base, newValue);
    if (customer.BaseCuryID != branch.BaseCuryID)
    {
      e.NewValue = (object) customer.AcctCD;
      e.Cancel = true;
      throw new PXSetPropertyException("The base currency of the expense claim differs from the base currency of the entity associated with the {0} account.", new object[1]
      {
        (object) customer.AcctCD
      });
    }
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
    PXUIFieldAttribute.SetError<EPExpenseClaimDetails.customerID>(this.Base.ExpenseClaimDetailsCurrent.Cache, (object) this.Base.ExpenseClaimDetailsCurrent.Current, (string) null);
    if (!row.Billable.GetValueOrDefault())
      return;
    PXAccess.MasterCollection.Branch branch = PXAccess.GetBranch(row.BranchID);
    PX.Objects.AR.Customer customer = PX.Objects.AR.Customer.PK.Find((PXGraph) this.Base, newValue);
    if (!(customer.BaseCuryID != branch.BaseCuryID))
      return;
    e.NewValue = (object) customer.AcctCD;
    e.Cancel = true;
    PXUIFieldAttribute.SetError<EPExpenseClaimDetails.customerID>(this.Base.ExpenseClaimDetailsCurrent.Cache, (object) this.Base.ExpenseClaimDetailsCurrent.Current, $"The base currency of the expense receipt differs from the base currency of the entity associated with the {customer.AcctCD} account.", customer.AcctCD);
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
    PX.Data.Events.FieldUpdated<EPExpenseClaimDetails.paidWith> e)
  {
    if (e.Row == null)
      return;
    PXUIFieldAttribute.SetError<EPExpenseClaimDetails.corpCardID>(this.Base.ExpenseClaimDetailsCurrent.Cache, (object) this.Base.ExpenseClaimDetailsCurrent.Current, (string) null);
    e.Cache.SetValueExt<EPExpenseClaimDetails.corpCardID>((object) this.Base.ExpenseClaimDetailsCurrent.Current, (object) null);
  }
}
