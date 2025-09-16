// Decompiled with JetBrains decompiler
// Type: PX.Objects.CT.ContractMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.WorkflowAPI;
using PX.Objects.AR;
using PX.Objects.Common.Discount;
using PX.Objects.Common.Discount.Mappers;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.Extensions.MultiCurrency;
using PX.Objects.IN;
using PX.Objects.PM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

#nullable enable
namespace PX.Objects.CT;

public class ContractMaint : PXGraph<
#nullable disable
ContractMaint, Contract>
{
  public PXSelectJoin<Contract, LeftJoin<PX.Objects.AR.Customer, On<Contract.customerID, Equal<PX.Objects.AR.Customer.bAccountID>>>, Where<Contract.baseType, Equal<CTPRType.contract>>> Contracts;
  public PXSelect<Contract, Where<Contract.contractID, Equal<Optional<Contract.contractID>>>> CurrentContract;
  [PXCopyPasteHiddenFields(new System.Type[] {typeof (ContractBillingSchedule.lastDate), typeof (ContractBillingSchedule.nextDate), typeof (ContractBillingSchedule.startBilling)})]
  public PXSelect<ContractBillingSchedule, Where<ContractBillingSchedule.contractID, Equal<Current<Contract.contractID>>>> Billing;
  public PXSelect<ContractSLAMapping, Where<ContractSLAMapping.contractID, Equal<Current<Contract.contractID>>>> SLAMapping;
  public PXSelectJoin<ContractDetail, LeftJoin<ContractItem, On<ContractDetail.contractItemID, Equal<ContractItem.contractItemID>>>, Where<ContractDetail.contractID, Equal<Current<Contract.contractID>>>> ContractDetails;
  public PXSelectJoin<ContractDetail, InnerJoin<ContractItem, On<ContractItem.contractItemID, Equal<ContractDetail.contractItemID>>, InnerJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.IN.InventoryItem.inventoryID, Equal<ContractItem.recurringItemID>, Or<PX.Objects.IN.InventoryItem.inventoryID, Equal<ContractItem.baseItemID>, And<ContractItem.deposit, Equal<True>>>>>>, Where<ContractDetail.contractID, Equal<Current<Contract.contractID>>>> RecurringDetails;
  public PXSelect<ContractItem> RecurringDetailsContractItem;
  public PXSetup<PX.Objects.CR.Location, Where<PX.Objects.CR.Location.bAccountID, Equal<Optional<ContractBillingSchedule.accountID>>, And<PX.Objects.CR.Location.locationID, Equal<Optional<ContractBillingSchedule.locationID>>>>> BillingLocation;
  public PXSelect<SelContractWatcher, Where<SelContractWatcher.contractID, Equal<Current<Contract.contractID>>>> Watchers;
  [PXCopyPasteHiddenView]
  public PXSelect<PX.Objects.AR.ARInvoice, Where<PX.Objects.AR.ARInvoice.projectID, Equal<Current<Contract.contractID>>>> Invoices;
  [PXCopyPasteHiddenView]
  public PXSelect<ContractRenewalHistory, Where<ContractRenewalHistory.contractID, Equal<Current<Contract.contractID>>>> RenewalHistory;
  public PXSetup<PX.Objects.GL.Company> Company;
  public PXSetup<ContractTemplate, Where<ContractTemplate.contractID, Equal<Current<Contract.templateID>>>> CurrentTemplate;
  [PXCopyPasteHiddenView]
  public PXFilter<ContractMaint.ActivationSettingsFilter> ActivationSettings;
  [PXCopyPasteHiddenView]
  public PXFilter<ContractMaint.SetupSettingsFilter> SetupSettings;
  [PXCopyPasteHiddenView]
  public PXFilter<ContractMaint.TerminationSettingsFilter> TerminationSettings;
  [PXCopyPasteHiddenView]
  public PXFilter<ContractMaint.BillingOnDemandSettingsFilter> OnDemandSettings;
  [PXCopyPasteHiddenView]
  public PXFilter<ContractMaint.RenewalSettingsFilter> RenewalSettings;
  public CRAttributeList<Contract> Answers;
  public PXSelectJoin<EPContractRate, LeftJoin<EPEmployee, On<EPContractRate.employeeID, Equal<EPEmployee.bAccountID>>, LeftJoin<EPEarningType, On<EPEarningType.typeCD, Equal<EPContractRate.earningType>>>>, Where<EPContractRate.contractID, Equal<Current<Contract.contractID>>>, OrderBy<Asc<EPContractRate.earningType, Asc<EPContractRate.employeeID>>>> ContractRates;
  public PXFilter<RenewManualNumberingFilter> renewManualNumberingFilter;
  public PXSelect<ContractTemplate> contractTemplate;
  public PXSelect<PX.Objects.CR.Location> customerLocation;
  public PXSelect<PMTran> pmTran;
  public PXSelect<PX.Objects.IN.InventoryItem> inventoryItem;
  public PXSelect<UsageData> usageData;
  public PXSelect<AccessInfo> accessInfo;
  public PXInitializeState<Contract> initializeState;
  public PXWorkflowEventHandler<Contract> OnSetupContract;
  public PXWorkflowEventHandler<Contract> OnExpireContract;
  public PXWorkflowEventHandler<Contract> OnActivateContract;
  public PXWorkflowEventHandler<Contract> OnCancelContract;
  public PXWorkflowEventHandler<Contract> OnUpgradeContract;
  public PXAction<Contract> action;
  public PXAction<Contract> inquiry;
  public PXAction<Contract> viewInvoice;
  public PXAction<Contract> viewUsage;
  public PXAction<Contract> showContact;
  public PXAction<Contract> renew;
  public PXAction<Contract> viewContract;
  public PXAction<Contract> bill;
  public PXAction<Contract> setup;
  public PXAction<Contract> activate;
  public PXAction<Contract> setupAndActivate;
  public PXAction<Contract> terminate;
  public PXAction<Contract> upgrade;
  public PXAction<Contract> activateUpgrade;
  public PXAction<Contract> undoBilling;
  public PXChangeID<Contract, Contract.contractCD> ChangeID;
  private bool customerChanged;
  private bool templateChanged;

  [PXMergeAttributes]
  [PXDBLong]
  protected void _(PX.Data.Events.CacheAttached<PMTran.projectCuryInfoID> e)
  {
  }

  [PXString(1, IsFixed = true)]
  [RecurringOption.ListForDeposits]
  [PXDefault("N")]
  [PXUIField(DisplayName = "Billing Type")]
  [PXFormula(typeof (Switch<Case<Where<ContractItem.deposit, Equal<True>>, RecurringOption.deposits>, ContractItem.recurringType>))]
  protected virtual void ContractItem_RecurringTypeForDeposits_CacheAttached(PXCache sender)
  {
  }

  [PXUIField(DisplayName = "UOM")]
  [PXString(10, IsFixed = true)]
  [PXFormula(typeof (Switch<Case<Where<ContractItem.deposit, Equal<True>>, ContractItem.curyID>, Selector<ContractItem.recurringItemID, PX.Objects.IN.InventoryItem.salesUnit>>))]
  protected virtual void ContractItem_UOMForDeposits_CacheAttached(PXCache sender)
  {
  }

  [PXBool]
  [PXFormula(typeof (Switch<Case<Where<ContractDetail.deposit, Equal<True>>, Switch<Case<Where<Div<Mult<ContractDetail.recurringIncluded, Selector<ContractDetail.contractItemID, ContractItem.retainRate>>, decimal100>, Greater<Sub<ContractDetail.recurringIncluded, ContractDetail.recurringUsedTotal>>>, True>, False>>, False>))]
  protected virtual void ContractDetail_WarningAmountForDeposit_CacheAttached(PXCache sender)
  {
  }

  public ContractMaint()
  {
    PXUIFieldAttribute.SetDisplayName(((PXGraph) this).Caches[typeof (PX.Objects.CR.Contact)], typeof (PX.Objects.CR.Contact.salutation).Name, "Attention");
    PXUIFieldAttribute.SetDisplayName<ContractDetail.used>(((PXSelectBase) this.RecurringDetails).Cache, "Used");
    PXUIFieldAttribute.SetDisplayName<ContractDetail.qty>(((PXSelectBase) this.RecurringDetails).Cache, "Included");
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) this).FieldDefaulting.AddHandler<BAccountR.type>(ContractMaint.\u003C\u003Ec.\u003C\u003E9__38_0 ?? (ContractMaint.\u003C\u003Ec.\u003C\u003E9__38_0 = new PXFieldDefaulting((object) ContractMaint.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003C\u002Ector\u003Eb__38_0))));
    PXDefaultAttribute.SetPersistingCheck<ContractBillingSchedule.billTo>(((PXSelectBase) this.Billing).Cache, (object) null, (PXPersistingCheck) 0);
    // ISSUE: method pointer
    ((PXGraph) this).FieldUpdated.AddHandler<Contract.expireDate>(new PXFieldUpdated((object) null, __methodptr(UpdateHistoryDate<Contract.expireDate, ContractRenewalHistory.expireDate>)));
    // ISSUE: method pointer
    ((PXGraph) this).FieldUpdated.AddHandler<Contract.startDate>(new PXFieldUpdated((object) null, __methodptr(UpdateHistoryDate<Contract.startDate, ContractRenewalHistory.startDate>)));
    // ISSUE: method pointer
    ((PXGraph) this).FieldUpdated.AddHandler<Contract.activationDate>(new PXFieldUpdated((object) null, __methodptr(UpdateHistoryDate<Contract.activationDate, ContractRenewalHistory.activationDate>)));
  }

  public virtual bool CanClipboardCopyPaste() => false;

  public virtual void Clear(PXClearOption option)
  {
    if (((Dictionary<System.Type, PXCache>) ((PXGraph) this).Caches).ContainsKey(typeof (ContractDetail)))
      ((PXGraph) this).Caches[typeof (ContractDetail)].ClearQueryCache();
    ((PXGraph) this).Clear(option);
  }

  public static Contract FindContract(PXGraph graph, int? contractID)
  {
    return PXResultset<Contract>.op_Implicit(PXSelectBase<Contract, PXSelect<Contract, Where<Contract.contractID, Equal<Required<Contract.contractID>>>>.Config>.Select(graph, new object[1]
    {
      (object) contractID
    }));
  }

  [PXUIField]
  [PXButton]
  protected virtual IEnumerable Action(PXAdapter adapter) => adapter.Get();

  [PXUIField]
  [PXButton]
  protected virtual IEnumerable Inquiry(PXAdapter adapter) => adapter.Get();

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewInvoice(PXAdapter adapter)
  {
    if (((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Invoices).Current == null)
      return adapter.Get();
    PXRedirectHelper.TryRedirect(((PXSelectBase) this.Invoices).Cache, (object) ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Invoices).Current, "View Invoice", (PXRedirectHelper.WindowMode) 3);
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewUsage(PXAdapter adapter)
  {
    if (((PXSelectBase<Contract>) this.Contracts).Current != null && ((PXSelectBase) this.Contracts).Cache.GetStatus((object) ((PXSelectBase<Contract>) this.Contracts).Current) != 2)
    {
      UsageMaint instance = PXGraph.CreateInstance<UsageMaint>();
      ((PXGraph) instance).Clear();
      ((PXSelectBase<UsageMaint.UsageFilter>) instance.Filter).Current.ContractID = ((PXSelectBase<Contract>) this.Contracts).Current.ContractID;
      ((PXSelectBase<UsageMaint.UsageFilter>) instance.Filter).Current.ContractStatus = ((PXSelectBase<Contract>) this.Contracts).Current.Status;
      throw new PXRedirectRequiredException((PXGraph) instance, "Contract Usage");
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ShowContact(PXAdapter adapter)
  {
    SelContractWatcher current = ((PXSelectBase<SelContractWatcher>) this.Watchers).Current;
    if (current != null && current.ContactID.HasValue)
    {
      ContactMaint instance = PXGraph.CreateInstance<ContactMaint>();
      ((PXGraph) instance).Clear();
      PX.Objects.CR.Contact contact = PXResultset<PX.Objects.CR.Contact>.op_Implicit(((PXSelectBase<PX.Objects.CR.Contact>) instance.Contact).Search<PX.Objects.CR.Contact.contactID>((object) current.ContactID, Array.Empty<object>()));
      if (contact != null)
      {
        ((PXSelectBase<PX.Objects.CR.Contact>) instance.Contact).Current = contact;
        throw new PXRedirectRequiredException((PXGraph) instance, "Contact Maintenance");
      }
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable Renew(PXAdapter adapter)
  {
    if (WebDialogResultExtension.IsPositive(((PXSelectBase<ContractMaint.RenewalSettingsFilter>) this.RenewalSettings).AskExt()) && this.RenewalSettings.VerifyRequired())
    {
      DateTime? renewalDate = ((PXSelectBase<ContractMaint.RenewalSettingsFilter>) this.RenewalSettings).Current.RenewalDate;
      if (renewalDate.HasValue)
      {
        renewalDate = ((PXSelectBase<ContractMaint.RenewalSettingsFilter>) this.RenewalSettings).Current.RenewalDate;
        this.RenewContract(renewalDate.Value, true);
      }
    }
    return adapter.Get();
  }

  public virtual void RenewContract(DateTime renewalDate, bool redirect = false)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    ContractMaint.\u003C\u003Ec__DisplayClass54_0 cDisplayClass540 = new ContractMaint.\u003C\u003Ec__DisplayClass54_0()
    {
      \u003C\u003E4__this = this,
      renewalDate = renewalDate,
      redirect = redirect,
      contract = PXCache<Contract>.CreateCopy(((PXSelectBase<Contract>) this.CurrentContract).Current)
    };
    // ISSUE: reference to a compiler-generated field
    cDisplayClass540.contract.IsLastActionUndoable = new bool?(true);
    // ISSUE: reference to a compiler-generated field
    ((PXSelectBase<Contract>) this.CurrentContract).Update(cDisplayClass540.contract);
    ((PXAction) this.Save).Press();
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass540.contract.Type == "E" || cDisplayClass540.contract.Type == "R" && ContractMaint.IsExpired(cDisplayClass540.contract, cDisplayClass540.renewalDate))
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      ContractMaint.\u003C\u003Ec__DisplayClass54_1 cDisplayClass541 = new ContractMaint.\u003C\u003Ec__DisplayClass54_1();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass541.CS\u0024\u003C\u003E8__locals1 = cDisplayClass540;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass541.target = PXGraph.CreateInstance<ContractMaint>();
      // ISSUE: reference to a compiler-generated field
      ((PXGraph) cDisplayClass541.target).Clear();
      bool flag = this.IsDimensionAutonumbered("CONTRACT");
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      if (!cDisplayClass541.CS\u0024\u003C\u003E8__locals1.redirect && !flag)
        throw new PXException("Cannot automatically renew contract when Auto Numbering is off. Please use Renew Contract action on Customer Contracts screen.");
      // ISSUE: reference to a compiler-generated field
      cDisplayClass541.contractCD = (string) null;
      if (!flag && WebDialogResultExtension.IsPositive(((PXSelectBase<RenewManualNumberingFilter>) this.renewManualNumberingFilter).AskExt()))
      {
        if (!this.renewManualNumberingFilter.VerifyRequired())
          return;
        if (PXSelectBase<Contract, PXSelectReadonly<Contract, Where<Contract.baseType, Equal<CTPRType.contract>, And<Contract.contractCD, Equal<Required<Contract.contractCD>>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) ((PXSelectBase<RenewManualNumberingFilter>) this.renewManualNumberingFilter).Current.ContractCD.Trim()
        }).Count != 0)
        {
          ((PXSelectBase) this.renewManualNumberingFilter).Cache.RaiseExceptionHandling<RenewManualNumberingFilter.contractCD>((object) ((PXSelectBase<RenewManualNumberingFilter>) this.renewManualNumberingFilter).Current, (object) ((PXSelectBase<RenewManualNumberingFilter>) this.renewManualNumberingFilter).Current.ContractCD, (Exception) new PXSetPropertyException("Contract with this Contract ID '{0}' already exists. Please enter another Contract ID.", new object[1]
          {
            (object) ((PXSelectBase<RenewManualNumberingFilter>) this.renewManualNumberingFilter).Current.ContractCD
          }));
          return;
        }
        // ISSUE: reference to a compiler-generated field
        cDisplayClass541.contractCD = ((PXSelectBase<RenewManualNumberingFilter>) this.renewManualNumberingFilter).Current.ContractCD;
      }
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass541, __methodptr(\u003CRenewContract\u003Eb__1)));
    }
    else
    {
      // ISSUE: reference to a compiler-generated field
      if (!(cDisplayClass540.contract.Type == "R"))
        return;
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass540, __methodptr(\u003CRenewContract\u003Eb__0)));
    }
  }

  private bool IsDimensionAutonumbered(string dimension)
  {
    return GraphHelper.RowCast<Segment>((IEnumerable) PXSelectBase<Segment, PXSelect<Segment, Where<Segment.dimensionID, Equal<Required<Segment.dimensionID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) dimension
    })).All<Segment>((Func<Segment, bool>) (segment => segment.AutoNumber.GetValueOrDefault()));
  }

  private void CreateExpiringRenewalHistory(Contract child)
  {
    if (!child.OriginalContractID.HasValue)
      return;
    Contract contract1 = PXResultset<Contract>.op_Implicit(PXSelectBase<Contract, PXSelect<Contract, Where<Contract.contractID, Equal<Required<Contract.originalContractID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) child.OriginalContractID
    }));
    contract1.LastActiveRevID = contract1.RevID;
    CTBillEngine instance = PXGraph.CreateInstance<CTBillEngine>();
    ContractBillingSchedule schedule = ((PXSelectBase<ContractBillingSchedule>) instance.BillingSchedule).SelectSingle(new object[1]
    {
      (object) contract1.ContractID
    });
    ContractRenewalHistory history = ((PXSelectBase<ContractRenewalHistory>) instance.CurrentRenewalHistory).SelectSingle(new object[2]
    {
      (object) contract1.ContractID,
      (object) contract1.RevID
    });
    CTBillEngine.UpdateContractHistoryEntry(history, contract1, schedule);
    history.ContractID = contract1.ContractID;
    history.ChildContractID = child.ContractID;
    history.Status = contract1.Status;
    history.Action = "R";
    history.RenewalDate = child.CreatedDateTime;
    ContractRenewalHistory contractRenewalHistory = history;
    Contract contract2 = contract1;
    int? revId = contract2.RevID;
    int? nullable1 = revId.HasValue ? new int?(revId.GetValueOrDefault() + 1) : new int?();
    contract2.RevID = nullable1;
    int? nullable2 = nullable1;
    contractRenewalHistory.RevID = nullable2;
    ((PXSelectBase<ContractRenewalHistory>) this.RenewalHistory).Insert(history);
    ((PXSelectBase<Contract>) this.Contracts).Update(contract1);
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewContract(PXAdapter adapter)
  {
    if (((PXSelectBase<ContractRenewalHistory>) this.RenewalHistory).Current != null && ((PXSelectBase<ContractRenewalHistory>) this.RenewalHistory).Current.ChildContractID.HasValue)
    {
      ContractMaint instance = PXGraph.CreateInstance<ContractMaint>();
      ((PXGraph) instance).Clear();
      ((PXSelectBase<Contract>) instance.Contracts).Current = PXResultset<Contract>.op_Implicit(PXSelectBase<Contract, PXSelect<Contract, Where<Contract.contractID, Equal<Current<ContractRenewalHistory.childContractID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, nameof (ViewContract));
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual void Bill()
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    ContractMaint.\u003C\u003Ec__DisplayClass60_0 cDisplayClass600 = new ContractMaint.\u003C\u003Ec__DisplayClass60_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass600.\u003C\u003E4__this = this;
    ((PXSelectBase<ContractBillingSchedule>) this.Billing).Current = PXResultset<ContractBillingSchedule>.op_Implicit(((PXSelectBase<ContractBillingSchedule>) this.Billing).Select(Array.Empty<object>()));
    // ISSUE: reference to a compiler-generated field
    cDisplayClass600.billingDate = !(((PXSelectBase<ContractBillingSchedule>) this.Billing).Current.Type == "D") || !WebDialogResultExtension.IsPositive(((PXSelectBase<ContractMaint.BillingOnDemandSettingsFilter>) this.OnDemandSettings).AskExt()) ? new DateTime?() : ((PXSelectBase<ContractMaint.BillingOnDemandSettingsFilter>) this.OnDemandSettings).Current.BillingDate;
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass600, __methodptr(\u003CBill\u003Eb__0)));
  }

  protected virtual void SetupSettingsFilter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    ContractMaint.SetupSettingsFilter row = (ContractMaint.SetupSettingsFilter) e.Row;
    if (row == null)
      return;
    PXCache pxCache = sender;
    ContractMaint.SetupSettingsFilter setupSettingsFilter = row;
    // ISSUE: variable of a boxed type
    __Boxed<DateTime?> startDate1 = (ValueType) row.StartDate;
    DateTime? startDate2 = row.StartDate;
    DateTime? startDate3 = ((PXSelectBase<Contract>) this.Contracts).Current.StartDate;
    PXSetPropertyException propertyException;
    if ((startDate2.HasValue == startDate3.HasValue ? (startDate2.HasValue ? (startDate2.GetValueOrDefault() != startDate3.GetValueOrDefault() ? 1 : 0) : 0) : 1) == 0)
      propertyException = (PXSetPropertyException) null;
    else
      propertyException = new PXSetPropertyException("The contract expiration date will be recalculated based on the specified {0}.", (PXErrorLevel) 2, new object[1]
      {
        (object) "[startDate]"
      });
    pxCache.RaiseExceptionHandling<ContractMaint.SetupSettingsFilter.startDate>((object) setupSettingsFilter, (object) startDate1, (Exception) propertyException);
  }

  protected virtual void ActivationSettingsFilter_RowSelected(
    PXCache sender,
    PXRowSelectedEventArgs e)
  {
    ContractMaint.ActivationSettingsFilter row = (ContractMaint.ActivationSettingsFilter) e.Row;
    if (row == null)
      return;
    PXCache pxCache = sender;
    ContractMaint.ActivationSettingsFilter activationSettingsFilter = row;
    // ISSUE: variable of a boxed type
    __Boxed<DateTime?> activationDate1 = (ValueType) row.ActivationDate;
    PXSetPropertyException propertyException;
    if (row.ActionName == "activate" || row.ActionName == "setupAndActivate")
    {
      DateTime? activationDate2 = row.ActivationDate;
      DateTime? activationDate3 = ((PXSelectBase<Contract>) this.Contracts).Current.ActivationDate;
      if ((activationDate2.HasValue == activationDate3.HasValue ? (activationDate2.HasValue ? (activationDate2.GetValueOrDefault() != activationDate3.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
      {
        propertyException = new PXSetPropertyException("The contract expiration date will be recalculated based on the specified {0}.", (PXErrorLevel) 2, new object[1]
        {
          (object) "[activationDate]"
        });
        goto label_5;
      }
    }
    propertyException = (PXSetPropertyException) null;
label_5:
    pxCache.RaiseExceptionHandling<ContractMaint.ActivationSettingsFilter.activationDate>((object) activationSettingsFilter, (object) activationDate1, (Exception) propertyException);
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable Setup(PXAdapter adapter)
  {
    if (((PXSelectBase<Contract>) this.Contracts).Current == null)
      return adapter.Get();
    // ISSUE: method pointer
    if (WebDialogResultExtension.IsPositive(((PXSelectBase<ContractMaint.SetupSettingsFilter>) this.SetupSettings).AskExt(new PXView.InitializePanel((object) this, __methodptr(\u003CSetup\u003Eb__64_0)))))
    {
      ((PXSelectBase<Contract>) this.Contracts).Current.StartDate = ((PXSelectBase<ContractMaint.SetupSettingsFilter>) this.SetupSettings).Current.StartDate;
      // ISSUE: method pointer
      ((PXGraph) this).FieldUpdated.RemoveHandler<Contract.startDate>(new PXFieldUpdated((object) null, __methodptr(UpdateHistoryDate<Contract.startDate, ContractRenewalHistory.startDate>)));
      // ISSUE: method pointer
      ((PXGraph) this).FieldUpdated.RemoveHandler<Contract.activationDate>(new PXFieldUpdated((object) null, __methodptr(UpdateHistoryDate<Contract.activationDate, ContractRenewalHistory.activationDate>)));
      // ISSUE: method pointer
      ((PXGraph) this).FieldUpdated.RemoveHandler<Contract.expireDate>(new PXFieldUpdated((object) null, __methodptr(UpdateHistoryDate<Contract.expireDate, ContractRenewalHistory.expireDate>)));
      ((PXSelectBase<Contract>) this.Contracts).Update(((PXSelectBase<Contract>) this.Contracts).Current);
      // ISSUE: method pointer
      ((PXGraph) this).FieldUpdated.AddHandler<Contract.startDate>(new PXFieldUpdated((object) null, __methodptr(UpdateHistoryDate<Contract.startDate, ContractRenewalHistory.startDate>)));
      // ISSUE: method pointer
      ((PXGraph) this).FieldUpdated.AddHandler<Contract.activationDate>(new PXFieldUpdated((object) null, __methodptr(UpdateHistoryDate<Contract.activationDate, ContractRenewalHistory.activationDate>)));
      // ISSUE: method pointer
      ((PXGraph) this).FieldUpdated.AddHandler<Contract.expireDate>(new PXFieldUpdated((object) null, __methodptr(UpdateHistoryDate<Contract.expireDate, ContractRenewalHistory.expireDate>)));
      ((PXAction) this.Save).Press();
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) this, __methodptr(\u003CSetup\u003Eb__64_1)));
    }
    return (IEnumerable) new object[1]
    {
      (object) ((PXSelectBase<Contract>) this.Contracts).Current
    };
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable Activate(PXAdapter adapter)
  {
    if (((PXSelectBase<Contract>) this.Contracts).Current == null)
      return adapter.Get();
    PXDefaultAttribute.SetPersistingCheck<Contract.activationDate>(((PXSelectBase) this.CurrentContract).Cache, (object) ((PXSelectBase<Contract>) this.CurrentContract).Current, (PXPersistingCheck) 1);
    // ISSUE: method pointer
    if (WebDialogResultExtension.IsPositive(((PXSelectBase<ContractMaint.ActivationSettingsFilter>) this.ActivationSettings).AskExt(new PXView.InitializePanel((object) this, __methodptr(\u003CActivate\u003Eb__66_0)))))
    {
      ((PXSelectBase<Contract>) this.Contracts).Current.ActivationDate = ((PXSelectBase<ContractMaint.ActivationSettingsFilter>) this.ActivationSettings).Current.ActivationDate;
      // ISSUE: method pointer
      ((PXGraph) this).FieldUpdated.RemoveHandler<Contract.activationDate>(new PXFieldUpdated((object) null, __methodptr(UpdateHistoryDate<Contract.activationDate, ContractRenewalHistory.activationDate>)));
      // ISSUE: method pointer
      ((PXGraph) this).FieldUpdated.RemoveHandler<Contract.expireDate>(new PXFieldUpdated((object) null, __methodptr(UpdateHistoryDate<Contract.expireDate, ContractRenewalHistory.expireDate>)));
      ((PXSelectBase<Contract>) this.Contracts).Update(((PXSelectBase<Contract>) this.Contracts).Current);
      // ISSUE: method pointer
      ((PXGraph) this).FieldUpdated.AddHandler<Contract.activationDate>(new PXFieldUpdated((object) null, __methodptr(UpdateHistoryDate<Contract.activationDate, ContractRenewalHistory.activationDate>)));
      // ISSUE: method pointer
      ((PXGraph) this).FieldUpdated.AddHandler<Contract.expireDate>(new PXFieldUpdated((object) null, __methodptr(UpdateHistoryDate<Contract.expireDate, ContractRenewalHistory.expireDate>)));
      ((PXAction) this.Save).Press();
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) this, __methodptr(\u003CActivate\u003Eb__66_1)));
    }
    return (IEnumerable) new object[1]
    {
      (object) ((PXSelectBase<Contract>) this.Contracts).Current
    };
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable SetupAndActivate(PXAdapter adapter)
  {
    if (((PXSelectBase<Contract>) this.Contracts).Current == null)
      return adapter.Get();
    PXDefaultAttribute.SetPersistingCheck<Contract.activationDate>(((PXSelectBase) this.CurrentContract).Cache, (object) ((PXSelectBase<Contract>) this.CurrentContract).Current, (PXPersistingCheck) 1);
    // ISSUE: method pointer
    if (WebDialogResultExtension.IsPositive(((PXSelectBase<ContractMaint.ActivationSettingsFilter>) this.ActivationSettings).AskExt(new PXView.InitializePanel((object) this, __methodptr(\u003CSetupAndActivate\u003Eb__68_0)))))
    {
      ((PXSelectBase<Contract>) this.Contracts).Current.ActivationDate = ((PXSelectBase<ContractMaint.ActivationSettingsFilter>) this.ActivationSettings).Current.ActivationDate;
      ((PXSelectBase<Contract>) this.Contracts).Current.StartDate = ((PXSelectBase<ContractMaint.ActivationSettingsFilter>) this.ActivationSettings).Current.ActivationDate;
      // ISSUE: method pointer
      ((PXGraph) this).FieldUpdated.RemoveHandler<Contract.startDate>(new PXFieldUpdated((object) null, __methodptr(UpdateHistoryDate<Contract.startDate, ContractRenewalHistory.startDate>)));
      // ISSUE: method pointer
      ((PXGraph) this).FieldUpdated.RemoveHandler<Contract.activationDate>(new PXFieldUpdated((object) null, __methodptr(UpdateHistoryDate<Contract.activationDate, ContractRenewalHistory.activationDate>)));
      // ISSUE: method pointer
      ((PXGraph) this).FieldUpdated.RemoveHandler<Contract.expireDate>(new PXFieldUpdated((object) null, __methodptr(UpdateHistoryDate<Contract.expireDate, ContractRenewalHistory.expireDate>)));
      ((PXSelectBase<Contract>) this.Contracts).Update(((PXSelectBase<Contract>) this.Contracts).Current);
      // ISSUE: method pointer
      ((PXGraph) this).FieldUpdated.AddHandler<Contract.startDate>(new PXFieldUpdated((object) null, __methodptr(UpdateHistoryDate<Contract.startDate, ContractRenewalHistory.startDate>)));
      // ISSUE: method pointer
      ((PXGraph) this).FieldUpdated.AddHandler<Contract.activationDate>(new PXFieldUpdated((object) null, __methodptr(UpdateHistoryDate<Contract.activationDate, ContractRenewalHistory.activationDate>)));
      // ISSUE: method pointer
      ((PXGraph) this).FieldUpdated.AddHandler<Contract.expireDate>(new PXFieldUpdated((object) null, __methodptr(UpdateHistoryDate<Contract.expireDate, ContractRenewalHistory.expireDate>)));
      ((PXAction) this.Save).Press();
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) this, __methodptr(\u003CSetupAndActivate\u003Eb__68_1)));
    }
    return (IEnumerable) new object[1]
    {
      (object) ((PXSelectBase<Contract>) this.Contracts).Current
    };
  }

  [PXUIField]
  [PXButton]
  public virtual void Terminate()
  {
    if (((PXSelectBase<Contract>) this.Contracts).Current == null)
      return;
    if (!((PXSelectBase<Contract>) this.Contracts).Current.CustomerID.HasValue)
      throw new PXException("This type of Contract cannot be Terminated.");
    if (!WebDialogResultExtension.IsPositive(((PXSelectBase<ContractMaint.TerminationSettingsFilter>) this.TerminationSettings).AskExt()))
      return;
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) this, __methodptr(\u003CTerminate\u003Eb__70_0)));
    if (!((PXGraph) this).IsImport)
      return;
    ((PXGraph) this).Actions.PressCancel();
  }

  [PXUIField]
  [PXButton]
  public virtual void Upgrade()
  {
    ((PXAction) this.Save).Press();
    if (((PXSelectBase<Contract>) this.Contracts).Current == null)
      return;
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) this, __methodptr(\u003CUpgrade\u003Eb__72_0)));
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ActivateUpgrade(PXAdapter adapter)
  {
    if (((PXSelectBase<Contract>) this.Contracts).Current == null)
      return adapter.Get();
    // ISSUE: method pointer
    if (WebDialogResultExtension.IsPositive(((PXSelectBase<ContractMaint.ActivationSettingsFilter>) this.ActivationSettings).AskExt(new PXView.InitializePanel((object) this, __methodptr(\u003CActivateUpgrade\u003Eb__74_0)))))
    {
      ((PXAction) this.Save).Press();
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) this, __methodptr(\u003CActivateUpgrade\u003Eb__74_1)));
    }
    return (IEnumerable) new object[1]
    {
      (object) ((PXSelectBase<Contract>) this.Contracts).Current
    };
  }

  [PXUIField]
  [PXButton]
  public virtual void UndoBilling()
  {
    if (((PXSelectBase<Contract>) this.Contracts).Current == null)
      return;
    if (!((PXSelectBase<Contract>) this.Contracts).Current.IsLastActionUndoable.GetValueOrDefault())
      throw new PXException("Last action can not be undone.");
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) this, __methodptr(\u003CUndoBilling\u003Eb__76_0)));
  }

  public ContractBillingSchedule contractBillingSchedule
  {
    get
    {
      return PXResultset<ContractBillingSchedule>.op_Implicit(((PXSelectBase<ContractBillingSchedule>) this.Billing).Select(Array.Empty<object>()));
    }
  }

  [PXDBInt]
  [PXDimensionSelector("INVENTORY", typeof (Search2<PX.Objects.IN.InventoryItem.inventoryID, LeftJoin<ARSalesPrice, On<ARSalesPrice.inventoryID, Equal<PX.Objects.IN.InventoryItem.inventoryID>, And<ARSalesPrice.uOM, Equal<PX.Objects.IN.InventoryItem.baseUnit>, And<ARSalesPrice.priceType, Equal<PriceTypes.basePrice>, And<ARSalesPrice.curyID, Equal<Current<ContractItem.curyID>>>>>>, LeftJoin<ARSalesPrice2, On<ARSalesPrice2.inventoryID, Equal<PX.Objects.IN.InventoryItem.inventoryID>, And<ARSalesPrice2.uOM, Equal<PX.Objects.IN.InventoryItem.baseUnit>, And<ARSalesPrice2.custPriceClassID, Equal<Current<PX.Objects.CR.Location.cPriceClassID>>, And<ARSalesPrice2.curyID, Equal<Current<ContractItem.curyID>>>>>>>>, Where<PX.Objects.IN.InventoryItem.stkItem, Equal<False>>>), typeof (PX.Objects.IN.InventoryItem.inventoryCD))]
  [PXUIField(DisplayName = "Setup Item")]
  public void ContractItem_BaseItemID_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt]
  [PXDimensionSelector("INVENTORY", typeof (Search2<PX.Objects.IN.InventoryItem.inventoryID, LeftJoin<ARSalesPrice, On<ARSalesPrice.inventoryID, Equal<PX.Objects.IN.InventoryItem.inventoryID>, And<ARSalesPrice.uOM, Equal<PX.Objects.IN.InventoryItem.baseUnit>, And<ARSalesPrice.priceType, Equal<PriceTypes.basePrice>, And<ARSalesPrice.curyID, Equal<Current<ContractItem.curyID>>>>>>, LeftJoin<ARSalesPrice2, On<ARSalesPrice2.inventoryID, Equal<PX.Objects.IN.InventoryItem.inventoryID>, And<ARSalesPrice2.uOM, Equal<PX.Objects.IN.InventoryItem.baseUnit>, And<ARSalesPrice2.custPriceClassID, Equal<Current<PX.Objects.CR.Location.cPriceClassID>>, And<ARSalesPrice2.curyID, Equal<Current<ContractItem.curyID>>>>>>>>, Where<PX.Objects.IN.InventoryItem.stkItem, Equal<False>>>), typeof (PX.Objects.IN.InventoryItem.inventoryCD))]
  [PXUIField(DisplayName = "Renewal Item")]
  public void ContractItem_RenewalItemID_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt]
  [PXDimensionSelector("INVENTORY", typeof (Search2<PX.Objects.IN.InventoryItem.inventoryID, LeftJoin<ARSalesPrice, On<ARSalesPrice.inventoryID, Equal<PX.Objects.IN.InventoryItem.inventoryID>, And<ARSalesPrice.uOM, Equal<PX.Objects.IN.InventoryItem.baseUnit>, And<ARSalesPrice.priceType, Equal<PriceTypes.basePrice>, And<ARSalesPrice.curyID, Equal<Current<ContractItem.curyID>>>>>>, LeftJoin<ARSalesPrice2, On<ARSalesPrice2.inventoryID, Equal<PX.Objects.IN.InventoryItem.inventoryID>, And<ARSalesPrice2.uOM, Equal<PX.Objects.IN.InventoryItem.baseUnit>, And<ARSalesPrice2.custPriceClassID, Equal<Current<PX.Objects.CR.Location.cPriceClassID>>, And<ARSalesPrice2.curyID, Equal<Current<ContractItem.curyID>>>>>>>>, Where<PX.Objects.IN.InventoryItem.stkItem, Equal<False>>>), typeof (PX.Objects.IN.InventoryItem.inventoryCD))]
  [PXUIField(DisplayName = "Recurring Item")]
  public void ContractItem_RecurringItemID_CacheAttached(PXCache sender)
  {
  }

  [PXDefault]
  [PXRestrictor(typeof (Where<PX.Objects.CR.BAccount.type, NotEqual<BAccountType.vendorType>>), "Only a customer or company business account can be specified.", new System.Type[] {})]
  [PXMergeAttributes]
  public void Contract_CustomerID_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(1, IsFixed = true)]
  [Contract.durationType.List]
  [PXDefault]
  [PXUIField(DisplayName = "Duration Unit")]
  public void Contract_DurationType_CacheAttached(PXCache sender)
  {
  }

  protected virtual void Contract_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    if (!(e.Row is Contract row))
      return;
    ContractBillingSchedule schedule = new ContractBillingSchedule();
    schedule.ContractID = row.ContractID;
    ((PXSelectBase<ContractBillingSchedule>) this.Billing).Insert(schedule);
    PXUIFieldAttribute.SetRequired<ContractBillingSchedule.nextDate>(sender, true);
    if (((PXSelectBase) this.SLAMapping).Cache.GetStateExt<ContractSLAMapping.severity>((object) null) is PXStringState stateExt && stateExt.AllowedValues != null && stateExt.AllowedValues.Length != 0)
    {
      foreach (string allowedValue in stateExt.AllowedValues)
        ((PXSelectBase<ContractSLAMapping>) this.SLAMapping).Insert(new ContractSLAMapping()
        {
          ContractID = row.ContractID,
          Severity = allowedValue
        });
    }
    ContractRenewalHistory history = new ContractRenewalHistory()
    {
      RenewalDate = ((PXGraph) this).Accessinfo.BusinessDate,
      Status = "D",
      Action = "N",
      ChildContractID = row.OriginalContractID
    };
    CTBillEngine.UpdateContractHistoryEntry(history, row, schedule);
    ((PXSelectBase<ContractRenewalHistory>) this.RenewalHistory).Insert(history);
    ((PXSelectBase) this.Billing).Cache.IsDirty = false;
    ((PXSelectBase) this.SLAMapping).Cache.IsDirty = false;
    ((PXSelectBase) this.RenewalHistory).Cache.IsDirty = false;
  }

  protected virtual void Contract_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is Contract row))
      return;
    this.SetControlsState(row, sender);
    this.CalcDetail(row);
    if (row.TotalsCalculated.GetValueOrDefault() == 1)
      return;
    this.CalcSummary(sender, row);
    if (!row.TotalUsage.HasValue)
      return;
    row.TotalsCalculated = new int?(1);
  }

  protected virtual void Contract_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    PXSelectorAttribute.CheckAndRaiseForeignKeyException(sender, e.Row, typeof (PMTran.projectID), (System.Type) null, (string) null);
    PXSelectorAttribute.CheckAndRaiseForeignKeyException(sender, e.Row, typeof (CRCase.contractID), (System.Type) null, (string) null);
    PXSelectorAttribute.CheckAndRaiseForeignKeyException(sender, e.Row, typeof (PX.Objects.AR.ARTran.projectID), (System.Type) null, (string) null);
  }

  protected virtual void _(PX.Data.Events.RowInserting<Contract> e)
  {
    string contractCd = e.Row?.ContractCD;
    if (contractCd == null)
      return;
    if (PXResultset<Contract>.op_Implicit(PXSelectBase<Contract, PXViewOf<Contract>.BasedOn<SelectFromBase<Contract, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<Contract.contractCD, IBqlString>.IsEqual<P.AsString>>>.ReadOnly.Config>.Select((PXGraph) this, new object[1]
    {
      (object) contractCd
    })) != null)
      throw new PXException("The contract cannot be created because the specified contract ID ({0}) had been previously used in the system. Specify another contract ID.", new object[1]
      {
        (object) contractCd.Trim()
      });
  }

  protected virtual void Contract_TemplateID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    int? templateId;
    if (e.Row is Contract row)
    {
      templateId = row.TemplateID;
      if (!templateId.HasValue)
      {
        using (IEnumerator<PXResult<ContractDetail>> enumerator = ((PXSelectBase<ContractDetail>) this.ContractDetails).Select(Array.Empty<object>()).GetEnumerator())
        {
          while (enumerator.MoveNext())
            ((PXSelectBase<ContractDetail>) this.ContractDetails).Delete(PXResult<ContractDetail>.op_Implicit(enumerator.Current));
          return;
        }
      }
    }
    if (row == null)
      return;
    templateId = row.TemplateID;
    if (!templateId.HasValue || ((PXSelectBase) this.Contracts).Cache.GetStatus((object) row) != 2)
      return;
    this.DefaultFromTemplate(row);
  }

  protected virtual void Contract_ContractCD_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (!(e.Row is Contract))
      return;
    ParameterExpression parameterExpression;
    // ISSUE: method reference
    // ISSUE: method reference
    if (((IQueryable<PXResult<ContractTemplate>>) PXSelectBase<ContractTemplate, PXSelect<ContractTemplate, Where<ContractTemplate.contractCD, Equal<Required<ContractTemplate.contractCD>>, And<ContractTemplate.baseType, Equal<CTPRType.contractTemplate>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      e.NewValue
    })).Select<PXResult<ContractTemplate>, string>(Expression.Lambda<Func<PXResult<ContractTemplate>, string>>((Expression) Expression.Property((Expression) Expression.Call(c, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PXResult.GetItem)), Array.Empty<Expression>()), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Contract.get_ContractCD))), parameterExpression)).FirstOrDefault<string>() != null)
      throw new PXSetPropertyException("This contract ID is already used by an existing contract or contract template.", (PXErrorLevel) 4);
  }

  protected virtual void Contract_Type_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is Contract row))
      return;
    this.SetControlsState(row, sender);
  }

  protected virtual void Contract_CustomerID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is Contract row))
      return;
    if (((PXSelectBase) this.Contracts).Cache.GetStatus((object) row) == 2)
    {
      PX.Objects.AR.Customer customer = PXResultset<PX.Objects.AR.Customer>.op_Implicit(((PXSelectBase<PX.Objects.AR.Customer>) new PXSelect<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Required<PX.Objects.AR.Customer.bAccountID>>>>((PXGraph) this)).Select(new object[1]
      {
        (object) row.CustomerID
      }));
      string str1 = (string) null;
      string str2 = (string) null;
      if (customer != null)
      {
        if (!string.IsNullOrEmpty(customer.CuryID))
          str1 = customer.CuryID;
        if (!string.IsNullOrEmpty(customer.CuryRateTypeID))
          str2 = customer.CuryRateTypeID;
        if (string.IsNullOrEmpty(str1) || string.IsNullOrEmpty(str2))
        {
          CustomerClass customerClass = PXResultset<CustomerClass>.op_Implicit(((PXSelectBase<CustomerClass>) new PXSelect<CustomerClass, Where<CustomerClass.customerClassID, Equal<Required<CustomerClass.customerClassID>>>>((PXGraph) this)).Select(new object[1]
          {
            (object) customer.CustomerClassID
          }));
          if (customerClass != null)
          {
            if (!string.IsNullOrEmpty(str1))
            {
              string curyId = customerClass.CuryID;
            }
            if (!string.IsNullOrEmpty(str2))
              str2 = customerClass.CuryRateTypeID;
          }
        }
      }
      if (!string.IsNullOrEmpty(str2))
        row.RateTypeID = str2;
    }
    ((PXSelectBase) this.Billing).Cache.SetDefaultExt<ContractBillingSchedule.accountID>((object) ((PXSelectBase<ContractBillingSchedule>) this.Billing).Current);
    sender.SetDefaultExt<Contract.locationID>((object) row);
    this.CheckBillingAccount(((PXSelectBase<ContractBillingSchedule>) this.Billing).Current);
    row.TotalsCalculated = new int?();
  }

  protected virtual void ResetDiscounts(PXCache sender, ContractDetail line)
  {
    line.BaseDiscountID = (string) null;
    line.BaseDiscountSeq = (string) null;
    line.BaseDiscountPct = new Decimal?(0M);
    line.BaseDiscountAmt = new Decimal?(0M);
    line.RecurringDiscountID = (string) null;
    line.RecurringDiscountSeq = (string) null;
    line.RecurringDiscountPct = new Decimal?(0M);
    line.RecurringDiscountAmt = new Decimal?(0M);
    line.RenewalDiscountID = (string) null;
    line.RenewalDiscountSeq = (string) null;
    line.RenewalDiscountPct = new Decimal?(0M);
    line.RenewalDiscountAmt = new Decimal?(0M);
  }

  public static void CalculateDiscounts(PXCache sender, Contract contract, ContractDetail det)
  {
    if (contract == null || contract.DiscountID == null)
      return;
    ContractItem contractItem = PXResultset<ContractItem>.op_Implicit(((PXSelectBase<ContractItem>) new PXSelect<ContractItem, Where<ContractItem.contractItemID, Equal<Required<ContractItem.contractItemID>>>>(sender.Graph)).Select(new object[1]
    {
      (object) det.ContractItemID
    }));
    if (contractItem == null)
      return;
    DateTime? effectiveFrom;
    if (contractItem.IsBaseValid.GetValueOrDefault())
    {
      if (PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(((PXSelectBase<PX.Objects.IN.InventoryItem>) new PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<PX.Objects.IN.InventoryItem.inventoryID>>>>(sender.Graph)).Select(new object[1]
      {
        (object) contractItem.BaseItemID
      })) != null)
      {
        PXCache cache = sender;
        ContractDetail line = det;
        DiscountLineFields<DiscountLineFields.skipDisc, ContractDetail.baseDiscountAmt, ContractDetail.baseDiscountPct, ContractDetail.baseDiscountID, ContractDetail.baseDiscountSeq, DiscountLineFields.discountsAppliedToLine, DiscountLineFields.manualDisc, DiscountLineFields.manualPrice, DiscountLineFields.lineType, DiscountLineFields.isFree, DiscountLineFields.calculateDiscountsOnImport, DiscountLineFields.automaticDiscountsDisabled, DiscountLineFields.skipLineDiscounts> dLine = new DiscountLineFields<DiscountLineFields.skipDisc, ContractDetail.baseDiscountAmt, ContractDetail.baseDiscountPct, ContractDetail.baseDiscountID, ContractDetail.baseDiscountSeq, DiscountLineFields.discountsAppliedToLine, DiscountLineFields.manualDisc, DiscountLineFields.manualPrice, DiscountLineFields.lineType, DiscountLineFields.isFree, DiscountLineFields.calculateDiscountsOnImport, DiscountLineFields.automaticDiscountsDisabled, DiscountLineFields.skipLineDiscounts>(sender, (object) det);
        string discountId = contract.DiscountID;
        Decimal? basePriceVal1 = det.BasePriceVal;
        Decimal? qty1 = det.Qty;
        Decimal? basePriceVal2 = det.BasePriceVal;
        Decimal? extPrice = qty1.HasValue & basePriceVal2.HasValue ? new Decimal?(qty1.GetValueOrDefault() * basePriceVal2.GetValueOrDefault()) : new Decimal?();
        Decimal? qty2 = det.Qty;
        int? locationId = contract.LocationID;
        int? customerId = contract.CustomerID;
        string curyId = contract.CuryID;
        effectiveFrom = contract.EffectiveFrom;
        DateTime? date = new DateTime?(effectiveFrom ?? contract.StartDate.Value);
        int? branchID = new int?();
        int? baseItemId = contractItem.BaseItemID;
        DiscountEngine.SetLineDiscountOnly<ContractDetail>(cache, line, (DiscountLineFields) dLine, discountId, basePriceVal1, extPrice, qty2, locationId, customerId, curyId, date, branchID, baseItemId);
      }
    }
    if (contractItem.IsFixedRecurringValid.GetValueOrDefault() || contractItem.IsUsageValid.GetValueOrDefault())
    {
      if (PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(((PXSelectBase<PX.Objects.IN.InventoryItem>) new PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<PX.Objects.IN.InventoryItem.inventoryID>>>>(sender.Graph)).Select(new object[1]
      {
        (object) contractItem.RecurringItemID
      })) != null)
      {
        PXCache cache = sender;
        ContractDetail line = det;
        DiscountLineFields<DiscountLineFields.skipDisc, ContractDetail.recurringDiscountAmt, ContractDetail.recurringDiscountPct, ContractDetail.recurringDiscountID, ContractDetail.recurringDiscountSeq, DiscountLineFields.discountsAppliedToLine, DiscountLineFields.manualDisc, DiscountLineFields.manualPrice, DiscountLineFields.lineType, DiscountLineFields.isFree, DiscountLineFields.calculateDiscountsOnImport, DiscountLineFields.automaticDiscountsDisabled, DiscountLineFields.skipLineDiscounts> dLine = new DiscountLineFields<DiscountLineFields.skipDisc, ContractDetail.recurringDiscountAmt, ContractDetail.recurringDiscountPct, ContractDetail.recurringDiscountID, ContractDetail.recurringDiscountSeq, DiscountLineFields.discountsAppliedToLine, DiscountLineFields.manualDisc, DiscountLineFields.manualPrice, DiscountLineFields.lineType, DiscountLineFields.isFree, DiscountLineFields.calculateDiscountsOnImport, DiscountLineFields.automaticDiscountsDisabled, DiscountLineFields.skipLineDiscounts>(sender, (object) det);
        string discountId = contract.DiscountID;
        Decimal? recurringPriceVal1 = det.FixedRecurringPriceVal;
        Decimal? qty3 = det.Qty;
        Decimal? recurringPriceVal2 = det.FixedRecurringPriceVal;
        Decimal? extPrice = qty3.HasValue & recurringPriceVal2.HasValue ? new Decimal?(qty3.GetValueOrDefault() * recurringPriceVal2.GetValueOrDefault()) : new Decimal?();
        Decimal? qty4 = det.Qty;
        int? locationId = contract.LocationID;
        int? customerId = contract.CustomerID;
        string curyId = contract.CuryID;
        effectiveFrom = contract.EffectiveFrom;
        DateTime? date = new DateTime?(effectiveFrom ?? contract.StartDate.Value);
        int? branchID = new int?();
        int? recurringItemId = contractItem.RecurringItemID;
        DiscountEngine.SetLineDiscountOnly<ContractDetail>(cache, line, (DiscountLineFields) dLine, discountId, recurringPriceVal1, extPrice, qty4, locationId, customerId, curyId, date, branchID, recurringItemId);
      }
    }
    if (!contractItem.IsRenewalValid.GetValueOrDefault())
      return;
    if (PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(((PXSelectBase<PX.Objects.IN.InventoryItem>) new PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<PX.Objects.IN.InventoryItem.inventoryID>>>>(sender.Graph)).Select(new object[1]
    {
      (object) contractItem.RenewalItemID
    })) == null)
      return;
    PXCache cache1 = sender;
    ContractDetail line1 = det;
    DiscountLineFields<DiscountLineFields.skipDisc, ContractDetail.renewalDiscountAmt, ContractDetail.renewalDiscountPct, ContractDetail.renewalDiscountID, ContractDetail.renewalDiscountSeq, DiscountLineFields.discountsAppliedToLine, DiscountLineFields.manualDisc, DiscountLineFields.manualPrice, DiscountLineFields.lineType, DiscountLineFields.isFree, DiscountLineFields.calculateDiscountsOnImport, DiscountLineFields.automaticDiscountsDisabled, DiscountLineFields.skipLineDiscounts> dLine1 = new DiscountLineFields<DiscountLineFields.skipDisc, ContractDetail.renewalDiscountAmt, ContractDetail.renewalDiscountPct, ContractDetail.renewalDiscountID, ContractDetail.renewalDiscountSeq, DiscountLineFields.discountsAppliedToLine, DiscountLineFields.manualDisc, DiscountLineFields.manualPrice, DiscountLineFields.lineType, DiscountLineFields.isFree, DiscountLineFields.calculateDiscountsOnImport, DiscountLineFields.automaticDiscountsDisabled, DiscountLineFields.skipLineDiscounts>(sender, (object) det);
    string discountId1 = contract.DiscountID;
    Decimal? renewalPriceVal1 = det.RenewalPriceVal;
    Decimal? qty5 = det.Qty;
    Decimal? renewalPriceVal2 = det.RenewalPriceVal;
    Decimal? extPrice1 = qty5.HasValue & renewalPriceVal2.HasValue ? new Decimal?(qty5.GetValueOrDefault() * renewalPriceVal2.GetValueOrDefault()) : new Decimal?();
    Decimal? qty6 = det.Qty;
    int? locationId1 = contract.LocationID;
    int? customerId1 = contract.CustomerID;
    string curyId1 = contract.CuryID;
    effectiveFrom = contract.EffectiveFrom;
    DateTime? date1 = new DateTime?(effectiveFrom ?? contract.StartDate.Value);
    int? branchID1 = new int?();
    int? renewalItemId = contractItem.RenewalItemID;
    DiscountEngine.SetLineDiscountOnly<ContractDetail>(cache1, line1, (DiscountLineFields) dLine1, discountId1, renewalPriceVal1, extPrice1, qty6, locationId1, customerId1, curyId1, date1, branchID1, renewalItemId);
  }

  protected virtual void Contract_DiscountID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    Contract row = (Contract) e.Row;
    foreach (PXResult<ContractDetail> pxResult in ((PXSelectBase<ContractDetail>) this.ContractDetails).Select(Array.Empty<object>()))
    {
      ContractDetail line = PXResult<ContractDetail>.op_Implicit(pxResult);
      this.ResetDiscounts(((PXSelectBase) this.ContractDetails).Cache, line);
      ((PXSelectBase<ContractDetail>) this.ContractDetails).Update(line);
    }
  }

  protected virtual void Contract_LocationID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    Contract row = (Contract) e.Row;
    if (row == null || ((PXSelectBase<ContractBillingSchedule>) this.Billing).Current == null || !(((PXSelectBase<ContractBillingSchedule>) this.Billing).Current.BillTo == "M"))
      return;
    ((PXSelectBase<ContractBillingSchedule>) this.Billing).Current.LocationID = row.LocationID;
    ((PXSelectBase) this.Billing).Cache.Update((object) ((PXSelectBase<ContractBillingSchedule>) this.Billing).Current);
  }

  protected virtual void Contract_EffectiveFrom_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (!(e.Row is Contract row))
      return;
    if (PXResultset<PX.Objects.AR.ARInvoice>.op_Implicit(PXSelectBase<PX.Objects.AR.ARInvoice, PXSelect<PX.Objects.AR.ARInvoice, Where<PX.Objects.AR.ARInvoice.projectID, Equal<Current<Contract.contractID>>, And<PX.Objects.AR.ARInvoice.docDate, Greater<Required<PX.Objects.AR.ARInvoice.docDate>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
    {
      (object) (DateTime?) e.NewValue
    })) == null)
      return;
    sender.RaiseExceptionHandling<Contract.effectiveFrom>((object) row, e.NewValue, (Exception) new PXSetPropertyException("Invoice exists past the effective date.", (PXErrorLevel) 2));
  }

  protected virtual void Contract_ExpireDate_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (!(e.Row is Contract row))
      return;
    ContractBillingSchedule contractBillingSchedule = PXResultset<ContractBillingSchedule>.op_Implicit(((PXSelectBase<ContractBillingSchedule>) this.Billing).Select(Array.Empty<object>()));
    DateTime? nullable1;
    if (contractBillingSchedule != null && e.NewValue != null)
    {
      DateTime newValue = (DateTime) e.NewValue;
      nullable1 = contractBillingSchedule.NextDate;
      if ((nullable1.HasValue ? (newValue < nullable1.GetValueOrDefault() ? 1 : 0) : 0) != 0)
        throw new PXSetPropertyException("Expiration Date of the contract cannot be earlier than the Next Billing Date '{0}'.", new object[1]
        {
          (object) contractBillingSchedule.NextDate
        });
    }
    nullable1 = row.StartDate;
    DateTime? nullable2 = row.ActivationDate;
    DateTime? nullable3 = (nullable1.HasValue & nullable2.HasValue ? (nullable1.GetValueOrDefault() > nullable2.GetValueOrDefault() ? 1 : 0) : 0) != 0 ? row.StartDate : row.ActivationDate;
    nullable2 = (DateTime?) e.NewValue;
    nullable1 = nullable3;
    if ((nullable2.HasValue & nullable1.HasValue ? (nullable2.GetValueOrDefault() < nullable1.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      throw new PXSetPropertyException("Incorrect value. The value to be entered must be greater than or equal to {0}.", new object[1]
      {
        (object) nullable3
      });
  }

  protected static void UpdateHistoryDate<ContractDate, HistoryDate>(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
    where ContractDate : IBqlField
    where HistoryDate : IBqlField
  {
    if (!(e.Row is Contract row))
      return;
    ContractRenewalHistory contractRenewalHistory = PXResultset<ContractRenewalHistory>.op_Implicit(PXSelectBase<ContractRenewalHistory, PXSelect<ContractRenewalHistory, Where<ContractRenewalHistory.contractID, Equal<Required<Contract.contractID>>, And<ContractRenewalHistory.revID, Equal<Required<Contract.revID>>>>>.Config>.Select(sender.Graph, new object[2]
    {
      (object) row.ContractID,
      (object) row.RevID
    }));
    ((PXCache) GraphHelper.Caches<ContractRenewalHistory>(sender.Graph)).SetValue<HistoryDate>((object) contractRenewalHistory, ((PXCache) GraphHelper.Caches<Contract>(sender.Graph)).GetValue<ContractDate>((object) row));
    GraphHelper.Caches<ContractRenewalHistory>(sender.Graph).Update(contractRenewalHistory);
  }

  protected virtual void ContractDetail_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is ContractDetail row))
      return;
    if (!ContractMaint.IsValidDetailPrice((PXGraph) this, row))
      PXUIFieldAttribute.SetWarning<ContractDetail.contractItemID>(sender, (object) row, "The item has no price set up in the currency selected in the Currency ID box.");
    bool? nullable = row.WarningAmountForDeposit;
    if (nullable.GetValueOrDefault())
      ((PXSelectBase) this.RecurringDetails).Cache.RaiseExceptionHandling<ContractDetail.recurringIncluded>((object) row, (object) row.RecurringIncluded, (Exception) new PXSetPropertyException("The deposit has been fully used.", (PXErrorLevel) 3));
    bool flag = !row.LastQty.HasValue;
    PXUIFieldAttribute.SetEnabled<ContractDetail.qty>(sender, (object) row, ((PXSelectBase<Contract>) this.CurrentContract).Current.Status == "D" || ((PXSelectBase<Contract>) this.CurrentContract).Current.Status == "U");
    PXCache pxCache1 = sender;
    ContractDetail contractDetail1 = row;
    nullable = row.BasePriceEditable;
    int num1 = nullable.GetValueOrDefault() & flag ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<ContractDetail.basePriceVal>(pxCache1, (object) contractDetail1, num1 != 0);
    PXCache pxCache2 = sender;
    ContractDetail contractDetail2 = row;
    nullable = row.RenewalPriceEditable;
    int num2 = !nullable.GetValueOrDefault() ? 0 : (flag ? 1 : (((PXSelectBase<Contract>) this.CurrentContract).Current.Status == "D" ? 1 : (((PXSelectBase<Contract>) this.CurrentContract).Current.Status == "U" ? 1 : 0)));
    PXUIFieldAttribute.SetEnabled<ContractDetail.renewalPriceVal>(pxCache2, (object) contractDetail2, num2 != 0);
    PXCache pxCache3 = sender;
    ContractDetail contractDetail3 = row;
    nullable = row.FixedRecurringPriceEditable;
    int num3 = nullable.GetValueOrDefault() & flag ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<ContractDetail.fixedRecurringPriceVal>(pxCache3, (object) contractDetail3, num3 != 0);
    PXCache pxCache4 = sender;
    ContractDetail contractDetail4 = row;
    nullable = row.UsagePriceEditable;
    int num4 = nullable.GetValueOrDefault() & flag ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<ContractDetail.usagePriceVal>(pxCache4, (object) contractDetail4, num4 != 0);
    PXCache pxCache5 = sender;
    ContractDetail contractDetail5 = row;
    int num5;
    if (((PXSelectBase<Contract>) this.CurrentContract).Current.Status == "D" || ((PXSelectBase<Contract>) this.CurrentContract).Current.Status == "U")
    {
      nullable = ((PXSelectBase<ContractTemplate>) this.CurrentTemplate).Current.AllowOverride;
      num5 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num5 = 0;
    PXUIFieldAttribute.SetEnabled<ContractDetail.contractItemID>(pxCache5, (object) contractDetail5, num5 != 0);
    PXCache pxCache6 = sender;
    ContractDetail contractDetail6 = row;
    int num6;
    if (((PXSelectBase<Contract>) this.CurrentContract).Current.Status == "D" || ((PXSelectBase<Contract>) this.CurrentContract).Current.Status == "U")
    {
      nullable = ((PXSelectBase<ContractTemplate>) this.CurrentTemplate).Current.AllowOverride;
      num6 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num6 = 0;
    PXUIFieldAttribute.SetEnabled<ContractDetail.description>(pxCache6, (object) contractDetail6, num6 != 0);
    PXUIFieldAttribute.SetVisible<ContractDetail.change>(sender, (object) null, ((PXSelectBase<Contract>) this.CurrentContract).Current.Status != "A");
  }

  protected virtual void ContractDetail_RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    if (!(e.Row is ContractDetail row))
      return;
    try
    {
      ContractMaint.ValidateUniqueness((PXGraph) this, row);
    }
    catch (PXException ex)
    {
      ContractItem contractItem = (ContractItem) PXSelectorAttribute.Select<ContractDetail.contractItemID>((PXCache) GraphHelper.Caches<ContractDetail>((PXGraph) this), (object) row);
      sender.RaiseExceptionHandling<ContractDetail.contractItemID>((object) row, (object) contractItem.ContractItemCD, (Exception) ex);
      ((CancelEventArgs) e).Cancel = true;
    }
    int? revId1 = row.RevID;
    int? contractDetailId = row.ContractDetailID;
    Guid? noteId = row.NoteID;
    object[] objArray = new object[3]
    {
      (object) row.ContractID,
      (object) row.ContractItemID,
      null
    };
    int? revId2 = row.RevID;
    objArray[2] = (object) (revId2.HasValue ? new int?(revId2.GetValueOrDefault() - 1) : new int?());
    ContractDetail contractDetail1 = (ContractDetail) PXResultset<ContractDetailExt>.op_Implicit(PXSelectBase<ContractDetailExt, PXSelectReadonly<ContractDetailExt, Where<ContractDetailExt.contractID, Equal<Required<ContractDetail.contractID>>, And<ContractDetailExt.contractItemID, Equal<Required<ContractDetail.contractItemID>>, And<ContractDetailExt.revID, Equal<Required<ContractDetail.revID>>>>>>.Config>.Select((PXGraph) this, objArray));
    if (contractDetail1 == null)
      return;
    sender.RestoreCopy((object) row, (object) contractDetail1);
    row.RevID = revId1;
    row.NoteID = noteId;
    ContractDetail contractDetail2 = sender.Locate((object) row) as ContractDetail;
    row.ContractDetailID = contractDetail2 != null ? contractDetail2.ContractDetailID : contractDetailId;
    row.LastQty = row.Qty;
    row.Change = new Decimal?(0M);
    row.LastBaseDiscountPct = row.BaseDiscountPct;
    row.LastRecurringDiscountPct = row.RecurringDiscountPct;
    row.LastRenewalDiscountPct = row.RenewalDiscountPct;
  }

  protected virtual void ContractDetail_RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    ContractDetail row = e.Row as ContractDetail;
    ContractDetail newRow = e.NewRow as ContractDetail;
    if (sender.ObjectsEqual<ContractDetail.contractItemID>((object) row, (object) newRow))
      return;
    try
    {
      ContractMaint.ValidateUniqueness((PXGraph) this, newRow);
    }
    catch (PXException ex)
    {
      sender.RaiseExceptionHandling<ContractDetail.contractItemID>((object) newRow, (object) newRow.ContractItemID, (Exception) ex);
      ((CancelEventArgs) e).Cancel = true;
    }
  }

  protected virtual void ContractDetail_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    if (!(e.Row is ContractDetail row))
      return;
    ContractMaint.CalculateDiscounts(sender, ((PXSelectBase<Contract>) this.Contracts).Current, row);
    ((PXSelectBase<Contract>) this.Contracts).Current.TotalsCalculated = new int?();
    if (((PXGraph) this).IsImport)
      return;
    ContractItem contractItem1 = PXResultset<ContractItem>.op_Implicit(PXSelectBase<ContractItem, PXSelect<ContractItem, Where<ContractItem.contractItemID, Equal<Required<ContractDetail.contractItemID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.ContractItemID
    }));
    if (contractItem1 == null)
      return;
    bool? deposit = contractItem1.Deposit;
    bool flag = false;
    if (!(deposit.GetValueOrDefault() == flag & deposit.HasValue) || !contractItem1.DepositItemID.HasValue)
      return;
    ContractItem contractItem2 = PXResultset<ContractItem>.op_Implicit(PXSelectBase<ContractItem, PXSelect<ContractItem, Where<ContractItem.contractItemID, Equal<Required<ContractDetail.contractItemID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) contractItem1.DepositItemID
    }));
    ContractDetail contractDetail = new ContractDetail();
    sender.SetValueExt<ContractDetail.contractItemID>((object) contractDetail, (object) contractItem2.ContractItemID);
    ((PXSelectBase<ContractDetail>) this.ContractDetails).Insert(contractDetail);
    ((PXSelectBase) this.ContractDetails).View.RequestRefresh();
  }

  protected virtual void ContractDetail_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    ContractMaint.CalculateDiscounts(sender, ((PXSelectBase<Contract>) this.Contracts).Current, (ContractDetail) e.Row);
    ((PXSelectBase<Contract>) this.Contracts).Current.TotalsCalculated = new int?();
  }

  protected virtual void ContractDetail_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    ((PXSelectBase<Contract>) this.Contracts).Current.TotalsCalculated = new int?();
  }

  protected virtual void ContractDetail_ContractItemID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is ContractDetail row))
      return;
    ContractDetail contractDetail = PXResultset<ContractDetail>.op_Implicit(PXSelectBase<ContractDetail, PXSelect<ContractDetail, Where<ContractDetail.contractID, Equal<Current<Contract.templateID>>, And<ContractDetail.contractItemID, Equal<Required<ContractDetail.contractItemID>>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.ContractItemID
    }));
    if (contractDetail != null)
    {
      row.Qty = contractDetail.Qty;
      row.Description = contractDetail.Description;
      PXDBLocalizableStringAttribute.CopyTranslations<ContractDetail.description, ContractDetail.description>(((PXGraph) this).Caches[typeof (ContractDetail)], (object) contractDetail, ((PXGraph) this).Caches[typeof (ContractDetail)], (object) row);
    }
    else
    {
      ContractItem contractItem = PXResultset<ContractItem>.op_Implicit(PXSelectBase<ContractItem, PXSelect<ContractItem, Where<ContractItem.contractItemID, Equal<Required<ContractItem.contractItemID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) row.ContractItemID
      }));
      if (contractItem != null)
      {
        row.Qty = contractItem.DefaultQty;
        row.Description = contractItem.Descr;
        PXDBLocalizableStringAttribute.CopyTranslations<ContractItem.descr, ContractDetail.description>(((PXGraph) this).Caches[typeof (ContractItem)], (object) contractItem, ((PXGraph) this).Caches[typeof (ContractDetail)], (object) row);
      }
    }
    this.ResetDiscounts(sender, row);
  }

  protected virtual void ContractDetail_ContractItemID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    ContractDetail row = (ContractDetail) e.Row;
    ContractItem contractItem = PXResultset<ContractItem>.op_Implicit(PXSelectBase<ContractItem, PXSelect<ContractItem, Where<ContractItem.contractItemID, Equal<Required<ContractDetail.contractItemID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      e.NewValue
    }));
    Contract contract = PXResultset<Contract>.op_Implicit(PXSelectBase<Contract, PXSelect<Contract, Where<Contract.contractID, Equal<Required<ContractDetail.contractID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.ContractID
    }));
    if (contractItem != null && contract != null && contractItem.CuryID != contract.CuryID)
    {
      e.NewValue = (object) contractItem.ContractItemCD;
      throw new PXSetPropertyException("The contract item '{0}' cannot be added because its currency ({1}) does not match the {3} currency ({2})", new object[4]
      {
        (object) contractItem.ContractItemCD,
        (object) contractItem.CuryID,
        (object) contract.CuryID,
        (object) PXUIFieldAttribute.GetItemName(((PXSelectBase) this.CurrentContract).Cache)
      });
    }
  }

  protected virtual void ContractDetail_Qty_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    ContractItem contractItem = PXResultset<ContractItem>.op_Implicit(PXSelectBase<ContractItem, PXSelect<ContractItem, Where<ContractItem.contractItemID, Equal<Required<ContractDetail.contractItemID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) ((ContractDetail) e.Row).ContractItemID
    }));
    if (contractItem != null)
    {
      Decimal? maxQty = contractItem.MaxQty;
      Decimal? newValue1 = (Decimal?) e.NewValue;
      if (!(maxQty.GetValueOrDefault() < newValue1.GetValueOrDefault() & maxQty.HasValue & newValue1.HasValue))
      {
        Decimal? minQty = contractItem.MinQty;
        Decimal? newValue2 = (Decimal?) e.NewValue;
        if (!(minQty.GetValueOrDefault() > newValue2.GetValueOrDefault() & minQty.HasValue & newValue2.HasValue))
          return;
      }
      throw new PXSetPropertyException("Included Quantity must be within the {0} and {1} limits.", new object[2]
      {
        (object) PXDBQuantityAttribute.Round(new Decimal?(contractItem.MinQty.GetValueOrDefault())),
        (object) PXDBQuantityAttribute.Round(new Decimal?(contractItem.MaxQty.GetValueOrDefault()))
      });
    }
  }

  protected virtual void ContractDetail_BasePriceVal_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!((e.Row as ContractDetail).BasePriceOption == "M"))
      return;
    sender.SetValue<ContractDetail.basePrice>(e.Row, sender.GetValue<ContractDetail.basePriceVal>(e.Row));
  }

  protected virtual void ContractDetail_RenewalPriceVal_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!((e.Row as ContractDetail).RenewalPriceOption == "M"))
      return;
    sender.SetValue<ContractDetail.renewalPrice>(e.Row, sender.GetValue<ContractDetail.renewalPriceVal>(e.Row));
  }

  protected virtual void ContractDetail_FixedRecurringPriceVal_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!((e.Row as ContractDetail).FixedRecurringPriceOption == "M"))
      return;
    sender.SetValue<ContractDetail.fixedRecurringPrice>(e.Row, sender.GetValue<ContractDetail.fixedRecurringPriceVal>(e.Row));
  }

  protected virtual void ContractDetail_UsagePriceVal_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!((e.Row as ContractDetail).UsagePriceOption == "M"))
      return;
    sender.SetValue<ContractDetail.usagePrice>(e.Row, sender.GetValue<ContractDetail.usagePriceVal>(e.Row));
  }

  protected virtual void ContractBillingSchedule_RowSelected(
    PXCache sender,
    PXRowSelectedEventArgs e)
  {
    if (!(e.Row is ContractBillingSchedule row))
      return;
    this.SetControlsState(row, sender);
    PXDefaultAttribute.SetPersistingCheck<ContractBillingSchedule.accountID>(sender, (object) row, row.BillTo == "S" ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXDefaultAttribute.SetPersistingCheck<ContractBillingSchedule.locationID>(sender, (object) row, row.BillTo == "M" ? (PXPersistingCheck) 2 : (PXPersistingCheck) 1);
    PXCache pxCache1 = sender;
    ContractBillingSchedule contractBillingSchedule1 = row;
    ContractTemplate current1 = ((PXSelectBase<ContractTemplate>) this.CurrentTemplate).Current;
    int num1 = current1 != null ? (current1.AllowOverrideFormulaDescription.GetValueOrDefault() ? 1 : 0) : 0;
    PXUIFieldAttribute.SetEnabled<ContractBillingSchedule.invoiceFormula>(pxCache1, (object) contractBillingSchedule1, num1 != 0);
    PXCache pxCache2 = sender;
    ContractBillingSchedule contractBillingSchedule2 = row;
    ContractTemplate current2 = ((PXSelectBase<ContractTemplate>) this.CurrentTemplate).Current;
    int num2 = current2 != null ? (current2.AllowOverrideFormulaDescription.GetValueOrDefault() ? 1 : 0) : 0;
    PXUIFieldAttribute.SetEnabled<ContractBillingSchedule.tranFormula>(pxCache2, (object) contractBillingSchedule2, num2 != 0);
  }

  protected virtual void ContractBillingSchedule_RowPersisting(
    PXCache sender,
    PXRowPersistingEventArgs e)
  {
    ContractBillingSchedule row = (ContractBillingSchedule) e.Row;
    if (row == null)
      return;
    this.CheckBillingAccount(row);
  }

  protected virtual void ContractBillingSchedule_BillTo_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    ContractBillingSchedule row = (ContractBillingSchedule) e.Row;
    if (row == null)
      return;
    int? nullable1 = ((PXSelectBase<Contract>) this.CurrentContract).Current.CustomerID;
    if (!nullable1.HasValue)
      return;
    sender.SetDefaultExt<ContractBillingSchedule.accountID>((object) row);
    switch (row.BillTo)
    {
      case "P":
        sender.SetDefaultExt<ContractBillingSchedule.locationID>((object) row);
        break;
      case "M":
        row.LocationID = ((PXSelectBase<Contract>) this.CurrentContract).Current.LocationID;
        break;
      default:
        ContractBillingSchedule contractBillingSchedule = row;
        nullable1 = new int?();
        int? nullable2 = nullable1;
        contractBillingSchedule.LocationID = nullable2;
        break;
    }
    this.CheckBillingAccount(row);
  }

  protected virtual void ContractBillingSchedule_AccountID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    ContractBillingSchedule row = (ContractBillingSchedule) e.Row;
    if (row == null)
      return;
    switch (row.BillTo)
    {
      case "P":
        PX.Objects.AR.Customer customer = PXResultset<PX.Objects.AR.Customer>.op_Implicit(PXSelectBase<PX.Objects.AR.Customer, PXSelect<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Required<PX.Objects.CR.BAccount.parentBAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) PXResultset<PX.Objects.CR.BAccount>.op_Implicit(PXSelectBase<PX.Objects.CR.BAccount, PXSelect<PX.Objects.CR.BAccount, Where<PX.Objects.CR.BAccount.bAccountID, Equal<Required<PX.Objects.AR.Customer.bAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
          {
            (object) ((PXSelectBase<Contract>) this.CurrentContract).Current.CustomerID
          })).ParentBAccountID
        }));
        if (customer == null)
          break;
        e.NewValue = (object) customer.BAccountID;
        break;
      case "M":
        e.NewValue = (object) ((PXSelectBase<Contract>) this.CurrentContract).Current.CustomerID;
        break;
      default:
        e.NewValue = (object) null;
        break;
    }
  }

  protected virtual void ContractBillingSchedule_AccountID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    sender.SetDefaultExt<ContractBillingSchedule.locationID>(e.Row);
  }

  protected virtual void ContractBillingSchedule_LocationID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    this.BillingLocation.RaiseFieldUpdated(sender, e.Row);
    foreach (PXResult<ContractDetail> pxResult in ((PXSelectBase<ContractDetail>) this.ContractDetails).Select(Array.Empty<object>()))
    {
      ContractDetail contractDetail = PXResult<ContractDetail>.op_Implicit(pxResult);
      ((PXSelectBase) this.ContractDetails).Cache.SetDefaultExt<ContractDetail.basePriceVal>((object) contractDetail);
      ((PXSelectBase) this.ContractDetails).Cache.SetDefaultExt<ContractDetail.renewalPriceVal>((object) contractDetail);
      ((PXSelectBase) this.ContractDetails).Cache.SetDefaultExt<ContractDetail.fixedRecurringPriceVal>((object) contractDetail);
      ((PXSelectBase) this.ContractDetails).Cache.SetDefaultExt<ContractDetail.usagePriceVal>((object) contractDetail);
      GraphHelper.MarkUpdated(((PXSelectBase) this.ContractDetails).Cache, (object) contractDetail);
    }
  }

  protected virtual void SelContractWatcher_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    SelContractWatcher row = (SelContractWatcher) e.Row;
    if (!row.ContactID.HasValue)
      return;
    PX.Objects.CR.Contact contact = PXResultset<PX.Objects.CR.Contact>.op_Implicit(PXSelectBase<PX.Objects.CR.Contact, PXSelect<PX.Objects.CR.Contact, Where<PX.Objects.CR.Contact.contactID, Equal<Required<PX.Objects.CR.Contact.contactID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.ContactID
    }));
    row.FirstName = contact.FirstName;
    row.MidName = contact.MidName;
    row.LastName = contact.LastName;
    row.Title = contact.Title;
  }

  protected virtual void SelContractWatcher_ContactID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is SelContractWatcher row))
      return;
    PX.Objects.CR.Contact contact = PXResultset<PX.Objects.CR.Contact>.op_Implicit(PXSelectBase<PX.Objects.CR.Contact, PXSelect<PX.Objects.CR.Contact>.Config>.Search<PX.Objects.CR.Contact.contactID>((PXGraph) this, (object) row.ContactID, Array.Empty<object>()));
    if (contact != null && !string.IsNullOrEmpty(contact.EMail) && string.IsNullOrEmpty(row.EMail))
      row.EMail = contact.EMail;
    if (contact == null || string.IsNullOrEmpty(contact.Salutation) || !string.IsNullOrEmpty(row.Salutation))
      return;
    row.Salutation = contact.Salutation;
  }

  protected virtual void SelContractWatcher_WatchTypeID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!(e.Row is SelContractWatcher))
      return;
    e.NewValue = (object) "A";
  }

  [PXDBInt]
  [PXParent(typeof (Select<Contract, Where<Contract.contractID, Equal<Current<EPContractRate.contractID>>>>))]
  [PXDBDefault(typeof (Contract.contractID))]
  protected virtual void EPContractRate_ContractID_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt]
  [PXEPEmployeeSelector]
  [PXCheckUnique(new System.Type[] {}, IgnoreNulls = false, Where = typeof (Where<EPContractRate.earningType, Equal<Current<EPContractRate.earningType>>, And<EPContractRate.contractID, Equal<Current<EPContractRate.contractID>>>>))]
  [PXUIField(DisplayName = "Employee")]
  protected virtual void EPContractRate_EmployeeID_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXRestrictor(typeof (Where<EPEarningType.isActive, Equal<True>>), "The earning type {0} selected on the Time & Expenses Preferences (EP101000) form is inactive. Inactive earning types are not available for data entry in new activities and time entries.", new System.Type[] {typeof (EPEarningType.typeCD)})]
  [PXSelector(typeof (EPEarningType.typeCD))]
  [PXUIField(DisplayName = "Earning Type")]
  protected virtual void EPContractRate_EarningType_CacheAttached(PXCache sender)
  {
  }

  public virtual void Persist()
  {
    ((PXSelectBase<ContractBillingSchedule>) this.Billing).Current = PXResultset<ContractBillingSchedule>.op_Implicit(((PXSelectBase<ContractBillingSchedule>) this.Billing).Select(Array.Empty<object>()));
    List<ContractDetail> list = GraphHelper.RowCast<ContractDetail>((IEnumerable) ((PXSelectBase<ContractDetail>) this.ContractDetails).Select(Array.Empty<object>())).ToList<ContractDetail>();
    foreach (ContractDetail detail in list.Where<ContractDetail>((Func<ContractDetail, bool>) (detail => ((PXSelectBase<ContractBillingSchedule>) this.Billing).Current != null && ((PXSelectBase<ContractBillingSchedule>) this.Billing).Current.Type == "D")))
    {
      string itemCD;
      if (!TemplateMaint.ValidItemForOnDemand((PXGraph) this, detail, out itemCD))
      {
        ((PXSelectBase) this.ContractDetails).Cache.RaiseExceptionHandling<ContractDetail.contractItemID>((object) detail, (object) itemCD, (Exception) new PXException("For contracts with billing on demand, items cannot have any recurring settings."));
        ((PXSelectBase) this.ContractDetails).Cache.SetStatus((object) detail, (PXEntryStatus) 1);
      }
    }
    TemplateMaint.CheckContractOnDepositItems(list, ((PXSelectBase<Contract>) this.Contracts).Current);
    ((PXGraph) this).Persist();
  }

  public virtual int ExecuteUpdate(
    string viewName,
    IDictionary keys,
    IDictionary values,
    params object[] parameters)
  {
    if (viewName == "Contracts" && values != null)
    {
      this.customerChanged = values.Contains((object) "CustomerID") && values[(object) "CustomerID"] != PXCache.NotSetValue;
      this.templateChanged = values.Contains((object) "TemplateID") && values[(object) "TemplateID"] != PXCache.NotSetValue;
    }
    if (viewName.ToLower() == "billing" && values != null)
    {
      if (!((PXGraph) this).IsImport && (this.customerChanged || this.templateChanged))
        values[(object) "BillTo"] = PXCache.NotSetValue;
      if (((PXSelectBase<ContractBillingSchedule>) this.Billing).Current != null && ((PXSelectBase<ContractBillingSchedule>) this.Billing).Current.BillTo != "S")
        values[(object) "AccountID"] = PXCache.NotSetValue;
    }
    return ((PXGraph) this).ExecuteUpdate(viewName, keys, values, parameters);
  }

  public static void ValidateUniqueness(PXGraph graph, ContractDetail row, bool validateRecurring = false)
  {
    if (!row.ContractItemID.HasValue)
      return;
    ContractDetail contractDetail = PXResultset<ContractDetail>.op_Implicit(((PXSelectBase<ContractDetail>) new PXSelect<ContractDetail, Where<ContractDetail.contractItemID, Equal<Required<ContractDetail.contractItemID>>, And<ContractDetail.contractID, Equal<Required<Contract.contractID>>, And<ContractDetail.revID, Equal<Required<ContractDetail.revID>>, And<ContractDetail.lineNbr, NotEqual<Required<ContractDetail.lineNbr>>>>>>>(graph)).SelectWindowed(0, 1, new object[4]
    {
      (object) row.ContractItemID,
      (object) row.ContractID,
      (object) row.RevID,
      (object) row.LineNbr
    }));
    ContractItem contractItem = (ContractItem) PXSelectorAttribute.Select<ContractDetail.contractItemID>((PXCache) GraphHelper.Caches<ContractDetail>(graph), (object) row);
    if (contractDetail != null)
      throw new PXException("Duplicate Item Code {0}.", new object[1]
      {
        (object) contractItem.ContractItemCD
      });
    if (!validateRecurring || !contractItem.RecurringItemID.HasValue)
      return;
    PX.Objects.IN.InventoryItem inventoryItem = (PX.Objects.IN.InventoryItem) PXSelectorAttribute.Select<ContractItem.recurringItemID>((PXCache) GraphHelper.Caches<ContractItem>(graph), (object) contractItem);
    if (PXResultset<ContractDetail>.op_Implicit(((PXSelectBase<ContractDetail>) new PXSelectJoin<ContractDetail, InnerJoin<ContractItem, On<ContractDetail.contractItemID, Equal<ContractItem.contractItemID>>>, Where<ContractDetail.contractID, Equal<Required<Contract.contractID>>, And<ContractDetail.revID, Equal<Required<ContractDetail.revID>>, And<ContractItem.recurringItemID, Equal<Required<ContractItem.recurringItemID>>>>>>(graph)).SelectWindowed(0, 1, new object[3]
    {
      (object) row.ContractID,
      (object) row.RevID,
      (object) contractItem.RecurringItemID
    })) != null)
      throw new PXException("The contract cannot be activated because it contains duplicate recurring items {0}.", new object[1]
      {
        (object) inventoryItem.InventoryCD
      });
  }

  protected virtual void SetControlsState(Contract row, PXCache cache)
  {
    if (row == null)
      return;
    ContractTemplate current = ((PXSelectBase<ContractTemplate>) this.CurrentTemplate).Current;
    bool flag1 = cache.GetStatus((object) row) != 2;
    bool flag2 = row.Status == "C";
    bool? nullable1;
    int num1;
    if (!flag2)
    {
      if (current != null)
      {
        nullable1 = current.AllowOverride;
        num1 = nullable1.GetValueOrDefault() ? 1 : 0;
      }
      else
        num1 = 1;
    }
    else
      num1 = 0;
    bool flag3 = num1 != 0;
    ((PXSelectBase) this.RecurringDetails).Cache.AllowUpdate = false;
    ((PXSelectBase) this.RecurringDetails).Cache.AllowInsert = false;
    ((PXSelectBase) this.RecurringDetails).Cache.AllowDelete = false;
    ((PXSelectBase) this.ContractDetails).Cache.AllowInsert = ((!row.TemplateID.HasValue ? 0 : (row.Status == "D" ? 1 : (row.Status == "U" ? 1 : 0))) & (flag3 ? 1 : 0)) != 0;
    ((PXSelectBase) this.ContractDetails).Cache.AllowUpdate = true;
    PXCache cache1 = ((PXSelectBase) this.ContractDetails).Cache;
    int? nullable2 = row.TemplateID;
    int num2 = (!nullable2.HasValue ? 0 : (row.Status == "D" ? 1 : (row.Status == "U" ? 1 : 0))) & (flag3 ? 1 : 0);
    cache1.AllowDelete = num2 != 0;
    PXAction<Contract> terminate = this.terminate;
    int num3;
    if (flag1)
    {
      nullable2 = row.CustomerID;
      num3 = nullable2.HasValue ? 1 : 0;
    }
    else
      num3 = 0;
    ((PXAction) terminate).SetEnabled(num3 != 0);
    PXUIFieldAttribute.SetEnabled<Contract.startDate>(cache, (object) row, !flag1 || !flag2);
    PXUIFieldAttribute.SetEnabled<Contract.activationDate>(cache, (object) row, !flag1 || !flag2);
    PXUIFieldAttribute.SetEnabled<Contract.autoRenew>(cache, (object) row, (!flag1 || !flag2) && row.Type != "U");
    PXUIFieldAttribute.SetEnabled<Contract.autoRenewDays>(cache, (object) row, !flag1 || !flag2);
    PXUIFieldAttribute.SetEnabled<Contract.calendarID>(cache, (object) row, !flag1 || !flag2);
    PXUIFieldAttribute.SetEnabled<Contract.rateTypeID>(cache, (object) row, (!flag1 || !flag2) && this.IsMultyCurrency);
    PXUIFieldAttribute.SetEnabled<Contract.detailedBilling>(cache, (object) row, !flag1 || !flag2);
    PXCache pxCache1 = cache;
    Contract contract1 = row;
    nullable1 = row.IsPendingUpdate;
    int num4 = nullable1.GetValueOrDefault() ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<Contract.effectiveFrom>(pxCache1, (object) contract1, num4 != 0);
    PXCache pxCache2 = cache;
    Contract contract2 = row;
    nullable2 = row.TemplateID;
    int num5 = !nullable2.HasValue ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<Contract.type>(pxCache2, (object) contract2, num5 != 0);
    PXUIFieldAttribute.SetEnabled<Contract.templateID>(cache, (object) row, !flag1);
    PXUIFieldAttribute.SetEnabled<Contract.customerID>(cache, (object) row, !flag1);
    PXCache pxCache3 = cache;
    Contract contract3 = row;
    int num6;
    if (!this.IsMultyCurrency)
    {
      if (!flag2 && current != null)
      {
        nullable1 = current.AllowOverrideCury;
        num6 = nullable1.GetValueOrDefault() ? 1 : 0;
      }
      else
        num6 = 0;
    }
    else
      num6 = 1;
    PXUIFieldAttribute.SetVisible<Contract.curyID>(pxCache3, (object) contract3, num6 != 0);
    PXCache pxCache4 = cache;
    Contract contract4 = row;
    int num7;
    if (current != null)
    {
      nullable1 = current.AllowOverride;
      if (!nullable1.GetValueOrDefault())
      {
        num7 = 0;
        goto label_18;
      }
    }
    num7 = row.Status == "D" || row.Status == "P" ? 1 : (row.Status == "U" ? 1 : 0);
label_18:
    PXUIFieldAttribute.SetEnabled<Contract.caseItemID>(pxCache4, (object) contract4, num7 != 0);
    PXCache cache2 = ((PXSelectBase) this.SLAMapping).Cache;
    int num8;
    if (!flag1 || !flag2)
    {
      if (!flag1)
      {
        nullable2 = row.TemplateID;
        num8 = nullable2.HasValue ? 1 : 0;
      }
      else
        num8 = 1;
    }
    else
      num8 = 0;
    cache2.AllowUpdate = num8 != 0;
    PXCache cache3 = ((PXSelectBase) this.SLAMapping).Cache;
    int num9;
    if (!flag1 || !flag2)
    {
      if (!flag1)
      {
        nullable2 = row.TemplateID;
        num9 = nullable2.HasValue ? 1 : 0;
      }
      else
        num9 = 1;
    }
    else
      num9 = 0;
    cache3.AllowInsert = num9 != 0;
    PXCache cache4 = ((PXSelectBase) this.SLAMapping).Cache;
    int num10;
    if (!flag1 || !flag2)
    {
      if (!flag1)
      {
        nullable2 = row.TemplateID;
        num10 = nullable2.HasValue ? 1 : 0;
      }
      else
        num10 = 1;
    }
    else
      num10 = 0;
    cache4.AllowDelete = num10 != 0;
    ((PXAction) this.Delete).SetEnabled(row.Status == "D");
    ((PXSelectBase) this.Contracts).Cache.AllowDelete = row.Status == "D";
    PXAction<Contract> undoBilling = this.undoBilling;
    nullable1 = row.IsLastActionUndoable;
    int num11 = nullable1.GetValueOrDefault() ? 1 : 0;
    ((PXAction) undoBilling).SetEnabled(num11 != 0);
  }

  protected virtual void SetControlsState(ContractBillingSchedule row, PXCache cache)
  {
    if (row == null)
      return;
    if (((PXSelectBase<Contract>) this.Contracts).Current != null && ((PXSelectBase<Contract>) this.Contracts).Current.TemplateID.HasValue)
      PXUIFieldAttribute.SetEnabled<ContractBillingSchedule.type>(cache, (object) row, false);
    if (((PXSelectBase<Contract>) this.Contracts).Current != null && ((PXSelectBase<Contract>) this.Contracts).Current.Status != "A")
      PXUIFieldAttribute.SetEnabled<ContractBillingSchedule.nextDate>(cache, (object) row, false);
    PXUIFieldAttribute.SetEnabled<ContractBillingSchedule.accountID>(cache, (object) row, row.BillTo == "S");
    PXUIFieldAttribute.SetEnabled<ContractBillingSchedule.locationID>(cache, (object) row, row.BillTo == "S" || row.BillTo == "P");
    Contract contract = PXResultset<Contract>.op_Implicit(PXSelectBase<Contract, PXSelectReadonly<Contract, Where<Contract.originalContractID, Equal<Current<Contract.contractID>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      (object) ((PXSelectBase<Contract>) this.Contracts).Current
    }, Array.Empty<object>()));
    PXAction<Contract> renew = this.renew;
    int num;
    if (((PXSelectBase<Contract>) this.Contracts).Current != null && ((PXSelectBase<Contract>) this.Contracts).Current.Type != "U")
    {
      DateTime? nextDate = row.NextDate;
      DateTime? nullable = ((PXSelectBase<Contract>) this.Contracts).Current.ExpireDate;
      if ((nextDate.HasValue & nullable.HasValue ? (nextDate.GetValueOrDefault() >= nullable.GetValueOrDefault() ? 1 : 0) : 0) == 0)
      {
        nullable = row.NextDate;
        if (nullable.HasValue || ((PXSelectBase<Contract>) this.Contracts).Current.IsCompleted.GetValueOrDefault())
          goto label_9;
      }
      num = contract == null ? 1 : 0;
      goto label_10;
    }
