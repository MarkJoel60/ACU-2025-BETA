// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMBillEngine
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using CommonServiceLocator;
using PX.CCProcessingBase;
using PX.Common;
using PX.Common.Parser;
using PX.CS;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CM;
using PX.Objects.CM.Extensions;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.CT;
using PX.Objects.Extensions.MultiCurrency;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods;
using PX.Objects.TX;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;

#nullable disable
namespace PX.Objects.PM;

public class PMBillEngine : PXGraph<PMBillEngine>, IRateTable
{
  public PXSelect<PMProject> Project;
  public PXSelect<PMRecurringItem> RecurringItems;
  public PXSelect<ContractBillingSchedule> BillingSchedule;
  public PXSelect<PMTran> Transactions;
  public PXSelect<PMBillingRecord, Where<PMBillingRecord.projectID, Equal<Current<PMProject.contractID>>>> BillingRecord;
  protected Dictionary<int, Dictionary<int, List<PXResult<PMTran>>>> transactions;
  protected Dictionary<int, List<PMRecurringItem>> recurrentItemsByTask;
  internal Dictionary<string, List<PMBillingRule>> billingRules;
  protected Dictionary<string, List<PMRateDefinition>> rateDefinitions;
  protected Dictionary<int, List<PXResult<PMRevenueBudget, PMAccountGroup>>> revenueBudgetLines;
  protected Dictionary<string, Decimal?> ratios;
  protected PMBillEngine.InvoicePersistingHandler invoicePersistingHandler;
  protected RateEngineV2 rateEngine;
  public PXSetup<ARSetup> arSetup;
  public PXSetup<PMSetup> Setup;
  public const string emptyInvoiceDescriptionKey = "EMPTY_INV_GROUP";
  protected ProformaEntry proformaEntry;
  protected ARInvoiceEntry invoiceEntry;
  protected RegisterEntry pmRegisterEntry;
  protected DateTime billingDate;

  [InjectDependency]
  public IProjectMultiCurrency MultiCurrencyService { get; set; }

  public virtual bool IncludeTodaysTransactions
  {
    get
    {
      bool todaysTransactions = true;
      PMSetup pmSetup = PXResultset<PMSetup>.op_Implicit(PXSelectBase<PMSetup, PXSelect<PMSetup>.Config>.Select((PXGraph) this, Array.Empty<object>()));
      if (pmSetup != null && pmSetup.CutoffDate == "E")
        todaysTransactions = false;
      return todaysTransactions;
    }
  }

  public PMBillEngine()
  {
    PXDBDefaultAttribute.SetDefaultForUpdate<ContractBillingSchedule.contractID>(((PXSelectBase) this.BillingSchedule).Cache, (object) null, false);
    PXDBDefaultAttribute.SetDefaultForUpdate<PMRecurringItem.projectID>(((PXSelectBase) this.RecurringItems).Cache, (object) null, false);
    this.transactions = new Dictionary<int, Dictionary<int, List<PXResult<PMTran>>>>();
    this.recurrentItemsByTask = new Dictionary<int, List<PMRecurringItem>>();
    this.billingRules = new Dictionary<string, List<PMBillingRule>>();
    this.rateDefinitions = new Dictionary<string, List<PMRateDefinition>>();
    this.revenueBudgetLines = new Dictionary<int, List<PXResult<PMRevenueBudget, PMAccountGroup>>>();
    this.invoicePersistingHandler = new PMBillEngine.InvoicePersistingHandler();
  }

  public virtual ProformaEntry ProformaEntry
  {
    get
    {
      if (this.proformaEntry == null)
      {
        this.proformaEntry = PXGraph.CreateInstance<ProformaEntry>();
        this.proformaEntry.RecalculateExternalTaxesSync = true;
        this.proformaEntry.IsBilling = true;
        PXGraph.RowPersistedEvents rowPersisted = ((PXGraph) this.proformaEntry).RowPersisted;
        PMBillEngine.InvoicePersistingHandler persistingHandler = this.invoicePersistingHandler;
        PXRowPersisted pxRowPersisted = new PXRowPersisted((object) persistingHandler, __vmethodptr(persistingHandler, OnProformaPersisted));
        rowPersisted.AddHandler<PMProforma>(pxRowPersisted);
        ((PXGraph) this.proformaEntry).FieldVerifying.AddHandler<PMProforma.locationID>(new PXFieldVerifying((object) this, __methodptr(VerifyCustomerLocation)));
      }
      return this.proformaEntry;
    }
  }

  public virtual ARInvoiceEntry InvoiceEntry
  {
    get
    {
      if (this.invoiceEntry == null)
      {
        this.invoiceEntry = PXGraph.CreateInstance<ARInvoiceEntry>();
        ((PXSelectBase<ARSetup>) this.invoiceEntry.ARSetup).Current.RequireControlTotal = new bool?(false);
        ((PXGraph) this.invoiceEntry).FieldVerifying.AddHandler<PX.Objects.AR.ARTran.taskID>(PMBillEngine.\u003C\u003Ec.\u003C\u003E9__29_0 ?? (PMBillEngine.\u003C\u003Ec.\u003C\u003E9__29_0 = new PXFieldVerifying((object) PMBillEngine.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003Cget_InvoiceEntry\u003Eb__29_0))));
        PXGraph.RowPersistedEvents rowPersisted = ((PXGraph) this.invoiceEntry).RowPersisted;
        PMBillEngine.InvoicePersistingHandler persistingHandler = this.invoicePersistingHandler;
        PXRowPersisted pxRowPersisted = new PXRowPersisted((object) persistingHandler, __vmethodptr(persistingHandler, OnInvoicePersisted));
        rowPersisted.AddHandler<PX.Objects.AR.ARInvoice>(pxRowPersisted);
      }
      return this.invoiceEntry;
    }
  }

  public virtual RegisterEntry PMEntry
  {
    get
    {
      if (this.pmRegisterEntry == null)
      {
        this.pmRegisterEntry = PXGraph.CreateInstance<RegisterEntry>();
        ((PXGraph) this.pmRegisterEntry).FieldVerifying.AddHandler<PMTran.projectID>(PMBillEngine.\u003C\u003Ec.\u003C\u003E9__32_0 ?? (PMBillEngine.\u003C\u003Ec.\u003C\u003E9__32_0 = new PXFieldVerifying((object) PMBillEngine.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003Cget_PMEntry\u003Eb__32_0))));
        ((PXGraph) this.pmRegisterEntry).FieldVerifying.AddHandler<PMTran.taskID>(PMBillEngine.\u003C\u003Ec.\u003C\u003E9__32_1 ?? (PMBillEngine.\u003C\u003Ec.\u003C\u003E9__32_1 = new PXFieldVerifying((object) PMBillEngine.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003Cget_PMEntry\u003Eb__32_1))));
        ((PXGraph) this.pmRegisterEntry).FieldVerifying.AddHandler<PMTran.inventoryID>(PMBillEngine.\u003C\u003Ec.\u003C\u003E9__32_2 ?? (PMBillEngine.\u003C\u003Ec.\u003C\u003E9__32_2 = new PXFieldVerifying((object) PMBillEngine.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003Cget_PMEntry\u003Eb__32_2))));
      }
      return this.pmRegisterEntry;
    }
  }

  [InjectDependency]
  public IFinPeriodRepository FinPeriodRepository { get; set; }

  /// <summary>Run billing procedure for specified project.</summary>
  /// <param name="projectID">Project ID billing procedure is being run for.</param>
  /// <param name="invoiceDate">Date invoices should be created on. Procedure finds out the value based on the project data if null.</param>
  /// <param name="finPeriod">
  /// Financial period invoices should be create in. Procedure finds out the value based on the project data if null.
  /// If not null, the value may be treated differently depending on MultipleCalendarsSupport feature.
  /// If the feature is turned on, the value is treated as a master calendar financial period.
  /// Otherwise, the value is treated as the organization related period.
  /// </param>
  /// <returns>Billing procedure result</returns>
  public virtual PMBillEngine.BillingResult Bill(
    int? projectID,
    DateTime? invoiceDate,
    string finPeriod)
  {
    PMProject project = this.SelectProjectByID(projectID);
    ContractBillingSchedule schedule = PXResultset<ContractBillingSchedule>.op_Implicit(PXSelectBase<ContractBillingSchedule, PXSelect<ContractBillingSchedule>.Config>.Search<ContractBillingSchedule.contractID>((PXGraph) this, (object) project.ContractID, Array.Empty<object>()));
    PX.Objects.AR.Customer customer = (PX.Objects.AR.Customer) null;
    if (project.CustomerID.HasValue)
      customer = PXResultset<PX.Objects.AR.Customer>.op_Implicit(PXSelectBase<PX.Objects.AR.Customer, PXSelect<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Required<PX.Objects.AR.Customer.bAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) project.CustomerID
      }));
    if (customer == null)
      throw new PXException("This Project has no Customer associated with it and thus cannot be billed.");
    Tuple<DateTime, DateTime> billingAndCutOffDates = this.GetBillingAndCutOffDates(schedule, invoiceDate);
    this.billingDate = billingAndCutOffDates.Item1;
    DateTime dateTime = billingAndCutOffDates.Item2;
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = this.GetCurrencyInfo(project, new DateTime?(this.billingDate));
    if (currencyInfo != null)
      project.CuryRate = currencyInfo.CuryRate;
    List<PMTask> tasks = this.SelectBillableTasks(project);
    HashSet<string> source1 = new HashSet<string>();
    foreach (PMTask pmTask in tasks)
    {
      if (!string.IsNullOrEmpty(pmTask.RateTableID))
        source1.Add(pmTask.RateTableID);
    }
    this.PreSelectRecurrentItems(projectID);
    this.PreSelectTasksTransactions(projectID, tasks, new DateTime?(dateTime));
    this.PreSelectRevenueBudgetLines(projectID);
    HashSet<string> source2 = new HashSet<string>();
    foreach (List<PMBillingRule> pmBillingRuleList in this.billingRules.Values)
    {
      foreach (PMBillingRule pmBillingRule in pmBillingRuleList)
      {
        if (!string.IsNullOrEmpty(pmBillingRule.RateTypeID))
          source2.Add(pmBillingRule.RateTypeID);
      }
    }
    this.rateEngine = this.CreateRateEngineV2((IList<string>) source1.ToList<string>(), (IList<string>) source2.ToList<string>());
    Dictionary<string, Tuple<int?, List<PMBillEngine.BillingData>>> dictionary1 = new Dictionary<string, Tuple<int?, List<PMBillEngine.BillingData>>>();
    PMRegister wipReversalDoc = (PMRegister) null;
    List<PMTran> pmTranList = new List<PMTran>();
    foreach (PMTask task in tasks)
    {
      string proformaTag = this.GenerateProformaTag(project, task);
      Tuple<int?, List<PMBillEngine.BillingData>> tuple;
      if (!dictionary1.TryGetValue(proformaTag, out tuple))
      {
        tuple = new Tuple<int?, List<PMBillEngine.BillingData>>(task.LocationID, new List<PMBillEngine.BillingData>());
        dictionary1.Add(proformaTag, tuple);
      }
      tuple.Item2.AddRange((IEnumerable<PMBillEngine.BillingData>) this.BillTask(project, customer, task, this.billingDate));
      pmTranList.AddRange((IEnumerable<PMTran>) this.ReverseWipTask(task, this.billingDate));
    }
    Dictionary<string, Tuple<int?, List<PMBillEngine.BillingData>>> dictionary2 = new Dictionary<string, Tuple<int?, List<PMBillEngine.BillingData>>>();
    Dictionary<string, string> dictionary3 = new Dictionary<string, string>();
    Decimal totalInvoicedTransactionMode = 0M;
    foreach (KeyValuePair<string, Tuple<int?, List<PMBillEngine.BillingData>>> keyValuePair in dictionary1)
    {
      foreach (PMBillEngine.BillingData billingData in keyValuePair.Value.Item2)
      {
        if (billingData.Tran.Type == "T")
          totalInvoicedTransactionMode += billingData.Tran.CuryAmount.GetValueOrDefault();
        string invoiceKey = this.GetInvoiceKey(keyValuePair.Key, billingData.Rule);
        if (CostCodeAttribute.UseCostCode() && !billingData.Tran.CostCodeID.HasValue)
          billingData.Tran.CostCodeID = CostCodeAttribute.DefaultCostCode;
        if (dictionary2.ContainsKey(invoiceKey))
        {
          dictionary2[invoiceKey].Item2.Add(billingData);
        }
        else
        {
          dictionary2.Add(invoiceKey, new Tuple<int?, List<PMBillEngine.BillingData>>(keyValuePair.Value.Item1, new List<PMBillEngine.BillingData>((IEnumerable<PMBillEngine.BillingData>) new PMBillEngine.BillingData[1]
          {
            billingData
          })));
          if (!string.IsNullOrEmpty(billingData.Rule.InvoiceFormula))
          {
            try
            {
              PMTran pmTran = new PMTran();
              pmTran.ProjectID = billingData.Tran.ProjectID;
              pmTran.TaskID = billingData.Tran.TaskID;
              pmTran.Description = billingData.Rule.Description;
              pmTran.AccountGroupID = billingData.Tran.AccountGroupID ?? billingData.Rule.AccountGroupID;
              pmTran.InventoryID = billingData.Tran.InventoryID;
              pmTran.BAccountID = project.CustomerID;
              pmTran.CostCodeID = billingData.Tran.CostCodeID;
              pmTran.ResourceID = billingData.Tran.ResourceID;
              pmTran.Date = new DateTime?(this.billingDate);
              PMAllocator.PMDataNavigator pmDataNavigator = new PMAllocator.PMDataNavigator((IRateTable) this, new List<PMTran>((IEnumerable<PMTran>) new PMTran[1]
              {
                pmTran
              }));
              ExpressionNode expressionNode = PMExpressionParser.Parse((IRateTable) this, billingData.Rule.InvoiceFormula);
              expressionNode.Bind((object) pmDataNavigator);
              using (new PXLocaleScope(customer.LocaleName))
              {
                object obj = expressionNode.Eval((object) pmTran);
                if (obj != null)
                  dictionary3.Add(invoiceKey, obj.ToString());
              }
            }
            catch (Exception ex)
            {
              throw new PXException("Failed to calculate the invoice description using the {3} step of the {0} billing rule. Invoice Description Formula: {1} Error: {2}", new object[4]
              {
                (object) billingData.Rule.BillingID,
                (object) billingData.Rule.DescriptionFormula,
                (object) ex.Message,
                (object) billingData.Rule.StepID
              });
            }
          }
        }
      }
    }
    schedule.NextDate = PMBillEngine.GetNextBillingDate((PXGraph) this, schedule, schedule.NextDate);
    schedule.LastDate = ((PXGraph) this).Accessinfo.BusinessDate;
    ((PXSelectBase<ContractBillingSchedule>) this.BillingSchedule).Update(schedule);
    foreach (PMTask pmTask in tasks)
    {
      List<PMRecurringItem> pmRecurringItemList;
      if (this.recurrentItemsByTask.TryGetValue(pmTask.TaskID.Value, out pmRecurringItemList))
      {
        foreach (PMRecurringItem pmRecurringItem in pmRecurringItemList)
        {
          if (string.Equals(pmRecurringItem.ResetUsage, "B", StringComparison.InvariantCultureIgnoreCase))
          {
            pmRecurringItem.Used = new Decimal?(0M);
            ((PXSelectBase<PMRecurringItem>) this.RecurringItems).Update(pmRecurringItem);
          }
        }
      }
    }
    PMBillEngine.BillingResult result = new PMBillEngine.BillingResult(this.billingDate);
    if (dictionary2.Count > 0)
    {
      project.BillingLineCntr = new int?(project.BillingLineCntr.GetValueOrDefault() + 1);
      ((PXSelectBase<PMProject>) this.Project).Update(project);
      bool? nullable = project.SteppedRetainage;
      if (nullable.GetValueOrDefault())
        this.UpdateRetainagePercent(project, totalInvoicedTransactionMode);
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        bool flag = false;
        foreach (KeyValuePair<string, Tuple<int?, List<PMBillEngine.BillingData>>> keyValuePair in dictionary2)
        {
          nullable = project.CreateProforma;
          if (nullable.GetValueOrDefault())
            ((PXGraph) this.ProformaEntry).Clear();
          else
            ((PXGraph) this.InvoiceEntry).Clear();
          string docDesc = (string) null;
          dictionary3.TryGetValue(keyValuePair.Key, out docDesc);
          PX.Objects.AR.ARInvoice arInvoice = (PX.Objects.AR.ARInvoice) null;
          nullable = project.CreateProforma;
          if (nullable.GetValueOrDefault())
          {
            this.InsertNewProformaDocument(finPeriod, customer, project, this.billingDate, docDesc, keyValuePair.Value.Item1);
            flag = true;
            result.Proformas.Add(((PXSelectBase<PMProforma>) this.ProformaEntry.Document).Current);
          }
          else
          {
            Tuple<string, bool> typeAndRetainage = this.GetDocTypeAndRetainage(project, keyValuePair.Value.Item2);
            arInvoice = this.InsertNewInvoiceDocument(finPeriod, typeAndRetainage.Item1, typeAndRetainage.Item2, customer, project, this.billingDate, docDesc, keyValuePair.Value.Item1);
          }
          List<PMBillEngine.BillingData> unbilled = new List<PMBillEngine.BillingData>((IEnumerable<PMBillEngine.BillingData>) keyValuePair.Value.Item2);
          this.ValidateLineAccount(unbilled);
          nullable = project.CreateProforma;
          if (nullable.GetValueOrDefault())
          {
            this.InsertTransactionsInProforma(project, unbilled);
            if (project.RetainageMode == "C")
              ((PXSelectBase<PMProject>) this.ProformaEntry.Project).Current.RetainagePct = project.RetainagePct;
            this.ProformaEntry.RecalculateRetainage();
            if (PXAccess.FeatureInstalled<FeaturesSet.construction>() && NonGenericIEnumerableExtensions.Any_(((PXSelectBase) this.ProformaEntry.ProgressiveLines).Cache.Inserted))
            {
              string str = PMBillEngine.IncLastProformaNumber(project.LastProformaNumber);
              if (str.Length > 15)
                throw new PXSetPropertyException<PMProforma.projectNbr>("The pro forma invoice cannot be created because the automatically generated Application Nbr. exceeds the length limit ({0} symbols). Correct the Last Application Nbr. of the {1} project, and run project billing again.", new object[2]
                {
                  (object) 15,
                  (object) project.ContractCD
                });
              try
              {
                ((PXSelectBase<PMProforma>) this.ProformaEntry.Document).Current.ProjectNbr = str;
                ((PXSelectBase<PMProforma>) this.ProformaEntry.Document).UpdateCurrent();
              }
              catch (PXSetPropertyException ex)
              {
                object[] objArray = Array.Empty<object>();
                throw new PXException((Exception) ex, "The pro forma invoice has not been generated as the application number is not valid. Change the Last Application Nbr. of the project.", objArray);
              }
              project.LastProformaNumber = str;
              ((PXSelectBase<PMProject>) this.Project).Update(project);
            }
          }
          else
          {
            int mult = 1;
            if (arInvoice.DocType == "CRM")
              mult = -1;
            this.InsertTransactionsInInvoice(project, unbilled, mult);
            PX.Objects.AR.ARInvoice copy1 = (PX.Objects.AR.ARInvoice) ((PXSelectBase) this.InvoiceEntry.Document).Cache.CreateCopy((object) arInvoice);
            arInvoice.CuryOrigDocAmt = arInvoice.CuryDocBal;
            ((PXSelectBase) this.InvoiceEntry.Document).Cache.RaiseRowUpdated((object) arInvoice, (object) copy1);
            ((PXSelectBase) this.InvoiceEntry.Document).Cache.SetValue<PX.Objects.AR.ARInvoice.curyOrigDocAmt>((object) arInvoice, (object) arInvoice.CuryDocBal);
            nullable = project.AutomaticReleaseAR;
            if (nullable.GetValueOrDefault())
            {
              PX.Objects.AR.ARInvoice copy2 = (PX.Objects.AR.ARInvoice) ((PXSelectBase) this.InvoiceEntry.Document).Cache.CreateCopy((object) ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.InvoiceEntry.Document).Current);
              copy2.Hold = new bool?(false);
              ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.InvoiceEntry.Document).Update(copy2);
            }
            result.Invoices.Add((PX.Objects.AR.ARRegister) ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.InvoiceEntry.Document).Current);
          }
          List<PMBillEngine.BillingData> billedData = keyValuePair.Value.Item2;
          PMRegister allocationReversal = (PMRegister) null;
          List<PMTran> source3 = new List<PMTran>();
          foreach (PMBillEngine.BillingData billingData in billedData)
          {
            for (int index = 0; index < billingData.Transactions.Count; ++index)
            {
              PMTran transaction = billingData.Transactions[index];
              transaction.Billed = new bool?(true);
              transaction.BilledDate = new DateTime?(this.billingDate);
              billingData.Transactions[index] = ((PXSelectBase<PMTran>) this.Transactions).Update(transaction);
              RegisterReleaseProcess.SubtractFromUnbilledSummary((PXGraph) this, transaction);
              nullable = project.CreateProforma;
              if (!nullable.GetValueOrDefault() && transaction.Reverse == "B")
              {
                foreach (PMTran pmTran in (IEnumerable<PMTran>) this.ReverseTran(transaction))
                {
                  pmTran.Date = new DateTime?(this.billingDate);
                  pmTran.FinPeriodID = (string) null;
                  source3.Add(pmTran);
                }
              }
            }
          }
          if (!flag)
          {
            nullable = project.CreateProforma;
            if (nullable.GetValueOrDefault())
              goto label_106;
          }
          PMBillingRecord instance = (PMBillingRecord) ((PXSelectBase) this.BillingRecord).Cache.CreateInstance();
          instance.ProjectID = project.ContractID;
          instance.RecordID = project.BillingLineCntr;
          instance.BillingTag = keyValuePair.Key;
          instance.Date = new DateTime?(this.billingDate);
          ((PXSelectBase<PMBillingRecord>) this.BillingRecord).Insert(instance);
