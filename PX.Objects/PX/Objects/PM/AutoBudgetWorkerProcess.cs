// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.AutoBudgetWorkerProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Extensions.MultiCurrency;
using PX.Objects.GL;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

#nullable disable
namespace PX.Objects.PM;

public class AutoBudgetWorkerProcess : PXGraph<AutoBudgetWorkerProcess>
{
  public PXSelect<PMTran> Transactions;

  [PXDefault]
  [PXDBInt]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTran.projectID> e)
  {
  }

  [PXDefault]
  [PXDBInt]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTran.taskID> e)
  {
  }

  [PXDefault]
  [PXDBInt]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTran.inventoryID> e)
  {
  }

  public virtual List<AutoBudgetWorkerProcess.Balance> Run(int? projectID)
  {
    Dictionary<string, AutoBudgetWorkerProcess.Balance> dictionary = new Dictionary<string, AutoBudgetWorkerProcess.Balance>();
    List<PMTran> expenseTransactions = this.CreateExpenseTransactions(projectID);
    List<long> longList = new List<long>();
    foreach (PMTran pmTran in expenseTransactions)
      longList.Add(pmTran.TranID.Value);
    if (expenseTransactions.Count == 0)
    {
      PXTrace.WriteError("Failed to emulate Expenses when running Auto Budget. Probably there is no Expense Account Group in the Budget.");
      return new List<AutoBudgetWorkerProcess.Balance>();
    }
    PMAllocatorEmulator instance1 = PXGraph.CreateInstance<PMAllocatorEmulator>();
    instance1.SourceTransactions = expenseTransactions;
    foreach (PMTran pmTran in expenseTransactions)
      ((PXSelectBase<PMTran>) instance1.Transactions).Insert(pmTran);
    PXSelect<PMTask, Where<PMTask.projectID, Equal<Required<PMTask.projectID>>, And<PMTask.allocationID, IsNotNull>>> pxSelect = new PXSelect<PMTask, Where<PMTask.projectID, Equal<Required<PMTask.projectID>>, And<PMTask.allocationID, IsNotNull>>>((PXGraph) this);
    List<PMTask> tasks = new List<PMTask>();
    object[] objArray = new object[1]{ (object) projectID };
    foreach (PXResult<PMTask> pxResult in ((PXSelectBase<PMTask>) pxSelect).Select(objArray))
    {
      PMTask pmTask = PXResult<PMTask>.op_Implicit(pxResult);
      tasks.Add(pmTask);
    }
    instance1.Execute(tasks);
    foreach (PMTran pmTran in ((PXSelectBase) instance1.Transactions).Cache.Inserted)
    {
      pmTran.Released = new bool?(true);
      ((PXSelectBase) this.Transactions).Cache.Update((object) pmTran);
      longList.Contains(pmTran.TranID.Value);
    }
    DateTime dateTime = DateTime.Now.AddDays(1.0);
    PMBillEngineEmulator instance2 = PXGraph.CreateInstance<PMBillEngineEmulator>();
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) instance2).FieldVerifying.AddHandler<PMTran.projectID>(AutoBudgetWorkerProcess.\u003C\u003Ec.\u003C\u003E9__5_0 ?? (AutoBudgetWorkerProcess.\u003C\u003Ec.\u003C\u003E9__5_0 = new PXFieldVerifying((object) AutoBudgetWorkerProcess.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CRun\u003Eb__5_0))));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) instance2).FieldVerifying.AddHandler<PMTran.taskID>(AutoBudgetWorkerProcess.\u003C\u003Ec.\u003C\u003E9__5_1 ?? (AutoBudgetWorkerProcess.\u003C\u003Ec.\u003C\u003E9__5_1 = new PXFieldVerifying((object) AutoBudgetWorkerProcess.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CRun\u003Eb__5_1))));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) instance2).FieldVerifying.AddHandler<PMTran.inventoryID>(AutoBudgetWorkerProcess.\u003C\u003Ec.\u003C\u003E9__5_2 ?? (AutoBudgetWorkerProcess.\u003C\u003Ec.\u003C\u003E9__5_2 = new PXFieldVerifying((object) AutoBudgetWorkerProcess.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CRun\u003Eb__5_2))));
    foreach (PMTran pmTran in ((PXSelectBase) this.Transactions).Cache.Cached)
      ((PXSelectBase<PMTran>) instance2.Transactions).Insert(pmTran);
    instance2.Bill(projectID, new DateTime?(dateTime), (string) null);
    foreach (PXResult<PX.Objects.AR.ARTran> pxResult in ((PXSelectBase<PX.Objects.AR.ARTran>) instance2.InvoiceEntry.Transactions).Select(Array.Empty<object>()))
    {
      PX.Objects.AR.ARTran arTran = PXResult<PX.Objects.AR.ARTran>.op_Implicit(pxResult);
      int? nullable1 = arTran.TaskID;
      if (nullable1.HasValue)
      {
        PX.Objects.GL.Account account = PXResultset<PX.Objects.GL.Account>.op_Implicit(PXSelectBase<PX.Objects.GL.Account, PXSelect<PX.Objects.GL.Account, Where<PX.Objects.GL.Account.accountID, Equal<Required<PX.Objects.GL.Account.accountID>>>>.Config>.Select((PXGraph) instance2.InvoiceEntry, new object[1]
        {
          (object) arTran.AccountID
        }));
        nullable1 = account.AccountGroupID;
        nullable1 = nullable1.HasValue ? arTran.TaskID : throw new PXException("The billing emulation cannot be run because of incorrect configuration. The sales account in the invoice is not mapped to any account group.");
        // ISSUE: variable of a boxed type
        __Boxed<int> local1 = (ValueType) nullable1.Value;
        // ISSUE: variable of a boxed type
        __Boxed<int?> accountGroupId = (ValueType) account.AccountGroupID;
        nullable1 = arTran.InventoryID;
        // ISSUE: variable of a boxed type
        __Boxed<int> local2 = (ValueType) (nullable1 ?? PMInventorySelectorAttribute.EmptyInventoryID);
        string key = $"{local1}.{accountGroupId}.{local2}";
        Decimal? nullable2;
        if (dictionary.ContainsKey(key))
        {
          AutoBudgetWorkerProcess.Balance balance1 = dictionary[key];
          Decimal amount = balance1.Amount;
          nullable2 = arTran.TranAmt;
          Decimal valueOrDefault1 = nullable2.GetValueOrDefault();
          balance1.Amount = amount + valueOrDefault1;
          AutoBudgetWorkerProcess.Balance balance2 = dictionary[key];
          Decimal quantity = balance2.Quantity;
          nullable2 = arTran.Qty;
          Decimal valueOrDefault2 = nullable2.GetValueOrDefault();
          balance2.Quantity = quantity + valueOrDefault2;
        }
        else
        {
          AutoBudgetWorkerProcess.Balance balance3 = new AutoBudgetWorkerProcess.Balance();
          AutoBudgetWorkerProcess.Balance balance4 = balance3;
          nullable1 = arTran.TaskID;
          int num1 = nullable1.Value;
          balance4.TaskID = num1;
          AutoBudgetWorkerProcess.Balance balance5 = balance3;
          nullable1 = account.AccountGroupID;
          int num2 = nullable1.Value;
          balance5.AccountGroupID = num2;
          AutoBudgetWorkerProcess.Balance balance6 = balance3;
          nullable1 = arTran.InventoryID;
          int num3 = nullable1 ?? PMInventorySelectorAttribute.EmptyInventoryID;
          balance6.InventoryID = num3;
          AutoBudgetWorkerProcess.Balance balance7 = balance3;
          nullable1 = arTran.CostCodeID;
          int num4 = nullable1 ?? CostCodeAttribute.GetDefaultCostCode();
          balance7.CostCodeID = num4;
          AutoBudgetWorkerProcess.Balance balance8 = balance3;
          nullable2 = arTran.TranAmt;
          Decimal valueOrDefault3 = nullable2.GetValueOrDefault();
          balance8.Amount = valueOrDefault3;
          AutoBudgetWorkerProcess.Balance balance9 = balance3;
          nullable2 = arTran.Qty;
          Decimal valueOrDefault4 = nullable2.GetValueOrDefault();
          balance9.Quantity = valueOrDefault4;
          dictionary.Add(key, balance3);
        }
      }
    }
    return new List<AutoBudgetWorkerProcess.Balance>((IEnumerable<AutoBudgetWorkerProcess.Balance>) dictionary.Values);
  }

  protected virtual void PMTran_AccountID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (!(e.Row is PMTran row) || e.NewValue == null)
      return;
    PX.Objects.GL.Account account = PXResultset<PX.Objects.GL.Account>.op_Implicit(PXSelectBase<PX.Objects.GL.Account, PXSelect<PX.Objects.GL.Account, Where2<Match<Current<AccessInfo.userName>>, And<PX.Objects.GL.Account.accountID, Equal<Required<PX.Objects.GL.Account.accountID>>>>>.Config>.Select(sender.Graph, new object[1]
    {
      e.NewValue
    }));
    if (row != null && account != null)
    {
      int? accountGroupId1 = account.AccountGroupID;
      if (accountGroupId1.HasValue)
      {
        accountGroupId1 = account.AccountGroupID;
        int? accountGroupId2 = row.AccountGroupID;
        if (accountGroupId1.GetValueOrDefault() == accountGroupId2.GetValueOrDefault() & accountGroupId1.HasValue == accountGroupId2.HasValue)
          return;
      }
      PMAccountGroup pmAccountGroup = PMAccountGroup.PK.Find(sender.Graph, row.AccountGroupID);
      throw new PXException("In the cost PM transaction emulated during the auto-budget process, the {0} debit account is not associated with the {1} account group.", new object[2]
      {
        (object) account.AccountCD,
        (object) pmAccountGroup.GroupCD
      });
    }
  }

  public virtual List<PMTran> CreateExpenseTransactions(int? projectID)
  {
    PXSelectJoin<PMBudget, InnerJoin<PMProject, On<PMProject.contractID, Equal<PMBudget.projectID>>, InnerJoin<PMTask, On<PMTask.projectID, Equal<PMBudget.projectID>, And<PMTask.taskID, Equal<PMBudget.projectTaskID>>>, InnerJoin<PMAccountGroup, On<PMBudget.accountGroupID, Equal<PMAccountGroup.groupID>>, InnerJoin<PMCostCode, On<PMBudget.costCodeID, Equal<PMCostCode.costCodeID>>, LeftJoin<PX.Objects.IN.InventoryItem, On<PMBudget.inventoryID, Equal<PX.Objects.IN.InventoryItem.inventoryID>>>>>>>, Where<PMBudget.projectID, Equal<Required<PMTask.projectID>>, And<PMAccountGroup.type, Equal<AccountType.expense>>>> pxSelectJoin = new PXSelectJoin<PMBudget, InnerJoin<PMProject, On<PMProject.contractID, Equal<PMBudget.projectID>>, InnerJoin<PMTask, On<PMTask.projectID, Equal<PMBudget.projectID>, And<PMTask.taskID, Equal<PMBudget.projectTaskID>>>, InnerJoin<PMAccountGroup, On<PMBudget.accountGroupID, Equal<PMAccountGroup.groupID>>, InnerJoin<PMCostCode, On<PMBudget.costCodeID, Equal<PMCostCode.costCodeID>>, LeftJoin<PX.Objects.IN.InventoryItem, On<PMBudget.inventoryID, Equal<PX.Objects.IN.InventoryItem.inventoryID>>>>>>>, Where<PMBudget.projectID, Equal<Required<PMTask.projectID>>, And<PMAccountGroup.type, Equal<AccountType.expense>>>>((PXGraph) this);
    List<PMTran> expenseTransactions = new List<PMTran>();
    object[] objArray = new object[1]{ (object) projectID };
    foreach (PXResult<PMBudget, PMProject, PMTask, PMAccountGroup, PMCostCode, PX.Objects.IN.InventoryItem> pxResult in ((PXSelectBase<PMBudget>) pxSelectJoin).Select(objArray))
    {
      PMTran pmTran = this.ExpenseTransactionFromBudget(PXResult<PMBudget, PMProject, PMTask, PMAccountGroup, PMCostCode, PX.Objects.IN.InventoryItem>.op_Implicit(pxResult), PXResult<PMBudget, PMProject, PMTask, PMAccountGroup, PMCostCode, PX.Objects.IN.InventoryItem>.op_Implicit(pxResult), PXResult<PMBudget, PMProject, PMTask, PMAccountGroup, PMCostCode, PX.Objects.IN.InventoryItem>.op_Implicit(pxResult), PXResult<PMBudget, PMProject, PMTask, PMAccountGroup, PMCostCode, PX.Objects.IN.InventoryItem>.op_Implicit(pxResult), PXResult<PMBudget, PMProject, PMTask, PMAccountGroup, PMCostCode, PX.Objects.IN.InventoryItem>.op_Implicit(pxResult), PXResult<PMBudget, PMProject, PMTask, PMAccountGroup, PMCostCode, PX.Objects.IN.InventoryItem>.op_Implicit(pxResult));
      expenseTransactions.Add(((PXSelectBase<PMTran>) this.Transactions).Insert(pmTran));
    }
    return expenseTransactions;
  }

  public virtual PMTran ExpenseTransactionFromBudget(
    PMBudget budget,
    PMProject project,
    PMTask task,
    PMAccountGroup accountGroup,
    PX.Objects.IN.InventoryItem item,
    PMCostCode costcode)
  {
    return new PMTran()
    {
      AccountGroupID = budget.AccountGroupID,
      ProjectID = budget.ProjectID,
      TaskID = budget.ProjectTaskID,
      InventoryID = budget.InventoryID,
      AccountID = item.InventoryID.HasValue ? item.COGSAcctID : accountGroup.AccountID,
      SubID = item.InventoryID.HasValue ? item.COGSSubID : accountGroup.AccountID,
      TranCuryAmount = budget.CuryRevisedAmount,
      ProjectCuryAmount = budget.CuryRevisedAmount,
      TranCuryID = project.CuryID,
      ProjectCuryID = project.CuryID,
      Qty = budget.RevisedQty,
      UOM = budget.UOM,
      BAccountID = task.CustomerID,
      LocationID = task.LocationID,
      Billable = new bool?(true),
      UseBillableQty = new bool?(true),
      BillableQty = budget.RevisedQty,
      Released = new bool?(true)
    };
  }

  public virtual void Persist()
  {
  }

  public string AccountGroupFromID(int? accountGroupID)
  {
    return PMAccountGroup.PK.Find((PXGraph) this, accountGroupID).GroupCD;
  }

  public string InventoryFromID(int? inventoryID)
  {
    if (inventoryID.HasValue)
    {
      int? nullable = inventoryID;
      int num = 0;
      if (!(nullable.GetValueOrDefault() == num & nullable.HasValue))
        return PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<PX.Objects.IN.InventoryItem.inventoryID>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) inventoryID
        })).InventoryCD;
    }
    return "<N/A>";
  }

  public class MultiCurrency : PMTranMultiCurrencyPM<AutoBudgetWorkerProcess>
  {
    protected override CurySource CurrentSourceSelect() => new CurySource();

    protected override PXSelectBase[] GetChildren()
    {
      return new PXSelectBase[1]
      {
        (PXSelectBase) this.Base.Transactions
      };
    }
  }

  [ExcludeFromCodeCoverage]
  public class Balance
  {
    public int TaskID { get; set; }

    public int AccountGroupID { get; set; }

    public int InventoryID { get; set; }

    public int CostCodeID { get; set; }

    public Decimal Amount { get; set; }

    public Decimal Quantity { get; set; }
  }
}
