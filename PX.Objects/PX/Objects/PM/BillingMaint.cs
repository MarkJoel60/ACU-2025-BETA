// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.BillingMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.EP;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.PM;

public class BillingMaint : PXGraph<BillingMaint, PMBilling>
{
  public PXSelect<PMTran> PMTranMetaData;
  public PXSelect<PMProject> PMProjectMetaData;
  public PXSelect<PMTask> PMTaskMetaData;
  public PXSelect<PMAccountGroup> PMAccountGroupMetaData;
  public PXSelect<PX.Objects.PM.PMBudget> PMBudget;
  public PXSelect<EPEmployee> EmployeesMetaData;
  public PXSelect<PX.Objects.AP.Vendor> VendorMetaData;
  public PXSelect<PX.Objects.AR.Customer> CustomerMetaData;
  public PXSelect<PX.Objects.IN.InventoryItem> InventoryItemMetaData;
  public PXSelect<PMBilling> Billing;
  public PXSelect<PMBillingRule, Where<PMBillingRule.billingID, Equal<Current<PMBilling.billingID>>>> BillingRules;
  public PXSelect<PMBillingRule, Where<PMBillingRule.billingID, Equal<Current<PMBilling.billingID>>, And<PMBillingRule.stepID, Equal<Current<PMBillingRule.stepID>>>>> BillingRule;

  [InjectDependency]
  private ICurrentUserInformationProvider _currentUserInformationProvider { get; set; }

  [PXMergeAttributes]
  [PXUIField]
  protected virtual void _(PX.Data.Events.CacheAttached<PMBilling.billingID> e)
  {
  }