label_106:
          if (source3.Any<PMTran>())
          {
            ((PXGraph) this.PMEntry).Clear();
            allocationReversal = (PMRegister) ((PXSelectBase) this.PMEntry.Document).Cache.Insert();
            allocationReversal.OrigDocType = "AR";
            allocationReversal.Description = PXMessages.LocalizeNoPrefix("Allocation Reversal on AR Invoice Generation");
            ((PXSelectBase<PMRegister>) this.PMEntry.Document).Current = allocationReversal;
            foreach (PMTran pmTran in source3)
              ((PXSelectBase<PMTran>) this.PMEntry.Transactions).Insert(pmTran);
          }
          this.invoicePersistingHandler.SetData(billedData, ((PXSelectBase<PMBillingRecord>) this.BillingRecord).Current, allocationReversal);
          nullable = project.CreateProforma;
          if (nullable.GetValueOrDefault())
          {
            PMProject copy = ((PXSelectBase) this.Project).Cache.CreateCopy((object) project) as PMProject;
            ((PXAction) this.ProformaEntry.autoApplyPrepayments).Press();
            ((PXAction) this.ProformaEntry.Save).Press();
            project = ((PXSelectBase<PMProject>) this.ProformaEntry.Project).Current;
            project.BillingLineCntr = copy.BillingLineCntr;
            project.LastProformaNumber = copy.LastProformaNumber;
            project.RetainagePct = copy.RetainagePct;
            ((PXSelectBase<PMProject>) this.Project).Update(project);
          }
          else
            ((PXAction) this.InvoiceEntry.Save).Press();
          if (source3.Any<PMTran>())
            ((PXAction) this.PMEntry.Save).Press();
        }
        ((PXGraph) this).Actions.PressSave();
        if (pmTranList.Count > 0)
        {
          ((PXGraph) this.PMEntry).Clear();
          wipReversalDoc = (PMRegister) ((PXSelectBase) this.PMEntry.Document).Cache.Insert();
          wipReversalDoc.OrigDocType = "WR";
          wipReversalDoc.Description = PXMessages.LocalizeNoPrefix("WIP Reversal");
          ((PXSelectBase<PMRegister>) this.PMEntry.Document).Current = wipReversalDoc;
          foreach (PMTran pmTran in pmTranList)
            ((PXSelectBase<PMTran>) this.PMEntry.Transactions).Insert(pmTran);
          ((PXAction) this.PMEntry.Save).Press();
        }
        transactionScope.Complete();
      }
    }
    else
    {
      ((PXGraph) this).Persist(typeof (ContractBillingSchedule), (PXDBOperation) 1);
      ((PXGraph) this).Persist(typeof (PX.Objects.CT.Contract), (PXDBOperation) 1);
    }
    this.AutoReleaseCreatedDocuments(project, result, wipReversalDoc);
    return result;
  }

  public static string IncLastProformaNumber(string lastProformaNumber)
  {
    string number = string.IsNullOrEmpty(lastProformaNumber) ? "0000" : lastProformaNumber;
    if (!char.IsDigit(number[number.Length - 1]))
      number = $"{number}0000";
    return NumberHelper.IncreaseNumber(number, 1);
  }

  private void ValidateLineAccount(List<PMBillEngine.BillingData> unbilled)
  {
    foreach (PMBillEngine.BillingData billingData in unbilled)
    {
      PX.Objects.GL.Account lineAccount = this.GetLineAccount(billingData.Tran.AccountID);
      PMBillingRule rule = billingData.Rule;
      if (!lineAccount.AccountGroupID.HasValue)
      {
        IDictionary<string, string> accountSources = BillingMaint.GetAccountSources(rule.Type);
        throw new PXOperationCompletedWithErrorException("The {0} billing step of the {1} billing rule failed. The {2} sales account, which is taken from the {3}, is not included in any account group.", new object[4]
        {
          (object) rule.StepID,
          (object) rule.BillingID,
          (object) lineAccount.AccountCD,
          (object) accountSources[rule.AccountSource]
        });
      }
    }
  }

  protected virtual void UpdateRetainagePercent(
    PMProject project,
    Decimal totalInvoicedTransactionMode)
  {
    PMProjectRevenueTotal budget = PXResultset<PMProjectRevenueTotal>.op_Implicit(PXSelectBase<PMProjectRevenueTotal, PXSelect<PMProjectRevenueTotal, Where<PMProjectRevenueTotal.projectID, Equal<Required<PMProjectRevenueTotal.projectID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) project.ContractID
    }));
    Decimal contractAmount = this.GetContractAmount(project, budget);
    Decimal? nullable = budget.CuryActualAmount;
    Decimal valueOrDefault1 = nullable.GetValueOrDefault();
    nullable = budget.CuryInvoicedAmount;
    Decimal valueOrDefault2 = nullable.GetValueOrDefault();
    Decimal num1 = valueOrDefault1 + valueOrDefault2;
    Decimal num2 = totalInvoicedTransactionMode + num1 + budget.CuryAmountToInvoice.GetValueOrDefault();
    List<PMBillEngine.StepThreshold> retainegeSteps = this.GetRetainegeSteps(project.ContractID.Value, contractAmount);
    int index1 = -1;
    for (int index2 = 0; index2 < retainegeSteps.Count; ++index2)
    {
      if (num1 >= retainegeSteps[index2].Min && num1 < retainegeSteps[index2].Max)
      {
        index1 = index2;
        break;
      }
    }
    Decimal num3 = 0M;
    if (index1 != -1)
    {
      Decimal num4 = num1;
      Decimal num5 = num2 - num1;
      while (num5 > 0M)
      {
        Decimal num6 = retainegeSteps[index1].Max - num4;
        if (num6 >= num5)
        {
          num3 += num5 * retainegeSteps[index1].RetainagePct * 0.01M;
          num5 = 0M;
        }
        else
        {
          num3 += num6 * retainegeSteps[index1].RetainagePct * 0.01M;
          num5 -= num6;
          ++index1;
          num4 += num6;
        }
      }
    }
    if (!(0M != num2 - num1))
      return;
    project.RetainagePct = new Decimal?(100M * num3 / (num2 - num1));
    ((PXSelectBase<PMProject>) this.Project).Update(project);
  }

  private List<PMBillEngine.StepThreshold> GetRetainegeSteps(int projectID, Decimal contractAmount)
  {
    List<PMRetainageStep> list = GraphHelper.RowCast<PMRetainageStep>((IEnumerable) PXSelectBase<PMRetainageStep, PXSelect<PMRetainageStep, Where<PMRetainageStep.projectID, Equal<Required<PMRetainageStep.projectID>>>, OrderBy<Asc<PMRetainageStep.thresholdPct>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) projectID
    })).ToList<PMRetainageStep>();
    List<PMBillEngine.StepThreshold> retainegeSteps = new List<PMBillEngine.StepThreshold>();
    PMBillEngine.StepThreshold stepThreshold1 = (PMBillEngine.StepThreshold) null;
    foreach (PMRetainageStep step in list)
    {
      PMBillEngine.StepThreshold stepThreshold2 = new PMBillEngine.StepThreshold(step, contractAmount);
      if (stepThreshold1 != null)
        stepThreshold1.Max = stepThreshold2.ThresholdPct * 0.01M * contractAmount;
      retainegeSteps.Add(stepThreshold2);
      stepThreshold1 = stepThreshold2;
    }
    return retainegeSteps;
  }

  protected virtual Decimal GetContractAmount(PMProject project, PMProjectRevenueTotal budget)
  {
    return project.IncludeCO.GetValueOrDefault() ? budget.CuryRevisedAmount.GetValueOrDefault() : budget.CuryAmount.GetValueOrDefault();
  }

  public virtual List<PMTask> SelectBillableTasks(PMProject project)
  {
    List<PMTask> pmTaskList = new List<PMTask>();
    foreach (PXResult<PMTask> pxResult in ((PXSelectBase<PMTask>) new PXSelect<PMTask, Where<PMTask.projectID, Equal<Required<PMTask.projectID>>, And<PMTask.billingID, IsNotNull, And<PMTask.isActive, Equal<True>>>>, OrderBy<Asc<PMTask.taskCD>>>((PXGraph) this)).Select(new object[1]
    {
      (object) project.ContractID
    }))
    {
      PMTask pmTask = PXResult<PMTask>.op_Implicit(pxResult);
      bool? isCompleted;
      if (pmTask.BillingOption == "T")
      {
        isCompleted = pmTask.IsCompleted;
        if (isCompleted.GetValueOrDefault())
          goto label_7;
      }
      if (pmTask.BillingOption == "P")
      {
        isCompleted = project.IsCompleted;
        if (isCompleted.GetValueOrDefault())
          goto label_7;
      }
      if (!(pmTask.BillingOption == "B"))
        continue;
label_7:
      pmTaskList.Add(pmTask);
    }
    return pmTaskList;
  }

  private PX.Objects.GL.Account GetLineAccount(int? accountID)
  {
    return PMBillEngine.GetLineAccount((PXGraph) this, accountID);
  }

  private static PX.Objects.GL.Account GetLineAccount(PXGraph graph, int? accountID)
  {
    return PXResultset<PX.Objects.GL.Account>.op_Implicit(PXSelectBase<PX.Objects.GL.Account, PXViewOf<PX.Objects.GL.Account>.BasedOn<SelectFromBase<PX.Objects.GL.Account, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.GL.Account.accountID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select(graph, new object[1]
    {
      (object) accountID
    }));
  }

  public virtual string GetInvoiceKey(string proformaTag, PMBillingRule rule)
  {
    return PMBillEngine.GetProformaBillingTag(proformaTag, rule);
  }

  public static string GetProformaBillingTag(string proformaTag, PMBillingRule rule = null)
  {
    return $"{proformaTag}-{(rule == null || string.IsNullOrEmpty(rule.InvoiceGroup) ? (object) "EMPTY_INV_GROUP" : (object) rule.InvoiceGroup)}";
  }

  protected virtual PMProject SelectProjectByID(int? projectID)
  {
    return PMProject.PK.Find((PXGraph) this, projectID);
  }

  public virtual void AutoReleaseCreatedDocuments(
    PMProject project,
    PMBillEngine.BillingResult result,
    PMRegister wipReversalDoc)
  {
    if (!project.AutomaticReleaseAR.GetValueOrDefault())
      return;
    if (result.Invoices.Count <= 0)
      return;
    try
    {
      ARDocumentRelease.ReleaseDoc(result.Invoices, false);
    }
    catch (Exception ex)
    {
      throw new PXException("Auto-release of ARInvoice document created during billing failed. Please try to release this document manually.", ex);
    }
    if (wipReversalDoc == null)
      return;
    try
    {
      RegisterRelease.Release(wipReversalDoc);
    }
    catch (Exception ex)
    {
      throw new PXException("During Billing ARInvoice was created successfully. PM Reversal document was created successfully. Auto-release of PM Reversal document failed. Please try to release this document manually.", ex);
    }
  }

  public virtual Tuple<DateTime, DateTime> GetBillingAndCutOffDates(
    ContractBillingSchedule schedule,
    DateTime? invoiceDate)
  {
    DateTime dateTime1;
    DateTime dateTime2;
    if (!invoiceDate.HasValue)
    {
      if (schedule.Type == "D")
      {
        dateTime1 = ((PXGraph) this).Accessinfo.BusinessDate ?? DateTime.Now;
        dateTime2 = dateTime1.AddDays(1.0);
      }
      else
      {
        dateTime1 = schedule.NextDate.Value;
        dateTime2 = dateTime1.AddDays((double) (this.IncludeTodaysTransactions ? 1 : 0));
      }
    }
    else
    {
      dateTime1 = invoiceDate.Value;
      dateTime2 = dateTime1.AddDays((double) (this.IncludeTodaysTransactions ? 1 : 0));
    }
    return new Tuple<DateTime, DateTime>(dateTime1, dateTime2);
  }

  public virtual string GenerateProformaTag(PMProject project, PMTask task)
  {
    string proformaTag = "P";
    if (task.BillSeparately.GetValueOrDefault())
      proformaTag = "T:" + task.TaskID.ToString();
    else if (task.LocationID.HasValue)
    {
      int? locationId1 = task.LocationID;
      int? locationId2 = project.LocationID;
      if (!(locationId1.GetValueOrDefault() == locationId2.GetValueOrDefault() & locationId1.HasValue == locationId2.HasValue))
        proformaTag = "L:" + task.LocationID.ToString();
    }
    return proformaTag;
  }

  public virtual void InsertTransactionsInProforma(
    PMProject project,
    List<PMBillEngine.BillingData> unbilled)
  {
    foreach (PMBillEngine.BillingData billingData in unbilled)
    {
      if (project.RetainageMode == "C" || project.SteppedRetainage.GetValueOrDefault())
      {
        billingData.Tran.RetainagePct = project.RetainagePct;
        if (billingData.RevenueBudget != null)
          billingData.RevenueBudget.RetainagePct = project.RetainagePct;
      }
      PMProformaLine pmProformaLine = this.InsertTransaction(project, billingData.Tran, billingData.SubCD, billingData.Note, billingData.Files);
      if (pmProformaLine != null)
      {
        foreach (PMTran transaction in billingData.Transactions)
        {
          transaction.ProformaRefNbr = pmProformaLine.RefNbr;
          transaction.ProformaLineNbr = pmProformaLine.LineNbr;
        }
      }
    }
  }

  public virtual void InsertTransactionsInInvoice(
    PMProject project,
    List<PMBillEngine.BillingData> unbilled,
    int mult)
  {
    PX.Objects.CM.Extensions.CurrencyInfo defaultCurrencyInfo = ((PXGraph) this.InvoiceEntry).FindImplementation<IPXCurrencyHelper>().GetDefaultCurrencyInfo();
    foreach (PMBillEngine.BillingData billingData in unbilled)
    {
      PX.Objects.AR.ARTran arTran1 = new PX.Objects.AR.ARTran();
      arTran1.BranchID = billingData.Tran.BranchID;
      int? inventoryId = billingData.Tran.InventoryID;
      int emptyInventoryId = PMInventorySelectorAttribute.EmptyInventoryID;
      arTran1.InventoryID = inventoryId.GetValueOrDefault() == emptyInventoryId & inventoryId.HasValue ? new int?() : billingData.Tran.InventoryID;
      arTran1.TranDesc = billingData.Tran.Description;
      arTran1.UOM = billingData.Tran.UOM;
      Decimal? nullable1 = billingData.Tran.Qty;
      Decimal num1 = (Decimal) mult;
      arTran1.Qty = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * num1) : new Decimal?();
      IProjectMultiCurrency multiCurrencyService = this.MultiCurrencyService;
      ARInvoiceEntry invoiceEntry = this.InvoiceEntry;
      PMProject project1 = project;
      PX.Objects.CM.Extensions.CurrencyInfo docCurrencyInfo = defaultCurrencyInfo;
      nullable1 = billingData.Tran.CuryAmount;
      Decimal num2 = (Decimal) mult;
      Decimal? nullable2 = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * num2) : new Decimal?();
      arTran1.CuryExtPrice = new Decimal?(multiCurrencyService.GetValueInBillingCurrency((PXGraph) invoiceEntry, project1, docCurrencyInfo, nullable2));
      arTran1.CuryUnitPrice = new Decimal?(this.MultiCurrencyService.GetValueInBillingCurrency((PXGraph) this.InvoiceEntry, project, defaultCurrencyInfo, billingData.Tran.CuryUnitPrice));
      arTran1.ProjectID = billingData.Tran.ProjectID;
      arTran1.TaskID = billingData.Tran.TaskID;
      arTran1.CostCodeID = billingData.Tran.CostCodeID;
      arTran1.Date = billingData.Tran.Date;
      arTran1.AccountID = billingData.Tran.AccountID;
      arTran1.SubID = billingData.Tran.SubID;
      arTran1.TaxCategoryID = billingData.Tran.TaxCategoryID;
      arTran1.RetainagePct = project.RetainageMode == "C" || project.SteppedRetainage.GetValueOrDefault() ? project.RetainagePct : billingData.Tran.RetainagePct;
      arTran1.FreezeManualDisc = new bool?(true);
      arTran1.ManualPrice = new bool?(true);
      PX.Objects.AR.ARTran tran = arTran1;
      bool? isPrepayment = billingData.Tran.IsPrepayment;
      if (isPrepayment.GetValueOrDefault() && !string.IsNullOrEmpty(billingData.Tran.DefCode))
        tran.DeferredCode = billingData.Tran.DefCode;
      PX.Objects.AR.ARTran arTran2 = this.InsertTransaction(tran, billingData.SubCD, billingData.Note, billingData.Files);
      isPrepayment = billingData.Tran.IsPrepayment;
      if (!isPrepayment.GetValueOrDefault() && billingData.Tran.AccountGroupID.HasValue)
      {
        ARInvoiceEntryExt extension = ((PXGraph) this.InvoiceEntry).GetExtension<ARInvoiceEntryExt>();
        PX.Objects.AR.ARTran line = arTran2;
        int? accountGroupId = billingData.Tran.AccountGroupID;
        nullable1 = ARDocType.SignAmount(arTran2.TranType);
        int mult1 = (int) (nullable1 ?? 1M);
        extension.SubtractValuesToInvoice(line, accountGroupId, mult1);
      }
      foreach (PMTran transaction in billingData.Transactions)
        transaction.RefLineNbr = arTran2.LineNbr;
    }
  }

  /// <summary>
  /// Inserts new AR transaction into current ARInvoice document
  /// </summary>
  /// <param name="tran">Transaction</param>
  /// <param name="subCD">override Subaccount </param>
  /// <param name="note">Note text</param>
  /// <param name="files">Attached files</param>
  public virtual PX.Objects.AR.ARTran InsertTransaction(
    PX.Objects.AR.ARTran tran,
    string subCD,
    string note,
    Guid[] files)
  {
    PX.Objects.AR.ARTran arTran = (PX.Objects.AR.ARTran) ((PXGraph) this.InvoiceEntry).Caches[typeof (PX.Objects.AR.ARTran)].Insert((object) tran);
    if (tran.AccountID.HasValue)
    {
      int? accountId1 = tran.AccountID;
      int? accountId2 = arTran.AccountID;
      if (!(accountId1.GetValueOrDefault() == accountId2.GetValueOrDefault() & accountId1.HasValue == accountId2.HasValue))
      {
        arTran.AccountID = tran.AccountID;
        arTran = ((PXSelectBase<PX.Objects.AR.ARTran>) this.InvoiceEntry.Transactions).Update(arTran);
      }
    }
    if (subCD != null)
      ((PXSelectBase<PX.Objects.AR.ARTran>) this.InvoiceEntry.Transactions).SetValueExt<PX.Objects.AR.ARTran.subID>(arTran, (object) subCD);
    if (note != null)
      PXNoteAttribute.SetNote(((PXSelectBase) this.InvoiceEntry.Transactions).Cache, (object) arTran, note);
    if (files != null && files.Length != 0)
      PXNoteAttribute.SetFileNotes(((PXSelectBase) this.InvoiceEntry.Transactions).Cache, (object) arTran, files);
    return arTran;
  }

  public virtual PMProformaLine InsertTransaction(
    PMProject project,
    PMProformaLine tran,
    string subCD,
    string note,
    Guid[] files)
  {
    PMProformaLine line = (PMProformaLine) null;
    PX.Objects.CM.Extensions.CurrencyInfo defaultCurrencyInfo = ((PXGraph) this.ProformaEntry).FindImplementation<IPXCurrencyHelper>().GetDefaultCurrencyInfo();
    tran.CuryAmount = new Decimal?(this.MultiCurrencyService.GetValueInBillingCurrency((PXGraph) this.ProformaEntry, project, defaultCurrencyInfo, tran.CuryAmount));
    tran.CuryBillableAmount = new Decimal?(this.MultiCurrencyService.GetValueInBillingCurrency((PXGraph) this.ProformaEntry, project, defaultCurrencyInfo, tran.CuryBillableAmount));
    tran.CuryUnitPrice = new Decimal?(this.MultiCurrencyService.GetValueInBillingCurrency((PXGraph) this.ProformaEntry, project, defaultCurrencyInfo, tran.CuryUnitPrice));
    if (tran.Type == "T")
    {
      line = (PMProformaLine) ((PXSelectBase<PMProformaTransactLine>) this.ProformaEntry.TransactionLines).Insert((PMProformaTransactLine) tran);
      if (subCD != null)
        ((PXGraph) this.ProformaEntry).Caches[typeof (PMProformaTransactLine)].SetValueExt<PMProformaTransactLine.subID>((object) line, (object) subCD);
      if (note != null)
        PXNoteAttribute.SetNote(((PXSelectBase) this.ProformaEntry.TransactionLines).Cache, (object) line, note);
      if (files != null && files.Length != 0)
        PXNoteAttribute.SetFileNotes(((PXSelectBase) this.ProformaEntry.TransactionLines).Cache, (object) line, files);
    }
    else if (((PXSelectBase) this.ProformaEntry.Document).Cache.GetStatus((object) ((PXSelectBase<PMProforma>) this.ProformaEntry.Document).Current) == 2)
    {
      line = (PMProformaLine) ((PXSelectBase<PMProformaProgressLine>) this.ProformaEntry.ProgressiveLines).Insert((PMProformaProgressLine) tran);
      this.ProformaEntry.SubtractValuesToInvoice((PMProformaProgressLine) line, line.CuryAmount, line.Qty);
      if (subCD != null)
        ((PXGraph) this.ProformaEntry).Caches[typeof (PMProformaProgressLine)].SetValueExt<PMProformaProgressLine.subID>((object) line, (object) subCD);
      if (note != null)
        PXNoteAttribute.SetNote(((PXSelectBase) this.ProformaEntry.ProgressiveLines).Cache, (object) line, note);
      if (files != null && files.Length != 0)
        PXNoteAttribute.SetFileNotes(((PXSelectBase) this.ProformaEntry.ProgressiveLines).Cache, (object) line, files);
    }
    return line;
  }

  public virtual PMProforma InsertNewProformaDocument(
    string finPeriod,
    PX.Objects.AR.Customer customer,
    PMProject project,
    DateTime billingDate,
    string docDesc,
    int? locationID)
  {
    PMProforma data = new PMProforma();
    data.ProjectID = project.ContractID;
    data.CustomerID = customer.BAccountID;
    data.LocationID = locationID ?? project.LocationID ?? customer.DefLocationID;
    data.InvoiceDate = new DateTime?(billingDate);
    data.Description = docDesc != null ? Extentions.Truncate(docDesc.Trim(), (int) byte.MaxValue) : (string) null;
    if (project.RetainageMode == "C" || project.SteppedRetainage.GetValueOrDefault())
      data.RetainagePct = project.RetainagePct;
    SharedRecordAttribute.DefaultRecord<PMProforma.shipAddressID>(((PXSelectBase) this.ProformaEntry.Document).Cache, (object) data);
    SharedRecordAttribute.DefaultRecord<PMProforma.shipContactID>(((PXSelectBase) this.ProformaEntry.Document).Cache, (object) data);
    int? nullable1 = project.DefaultBranchID;
    if (nullable1.HasValue)
      data.BranchID = project.DefaultBranchID;
    data.FinPeriodID = finPeriod;
    PMProforma pmProforma = ((PXSelectBase<PMProforma>) this.ProformaEntry.Document).Insert(data);
    ((PXSelectBase) this.ProformaEntry.Document).Cache.SetValueExt<PMProforma.customerID>((object) pmProforma, (object) customer.BAccountID);
    ((PXSelectBase) this.ProformaEntry.Document).Cache.SetValueExt<PMProforma.curyID>((object) pmProforma, (object) project.BillingCuryID);
    ((PXSelectBase) this.ProformaEntry.Document).Cache.RaiseFieldUpdated<PMProforma.termsID>((object) pmProforma, (object) null);
    if (PXAccess.FeatureInstalled<FeaturesSet.multipleCalendarsSupport>() && finPeriod != null)
      pmProforma.FinPeriodID = this.FinPeriodRepository.GetFinPeriodByBranchAndMasterPeriodID(pmProforma.BranchID, finPeriod);
    if (string.IsNullOrEmpty(pmProforma.TaxZoneID))
    {
      TaxBaseAttribute.SetTaxCalc<PMProformaTransactLine.taxCategoryID>(((PXSelectBase) this.ProformaEntry.TransactionLines).Cache, (object) null, TaxCalc.NoCalc);
      TaxBaseAttribute.SetTaxCalc<PMProformaProgressLine.taxCategoryID>(((PXSelectBase) this.ProformaEntry.ProgressiveLines).Cache, (object) null, TaxCalc.NoCalc);
    }
    else
    {
      TaxBaseAttribute.SetTaxCalc<PMProformaTransactLine.taxCategoryID>(((PXSelectBase) this.ProformaEntry.TransactionLines).Cache, (object) null, TaxCalc.Calc);
      TaxBaseAttribute.SetTaxCalc<PMProformaProgressLine.taxCategoryID>(((PXSelectBase) this.ProformaEntry.ProgressiveLines).Cache, (object) null, TaxCalc.Calc);
    }
    PMAddress source = PXResultset<PMAddress>.op_Implicit(PXSelectBase<PMAddress, PXSelect<PMAddress, Where<PMAddress.addressID, Equal<Required<PMProject.siteAddressID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) project.SiteAddressID
    }));
    if (source != null && !source.IsDefaultAddress.GetValueOrDefault() && !string.IsNullOrWhiteSpace(project.RevenueTaxZoneID) && ((PXSelectBase<PMSetup>) this.Setup).Current.CalculateProjectSpecificTaxes.GetValueOrDefault())
    {
      PMShippingAddress current = ((PXSelectBase<PMShippingAddress>) this.ProformaEntry.Shipping_Address).Current;
      PMAddress pmAddress = PXResultset<PMAddress>.op_Implicit(PXSelectBase<PMAddress, PXSelect<PMAddress, Where<PMAddress.addressID, Equal<Required<PMProject.billAddressID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) project.BillAddressID
      }));
      if (current == null)
      {
        PMShippingAddress instance = (PMShippingAddress) ((PXSelectBase) this.ProformaEntry.Shipping_Address).Cache.CreateInstance();
        AddressAttribute.Copy((IAddress) instance, (IAddress) source);
        instance.IsValidated = source.IsValidated;
        PMShippingAddress pmShippingAddress = instance;
        int? nullable2;
        if (pmAddress == null)
        {
          nullable1 = new int?();
          nullable2 = nullable1;
        }
        else
          nullable2 = pmAddress.BAccountAddressID;
        pmShippingAddress.BAccountAddressID = nullable2;
        instance.IsDefaultAddress = new bool?(true);
        ((PXSelectBase<PMProforma>) this.ProformaEntry.Document).Current.ShipAddressID = (((PXSelectBase<PMShippingAddress>) this.ProformaEntry.Shipping_Address).Current = ((PXSelectBase<PMShippingAddress>) this.ProformaEntry.Shipping_Address).Insert(instance)).AddressID;
      }
      else
      {
        PMShippingAddress pmShippingAddress = current;
        int? nullable3;
        if (pmAddress == null)
        {
          nullable1 = new int?();
          nullable3 = nullable1;
        }
        else
          nullable3 = pmAddress.BAccountAddressID;
        pmShippingAddress.BAccountAddressID = nullable3;
        current.BAccountID = source.BAccountID;
        current.RevisionID = source.RevisionID;
        current.IsDefaultAddress = source.IsDefaultAddress;
        current.AddressLine1 = source.AddressLine1;
        current.AddressLine2 = source.AddressLine2;
        current.AddressLine3 = source.AddressLine3;
        current.City = source.City;
        current.State = source.State;
        current.PostalCode = source.PostalCode;
        current.CountryID = source.CountryID;
        current.IsValidated = source.IsValidated;
        current.Latitude = source.Latitude;
        current.Longitude = source.Longitude;
        current.Department = source.Department;
        current.SubDepartment = source.SubDepartment;
        current.StreetName = source.StreetName;
        current.BuildingNumber = source.BuildingNumber;
        current.BuildingName = source.BuildingName;
        current.Floor = source.Floor;
        current.UnitNumber = source.UnitNumber;
        current.PostBox = source.PostBox;
        current.Room = source.Room;
        current.TownLocationName = source.TownLocationName;
        current.DistrictName = source.DistrictName;
        current.IsDefaultAddress = new bool?(true);
      }
    }
    return pmProforma;
  }

  public virtual PX.Objects.AR.ARInvoice InsertNewInvoiceDocument(
    string finPeriod,
    string docType,
    bool applyRetainage,
    PX.Objects.AR.Customer customer,
    PMProject project,
    DateTime billingDate,
    string docDesc,
    int? locationID)
  {
    this.CheckMigrationMode();
    PX.Objects.AR.ARInvoice row = (PX.Objects.AR.ARInvoice) ((PXSelectBase) this.InvoiceEntry.Document).Cache.CreateInstance();
    row.DocType = docType;
    row.CustomerID = customer.BAccountID;
    row.DocDate = new DateTime?(billingDate);
    row.DocDesc = docDesc != null ? Extentions.Truncate(docDesc.Trim(), 256 /*0x0100*/) : (string) null;
    row.RetainageApply = new bool?(applyRetainage);
    if (project.RetainageMode != "N")
      row.PaymentsByLinesAllowed = new bool?(true);
    if (PXAccess.FeatureInstalled<FeaturesSet.multipleCalendarsSupport>())
    {
      AROpenPeriodAttribute openPeriodAttribute = ((PXSelectBase) this.InvoiceEntry.Document).Cache.GetAttributesReadonly<PX.Objects.AR.ARInvoice.finPeriodID>().OfType<AROpenPeriodAttribute>().SingleOrDefault<AROpenPeriodAttribute>();
      PeriodValidation periodValidation = openPeriodAttribute != null ? openPeriodAttribute.ValidatePeriod : throw new NullReferenceException("Cannot find AROpenPeriodAttribute on field finPeriodID. openPeriodAttribute is null");
      bool redefaultOnDateChanged = openPeriodAttribute.RedefaultOnDateChanged;
      PXCacheEx.Adjust<AROpenPeriodAttribute>(((PXSelectBase) this.InvoiceEntry.Document).Cache, (object) null).For<PX.Objects.AR.ARInvoice.finPeriodID>((Action<AROpenPeriodAttribute>) (attr =>
      {
        attr.ValidatePeriod = PeriodValidation.Nothing;
        attr.RedefaultOnDateChanged = false;
      }));
      try
      {
        row = ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.InvoiceEntry.Document).Insert(row);
      }
      finally
      {
        PXCacheEx.Adjust<AROpenPeriodAttribute>(((PXSelectBase) this.InvoiceEntry.Document).Cache, (object) null).For<PX.Objects.AR.ARInvoice.finPeriodID>((Action<AROpenPeriodAttribute>) (attr =>
        {
          attr.ValidatePeriod = periodValidation;
          attr.RedefaultOnDateChanged = redefaultOnDateChanged;
        }));
      }
    }
    else
      row = ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.InvoiceEntry.Document).Insert(row);
    bool? retainageApply = row.RetainageApply;
    bool flag = false;
    if (retainageApply.GetValueOrDefault() == flag & retainageApply.HasValue)
    {
      ((PXSelectBase) this.InvoiceEntry.Document).Cache.SetValue<PX.Objects.AR.ARRegister.retainageSubID>((object) row, (object) null);
      ((PXSelectBase) this.InvoiceEntry.Document).Cache.SetValue<PX.Objects.AR.ARRegister.retainageAcctID>((object) row, (object) null);
    }
    ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.InvoiceEntry.Document).SetValueExt<PX.Objects.AR.ARInvoice.customerID>(row, (object) customer.BAccountID);
    if (!string.IsNullOrEmpty(finPeriod))
      FinPeriodIDAttribute.SetPeriodsByMaster<PX.Objects.AR.ARInvoice.finPeriodID>(((PXSelectBase) this.InvoiceEntry.Document).Cache, (object) row, finPeriod);
    if (locationID.HasValue)
      ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.InvoiceEntry.Document).SetValueExt<PX.Objects.AR.ARInvoice.customerLocationID>(row, (object) locationID);
    row.ProjectID = project.ContractID;
    ((PXSelectBase) this.InvoiceEntry.Document).Cache.SetDefaultExt<PX.Objects.AR.ARInvoice.taxZoneID>((object) row);
    ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.InvoiceEntry.Document).SetValueExt<PX.Objects.AR.ARInvoice.curyID>(row, (object) project.BillingCuryID);
    if (!string.IsNullOrEmpty(project.TermsID))
      ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.InvoiceEntry.Document).SetValueExt<PX.Objects.AR.ARInvoice.termsID>(row, (object) project.TermsID);
    bool? nullable;
    if (docType == "CRM")
    {
      nullable = ((PXSelectBase<ARSetup>) this.arSetup).Current.TermsInCreditMemos;
      if (!nullable.GetValueOrDefault())
        ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.InvoiceEntry.Document).SetValueExt<PX.Objects.AR.ARInvoice.termsID>(row, (object) null);
    }
    if (applyRetainage)
      ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.InvoiceEntry.Document).SetValueExt<PX.Objects.AR.ARInvoice.retainageApply>(row, (object) true);
    PMAddress source1 = PXResultset<PMAddress>.op_Implicit(PXSelectBase<PMAddress, PXSelect<PMAddress, Where<PMAddress.addressID, Equal<Required<PMProject.billAddressID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) project.BillAddressID
    }));
    if (source1 != null)
    {
      nullable = source1.IsDefaultAddress;
      if (!nullable.GetValueOrDefault())
      {
        ARAddress current = ((PXSelectBase<ARAddress>) this.InvoiceEntry.Billing_Address).Current;
        if (current == null)
        {
          ARAddress instance = (ARAddress) ((PXSelectBase) this.InvoiceEntry.Billing_Address).Cache.CreateInstance();
          AddressAttribute.Copy((IAddress) instance, (IAddress) source1);
          instance.IsValidated = source1.IsValidated;
          instance.BAccountAddressID = source1.BAccountAddressID;
          instance.IsDefaultAddress = new bool?(true);
          ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.InvoiceEntry.Document).Current.BillAddressID = (((PXSelectBase<ARAddress>) this.InvoiceEntry.Billing_Address).Current = ((PXSelectBase<ARAddress>) this.InvoiceEntry.Billing_Address).Insert(instance)).AddressID;
        }
        else
        {
          current.BAccountAddressID = source1.BAccountAddressID;
          current.BAccountID = source1.BAccountID;
          current.RevisionID = source1.RevisionID;
          current.IsDefaultAddress = source1.IsDefaultAddress;
          current.AddressLine1 = source1.AddressLine1;
          current.AddressLine2 = source1.AddressLine2;
          current.AddressLine3 = source1.AddressLine3;
          current.City = source1.City;
          current.State = source1.State;
          current.PostalCode = source1.PostalCode;
          current.CountryID = source1.CountryID;
          current.IsValidated = source1.IsValidated;
          current.Department = source1.Department;
          current.SubDepartment = source1.SubDepartment;
          current.StreetName = source1.StreetName;
          current.BuildingNumber = source1.BuildingNumber;
          current.BuildingName = source1.BuildingName;
          current.Floor = source1.Floor;
          current.UnitNumber = source1.UnitNumber;
          current.PostBox = source1.PostBox;
          current.Room = source1.Room;
          current.TownLocationName = source1.TownLocationName;
          current.DistrictName = source1.DistrictName;
        }
      }
    }
    PMAddress source2 = PXResultset<PMAddress>.op_Implicit(PXSelectBase<PMAddress, PXSelect<PMAddress, Where<PMAddress.addressID, Equal<Required<PMProject.siteAddressID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) project.SiteAddressID
    }));
    if (source2 != null)
    {
      nullable = source2.IsDefaultAddress;
      if (!nullable.GetValueOrDefault() && !string.IsNullOrWhiteSpace(project.RevenueTaxZoneID))
      {
        ARAddress current = (ARAddress) ((PXSelectBase<ARShippingAddress>) this.InvoiceEntry.Shipping_Address).Current;
        if (current == null)
        {
          ARShippingAddress instance = (ARShippingAddress) ((PXSelectBase) this.InvoiceEntry.Shipping_Address).Cache.CreateInstance();
          AddressAttribute.Copy((IAddress) instance, (IAddress) source2);
          instance.IsValidated = source2.IsValidated;
          instance.BAccountAddressID = source1.BAccountAddressID;
          instance.IsDefaultAddress = new bool?(true);
          ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.InvoiceEntry.Document).Current.ShipAddressID = (((PXSelectBase<ARShippingAddress>) this.InvoiceEntry.Shipping_Address).Current = ((PXSelectBase<ARShippingAddress>) this.InvoiceEntry.Shipping_Address).Insert(instance)).AddressID;
        }
        else
        {
          current.BAccountAddressID = source1.BAccountAddressID;
          current.BAccountID = source2.BAccountID;
          current.RevisionID = source2.RevisionID;
          current.IsDefaultAddress = source2.IsDefaultAddress;
          current.AddressLine1 = source2.AddressLine1;
          current.AddressLine2 = source2.AddressLine2;
          current.AddressLine3 = source2.AddressLine3;
          current.City = source2.City;
          current.State = source2.State;
          current.PostalCode = source2.PostalCode;
          current.CountryID = source2.CountryID;
          current.IsValidated = source2.IsValidated;
          current.Latitude = source2.Latitude;
          current.Longitude = source2.Longitude;
          current.Department = source2.Department;
          current.SubDepartment = source2.SubDepartment;
          current.StreetName = source2.StreetName;
          current.BuildingNumber = source2.BuildingNumber;
          current.BuildingName = source2.BuildingName;
          current.Floor = source2.Floor;
          current.UnitNumber = source2.UnitNumber;
          current.PostBox = source2.PostBox;
          current.Room = source2.Room;
          current.TownLocationName = source2.TownLocationName;
          current.DistrictName = source2.DistrictName;
          current.IsDefaultAddress = new bool?(true);
        }
      }
    }
    PMContact source3 = PXResultset<PMContact>.op_Implicit(PXSelectBase<PMContact, PXSelect<PMContact, Where<PMContact.contactID, Equal<Required<PMProject.billContactID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) project.BillContactID
    }));
    if (source3 != null)
    {
      nullable = source3.IsDefaultContact;
      if (!nullable.GetValueOrDefault())
      {
        ARContact current = ((PXSelectBase<ARContact>) this.InvoiceEntry.Billing_Contact).Current;
        if (current == null)
        {
          ARContact instance = (ARContact) ((PXSelectBase) this.InvoiceEntry.Billing_Contact).Cache.CreateInstance();
          ContactAttribute.CopyContact((IContact) instance, (IContact) source3);
          instance.BAccountContactID = source3.BAccountContactID;
          instance.IsDefaultContact = new bool?(true);
          ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.InvoiceEntry.Document).Current.BillContactID = (((PXSelectBase<ARContact>) this.InvoiceEntry.Billing_Contact).Current = ((PXSelectBase<ARContact>) this.InvoiceEntry.Billing_Contact).Insert(instance)).ContactID;
        }
        else
        {
          current.BAccountContactID = source3.BAccountContactID;
          current.BAccountID = source3.BAccountID;
          current.RevisionID = source3.RevisionID;
          current.IsDefaultContact = source3.IsDefaultContact;
          current.FullName = source3.FullName;
          current.Salutation = source3.Salutation;
          current.Attention = source3.Attention;
          current.Title = source3.Title;
          current.Phone1 = source3.Phone1;
          current.Phone1Type = source3.Phone1Type;
          current.Phone2 = source3.Phone2;
          current.Phone2Type = source3.Phone2Type;
          current.Phone3 = source3.Phone3;
          current.Phone3Type = source3.Phone3Type;
          current.Fax = source3.Fax;
          current.FaxType = source3.FaxType;
          current.Email = source3.Email;
        }
      }
    }
    return row;
  }

  private void VerifyCustomerLocation(PXCache cache, PXFieldVerifyingEventArgs e)
  {
    PMProforma row = (PMProforma) e.Row;
    int? locationID = (int?) e.NewValue;
    if (row == null || !row.CustomerID.HasValue || !locationID.HasValue || ((PXSelectBase<PMProject>) this.Project).Current == null || PX.Objects.CR.Location.PK.Find((PXGraph) this, row.CustomerID, locationID) != null)
      return;
    PMTask pmTask = this.SelectBillableTasks(((PXSelectBase<PMProject>) this.Project).Current).Where<PMTask>((Func<PMTask, bool>) (x =>
    {
      int? locationId = x.LocationID;
      int? nullable = locationID;
      return locationId.GetValueOrDefault() == nullable.GetValueOrDefault() & locationId.HasValue == nullable.HasValue;
    })).FirstOrDefault<PMTask>();
    if (pmTask != null)
    {
      PX.Objects.AR.Customer customer = PX.Objects.AR.Customer.PK.Find((PXGraph) this, row.CustomerID);
      throw new PXException("The {0} customer location that is assigned to the {1} project task of the {2} project does not belong to the {3} customer of the project. Specify another customer location for the project task on the Project Tasks (PM302000) form.", new object[4]
      {
        (object) pmTask.LocationID,
        (object) pmTask.TaskCD,
        (object) ((PXSelectBase<PMProject>) this.Project).Current.ContractCD,
        (object) customer.AcctCD
      });
    }
  }

  public virtual Tuple<string, bool> GetDocTypeAndRetainage(
    PMProject project,
    List<PMBillEngine.BillingData> value)
  {
    Decimal num1 = 0M;
    bool flag = false;
    foreach (PMBillEngine.BillingData billingData in value)
    {
      num1 += billingData.Tran.CuryAmount.GetValueOrDefault();
      Decimal? retainagePct = billingData.Tran.RetainagePct;
      Decimal num2 = 0M;
      if (retainagePct.GetValueOrDefault() > num2 & retainagePct.HasValue)
        flag = true;
    }
    return num1 >= 0M ? new Tuple<string, bool>("INV", flag) : new Tuple<string, bool>("CRM", flag);
  }

  public virtual bool IsNonGL(PMTran tran)
  {
    return tran.IsNonGL.GetValueOrDefault() || !tran.AccountID.HasValue && !tran.OffsetAccountID.HasValue || tran.BatchNbr == null;
  }

  public virtual List<PMBillEngine.BillingData> BillTask(
    PMProject project,
    PX.Objects.AR.Customer customer,
    PMTask task,
    DateTime billingDate)
  {
    List<PMBillEngine.BillingData> source1 = new List<PMBillEngine.BillingData>();
    Dictionary<int, Decimal> availableQty = new Dictionary<int, Decimal>();
    Dictionary<int, PMRecurringItem> billingItems = new Dictionary<int, PMRecurringItem>();
    List<PMBillingRule> source2;
    if (this.billingRules.TryGetValue(task.BillingID, out source2))
    {
      bool flag = false;
      foreach (PMBillingRule rule in source2)
      {
        if (!flag)
        {
          source1.AddRange((IEnumerable<PMBillEngine.BillingData>) this.BillPrepayment(project, customer, task, rule, billingDate));
          flag = true;
        }
        if (rule.Type == "T")
          source1.AddRange((IEnumerable<PMBillEngine.BillingData>) this.BillTask(project, customer, task, rule, billingDate, availableQty, billingItems, false));
        else
          source1.AddRange((IEnumerable<PMBillEngine.BillingData>) this.BillFixPriceTask(project, customer, task, rule, billingDate));
      }
    }
    int? taskId;
    List<PMRecurringItem> recurrentItems;
    if (source1.Count != 0)
    {
      Dictionary<int, List<PMRecurringItem>> recurrentItemsByTask = this.recurrentItemsByTask;
      taskId = task.TaskID;
      int key = taskId.Value;
      ref List<PMRecurringItem> local = ref recurrentItems;
      if (recurrentItemsByTask.TryGetValue(key, out local))
      {
        PMBillingRule rule = source1.First<PMBillEngine.BillingData>().Rule;
        source1.AddRange((IEnumerable<PMBillEngine.BillingData>) this.BillRecurrentItems(recurrentItems, task, rule, billingDate, out availableQty, out billingItems));
        goto label_16;
      }
    }
    Dictionary<int, List<PMRecurringItem>> recurrentItemsByTask1 = this.recurrentItemsByTask;
    taskId = task.TaskID;
    int key1 = taskId.Value;
    ref List<PMRecurringItem> local1 = ref recurrentItems;
    if (recurrentItemsByTask1.TryGetValue(key1, out local1))
    {
      PMBillingRule rule = source2.FirstOrDefault<PMBillingRule>();
      if (rule != null)
        source1.AddRange((IEnumerable<PMBillEngine.BillingData>) this.BillRecurrentItems(recurrentItems, task, rule, billingDate, out availableQty, out billingItems));
    }
label_16:
    return source1;
  }

  public virtual List<PMTran> ReverseWipTask(PMTask task, DateTime billingDate)
  {
    List<PMTran> pmTranList = new List<PMTran>();
    if (task.WipAccountGroupID.HasValue)
    {
      PXSelect<PMTran, Where<PMTran.projectID, Equal<Required<PMTran.projectID>>, And<PMTran.taskID, Equal<Required<PMTran.taskID>>, And<PMTran.accountGroupID, Equal<Required<PMTran.accountGroupID>>, And<PMTran.date, Less<Required<PMTran.date>>, And<PMTran.billed, Equal<False>, And<PMTran.excludedFromBilling, Equal<False>, And<PMTran.released, Equal<True>>>>>>>>> pxSelect = new PXSelect<PMTran, Where<PMTran.projectID, Equal<Required<PMTran.projectID>>, And<PMTran.taskID, Equal<Required<PMTran.taskID>>, And<PMTran.accountGroupID, Equal<Required<PMTran.accountGroupID>>, And<PMTran.date, Less<Required<PMTran.date>>, And<PMTran.billed, Equal<False>, And<PMTran.excludedFromBilling, Equal<False>, And<PMTran.released, Equal<True>>>>>>>>>((PXGraph) this);
      DateTime dateTime = billingDate;
      ContractBillingSchedule contractBillingSchedule = PXResultset<ContractBillingSchedule>.op_Implicit(PXSelectBase<ContractBillingSchedule, PXSelect<ContractBillingSchedule>.Config>.Search<ContractBillingSchedule.contractID>((PXGraph) this, (object) task.ProjectID, Array.Empty<object>()));
      if (contractBillingSchedule != null && contractBillingSchedule.Type == "D")
        dateTime = billingDate.AddDays(1.0);
      else if (this.IncludeTodaysTransactions)
        dateTime = billingDate.AddDays(1.0);
      object[] objArray = new object[4]
      {
        (object) task.ProjectID,
        (object) task.TaskID,
        (object) task.WipAccountGroupID,
        (object) dateTime
      };
      foreach (PXResult<PMTran> pxResult in ((PXSelectBase<PMTran>) pxSelect).Select(objArray))
      {
        PMTran tran = PXResult<PMTran>.op_Implicit(pxResult);
        foreach (PMTran pmTran in (IEnumerable<PMTran>) this.ReverseTran(tran))
        {
          pmTran.Date = new DateTime?(billingDate);
          pmTran.FinPeriodID = (string) null;
          pmTranList.Add(pmTran);
        }
        tran.ExcludedFromBilling = new bool?(true);
        tran.ExcludedFromBillingReason = PXMessages.LocalizeNoPrefix("WIP Reversed");
        ((PXSelectBase<PMTran>) this.Transactions).Update(tran);
        RegisterReleaseProcess.SubtractFromUnbilledSummary((PXGraph) this, tran);
      }
    }
    return pmTranList;
  }

  public virtual List<PMBillEngine.BillingData> BillRecurrentItems(
    List<PMRecurringItem> recurrentItems,
    PMTask task,
    PMBillingRule rule,
    DateTime billingDate,
    out Dictionary<int, Decimal> availableQty,
    out Dictionary<int, PMRecurringItem> billingItems)
  {
    List<PMBillEngine.BillingData> billingDataList = new List<PMBillEngine.BillingData>();
    availableQty = new Dictionary<int, Decimal>();
    billingItems = new Dictionary<int, PMRecurringItem>();
    if (task.IsCompleted.GetValueOrDefault())
      return billingDataList;
    PMProject pmProject = PMProject.PK.Find((PXGraph) this, task.ProjectID);
    PX.Objects.AR.Customer customer = PXResultset<PX.Objects.AR.Customer>.op_Implicit(PXSelectBase<PX.Objects.AR.Customer, PXSelect<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Required<PX.Objects.AR.Customer.bAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) pmProject.CustomerID
    }));
    foreach (PMRecurringItem recurrentItem in recurrentItems)
    {
      if (recurrentItem.InventoryID.HasValue)
      {
        billingItems.Add(recurrentItem.InventoryID.Value, recurrentItem);
        Decimal? nullable1 = recurrentItem.Included;
        Decimal num1 = 0M;
        if (nullable1.GetValueOrDefault() > num1 & nullable1.HasValue)
        {
          if (recurrentItem.ResetUsage == "B")
          {
            Dictionary<int, Decimal> dictionary = availableQty;
            int key = recurrentItem.InventoryID.Value;
            nullable1 = recurrentItem.Included;
            Decimal num2 = nullable1.Value;
            dictionary.Add(key, num2);
          }
          else
          {
            Decimal? nullable2 = recurrentItem.Included;
            Decimal num3 = nullable2.Value;
            nullable1 = recurrentItem.LastBilledQty;
            Decimal? nullable3;
            if (!nullable1.HasValue)
            {
              nullable2 = new Decimal?();
              nullable3 = nullable2;
            }
            else
              nullable3 = new Decimal?(num3 - nullable1.GetValueOrDefault());
            nullable2 = nullable3;
            Decimal valueOrDefault = nullable2.GetValueOrDefault();
            if (valueOrDefault > 0M)
              availableQty.Add(recurrentItem.InventoryID.Value, valueOrDefault);
          }
        }
      }
      bool flag = false;
      if (recurrentItem.ResetUsage == "B")
        flag = true;
      else if (!recurrentItem.LastBilledDate.HasValue)
        flag = true;
      if (flag)
      {
        PMProformaTransactLine tran = new PMProformaTransactLine();
        tran.Type = "T";
        tran.InventoryID = recurrentItem.InventoryID;
        tran.Description = recurrentItem.Description;
        tran.BillableQty = recurrentItem.Included;
        tran.Qty = tran.BillableQty;
        tran.UOM = recurrentItem.UOM;
        tran.CuryBillableAmount = recurrentItem.Amount;
        tran.CuryAmount = tran.CuryBillableAmount;
        tran.ProjectID = task.ProjectID;
        tran.TaskID = task.TaskID;
        tran.BranchID = recurrentItem.BranchID;
        tran.CostCodeID = CostCodeAttribute.DefaultCostCode;
        string subCD = (string) null;
        if (recurrentItem.AccountSource != "N")
        {
          if (recurrentItem.AccountSource == "B")
          {
            if (recurrentItem.AccountID.HasValue)
              tran.AccountID = recurrentItem.AccountID;
            else if (recurrentItem.InventoryID.HasValue)
            {
              PX.Objects.IN.InventoryItem inventoryItem = PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<PX.Objects.IN.InventoryItem.inventoryID>>>>.Config>.Select((PXGraph) this, new object[1]
              {
                (object) recurrentItem.InventoryID
              }));
              throw new PXException("Recurring billing for the {0} task and the {1} item has been configured to use the account from the recurring item, but the account has not been specified for the recurring item.", new object[2]
              {
                (object) task.TaskCD,
                (object) inventoryItem.InventoryCD
              });
            }
          }
          else if (recurrentItem.AccountSource == "P")
          {
            if (pmProject.DefaultSalesAccountID.HasValue)
              tran.AccountID = pmProject.DefaultSalesAccountID;
            else if (recurrentItem.InventoryID.HasValue)
              throw new PXException("The {0} recurrent item has been configured to use the account from the project, but the default account has not been specified for the {1} project.", new object[2]
              {
                (object) PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<PX.Objects.IN.InventoryItem.inventoryID>>>>.Config>.Select((PXGraph) this, new object[1]
                {
                  (object) recurrentItem.InventoryID
                })).InventoryCD,
                (object) pmProject.ContractCD
              });
          }
          else if (recurrentItem.AccountSource == "T")
          {
            if (task.DefaultSalesAccountID.HasValue)
              tran.AccountID = task.DefaultSalesAccountID;
            else if (recurrentItem.InventoryID.HasValue)
            {
              PX.Objects.IN.InventoryItem inventoryItem = PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<PX.Objects.IN.InventoryItem.inventoryID>>>>.Config>.Select((PXGraph) this, new object[1]
              {
                (object) recurrentItem.InventoryID
              }));
              throw new PXException("Recurring billing for the {0} task and the {1} item has been configured to use the account from the task, but the default sales account has not been specified for the {0} task of the {2} project.", new object[3]
              {
                (object) task.TaskCD,
                (object) inventoryItem.InventoryCD,
                (object) pmProject.ContractCD
              });
            }
          }
          else if (recurrentItem.AccountSource == "I")
          {
            PX.Objects.IN.InventoryItem inventoryItem = PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<PX.Objects.IN.InventoryItem.inventoryID>>>>.Config>.Select((PXGraph) this, new object[1]
            {
              (object) recurrentItem.InventoryID
            }));
            if (inventoryItem != null)
            {
              if (inventoryItem.SalesAcctID.HasValue)
                tran.AccountID = inventoryItem.SalesAcctID;
              else
                throw new PXException("Recurring billing for the {0} task and the {1} item has been configured to use the account from the inventory item, but the sales account has not been specified for the item.", new object[2]
                {
                  (object) task.TaskCD,
                  (object) inventoryItem.InventoryCD
                });
            }
          }
          else if (recurrentItem.AccountSource == "C" && customer != null)
          {
            PX.Objects.CR.Location location = PXResultset<PX.Objects.CR.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Location, PXSelect<PX.Objects.CR.Location, Where<PX.Objects.CR.Location.bAccountID, Equal<Required<PX.Objects.CR.Location.bAccountID>>, And<PX.Objects.CR.Location.locationID, Equal<Required<PX.Objects.CR.Location.locationID>>>>>.Config>.Select((PXGraph) this, new object[2]
            {
              (object) customer.BAccountID,
              (object) customer.DefLocationID
            }));
            if (location != null)
            {
              if (location.CSalesAcctID.HasValue)
                tran.AccountID = location.CSalesAcctID;
              else
                throw new PXException("The {0} recurring item has been configured to use the account from the customer, but the sales account has not been specified for the {1} customer.", new object[2]
                {
                  (object) PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<PX.Objects.IN.InventoryItem.inventoryID>>>>.Config>.Select((PXGraph) this, new object[1]
                  {
                    (object) recurrentItem.InventoryID
                  })).InventoryCD,
                  (object) customer.AcctCD
                });
            }
          }
          if (!tran.AccountID.HasValue && !string.IsNullOrEmpty(recurrentItem.SubMask) && recurrentItem.InventoryID.HasValue)
            throw new PXException("Billing Rule '{0}' will not be able the compose the subaccount since account was not determined.", new object[1]
            {
              (object) PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<PX.Objects.IN.InventoryItem.inventoryID>>>>.Config>.Select((PXGraph) this, new object[1]
              {
                (object) recurrentItem.InventoryID
              })).InventoryCD
            });
          if (tran.AccountID.HasValue && !string.IsNullOrEmpty(recurrentItem.SubMask))
            subCD = PMRecurentBillSubAccountMaskAttribute.MakeSub<PMBillingRule.subMask>((PXGraph) this, recurrentItem.SubMask, new object[3]
            {
              (object) recurrentItem.SubID,
              (object) pmProject.DefaultSalesSubID,
              (object) task.DefaultSalesSubID
            }, new System.Type[3]
            {
              typeof (PMBillingRule.subID),
              typeof (PMProject.defaultSalesSubID),
              typeof (PMTask.defaultSalesSubID)
            });
        }
        billingDataList.Add(new PMBillEngine.BillingData((PMProformaLine) tran, rule, (PMTran) null, subCD, (string) null, (Guid[]) null));
        recurrentItem.LastBilledDate = new DateTime?(billingDate);
        ((PXSelectBase<PMRecurringItem>) this.RecurringItems).Update(recurrentItem);
      }
    }
    return billingDataList;
  }

  public virtual List<PMBillEngine.BillingData> BillTask(
    PMProject project,
    PX.Objects.AR.Customer customer,
    PMTask task,
    PMBillingRule rule,
    DateTime billingDate,
    Dictionary<int, Decimal> availableQty,
    Dictionary<int, PMRecurringItem> billingItems)
  {
    return this.BillTask(project, customer, task, rule, billingDate, availableQty, billingItems, false);
  }

  public virtual List<PMBillEngine.BillingData> BillTask(
    PMProject project,
    PX.Objects.AR.Customer customer,
    PMTask task,
    PMBillingRule rule,
    DateTime billingDate,
    Dictionary<int, Decimal> availableQty,
    Dictionary<int, PMRecurringItem> billingItems,
    bool groupConditionsIgnore)
  {
    List<PMBillEngine.BillingData> billingDataList = new List<PMBillEngine.BillingData>();
    int num1 = 1;
    PMAccountGroup pmAccountGroup = PMAccountGroup.PK.Find((PXGraph) this, rule.AccountGroupID);
    if (pmAccountGroup == null)
      throw new PXException("The {0} billing rule has the {1} account group that has not been found in the system.", new object[2]
      {
        (object) rule.BillingID,
        (object) rule.AccountGroupID
      });
    if (pmAccountGroup.Type == "L" || pmAccountGroup.Type == "I")
      num1 = -1;
    List<PMTran> pmTranList = this.SelectBillingBase(task.ProjectID, task.TaskID, rule.AccountGroupID, rule.IncludeNonBillable.GetValueOrDefault());
    this.Transform(project, pmTranList, rule, task);
    PXSelect<BAccountR, Where<BAccountR.bAccountID, Equal<Required<BAccountR.bAccountID>>>> pxSelect = new PXSelect<BAccountR, Where<BAccountR.bAccountID, Equal<Required<BAccountR.bAccountID>>>>((PXGraph) this);
    if (rule.FullDetail | groupConditionsIgnore)
    {
      foreach (PMTran pmTran in pmTranList)
      {
        bool? nullable1 = rule.IncludeZeroAmountAndQty;
        bool flag = false;
        Decimal? nullable2;
        if (nullable1.GetValueOrDefault() == flag & nullable1.HasValue)
        {
          nullable2 = pmTran.InvoicedQty;
          Decimal num2 = 0M;
          if (nullable2.GetValueOrDefault() == num2 & nullable2.HasValue)
          {
            nullable2 = pmTran.ProjectCuryInvoicedAmount;
            Decimal num3 = 0M;
            if (nullable2.GetValueOrDefault() == num3 & nullable2.HasValue)
              continue;
          }
        }
        PMProformaTransactLine proformaTransactLine1 = new PMProformaTransactLine();
        proformaTransactLine1.Type = "T";
        proformaTransactLine1.BranchID = this.CalculateTargetBranchID(rule, project, task, pmTran, customer, pmTran.BranchID);
        proformaTransactLine1.InventoryID = pmTran.InventoryID;
        proformaTransactLine1.Description = pmTran.InvoicedDescription;
        proformaTransactLine1.UOM = pmTran.UOM;
        PMProformaTransactLine proformaTransactLine2 = proformaTransactLine1;
        nullable2 = pmTran.InvoicedQty;
        Decimal num4 = (Decimal) num1;
        Decimal? nullable3;
        Decimal? nullable4;
        if (!nullable2.HasValue)
        {
          nullable3 = new Decimal?();
          nullable4 = nullable3;
        }
        else
          nullable4 = new Decimal?(nullable2.GetValueOrDefault() * num4);
        proformaTransactLine2.BillableQty = nullable4;
        proformaTransactLine1.Qty = proformaTransactLine1.BillableQty;
        PMProformaTransactLine proformaTransactLine3 = proformaTransactLine1;
        nullable2 = project.RetainagePct;
        Decimal? nullable5 = new Decimal?(nullable2.GetValueOrDefault());
        proformaTransactLine3.RetainagePct = nullable5;
        proformaTransactLine1.ProjectID = task.ProjectID;
        proformaTransactLine1.TaskID = task.TaskID;
        proformaTransactLine1.Date = pmTran.Date;
        proformaTransactLine1.CostCodeID = pmTran.CostCodeID;
        proformaTransactLine1.ResourceID = pmTran.ResourceID;
        PMProformaTransactLine proformaTransactLine4 = proformaTransactLine1;
        nullable1 = pmTran.Billable;
        Decimal? nullable6;
        if (!nullable1.GetValueOrDefault())
        {
          nullable6 = new Decimal?(0M);
        }
        else
        {
          nullable2 = pmTran.ProjectCuryInvoicedAmount;
          Decimal num5 = (Decimal) num1;
          if (!nullable2.HasValue)
          {
            nullable3 = new Decimal?();
            nullable6 = nullable3;
          }
          else
            nullable6 = new Decimal?(nullable2.GetValueOrDefault() * num5);
        }
        proformaTransactLine4.CuryBillableAmount = nullable6;
        proformaTransactLine1.CuryAmount = proformaTransactLine1.CuryBillableAmount;
        PMProformaTransactLine proformaTransactLine5 = proformaTransactLine1;
        nullable2 = proformaTransactLine1.Qty;
        Decimal num6 = 0M;
        Decimal? nullable7;
        Decimal? nullable8;
        if (nullable2.GetValueOrDefault() == num6 & nullable2.HasValue)
        {
          nullable8 = new Decimal?(0M);
        }
        else
        {
          nullable2 = proformaTransactLine1.CuryAmount;
          nullable3 = proformaTransactLine1.Qty;
          if (!(nullable2.HasValue & nullable3.HasValue))
          {
            nullable7 = new Decimal?();
            nullable8 = nullable7;
          }
          else
            nullable8 = new Decimal?(nullable2.GetValueOrDefault() / nullable3.GetValueOrDefault());
        }
        proformaTransactLine5.CuryUnitPrice = nullable8;
        proformaTransactLine1.OrigAccountGroupID = pmAccountGroup.GroupID;
        int? nullable9 = pmTran.BAccountID;
        if (nullable9.HasValue)
        {
          BAccountR baccountR = PXResultset<BAccountR>.op_Implicit(((PXSelectBase<BAccountR>) pxSelect).Select(new object[1]
          {
            (object) pmTran.BAccountID
          }));
          if (baccountR != null && (baccountR.Type == "VE" || baccountR.Type == "EP" || baccountR.Type == "VC"))
            proformaTransactLine1.VendorID = pmTran.BAccountID;
        }
        proformaTransactLine1.AccountID = this.CalculateTargetSalesAccountID(rule, project, task, pmTran, (PMProformaLine) proformaTransactLine1, customer);
        string salesSubaccountCd = this.CalculateTargetSalesSubaccountCD(rule, project, task, pmTran.SubID, pmTran.ResourceID, proformaTransactLine1.BranchID, proformaTransactLine1.InventoryID, customer);
        nullable1 = rule.CopyNotes;
        string note = nullable1.GetValueOrDefault() ? PXNoteAttribute.GetNote(((PXSelectBase) this.Transactions).Cache, (object) pmTran) : (string) null;
        nullable1 = rule.CopyNotes;
        Guid[] source = nullable1.GetValueOrDefault() ? PXNoteAttribute.GetFileNotes(((PXSelectBase) this.Transactions).Cache, (object) pmTran) : new Guid[0];
        billingDataList.Add(new PMBillEngine.BillingData((PMProformaLine) proformaTransactLine1, rule, pmTran, salesSubaccountCd, note, ((IEnumerable<Guid>) source).ToArray<Guid>()));
        nullable9 = pmTran.InventoryID;
        if (nullable9.HasValue)
        {
          Dictionary<int, PMRecurringItem> dictionary1 = billingItems;
          nullable9 = pmTran.InventoryID;
          int key1 = nullable9.Value;
          if (dictionary1.ContainsKey(key1))
          {
            Dictionary<int, Decimal> dictionary2 = availableQty;
            nullable9 = pmTran.InventoryID;
            int key2 = nullable9.Value;
            if (dictionary2.ContainsKey(key2))
            {
              Dictionary<int, Decimal> dictionary3 = availableQty;
              nullable9 = pmTran.InventoryID;
              int key3 = nullable9.Value;
              Decimal num7 = dictionary3[key3];
              nullable3 = pmTran.InvoicedQty;
              Decimal num8 = num7;
              if (nullable3.GetValueOrDefault() <= num8 & nullable3.HasValue)
              {
                using (new PXLocaleScope(customer.LocaleName))
                  proformaTransactLine1.Description = $"{PXMessages.LocalizeNoPrefix("Included Usage")} {pmTran.InvoicedDescription}";
                availableQty[pmTran.InventoryID.Value] -= proformaTransactLine1.Qty.Value;
                proformaTransactLine1.CuryUnitPrice = new Decimal?(0M);
                proformaTransactLine1.CuryAmount = new Decimal?(0M);
                proformaTransactLine1.CuryBillableAmount = new Decimal?(0M);
                proformaTransactLine1.UnitPrice = new Decimal?(0M);
                proformaTransactLine1.Amount = new Decimal?(0M);
                proformaTransactLine1.BillableAmount = new Decimal?(0M);
                proformaTransactLine1.Option = "C";
              }
              else
              {
                using (new PXLocaleScope(customer.LocaleName))
                  proformaTransactLine1.Description = $"{PXMessages.LocalizeNoPrefix("Overused")} {pmTran.InvoicedDescription}";
                nullable3 = proformaTransactLine1.Qty;
                Decimal num9 = 0M;
                if (!(nullable3.GetValueOrDefault() == num9 & nullable3.HasValue))
                {
                  PMProformaTransactLine proformaTransactLine6 = proformaTransactLine1;
                  nullable7 = pmTran.ProjectCuryInvoicedAmount;
                  Decimal? nullable10 = proformaTransactLine1.Qty;
                  Decimal num10 = num7;
                  Decimal? nullable11 = nullable10.HasValue ? new Decimal?(nullable10.GetValueOrDefault() - num10) : new Decimal?();
                  Decimal? nullable12;
                  if (!(nullable7.HasValue & nullable11.HasValue))
                  {
                    nullable10 = new Decimal?();
                    nullable12 = nullable10;
                  }
                  else
                    nullable12 = new Decimal?(nullable7.GetValueOrDefault() * nullable11.GetValueOrDefault());
                  nullable3 = nullable12;
                  Decimal? qty = proformaTransactLine1.Qty;
                  Decimal? nullable13;
                  if (!(nullable3.HasValue & qty.HasValue))
                  {
                    nullable11 = new Decimal?();
                    nullable13 = nullable11;
                  }
                  else
                    nullable13 = new Decimal?(nullable3.GetValueOrDefault() / qty.GetValueOrDefault());
                  proformaTransactLine6.CuryBillableAmount = nullable13;
                }
                PMProformaTransactLine proformaTransactLine7 = proformaTransactLine1;
                Decimal? qty1 = proformaTransactLine1.Qty;
                Decimal num11 = num7;
                Decimal? nullable14;
                if (!qty1.HasValue)
                {
                  nullable3 = new Decimal?();
                  nullable14 = nullable3;
                }
                else
                  nullable14 = new Decimal?(qty1.GetValueOrDefault() - num11);
                proformaTransactLine7.BillableQty = nullable14;
                proformaTransactLine1.CuryAmount = proformaTransactLine1.CuryBillableAmount;
                proformaTransactLine1.Qty = proformaTransactLine1.BillableQty;
                proformaTransactLine1.Option = "C";
                availableQty[pmTran.InventoryID.Value] = 0M;
              }
            }
          }
        }
      }
    }
    else
    {
      foreach (Group breakIntoGroup in this.CreateGrouping(rule).BreakIntoGroups(pmTranList))
      {
        if (breakIntoGroup.List.Count != 0)
        {
          Decimal num12 = 0M;
          Decimal num13 = 0M;
          List<Guid> guidList = new List<Guid>();
          StringBuilder stringBuilder = new StringBuilder();
          Decimal? nullable15;
          foreach (PMTran pmTran in breakIntoGroup.List)
          {
            Decimal num14 = num12;
            nullable15 = pmTran.InvoicedQty;
            Decimal valueOrDefault = nullable15.GetValueOrDefault();
            num12 = num14 + valueOrDefault;
            Decimal num15 = num13;
            bool? nullable16 = pmTran.Billable;
            Decimal num16;
            if (!nullable16.GetValueOrDefault())
            {
              num16 = 0M;
            }
            else
            {
              nullable15 = pmTran.ProjectCuryInvoicedAmount;
              num16 = nullable15.GetValueOrDefault();
            }
            num13 = num15 + num16;
            nullable16 = rule.CopyNotes;
            if (nullable16.GetValueOrDefault())
            {
              guidList.AddRange((IEnumerable<Guid>) PXNoteAttribute.GetFileNotes(((PXSelectBase) this.Transactions).Cache, (object) pmTran));
              string note = PXNoteAttribute.GetNote(((PXSelectBase) this.Transactions).Cache, (object) pmTran);
              if (!string.IsNullOrEmpty(note))
                stringBuilder.AppendLine(note);
            }
          }
          string note1 = stringBuilder.Length > 0 ? stringBuilder.ToString() : (string) null;
          bool? zeroAmountAndQty = rule.IncludeZeroAmountAndQty;
          bool flag = false;
          if (!(zeroAmountAndQty.GetValueOrDefault() == flag & zeroAmountAndQty.HasValue) || !(num12 == 0M) || !(num13 == 0M))
          {
            PMTran tran = new PMTran();
            PMProformaTransactLine proformaTransactLine8 = new PMProformaTransactLine();
            proformaTransactLine8.Type = "T";
            proformaTransactLine8.BranchID = this.CalculateTargetBranchID(rule, project, task, breakIntoGroup.List[0], customer, breakIntoGroup.List[0].BranchID);
            proformaTransactLine8.InventoryID = breakIntoGroup.List[0].InventoryID;
            proformaTransactLine8.Description = breakIntoGroup.List[0].InvoicedDescription;
            proformaTransactLine8.UOM = breakIntoGroup.List[0].UOM;
            proformaTransactLine8.BillableQty = new Decimal?(num12 * (Decimal) num1);
            proformaTransactLine8.Qty = proformaTransactLine8.BillableQty;
            proformaTransactLine8.RetainagePct = project.RetainagePct;
            proformaTransactLine8.ProjectID = task.ProjectID;
            proformaTransactLine8.TaskID = task.TaskID;
            proformaTransactLine8.Date = breakIntoGroup.List[0].Date;
            proformaTransactLine8.CostCodeID = breakIntoGroup.List[0].CostCodeID;
            proformaTransactLine8.ResourceID = breakIntoGroup.List[0].ResourceID;
            proformaTransactLine8.OrigAccountGroupID = pmAccountGroup.GroupID;
            if (!breakIntoGroup.HasMixedAccountID)
              tran.AccountID = breakIntoGroup.List[0].AccountID;
            if (!breakIntoGroup.HasMixedSubID)
              tran.SubID = breakIntoGroup.List[0].SubID;
            proformaTransactLine8.CuryBillableAmount = new Decimal?(num13 * (Decimal) num1);
            proformaTransactLine8.CuryAmount = proformaTransactLine8.CuryBillableAmount;
            PMProformaTransactLine proformaTransactLine9 = proformaTransactLine8;
            nullable15 = proformaTransactLine8.Qty;
            Decimal num17 = 0M;
            Decimal? nullable17;
            if (nullable15.GetValueOrDefault() == num17 & nullable15.HasValue)
            {
              nullable17 = new Decimal?(0M);
            }
            else
            {
              nullable15 = proformaTransactLine8.CuryAmount;
              Decimal? qty = proformaTransactLine8.Qty;
              nullable17 = nullable15.HasValue & qty.HasValue ? new Decimal?(nullable15.GetValueOrDefault() / qty.GetValueOrDefault()) : new Decimal?();
            }
            proformaTransactLine9.CuryUnitPrice = nullable17;
            if (breakIntoGroup.List[0].BAccountID.HasValue)
            {
              BAccountR baccountR = PXResultset<BAccountR>.op_Implicit(((PXSelectBase<BAccountR>) pxSelect).Select(new object[1]
              {
                (object) breakIntoGroup.List[0].BAccountID
              }));
              if (baccountR != null && (baccountR.Type == "VE" || baccountR.Type == "VC"))
                proformaTransactLine8.VendorID = breakIntoGroup.List[0].BAccountID;
            }
            proformaTransactLine8.AccountID = this.CalculateTargetSalesAccountID(rule, project, task, tran, (PMProformaLine) proformaTransactLine8, customer);
            string salesSubaccountCd = this.CalculateTargetSalesSubaccountCD(rule, project, task, tran.SubID, proformaTransactLine8.ResourceID, proformaTransactLine8.BranchID, proformaTransactLine8.InventoryID, customer);
            billingDataList.Add(new PMBillEngine.BillingData((PMProformaLine) proformaTransactLine8, rule, breakIntoGroup.List, salesSubaccountCd, note1, guidList.ToArray()));
            int? nullable18 = breakIntoGroup.List[0].InventoryID;
            if (nullable18.HasValue)
            {
              Dictionary<int, PMRecurringItem> dictionary4 = billingItems;
              nullable18 = breakIntoGroup.List[0].InventoryID;
              int key4 = nullable18.Value;
              if (dictionary4.ContainsKey(key4))
              {
                Dictionary<int, Decimal> dictionary5 = availableQty;
                nullable18 = breakIntoGroup.List[0].InventoryID;
                int key5 = nullable18.Value;
                if (dictionary5.ContainsKey(key5))
                {
                  Dictionary<int, Decimal> dictionary6 = availableQty;
                  nullable18 = breakIntoGroup.List[0].InventoryID;
                  int key6 = nullable18.Value;
                  Decimal num18 = dictionary6[key6];
                  Decimal? invoicedQty = breakIntoGroup.List[0].InvoicedQty;
                  Decimal num19 = num18;
                  if (invoicedQty.GetValueOrDefault() <= num19 & invoicedQty.HasValue)
                  {
                    using (new PXLocaleScope(customer.LocaleName))
                      proformaTransactLine8.Description = $"{PXMessages.LocalizeNoPrefix("Included Usage")} {breakIntoGroup.List[0].InvoicedDescription}";
                    availableQty[breakIntoGroup.List[0].InventoryID.Value] -= proformaTransactLine8.Qty.Value;
                    proformaTransactLine8.CuryUnitPrice = new Decimal?(0M);
                    proformaTransactLine8.CuryAmount = new Decimal?(0M);
                    proformaTransactLine8.CuryBillableAmount = new Decimal?(0M);
                    proformaTransactLine8.UnitPrice = new Decimal?(0M);
                    proformaTransactLine8.Amount = new Decimal?(0M);
                    proformaTransactLine8.BillableAmount = new Decimal?(0M);
                    proformaTransactLine8.Option = "C";
                  }
                  else
                  {
                    using (new PXLocaleScope(customer.LocaleName))
                      proformaTransactLine8.Description = $"{PXMessages.LocalizeNoPrefix("Overused")} {breakIntoGroup.List[0].InvoicedDescription}";
                    Decimal? nullable19 = proformaTransactLine8.Qty;
                    Decimal num20 = 0M;
                    if (!(nullable19.GetValueOrDefault() == num20 & nullable19.HasValue))
                    {
                      PMProformaTransactLine proformaTransactLine10 = proformaTransactLine8;
                      Decimal? curyInvoicedAmount = breakIntoGroup.List[0].ProjectCuryInvoicedAmount;
                      Decimal? nullable20 = proformaTransactLine8.Qty;
                      Decimal num21 = num18;
                      Decimal? nullable21 = nullable20.HasValue ? new Decimal?(nullable20.GetValueOrDefault() - num21) : new Decimal?();
                      Decimal? nullable22;
                      if (!(curyInvoicedAmount.HasValue & nullable21.HasValue))
                      {
                        nullable20 = new Decimal?();
                        nullable22 = nullable20;
                      }
                      else
                        nullable22 = new Decimal?(curyInvoicedAmount.GetValueOrDefault() * nullable21.GetValueOrDefault());
                      nullable19 = nullable22;
                      Decimal? qty = proformaTransactLine8.Qty;
                      Decimal? nullable23;
                      if (!(nullable19.HasValue & qty.HasValue))
                      {
                        nullable21 = new Decimal?();
                        nullable23 = nullable21;
                      }
                      else
                        nullable23 = new Decimal?(nullable19.GetValueOrDefault() / qty.GetValueOrDefault());
                      proformaTransactLine10.CuryBillableAmount = nullable23;
                    }
                    proformaTransactLine8.CuryAmount = proformaTransactLine8.CuryBillableAmount;
                    PMProformaTransactLine proformaTransactLine11 = proformaTransactLine8;
                    Decimal? qty2 = proformaTransactLine8.Qty;
                    Decimal num22 = num18;
                    Decimal? nullable24;
                    if (!qty2.HasValue)
                    {
                      nullable19 = new Decimal?();
                      nullable24 = nullable19;
                    }
                    else
                      nullable24 = new Decimal?(qty2.GetValueOrDefault() - num22);
                    proformaTransactLine11.BillableQty = nullable24;
                    proformaTransactLine8.Qty = proformaTransactLine8.BillableQty;
                    proformaTransactLine8.Option = "C";
                    Dictionary<int, Decimal> dictionary7 = availableQty;
                    nullable18 = breakIntoGroup.List[0].InventoryID;
                    int key7 = nullable18.Value;
                    dictionary7[key7] = 0M;
                  }
                }
              }
            }
            if (breakIntoGroup.HasMixedInventory)
              proformaTransactLine8.InventoryID = new int?(PMInventorySelectorAttribute.EmptyInventoryID);
            if (breakIntoGroup.HasMixedUOM)
            {
              proformaTransactLine8.Qty = new Decimal?(0M);
              proformaTransactLine8.BillableQty = new Decimal?(0M);
              proformaTransactLine8.UOM = (string) null;
              proformaTransactLine8.CuryUnitPrice = new Decimal?(0M);
              proformaTransactLine8.UnitPrice = new Decimal?(0M);
            }
            if (breakIntoGroup.HasMixedBAccount)
            {
              PMProformaTransactLine proformaTransactLine12 = proformaTransactLine8;
              nullable18 = new int?();
              int? nullable25 = nullable18;
              proformaTransactLine12.VendorID = nullable25;
            }
          }
        }
      }
    }
    return billingDataList;
  }

  private PMCostCode SelectCostCode(PMRevenueBudget line)
  {
    return PXResultset<PMCostCode>.op_Implicit(PXSelectBase<PMCostCode, PXViewOf<PMCostCode>.BasedOn<SelectFromBase<PMCostCode, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PMCostCode.costCodeID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
    {
      (object) line.CostCodeID
    }));
  }

  private PX.Objects.IN.InventoryItem SelectInventoryItem(PMRevenueBudget line)
  {
    return PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXViewOf<PX.Objects.IN.InventoryItem>.BasedOn<SelectFromBase<PX.Objects.IN.InventoryItem, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.IN.InventoryItem.inventoryID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
    {
      (object) line.InventoryID
    }));
  }

  public virtual List<PMBillEngine.BillingData> BillFixPriceTask(
    PMProject project,
    PX.Objects.AR.Customer customer,
    PMTask task,
    PMBillingRule rule,
    DateTime billingDate)
  {
    if (((PXSelectBase<PMSetup>) this.Setup).Current.AutoCompleteRevenueBudget.GetValueOrDefault())
      this.InitializeRatios(task.ProjectID);
    List<PMBillEngine.BillingData> billingDataList = new List<PMBillEngine.BillingData>();
    if (!task.TaskID.HasValue || !this.revenueBudgetLines.ContainsKey(task.TaskID.Value))
      return billingDataList;
    List<PXResult<PMRevenueBudget, PMAccountGroup>> revenueBudgetLine = this.revenueBudgetLines[task.TaskID.Value];
    PMBillEngine.ValidateProgressBillingBase(revenueBudgetLine.Select<PXResult<PMRevenueBudget, PMAccountGroup>, PMBillEngine.InvalidBillingBaseException.BillingComponent>((Func<PXResult<PMRevenueBudget, PMAccountGroup>, PMBillEngine.InvalidBillingBaseException.BillingComponent>) (r => new PMBillEngine.InvalidBillingBaseException.BillingComponent(project.BudgetLevel, ((PXResult) r).GetItem<PMRevenueBudget>().ProgressBillingBase, task.TaskCD, ((PXResult) r).GetItem<PMAccountGroup>().GroupCD, (Func<PX.Objects.IN.InventoryItem>) (() => this.SelectInventoryItem(PXResult<PMRevenueBudget, PMAccountGroup>.op_Implicit(r))), (Func<PMCostCode>) (() => this.SelectCostCode(PXResult<PMRevenueBudget, PMAccountGroup>.op_Implicit(r)))))));
    foreach (PXResult<PMRevenueBudget, PMAccountGroup> pxResult in revenueBudgetLine)
    {
      PMRevenueBudget pmRevenueBudget = PXResult<PMRevenueBudget, PMAccountGroup>.op_Implicit(pxResult);
      Decimal num1 = 0M;
      bool? nullable1 = ((PXSelectBase<PMSetup>) this.Setup).Current.AutoCompleteRevenueBudget;
      int? nullable2;
      Decimal? nullable3;
      if (nullable1.GetValueOrDefault())
      {
        // ISSUE: variable of a boxed type
        __Boxed<int?> projectTaskId = (ValueType) pmRevenueBudget.ProjectTaskID;
        nullable2 = pmRevenueBudget.InventoryID;
        // ISSUE: variable of a boxed type
        __Boxed<int> local = (ValueType) (nullable2 ?? PMInventorySelectorAttribute.EmptyInventoryID);
        Decimal? nullable4;
        if (this.ratios.TryGetValue($"{projectTaskId}.{local}", out nullable4) && nullable4.HasValue)
        {
          nullable3 = pmRevenueBudget.CuryRevisedAmount;
          Decimal num2 = nullable3.GetValueOrDefault() * nullable4.GetValueOrDefault() / 100M;
          nullable3 = pmRevenueBudget.CuryInvoicedAmount;
          Decimal valueOrDefault = nullable3.GetValueOrDefault();
          num1 = Math.Max(0M, num2 - valueOrDefault);
        }
      }
      else
      {
        nullable3 = pmRevenueBudget.CuryAmountToInvoice;
        num1 = nullable3.GetValueOrDefault();
      }
      nullable1 = rule.IncludeZeroAmount;
      bool flag = false;
      if (!(nullable1.GetValueOrDefault() == flag & nullable1.HasValue) || !(num1 == 0M))
      {
        string description = (string) null;
        if (!string.IsNullOrEmpty(rule.DescriptionFormula))
        {
          PMTran tran = new PMTran();
          tran.ProjectID = pmRevenueBudget.ProjectID;
          tran.TaskID = pmRevenueBudget.ProjectTaskID;
          tran.Description = pmRevenueBudget.Description;
          tran.AccountGroupID = pmRevenueBudget.AccountGroupID;
          tran.InventoryID = pmRevenueBudget.InventoryID;
          tran.CostCodeID = pmRevenueBudget.CostCodeID;
          tran.UOM = pmRevenueBudget.UOM;
          tran.TranCuryID = project.CuryID;
          tran.ProjectCuryID = project.CuryID;
          tran.TranCuryAmount = new Decimal?(num1);
          tran.ProjectCuryAmount = new Decimal?(num1);
          PMAllocator.PMDataNavigator navigator = new PMAllocator.PMDataNavigator((IRateTable) this, new List<PMTran>((IEnumerable<PMTran>) new PMTran[1]
          {
            tran
          }));
          this.CalculateFormulas(project, navigator, rule, tran, out Decimal? _, out Decimal? _, out description);
        }
        else
          description = pmRevenueBudget.Description;
        PMProformaProgressLine proformaProgressLine1 = new PMProformaProgressLine();
        proformaProgressLine1.Type = "P";
        proformaProgressLine1.Description = description;
        proformaProgressLine1.BillableQty = new Decimal?(0M);
        proformaProgressLine1.Qty = pmRevenueBudget.QtyToInvoice;
        proformaProgressLine1.UOM = pmRevenueBudget.UOM;
        proformaProgressLine1.ProjectID = pmRevenueBudget.ProjectID;
        proformaProgressLine1.TaskID = pmRevenueBudget.ProjectTaskID;
        proformaProgressLine1.AccountGroupID = pmRevenueBudget.AccountGroupID;
        proformaProgressLine1.CostCodeID = pmRevenueBudget.CostCodeID;
        proformaProgressLine1.InventoryID = pmRevenueBudget.InventoryID;
        proformaProgressLine1.TaxCategoryID = pmRevenueBudget.TaxCategoryID;
        proformaProgressLine1.AccountID = this.CalculateTargetSalesAccountID(rule, project, task, (PMTran) null, (PMProformaLine) proformaProgressLine1, customer);
        PMProformaProgressLine proformaProgressLine2 = proformaProgressLine1;
        PMBillingRule rule1 = rule;
        PMProject project1 = project;
        PMTask task1 = task;
        PX.Objects.AR.Customer customer1 = customer;
        nullable2 = new int?();
        int? defaultvalue = nullable2;
        int? targetBranchId = this.CalculateTargetBranchID(rule1, project1, task1, (PMTran) null, customer1, defaultvalue);
        proformaProgressLine2.BranchID = targetBranchId;
        proformaProgressLine1.RetainagePct = pmRevenueBudget.RetainagePct;
        proformaProgressLine1.CuryBillableAmount = new Decimal?(num1);
        proformaProgressLine1.CuryAmount = proformaProgressLine1.CuryBillableAmount;
        proformaProgressLine1.CuryUnitPrice = pmRevenueBudget.CuryUnitRate;
        proformaProgressLine1.ProgressBillingBase = pmRevenueBudget.ProgressBillingBase;
        PMBillingRule rule2 = rule;
        PMProject project2 = project;
        PMTask task2 = task;
        nullable2 = new int?();
        int? subIDTran = nullable2;
        nullable2 = new int?();
        int? employeeID = nullable2;
        nullable2 = new int?();
        int? branchID = nullable2;
        int? inventoryId = pmRevenueBudget.InventoryID;
        PX.Objects.AR.Customer customer2 = customer;
        string salesSubaccountCd = this.CalculateTargetSalesSubaccountCD(rule2, project2, task2, subIDTran, employeeID, branchID, inventoryId, customer2);
        nullable1 = rule.CopyNotes;
        string note = nullable1.GetValueOrDefault() ? PXNoteAttribute.GetNote(((PXGraph) this).Caches[typeof (PMRevenueBudget)], (object) task) : (string) null;
        nullable1 = rule.CopyNotes;
        Guid[] files = nullable1.GetValueOrDefault() ? PXNoteAttribute.GetFileNotes(((PXGraph) this).Caches[typeof (PMRevenueBudget)], (object) task) : new Guid[0];
        PMBillEngine.BillingData billingData = new PMBillEngine.BillingData((PMProformaLine) proformaProgressLine1, rule, (PMTran) null, salesSubaccountCd, note, files)
        {
          RevenueBudget = pmRevenueBudget
        };
        billingDataList.Add(billingData);
      }
    }
    return billingDataList;
  }

  public virtual List<PMBillEngine.BillingData> BillPrepayment(
    PMProject project,
    PX.Objects.AR.Customer customer,
    PMTask task,
    PMBillingRule rule,
    DateTime billingDate)
  {
    List<PMBillEngine.BillingData> billingDataList = new List<PMBillEngine.BillingData>();
    if (!task.TaskID.HasValue || !this.revenueBudgetLines.ContainsKey(task.TaskID.Value))
      return billingDataList;
    IEnumerable<PXResult<PMRevenueBudget, PMAccountGroup>> source = this.revenueBudgetLines[task.TaskID.Value].Where<PXResult<PMRevenueBudget, PMAccountGroup>>((Func<PXResult<PMRevenueBudget, PMAccountGroup>, bool>) (x =>
    {
      Decimal? prepaymentAmount = PXResult<PMRevenueBudget, PMAccountGroup>.op_Implicit(x).CuryPrepaymentAmount;
      Decimal? prepaymentInvoiced = PXResult<PMRevenueBudget, PMAccountGroup>.op_Implicit(x).CuryPrepaymentInvoiced;
      return prepaymentAmount.GetValueOrDefault() > prepaymentInvoiced.GetValueOrDefault() & prepaymentAmount.HasValue & prepaymentInvoiced.HasValue;
    }));
    if (rule.Type == "B")
      PMBillEngine.ValidateProgressBillingBase(source.Select<PXResult<PMRevenueBudget, PMAccountGroup>, PMBillEngine.InvalidBillingBaseException.BillingComponent>((Func<PXResult<PMRevenueBudget, PMAccountGroup>, PMBillEngine.InvalidBillingBaseException.BillingComponent>) (r => new PMBillEngine.InvalidBillingBaseException.BillingComponent(project.BudgetLevel, ((PXResult) r).GetItem<PMRevenueBudget>().ProgressBillingBase, task.TaskCD, ((PXResult) r).GetItem<PMAccountGroup>().GroupCD, (Func<PX.Objects.IN.InventoryItem>) (() => this.SelectInventoryItem(PXResult<PMRevenueBudget, PMAccountGroup>.op_Implicit(r))), (Func<PMCostCode>) (() => this.SelectCostCode(PXResult<PMRevenueBudget, PMAccountGroup>.op_Implicit(r)))))));
    foreach (PXResult<PMRevenueBudget, PMAccountGroup> pxResult in source)
    {
      PMRevenueBudget pmRevenueBudget = PXResult<PMRevenueBudget, PMAccountGroup>.op_Implicit(pxResult);
      PXResult<PMRevenueBudget, PMAccountGroup>.op_Implicit(pxResult);
      PMProformaLine tran;
      if (rule.Type == "T")
      {
        tran = (PMProformaLine) new PMProformaTransactLine();
        tran.Type = "T";
      }
      else
      {
        tran = (PMProformaLine) new PMProformaProgressLine();
        tran.Type = "P";
      }
      tran.IsPrepayment = new bool?(true);
      tran.Description = PXMessages.LocalizeNoPrefix("Prepayment");
      PMProformaLine pmProformaLine = tran;
      Decimal? nullable1 = pmRevenueBudget.CuryPrepaymentAmount;
      Decimal num1 = nullable1.Value;
      nullable1 = pmRevenueBudget.CuryPrepaymentInvoiced;
      Decimal num2 = nullable1.Value;
      Decimal? nullable2 = new Decimal?(num1 - num2);
      pmProformaLine.CuryBillableAmount = nullable2;
      tran.BillableQty = new Decimal?(0M);
      tran.CuryAmount = tran.CuryBillableAmount;
      tran.Qty = pmRevenueBudget.QtyToInvoice;
      tran.UOM = pmRevenueBudget.UOM;
      tran.ProjectID = pmRevenueBudget.ProjectID;
      tran.TaskID = pmRevenueBudget.ProjectTaskID;
      tran.AccountGroupID = pmRevenueBudget.AccountGroupID;
      tran.CostCodeID = pmRevenueBudget.CostCodeID;
      tran.InventoryID = pmRevenueBudget.InventoryID;
      tran.TaxCategoryID = pmRevenueBudget.TaxCategoryID;
      tran.DefCode = project.PrepaymentDefCode;
      tran.BranchID = this.CalculateTargetBranchID(rule, project, task, (PMTran) null, customer, new int?());
      tran.ProgressBillingBase = pmRevenueBudget.ProgressBillingBase;
      string salesSubaccountCd = this.CalculateTargetSalesSubaccountCD(rule, project, task, new int?(), new int?(), new int?(), pmRevenueBudget.InventoryID, customer);
      bool? copyNotes = rule.CopyNotes;
      string note = copyNotes.GetValueOrDefault() ? PXNoteAttribute.GetNote(((PXGraph) this).Caches[typeof (PMRevenueBudget)], (object) task) : (string) null;
      copyNotes = rule.CopyNotes;
      Guid[] files = copyNotes.GetValueOrDefault() ? PXNoteAttribute.GetFileNotes(((PXGraph) this).Caches[typeof (PMRevenueBudget)], (object) task) : new Guid[0];
      billingDataList.Add(new PMBillEngine.BillingData(tran, rule, (PMTran) null, salesSubaccountCd, note, files)
      {
        RevenueBudget = pmRevenueBudget
      });
    }
    return billingDataList;
  }

  private static void ValidateProgressBillingBase(
    IEnumerable<PMBillEngine.InvalidBillingBaseException.BillingComponent> lines)
  {
    List<PMBillEngine.InvalidBillingBaseException.BillingComponent> list = lines.Where<PMBillEngine.InvalidBillingBaseException.BillingComponent>((Func<PMBillEngine.InvalidBillingBaseException.BillingComponent, bool>) (l => EnumerableExtensions.IsNotIn<string>(l.ProgressBillingBase, "A", "Q"))).ToList<PMBillEngine.InvalidBillingBaseException.BillingComponent>();
    if (list.Count > 0)
      throw new PMBillEngine.InvalidBillingBaseException((IEnumerable<PMBillEngine.InvalidBillingBaseException.BillingComponent>) list);
  }

  public virtual int? CalculateTargetBranchID(
    PMBillingRule rule,
    PMProject project,
    PMTask task,
    PMTran tran,
    PX.Objects.AR.Customer customer,
    int? defaultvalue)
  {
    return PMBillEngine.CalculateTargetBranchID((PXGraph) this, rule, project, task, tran, customer, defaultvalue);
  }

  public static int? CalculateTargetBranchID(
    PXGraph graph,
    PMBillingRule rule,
    PMProject project,
    PMTask task,
    PMTran tran,
    PX.Objects.AR.Customer customer,
    int? defaultvalue)
  {
    int? targetBranchId = defaultvalue;
    int? nullable;
    if (rule.BranchSource == "P")
    {
      nullable = project.DefaultBranchID;
      if (nullable.HasValue)
      {
        targetBranchId = project.DefaultBranchID;
        goto label_18;
      }
    }
    if (rule.BranchSource == "T")
    {
      nullable = task.DefaultBranchID;
      if (nullable.HasValue)
      {
        targetBranchId = task.DefaultBranchID;
        goto label_18;
      }
    }
    if (rule.BranchSource == "B")
    {
      nullable = rule.TargetBranchID;
      if (nullable.HasValue)
      {
        targetBranchId = rule.TargetBranchID;
        goto label_18;
      }
    }
    if (rule.BranchSource == "E" && tran != null)
    {
      nullable = tran.ResourceID;
      if (nullable.HasValue)
      {
        PX.Objects.EP.EPEmployee epEmployee = PXResultset<PX.Objects.EP.EPEmployee>.op_Implicit(PXSelectBase<PX.Objects.EP.EPEmployee, PXSelect<PX.Objects.EP.EPEmployee, Where<PX.Objects.EP.EPEmployee.bAccountID, Equal<Required<PX.Objects.EP.EPEmployee.bAccountID>>>>.Config>.Select(graph, new object[1]
        {
          (object) tran.ResourceID
        }));
        if (epEmployee != null)
        {
          PX.Objects.GL.Branch branch = PXResultset<PX.Objects.GL.Branch>.op_Implicit(PXSelectBase<PX.Objects.GL.Branch, PXSelect<PX.Objects.GL.Branch, Where<PX.Objects.GL.Branch.bAccountID, Equal<Required<PX.Objects.EP.EPEmployee.parentBAccountID>>>>.Config>.Select(graph, new object[1]
          {
            (object) epEmployee.ParentBAccountID
          }));
          if (branch != null)
          {
            targetBranchId = branch.BranchID;
            goto label_18;
          }
          goto label_18;
        }
        goto label_18;
      }
    }
    if (rule.BranchSource == "C")
    {
      PX.Objects.CR.Location location = PXResultset<PX.Objects.CR.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Location, PXSelect<PX.Objects.CR.Location, Where<PX.Objects.CR.Location.locationID, Equal<Required<PX.Objects.CR.Location.locationID>>>>.Config>.Select(graph, new object[1]
      {
        (object) customer.DefLocationID
      }));
      if (location != null)
      {
        nullable = location.CBranchID;
        if (nullable.HasValue)
          targetBranchId = location.CBranchID;
      }
    }
