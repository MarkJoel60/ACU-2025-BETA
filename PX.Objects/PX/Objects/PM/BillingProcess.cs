// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.BillingProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR;
using PX.Objects.AR.MigrationMode;
using PX.Objects.CS;
using PX.Objects.CT;
using PX.Objects.GL;
using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

[TableDashboardType]
public class BillingProcess : PXGraph<
#nullable disable
BillingProcess>
{
  public PXCancel<BillingProcess.BillingFilter> Cancel;
  public PXFilter<BillingProcess.BillingFilter> Filter;
  [PXFilterable(new Type[] {})]
  public PXFilteredProcessing<PX.Objects.PM.Billing.Contract, BillingProcess.BillingFilter> Items;
  public PXSetup<PMSetup> Setup;
  public PXAction<BillingProcess.BillingFilter> viewDocumentProject;
  public PXSelectJoinGroupBy<PX.Objects.PM.Billing.Contract, InnerJoin<PMUnbilledDailySummary, On<PMUnbilledDailySummary.projectID, Equal<PX.Objects.PM.Billing.Contract.contractID>>, InnerJoin<ContractBillingSchedule, On<PX.Objects.PM.Billing.Contract.contractID, Equal<ContractBillingSchedule.contractID>>, InnerJoin<PX.Objects.AR.Customer, On<PX.Objects.PM.Billing.Contract.customerID, Equal<PX.Objects.AR.Customer.bAccountID>>, InnerJoin<PMTask, On<PMTask.projectID, Equal<PMUnbilledDailySummary.projectID>, And<PMTask.isActive, Equal<True>, And<PMTask.taskID, Equal<PMUnbilledDailySummary.taskID>, And<Where<PMTask.billingOption, Equal<PMBillingOption.onBilling>, Or2<Where<PMTask.billingOption, Equal<PMBillingOption.onTaskCompletion>, And<PMTask.isCompleted, Equal<True>>>, Or<Where<PMTask.billingOption, Equal<PMBillingOption.onProjectCompetion>, And<PX.Objects.PM.Billing.Contract.isCompleted, Equal<True>>>>>>>>>>, InnerJoin<PMBillingRule, On<PMBillingRule.billingID, Equal<PMTask.billingID>, And<PMBillingRule.accountGroupID, Equal<PMUnbilledDailySummary.accountGroupID>>>>>>>>, Where2<Where<ContractBillingSchedule.nextDate, LessEqual<Current<BillingProcess.BillingFilter.invoiceDate>>, Or<ContractBillingSchedule.type, Equal<BillingType.BillingOnDemand>>>, And2<Where<PMBillingRule.includeNonBillable, Equal<False>, And<PMUnbilledDailySummary.billable, Greater<int0>, Or<Where<PMBillingRule.includeNonBillable, Equal<True>, And<Where<PMUnbilledDailySummary.nonBillable, Greater<int0>, Or<PMUnbilledDailySummary.billable, Greater<int0>>>>>>>>, And2<Where<PMUnbilledDailySummary.date, LessEqual<Current<BillingProcess.BillingFilter.invoiceDate>>>, And2<Match<Current<AccessInfo.userName>>, And<PX.Objects.PM.Billing.Contract.status, NotEqual<ProjectStatus.closed>>>>>>, Aggregate<GroupBy<PX.Objects.PM.Billing.Contract.contractID>>> ProjectsUnbilled;
  public PXSelectJoinGroupBy<PX.Objects.PM.Billing.Contract, InnerJoin<PMUnbilledDailySummary, On<PMUnbilledDailySummary.projectID, Equal<PX.Objects.PM.Billing.Contract.contractID>>, InnerJoin<ContractBillingSchedule, On<PX.Objects.PM.Billing.Contract.contractID, Equal<ContractBillingSchedule.contractID>>, InnerJoin<PX.Objects.AR.Customer, On<PX.Objects.PM.Billing.Contract.customerID, Equal<PX.Objects.AR.Customer.bAccountID>>, InnerJoin<PMTask, On<PMTask.projectID, Equal<PMUnbilledDailySummary.projectID>, And<PMTask.isActive, Equal<True>, And<PMTask.taskID, Equal<PMUnbilledDailySummary.taskID>, And<Where<PMTask.billingOption, Equal<PMBillingOption.onBilling>, Or2<Where<PMTask.billingOption, Equal<PMBillingOption.onTaskCompletion>, And<PMTask.isCompleted, Equal<True>>>, Or<Where<PMTask.billingOption, Equal<PMBillingOption.onProjectCompetion>, And<PX.Objects.PM.Billing.Contract.isCompleted, Equal<True>>>>>>>>>>, InnerJoin<PMBillingRule, On<PMBillingRule.billingID, Equal<PMTask.billingID>, And<PMBillingRule.accountGroupID, Equal<PMUnbilledDailySummary.accountGroupID>>>>>>>>, Where2<Where<ContractBillingSchedule.nextDate, LessEqual<Current<BillingProcess.BillingFilter.invoiceDate>>, Or<ContractBillingSchedule.type, Equal<BillingType.BillingOnDemand>>>, And2<Where<PMBillingRule.includeNonBillable, Equal<False>, And<PMUnbilledDailySummary.billable, Greater<int0>, Or<Where<PMBillingRule.includeNonBillable, Equal<True>, And<Where<PMUnbilledDailySummary.nonBillable, Greater<int0>, Or<PMUnbilledDailySummary.billable, Greater<int0>>>>>>>>, And2<Where<PMUnbilledDailySummary.date, Less<Current<BillingProcess.BillingFilter.invoiceDate>>>, And2<Match<Current<AccessInfo.userName>>, And<PX.Objects.PM.Billing.Contract.status, NotEqual<ProjectStatus.closed>>>>>>, Aggregate<GroupBy<PX.Objects.PM.Billing.Contract.contractID>>> ProjectsUbilledCutOffDateExcluded;
  public PXSelectJoinGroupBy<PX.Objects.PM.Billing.Contract, InnerJoin<ContractBillingSchedule, On<PX.Objects.PM.Billing.Contract.contractID, Equal<ContractBillingSchedule.contractID>>, InnerJoin<PX.Objects.AR.Customer, On<PX.Objects.PM.Billing.Contract.customerID, Equal<PX.Objects.AR.Customer.bAccountID>>, InnerJoin<PMTask, On<PMTask.projectID, Equal<PX.Objects.PM.Billing.Contract.contractID>>, InnerJoin<PMBillingRule, On<PMBillingRule.billingID, Equal<PMTask.billingID>>, InnerJoin<PMRecurringItem, On<PMTask.projectID, Equal<PMRecurringItem.projectID>, And<PMTask.taskID, Equal<PMRecurringItem.taskID>, And<PMTask.isCompleted, Equal<False>>>>>>>>>, Where2<Where<ContractBillingSchedule.nextDate, LessEqual<Current<BillingProcess.BillingFilter.invoiceDate>>, Or<ContractBillingSchedule.type, Equal<BillingType.BillingOnDemand>>>, And2<Match<Current<AccessInfo.userName>>, And<PX.Objects.PM.Billing.Contract.status, NotEqual<ProjectStatus.closed>>>>, Aggregate<GroupBy<PX.Objects.PM.Billing.Contract.contractID>>> ProjectsRecurring;
  public PXSelectJoinGroupBy<PX.Objects.PM.Billing.Contract, InnerJoin<ContractBillingSchedule, On<PX.Objects.PM.Billing.Contract.contractID, Equal<ContractBillingSchedule.contractID>>, InnerJoin<PX.Objects.AR.Customer, On<PX.Objects.PM.Billing.Contract.customerID, Equal<PX.Objects.AR.Customer.bAccountID>>, InnerJoin<PMTask, On<PMTask.projectID, Equal<PX.Objects.PM.Billing.Contract.contractID>>, InnerJoin<PMBillingRule, On<PMBillingRule.billingID, Equal<PMTask.billingID>>, InnerJoin<PMBudget, On<PMTask.projectID, Equal<PMBudget.projectID>, And<PMTask.taskID, Equal<PMBudget.projectTaskID>, And<PMBudget.type, Equal<AccountType.income>, And<PMBudget.curyAmountToInvoice, NotEqual<decimal0>>>>>>>>>>, Where2<Match<Current<AccessInfo.userName>>, And<PX.Objects.PM.Billing.Contract.status, NotEqual<ProjectStatus.closed>>>, Aggregate<GroupBy<PX.Objects.PM.Billing.Contract.contractID>>> ProjectsProgressive;

  [PXUIField]
  [PXEditDetailButton]
  public virtual IEnumerable ViewDocumentProject(PXAdapter adapter)
  {
    if (((PXSelectBase<PX.Objects.PM.Billing.Contract>) this.Items).Current != null)
      ProjectAccountingService.NavigateToProjectScreen(((PXSelectBase<PX.Objects.PM.Billing.Contract>) this.Items).Current.ContractID);
    return adapter.Get();
  }

  protected virtual IEnumerable items()
  {
    BillingProcess.BillingFilter filter = ((PXSelectBase<BillingProcess.BillingFilter>) this.Filter).Current;
    if (filter != null)
    {
      bool found = false;
      foreach (PX.Objects.PM.Billing.Contract contract in ((PXSelectBase) this.Items).Cache.Inserted)
      {
        found = true;
        yield return (object) contract;
      }
      if (!found)
      {
        PXSelectBase<PX.Objects.PM.Billing.Contract> pxSelectBase = (PXSelectBase<PX.Objects.PM.Billing.Contract>) this.ProjectsUnbilled;
        if (((PXSelectBase<PMSetup>) this.Setup).Current.CutoffDate == "E")
          pxSelectBase = (PXSelectBase<PX.Objects.PM.Billing.Contract>) this.ProjectsUbilledCutOffDateExcluded;
        if (filter.StatementCycleId != null)
        {
          pxSelectBase.WhereAnd<Where<PX.Objects.AR.Customer.statementCycleId, Equal<Current<BillingProcess.BillingFilter.statementCycleId>>>>();
          ((PXSelectBase<PX.Objects.PM.Billing.Contract>) this.ProjectsRecurring).WhereAnd<Where<PX.Objects.AR.Customer.statementCycleId, Equal<Current<BillingProcess.BillingFilter.statementCycleId>>>>();
          ((PXSelectBase<PX.Objects.PM.Billing.Contract>) this.ProjectsProgressive).WhereAnd<Where<PX.Objects.AR.Customer.statementCycleId, Equal<Current<BillingProcess.BillingFilter.statementCycleId>>>>();
        }
        if (filter.CustomerClassID != null)
        {
          pxSelectBase.WhereAnd<Where<PX.Objects.AR.Customer.customerClassID, Equal<Current<BillingProcess.BillingFilter.customerClassID>>>>();
          ((PXSelectBase<PX.Objects.PM.Billing.Contract>) this.ProjectsRecurring).WhereAnd<Where<PX.Objects.AR.Customer.customerClassID, Equal<Current<BillingProcess.BillingFilter.customerClassID>>>>();
          ((PXSelectBase<PX.Objects.PM.Billing.Contract>) this.ProjectsProgressive).WhereAnd<Where<PX.Objects.AR.Customer.customerClassID, Equal<Current<BillingProcess.BillingFilter.customerClassID>>>>();
        }
        if (filter.CustomerID.HasValue)
        {
          pxSelectBase.WhereAnd<Where<PX.Objects.AR.Customer.bAccountID, Equal<Current<BillingProcess.BillingFilter.customerID>>>>();
          ((PXSelectBase<PX.Objects.PM.Billing.Contract>) this.ProjectsRecurring).WhereAnd<Where<PX.Objects.AR.Customer.bAccountID, Equal<Current<BillingProcess.BillingFilter.customerID>>>>();
          ((PXSelectBase<PX.Objects.PM.Billing.Contract>) this.ProjectsProgressive).WhereAnd<Where<PX.Objects.AR.Customer.bAccountID, Equal<Current<BillingProcess.BillingFilter.customerID>>>>();
        }
        if (filter.TemplateID.HasValue)
        {
          pxSelectBase.WhereAnd<Where<PX.Objects.PM.Billing.Contract.templateID, Equal<Current<BillingProcess.BillingFilter.templateID>>>>();
          ((PXSelectBase<PX.Objects.PM.Billing.Contract>) this.ProjectsRecurring).WhereAnd<Where<PX.Objects.PM.Billing.Contract.templateID, Equal<Current<BillingProcess.BillingFilter.templateID>>>>();
          ((PXSelectBase<PX.Objects.PM.Billing.Contract>) this.ProjectsProgressive).WhereAnd<Where<PX.Objects.PM.Billing.Contract.templateID, Equal<Current<BillingProcess.BillingFilter.templateID>>>>();
        }
        if (filter.Status != "Z")
        {
          pxSelectBase.WhereAnd<Where<PX.Objects.PM.Billing.Contract.status, Equal<Current<BillingProcess.BillingFilter.status>>>>();
          ((PXSelectBase<PX.Objects.PM.Billing.Contract>) this.ProjectsRecurring).WhereAnd<Where<PX.Objects.PM.Billing.Contract.status, Equal<Current<BillingProcess.BillingFilter.status>>>>();
          ((PXSelectBase<PX.Objects.PM.Billing.Contract>) this.ProjectsProgressive).WhereAnd<Where<PX.Objects.PM.Billing.Contract.status, Equal<Current<BillingProcess.BillingFilter.status>>>>();
        }
        foreach (PXResult pxResult in pxSelectBase.Select(Array.Empty<object>()))
        {
          PX.Objects.PM.Billing.Contract listItem = this.CreateListItem(pxResult);
          if (((PXSelectBase<PX.Objects.PM.Billing.Contract>) this.Items).Locate(listItem) == null)
            yield return (object) ((PXSelectBase<PX.Objects.PM.Billing.Contract>) this.Items).Insert(listItem);
        }
        foreach (PXResult pxResult in ((PXSelectBase<PX.Objects.PM.Billing.Contract>) this.ProjectsRecurring).Select(Array.Empty<object>()))
        {
          PX.Objects.PM.Billing.Contract listItem = this.CreateListItem(pxResult);
          if (((PXSelectBase<PX.Objects.PM.Billing.Contract>) this.Items).Locate(listItem) == null)
            yield return (object) ((PXSelectBase<PX.Objects.PM.Billing.Contract>) this.Items).Insert(listItem);
        }
        foreach (PXResult pxResult in ((PXSelectBase<PX.Objects.PM.Billing.Contract>) this.ProjectsProgressive).Select(Array.Empty<object>()))
        {
          PX.Objects.PM.Billing.Contract listItem = this.CreateListItem(pxResult);
          if (((PXSelectBase<PX.Objects.PM.Billing.Contract>) this.Items).Locate(listItem) == null)
            yield return (object) ((PXSelectBase<PX.Objects.PM.Billing.Contract>) this.Items).Insert(listItem);
        }
        ((PXSelectBase) this.Items).Cache.IsDirty = false;
      }
    }
  }

  protected virtual PX.Objects.PM.Billing.Contract CreateListItem(PXResult item)
  {
    PX.Objects.PM.Billing.Contract listItem = PXResult.Unwrap<PX.Objects.PM.Billing.Contract>((object) item);
    ContractBillingSchedule contractBillingSchedule = PXResult.Unwrap<ContractBillingSchedule>((object) item);
    PXResult.Unwrap<PX.Objects.AR.Customer>((object) item);
    listItem.LastDate = contractBillingSchedule.LastDate;
    DateTime? nullable = new DateTime?();
    if (contractBillingSchedule.NextDate.HasValue)
    {
      switch (contractBillingSchedule.Type)
      {
        case "A":
          nullable = new DateTime?(contractBillingSchedule.NextDate.Value.AddYears(-1));
          break;
        case "M":
          nullable = new DateTime?(contractBillingSchedule.NextDate.Value.AddMonths(-1));
          break;
        case "W":
          nullable = new DateTime?(contractBillingSchedule.NextDate.Value.AddDays(-7.0));
          break;
        case "Q":
          nullable = new DateTime?(contractBillingSchedule.NextDate.Value.AddMonths(-3));
          break;
      }
    }
    listItem.BillingResult = listItem.CreateProforma.GetValueOrDefault() ? "Pro Forma Invoice" : "AR Invoice";
    listItem.FromDate = nullable;
    listItem.NextDate = contractBillingSchedule.NextDate;
    return listItem;
  }

  public BillingProcess()
  {
    ARSetupNoMigrationMode.EnsureMigrationModeDisabled((PXGraph) this);
    ((PXProcessing<PX.Objects.PM.Billing.Contract>) this.Items).SetProcessCaption("Process");
    ((PXProcessing<PX.Objects.PM.Billing.Contract>) this.Items).SetProcessAllCaption("Process All");
    OpenPeriodAttribute.SetValidatePeriod<BillingProcess.BillingFilter.invFinPeriodID>(((PXSelectBase) this.Filter).Cache, (object) null, ((PXGraph) this).IsContractBasedAPI || ((PXGraph) this).IsImport || ((PXGraph) this).IsExport || ((PXGraph) this).UnattendedMode ? PeriodValidation.DefaultUpdate : PeriodValidation.DefaultSelectUpdate);
  }

  protected virtual void BillingFilter_RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
  {
    if (cache.ObjectsEqual<BillingProcess.BillingFilter.invoiceDate, BillingProcess.BillingFilter.invFinPeriodID, BillingProcess.BillingFilter.statementCycleId, BillingProcess.BillingFilter.customerClassID, BillingProcess.BillingFilter.customerID, BillingProcess.BillingFilter.templateID, BillingProcess.BillingFilter.status>(e.Row, e.OldRow))
      return;
    ((PXSelectBase) this.Items).Cache.Clear();
  }

  protected virtual void BillingFilter_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: method pointer
    ((PXProcessingBase<PX.Objects.PM.Billing.Contract>) this.Items).SetProcessDelegate<PMBillEngine>(new PXProcessingBase<PX.Objects.PM.Billing.Contract>.ProcessItemDelegate<PMBillEngine>((object) new BillingProcess.\u003C\u003Ec__DisplayClass14_0()
    {
      filter = ((PXSelectBase<BillingProcess.BillingFilter>) this.Filter).Current
    }, __methodptr(\u003CBillingFilter_RowSelected\u003Eb__0)));
  }

  [PXHidden]
  [ExcludeFromCodeCoverage]
  [Serializable]
  public class BillingFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected DateTime? _InvoiceDate;
    protected string _InvFinPeriodID;
    protected string _StatementCycleId;
    protected string _CustomerClassID;
    protected int? _CustomerID;
    protected int? _TemplateID;

    [PXDBDate]
    [PXDefault(typeof (AccessInfo.businessDate))]
    [PXUIField]
    public virtual DateTime? InvoiceDate
    {
      get => this._InvoiceDate;
      set => this._InvoiceDate = value;
    }

    [OpenPeriod(typeof (BillingProcess.BillingFilter.invoiceDate), ValidatePeriod = PeriodValidation.DefaultSelectUpdate)]
    [PXUIField]
    public virtual string InvFinPeriodID
    {
      get => this._InvFinPeriodID;
      set => this._InvFinPeriodID = value;
    }

    [PXDBString(10, IsUnicode = true)]
    [PXUIField(DisplayName = "Statement Cycle")]
    [PXSelector(typeof (ARStatementCycle.statementCycleId), DescriptionField = typeof (ARStatementCycle.descr))]
    public virtual string StatementCycleId
    {
      get => this._StatementCycleId;
      set => this._StatementCycleId = value;
    }

    [PXDBString(10, IsUnicode = true)]
    [PXSelector(typeof (CustomerClass.customerClassID), DescriptionField = typeof (CustomerClass.descr), CacheGlobal = true)]
    [PXUIField(DisplayName = "Customer Class")]
    public virtual string CustomerClassID
    {
      get => this._CustomerClassID;
      set => this._CustomerClassID = value;
    }

    [PXUIField(DisplayName = "Customer")]
    [Customer(DescriptionField = typeof (PX.Objects.AR.Customer.acctName))]
    public virtual int? CustomerID
    {
      get => this._CustomerID;
      set => this._CustomerID = value;
    }

    [Project(typeof (Where<PMProject.baseType, Equal<CTPRType.projectTemplate>>), DisplayName = "Project Template")]
    public virtual int? TemplateID
    {
      get => this._TemplateID;
      set => this._TemplateID = value;
    }

    /// <summary>The <see cref="T:PX.Objects.PM.ProjectStatus">status</see> of the project.</summary>
    [PXDBString(1, IsFixed = true)]
    [PXDefault("Z")]
    [ProjectStatus.BillableProjectStatusList]
    [PXUIField(DisplayName = "Status")]
    public virtual string Status { get; set; }

    public abstract class invoiceDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      BillingProcess.BillingFilter.invoiceDate>
    {
    }

    public abstract class invFinPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      BillingProcess.BillingFilter.invFinPeriodID>
    {
    }

    public abstract class statementCycleId : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      BillingProcess.BillingFilter.statementCycleId>
    {
    }

    public abstract class customerClassID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      BillingProcess.BillingFilter.customerClassID>
    {
    }

    public abstract class customerID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      BillingProcess.BillingFilter.customerID>
    {
    }

    public abstract class templateID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      BillingProcess.BillingFilter.templateID>
    {
    }

    public abstract class status : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      BillingProcess.BillingFilter.status>
    {
    }
  }
}