  protected virtual void PMBillingRule_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is PMBillingRule row))
      return;
    PXUIFieldAttribute.SetVisible<PMBillingRule.subMask>(sender, e.Row, row.Type == "T");
    PXUIFieldAttribute.SetVisible<PMBillingRule.subMaskBudget>(sender, e.Row, row.Type == "B");
    PXUIFieldAttribute.SetVisible<PMBillingRule.branchSourceBudget>(sender, e.Row, this.ShowBranchOptions() && row.Type == "B");
    PXUIFieldAttribute.SetVisible<PMBillingRule.accountID>(sender, e.Row, row.Type == "T");
    PXUIFieldAttribute.SetVisible<PMBillingRule.accountGroupID>(sender, e.Row, row.Type == "T");
    PXUIFieldAttribute.SetVisible<PMBillingRule.amountFormula>(sender, e.Row, row.Type == "T");
    PXUIFieldAttribute.SetVisible<PMBillingRule.qtyFormula>(sender, e.Row, row.Type == "T");
    PXUIFieldAttribute.SetVisible<PMBillingRule.rateTypeID>(sender, e.Row, row.Type == "T");
    PXUIFieldAttribute.SetVisible<PMBillingRule.noRateOption>(sender, e.Row, row.Type == "T");
    PXUIFieldAttribute.SetVisible<PMBillingRule.includeNonBillable>(sender, e.Row, row.Type == "T");
    PXUIFieldAttribute.SetVisible<PMBillingRule.copyNotes>(sender, e.Row, row.Type == "T");
    PXUIFieldAttribute.SetVisible<PMBillingRule.includeZeroAmountAndQty>(sender, e.Row, row.Type == "T");
    PXUIFieldAttribute.SetVisible<PMBillingRule.includeZeroAmount>(sender, e.Row, row.Type == "B");
    PXUIFieldAttribute.SetVisible<PMBillingRule.groupByDate>(sender, e.Row, row.Type == "T");
    PXUIFieldAttribute.SetVisible<PMBillingRule.groupByEmployee>(sender, e.Row, row.Type == "T");
    PXUIFieldAttribute.SetVisible<PMBillingRule.groupByItem>(sender, e.Row, row.Type == "T");
    PXUIFieldAttribute.SetVisible<PMBillingRule.groupByVendor>(sender, e.Row, row.Type == "T");
    PXUIFieldAttribute.SetVisible<PMBillingRule.branchSource>(sender, e.Row, this.ShowBranchOptions() && row.Type == "T");
    PXUIFieldAttribute.SetEnabled<PMBillingRule.amountFormula>(sender, e.Row, true);
    PXUIFieldAttribute.SetEnabled<PMBillingRule.qtyFormula>(sender, e.Row, true);
    PXUIFieldAttribute.SetEnabled<PMBillingRule.invoiceFormula>(sender, e.Row, true);
    PXUIFieldAttribute.SetEnabled<PMBillingRule.descriptionFormula>(sender, e.Row, true);
  }

  public static IDictionary<string, string> GetAccountSources(string billingType)
  {
    Dictionary<string, string> accountSources = new Dictionary<string, string>();
    int num = billingType == "T" ? 1 : 0;
    if (num != 0)
    {
      accountSources.Add("N", Messages.GetLocal("Source Transaction"));
      accountSources.Add("B", Messages.GetLocal("Billing Rule"));
    }
    else
      accountSources.Add("A", Messages.GetLocal("Account Group"));
    accountSources.Add("P", Messages.GetLocal("Project"));
    accountSources.Add("T", Messages.GetLocal("Task"));
    accountSources.Add("I", Messages.GetLocal("Inventory Item"));
    if (num != 0)
    {
      accountSources.Add("C", Messages.GetLocal("Customer"));
      accountSources.Add("E", Messages.GetLocal("Employee"));
    }
    return (IDictionary<string, string>) accountSources;
  }

  [Obsolete("Remove due to AC-295442.")]
  protected virtual void _(PX.Data.Events.RowUpdated<PMBillingRule> e)
  {
  }

  [Obsolete("Remove due to AC-295442.")]
  protected virtual void _(PX.Data.Events.RowPersisting<PMBilling> e)
  {
  }

  protected virtual void _(
    PX.Data.Events.FieldSelecting<PMBillingRule, PMBillingRule.accountSource> e)
  {
    if (e.Row == null)
      return;
    IDictionary<string, string> accountSources = BillingMaint.GetAccountSources(e.Row.Type);
    string[] array1 = accountSources.Keys.ToArray<string>();
    string[] array2 = accountSources.Values.ToArray<string>();
    ((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<PMBillingRule, PMBillingRule.accountSource>>) e).ReturnState = (object) PXStringState.CreateInstance(((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<PMBillingRule, PMBillingRule.accountSource>>) e).ReturnState, new int?(1), new bool?(false), typeof (PMBillingRule.accountSource).Name, new bool?(true), new int?(1), (string) null, array1, array2, new bool?(true), array1[0], (string[]) null);
  }

  protected virtual void PMBillingRule_Type_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is PMBillingRule row))
      return;
    if (row.Type == "B")
    {
      row.AccountGroupID = new int?();
      row.AccountSource = "A";
    }
    else
      row.AccountSource = "N";
    row.AccountID = new int?();
    row.SubID = new int?();
  }

  protected virtual void PMBillingRule_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (!(e.Row is PMBillingRule row))
      return;
    int? nullable;
    if (row.Type == "T")
    {
      nullable = row.AccountGroupID;
      if (!nullable.HasValue)
        sender.RaiseExceptionHandling<PMBillingRule.accountGroupID>((object) row, (object) null, (Exception) new PXSetPropertyException<PMBillingRule.accountGroupID>("'{0}' cannot be empty.", new object[1]
        {
          (object) "[accountGroupID]"
        }));
    }
    if (row.BranchSourceBudget == "B")
    {
      nullable = row.TargetBranchID;
      if (!nullable.HasValue)
        sender.RaiseExceptionHandling<PMBillingRule.targetBranchID>((object) row, (object) null, (Exception) new PXSetPropertyException<PMBillingRule.targetBranchID>("If Use Destination Branch From is set to Billing Rule, the destination branch cannot be empty. Specify the destination branch."));
    }
    if (row.AccountSource == "B")
    {
      nullable = row.AccountID;
      if (!nullable.HasValue)
        sender.RaiseExceptionHandling<PMBillingRule.accountID>((object) row, (object) null, (Exception) new PXSetPropertyException<PMBillingRule.accountID>("If Use Sales Account From is set to Billing Rule, the sales account cannot be empty. Specify the sales account."));
    }
    if (row.SubMask == null)
    {
      if (PXAccess.FeatureInstalled<FeaturesSet.subAccount>())
        sender.RaiseExceptionHandling<PMBillingRule.subMask>((object) row, (object) null, (Exception) new PXSetPropertyException<PMBillingRule.subID>("'{0}' cannot be empty.", new object[1]
        {
          (object) "[subMask]"
        }));
    }
    else if (row.SubMask.Contains("B"))
    {
      nullable = row.SubID;
      if (!nullable.HasValue)
        sender.RaiseExceptionHandling<PMBillingRule.subID>((object) row, (object) null, (Exception) new PXSetPropertyException<PMBillingRule.subID>("If Use Sales Subaccount From uses the subaccount mask (B), the sales subaccount cannot be empty. Specify the sales subaccount."));
    }
    if (row.SubMaskBudget == null)
    {
      if (!PXAccess.FeatureInstalled<FeaturesSet.subAccount>())
        return;
      sender.RaiseExceptionHandling<PMBillingRule.subMaskBudget>((object) row, (object) null, (Exception) new PXSetPropertyException<PMBillingRule.subID>("'{0}' cannot be empty.", new object[1]
      {
        (object) "[subMaskBudget]"
      }));
    }
    else
    {
      if (!row.SubMaskBudget.Contains("B"))
        return;
      nullable = row.SubID;
      if (nullable.HasValue)
        return;
      sender.RaiseExceptionHandling<PMBillingRule.subID>((object) row, (object) null, (Exception) new PXSetPropertyException<PMBillingRule.subID>("If Use Sales Subaccount From uses the subaccount mask (B), the sales subaccount cannot be empty. Specify the sales subaccount."));
    }
  }

  public virtual bool ShowBranchOptions()
  {
    if (!PXAccess.FeatureInstalled<FeaturesSet.branch>())
      return false;
    IEnumerable<BranchInfo> activeBranches = this._currentUserInformationProvider.GetActiveBranches();
    return activeBranches != null && activeBranches.Count<BranchInfo>() > 1;
  }

  [Obsolete("Remove due to AC-295442.")]
  protected virtual bool DoesExistActiveBillingRuleWithTheSameAccountGroupId(
    int? accountGroupId,
    int? exceptStepId)
  {
    throw new NotImplementedException();
  }

  [Obsolete("Remove due to AC-295442.")]
  protected virtual PMAccountGroup GetRepeatingAccountGroup(
    out PMBillingRule ruleWithRepeatingAccountGroupId)
  {
    throw new NotImplementedException();
  }

  [Obsolete("Remove due to AC-295442.")]
  protected virtual PMAccountGroup GetAccountGroupByGroupId(int? groupId)
  {
    throw new NotImplementedException();
  }
}