label_18:
    return targetBranchId;
  }

  public virtual int? CalculateTargetSalesAccountID(
    PMBillingRule rule,
    PMProject project,
    PMTask task,
    PMTran tran,
    PMProformaLine line,
    PX.Objects.AR.Customer customer)
  {
    return PMBillEngine.CalculateTargetSalesAccountID((PXGraph) this, rule, project, task, tran, line, customer);
  }

  public static int? CalculateTargetSalesAccountID(
    PXGraph graph,
    PMBillingRule rule,
    PMProject project,
    PMTask task,
    PMTran tran,
    PMProformaLine line,
    PX.Objects.AR.Customer customer)
  {
    int? targetSalesAccountId = new int?();
    if (rule.AccountSource == "B")
    {
      if (rule.AccountID.HasValue)
        targetSalesAccountId = rule.AccountID;
      else
        throw new PXException("The {0} billing rule has been configured to use the sales account from the billing rule, but the account has not been specified for the {0} billing rule.", new object[1]
        {
          (object) rule.BillingID
        });
    }
    else if (rule.AccountSource == "P")
    {
      if (project.DefaultSalesAccountID.HasValue)
        targetSalesAccountId = project.DefaultSalesAccountID;
      else
        throw new PXException("The {0} billing rule has been configured to use the sales account from the project, but the default sales account has not been specified for the {1} project.", new object[2]
        {
          (object) rule.BillingID,
          (object) project.ContractCD
        });
    }
    else if (rule.AccountSource == "T")
    {
      if (task.DefaultSalesAccountID.HasValue)
        targetSalesAccountId = task.DefaultSalesAccountID;
      else
        throw new PXException("The {0} billing rule has been configured to get its account from the task, but the default sales account has not been configured for the {1} task of the {2} project.", new object[3]
        {
          (object) rule.BillingID,
          (object) task.TaskCD,
          (object) project.ContractCD
        });
    }
    else if (rule.AccountSource == "I")
    {
      PX.Objects.IN.InventoryItem inventoryItem = PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<PX.Objects.IN.InventoryItem.inventoryID>>>>.Config>.Select(graph, new object[1]
      {
        (object) line.InventoryID
      }));
      if (inventoryItem != null && inventoryItem.ItemStatus != "XX")
      {
        if (inventoryItem.SalesAcctID.HasValue)
          targetSalesAccountId = inventoryItem.SalesAcctID;
        else
          throw new PXException("The {0} billing rule has been configured to use the sales account from the inventory item, but the sales account has not been specified for the {1} inventory item.", new object[2]
          {
            (object) rule.BillingID,
            (object) inventoryItem.InventoryCD
          });
      }
      else if (project.DefaultSalesAccountID.HasValue)
        targetSalesAccountId = project.DefaultSalesAccountID;
      else
        throw new PXException("The {0} billing rule has been configured to use the sales account from the inventory item. In case the empty item code is specified for a project budget line, the default account of the project is used instead of the sales account of the inventory item, but no default account has been specified for the {1} project.", new object[2]
        {
          (object) rule.BillingID,
          (object) project.ContractCD
        });
    }
    else if (rule.AccountSource == "C" && customer != null)
    {
      PX.Objects.CR.Location location = PXResultset<PX.Objects.CR.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Location, PXSelect<PX.Objects.CR.Location, Where<PX.Objects.CR.Location.bAccountID, Equal<Required<PX.Objects.CR.Location.bAccountID>>, And<PX.Objects.CR.Location.locationID, Equal<Required<PX.Objects.CR.Location.locationID>>>>>.Config>.Select(graph, new object[2]
      {
        (object) customer.BAccountID,
        (object) customer.DefLocationID
      }));
      if (location != null)
      {
        if (location.CSalesAcctID.HasValue)
          targetSalesAccountId = location.CSalesAcctID;
        else
          throw new PXException("The {0} billing rule has been configured to use the sales account from the customer, but the sales account has not been specified for the {1} customer.", new object[2]
          {
            (object) rule.BillingID,
            (object) customer.AcctCD
          });
      }
    }
    else if (rule.AccountSource == "E")
    {
      PX.Objects.EP.EPEmployee epEmployee = PX.Objects.EP.EPEmployee.PK.Find(graph, line.ResourceID);
      if (epEmployee == null)
        throw new PXException("The {0} billing step of the {1} billing rule has failed to process the {2} project transaction. The source of the sales account in the billing step is {3}, but it has not been specified in at least one line of the project transactions to be billed.", new object[4]
        {
          (object) rule.StepID,
          (object) rule.BillingID,
          (object) tran.RefNbr,
          (object) Messages.GetLocal("Employee")
        });
      if (!epEmployee.SalesAcctID.HasValue)
        throw new PXException("The {0} billing rule has been configured to use the sales account from the employee, but the sales account has not been specified for the {1} employee.", new object[2]
        {
          (object) rule.BillingID,
          (object) epEmployee.AcctCD
        });
      targetSalesAccountId = epEmployee.SalesAcctID;
    }
    else if (rule.AccountSource == "A")
    {
      PMAccountGroup pmAccountGroup = PMAccountGroup.PK.Find(graph, line.AccountGroupID);
      if (pmAccountGroup != null)
      {
        if (PMBillEngine.GetLineAccount(graph, pmAccountGroup.AccountID) != null)
          targetSalesAccountId = pmAccountGroup.AccountID;
        else
          throw new PXException("The {0} billing rule has been configured to use the sales account from the account group, but the default account has not been specified for the {1} account group.", new object[2]
          {
            (object) rule.BillingID,
            (object) pmAccountGroup.GroupCD
          });
      }
    }
    else if (rule.AccountSource == "N" && tran != null)
      targetSalesAccountId = (PMBillEngine.GetLineAccount(graph, tran.AccountID) ?? throw new PXException("The {0} billing step of the {1} billing rule has failed to process the {2} {3} project transaction. The billing step is configured to use the sales account from the source transaction, but the debit account has not been specified in at least one line of this project transaction.", new object[4]
      {
        (object) rule.StepID,
        (object) rule.BillingID,
        (object) tran.TranType,
        (object) tran.RefNbr
      })).AccountID;
    return targetSalesAccountId;
  }

  public virtual string CalculateTargetSalesSubaccountCD(
    PMBillingRule rule,
    PMProject project,
    PMTask task,
    int? subIDTran,
    int? employeeID,
    int? branchID,
    int? inventoryID,
    PX.Objects.AR.Customer customer)
  {
    return PMBillEngine.CalculateTargetSalesSubaccountCD((PXGraph) this, rule, project, task, subIDTran, employeeID, branchID, inventoryID, customer);
  }

  public static string CalculateTargetSalesSubaccountCD(
    PXGraph graph,
    PMBillingRule rule,
    PMProject project,
    PMTask task,
    int? subIDTran,
    int? employeeID,
    int? branchID,
    int? inventoryID,
    PX.Objects.AR.Customer customer)
  {
    string mask = rule.SubMask;
    int? nullable1 = new int?();
    int? nullable2 = new int?();
    int? nullable3 = new int?();
    int? nullable4 = new int?();
    int? nullable5 = new int?();
    if (employeeID.HasValue && !string.IsNullOrEmpty(rule.SubMask) && rule.SubMask.Contains("E"))
    {
      PX.Objects.EP.EPEmployee epEmployee = PXResultset<PX.Objects.EP.EPEmployee>.op_Implicit(PXSelectBase<PX.Objects.EP.EPEmployee, PXSelect<PX.Objects.EP.EPEmployee, Where<PX.Objects.EP.EPEmployee.bAccountID, Equal<Required<PX.Objects.EP.EPEmployee.bAccountID>>>>.Config>.Select(graph, new object[1]
      {
        (object) employeeID
      }));
      if (epEmployee != null)
        nullable1 = epEmployee.SalesSubID;
    }
    int? nullable6;
    if (inventoryID.HasValue && !string.IsNullOrEmpty(rule.SubMask) && rule.SubMask.Contains("I"))
    {
      int emptyInventoryId = PMInventorySelectorAttribute.EmptyInventoryID;
      nullable6 = inventoryID;
      int valueOrDefault = nullable6.GetValueOrDefault();
      if (!(emptyInventoryId == valueOrDefault & nullable6.HasValue))
      {
        PX.Objects.IN.InventoryItem inventoryItem = PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<PX.Objects.IN.InventoryItem.inventoryID>>>>.Config>.Select(graph, new object[1]
        {
          (object) inventoryID
        }));
        if (inventoryItem != null)
        {
          nullable2 = inventoryItem.SalesSubID;
          goto label_10;
        }
        goto label_10;
      }
    }
    if (inventoryID.HasValue)
    {
      int emptyInventoryId = PMInventorySelectorAttribute.EmptyInventoryID;
      nullable6 = inventoryID;
      int valueOrDefault = nullable6.GetValueOrDefault();
      if (!(emptyInventoryId == valueOrDefault & nullable6.HasValue))
        goto label_10;
    }
    mask = mask.Replace("I", "J");