label_9:
    num = 0;
label_10:
    ((PXAction) renew).SetEnabled(num != 0);
  }

  protected virtual void DefaultFromTemplate(Contract contract)
  {
    ContractTemplate contractTemplate = PXResultset<ContractTemplate>.op_Implicit(PXSelectBase<ContractTemplate, PXSelectReadonly<ContractTemplate>.Config>.Search<ContractTemplate.contractID>((PXGraph) this, (object) contract.TemplateID, Array.Empty<object>()));
    PXCache pxCache = (PXCache) GraphHelper.Caches<Contract>((PXGraph) this);
    if (contractTemplate == null)
      return;
    contract.AutoRenew = contractTemplate.AutoRenew;
    contract.AutoRenewDays = contractTemplate.AutoRenewDays;
    contract.ClassType = contractTemplate.ClassType;
    contract.CuryID = contractTemplate.CuryID;
    contract.GracePeriod = contractTemplate.GracePeriod;
    contract.RateTypeID = contractTemplate.RateTypeID;
    contract.Type = contractTemplate.Type;
    contract.Description = contractTemplate.Description;
    PXDBLocalizableStringAttribute.CopyTranslations<ContractTemplate.description, Contract.description>(((PXGraph) this).Caches[typeof (ContractTemplate)], (object) contractTemplate, pxCache, (object) contract);
    contract.Duration = contractTemplate.Duration;
    contract.CalendarID = contractTemplate.CalendarID;
    contract.DetailedBilling = contractTemplate.DetailedBilling;
    contract.CaseItemID = contractTemplate.CaseItemID;
    contract.AutomaticReleaseAR = contractTemplate.AutomaticReleaseAR;
    contract.ScheduleStartsOn = contractTemplate.ScheduleStartsOn;
    pxCache.SetValueExt<Contract.durationType>((object) contract, (object) contractTemplate.DurationType);
    ContractBillingSchedule contractBillingSchedule = PXResultset<ContractBillingSchedule>.op_Implicit(PXSelectBase<ContractBillingSchedule, PXSelectReadonly<ContractBillingSchedule, Where<ContractBillingSchedule.contractID, Equal<Current<Contract.templateID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    if (contractBillingSchedule != null && ((PXSelectBase<ContractBillingSchedule>) this.Billing).Current != null)
    {
      ((PXSelectBase<ContractBillingSchedule>) this.Billing).Current.Type = contractBillingSchedule.Type;
      ((PXSelectBase) this.Billing).Cache.SetValueExt<ContractBillingSchedule.billTo>((object) ((PXSelectBase<ContractBillingSchedule>) this.Billing).Current, (object) contractBillingSchedule.BillTo);
      ((PXSelectBase) this.Billing).Cache.SetValueExt<ContractBillingSchedule.invoiceFormula>((object) ((PXSelectBase<ContractBillingSchedule>) this.Billing).Current, (object) contractBillingSchedule.InvoiceFormula);
      ((PXSelectBase) this.Billing).Cache.SetValueExt<ContractBillingSchedule.tranFormula>((object) ((PXSelectBase<ContractBillingSchedule>) this.Billing).Current, (object) contractBillingSchedule.TranFormula);
    }
    ((PXSelectBase) this.SLAMapping).Cache.Clear();
    foreach (PXResult<ContractSLAMapping> pxResult in PXSelectBase<ContractSLAMapping, PXSelectReadonly<ContractSLAMapping, Where<ContractSLAMapping.contractID, Equal<Current<Contract.templateID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()))
    {
      ContractSLAMapping contractSlaMapping = PXResult<ContractSLAMapping>.op_Implicit(pxResult);
      ((PXSelectBase<ContractSLAMapping>) this.SLAMapping).Insert(new ContractSLAMapping()
      {
        ContractID = contract.ContractID,
        Severity = contractSlaMapping.Severity,
        Period = contractSlaMapping.Period
      });
    }
    foreach (PXResult<ContractDetail> pxResult in ((PXSelectBase<ContractDetail>) this.ContractDetails).Select(Array.Empty<object>()))
      ((PXSelectBase<ContractDetail>) this.ContractDetails).Delete(PXResult<ContractDetail>.op_Implicit(pxResult));
    foreach (PXResult<ContractDetail> pxResult in PXSelectBase<ContractDetail, PXSelectReadonly<ContractDetail, Where<ContractDetail.contractID, Equal<Current<Contract.templateID>>, And<ContractDetail.inventoryID, IsNull>>>.Config>.Select((PXGraph) this, Array.Empty<object>()))
    {
      ContractDetail source = PXResult<ContractDetail>.op_Implicit(pxResult);
      ContractDetail copy = (ContractDetail) ((PXSelectBase) this.ContractDetails).Cache.CreateCopy((object) ((PXSelectBase<ContractDetail>) this.ContractDetails).Insert(new ContractDetail()
      {
        ContractItemID = source.ContractItemID
      }));
      if (!source.Deposit.GetValueOrDefault() || copy != null)
      {
        this.CopyTemplateDetail(source, copy);
        ((PXSelectBase<ContractDetail>) this.ContractDetails).Update(copy);
      }
    }
    ContractRenewalHistory current = ((PXSelectBase<ContractRenewalHistory>) this.RenewalHistory).Current;
    CTBillEngine.UpdateContractHistoryEntry(current, contract, ((PXSelectBase<ContractBillingSchedule>) this.Billing).Current);
    ((PXSelectBase<ContractRenewalHistory>) this.RenewalHistory).Update(current);
  }

  protected virtual void CopyTemplateDetail(ContractDetail source, ContractDetail target)
  {
    target.Description = source.Description;
    PXDBLocalizableStringAttribute.CopyTranslations<ContractDetail.description, ContractDetail.description>(((PXGraph) this).Caches[typeof (ContractDetail)], (object) source, ((PXGraph) this).Caches[typeof (ContractDetail)], (object) target);
    target.Included = source.Included;
    target.InventoryID = source.InventoryID;
    target.ResetUsage = source.ResetUsage;
    target.UOM = source.UOM;
    target.ContractItemID = source.ContractItemID;
    target.Qty = source.Qty;
  }

  protected virtual void CopyContractDetail(ContractDetail source, ContractDetail target)
  {
    this.CopyTemplateDetail(source, target);
    target.BasePrice = source.BasePrice;
    target.BasePriceOption = source.BasePriceOption;
    target.RenewalPrice = source.RenewalPrice;
    target.RenewalPriceOption = source.RenewalPriceOption;
    target.FixedRecurringPrice = source.FixedRecurringPrice;
    target.FixedRecurringPriceOption = source.FixedRecurringPriceOption;
    target.FixedRecurringPrice = source.FixedRecurringPrice;
    target.FixedRecurringPriceOption = source.FixedRecurringPriceOption;
    target.UsagePrice = source.UsagePrice;
    target.UsagePriceOption = source.UsagePriceOption;
  }

  private void CalcDetail(Contract row)
  {
    Decimal num1 = 0M;
    Decimal num2 = 0M;
    Decimal num3 = 0M;
    Decimal num4 = 0M;
    Decimal num5 = 0M;
    Decimal num6 = 0M;
    PXView view = ((PXSelectBase) this.ContractDetails).View;
    object[] objArray1 = new object[1]{ (object) row };
    object[] objArray2 = Array.Empty<object>();
    foreach (PXResult<ContractDetail, ContractItem> pxResult in view.SelectMultiBound(objArray1, objArray2))
    {
      ContractDetail contractDetail = PXResult<ContractDetail, ContractItem>.op_Implicit(pxResult);
      PXResult<ContractDetail, ContractItem>.op_Implicit(pxResult);
      Decimal num7 = num1;
      Decimal? nullable = contractDetail.Change;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = contractDetail.BasePriceVal;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      Decimal num8 = valueOrDefault1 * valueOrDefault2;
      nullable = contractDetail.BaseDiscountPct;
      Decimal num9 = 100M - nullable.GetValueOrDefault();
      Decimal num10 = num8 * num9 / 100M;
      num1 = num7 + num10;
      Decimal num11 = num2;
      nullable = contractDetail.Qty;
      Decimal valueOrDefault3 = nullable.GetValueOrDefault();
      nullable = contractDetail.FixedRecurringPriceVal;
      Decimal valueOrDefault4 = nullable.GetValueOrDefault();
      Decimal num12 = valueOrDefault3 * valueOrDefault4;
      nullable = contractDetail.RecurringDiscountPct;
      Decimal num13 = 100M - nullable.GetValueOrDefault();
      Decimal num14 = num12 * num13 / 100M;
      num2 = num11 + num14;
      Decimal num15 = num3;
      nullable = contractDetail.Qty;
      Decimal valueOrDefault5 = nullable.GetValueOrDefault();
      nullable = contractDetail.RenewalPriceVal;
      Decimal valueOrDefault6 = nullable.GetValueOrDefault();
      Decimal num16 = valueOrDefault5 * valueOrDefault6;
      nullable = contractDetail.RenewalDiscountPct;
      Decimal num17 = 100M - nullable.GetValueOrDefault();
      Decimal num18 = num16 * num17 / 100M;
      num3 = num15 + num18;
      Decimal num19 = num4;
      nullable = contractDetail.LastQty;
      Decimal valueOrDefault7 = nullable.GetValueOrDefault();
      nullable = contractDetail.BasePriceVal;
      Decimal valueOrDefault8 = nullable.GetValueOrDefault();
      Decimal num20 = valueOrDefault7 * valueOrDefault8;
      nullable = contractDetail.LastBaseDiscountPct;
      Decimal num21 = 100M - nullable.GetValueOrDefault();
      Decimal num22 = num20 * num21 / 100M;
      num4 = num19 + num22;
      Decimal num23 = num5;
      nullable = contractDetail.LastQty;
      Decimal valueOrDefault9 = nullable.GetValueOrDefault();
      nullable = contractDetail.FixedRecurringPriceVal;
      Decimal valueOrDefault10 = nullable.GetValueOrDefault();
      Decimal num24 = valueOrDefault9 * valueOrDefault10;
      nullable = contractDetail.LastRecurringDiscountPct;
      Decimal num25 = 100M - nullable.GetValueOrDefault();
      Decimal num26 = num24 * num25 / 100M;
      num5 = num23 + num26;
      Decimal num27 = num6;
      nullable = contractDetail.LastQty;
      Decimal valueOrDefault11 = nullable.GetValueOrDefault();
      nullable = contractDetail.RenewalPriceVal;
      Decimal valueOrDefault12 = nullable.GetValueOrDefault();
      Decimal num28 = valueOrDefault11 * valueOrDefault12;
      nullable = contractDetail.LastRenewalDiscountPct;
      Decimal num29 = 100M - nullable.GetValueOrDefault();
      Decimal num30 = num28 * num29 / 100M;
      num6 = num27 + num30;
    }
    row.PendingSetup = new Decimal?(num1);
    row.PendingRecurring = new Decimal?(num2);
    row.PendingRenewal = new Decimal?(num3);
    row.CurrentSetup = new Decimal?(row.Status == "A" ? num1 : num4);
    row.CurrentRecurring = new Decimal?(row.Status == "A" ? num2 : num5);
    row.CurrentRenewal = new Decimal?(row.Status == "A" ? num3 : num6);
    Contract contract = row;
    Decimal? pendingRecurring = row.PendingRecurring;
    Decimal? nullable1 = row.PendingRenewal;
    Decimal? nullable2 = pendingRecurring.HasValue & nullable1.HasValue ? new Decimal?(pendingRecurring.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
    Decimal? pendingSetup = row.PendingSetup;
    Decimal? nullable3;
    if (!(nullable2.HasValue & pendingSetup.HasValue))
    {
      nullable1 = new Decimal?();
      nullable3 = nullable1;
    }
    else
      nullable3 = new Decimal?(nullable2.GetValueOrDefault() + pendingSetup.GetValueOrDefault());
    contract.TotalPending = nullable3;
  }

  private void CalcSummary(PXCache sender, Contract row)
  {
    PXSelectGroupBy<PX.Objects.AR.ARInvoice, Where<PX.Objects.AR.ARInvoice.projectID, Equal<Required<Contract.contractID>>, And<PX.Objects.AR.ARInvoice.docType, NotEqual<ARDocType.creditMemo>, And<PX.Objects.AR.ARInvoice.released, Equal<True>>>>, Aggregate<Sum<PX.Objects.AR.ARInvoice.curyDocBal>>> pxSelectGroupBy = new PXSelectGroupBy<PX.Objects.AR.ARInvoice, Where<PX.Objects.AR.ARInvoice.projectID, Equal<Required<Contract.contractID>>, And<PX.Objects.AR.ARInvoice.docType, NotEqual<ARDocType.creditMemo>, And<PX.Objects.AR.ARInvoice.released, Equal<True>>>>, Aggregate<Sum<PX.Objects.AR.ARInvoice.curyDocBal>>>((PXGraph) this);
    PXSelectBase<PX.Objects.AR.ARInvoice> pxSelectBase = (PXSelectBase<PX.Objects.AR.ARInvoice>) new PXSelectGroupBy<PX.Objects.AR.ARInvoice, Where<PX.Objects.AR.ARInvoice.projectID, Equal<Required<Contract.contractID>>, And<PX.Objects.AR.ARInvoice.docType, Equal<ARDocType.creditMemo>, And<PX.Objects.AR.ARInvoice.released, Equal<True>>>>, Aggregate<Sum<PX.Objects.AR.ARInvoice.curyDocBal>>>((PXGraph) this);
    object[] objArray1 = new object[1]
    {
      (object) row.ContractID
    };
    PX.Objects.AR.ARInvoice arInvoice1 = ((PXSelectBase<PX.Objects.AR.ARInvoice>) pxSelectGroupBy).SelectSingle(objArray1) ?? new PX.Objects.AR.ARInvoice();
    PX.Objects.AR.ARInvoice arInvoice2 = pxSelectBase.SelectSingle(new object[1]
    {
      (object) row.ContractID
    }) ?? new PX.Objects.AR.ARInvoice();
    Contract contract1 = row;
    Decimal? curyDocBal = arInvoice1.CuryDocBal;
    Decimal valueOrDefault1 = curyDocBal.GetValueOrDefault();
    curyDocBal = arInvoice2.CuryDocBal;
    Decimal valueOrDefault2 = curyDocBal.GetValueOrDefault();
    Decimal? nullable1 = new Decimal?(valueOrDefault1 - valueOrDefault2);
    contract1.Balance = nullable1;
    Decimal? nullable2 = new Decimal?(0M);
    Decimal? nullable3 = new Decimal?(0M);
    PXView view = ((PXSelectBase) this.RecurringDetails).View;
    object[] objArray2 = new object[1]{ (object) row };
    object[] objArray3 = Array.Empty<object>();
    foreach (PXResult<ContractDetail, ContractItem, PX.Objects.IN.InventoryItem> pxResult in view.SelectMultiBound(objArray2, objArray3))
    {
      ContractDetail contractDetail = PXResult<ContractDetail, ContractItem, PX.Objects.IN.InventoryItem>.op_Implicit(pxResult);
      ContractItem contractItem = PXResult<ContractDetail, ContractItem, PX.Objects.IN.InventoryItem>.op_Implicit(pxResult);
      if (!contractItem.DepositItemID.HasValue)
      {
        Decimal? nullable4 = nullable2;
        Decimal? nullable5 = contractDetail.Qty;
        Decimal valueOrDefault3 = nullable5.GetValueOrDefault();
        nullable5 = contractDetail.FixedRecurringPriceVal;
        Decimal valueOrDefault4 = nullable5.GetValueOrDefault();
        Decimal num1 = valueOrDefault3 * valueOrDefault4;
        nullable5 = contractDetail.RecurringDiscountPct;
        Decimal num2 = 100M - nullable5.GetValueOrDefault();
        Decimal num3 = num1 * num2 * 0.01M;
        Decimal? nullable6;
        if (!nullable4.HasValue)
        {
          nullable5 = new Decimal?();
          nullable6 = nullable5;
        }
        else
          nullable6 = new Decimal?(nullable4.GetValueOrDefault() + num3);
        nullable2 = nullable6;
        Decimal num4;
        if (contractItem.ResetUsageOnBilling.GetValueOrDefault())
        {
          nullable4 = contractDetail.Used;
          Decimal valueOrDefault5 = nullable4.GetValueOrDefault();
          nullable4 = contractDetail.Qty;
          Decimal valueOrDefault6 = nullable4.GetValueOrDefault();
          num4 = valueOrDefault5 - valueOrDefault6;
        }
        else
        {
          nullable4 = contractDetail.UsedTotal;
          Decimal valueOrDefault7 = nullable4.GetValueOrDefault();
          nullable4 = contractDetail.Used;
          Decimal valueOrDefault8 = nullable4.GetValueOrDefault();
          Decimal num5 = valueOrDefault7 - valueOrDefault8;
          nullable4 = contractDetail.Qty;
          Decimal val1 = nullable4.GetValueOrDefault() - num5;
          nullable4 = contractDetail.Used;
          num4 = nullable4.GetValueOrDefault() - Math.Max(val1, 0M);
        }
        if (num4 > 0M)
        {
          nullable4 = nullable3;
          Decimal num6 = num4;
          nullable5 = contractDetail.UsagePriceVal;
          Decimal valueOrDefault9 = nullable5.GetValueOrDefault();
          Decimal num7 = num6 * valueOrDefault9;
          Decimal? nullable7;
          if (!nullable4.HasValue)
          {
            nullable5 = new Decimal?();
            nullable7 = nullable5;
          }
          else
            nullable7 = new Decimal?(nullable4.GetValueOrDefault() + num7);
          nullable3 = nullable7;
        }
      }
    }
    CTBillEngine instance = PXGraph.CreateInstance<CTBillEngine>();
    Decimal? nullable8;
    try
    {
      Decimal? nullable9 = nullable3;
      Decimal? nullable10 = instance.RecalcDollarUsage(row);
      nullable8 = nullable9.HasValue & nullable10.HasValue ? new Decimal?(nullable9.GetValueOrDefault() + nullable10.GetValueOrDefault()) : new Decimal?();
    }
    catch (Exception ex)
    {
      nullable8 = new Decimal?();
      sender.RaiseExceptionHandling<Contract.totalUsage>((object) row, (object) null, (Exception) new PXSetPropertyException("Extra Usage Total cannot be calculated.", (PXErrorLevel) 2));
    }
    row.TotalRecurring = nullable2;
    row.TotalUsage = nullable8;
    Contract contract2 = row;
    Decimal? nullable11 = nullable2;
    Decimal? nullable12 = nullable8;
    Decimal? nullable13 = nullable11.HasValue & nullable12.HasValue ? new Decimal?(nullable11.GetValueOrDefault() + nullable12.GetValueOrDefault()) : new Decimal?();
    contract2.TotalDue = nullable13;
  }

  protected virtual void RenewExpiring(
    ContractMaint graph,
    DateTime renewalDate,
    string contractCD)
  {
    Contract contract = PXResultset<Contract>.op_Implicit(PXSelectBase<Contract, PXSelect<Contract, Where<Contract.originalContractID, Equal<Current<Contract.contractID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    if (contract != null)
      throw new PXException("This Contract has already been renewed. An Expiring Contract can be renewed only once.  See {0}.", new object[1]
      {
        (object) contract.ContractCD
      });
    Contract newContract = PXCache<Contract>.CreateCopy(((PXSelectBase<Contract>) this.Contracts).Current);
    PXDBLocalizableStringAttribute.CopyTranslations<Contract.description, Contract.description>(((PXSelectBase) this.CurrentContract).Cache, (object) ((PXSelectBase<Contract>) this.CurrentContract).Current, ((PXSelectBase) this.CurrentContract).Cache, (object) newContract);
    newContract.ContractCD = contractCD;
    newContract.ContractID = new int?();
    newContract.NoteID = new Guid?(Guid.NewGuid());
    newContract.IsCompleted = new bool?(false);
    newContract.IsActive = new bool?(false);
    newContract.IsPendingUpdate = new bool?(false);
    newContract.RevID = new int?(1);
    newContract.LastActiveRevID = new int?();
    ContractBillingSchedule contractBillingSchedule1 = PXResultset<ContractBillingSchedule>.op_Implicit(((PXSelectBase<ContractBillingSchedule>) this.Billing).Select(Array.Empty<object>()));
    if (ContractMaint.IsExpired(((PXSelectBase<Contract>) this.Contracts).Current, renewalDate) && contractBillingSchedule1 != null && contractBillingSchedule1.Type == "D")
    {
      ((PXSelectBase<Contract>) this.Contracts).Current.IsCompleted = new bool?(true);
      ((SelectedEntityEvent<Contract>) PXEntityEventBase<Contract>.Container<Contract.Events>.Select((Expression<Func<Contract.Events, PXEntityEvent<Contract.Events>>>) (ev => ev.ExpireContract))).FireOn((PXGraph) this, ((PXSelectBase<Contract>) this.Contracts).Current);
    }
    newContract.OriginalContractID = ((PXSelectBase<Contract>) this.Contracts).Current.ContractID;
    newContract.StartDate = new DateTime?(this.GetNextStartDate());
    newContract.ActivationDate = newContract.StartDate;
    newContract.RenewalBillingStartDate = new DateTime?();
    newContract.ExpireDate = new DateTime?();
    newContract.LineCtr = new int?(0);
    newContract.CustomerID = new int?();
    newContract.LocationID = new int?();
    newContract = ((PXSelectBase<Contract>) graph.Contracts).Insert(newContract);
    ((PXSelectBase<ContractRenewalHistory>) graph.RenewalHistory).Current.ChildContractID = newContract.OriginalContractID;
    ContractBillingSchedule contractBillingSchedule2 = PXResultset<ContractBillingSchedule>.op_Implicit(((PXSelectBase<ContractBillingSchedule>) this.Billing).Select(Array.Empty<object>()));
    ((PXSelectBase<ContractBillingSchedule>) graph.Billing).Current.Type = contractBillingSchedule2.Type;
    ((PXSelectBase<ContractBillingSchedule>) graph.Billing).Current.BillTo = contractBillingSchedule2.BillTo;
    ((PXSelectBase<ContractBillingSchedule>) graph.Billing).Update(((PXSelectBase<ContractBillingSchedule>) graph.Billing).Current);
    newContract.CustomerID = ((PXSelectBase<Contract>) this.Contracts).Current.CustomerID;
    newContract.LocationID = ((PXSelectBase<Contract>) this.Contracts).Current.LocationID;
    newContract = ((PXSelectBase<Contract>) graph.Contracts).Update(newContract);
    ((PXSelectBase) graph.SLAMapping).Cache.Clear();
    EnumerableExtensions.ForEach<ContractSLAMapping>(GraphHelper.RowCast<ContractSLAMapping>((IEnumerable) PXSelectBase<ContractSLAMapping, PXSelectReadonly<ContractSLAMapping, Where<ContractSLAMapping.contractID, Equal<Current<Contract.contractID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>())), (System.Action<ContractSLAMapping>) (item => ((PXSelectBase<ContractSLAMapping>) graph.SLAMapping).Insert(new ContractSLAMapping()
    {
      ContractID = newContract.ContractID,
      Severity = item.Severity,
      Period = item.Period
    })));
    foreach (SelContractWatcher selContractWatcher in ((IEnumerable<PXResult<SelContractWatcher>>) PXSelectBase<SelContractWatcher, PXSelectReadonly<SelContractWatcher, Where<SelContractWatcher.contractID, Equal<Current<Contract.contractID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>())).AsEnumerable<PXResult<SelContractWatcher>>().Cast<SelContractWatcher>().Select<SelContractWatcher, SelContractWatcher>((Func<SelContractWatcher, SelContractWatcher>) (item => (SelContractWatcher) ((PXSelectBase) this.Watchers).Cache.CreateCopy((object) item))))
    {
      selContractWatcher.ContractID = newContract.ContractID;
      ((PXSelectBase<SelContractWatcher>) graph.Watchers).Insert(selContractWatcher);
    }
    foreach (CSAnswers csAnswers in GraphHelper.RowCast<CSAnswers>((IEnumerable) PXSelectBase<CSAnswers, PXSelectReadonly<CSAnswers, Where<CSAnswers.refNoteID, Equal<Current<Contract.noteID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>())).Select<CSAnswers, CSAnswers>((Func<CSAnswers, CSAnswers>) (cSAnswer => (CSAnswers) ((PXSelectBase) this.Answers).Cache.CreateCopy((object) cSAnswer))))
    {
      csAnswers.RefNoteID = newContract.NoteID;
      graph.Answers.Insert(csAnswers);
    }
    foreach (PXResult<ContractDetail> pxResult in PXSelectBase<ContractDetail, PXSelect<ContractDetail, Where<ContractDetail.contractID, Equal<Current<Contract.contractID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()))
    {
      ContractDetail source = PXResult<ContractDetail>.op_Implicit(pxResult);
      this.CopyContractDetail(source, ((PXSelectBase<ContractDetail>) graph.ContractDetails).Insert(new ContractDetail()));
      ContractDetail copy = PXCache<ContractDetail>.CreateCopy(source);
      ((PXSelectBase) this.ContractDetails).Cache.Remove((object) copy);
      ContractDetail contractDetail = copy;
      int? revId = contractDetail.RevID;
      contractDetail.RevID = revId.HasValue ? new int?(revId.GetValueOrDefault() + 1) : new int?();
      copy.NoteID = new Guid?(Guid.NewGuid());
      PXDBLocalizableStringAttribute.CopyTranslations<ContractDetail.description, ContractDetail.description>(((PXSelectBase) this.ContractDetails).Cache, (object) source, ((PXSelectBase) this.ContractDetails).Cache, (object) copy);
      ((PXSelectBase<ContractDetail>) this.ContractDetails).Insert(copy);
    }
    if (!((PXSelectBase<ContractTemplate>) this.CurrentTemplate).Current.RefreshOnRenewal.GetValueOrDefault())
      return;
    foreach (PXResult<ContractDetail> pxResult in ((PXSelectBase<ContractDetail>) graph.ContractDetails).Select(Array.Empty<object>()))
    {
      ContractDetail target = PXResult<ContractDetail>.op_Implicit(pxResult);
      ContractDetail source = PXResultset<ContractDetail>.op_Implicit(PXSelectBase<ContractDetail, PXSelect<ContractDetail, Where<ContractDetail.contractID, Equal<Current<Contract.templateID>>, And<ContractDetail.contractItemID, Equal<Required<ContractDetail.contractItemID>>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) target.ContractItemID
      }));
      if (source != null)
      {
        graph.CopyTemplateDetail(source, target);
        ((PXSelectBase) graph.ContractDetails).Cache.SetDefaultExt<ContractDetail.basePrice>((object) target);
        ((PXSelectBase) graph.ContractDetails).Cache.SetDefaultExt<ContractDetail.renewalPrice>((object) target);
        ((PXSelectBase) graph.ContractDetails).Cache.SetDefaultExt<ContractDetail.fixedRecurringPrice>((object) target);
        ((PXSelectBase) graph.ContractDetails).Cache.SetDefaultExt<ContractDetail.usagePrice>((object) target);
        ((PXSelectBase) graph.ContractDetails).Cache.SetDefaultExt<ContractDetail.basePriceOption>((object) target);
        ((PXSelectBase) graph.ContractDetails).Cache.SetDefaultExt<ContractDetail.renewalPriceOption>((object) target);
        ((PXSelectBase) graph.ContractDetails).Cache.SetDefaultExt<ContractDetail.fixedRecurringPriceOption>((object) target);
        ((PXSelectBase) graph.ContractDetails).Cache.SetDefaultExt<ContractDetail.usagePriceOption>((object) target);
        ((PXSelectBase<ContractDetail>) graph.ContractDetails).Update(target);
      }
    }
  }

  protected virtual DateTime GetNextStartDate()
  {
    if (((PXSelectBase<ContractTemplate>) this.CurrentTemplate).Current == null)
      return ((PXSelectBase<Contract>) this.Contracts).Current.ExpireDate.Value.AddDays(1.0);
    return ((PXSelectBase<ContractTemplate>) this.CurrentTemplate).Current.IsContinuous.GetValueOrDefault() || !ContractMaint.IsExpired(((PXSelectBase<Contract>) this.Contracts).Current, ((PXGraph) this).Accessinfo.BusinessDate.Value) ? ((PXSelectBase<Contract>) this.Contracts).Current.ExpireDate.Value.AddDays(1.0) : ((PXGraph) this).Accessinfo.BusinessDate.Value;
  }

  public static bool IsExpired(Contract row, DateTime businessDate)
  {
    return row.ExpireDate.HasValue && businessDate.Date.Subtract(row.ExpireDate.Value).Days > row.GracePeriod.GetValueOrDefault();
  }

  public static bool IsInGracePeriod(Contract row, DateTime businessDate, out int daysLeft)
  {
    daysLeft = 0;
    if (!row.ExpireDate.HasValue)
      return false;
    int days = businessDate.Subtract(row.ExpireDate.Value.Date).Days;
    if (days <= 0 || days >= row.GracePeriod.GetValueOrDefault())
      return false;
    daysLeft = row.GracePeriod.GetValueOrDefault() - days;
    return true;
  }

  protected bool IsMultyCurrency => PXAccess.FeatureInstalled<FeaturesSet.multicurrency>();

  private void CheckBillingAccount(ContractBillingSchedule schedule)
  {
    if (!schedule.AccountID.HasValue && schedule.BillTo == "P")
      ((PXSelectBase) this.Billing).Cache.RaiseExceptionHandling<ContractBillingSchedule.billTo>((object) schedule, (object) "P", (Exception) new PXSetPropertyException("Customer does not have Parent Account"));
    if (!schedule.AccountID.HasValue || ((PXSelectBase<Contract>) this.CurrentContract).Current == null)
      return;
    PX.Objects.AR.Customer customer = PXResultset<PX.Objects.AR.Customer>.op_Implicit(PXSelectBase<PX.Objects.AR.Customer, PXSelect<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Required<PX.Objects.AR.Customer.bAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) schedule.AccountID
    }));
    CustomerClass customerClass = PXResultset<CustomerClass>.op_Implicit(PXSelectBase<CustomerClass, PXSelect<CustomerClass, Where<CustomerClass.customerClassID, Equal<Required<PX.Objects.AR.Customer.customerClassID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) customer.CustomerClassID
    }));
    string str = customer.CuryID ?? customerClass.CuryID;
    if (((PXSelectBase<Contract>) this.CurrentContract).Current.CuryID == null || !(((PXSelectBase<Contract>) this.CurrentContract).Current.CuryID != str) || customer.AllowOverrideCury.GetValueOrDefault())
      return;
    ((PXSelectBase) this.Billing).Cache.RaiseExceptionHandling<ContractBillingSchedule.accountID>((object) schedule, (object) customer.AcctCD, (Exception) new PXSetPropertyException("Customer Currency does not match with Contract Currency and Currency Overriding is not allowed for the Customer."));
  }

  public static bool IsValidDetailPrice(PXGraph graph, ContractDetail detail, out string message)
  {
    PXCache cache = (PXCache) GraphHelper.Caches<ContractDetail>(graph);
    return ContractMaint.IsValidPrice<ContractDetail.basePriceVal>(cache, detail, out message) && ContractMaint.IsValidPrice<ContractDetail.fixedRecurringPriceVal>(cache, detail, out message) && ContractMaint.IsValidPrice<ContractDetail.renewalPriceVal>(cache, detail, out message);
  }

  public static bool IsValidDetailPrice(PXGraph graph, ContractDetail detail)
  {
    return ContractMaint.IsValidDetailPrice(graph, detail, out string _);
  }

  private static bool IsValidPrice<PriceField>(
    PXCache cache,
    ContractDetail detail,
    out string message)
    where PriceField : IBqlField
  {
    message = detail == null || cache.GetValue<PriceField>((object) detail) != null ? (string) null : PXUIFieldAttribute.GetDisplayName<PriceField>(cache);
    return message == null;
  }

  public class SingleCurrency : SingleCurrencyGraph<ContractMaint, Contract>
  {
  }

  [Serializable]
  public class SetupSettingsFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected DateTime? _StartDate;

    [PXDefault(typeof (Contract.startDate))]
    [PXDBDate]
    [PXUIField(DisplayName = "Setup Date", Required = true)]
    public virtual DateTime? StartDate
    {
      get => this._StartDate;
      set => this._StartDate = value;
    }

    public abstract class startDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ContractMaint.SetupSettingsFilter.startDate>
    {
    }
  }

  [Serializable]
  public class ActivationSettingsFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected DateTime? _ActivationDate;

    [PXFormula(typeof (Switch<Case<Where<Current<Contract.status>, Equal<Contract.status.inUpgrade>, And<Current<Contract.effectiveFrom>, IsNotNull>>, Current<Contract.effectiveFrom>>, Current<Contract.activationDate>>))]
    [PXDBDate]
    [PXUIField(DisplayName = "Activation Date", Required = true)]
    public virtual DateTime? ActivationDate
    {
      get => this._ActivationDate;
      set => this._ActivationDate = value;
    }

    [PXString]
    [PXUIField]
    public virtual string ActionName { get; set; }

    public abstract class activationDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ContractMaint.ActivationSettingsFilter.activationDate>
    {
    }

    public abstract class actionName : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ContractMaint.ActivationSettingsFilter.actionName>
    {
    }
  }

  [Serializable]
  public class TerminationSettingsFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected DateTime? _TerminationDate;

    [PXDefault(typeof (AccessInfo.businessDate))]
    [PXDate]
    [PXUIField(DisplayName = "Termination Date", Required = true)]
    public virtual DateTime? TerminationDate
    {
      get => this._TerminationDate;
      set => this._TerminationDate = value;
    }

    public abstract class terminationDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ContractMaint.TerminationSettingsFilter.terminationDate>
    {
    }
  }

  [Serializable]
  public class BillingOnDemandSettingsFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected DateTime? _BillingDate;

    [PXDefault(typeof (AccessInfo.businessDate))]
    [PXDate]
    [PXUIField(DisplayName = "Billing Date", Required = true)]
    public virtual DateTime? BillingDate
    {
      get => this._BillingDate;
      set => this._BillingDate = value;
    }

    public abstract class billingDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ContractMaint.BillingOnDemandSettingsFilter.billingDate>
    {
    }
  }

  [Serializable]
  public class RenewalSettingsFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDefault(typeof (AccessInfo.businessDate))]
    [PXDBDate]
    [PXUIField(DisplayName = "Renewal Date", Required = true)]
    public virtual DateTime? RenewalDate { get; set; }

    public abstract class renewalDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ContractMaint.RenewalSettingsFilter.renewalDate>
    {
    }
  }
}