label_10:
    if (!string.IsNullOrEmpty(rule.SubMask) && rule.SubMask.Contains("C"))
    {
      PX.Objects.CR.Location location = PXResultset<PX.Objects.CR.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Location, PXSelect<PX.Objects.CR.Location, Where<PX.Objects.CR.Location.bAccountID, Equal<Required<PX.Objects.CR.Location.bAccountID>>, And<PX.Objects.CR.Location.locationID, Equal<Required<PX.Objects.CR.Location.locationID>>>>>.Config>.Select(graph, new object[2]
      {
        (object) customer.BAccountID,
        (object) customer.DefLocationID
      }));
      if (location != null)
        nullable3 = location.CSalesSubID;
    }
    if (rule.SubMaskBudget.Contains("R"))
      branchID = graph.Accessinfo.BranchID;
    if (!string.IsNullOrEmpty(rule.SubMask) && rule.SubMask.Contains("R") && branchID.HasValue)
    {
      PX.Objects.GL.Branch branch = PXResultset<PX.Objects.GL.Branch>.op_Implicit(PXSelectBase<PX.Objects.GL.Branch, PXSelect<PX.Objects.GL.Branch, Where<PX.Objects.GL.Branch.branchID, Equal<Required<PX.Objects.GL.Branch.branchID>>>>.Config>.Select(graph, new object[1]
      {
        (object) branchID
      }));
      if (branch != null)
      {
        PX.Objects.CR.Standalone.Location location = PXResultset<PX.Objects.CR.Standalone.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Standalone.Location, PXSelectJoin<PX.Objects.CR.Standalone.Location, LeftJoin<PX.Objects.CR.BAccount, On<PX.Objects.CR.BAccount.bAccountID, Equal<Required<PX.Objects.CR.BAccount.bAccountID>>>>, Where<PX.Objects.CR.Standalone.Location.bAccountID, Equal<PX.Objects.CR.BAccount.bAccountID>, And<PX.Objects.CR.Standalone.Location.locationID, Equal<PX.Objects.CR.BAccount.defLocationID>>>>.Config>.Select(graph, new object[1]
        {
          (object) branch.BAccountID
        }));
        if (location != null)
          nullable4 = location.CMPSalesSubID;
      }
    }
    if (subIDTran.HasValue && !string.IsNullOrEmpty(rule.SubMask) && rule.SubMask.Contains("S"))
      nullable5 = subIDTran;
    return PMBillSubAccountMaskAttribute.MakeSub<PMBillingRule.subMask>(graph, mask, new object[8]
    {
      (object) rule.SubID,
      (object) project.DefaultSalesSubID,
      (object) task.DefaultSalesSubID,
      (object) nullable1,
      (object) nullable5,
      (object) nullable2,
      (object) nullable3,
      (object) nullable4
    }, new System.Type[8]
    {
      typeof (PMBillingRule.subID),
      typeof (PMProject.defaultSalesSubID),
      typeof (PMTask.defaultSalesSubID),
      typeof (PX.Objects.EP.EPEmployee.salesSubID),
      typeof (PMTran.subID),
      typeof (PX.Objects.IN.InventoryItem.salesSubID),
      typeof (PX.Objects.CR.Location.cSalesSubID),
      typeof (PX.Objects.CR.Location.cMPSalesSubID)
    });
  }

  public virtual void Transform(
    PMProject project,
    List<PMTran> billingBase,
    PMBillingRule rule,
    PMTask task)
  {
    foreach (PMTran tran in billingBase)
    {
      tran.ProjectCuryID = project.CuryID;
      tran.Rate = this.GetRate(rule, tran, task.RateTableID);
      PMAllocator.PMDataNavigator navigator = new PMAllocator.PMDataNavigator((IRateTable) this, new List<PMTran>((IEnumerable<PMTran>) new PMTran[1]
      {
        tran
      }));
      Decimal? qty;
      Decimal? amt;
      string description;
      this.CalculateFormulas(project, navigator, rule, tran, out qty, out amt, out description);
      tran.InvoicedQty = qty;
      ((PXSelectBase<PMTran>) this.Transactions).SetValueExt<PMTran.projectCuryInvoicedAmount>(tran, (object) amt);
      tran.InvoicedDescription = description;
    }
  }

  protected virtual void CalculateFormulas(
    PMProject project,
    PMAllocator.PMDataNavigator navigator,
    PMBillingRule rule,
    PMTran tran,
    out Decimal? qty,
    out Decimal? amt,
    out string description)
  {
    qty = new Decimal?();
    amt = new Decimal?();
    description = (string) null;
    long? remainderOfTranId;
    if (rule.Type == "T" && !string.IsNullOrEmpty(rule.QtyFormula))
    {
      remainderOfTranId = tran.RemainderOfTranID;
      if (!remainderOfTranId.HasValue)
      {
        try
        {
          ExpressionNode expressionNode = PMExpressionParser.Parse((IRateTable) this, rule.QtyFormula);
          expressionNode.Bind((object) navigator);
          qty = new Decimal?(Convert.ToDecimal(expressionNode.Eval((object) tran) ?? throw new Exception("No result has been returned. Please check Trace for details.")));
          goto label_7;
        }
        catch (Exception ex)
        {
          throw new PXException("Failed to calculate the quantity using the {3} step of the {0} billing rule. Line Quantity Formula: {1} Error: {2}", new object[4]
          {
            (object) rule.BillingID,
            (object) rule.QtyFormula,
            (object) ex.Message,
            (object) rule.StepID
          });
        }
      }
    }
    qty = tran.Qty;
label_7:
    if (rule.Type == "T" && !string.IsNullOrEmpty(rule.AmountFormula))
    {
      remainderOfTranId = tran.RemainderOfTranID;
      if (!remainderOfTranId.HasValue)
      {
        try
        {
          ExpressionNode expressionNode = PMExpressionParser.Parse((IRateTable) this, rule.AmountFormula);
          expressionNode.Bind((object) navigator);
          amt = new Decimal?(Convert.ToDecimal(expressionNode.Eval((object) tran) ?? throw new Exception("No result has been returned. Please check Trace for details.")));
          goto label_14;
        }
        catch (Exception ex)
        {
          throw new PXException("Failed to calculate the amount using the {3} step of the {0} billing rule. Line Amount Formula: {1} Error: {2}", new object[4]
          {
            (object) rule.BillingID,
            (object) rule.AmountFormula,
            (object) ex.Message,
            (object) rule.StepID
          });
        }
      }
    }
    amt = tran.TranCuryAmount;
label_14:
    if (!string.IsNullOrEmpty(rule.DescriptionFormula))
    {
      try
      {
        ExpressionNode expressionNode = PMExpressionParser.Parse((IRateTable) this, rule.DescriptionFormula);
        expressionNode.Bind((object) navigator);
        using (new PXLocaleScope(PXResultset<PX.Objects.AR.Customer>.op_Implicit(PXSelectBase<PX.Objects.AR.Customer, PXSelectJoin<PX.Objects.AR.Customer, InnerJoin<PMProject, On<PX.Objects.AR.Customer.bAccountID, Equal<PMProject.customerID>>>, Where<PMProject.contractID, Equal<Current<PMTran.projectID>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
        {
          (object) tran
        }, Array.Empty<object>())).LocaleName))
        {
          object obj = expressionNode.Eval((object) tran);
          if (obj != null)
            description = obj.ToString();
        }
      }
      catch (Exception ex)
      {
        throw new PXException("Failed to calculate the line description using the {3} step of the {0} billing rule. Line Description Formula: {1} Error: {2}", new object[4]
        {
          (object) rule.BillingID,
          (object) rule.DescriptionFormula,
          (object) ex.Message,
          (object) rule.StepID
        });
      }
    }
    else
      description = tran.Description;
    if (string.IsNullOrEmpty(description) || description.Length <= 256 /*0x0100*/)
      return;
    description = description.Substring(0, 256 /*0x0100*/);
  }

  public virtual object Evaluate(
    PMObjectType objectName,
    string fieldName,
    string attribute,
    PMTran row)
  {
    switch (objectName)
    {
      case PMObjectType.PMTran:
        return this.ConvertFromExtValue(((PXGraph) this).Caches[typeof (PMTran)].GetValueExt((object) row, fieldName));
      case PMObjectType.PMProject:
        PMProject pmProject = PMProject.PK.Find((PXGraph) this, row.ProjectID);
        if (pmProject != null)
          return attribute != null ? this.EvaluateAttribute(attribute, pmProject.NoteID) : this.ConvertFromExtValue(((PXGraph) this).Caches[typeof (PMProject)].GetValueExt((object) pmProject, fieldName));
        break;
      case PMObjectType.PMTask:
        PMTask dirty = PMTask.PK.FindDirty((PXGraph) this, row.ProjectID, row.TaskID);
        if (dirty != null)
          return attribute != null ? this.EvaluateAttribute(attribute, dirty.NoteID) : this.ConvertFromExtValue(((PXGraph) this).Caches[typeof (PMTask)].GetValueExt((object) dirty, fieldName));
        break;
      case PMObjectType.PMAccountGroup:
        PMAccountGroup pmAccountGroup = PMAccountGroup.PK.Find((PXGraph) this, row.AccountGroupID);
        if (pmAccountGroup != null)
          return attribute != null ? this.EvaluateAttribute(attribute, pmAccountGroup.NoteID) : this.ConvertFromExtValue(((PXGraph) this).Caches[typeof (PMAccountGroup)].GetValueExt((object) pmAccountGroup, fieldName));
        break;
      case PMObjectType.EPEmployee:
        PX.Objects.EP.EPEmployee epEmployee = PXResultset<PX.Objects.EP.EPEmployee>.op_Implicit(PXSelectBase<PX.Objects.EP.EPEmployee, PXSelect<PX.Objects.EP.EPEmployee, Where<PX.Objects.EP.EPEmployee.bAccountID, Equal<Required<PX.Objects.EP.EPEmployee.bAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) row.ResourceID
        }));
        if (epEmployee != null)
          return attribute != null ? this.EvaluateAttribute(attribute, epEmployee.NoteID) : this.ConvertFromExtValue(((PXGraph) this).Caches[typeof (PX.Objects.EP.EPEmployee)].GetValueExt((object) epEmployee, fieldName));
        break;
      case PMObjectType.Customer:
        PX.Objects.AR.Customer customer = PXResultset<PX.Objects.AR.Customer>.op_Implicit(PXSelectBase<PX.Objects.AR.Customer, PXSelect<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Required<PX.Objects.AR.Customer.bAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) row.BAccountID
        }));
        if (customer != null)
          return attribute != null ? this.EvaluateAttribute(attribute, customer.NoteID) : this.ConvertFromExtValue(((PXGraph) this).Caches[typeof (PX.Objects.AR.Customer)].GetValueExt((object) customer, fieldName));
        break;
      case PMObjectType.Vendor:
        VendorR vendorR = PXResultset<VendorR>.op_Implicit(PXSelectBase<VendorR, PXSelect<VendorR, Where<VendorR.bAccountID, Equal<Required<VendorR.bAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) row.BAccountID
        }));
        if (vendorR != null)
          return attribute != null ? this.EvaluateAttribute(attribute, vendorR.NoteID) : this.ConvertFromExtValue(((PXGraph) this).Caches[typeof (VendorR)].GetValueExt((object) vendorR, fieldName));
        break;
      case PMObjectType.InventoryItem:
        PX.Objects.IN.InventoryItem inventoryItem = PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<PX.Objects.IN.InventoryItem.inventoryID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) row.InventoryID
        }));
        if (inventoryItem != null)
          return attribute != null ? this.EvaluateAttribute(attribute, inventoryItem.NoteID) : this.ConvertFromExtValue(((PXGraph) this).Caches[typeof (PX.Objects.IN.InventoryItem)].GetValueExt((object) inventoryItem, fieldName));
        break;
      case PMObjectType.PMBudget:
        PMBudget pmBudget = PXResultset<PMBudget>.op_Implicit(((PXSelectBase<PMBudget>) new PXSelect<PMBudget, Where<PMBudget.accountGroupID, Equal<Required<PMBudget.accountGroupID>>, And<PMBudget.projectID, Equal<Required<PMBudget.projectID>>, And<PMBudget.projectTaskID, Equal<Required<PMBudget.projectTaskID>>, And<PMBudget.inventoryID, Equal<Required<PMBudget.inventoryID>>, And<PMBudget.costCodeID, Equal<Required<PMBudget.costCodeID>>>>>>>>((PXGraph) this)).Select(new object[5]
        {
          (object) row.AccountGroupID,
          (object) row.ProjectID,
          (object) row.TaskID,
          (object) (row.InventoryID ?? PMInventorySelectorAttribute.EmptyInventoryID),
          (object) (row.CostCodeID ?? CostCodeAttribute.DefaultCostCode.GetValueOrDefault())
        }));
        if (pmBudget != null)
          return this.ConvertFromExtValue(((PXGraph) this).Caches[typeof (PMBudget)].GetValueExt((object) pmBudget, fieldName));
        PXTrace.WriteWarning("Budget was not found");
        break;
    }
    return (object) null;
  }

  public virtual Decimal? GetPrice(PMTran tran)
  {
    Decimal? price = new Decimal?();
    if (tran.InventoryID.HasValue)
    {
      int? inventoryId = tran.InventoryID;
      int emptyInventoryId = PMInventorySelectorAttribute.EmptyInventoryID;
      if (!(inventoryId.GetValueOrDefault() == emptyInventoryId & inventoryId.HasValue))
      {
        string custPriceClass = "BASE";
        PMProject project = (PMProject) PXSelectorAttribute.Select(((PXGraph) this).Caches[typeof (PMTran)], (object) tran, "ProjectID");
        PMTask pmTask = (PMTask) PXSelectorAttribute.Select(((PXGraph) this).Caches[typeof (PMTran)], (object) tran, "TaskID");
        PX.Objects.CR.Location location = (PX.Objects.CR.Location) PXSelectorAttribute.Select(((PXGraph) this).Caches[typeof (PMTask)], (object) pmTask, "LocationID");
        if (location != null && !string.IsNullOrEmpty(location.CPriceClassID))
          custPriceClass = location.CPriceClassID;
        PX.Objects.CM.CurrencyInfo cm = this.GetCurrencyInfo(project, new DateTime?(this.billingDate)).GetCM();
        bool alwaysFromBaseCurrency = false;
        if (((PXSelectBase<ARSetup>) this.arSetup).Current != null)
          alwaysFromBaseCurrency = ((PXSelectBase<ARSetup>) this.arSetup).Current.AlwaysFromBaseCury.GetValueOrDefault();
        string taxCalcMode = location?.CTaxCalcMode ?? "T";
        price = ARSalesPriceMaint.CalculateSalesPrice(((PXGraph) this).Caches[typeof (PMTran)], custPriceClass, pmTask.CustomerID, tran.InventoryID, cm, tran.Qty, tran.UOM, tran.Date.Value, alwaysFromBaseCurrency, taxCalcMode);
        if (!alwaysFromBaseCurrency && !price.HasValue)
          price = ARSalesPriceMaint.CalculateSalesPrice(((PXGraph) this).Caches[typeof (PMTran)], custPriceClass, pmTask.CustomerID, tran.InventoryID, cm, tran.Qty, tran.UOM, tran.Date.Value, true, taxCalcMode);
      }
    }
    return price;
  }

  public Decimal? ConvertAmountToCurrency(
    string fromCuryID,
    string toCuryID,
    string rateType,
    DateTime? effectiveDate,
    Decimal? value)
  {
    if (string.IsNullOrEmpty(fromCuryID))
      throw new ArgumentNullException(nameof (fromCuryID), "From CuryID is null or an empty string.");
    if (string.IsNullOrEmpty(toCuryID))
      throw new ArgumentNullException(nameof (toCuryID), "To CuryID is null or an empty string.");
    if (string.IsNullOrEmpty(rateType))
      throw new ArgumentNullException(nameof (rateType), "RateType is null or an empty string.");
    if (!effectiveDate.HasValue)
      throw new ArgumentNullException(nameof (effectiveDate), "Effective Date is required.");
    if (!value.HasValue)
      return new Decimal?();
    if (value.Value == 0M)
      return new Decimal?(0M);
    if (string.Equals(fromCuryID, toCuryID, StringComparison.InvariantCultureIgnoreCase))
      return new Decimal?(value.Value);
    IPXCurrencyService pxCurrencyService = ServiceLocator.Current.GetInstance<Func<PXGraph, IPXCurrencyService>>()((PXGraph) this);
    return new Decimal?(PMCommitmentAttribute.CuryConvCury(pxCurrencyService.GetRate(fromCuryID, toCuryID, rateType, effectiveDate) ?? throw new PXException("Please define a conversion rate from the {0} to {1} currency within the {2} currency rate type and the {3:d} effective date on the Currency Rates (CM301000) form.", new object[4]
    {
      (object) fromCuryID,
      (object) toCuryID,
      (object) rateType,
      (object) effectiveDate
    }), value.GetValueOrDefault(), new int?(pxCurrencyService.CuryDecimalPlaces(toCuryID))));
  }

  public virtual PX.Objects.CM.Extensions.CurrencyInfo GetCurrencyInfo(
    PMProject project,
    DateTime? effectiveDate)
  {
    string billingCuryId = project.BillingCuryID;
    CMSetup cmSetup = PXResultset<CMSetup>.op_Implicit(PXSelectBase<CMSetup, PXSelect<CMSetup>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    string str = project.RateTypeID ?? cmSetup?.PMRateTypeDflt;
    PX.Objects.CM.Extensions.CurrencyInfo info = new PX.Objects.CM.Extensions.CurrencyInfo()
    {
      ModuleCode = "PM",
      BaseCuryID = project.BaseCuryID
    };
    if (billingCuryId == project.BaseCuryID)
    {
      info.CuryID = project.BaseCuryID;
      info.CuryRate = new Decimal?((Decimal) 1);
      info.RecipRate = new Decimal?((Decimal) 1);
      info.CuryMultDiv = "M";
    }
    else if (project.CuryID == project.BillingCuryID && billingCuryId != project.BaseCuryID)
    {
      info = ((PXGraph) this).GetExtension<PMBillEngine.MultiCurrency>().GetCurrencyInfo(project.CuryInfoID);
    }
    else
    {
      if (!effectiveDate.HasValue)
        effectiveDate = new DateTime?(((PXGraph) this).Accessinfo.BusinessDate ?? DateTime.Now);
      info.CuryID = billingCuryId;
      info.CuryRateTypeID = str;
      info.CuryEffDate = effectiveDate;
      info.SearchForNewRate((PXGraph) this).Populate(info);
    }
    return info;
  }

  protected virtual object ConvertFromExtValue(object extValue)
  {
    return extValue is PXFieldState pxFieldState ? pxFieldState.Value : extValue;
  }

  protected virtual object EvaluateAttribute(string attribute, Guid? refNoteID)
  {
    PXResultset<CSAnswers> pxResultset = PXSelectBase<CSAnswers, PXSelectJoin<CSAnswers, InnerJoin<CSAttribute, On<CSAttribute.attributeID, Equal<CSAnswers.attributeID>>>, Where<CSAnswers.refNoteID, Equal<Required<CSAnswers.refNoteID>>, And<CSAnswers.attributeID, Equal<Required<CSAnswers.attributeID>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) refNoteID,
      (object) attribute
    });
    CSAnswers csAnswers = (CSAnswers) null;
    CSAttribute csAttribute = (CSAttribute) null;
    if (pxResultset.Count > 0)
    {
      csAnswers = (CSAnswers) ((PXResult) pxResultset[0])[0];
      csAttribute = (CSAttribute) ((PXResult) pxResultset[0])[1];
    }
    if (csAnswers == null || csAnswers.AttributeID == null)
    {
      csAttribute = PXResultset<CSAttribute>.op_Implicit(PXSelectBase<CSAttribute, PXSelect<CSAttribute, Where<CSAttribute.attributeID, Equal<Required<CSAttribute.attributeID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) attribute
      }));
      if (csAttribute != null)
      {
        object defaultValueFor = KeyValueHelper.GetDefaultValueFor(csAttribute.ControlType);
        if (defaultValueFor != null)
          return defaultValueFor;
      }
    }
    if (csAnswers != null)
    {
      if (csAnswers.Value != null)
        return (object) csAnswers.Value;
      if (csAttribute != null)
      {
        object defaultValueFor = KeyValueHelper.GetDefaultValueFor(csAttribute.ControlType);
        if (defaultValueFor != null)
          return defaultValueFor;
      }
    }
    return (object) string.Empty;
  }

  public virtual bool PreSelectTasksTransactions(
    int? projectID,
    List<PMTask> tasks,
    DateTime? cuttoffDate)
  {
    this.transactions.Clear();
    if (tasks.Count == 0)
      return false;
    HashSet<int> intSet = new HashSet<int>();
    foreach (PMTask task in tasks)
    {
      this.transactions.Add(task.TaskID.Value, new Dictionary<int, List<PXResult<PMTran>>>());
      List<PMBillingRule> pmBillingRuleList;
      if (!this.billingRules.TryGetValue(task.BillingID, out pmBillingRuleList))
      {
        PXSelect<PMBillingRule, Where<PMBillingRule.billingID, Equal<Required<PMBillingRule.billingID>>, And<PMBillingRule.isActive, Equal<True>>>> pxSelect = new PXSelect<PMBillingRule, Where<PMBillingRule.billingID, Equal<Required<PMBillingRule.billingID>>, And<PMBillingRule.isActive, Equal<True>>>>((PXGraph) this);
        pmBillingRuleList = new List<PMBillingRule>();
        object[] objArray = new object[1]
        {
          (object) task.BillingID
        };
        foreach (PXResult<PMBillingRule> pxResult in ((PXSelectBase<PMBillingRule>) pxSelect).Select(objArray))
        {
          PMBillingRule pmBillingRule = PXResult<PMBillingRule>.op_Implicit(pxResult);
          pmBillingRuleList.Add(pmBillingRule);
        }
        this.billingRules.Add(task.BillingID, pmBillingRuleList);
      }
      foreach (PMBillingRule pmBillingRule in pmBillingRuleList)
      {
        if (pmBillingRule.Type == "T")
          intSet.Add(pmBillingRule.AccountGroupID.Value);
      }
    }
    foreach (int num in intSet)
    {
      foreach (PXResult<PMTran> pxResult in this.GetTranFromDatabase(projectID, num, cuttoffDate))
      {
        Dictionary<int, List<PXResult<PMTran>>> dictionary;
        if (this.transactions.TryGetValue(PXResult<PMTran>.op_Implicit(pxResult).TaskID.Value, out dictionary))
        {
          List<PXResult<PMTran>> pxResultList;
          if (!dictionary.TryGetValue(num, out pxResultList))
          {
            pxResultList = new List<PXResult<PMTran>>();
            dictionary.Add(num, pxResultList);
          }
          pxResultList.Add(pxResult);
        }
      }
    }
    return true;
  }

  public virtual void PreSelectRecurrentItems(int? projectID)
  {
    this.recurrentItemsByTask.Clear();
    foreach (PXResult<PMRecurringItem> pxResult in ((PXSelectBase<PMRecurringItem>) new PXSelect<PMRecurringItem, Where<PMRecurringItem.projectID, Equal<Required<PMRecurringItem.projectID>>>>((PXGraph) this)).Select(new object[1]
    {
      (object) projectID
    }))
    {
      PMRecurringItem pmRecurringItem = PXResult<PMRecurringItem>.op_Implicit(pxResult);
      Dictionary<int, List<PMRecurringItem>> recurrentItemsByTask1 = this.recurrentItemsByTask;
      int? taskId = pmRecurringItem.TaskID;
      int key1 = taskId.Value;
      List<PMRecurringItem> pmRecurringItemList1;
      ref List<PMRecurringItem> local = ref pmRecurringItemList1;
      if (!recurrentItemsByTask1.TryGetValue(key1, out local))
      {
        pmRecurringItemList1 = new List<PMRecurringItem>();
        Dictionary<int, List<PMRecurringItem>> recurrentItemsByTask2 = this.recurrentItemsByTask;
        taskId = pmRecurringItem.TaskID;
        int key2 = taskId.Value;
        List<PMRecurringItem> pmRecurringItemList2 = pmRecurringItemList1;
        recurrentItemsByTask2.Add(key2, pmRecurringItemList2);
      }
      pmRecurringItemList1.Add(pmRecurringItem);
    }
  }

  public virtual void PreSelectRevenueBudgetLines(int? projectID)
  {
    this.revenueBudgetLines.Clear();
    FbqlSelect<SelectFromBase<PMRevenueBudget, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PMAccountGroup>.On<BqlOperand<PMRevenueBudget.accountGroupID, IBqlInt>.IsEqual<PMAccountGroup.groupID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMRevenueBudget.projectID, Equal<P.AsInt>>>>>.And<BqlOperand<PMRevenueBudget.type, IBqlString>.IsEqual<AccountType.income>>>, PMRevenueBudget>.View view = new FbqlSelect<SelectFromBase<PMRevenueBudget, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PMAccountGroup>.On<BqlOperand<PMRevenueBudget.accountGroupID, IBqlInt>.IsEqual<PMAccountGroup.groupID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMRevenueBudget.projectID, Equal<P.AsInt>>>>>.And<BqlOperand<PMRevenueBudget.type, IBqlString>.IsEqual<AccountType.income>>>, PMRevenueBudget>.View((PXGraph) this);
    if (CostCodeAttribute.UseCostCode())
      ((PXSelectBase<PMRevenueBudget>) view).OrderByNew<OrderBy<Asc<PMRevenueBudget.inventoryID, Asc<PMRevenueBudget.costCodeID>>>>();
    ParameterExpression parameterExpression;
    // ISSUE: method reference
    this.revenueBudgetLines = ((IQueryable<PXResult<PMRevenueBudget>>) ((PXSelectBase<PMRevenueBudget>) view).Select(new object[1]
    {
      (object) projectID
    })).Select<PXResult<PMRevenueBudget>, PXResult<PMRevenueBudget, PMAccountGroup>>((Expression<Func<PXResult<PMRevenueBudget>, PXResult<PMRevenueBudget, PMAccountGroup>>>) (x => (PXResult<PMRevenueBudget, PMAccountGroup>) x)).Where<PXResult<PMRevenueBudget, PMAccountGroup>>(Expression.Lambda<Func<PXResult<PMRevenueBudget, PMAccountGroup>, bool>>((Expression) Expression.NotEqual((Expression) Expression.Call(x, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PXResult.GetItem)), Array.Empty<Expression>()), (Expression) Expression.Constant((object) null, typeof (object))), parameterExpression)).AsEnumerable<PXResult<PMRevenueBudget, PMAccountGroup>>().GroupBy<PXResult<PMRevenueBudget, PMAccountGroup>, int?>((Func<PXResult<PMRevenueBudget, PMAccountGroup>, int?>) (x => ((PXResult) x).GetItem<PMRevenueBudget>().TaskID)).ToDictionary<IGrouping<int?, PXResult<PMRevenueBudget, PMAccountGroup>>, int, List<PXResult<PMRevenueBudget, PMAccountGroup>>>((Func<IGrouping<int?, PXResult<PMRevenueBudget, PMAccountGroup>>, int>) (g => g.Key.Value), (Func<IGrouping<int?, PXResult<PMRevenueBudget, PMAccountGroup>>, List<PXResult<PMRevenueBudget, PMAccountGroup>>>) (g => g.ToList<PXResult<PMRevenueBudget, PMAccountGroup>>()));
  }

  public virtual List<PMTran> SelectBillingBase(
    int? projectID,
    int? taskID,
    int? accountGroupID,
    bool includeNonBillable)
  {
    List<PMTran> pmTranList = new List<PMTran>();
    Dictionary<int, List<PXResult<PMTran>>> dictionary;
    List<PXResult<PMTran>> pxResultList;
    if (this.transactions.TryGetValue(taskID.Value, out dictionary) && dictionary.TryGetValue(accountGroupID.Value, out pxResultList))
    {
      foreach (PXResult<PMTran> pxResult in pxResultList)
      {
        PMTran pmTran = PXResult<PMTran>.op_Implicit(pxResult);
        if (includeNonBillable || pmTran.Billable.GetValueOrDefault())
          pmTranList.Add(pmTran);
      }
    }
    return pmTranList;
  }

  public virtual PXResultset<PMTran> GetTranFromDatabase(
    int? projectID,
    int groupID,
    DateTime? cuttofDate)
  {
    PXSelectBase<PMTran> pxSelectBase = (PXSelectBase<PMTran>) new PXSelectReadonly<PMTran, Where<PMTran.billed, Equal<False>, And<PMTran.excludedFromBilling, Equal<False>, And<PMTran.released, Equal<True>, And<PMTran.accountGroupID, Equal<Required<PMTran.accountGroupID>>, And<PMTran.projectID, Equal<Required<PMTran.projectID>>, And<PMTran.date, Less<Required<PMTran.date>>>>>>>>>((PXGraph) this);
    if (CostCodeAttribute.UseCostCode())
      pxSelectBase.OrderByNew<OrderBy<Asc<PMTran.costCodeID>>>();
    return pxSelectBase.Select(new object[3]
    {
      (object) groupID,
      (object) projectID,
      (object) cuttofDate
    });
  }

  public virtual IList<PMTran> ReverseTran(PMTran tran)
  {
    return (IList<PMTran>) new List<PMTran>()
    {
      this.ReverseTran(tran, false)
    };
  }

  public virtual PMTran ReverseTran(PMTran tran, bool copyTranDateAndPeriod)
  {
    PMTran copy = PXCache<PMTran>.CreateCopy(tran);
    copy.OrigTranID = tran.TranID;
    copy.TranID = new long?();
    copy.TranType = (string) null;
    copy.RefNbr = (string) null;
    copy.ARRefNbr = (string) null;
    copy.ARTranType = (string) null;
    copy.RefLineNbr = new int?();
    copy.ProformaRefNbr = (string) null;
    copy.ProformaLineNbr = new int?();
    copy.BatchNbr = (string) null;
    copy.RemainderOfTranID = new long?();
    copy.OrigProjectID = new int?();
    copy.OrigTaskID = new int?();
    copy.OrigAccountGroupID = new int?();
    copy.NoteID = new Guid?();
    copy.AllocationID = (string) null;
    if (!copyTranDateAndPeriod)
    {
      copy.TranDate = new DateTime?();
      copy.TranPeriodID = (string) null;
    }
    if (tran.IsNonGL.GetValueOrDefault())
    {
      copy.AccountID = new int?();
      copy.SubID = new int?();
      copy.OffsetAccountID = new int?();
      copy.OffsetSubID = new int?();
    }
    int? nullable1 = tran.MigrationOffsetAccountGroupID;
    if (nullable1.HasValue)
    {
      nullable1 = copy.OffsetAccountGroupID;
      if (!nullable1.HasValue)
      {
        copy.OffsetAccountGroupID = tran.MigrationOffsetAccountGroupID;
        PMTran pmTran = copy;
        nullable1 = new int?();
        int? nullable2 = nullable1;
        pmTran.MigrationOffsetAccountGroupID = nullable2;
      }
    }
    PMTran pmTran1 = copy;
    Decimal? nullable3 = pmTran1.TranCuryAmount;
    Decimal num1 = (Decimal) -1;
    pmTran1.TranCuryAmount = nullable3.HasValue ? new Decimal?(nullable3.GetValueOrDefault() * num1) : new Decimal?();
    PMTran pmTran2 = copy;
    nullable3 = pmTran2.ProjectCuryAmount;
    Decimal num2 = (Decimal) -1;
    pmTran2.ProjectCuryAmount = nullable3.HasValue ? new Decimal?(nullable3.GetValueOrDefault() * num2) : new Decimal?();
    PMTran pmTran3 = copy;
    nullable3 = new Decimal?();
    Decimal? nullable4 = nullable3;
    pmTran3.TranCuryAmountCopy = nullable4;
    PMTran pmTran4 = copy;
    nullable3 = pmTran4.Amount;
    Decimal num3 = (Decimal) -1;
    pmTran4.Amount = nullable3.HasValue ? new Decimal?(nullable3.GetValueOrDefault() * num3) : new Decimal?();
    PMTran pmTran5 = copy;
    nullable3 = pmTran5.Qty;
    Decimal num4 = (Decimal) -1;
    pmTran5.Qty = nullable3.HasValue ? new Decimal?(nullable3.GetValueOrDefault() * num4) : new Decimal?();
    PMTran pmTran6 = copy;
    nullable3 = pmTran6.BillableQty;
    Decimal num5 = (Decimal) -1;
    pmTran6.BillableQty = nullable3.HasValue ? new Decimal?(nullable3.GetValueOrDefault() * num5) : new Decimal?();
    copy.Released = new bool?(false);
    copy.Billed = new bool?(false);
    copy.Allocated = new bool?(false);
    copy.Billable = new bool?(false);
    copy.ExcludedFromAllocation = new bool?(true);
    copy.ExcludedFromBilling = new bool?(true);
    copy.ExcludedFromBillingReason = PXMessages.LocalizeFormatNoPrefix("Reversal of Tran. ID {0}", new object[1]
    {
      (object) tran.TranID
    });
    copy.Reverse = "N";
    return copy;
  }

  public static DateTime? GetNextBillingDate(
    PXGraph graph,
    ContractBillingSchedule schedule,
    DateTime? date)
  {
    if (!date.HasValue)
      return new DateTime?();
    switch (schedule.Type)
    {
      case "A":
        return new DateTime?(date.Value.AddYears(1));
      case "M":
        return new DateTime?(date.Value.AddMonths(1));
      case "W":
        return new DateTime?(date.Value.AddDays(7.0));
      case "Q":
        return new DateTime?(date.Value.AddMonths(3));
      case "D":
        return new DateTime?();
      default:
        throw new ArgumentException("The schedule type is invalid.", nameof (schedule));
    }
  }

  public virtual RateEngineV2 CreateRateEngineV2(IList<string> rateTables, IList<string> rateTypes)
  {
    return new RateEngineV2((PXGraph) this, rateTables, rateTypes);
  }

  /// <summary>
  /// Returns RateDefinitions from Cached rateDefinitions collection or from database if not found.
  /// </summary>
  public virtual IList<PMRateDefinition> GetRateDefinitions(string rateTable)
  {
    List<PMRateDefinition> rateDefinitions;
    if (!this.rateDefinitions.TryGetValue(rateTable, out rateDefinitions))
    {
      rateDefinitions = new List<PMRateDefinition>(GraphHelper.RowCast<PMRateDefinition>((IEnumerable) ((PXSelectBase<PMRateDefinition>) new PXSelect<PMRateDefinition, Where<PMRateDefinition.rateTableID, Equal<Required<PMRateDefinition.rateTableID>>>, OrderBy<Asc<PMRateDefinition.rateTypeID, Asc<PMRateDefinition.sequence>>>>((PXGraph) this)).Select(new object[1]
      {
        (object) rateTable
      })));
      this.rateDefinitions.Add(rateTable, rateDefinitions);
    }
    return (IList<PMRateDefinition>) rateDefinitions;
  }

  protected virtual Decimal? GetRate(PMBillingRule rule, PMTran tran, string rateTableID)
  {
    if (string.IsNullOrEmpty(rule.RateTypeID))
    {
      switch (rule.NoRateOption)
      {
        case "0":
          return new Decimal?(0M);
        case "E":
          throw new PXException("The rate type is not defined for the '{1}' step of the '{0}' billing rule.", new object[2]
          {
            (object) rule.BillingID,
            (object) rule.StepID
          });
        case "N":
          return new Decimal?();
        default:
          return new Decimal?((Decimal) 1);
      }
    }
    else
    {
      Decimal? rate = new Decimal?();
      string str = (string) null;
      if (!string.IsNullOrEmpty(rateTableID))
      {
        rate = this.rateEngine.GetRate(rateTableID, rule.RateTypeID, tran);
        str = this.rateEngine.GetTrace(tran);
      }
      if (rate.HasValue)
        return rate;
      switch (rule.NoRateOption)
      {
        case "0":
          return new Decimal?(0M);
        case "E":
          if (!string.IsNullOrEmpty(str))
            PXTrace.WriteInformation(str);
          PXTrace.WriteError("The @Rate is not defined for the {1} step of the {0} billing rule. Check Trace for details.", new object[2]
          {
            (object) rule.BillingID,
            (object) rule.StepID
          });
          throw new PXException("The @Rate is not defined for the {1} step of the {0} billing rule. Check Trace for details.", new object[2]
          {
            (object) rule.BillingID,
            (object) rule.StepID
          });
        case "N":
          if (!string.IsNullOrEmpty(str))
            PXTrace.WriteInformation(str);
          return new Decimal?();
        default:
          return new Decimal?((Decimal) 1);
      }
    }
  }

  public virtual void InitializeRatios(int? projectID)
  {
    if (this.ratios != null)
      return;
    PXSelect<PMProductionBudget, Where<PMProductionBudget.projectID, Equal<Required<PMProject.contractID>>>> pxSelect = new PXSelect<PMProductionBudget, Where<PMProductionBudget.projectID, Equal<Required<PMProject.contractID>>>>((PXGraph) this);
    this.ratios = new Dictionary<string, Decimal?>();
    object[] objArray = new object[1]{ (object) projectID };
    foreach (PXResult<PMProductionBudget> pxResult in ((PXSelectBase<PMProductionBudget>) pxSelect).Select(objArray))
    {
      PMProductionBudget productionBudget = PXResult<PMProductionBudget>.op_Implicit(pxResult);
      string key = $"{productionBudget.RevenueTaskID}.{productionBudget.RevenueInventoryID ?? PMInventorySelectorAttribute.EmptyInventoryID}";
      Decimal? nullable1 = new Decimal?();
      Decimal? nullable2 = productionBudget.CuryRevisedAmount;
      if (nullable2.GetValueOrDefault() != 0M)
      {
        ref Decimal? local = ref nullable1;
        nullable2 = productionBudget.CuryActualAmount;
        Decimal num1 = 100M * nullable2.GetValueOrDefault();
        nullable2 = productionBudget.CuryRevisedAmount;
        Decimal valueOrDefault = nullable2.GetValueOrDefault();
        Decimal num2 = Decimal.Round(num1 / valueOrDefault, 2);
        local = new Decimal?(num2);
      }
      this.ratios.Add(key, nullable1);
    }
  }

  public virtual Grouping CreateGrouping(PMBillingRule rule)
  {
    return new Grouping((IComparer<PMTran>) new PMTranComparer(rule.GroupByItem, rule.GroupByVendor, rule.GroupByDate, rule.GroupByEmployee, false, true, false, true));
  }

  public virtual void CheckMigrationMode()
  {
    if (((PXSelectBase<ARSetup>) this.arSetup).Current.MigrationMode.GetValueOrDefault())
      throw new PXException("The operation is not available because the migration mode is enabled for accounts receivable.");
  }

  public class MultiCurrency : PMTranMultiCurrencyPM<PMBillEngine>
  {
    protected override MultiCurrencyGraph<PMBillEngine, PMTran>.CurySourceMapping GetCurySourceMapping()
    {
      return new MultiCurrencyGraph<PMBillEngine, PMTran>.CurySourceMapping(typeof (PMProject))
      {
        CuryID = typeof (PMProject.baseCuryID)
      };
    }

    protected override PXSelectBase[] GetChildren() => Array<PXSelectBase>.Empty;
  }

  private class StepThreshold
  {
    public Decimal ThresholdPct { get; private set; }

    public Decimal RetainagePct { get; private set; }

    public Decimal Min { get; private set; }

    public Decimal Max { get; set; }

    public Decimal Amount => this.Max - this.Min;

    public StepThreshold(PMRetainageStep step, Decimal contractAmount)
    {
      this.RetainagePct = step.RetainagePct.GetValueOrDefault();
      this.ThresholdPct = step.ThresholdPct.GetValueOrDefault();
      this.Min = this.ThresholdPct * 0.01M * contractAmount;
      this.Max = Decimal.MaxValue;
    }
  }

  public class BillingData
  {
    public PMProformaLine Tran { get; private set; }

    public string SubCD { get; private set; }

    public PMBillingRule Rule { get; private set; }

    public List<PMTran> Transactions { get; private set; }

    public string Note { get; private set; }

    public Guid[] Files { get; private set; }

    public PMRevenueBudget RevenueBudget { get; set; }

    public BillingData(
      PMProformaLine tran,
      PMBillingRule rule,
      PMTran pmTran,
      string subCD,
      string note,
      Guid[] files)
    {
      this.Transactions = new List<PMTran>();
      if (pmTran != null)
        this.Transactions.Add(pmTran);
      this.Tran = tran;
      this.Rule = rule;
      this.SubCD = subCD;
      this.Note = note;
      this.Files = files;
    }

    public BillingData(
      PMProformaLine tran,
      PMBillingRule rule,
      List<PMTran> transactions,
      string subCD,
      string note,
      Guid[] files)
    {
      this.Transactions = transactions;
      this.Tran = tran;
      this.Rule = rule;
      this.SubCD = subCD;
      this.Note = note;
      this.Files = files;
    }
  }

  /// <summary>Result of billing process</summary>
  public class BillingResult
  {
    /// <summary>Date of billing</summary>
    public DateTime BillingDate { get; private set; }

    /// <summary>Created and Modified proformas during billing</summary>
    public List<PMProforma> Proformas { get; } = new List<PMProforma>();

    /// <summary>Created Invoices during billing</summary>
    public List<PX.Objects.AR.ARRegister> Invoices { get; } = new List<PX.Objects.AR.ARRegister>();

    /// <summary>Nothing created</summary>
    public bool IsEmpty => this.Proformas.Count + this.Invoices.Count == 0;

    /// <summary>Only one documant created</summary>
    public bool IsSingle => this.Proformas.Count + this.Invoices.Count == 1;

    /// <summary>Ctor</summary>
    /// <param name="billingDate">Date of billing</param>
    public BillingResult(DateTime billingDate) => this.BillingDate = billingDate;
  }

  public class InvoicePersistingHandler
  {
    private List<PMBillEngine.BillingData> billedData;
    private PMBillingRecord billingRecord;
    private PMRegister allocationReversal;

    public void SetData(
      List<PMBillEngine.BillingData> billedData,
      PMBillingRecord billingRecord,
      PMRegister allocationReversal)
    {
      this.billedData = billedData;
      this.billingRecord = billingRecord;
      this.allocationReversal = allocationReversal;
    }

    public virtual void OnProformaPersisted(PXCache sender, PXRowPersistedEventArgs e)
    {
      if (e.TranStatus != null || e.Operation != 2)
        return;
      foreach (PMBillEngine.BillingData billingData in this.billedData)
      {
        foreach (PMTran transaction in billingData.Transactions)
          transaction.ProformaRefNbr = ((PMProforma) e.Row).RefNbr;
      }
      if (this.billingRecord == null)
        return;
      this.billingRecord.ProformaRefNbr = ((PMProforma) e.Row).RefNbr;
    }

    public virtual void OnInvoicePersisted(PXCache sender, PXRowPersistedEventArgs e)
    {
      if (e.TranStatus != null || e.Operation != 2)
        return;
      PX.Objects.AR.ARInvoice row = (PX.Objects.AR.ARInvoice) e.Row;
      foreach (PMBillEngine.BillingData billingData in this.billedData)
      {
        foreach (PMTran transaction in billingData.Transactions)
        {
          transaction.ARTranType = row.DocType;
          transaction.ARRefNbr = row.RefNbr;
        }
      }
      if (this.billingRecord != null)
      {
        this.billingRecord.ARDocType = row.DocType;
        this.billingRecord.ARRefNbr = row.RefNbr;
      }
      if (this.allocationReversal == null)
        return;
      this.allocationReversal.OrigNoteID = row.NoteID;
    }
  }

  public class InvalidBillingBaseException : PXException
  {
    private static string GetFormatValue(
      IEnumerable<PMBillEngine.InvalidBillingBaseException.BillingComponent> lines)
    {
      StringBuilder stringBuilder = new StringBuilder();
      foreach (PMBillEngine.InvalidBillingBaseException.BillingComponent line in lines)
        stringBuilder.AppendLine(line.ToString());
      return stringBuilder.ToString();
    }

    public IEnumerable<PMRevenueBudget> InvalidLines { get; }

    public InvalidBillingBaseException(
      IEnumerable<PMBillEngine.InvalidBillingBaseException.BillingComponent> lines)
      : base("You must specify Progress Billing Base in the following budget lines on the Revenue Budget tab: {0}", new object[1]
      {
        (object) PMBillEngine.InvalidBillingBaseException.GetFormatValue(lines)
      })
    {
    }

    public InvalidBillingBaseException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    public class BillingComponent
    {
      private readonly string RevenueBudgetLevel;
      private readonly string TaskCD;
      private readonly string AccountGroupCD;
      private readonly Lazy<PX.Objects.IN.InventoryItem> Item;
      private readonly Lazy<PMCostCode> CostCode;

      public string ProgressBillingBase { get; }

      public BillingComponent(
        string revenueBudgetLevel,
        string progressBillingBase,
        string taskCD,
        string accountGroupCD,
        Func<PX.Objects.IN.InventoryItem> itemGetter,
        Func<PMCostCode> costCodeGetter)
      {
        this.ProgressBillingBase = progressBillingBase;
        this.RevenueBudgetLevel = revenueBudgetLevel;
        this.AccountGroupCD = accountGroupCD;
        this.TaskCD = taskCD;
        this.Item = new Lazy<PX.Objects.IN.InventoryItem>(itemGetter);
        this.CostCode = new Lazy<PMCostCode>(costCodeGetter);
      }

      public override string ToString()
      {
        int num1 = EnumerableExtensions.IsIn<string>(this.RevenueBudgetLevel, "C", "D") ? 1 : 0;
        int num2 = EnumerableExtensions.IsIn<string>(this.RevenueBudgetLevel, "I", "D") ? 1 : 0;
        StringBuilder stringBuilder = new StringBuilder($"{this.TaskCD ?? string.Empty}-{this.AccountGroupCD ?? string.Empty}");
        if (num2 != 0)
          stringBuilder.Append("-" + (this.Item?.Value?.InventoryCD ?? string.Empty));
        if (num1 != 0)
          stringBuilder.Append("-" + (this.CostCode?.Value?.CostCodeCD ?? string.Empty));
        return stringBuilder.ToString();
      }
    }
  }
}
