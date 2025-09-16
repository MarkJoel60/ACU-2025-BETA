// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.RMReportReaderPM
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.CS;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.Reports;
using PX.Objects.CT;
using PX.Objects.PM;
using PX.Reports.ARm;
using PX.Reports.ARm.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

#nullable enable
namespace PX.Objects.CS;

public class RMReportReaderPM : PXGraphExtension<
#nullable disable
RMReportReaderGL, RMReportReader>
{
  public PXSelect<PX.Objects.CS.LightDAC.PMTask> DummyTask;
  private HashSet<int> _historyLoaded;
  private HashSet<PMHistoryKeyTuple> _historySegments;
  private PMHistoryHierDict _pmhistoryPeriodsNested;
  private Dictionary<BudgetKeyTuple, PMBudget> _budgetByKey;
  private RMReportPeriods<PMHistory> _reportPeriods;
  private RMReportRange<PMAccountGroup> _accountGroupsRangeCache;
  private RMReportRange<PX.Objects.CS.LightDAC.Contract> _projectsRangeCache;
  private RMReportRange<PX.Objects.CS.LightDAC.PMTask> _tasksRangeCache;
  private RMReportRange<PX.Objects.IN.InventoryItem> _itemRangeCache;
  private RMReportRange<PMCostCode> _costCodeRangeCache;
  private string _accountGroupMask;
  private string _projectMask;
  private string _projectTaskMask;
  private string _inventoryMask;
  public FbqlSelect<SelectFromBase<PMAccountGroup, TypeArrayOf<IFbqlJoin>.Empty>.Order<By<BqlField<
  #nullable enable
  PMAccountGroup.groupCD, IBqlString>.Asc>>, 
  #nullable disable
  PMAccountGroup>.View accountGroupSelect;
  public FbqlSelect<SelectFromBase<PX.Objects.CS.LightDAC.Contract, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PX.Objects.CS.LightDAC.Contract.nonProject, 
  #nullable disable
  Equal<False>>>>>.And<BqlOperand<
  #nullable enable
  PX.Objects.CS.LightDAC.Contract.baseType, IBqlString>.IsEqual<
  #nullable disable
  CTPRType.project>>>.Order<By<BqlField<
  #nullable enable
  PX.Objects.CS.LightDAC.Contract.contractCD, IBqlString>.Asc>>, 
  #nullable disable
  PX.Objects.CS.LightDAC.Contract>.View projectSelect;
  public FbqlSelect<SelectFromBase<PX.Objects.CS.LightDAC.PMTask, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.CS.LightDAC.Contract>.On<BqlOperand<
  #nullable enable
  PX.Objects.CS.LightDAC.PMTask.projectID, IBqlInt>.IsEqual<
  #nullable disable
  PX.Objects.CS.LightDAC.Contract.contractID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PX.Objects.CS.LightDAC.Contract.baseType, 
  #nullable disable
  Equal<CTPRType.project>>>>>.And<BqlOperand<
  #nullable enable
  PX.Objects.CS.LightDAC.Contract.nonProject, IBqlBool>.IsEqual<
  #nullable disable
  False>>>.Order<By<BqlField<
  #nullable enable
  PX.Objects.CS.LightDAC.PMTask.taskCD, IBqlString>.Asc>>, 
  #nullable disable
  PX.Objects.CS.LightDAC.PMTask>.View taskSelect;
  public FbqlSelect<SelectFromBase<PX.Objects.IN.InventoryItem, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PMHistory>.On<BqlOperand<
  #nullable enable
  PX.Objects.IN.InventoryItem.inventoryID, IBqlInt>.IsEqual<
  #nullable disable
  PMHistory.inventoryID>>>>.Aggregate<To<GroupBy<PX.Objects.IN.InventoryItem.inventoryID>>>.Order<By<BqlField<
  #nullable enable
  PX.Objects.IN.InventoryItem.inventoryCD, IBqlString>.Asc>>, 
  #nullable disable
  PX.Objects.IN.InventoryItem>.View itemFromHistorySelect;
  public FbqlSelect<SelectFromBase<PX.Objects.IN.InventoryItem, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PMBudget>.On<BqlOperand<
  #nullable enable
  PX.Objects.IN.InventoryItem.inventoryID, IBqlInt>.IsEqual<
  #nullable disable
  PMBudget.inventoryID>>>>.Aggregate<To<GroupBy<PX.Objects.IN.InventoryItem.inventoryID>>>.Order<By<BqlField<
  #nullable enable
  PX.Objects.IN.InventoryItem.inventoryCD, IBqlString>.Asc>>, 
  #nullable disable
  PX.Objects.IN.InventoryItem>.View itemFromBudgetSelect;
  public FbqlSelect<SelectFromBase<PMCostCode, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PMHistory>.On<BqlOperand<
  #nullable enable
  PMCostCode.costCodeID, IBqlInt>.IsEqual<
  #nullable disable
  PMHistory.costCodeID>>>>.Aggregate<To<GroupBy<PMCostCode.costCodeID>>>.Order<By<BqlField<
  #nullable enable
  PMCostCode.costCodeCD, IBqlString>.Asc>>, 
  #nullable disable
  PMCostCode>.View costCodeFromHistorySelect;
  public FbqlSelect<SelectFromBase<PMCostCode, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PMBudget>.On<BqlOperand<
  #nullable enable
  PMCostCode.costCodeID, IBqlInt>.IsEqual<
  #nullable disable
  PMBudget.costCodeID>>>>.Aggregate<To<GroupBy<PMCostCode.costCodeID>>>.Order<By<BqlField<
  #nullable enable
  PMCostCode.costCodeCD, IBqlString>.Asc>>, 
  #nullable disable
  PMCostCode>.View costCodeFromBudgetSelect;
  private static readonly IDictionary<string, RMReportReaderPM.Keys> _keysDictionary = (IDictionary<string, RMReportReaderPM.Keys>) Enum.GetValues(typeof (RMReportReaderPM.Keys)).Cast<RMReportReaderPM.Keys>().ToDictionary<RMReportReaderPM.Keys, string, RMReportReaderPM.Keys>((Func<RMReportReaderPM.Keys, string>) (e => e.ToString()), (Func<RMReportReaderPM.Keys, RMReportReaderPM.Keys>) (e => e));
  private ArmDATA _Data;

  [PXMergeAttributes]
  [PXDBInt(IsKey = true)]
  protected virtual void PMTask_ProjectID_CacheAttached(PXCache sender)
  {
  }

  [PXOverride]
  public void Clear(Action del)
  {
    del();
    this._historyLoaded = (HashSet<int>) null;
    this._accountGroupsRangeCache = (RMReportRange<PMAccountGroup>) null;
    this._projectsRangeCache = (RMReportRange<PX.Objects.CS.LightDAC.Contract>) null;
    this._tasksRangeCache = (RMReportRange<PX.Objects.CS.LightDAC.PMTask>) null;
    this._itemRangeCache = (RMReportRange<PX.Objects.IN.InventoryItem>) null;
    this._costCodeRangeCache = (RMReportRange<PMCostCode>) null;
  }

  public virtual void PMEnsureInitialized()
  {
    if (this._historyLoaded != null)
      return;
    this._reportPeriods = new RMReportPeriods<PMHistory>((PXGraph) ((PXGraphExtension<RMReportReader>) this).Base);
    ((PXSelectBase) this.accountGroupSelect).View.Clear();
    ((PXSelectBase) this.accountGroupSelect).Cache.Clear();
    ((PXSelectBase) this.projectSelect).View.Clear();
    ((PXSelectBase) this.projectSelect).Cache.Clear();
    ((PXSelectBase) this.taskSelect).View.Clear();
    ((PXSelectBase) this.taskSelect).Cache.Clear();
    ((PXSelectBase) this.itemFromHistorySelect).View.Clear();
    ((PXSelectBase) this.itemFromHistorySelect).Cache.Clear();
    ((PXSelectBase) this.itemFromBudgetSelect).View.Clear();
    ((PXSelectBase) this.itemFromBudgetSelect).Cache.Clear();
    ((PXSelectBase) this.costCodeFromHistorySelect).View.Clear();
    ((PXSelectBase) this.costCodeFromHistorySelect).Cache.Clear();
    ((PXSelectBase) this.costCodeFromBudgetSelect).View.Clear();
    ((PXSelectBase) this.costCodeFromBudgetSelect).Cache.Clear();
    this._budgetByKey = new Dictionary<BudgetKeyTuple, PMBudget>();
    foreach (PXResult<PMBudget, PX.Objects.CS.LightDAC.PMTask> selectBudgetRecord in this.SelectBudgetRecords())
    {
      PMBudget pmBudget1 = PXResult<PMBudget, PX.Objects.CS.LightDAC.PMTask>.op_Implicit(selectBudgetRecord);
      Dictionary<BudgetKeyTuple, PMBudget> budgetByKey = this._budgetByKey;
      int? nullable = pmBudget1.ProjectID;
      int projectID = nullable.Value;
      nullable = pmBudget1.ProjectTaskID;
      int projectTaskID = nullable.Value;
      nullable = pmBudget1.AccountGroupID;
      int accountGroupID = nullable.Value;
      nullable = pmBudget1.InventoryID;
      int inventoryID = nullable.Value;
      nullable = pmBudget1.CostCodeID;
      int costCodeID = nullable.Value;
      BudgetKeyTuple key = new BudgetKeyTuple(projectID, projectTaskID, accountGroupID, inventoryID, costCodeID);
      PMBudget pmBudget2 = pmBudget1;
      budgetByKey.Add(key, pmBudget2);
    }
    if (((PXSelectBase<RMReport>) ((PXGraphExtension<RMReportReader>) this).Base.Report).Current.ApplyRestrictionGroups.GetValueOrDefault())
    {
      ((PXSelectBase<PX.Objects.CS.LightDAC.Contract>) this.projectSelect).WhereAnd<Where<Match<Current<AccessInfo.userName>>>>();
      ((PXSelectBase<PX.Objects.CS.LightDAC.PMTask>) this.taskSelect).WhereAnd<Where<Match<Current<AccessInfo.userName>>>>();
      ((PXSelectBase<PX.Objects.IN.InventoryItem>) this.itemFromHistorySelect).WhereAnd<Where<Match<Current<AccessInfo.userName>>>>();
      ((PXSelectBase<PX.Objects.IN.InventoryItem>) this.itemFromBudgetSelect).WhereAnd<Where<Match<Current<AccessInfo.userName>>>>();
    }
    ((IEnumerable<PXResult<PMAccountGroup>>) ((PXSelectBase<PMAccountGroup>) this.accountGroupSelect).Select(Array.Empty<object>())).ToList<PXResult<PMAccountGroup>>();
    ((IEnumerable<PXResult<PX.Objects.CS.LightDAC.Contract>>) ((PXSelectBase<PX.Objects.CS.LightDAC.Contract>) this.projectSelect).Select(Array.Empty<object>())).ToList<PXResult<PX.Objects.CS.LightDAC.Contract>>();
    ((IEnumerable<PXResult<PX.Objects.CS.LightDAC.PMTask>>) ((PXSelectBase<PX.Objects.CS.LightDAC.PMTask>) this.taskSelect).Select(Array.Empty<object>())).ToList<PXResult<PX.Objects.CS.LightDAC.PMTask>>();
    foreach (PXResult<PX.Objects.IN.InventoryItem> pxResult in (IEnumerable<PXResult<PX.Objects.IN.InventoryItem>>) ((IQueryable<PXResult<PX.Objects.IN.InventoryItem>>) ((PXSelectBase<PX.Objects.IN.InventoryItem>) this.itemFromHistorySelect).Select(Array.Empty<object>())).Union<PXResult<PX.Objects.IN.InventoryItem>>((IEnumerable<PXResult<PX.Objects.IN.InventoryItem>>) ((PXSelectBase<PX.Objects.IN.InventoryItem>) this.itemFromBudgetSelect).Select(Array.Empty<object>())))
      ((PXSelectBase) this.itemFromHistorySelect).Cache.SetStatus((object) PXResult<PX.Objects.IN.InventoryItem>.op_Implicit(pxResult), (PXEntryStatus) 0);
    foreach (PXResult<PMCostCode> pxResult in (IEnumerable<PXResult<PMCostCode>>) ((IQueryable<PXResult<PMCostCode>>) ((PXSelectBase<PMCostCode>) this.costCodeFromHistorySelect).Select(Array.Empty<object>())).Union<PXResult<PMCostCode>>((IEnumerable<PXResult<PMCostCode>>) ((PXSelectBase<PMCostCode>) this.costCodeFromBudgetSelect).Select(Array.Empty<object>())))
      ((PXSelectBase) this.costCodeFromHistorySelect).Cache.SetStatus((object) PXResult<PMCostCode>.op_Implicit(pxResult), (PXEntryStatus) 0);
    this._historySegments = new HashSet<PMHistoryKeyTuple>();
    this._pmhistoryPeriodsNested = new PMHistoryHierDict();
    this._historyLoaded = new HashSet<int>();
    this._accountGroupsRangeCache = new RMReportRange<PMAccountGroup>((PXGraph) ((PXGraphExtension<RMReportReader>) this).Base, "ACCGROUP", RMReportConstants.WildcardMode.Fixed, RMReportConstants.BetweenMode.Fixed);
    this._projectsRangeCache = new RMReportRange<PX.Objects.CS.LightDAC.Contract>((PXGraph) ((PXGraphExtension<RMReportReader>) this).Base, "PROJECT", RMReportConstants.WildcardMode.Fixed, RMReportConstants.BetweenMode.Fixed);
    this._tasksRangeCache = new RMReportRange<PX.Objects.CS.LightDAC.PMTask>((PXGraph) ((PXGraphExtension<RMReportReader>) this).Base, "PROTASK", RMReportConstants.WildcardMode.Normal, RMReportConstants.BetweenMode.Fixed);
    this._itemRangeCache = new RMReportRange<PX.Objects.IN.InventoryItem>((PXGraph) ((PXGraphExtension<RMReportReader>) this).Base, "INVENTORY", RMReportConstants.WildcardMode.Fixed, RMReportConstants.BetweenMode.Fixed);
    this._costCodeRangeCache = new RMReportRange<PMCostCode>((PXGraph) ((PXGraphExtension<RMReportReader>) this).Base, "COSTCODE", RMReportConstants.WildcardMode.Fixed, RMReportConstants.BetweenMode.Fixed);
    this._accountGroupMask = ((PXGraph) ((PXGraphExtension<RMReportReader>) this).Base).Caches[typeof (PMAccountGroup)].GetStateExt<PMAccountGroup.groupCD>((object) null) is PXStringState stateExt1 ? stateExt1.InputMask : (string) null;
    this._projectMask = ((PXGraph) ((PXGraphExtension<RMReportReader>) this).Base).Caches[typeof (PX.Objects.CS.LightDAC.Contract)].GetStateExt<PX.Objects.CS.LightDAC.Contract.contractCD>((object) null) is PXStringState stateExt2 ? stateExt2.InputMask : (string) null;
    this._projectTaskMask = ((PXGraph) ((PXGraphExtension<RMReportReader>) this).Base).Caches[typeof (PX.Objects.CS.LightDAC.PMTask)].GetStateExt<PX.Objects.CS.LightDAC.PMTask.taskCD>((object) null) is PXStringState stateExt3 ? stateExt3.InputMask : (string) null;
    this._inventoryMask = ((PXGraph) ((PXGraphExtension<RMReportReader>) this).Base).Caches[typeof (PX.Objects.IN.InventoryItem)].GetStateExt<PX.Objects.IN.InventoryItem.inventoryCD>((object) null) is PXStringState stateExt4 ? stateExt4.InputMask : (string) null;
  }

  public virtual PXResultset<PMBudget> SelectBudgetRecords()
  {
    FbqlSelect<SelectFromBase<PMBudget, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.CS.LightDAC.PMTask>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMBudget.projectID, Equal<PX.Objects.CS.LightDAC.PMTask.projectID>>>>>.And<BqlOperand<PMBudget.projectTaskID, IBqlInt>.IsEqual<PX.Objects.CS.LightDAC.PMTask.taskID>>>>>, PMBudget>.View view = new FbqlSelect<SelectFromBase<PMBudget, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.CS.LightDAC.PMTask>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMBudget.projectID, Equal<PX.Objects.CS.LightDAC.PMTask.projectID>>>>>.And<BqlOperand<PMBudget.projectTaskID, IBqlInt>.IsEqual<PX.Objects.CS.LightDAC.PMTask.taskID>>>>>, PMBudget>.View((PXGraph) ((PXGraphExtension<RMReportReader>) this).Base);
    using (new PXFieldScope(((PXSelectBase) view).View, new Type[28]
    {
      typeof (PMBudget.projectID),
      typeof (PMBudget.projectTaskID),
      typeof (PMBudget.accountGroupID),
      typeof (PMBudget.inventoryID),
      typeof (PMBudget.costCodeID),
      typeof (PMBudget.qty),
      typeof (PMBudget.curyAmount),
      typeof (PMBudget.revisedQty),
      typeof (PMBudget.curyRevisedAmount),
      typeof (PMBudget.curyCommittedOrigAmount),
      typeof (PMBudget.committedOrigAmount),
      typeof (PMBudget.curyCommittedAmount),
      typeof (PMBudget.committedAmount),
      typeof (PMBudget.curyCommittedInvoicedAmount),
      typeof (PMBudget.committedInvoicedQty),
      typeof (PMBudget.curyCommittedOpenAmount),
      typeof (PMBudget.committedOpenQty),
      typeof (PMBudget.committedOrigQty),
      typeof (PMBudget.committedQty),
      typeof (PMBudget.committedReceivedQty),
      typeof (PMBudget.actualQty),
      typeof (PMBudget.actualAmount),
      typeof (PX.Objects.CS.LightDAC.PMTask.taskID),
      typeof (PX.Objects.CS.LightDAC.PMTask.taskCD),
      typeof (PMBudget.amount),
      typeof (PMBudget.revisedAmount),
      typeof (PMBudget.committedOpenAmount),
      typeof (PMBudget.committedInvoicedAmount)
    }))
      return ((PXSelectBase<PMBudget>) view).Select(Array.Empty<object>());
  }

  public virtual void NormalizeDataSource(RMDataSourcePM dsPM)
  {
    if (dsPM.StartAccountGroup != null && dsPM.StartAccountGroup.TrimEnd() == "")
      dsPM.StartAccountGroup = (string) null;
    if (dsPM.EndAccountGroup != null && dsPM.EndAccountGroup.TrimEnd() == "")
      dsPM.EndAccountGroup = (string) null;
    if (dsPM.StartProject != null && dsPM.StartProject.TrimEnd() == "")
      dsPM.StartProject = (string) null;
    if (dsPM.EndProject != null && dsPM.EndProject.TrimEnd() == "")
      dsPM.EndProject = (string) null;
    if (dsPM.StartProjectTask != null && dsPM.StartProjectTask.TrimEnd() == "")
      dsPM.StartProjectTask = (string) null;
    if (dsPM.EndProjectTask != null && dsPM.EndProjectTask.TrimEnd() == "")
      dsPM.EndProjectTask = (string) null;
    if (dsPM.StartInventory != null && dsPM.StartInventory.TrimEnd() == "")
      dsPM.StartInventory = (string) null;
    if (dsPM.EndInventory == null || !(dsPM.EndInventory.TrimEnd() == ""))
      return;
    dsPM.EndInventory = (string) null;
  }

  public void ProcessPMResultset(PXResultset<PMHistory> resultset)
  {
    foreach (PXResult<PMHistory, PX.Objects.CS.LightDAC.PMTask> pxResult in resultset)
    {
      PMHistory pmHistory1 = PXResult<PMHistory, PX.Objects.CS.LightDAC.PMTask>.op_Implicit(pxResult);
      PX.Objects.CS.LightDAC.PMTask pmTask = PXResult<PMHistory, PX.Objects.CS.LightDAC.PMTask>.op_Implicit(pxResult);
      (int, string, int, (int, int)) valueTuple1;
      ref (int, string, int, (int, int)) local = ref valueTuple1;
      int? nullable1 = pmHistory1.AccountGroupID;
      int num1 = nullable1.Value;
      string taskCd = pmTask.TaskCD;
      nullable1 = pmHistory1.CostCodeID;
      int num2 = nullable1.Value;
      nullable1 = pmHistory1.ProjectID;
      int num3 = nullable1.Value;
      nullable1 = pmHistory1.InventoryID;
      int num4 = nullable1.Value;
      (int, int) valueTuple2 = (num3, num4);
      local = (num1, taskCd, num2, valueTuple2);
      Dictionary<string, PMHistory> dictionary;
      if (this._pmhistoryPeriodsNested.TryGetValueNested(valueTuple1, ref dictionary))
      {
        PMHistory pmHistory2;
        if (dictionary.TryGetValue(pmHistory1.PeriodID, out pmHistory2))
        {
          PMHistory pmHistory3 = pmHistory2;
          Decimal? nullable2 = pmHistory3.TranPTDAmount;
          Decimal? nullable3 = pmHistory1.TranPTDAmount;
          pmHistory3.TranPTDAmount = nullable2.HasValue & nullable3.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
          PMHistory pmHistory4 = pmHistory2;
          nullable3 = pmHistory4.TranPTDCuryAmount;
          nullable2 = pmHistory1.TranPTDCuryAmount;
          pmHistory4.TranPTDCuryAmount = nullable3.HasValue & nullable2.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
          PMHistory pmHistory5 = pmHistory2;
          nullable2 = pmHistory5.TranPTDQty;
          nullable3 = pmHistory1.TranPTDQty;
          pmHistory5.TranPTDQty = nullable2.HasValue & nullable3.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
          PMHistory pmHistory6 = pmHistory2;
          nullable3 = pmHistory6.TranYTDAmount;
          nullable2 = pmHistory1.TranYTDAmount;
          pmHistory6.TranYTDAmount = nullable3.HasValue & nullable2.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
          PMHistory pmHistory7 = pmHistory2;
          nullable2 = pmHistory7.TranYTDCuryAmount;
          nullable3 = pmHistory1.TranYTDCuryAmount;
          pmHistory7.TranYTDCuryAmount = nullable2.HasValue & nullable3.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
          PMHistory pmHistory8 = pmHistory2;
          nullable3 = pmHistory8.TranYTDQty;
          nullable2 = pmHistory1.TranYTDQty;
          pmHistory8.TranYTDQty = nullable3.HasValue & nullable2.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
        }
        else
          dictionary.Add(pmHistory1.PeriodID, pmHistory1);
      }
      else
        this._pmhistoryPeriodsNested.AddNested(valueTuple1, new Dictionary<string, PMHistory>()
        {
          {
            pmHistory1.PeriodID,
            pmHistory1
          }
        });
      this._historySegments.Add(new PMHistoryKeyTuple(0, string.Empty, pmHistory1.AccountGroupID.Value, 0, 0));
      HashSet<PMHistoryKeyTuple> historySegments1 = this._historySegments;
      int? nullable4 = pmHistory1.ProjectID;
      int projectID = nullable4.Value;
      string empty = string.Empty;
      nullable4 = pmHistory1.AccountGroupID;
      int accountGroupID = nullable4.Value;
      PMHistoryKeyTuple pmHistoryKeyTuple1 = new PMHistoryKeyTuple(projectID, empty, accountGroupID, 0, 0);
      historySegments1.Add(pmHistoryKeyTuple1);
      HashSet<PMHistoryKeyTuple> historySegments2 = this._historySegments;
      nullable4 = pmHistory1.ProjectID;
      PMHistoryKeyTuple pmHistoryKeyTuple2 = new PMHistoryKeyTuple(nullable4.Value, pmTask.TaskCD, pmHistory1.AccountGroupID.Value, 0, 0);
      historySegments2.Add(pmHistoryKeyTuple2);
    }
  }

  public void UpdateHistorySegmentsWithRecordsFromBudget()
  {
    foreach (PXResult<PMBudget, PX.Objects.CS.LightDAC.PMTask> selectBudgetRecord in this.SelectBudgetRecords())
    {
      PMBudget pmBudget = PXResult<PMBudget, PX.Objects.CS.LightDAC.PMTask>.op_Implicit(selectBudgetRecord);
      PX.Objects.CS.LightDAC.PMTask pmTask = PXResult<PMBudget, PX.Objects.CS.LightDAC.PMTask>.op_Implicit(selectBudgetRecord);
      (int, string, int, (int, int)) valueTuple1;
      ref (int, string, int, (int, int)) local = ref valueTuple1;
      int? nullable = pmBudget.AccountGroupID;
      int num1 = nullable.Value;
      string taskCd1 = pmTask.TaskCD;
      nullable = pmBudget.CostCodeID;
      int num2 = nullable.Value;
      nullable = pmBudget.ProjectID;
      int num3 = nullable.Value;
      nullable = pmBudget.InventoryID;
      int num4 = nullable.Value;
      (int, int) valueTuple2 = (num3, num4);
      local = (num1, taskCd1, num2, valueTuple2);
      Dictionary<string, PMHistory> dictionary;
      if (!this._pmhistoryPeriodsNested.TryGetValueNested(valueTuple1, ref dictionary))
        this._pmhistoryPeriodsNested.AddNested(valueTuple1, new Dictionary<string, PMHistory>());
      HashSet<PMHistoryKeyTuple> historySegments1 = this._historySegments;
      string empty1 = string.Empty;
      nullable = pmBudget.AccountGroupID;
      int accountGroupID1 = nullable.Value;
      PMHistoryKeyTuple pmHistoryKeyTuple1 = new PMHistoryKeyTuple(0, empty1, accountGroupID1, 0, 0);
      historySegments1.Add(pmHistoryKeyTuple1);
      HashSet<PMHistoryKeyTuple> historySegments2 = this._historySegments;
      nullable = pmBudget.ProjectID;
      int projectID1 = nullable.Value;
      string empty2 = string.Empty;
      nullable = pmBudget.AccountGroupID;
      int accountGroupID2 = nullable.Value;
      PMHistoryKeyTuple pmHistoryKeyTuple2 = new PMHistoryKeyTuple(projectID1, empty2, accountGroupID2, 0, 0);
      historySegments2.Add(pmHistoryKeyTuple2);
      HashSet<PMHistoryKeyTuple> historySegments3 = this._historySegments;
      nullable = pmBudget.ProjectID;
      int projectID2 = nullable.Value;
      string taskCd2 = pmTask.TaskCD;
      nullable = pmBudget.AccountGroupID;
      int accountGroupID3 = nullable.Value;
      PMHistoryKeyTuple pmHistoryKeyTuple3 = new PMHistoryKeyTuple(projectID2, taskCd2, accountGroupID3, 0, 0);
      historySegments3.Add(pmHistoryKeyTuple3);
    }
  }

  [PXOverride]
  public virtual object GetHistoryValue(
    ARmDataSet dataSet,
    bool drilldown,
    Func<ARmDataSet, bool, object> del)
  {
    if (!(((PXSelectBase<RMReport>) ((PXGraphExtension<RMReportReader>) this).Base.Report).Current.Type == "PM"))
      return del(dataSet, drilldown);
    RMDataSource current = ((PXSelectBase<RMDataSource>) ((PXGraphExtension<RMReportReader>) this).Base.DataSourceByID).Current;
    RMDataSourcePM extension1 = ((PXGraph) ((PXGraphExtension<RMReportReader>) this).Base).Caches[typeof (RMDataSource)].GetExtension<RMDataSourcePM>((object) current);
    current.AmountType = (short?) dataSet[(object) RMReportReaderGL.Keys.AmountType];
    RMDataSourcePM rmDataSourcePm1 = extension1;
    if (!(dataSet[(object) RMReportReaderPM.Keys.StartAccountGroup] is string str1))
      str1 = "";
    rmDataSourcePm1.StartAccountGroup = str1;
    RMDataSourcePM rmDataSourcePm2 = extension1;
    if (!(dataSet[(object) RMReportReaderPM.Keys.EndAccountGroup] is string str2))
      str2 = "";
    rmDataSourcePm2.EndAccountGroup = str2;
    RMDataSourcePM rmDataSourcePm3 = extension1;
    if (!(dataSet[(object) RMReportReaderPM.Keys.StartProject] is string str3))
      str3 = "";
    rmDataSourcePm3.StartProject = str3;
    RMDataSourcePM rmDataSourcePm4 = extension1;
    if (!(dataSet[(object) RMReportReaderPM.Keys.EndProject] is string str4))
      str4 = "";
    rmDataSourcePm4.EndProject = str4;
    RMDataSourcePM rmDataSourcePm5 = extension1;
    if (!(dataSet[(object) RMReportReaderPM.Keys.StartProjectTask] is string str5))
      str5 = "";
    rmDataSourcePm5.StartProjectTask = str5;
    RMDataSourcePM rmDataSourcePm6 = extension1;
    if (!(dataSet[(object) RMReportReaderPM.Keys.EndProjectTask] is string str6))
      str6 = "";
    rmDataSourcePm6.EndProjectTask = str6;
    RMDataSourcePM rmDataSourcePm7 = extension1;
    if (!(dataSet[(object) RMReportReaderPM.Keys.StartInventory] is string str7))
      str7 = "";
    rmDataSourcePm7.StartInventory = str7;
    RMDataSourcePM rmDataSourcePm8 = extension1;
    if (!(dataSet[(object) RMReportReaderPM.Keys.EndInventory] is string str8))
      str8 = "";
    rmDataSourcePm8.EndInventory = str8;
    RMDataSourceGL extension2 = ((PXGraph) ((PXGraphExtension<RMReportReader>) this).Base).Caches[typeof (RMDataSource)].GetExtension<RMDataSourceGL>((object) current);
    RMDataSourceGL rmDataSourceGl1 = extension2;
    if (!(dataSet[(object) RMReportReaderGL.Keys.StartBranch] is string str9))
      str9 = "";
    rmDataSourceGl1.StartBranch = str9;
    RMDataSourceGL rmDataSourceGl2 = extension2;
    if (!(dataSet[(object) RMReportReaderGL.Keys.EndBranch] is string str10))
      str10 = "";
    rmDataSourceGl2.EndBranch = str10;
    RMDataSourceGL rmDataSourceGl3 = extension2;
    if (!(dataSet[(object) RMReportReaderGL.Keys.EndPeriod] is string str11))
      str11 = "";
    string str12;
    if (str11.Length <= 2)
    {
      str12 = "    ";
    }
    else
    {
      if (!(dataSet[(object) RMReportReaderGL.Keys.EndPeriod] is string str13))
        str13 = "";
      str12 = (str13.Substring(2) + "    ").Substring(0, 4);
    }
    if (!(dataSet[(object) RMReportReaderGL.Keys.EndPeriod] is string str14))
      str14 = "";
    string str15;
    if (str14.Length <= 2)
    {
      str15 = dataSet[(object) RMReportReaderGL.Keys.EndPeriod] is string data1 ? data1 : "";
    }
    else
    {
      if (!(dataSet[(object) RMReportReaderGL.Keys.EndPeriod] is string str16))
        str16 = "";
      str15 = str16.Substring(0, 2);
    }
    string str17 = str12 + str15;
    rmDataSourceGl3.EndPeriod = str17;
    RMDataSourceGL rmDataSourceGl4 = extension2;
    int? nullable1 = (int?) dataSet[(object) RMReportReaderGL.Keys.EndOffset];
    short? nullable2 = nullable1.HasValue ? new short?((short) nullable1.GetValueOrDefault()) : new short?();
    rmDataSourceGl4.EndPeriodOffset = nullable2;
    RMDataSourceGL rmDataSourceGl5 = extension2;
    nullable1 = (int?) dataSet[(object) RMReportReaderGL.Keys.EndYearOffset];
    short? nullable3 = nullable1.HasValue ? new short?((short) nullable1.GetValueOrDefault()) : new short?();
    rmDataSourceGl5.EndPeriodYearOffset = nullable3;
    RMDataSourceGL rmDataSourceGl6 = extension2;
    if (!(dataSet[(object) RMReportReaderGL.Keys.StartPeriod] is string str18))
      str18 = "";
    string str19;
    if (str18.Length <= 2)
    {
      str19 = "    ";
    }
    else
    {
      if (!(dataSet[(object) RMReportReaderGL.Keys.StartPeriod] is string str20))
        str20 = "";
      str19 = (str20.Substring(2) + "    ").Substring(0, 4);
    }
    if (!(dataSet[(object) RMReportReaderGL.Keys.StartPeriod] is string str21))
      str21 = "";
    string str22;
    if (str21.Length <= 2)
    {
      str22 = dataSet[(object) RMReportReaderGL.Keys.StartPeriod] is string data2 ? data2 : "";
    }
    else
    {
      if (!(dataSet[(object) RMReportReaderGL.Keys.StartPeriod] is string str23))
        str23 = "";
      str22 = str23.Substring(0, 2);
    }
    string str24 = str19 + str22;
    rmDataSourceGl6.StartPeriod = str24;
    RMDataSourceGL rmDataSourceGl7 = extension2;
    nullable1 = (int?) dataSet[(object) RMReportReaderGL.Keys.StartOffset];
    short? nullable4 = nullable1.HasValue ? new short?((short) nullable1.GetValueOrDefault()) : new short?();
    rmDataSourceGl7.StartPeriodOffset = nullable4;
    RMDataSourceGL rmDataSourceGl8 = extension2;
    nullable1 = (int?) dataSet[(object) RMReportReaderGL.Keys.StartYearOffset];
    short? nullable5 = nullable1.HasValue ? new short?((short) nullable1.GetValueOrDefault()) : new short?();
    rmDataSourceGl8.StartPeriodYearOffset = nullable5;
    List<object[]> splitret = (List<object[]>) null;
    if (current.Expand != "N")
      splitret = new List<object[]>();
    short? amountType = current.AmountType;
    if (amountType.HasValue)
    {
      amountType = current.AmountType;
      nullable1 = amountType.HasValue ? new int?((int) amountType.GetValueOrDefault()) : new int?();
      int num = 0;
      if (!(nullable1.GetValueOrDefault() == num & nullable1.HasValue))
      {
        this.PMEnsureInitialized();
        this.EnsureHistoryLoaded(extension1);
        this.NormalizeDataSource(extension1);
        List<PMAccountGroup> itemsInRange1 = this.GetItemsInRange<PMAccountGroup>(dataSet);
        List<PX.Objects.CS.LightDAC.Contract> projects = this.GetItemsInRange<PX.Objects.CS.LightDAC.Contract>(dataSet);
        List<PX.Objects.CS.LightDAC.PMTask> pmTaskList = this.GetItemsInRange<PX.Objects.CS.LightDAC.PMTask>(dataSet);
        List<PX.Objects.IN.InventoryItem> itemsInRange2 = this.GetItemsInRange<PX.Objects.IN.InventoryItem>(dataSet);
        List<PMCostCode> itemsInRange3 = this.GetItemsInRange<PMCostCode>(dataSet);
        if (current.Expand == "G")
        {
          foreach (PMAccountGroup pmAccountGroup in itemsInRange1)
          {
            ARmDataSet armDataSet = new ARmDataSet(dataSet);
            armDataSet[(object) RMReportReaderPM.Keys.StartAccountGroup] = armDataSet[(object) RMReportReaderPM.Keys.EndAccountGroup] = (object) pmAccountGroup.GroupCD;
            splitret.Add(new object[6]
            {
              (object) pmAccountGroup.GroupCD,
              (object) pmAccountGroup.Description,
              (object) 0M,
              (object) armDataSet,
              (object) string.Empty,
              (object) Mask.Format(this._accountGroupMask, pmAccountGroup.GroupCD)
            });
          }
        }
        else if (current.Expand == "P")
        {
          foreach (PX.Objects.CS.LightDAC.Contract contract in projects)
          {
            ARmDataSet armDataSet = new ARmDataSet(dataSet);
            armDataSet[(object) RMReportReaderPM.Keys.StartProject] = armDataSet[(object) RMReportReaderPM.Keys.EndProject] = (object) contract.ContractCD;
            splitret.Add(new object[6]
            {
              (object) contract.ContractCD,
              (object) contract.Description,
              (object) 0M,
              (object) armDataSet,
              (object) string.Empty,
              (object) Mask.Format(this._projectMask, contract.ContractCD)
            });
          }
        }
        else if (current.Expand == "T")
        {
          IGrouping<string, PX.Objects.CS.LightDAC.PMTask>[] array = pmTaskList.Where<PX.Objects.CS.LightDAC.PMTask>((Func<PX.Objects.CS.LightDAC.PMTask, bool>) (t => projects.Any<PX.Objects.CS.LightDAC.Contract>((Func<PX.Objects.CS.LightDAC.Contract, bool>) (p =>
          {
            int? projectId = t.ProjectID;
            int? contractId = p.ContractID;
            return projectId.GetValueOrDefault() == contractId.GetValueOrDefault() & projectId.HasValue == contractId.HasValue;
          })))).GroupBy<PX.Objects.CS.LightDAC.PMTask, string>((Func<PX.Objects.CS.LightDAC.PMTask, string>) (t => t.TaskCD)).ToArray<IGrouping<string, PX.Objects.CS.LightDAC.PMTask>>();
          pmTaskList = new List<PX.Objects.CS.LightDAC.PMTask>();
          foreach (IGrouping<string, PX.Objects.CS.LightDAC.PMTask> grouping in array)
          {
            string key = grouping.Key;
            ARmDataSet armDataSet = new ARmDataSet(dataSet)
            {
              [(object) RMReportReaderPM.Keys.StartProjectTask] = (object) key,
              [(object) RMReportReaderPM.Keys.EndProjectTask] = (object) key
            };
            splitret.Add(new object[6]
            {
              (object) key,
              (object) grouping.Min<PX.Objects.CS.LightDAC.PMTask, string>((Func<PX.Objects.CS.LightDAC.PMTask, string>) (t => t.Description)),
              (object) 0M,
              (object) armDataSet,
              (object) string.Empty,
              (object) Mask.Format(this._projectTaskMask, key)
            });
            pmTaskList.AddRange((IEnumerable<PX.Objects.CS.LightDAC.PMTask>) grouping);
          }
        }
        else if (current.Expand == "I")
        {
          foreach (PX.Objects.IN.InventoryItem inventoryItem in itemsInRange2)
          {
            ARmDataSet armDataSet = new ARmDataSet(dataSet);
            armDataSet[(object) RMReportReaderPM.Keys.StartInventory] = armDataSet[(object) RMReportReaderPM.Keys.EndInventory] = (object) inventoryItem.InventoryCD;
            splitret.Add(new object[6]
            {
              (object) inventoryItem.InventoryCD,
              (object) inventoryItem.Descr,
              (object) 0M,
              (object) armDataSet,
              (object) string.Empty,
              (object) Mask.Format(this._inventoryMask, inventoryItem.InventoryCD)
            });
          }
        }
        return this.CalculateAndExpandValue(drilldown, current, extension2, extension1, dataSet, itemsInRange1, projects, pmTaskList, itemsInRange2, itemsInRange3, splitret);
      }
    }
    return (object) 0M;
  }

  private List<T> GetItemsInRange<T>(ARmDataSet dataSet)
  {
    return (List<T>) ((PXGraphExtension<RMReportReader>) this).Base.GetItemsInRange(typeof (T), dataSet);
  }

  [PXOverride]
  public virtual IEnumerable GetItemsInRange(
    Type table,
    ARmDataSet dataSet,
    Func<Type, ARmDataSet, IEnumerable> del)
  {
    if (table == typeof (PMAccountGroup))
      return (IEnumerable) this._accountGroupsRangeCache.GetItemsInRange(dataSet[(object) RMReportReaderPM.Keys.StartAccountGroup] as string, (Func<PMAccountGroup, string>) (group => group.GroupCD), (Action<PMAccountGroup, string>) ((group, code) => group.GroupCD = code));
    if (table == typeof (PX.Objects.CS.LightDAC.Contract))
      return (IEnumerable) this._projectsRangeCache.GetItemsInRange(dataSet[(object) RMReportReaderPM.Keys.StartProject] as string, (Func<PX.Objects.CS.LightDAC.Contract, string>) (project => project.ContractCD), (Action<PX.Objects.CS.LightDAC.Contract, string>) ((project, code) => project.ContractCD = code));
    if (table == typeof (PX.Objects.CS.LightDAC.PMTask))
      return (IEnumerable) this._tasksRangeCache.GetItemsInRange(dataSet[(object) RMReportReaderPM.Keys.StartProjectTask] as string, (Func<PX.Objects.CS.LightDAC.PMTask, string>) (task => task.TaskCD), (Action<PX.Objects.CS.LightDAC.PMTask, string>) ((task, code) => task.TaskCD = code));
    if (table == typeof (PX.Objects.IN.InventoryItem))
      return (IEnumerable) this._itemRangeCache.GetItemsInRange(dataSet[(object) RMReportReaderPM.Keys.StartInventory] as string, (Func<PX.Objects.IN.InventoryItem, string>) (item => item.InventoryCD), (Action<PX.Objects.IN.InventoryItem, string>) ((item, code) => item.InventoryCD = code));
    if (table == typeof (PMCostCode))
      return (IEnumerable) GraphHelper.RowCast<PMCostCode>(this._costCodeRangeCache.Cache.Cached).ToList<PMCostCode>();
    if (del != null)
      return del(table, dataSet);
    throw new NotSupportedException();
  }

  public object CalculateAndExpandValue(
    bool drilldown,
    RMDataSource ds,
    RMDataSourceGL dsGL,
    RMDataSourcePM dsPM,
    ARmDataSet dataSet,
    List<PMAccountGroup> accountGroups,
    List<PX.Objects.CS.LightDAC.Contract> projects,
    List<PX.Objects.CS.LightDAC.PMTask> tasks,
    List<PX.Objects.IN.InventoryItem> items,
    List<PMCostCode> costCodes,
    List<object[]> splitret)
  {
    RMReportReaderPM.SharedContextPM sharedContext = new RMReportReaderPM.SharedContextPM(this, drilldown, ds, dsGL, dsPM, dataSet, accountGroups, tasks, costCodes, items, projects, splitret);
    if (sharedContext.ParallelizeAccountGroups)
    {
      Parallel.For(0, sharedContext.AccountGroups.Count, sharedContext.ParallelOptions, new Action<int>(sharedContext.AccountGroupIterationNoClosures));
    }
    else
    {
      for (int accountGroupIndex = 0; accountGroupIndex < sharedContext.AccountGroups.Count; ++accountGroupIndex)
        RMReportReaderPM.AccountGroupIteration(sharedContext, accountGroupIndex);
    }
    if (drilldown)
    {
      IEnumerable<PXResult<PMHistory, PMBudget, PX.Objects.CS.LightDAC.Contract, PX.Objects.CS.LightDAC.PMTask, PMAccountGroup, PX.Objects.IN.InventoryItem, PMCostCode>> pxResults = sharedContext.DrilldownData.Values.Select<PXResult<PMHistory, PMBudget, PX.Objects.CS.LightDAC.Contract, PX.Objects.CS.LightDAC.PMTask, PMAccountGroup, PX.Objects.IN.InventoryItem, PMCostCode>, (PXResult<PMHistory, PMBudget, PX.Objects.CS.LightDAC.Contract, PX.Objects.CS.LightDAC.PMTask, PMAccountGroup, PX.Objects.IN.InventoryItem, PMCostCode>, string, string, string, string, string)>((Func<PXResult<PMHistory, PMBudget, PX.Objects.CS.LightDAC.Contract, PX.Objects.CS.LightDAC.PMTask, PMAccountGroup, PX.Objects.IN.InventoryItem, PMCostCode>, (PXResult<PMHistory, PMBudget, PX.Objects.CS.LightDAC.Contract, PX.Objects.CS.LightDAC.PMTask, PMAccountGroup, PX.Objects.IN.InventoryItem, PMCostCode>, string, string, string, string, string)>) (row => (row, ((PXResult) row).GetItem<PX.Objects.CS.LightDAC.Contract>()?.ContractCD, ((PXResult) row).GetItem<PX.Objects.CS.LightDAC.PMTask>()?.TaskCD, ((PXResult) row).GetItem<PMAccountGroup>()?.GroupCD, ((PXResult) row).GetItem<PX.Objects.IN.InventoryItem>()?.InventoryCD, ((PXResult) row).GetItem<PMCostCode>()?.CostCodeCD))).OrderBy<(PXResult<PMHistory, PMBudget, PX.Objects.CS.LightDAC.Contract, PX.Objects.CS.LightDAC.PMTask, PMAccountGroup, PX.Objects.IN.InventoryItem, PMCostCode>, string, string, string, string, string), string>((Func<(PXResult<PMHistory, PMBudget, PX.Objects.CS.LightDAC.Contract, PX.Objects.CS.LightDAC.PMTask, PMAccountGroup, PX.Objects.IN.InventoryItem, PMCostCode>, string, string, string, string, string), string>) (tuple => tuple.ProjectCD)).ThenBy<(PXResult<PMHistory, PMBudget, PX.Objects.CS.LightDAC.Contract, PX.Objects.CS.LightDAC.PMTask, PMAccountGroup, PX.Objects.IN.InventoryItem, PMCostCode>, string, string, string, string, string), string>((Func<(PXResult<PMHistory, PMBudget, PX.Objects.CS.LightDAC.Contract, PX.Objects.CS.LightDAC.PMTask, PMAccountGroup, PX.Objects.IN.InventoryItem, PMCostCode>, string, string, string, string, string), string>) (tuple => tuple.TaskCD)).ThenBy<(PXResult<PMHistory, PMBudget, PX.Objects.CS.LightDAC.Contract, PX.Objects.CS.LightDAC.PMTask, PMAccountGroup, PX.Objects.IN.InventoryItem, PMCostCode>, string, string, string, string, string), string>((Func<(PXResult<PMHistory, PMBudget, PX.Objects.CS.LightDAC.Contract, PX.Objects.CS.LightDAC.PMTask, PMAccountGroup, PX.Objects.IN.InventoryItem, PMCostCode>, string, string, string, string, string), string>) (tuple => tuple.AccGroupCD)).ThenBy<(PXResult<PMHistory, PMBudget, PX.Objects.CS.LightDAC.Contract, PX.Objects.CS.LightDAC.PMTask, PMAccountGroup, PX.Objects.IN.InventoryItem, PMCostCode>, string, string, string, string, string), string>((Func<(PXResult<PMHistory, PMBudget, PX.Objects.CS.LightDAC.Contract, PX.Objects.CS.LightDAC.PMTask, PMAccountGroup, PX.Objects.IN.InventoryItem, PMCostCode>, string, string, string, string, string), string>) (tuple => tuple.InventoryCD)).ThenBy<(PXResult<PMHistory, PMBudget, PX.Objects.CS.LightDAC.Contract, PX.Objects.CS.LightDAC.PMTask, PMAccountGroup, PX.Objects.IN.InventoryItem, PMCostCode>, string, string, string, string, string), string>((Func<(PXResult<PMHistory, PMBudget, PX.Objects.CS.LightDAC.Contract, PX.Objects.CS.LightDAC.PMTask, PMAccountGroup, PX.Objects.IN.InventoryItem, PMCostCode>, string, string, string, string, string), string>) (tuple => tuple.CostCodeCD)).Select<(PXResult<PMHistory, PMBudget, PX.Objects.CS.LightDAC.Contract, PX.Objects.CS.LightDAC.PMTask, PMAccountGroup, PX.Objects.IN.InventoryItem, PMCostCode>, string, string, string, string, string), PXResult<PMHistory, PMBudget, PX.Objects.CS.LightDAC.Contract, PX.Objects.CS.LightDAC.PMTask, PMAccountGroup, PX.Objects.IN.InventoryItem, PMCostCode>>((Func<(PXResult<PMHistory, PMBudget, PX.Objects.CS.LightDAC.Contract, PX.Objects.CS.LightDAC.PMTask, PMAccountGroup, PX.Objects.IN.InventoryItem, PMCostCode>, string, string, string, string, string), PXResult<PMHistory, PMBudget, PX.Objects.CS.LightDAC.Contract, PX.Objects.CS.LightDAC.PMTask, PMAccountGroup, PX.Objects.IN.InventoryItem, PMCostCode>>) (tuple => tuple.Row));
      PXResultset<PMHistory, PMBudget, PX.Objects.CS.LightDAC.Contract, PX.Objects.CS.LightDAC.PMTask, PMAccountGroup, PX.Objects.IN.InventoryItem, PMCostCode> andExpandValue = new PXResultset<PMHistory, PMBudget, PX.Objects.CS.LightDAC.Contract, PX.Objects.CS.LightDAC.PMTask, PMAccountGroup, PX.Objects.IN.InventoryItem, PMCostCode>();
      ((PXResultset<PMHistory>) andExpandValue).AddRange((IEnumerable<PXResult<PMHistory>>) pxResults);
      return (object) andExpandValue;
    }
    return sharedContext.DataSource.Expand != "N" ? (object) sharedContext.SplitReturn : (object) sharedContext.TotalAmount;
  }

  private static void AccountGroupIteration(
    RMReportReaderPM.SharedContextPM sharedContext,
    int accountGroupIndex)
  {
    PMAccountGroup accountGroup = sharedContext.AccountGroups[accountGroupIndex];
    NestedDictionary<string, int, (int, int), Dictionary<string, PMHistory>> accountGroupDict;
    if (!((Dictionary<int, NestedDictionary<string, int, (int, int), Dictionary<string, PMHistory>>>) sharedContext.This._pmhistoryPeriodsNested).TryGetValue(accountGroup.GroupID.Value, out accountGroupDict) || !sharedContext.This._historySegments.Contains(new PMHistoryKeyTuple(0, string.Empty, accountGroup.GroupID.Value, 0, 0)))
      return;
    RMReportReaderPM.TaskIterationContext taskIterationContext = new RMReportReaderPM.TaskIterationContext(sharedContext, accountGroup, accountGroupIndex, accountGroupDict);
    if (sharedContext.ParallelizeTasks)
    {
      Parallel.For(0, sharedContext.Tasks.Count, sharedContext.ParallelOptions, new Action<int>(taskIterationContext.TaskIterationNoClosures));
    }
    else
    {
      for (int taskIndex = 0; taskIndex < sharedContext.Tasks.Count; ++taskIndex)
        RMReportReaderPM.TaskIteration(in taskIterationContext, taskIndex);
    }
  }

  private static void TaskIteration(
    in RMReportReaderPM.TaskIterationContext taskIterationContext,
    int taskIndex)
  {
    RMReportReaderPM.SharedContextPM sharedContext = taskIterationContext.SharedContext;
    PX.Objects.CS.LightDAC.PMTask task = sharedContext.Tasks[taskIndex];
    NestedDictionary<int, (int, int), Dictionary<string, PMHistory>> taskDict;
    (PX.Objects.CS.LightDAC.Contract Project, int ProjectIndex) tuple;
    if (task == null || !((Dictionary<string, NestedDictionary<int, (int, int), Dictionary<string, PMHistory>>>) taskIterationContext.AccountGroupDict).TryGetValue(task.TaskCD, out taskDict) || !sharedContext.ProjectsDict.TryGetValue(task.ProjectID, out tuple))
      return;
    (PX.Objects.CS.LightDAC.Contract contract, int num) = tuple;
    PMAccountGroup currentAccountGroup = taskIterationContext.CurrentAccountGroup;
    if (!sharedContext.This._historySegments.Contains(new PMHistoryKeyTuple(contract.ContractID.Value, string.Empty, currentAccountGroup.GroupID.Value, 0, 0)) || !sharedContext.This._historySegments.Contains(new PMHistoryKeyTuple(contract.ContractID.Value, task.TaskCD, currentAccountGroup.GroupID.Value, 0, 0)))
      return;
    if (sharedContext.DataSource.Expand != "T")
    {
      int? projectId = task.ProjectID;
      int? contractId = contract.ContractID;
      if (!(projectId.GetValueOrDefault() == contractId.GetValueOrDefault() & projectId.HasValue == contractId.HasValue))
        return;
    }
    RMReportReaderPM.CostCodeIterationContextPM costCodeIterationContext = new RMReportReaderPM.CostCodeIterationContextPM(sharedContext, taskIterationContext.CurrentAccountGroup, taskIterationContext.AccountGroupIndex, task, taskIndex, contract, num, taskDict);
    if (sharedContext.ParallelizeCostCodes)
    {
      Parallel.For(0, sharedContext.CostCodes.Count, sharedContext.ParallelOptions, new Action<int>(costCodeIterationContext.CostCodeIterationNoClosures));
    }
    else
    {
      for (int costCodeIndex = 0; costCodeIndex < sharedContext.CostCodes.Count; ++costCodeIndex)
        RMReportReaderPM.CostCodeIteration(in costCodeIterationContext, costCodeIndex);
    }
  }

  private static void CostCodeIteration(
    in RMReportReaderPM.CostCodeIterationContextPM costCodeIterationContext,
    int costCodeIndex)
  {
    RMReportReaderPM.SharedContextPM sharedContext = costCodeIterationContext.SharedContext;
    PMCostCode costCode = sharedContext.CostCodes[costCodeIndex];
    Dictionary<(int, int), Dictionary<string, PMHistory>> costDict;
    if (!((Dictionary<int, Dictionary<(int, int), Dictionary<string, PMHistory>>>) costCodeIterationContext.TaskDict).TryGetValue(costCode.CostCodeID.Value, out costDict))
      return;
    RMReportReaderPM.InventoryItemIterationContextPM itemIterationContext = new RMReportReaderPM.InventoryItemIterationContextPM(in costCodeIterationContext, costCode, costDict);
    if (sharedContext.ParallelizeInvItems)
    {
      Parallel.For(0, sharedContext.Items.Count, sharedContext.ParallelOptions, new Action<int>(itemIterationContext.InvItemIterationNoClosures));
    }
    else
    {
      for (int itemIndex = 0; itemIndex < sharedContext.Items.Count; ++itemIndex)
        RMReportReaderPM.InvItemIteration(in itemIterationContext, itemIndex);
    }
  }

  private static void InvItemIteration(
    in RMReportReaderPM.InventoryItemIterationContextPM itemIterationContext,
    int itemIndex)
  {
    RMReportReaderPM.SharedContextPM sharedContext = itemIterationContext.SharedContext;
    PX.Objects.IN.InventoryItem currentItem = sharedContext.Items[itemIndex];
    PMAccountGroup currentAccountGroup = itemIterationContext.CurrentAccountGroup;
    PX.Objects.CS.LightDAC.Contract currentProject = itemIterationContext.CurrentProject;
    PX.Objects.CS.LightDAC.PMTask currentTask = itemIterationContext.CurrentTask;
    PMCostCode currentCostCode = itemIterationContext.CurrentCostCode;
    IReadOnlyCollection<PMHistory> pmHistories = (IReadOnlyCollection<PMHistory>) null;
    Dictionary<(int, int), Dictionary<string, PMHistory>> costDict = itemIterationContext.CostDict;
    int? nullable1 = currentProject.ContractID;
    int num1 = nullable1.Value;
    nullable1 = currentItem.InventoryID;
    int num2 = nullable1.Value;
    (int, int) key1 = (num1, num2);
    Dictionary<string, PMHistory> periodsForKey;
    ref Dictionary<string, PMHistory> local1 = ref periodsForKey;
    if (costDict.TryGetValue(key1, out local1))
      pmHistories = sharedContext.This.GetPeriodsToCalculate(periodsForKey, sharedContext.DataSource, sharedContext.DataSourceGL, sharedContext.Drilldown);
    if (pmHistories == null || pmHistories.Count == 0)
    {
      if (!sharedContext.This.IsBudgetValue(sharedContext.DataSource))
        return;
      PMHistory period = new PMHistory()
      {
        ProjectID = currentProject.ContractID,
        ProjectTaskID = currentTask.TaskID,
        AccountGroupID = currentAccountGroup.GroupID,
        InventoryID = currentItem.InventoryID,
        CostCodeID = currentCostCode.CostCodeID
      };
      BudgetKeyTuple key2;
      ref BudgetKeyTuple local2 = ref key2;
      int? nullable2 = currentProject.ContractID;
      int projectID = nullable2.Value;
      nullable2 = currentTask.TaskID;
      int projectTaskID = nullable2.Value;
      nullable2 = currentAccountGroup.GroupID;
      int accountGroupID = nullable2.Value;
      int? nullable3 = currentItem.InventoryID;
      int inventoryID = nullable3.Value;
      nullable3 = currentCostCode.CostCodeID;
      int costCodeID = nullable3.Value;
      local2 = new BudgetKeyTuple(projectID, projectTaskID, accountGroupID, inventoryID, costCodeID);
      Decimal amount = sharedContext.This.GetAmountFromPMBudget(sharedContext.DataSource, key2);
      RMReportReaderPM.ProcessAmount(sharedContext, in itemIterationContext, currentItem, itemIndex, period, in amount);
    }
    else
    {
      List<(Decimal, PMHistory)> valueTupleList = new List<(Decimal, PMHistory)>(pmHistories.Count);
      if (sharedContext.This.IsBudgetValue(sharedContext.DataSource))
      {
        BudgetKeyTuple key3;
        ref BudgetKeyTuple local3 = ref key3;
        int projectID = currentProject.ContractID.Value;
        int projectTaskID = currentTask.TaskID.Value;
        int accountGroupID = currentAccountGroup.GroupID.Value;
        int? nullable4 = currentItem.InventoryID;
        int inventoryID = nullable4.Value;
        nullable4 = currentCostCode.CostCodeID;
        int costCodeID = nullable4.Value;
        local3 = new BudgetKeyTuple(projectID, projectTaskID, accountGroupID, inventoryID, costCodeID);
        bool flag = true;
        foreach (PMHistory pmHistory in (IEnumerable<PMHistory>) pmHistories)
        {
          Decimal amountFromPmBudget = flag ? sharedContext.This.GetAmountFromPMBudget(sharedContext.DataSource, key3) : 0M;
          valueTupleList.Add((amountFromPmBudget, pmHistory));
          flag = false;
        }
      }
      else
      {
        foreach (PMHistory hist in (IEnumerable<PMHistory>) pmHistories)
        {
          Decimal amountFromPmHistory = RMReportReaderPM.GetAmountFromPMHistory(sharedContext.DataSource, hist);
          valueTupleList.Add((amountFromPmHistory, hist));
        }
      }
      foreach ((Decimal amount, PMHistory period) in valueTupleList)
        RMReportReaderPM.ProcessAmount(sharedContext, in itemIterationContext, currentItem, itemIndex, period, in amount);
    }
  }

  private static void ProcessAmount(
    RMReportReaderPM.SharedContextPM sharedContext,
    in RMReportReaderPM.InventoryItemIterationContextPM inventoryItemIterationContext,
    PX.Objects.IN.InventoryItem currentItem,
    int itemIndex,
    PMHistory period,
    in Decimal amount)
  {
    sharedContext.AddToTotalAmount(in amount);
    PMAccountGroup currentAccountGroup = inventoryItemIterationContext.CurrentAccountGroup;
    PX.Objects.CS.LightDAC.Contract currentProject = inventoryItemIterationContext.CurrentProject;
    PX.Objects.CS.LightDAC.PMTask currentTask = inventoryItemIterationContext.CurrentTask;
    PMCostCode currentCostCode = inventoryItemIterationContext.CurrentCostCode;
    if (sharedContext.Drilldown)
    {
      PMHistoryKeyTuple key1;
      ref PMHistoryKeyTuple local1 = ref key1;
      int projectID1 = currentProject.ContractID.Value;
      string taskCd = currentTask.TaskCD;
      int? nullable = currentAccountGroup.GroupID;
      int accountGroupID1 = nullable.Value;
      nullable = currentItem.InventoryID;
      int inventoryID1 = nullable.Value;
      nullable = currentCostCode.CostCodeID;
      int costCodeID1 = nullable.Value;
      local1 = new PMHistoryKeyTuple(projectID1, taskCd, accountGroupID1, inventoryID1, costCodeID1);
      PXResult<PMHistory, PMBudget, PX.Objects.CS.LightDAC.Contract, PX.Objects.CS.LightDAC.PMTask, PMAccountGroup, PX.Objects.IN.InventoryItem, PMCostCode> pxResult = (PXResult<PMHistory, PMBudget, PX.Objects.CS.LightDAC.Contract, PX.Objects.CS.LightDAC.PMTask, PMAccountGroup, PX.Objects.IN.InventoryItem, PMCostCode>) null;
      BudgetKeyTuple key2;
      ref BudgetKeyTuple local2 = ref key2;
      nullable = currentProject.ContractID;
      int projectID2 = nullable.Value;
      nullable = currentTask.TaskID;
      int projectTaskID = nullable.Value;
      nullable = currentAccountGroup.GroupID;
      int accountGroupID2 = nullable.Value;
      nullable = currentItem.InventoryID;
      int inventoryID2 = nullable.Value;
      nullable = currentCostCode.CostCodeID;
      int costCodeID2 = nullable.Value;
      local2 = new BudgetKeyTuple(projectID2, projectTaskID, accountGroupID2, inventoryID2, costCodeID2);
      PMBudget pmBudget;
      if (!sharedContext.This._budgetByKey.TryGetValue(key2, out pmBudget))
        pmBudget = new PMBudget()
        {
          ProjectID = currentProject.ContractID,
          ProjectTaskID = currentTask.TaskID,
          AccountGroupID = currentAccountGroup.GroupID,
          InventoryID = currentItem.InventoryID,
          CostCodeID = currentCostCode.CostCodeID
        };
      lock (sharedContext.DrilldownData)
      {
        if (!sharedContext.DrilldownData.TryGetValue(key1, out pxResult))
        {
          pxResult = new PXResult<PMHistory, PMBudget, PX.Objects.CS.LightDAC.Contract, PX.Objects.CS.LightDAC.PMTask, PMAccountGroup, PX.Objects.IN.InventoryItem, PMCostCode>(new PMHistory(), pmBudget, currentProject, currentTask, currentAccountGroup, currentItem, currentCostCode);
          sharedContext.DrilldownData.Add(key1, pxResult);
        }
      }
      lock (pxResult)
        RMReportReaderPM.AggregatePMHistoryForDrilldown(PXResult<PMHistory, PMBudget, PX.Objects.CS.LightDAC.Contract, PX.Objects.CS.LightDAC.PMTask, PMAccountGroup, PX.Objects.IN.InventoryItem, PMCostCode>.op_Implicit(pxResult), period);
    }
    if (!sharedContext.Expansion)
      return;
    int num = -1;
    RMReportReaderPM.Keys datasetEndKey;
    RMReportReaderPM.Keys datasetStartKey = datasetEndKey = RMReportReaderPM.Keys.StartAccountGroup;
    string datasetValue = string.Empty;
    switch (sharedContext.DataSource.Expand)
    {
      case "G":
        num = inventoryItemIterationContext.AccountGroupIndex;
        datasetValue = currentAccountGroup.GroupCD;
        datasetStartKey = RMReportReaderPM.Keys.StartAccountGroup;
        datasetEndKey = RMReportReaderPM.Keys.EndAccountGroup;
        break;
      case "P":
        num = inventoryItemIterationContext.ProjectIndex;
        datasetValue = currentProject.ContractCD;
        datasetStartKey = RMReportReaderPM.Keys.StartProject;
        datasetEndKey = RMReportReaderPM.Keys.EndProject;
        break;
      case "T":
        num = inventoryItemIterationContext.TaskIndex;
        datasetValue = currentTask.TaskCD;
        datasetStartKey = RMReportReaderPM.Keys.StartProjectTask;
        datasetEndKey = RMReportReaderPM.Keys.EndProjectTask;
        break;
      case "I":
        num = itemIndex;
        datasetValue = currentItem.InventoryCD;
        datasetStartKey = RMReportReaderPM.Keys.StartInventory;
        datasetEndKey = RMReportReaderPM.Keys.EndInventory;
        break;
    }
    if (num <= -1)
      return;
    int index = num % sharedContext.SplitReturn.Count;
    lock (sharedContext.SplitLockers[index])
      RMReportReaderPM.PopulateRet(sharedContext.SplitReturn[index], amount, sharedContext.DataSet, datasetValue, datasetStartKey, datasetEndKey);
  }

  private static void PopulateRet(
    object[] ret,
    Decimal value,
    ARmDataSet dataSet,
    string datasetValue,
    RMReportReaderPM.Keys datasetStartKey,
    RMReportReaderPM.Keys datasetEndKey)
  {
    ret[2] = (object) ((Decimal) ret[2] + value);
    if (ret[3] != null)
      return;
    ARmDataSet armDataSet = new ARmDataSet(dataSet)
    {
      [(object) datasetStartKey] = (object) datasetValue,
      [(object) datasetEndKey] = (object) datasetValue
    };
    ret[3] = (object) armDataSet;
  }

  private static void AggregatePMHistoryForDrilldown(PMHistory resulthist, PMHistory hist)
  {
    resulthist.ProjectID = hist.ProjectID;
    resulthist.ProjectTaskID = hist.ProjectTaskID;
    resulthist.AccountGroupID = hist.AccountGroupID;
    resulthist.InventoryID = hist.InventoryID;
    resulthist.CostCodeID = hist.CostCodeID;
    resulthist.PeriodID = hist.PeriodID;
    PMHistory pmHistory1 = resulthist;
    Decimal? finPtdAmount = resulthist.FinPTDAmount;
    Decimal valueOrDefault1 = finPtdAmount.GetValueOrDefault();
    finPtdAmount = hist.FinPTDAmount;
    Decimal valueOrDefault2 = finPtdAmount.GetValueOrDefault();
    Decimal? nullable1 = new Decimal?(valueOrDefault1 + valueOrDefault2);
    pmHistory1.FinPTDAmount = nullable1;
    resulthist.FinYTDAmount = new Decimal?(hist.FinYTDAmount.GetValueOrDefault());
    PMHistory pmHistory2 = resulthist;
    Decimal? finPtdQty = resulthist.FinPTDQty;
    Decimal valueOrDefault3 = finPtdQty.GetValueOrDefault();
    finPtdQty = hist.FinPTDQty;
    Decimal valueOrDefault4 = finPtdQty.GetValueOrDefault();
    Decimal? nullable2 = new Decimal?(valueOrDefault3 + valueOrDefault4);
    pmHistory2.FinPTDQty = nullable2;
    resulthist.FinYTDQty = new Decimal?(hist.FinYTDQty.GetValueOrDefault());
    PMHistory pmHistory3 = resulthist;
    Decimal? tranPtdAmount = resulthist.TranPTDAmount;
    Decimal valueOrDefault5 = tranPtdAmount.GetValueOrDefault();
    tranPtdAmount = hist.TranPTDAmount;
    Decimal valueOrDefault6 = tranPtdAmount.GetValueOrDefault();
    Decimal? nullable3 = new Decimal?(valueOrDefault5 + valueOrDefault6);
    pmHistory3.TranPTDAmount = nullable3;
    resulthist.TranYTDAmount = new Decimal?(hist.TranYTDAmount.GetValueOrDefault());
    PMHistory pmHistory4 = resulthist;
    Decimal? tranPtdQty = resulthist.TranPTDQty;
    Decimal valueOrDefault7 = tranPtdQty.GetValueOrDefault();
    tranPtdQty = hist.TranPTDQty;
    Decimal valueOrDefault8 = tranPtdQty.GetValueOrDefault();
    Decimal? nullable4 = new Decimal?(valueOrDefault7 + valueOrDefault8);
    pmHistory4.TranPTDQty = nullable4;
    resulthist.TranYTDQty = new Decimal?(hist.TranYTDQty.GetValueOrDefault());
  }

  public IReadOnlyCollection<PMHistory> GetPeriodsToCalculate(
    Dictionary<string, PMHistory> periodsForKey,
    RMDataSource ds,
    RMDataSourceGL dsGL,
    bool allowStartOfProject)
  {
    if (this.FromStart(ds, allowStartOfProject))
    {
      dsGL.StartPeriod = this._reportPeriods.PerWildcard;
      dsGL.StartPeriodOffset = new short?((short) 0);
      dsGL.StartPeriodYearOffset = new short?((short) 0);
    }
    return this._reportPeriods.GetPeriodsForRegularAmountOptimized(dsGL, periodsForKey);
  }

  private bool FromStart(RMDataSource ds, bool drilldown)
  {
    if (!drilldown)
    {
      short? amountType1 = ds.AmountType;
      int? nullable = amountType1.HasValue ? new int?((int) amountType1.GetValueOrDefault()) : new int?();
      int num1 = 6;
      if (!(nullable.GetValueOrDefault() == num1 & nullable.HasValue))
      {
        short? amountType2 = ds.AmountType;
        nullable = amountType2.HasValue ? new int?((int) amountType2.GetValueOrDefault()) : new int?();
        int num2 = 7;
        if (!(nullable.GetValueOrDefault() == num2 & nullable.HasValue))
        {
          short? amountType3 = ds.AmountType;
          nullable = amountType3.HasValue ? new int?((int) amountType3.GetValueOrDefault()) : new int?();
          int num3 = 8;
          if (!(nullable.GetValueOrDefault() == num3 & nullable.HasValue))
          {
            short? amountType4 = ds.AmountType;
            nullable = amountType4.HasValue ? new int?((int) amountType4.GetValueOrDefault()) : new int?();
            int num4 = 9;
            if (!(nullable.GetValueOrDefault() == num4 & nullable.HasValue))
            {
              short? amountType5 = ds.AmountType;
              nullable = amountType5.HasValue ? new int?((int) amountType5.GetValueOrDefault()) : new int?();
              int num5 = 10;
              if (!(nullable.GetValueOrDefault() == num5 & nullable.HasValue))
              {
                short? amountType6 = ds.AmountType;
                nullable = amountType6.HasValue ? new int?((int) amountType6.GetValueOrDefault()) : new int?();
                int num6 = 11;
                if (!(nullable.GetValueOrDefault() == num6 & nullable.HasValue))
                {
                  short? amountType7 = ds.AmountType;
                  nullable = amountType7.HasValue ? new int?((int) amountType7.GetValueOrDefault()) : new int?();
                  int num7 = 26;
                  if (!(nullable.GetValueOrDefault() == num7 & nullable.HasValue))
                  {
                    short? amountType8 = ds.AmountType;
                    nullable = amountType8.HasValue ? new int?((int) amountType8.GetValueOrDefault()) : new int?();
                    int num8 = 27;
                    if (!(nullable.GetValueOrDefault() == num8 & nullable.HasValue))
                    {
                      short? amountType9 = ds.AmountType;
                      nullable = amountType9.HasValue ? new int?((int) amountType9.GetValueOrDefault()) : new int?();
                      int num9 = 28;
                      if (!(nullable.GetValueOrDefault() == num9 & nullable.HasValue))
                      {
                        short? amountType10 = ds.AmountType;
                        nullable = amountType10.HasValue ? new int?((int) amountType10.GetValueOrDefault()) : new int?();
                        int num10 = 29;
                        if (!(nullable.GetValueOrDefault() == num10 & nullable.HasValue))
                        {
                          short? amountType11 = ds.AmountType;
                          nullable = amountType11.HasValue ? new int?((int) amountType11.GetValueOrDefault()) : new int?();
                          int num11 = 32 /*0x20*/;
                          if (!(nullable.GetValueOrDefault() == num11 & nullable.HasValue))
                          {
                            short? amountType12 = ds.AmountType;
                            nullable = amountType12.HasValue ? new int?((int) amountType12.GetValueOrDefault()) : new int?();
                            int num12 = 33;
                            if (!(nullable.GetValueOrDefault() == num12 & nullable.HasValue))
                            {
                              short? amountType13 = ds.AmountType;
                              nullable = amountType13.HasValue ? new int?((int) amountType13.GetValueOrDefault()) : new int?();
                              int num13 = 31 /*0x1F*/;
                              if (!(nullable.GetValueOrDefault() == num13 & nullable.HasValue))
                              {
                                short? amountType14 = ds.AmountType;
                                nullable = amountType14.HasValue ? new int?((int) amountType14.GetValueOrDefault()) : new int?();
                                int num14 = 34;
                                if (!(nullable.GetValueOrDefault() == num14 & nullable.HasValue))
                                {
                                  short? amountType15 = ds.AmountType;
                                  nullable = amountType15.HasValue ? new int?((int) amountType15.GetValueOrDefault()) : new int?();
                                  int num15 = 35;
                                  return nullable.GetValueOrDefault() == num15 & nullable.HasValue;
                                }
                              }
                            }
                          }
                        }
                      }
                    }
                  }
                }
              }
            }
          }
        }
      }
      return true;
    }
    short? amountType = ds.AmountType;
    int? nullable1 = amountType.HasValue ? new int?((int) amountType.GetValueOrDefault()) : new int?();
    int num = 6;
    return nullable1.GetValueOrDefault() == num & nullable1.HasValue || this.IsBudgetValue(ds);
  }

  public static Decimal GetAmountFromPMHistory(RMDataSource ds, PMHistory hist)
  {
    switch (ds.AmountType.Value)
    {
      case 6:
      case 12:
        return hist.TranPTDAmount.Value;
      case 7:
      case 13:
        return hist.TranPTDQty.Value;
      default:
        return 0M;
    }
  }

  public virtual bool IsBudgetValue(RMDataSource ds)
  {
    switch (ds.AmountType.Value)
    {
      case 8:
      case 9:
      case 10:
      case 11:
      case 26:
      case 27:
      case 28:
      case 29:
      case 31 /*0x1F*/:
      case 32 /*0x20*/:
      case 33:
      case 34:
      case 35:
      case 36:
      case 37:
        return true;
      default:
        return false;
    }
  }

  public virtual Decimal GetAmountFromPMBudget(RMDataSource ds, BudgetKeyTuple key)
  {
    PMBudget pmBudget;
    if (!this._budgetByKey.TryGetValue(key, out pmBudget))
      return 0M;
    switch (ds.AmountType.Value)
    {
      case 8:
        return pmBudget.CuryAmount.Value;
      case 9:
        return pmBudget.Qty.Value;
      case 10:
        return pmBudget.CuryRevisedAmount.Value;
      case 11:
        return pmBudget.RevisedQty.Value;
      case 26:
        return pmBudget.CuryCommittedAmount.Value;
      case 27:
        return pmBudget.CommittedQty.Value;
      case 28:
        return pmBudget.CuryCommittedOpenAmount.Value;
      case 29:
        return pmBudget.CommittedOpenQty.Value;
      case 31 /*0x1F*/:
        return pmBudget.CommittedReceivedQty.Value;
      case 32 /*0x20*/:
        return pmBudget.CuryCommittedInvoicedAmount.Value;
      case 33:
        return pmBudget.CommittedInvoicedQty.Value;
      case 34:
        return pmBudget.ChangeOrderQty.Value;
      case 35:
        return pmBudget.CuryChangeOrderAmount.Value;
      case 36:
        return pmBudget.CuryCommittedOrigAmount.Value;
      case 37:
        return pmBudget.CommittedOrigQty.Value;
      case 38:
        return pmBudget.DraftChangeOrderQty.Value;
      case 39:
        return pmBudget.CuryDraftChangeOrderAmount.Value;
      default:
        return 0M;
    }
  }

  [PXOverride]
  public string GetUrl(Func<string> del)
  {
    if (!(((PXSelectBase<RMReport>) ((PXGraphExtension<RMReportReader>) this).Base.Report).Current.Type == "PM"))
      return del();
    string str = "CS600010";
    PXSiteMapNode mapNodeByScreenId = PXSiteMap.Provider.FindSiteMapNodeByScreenID(str);
    if (mapNodeByScreenId != null)
      return !Str.Contains(mapNodeByScreenId.Url, "ReportLauncher.aspx", StringComparison.OrdinalIgnoreCase) ? ReportMaint.GetRedirectUrlToReport(str, false, (Dictionary<string, string>) null) : PXUrl.TrimUrl(mapNodeByScreenId.Url);
    throw new PXException("You have insufficient rights to access the object ({0}).", new object[1]
    {
      (object) str
    });
  }

  public void EnsureHistoryLoaded(RMDataSourcePM dsPM)
  {
    int num = 1;
    if (this._historyLoaded.Contains(num))
      return;
    this.ProcessPMResultset(PXSelectBase<PMHistory, PXSelectReadonly2<PMHistory, InnerJoin<PX.Objects.CS.LightDAC.PMTask, On<PMHistory.projectID, Equal<PX.Objects.CS.LightDAC.PMTask.projectID>, And<PMHistory.projectTaskID, Equal<PX.Objects.CS.LightDAC.PMTask.taskID>>>>>.Config>.Select((PXGraph) ((PXGraphExtension<RMReportReader>) this).Base, Array.Empty<object>()));
    this.UpdateHistorySegmentsWithRecordsFromBudget();
    this._historyLoaded.Add(num);
  }

  [PXOverride]
  public bool IsParameter(
    ARmDataSet ds,
    string name,
    ValueBucket value,
    Func<ARmDataSet, string, ValueBucket, bool> del)
  {
    bool flag = del(ds, name, value);
    if (flag)
      return flag;
    RMReportReaderPM.Keys keys;
    if (!RMReportReaderPM._keysDictionary.TryGetValue(name, out keys))
      return false;
    value.Value = ds[(object) keys];
    return true;
  }

  [PXOverride]
  public ARmDataSet MergeDataSet(
    IEnumerable<ARmDataSet> list,
    string expand,
    MergingMode mode,
    Func<IEnumerable<ARmDataSet>, string, MergingMode, ARmDataSet> del)
  {
    ARmDataSet target = del(list, expand, mode);
    if (((PXSelectBase<RMReport>) ((PXGraphExtension<RMReportReader>) this).Base.Report).Current.Type == "PM")
    {
      foreach (ARmDataSet source in list)
      {
        if (source != null)
        {
          RMReportWildcard.ConcatenateRangeWithDataSet(target, source, (object) RMReportReaderPM.Keys.StartAccountGroup, (object) RMReportReaderPM.Keys.EndAccountGroup, mode);
          RMReportWildcard.ConcatenateRangeWithDataSet(target, source, (object) RMReportReaderPM.Keys.StartProject, (object) RMReportReaderPM.Keys.EndProject, mode);
          RMReportWildcard.ConcatenateRangeWithDataSet(target, source, (object) RMReportReaderPM.Keys.StartProjectTask, (object) RMReportReaderPM.Keys.EndProjectTask, mode);
          RMReportWildcard.ConcatenateRangeWithDataSet(target, source, (object) RMReportReaderPM.Keys.StartInventory, (object) RMReportReaderPM.Keys.EndInventory, mode);
        }
      }
      List<ARmDataSet> list1 = list.ToList<ARmDataSet>();
      target.Expand = (list1.Count == 4 ? list1[1] : list1[0]).Expand;
    }
    return target;
  }

  [PXOverride]
  public ARmReport GetReport(Func<ARmReport> del)
  {
    ARmReport report = del();
    int? styleId = ((PXSelectBase<RMReport>) ((PXGraphExtension<RMReportReader>) this).Base.Report).Current.StyleID;
    if (styleId.HasValue)
      ((PXGraphExtension<RMReportReader>) this).Base.fillStyle(((PXSelectBase<RMStyle>) ((PXGraphExtension<RMReportReader>) this).Base.StyleByID).SelectSingle(new object[1]
      {
        (object) styleId
      }), report.Style);
    int? dataSourceId = ((PXSelectBase<RMReport>) ((PXGraphExtension<RMReportReader>) this).Base.Report).Current.DataSourceID;
    if (dataSourceId.HasValue)
      this.FillDataSourceInternal(((PXSelectBase<RMDataSource>) ((PXGraphExtension<RMReportReader>) this).Base.DataSourceByID).SelectSingle(new object[1]
      {
        (object) dataSourceId
      }), report.DataSet, report.Type);
    List<ARmReport.ARmReportParameter> armParams = report.ARmParams;
    RMReportPM extension = ((PXSelectBase) ((PXGraphExtension<RMReportReader>) this).Base.Report).Cache.GetExtension<RMReportPM>((object) ((PXSelectBase<RMReport>) ((PXGraphExtension<RMReportReader>) this).Base.Report).Current);
    if (report.Type == "PM")
    {
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      bool valueOrDefault1 = extension.RequestEndAccountGroup.GetValueOrDefault();
      int num1 = 2;
      string str1;
      string str2 = str1 = string.Empty;
      if (((PXSelectBase) ((PXGraphExtension<RMReportReader>) this).Base.DataSourceByID).Cache.GetStateExt<RMDataSourcePM.startAccountGroup>((object) null) is PXFieldState stateExt1 && !string.IsNullOrEmpty(stateExt1.ViewName))
      {
        str2 = stateExt1.ViewName;
        if (stateExt1 is PXStringState)
          str1 = ((PXStringState) stateExt1).InputMask;
      }
      ((PXGraphExtension<RMReportReader>) this).Base.CreateParameter((object) RMReportReaderPM.Keys.StartAccountGroup, "StartAccountGroup", Messages.GetLocal("Start Account Group :"), report.DataSet[(object) RMReportReaderPM.Keys.StartAccountGroup] as string, extension.RequestStartAccountGroup.GetValueOrDefault(), num1, str2, str1, armParams);
      string str3;
      string str4 = str3 = string.Empty;
      if (((PXSelectBase) ((PXGraphExtension<RMReportReader>) this).Base.DataSourceByID).Cache.GetStateExt<RMDataSourcePM.endAccountGroup>((object) null) is PXFieldState stateExt2 && !string.IsNullOrEmpty(stateExt2.ViewName))
      {
        str4 = stateExt2.ViewName;
        if (stateExt2 is PXStringState)
          str3 = ((PXStringState) stateExt2).InputMask;
      }
      ((PXGraphExtension<RMReportReader>) this).Base.CreateParameter((object) RMReportReaderPM.Keys.EndAccountGroup, "EndAccountGroup", Messages.GetLocal("End Account Group :"), report.DataSet[(object) RMReportReaderPM.Keys.EndAccountGroup] as string, valueOrDefault1, num1, str4, str3, armParams);
      bool valueOrDefault2 = extension.RequestEndProject.GetValueOrDefault();
      int num2 = 2;
      string str5;
      string str6 = str5 = string.Empty;
      if (((PXSelectBase) ((PXGraphExtension<RMReportReader>) this).Base.DataSourceByID).Cache.GetStateExt<RMDataSourcePM.startProject>((object) null) is PXFieldState stateExt3 && !string.IsNullOrEmpty(stateExt3.ViewName))
      {
        str6 = stateExt3.ViewName;
        if (stateExt3 is PXStringState)
          str5 = ((PXStringState) stateExt3).InputMask;
      }
      ((PXGraphExtension<RMReportReader>) this).Base.CreateParameter((object) RMReportReaderPM.Keys.StartProject, "StartProject", Messages.GetLocal("Start Project :"), report.DataSet[(object) RMReportReaderPM.Keys.StartProject] as string, extension.RequestStartProject.GetValueOrDefault(), num2, str6, str5, armParams);
      string str7;
      string str8 = str7 = string.Empty;
      if (((PXSelectBase) ((PXGraphExtension<RMReportReader>) this).Base.DataSourceByID).Cache.GetStateExt<RMDataSourcePM.endProject>((object) null) is PXFieldState stateExt4 && !string.IsNullOrEmpty(stateExt4.ViewName))
      {
        str8 = stateExt4.ViewName;
        if (stateExt4 is PXStringState)
          str7 = ((PXStringState) stateExt4).InputMask;
      }
      ((PXGraphExtension<RMReportReader>) this).Base.CreateParameter((object) RMReportReaderPM.Keys.EndProject, "EndProject", Messages.GetLocal("End Project :"), report.DataSet[(object) RMReportReaderPM.Keys.EndProject] as string, valueOrDefault2, num2, str8, str7, armParams);
      bool valueOrDefault3 = extension.RequestEndProjectTask.GetValueOrDefault();
      int num3 = 2;
      string str9;
      string str10 = str9 = string.Empty;
      if (((PXSelectBase) ((PXGraphExtension<RMReportReader>) this).Base.DataSourceByID).Cache.GetStateExt<RMDataSourcePM.startProjectTask>((object) null) is PXFieldState stateExt5 && !string.IsNullOrEmpty(stateExt5.ViewName))
      {
        str10 = stateExt5.ViewName;
        if (stateExt5 is PXStringState)
          str9 = ((PXStringState) stateExt5).InputMask;
      }
      ((PXGraphExtension<RMReportReader>) this).Base.CreateParameter((object) RMReportReaderPM.Keys.StartProjectTask, "StartTask", Messages.GetLocal("Start Task :"), report.DataSet[(object) RMReportReaderPM.Keys.StartProjectTask] as string, extension.RequestStartProjectTask.GetValueOrDefault(), num3, str10, str9, armParams);
      string str11;
      string str12 = str11 = string.Empty;
      if (((PXSelectBase) ((PXGraphExtension<RMReportReader>) this).Base.DataSourceByID).Cache.GetStateExt<RMDataSourcePM.endProjectTask>((object) null) is PXFieldState stateExt6 && !string.IsNullOrEmpty(stateExt6.ViewName))
      {
        str12 = stateExt6.ViewName;
        if (stateExt6 is PXStringState)
          str11 = ((PXStringState) stateExt6).InputMask;
      }
      ((PXGraphExtension<RMReportReader>) this).Base.CreateParameter((object) RMReportReaderPM.Keys.EndProjectTask, "EndTask", Messages.GetLocal("End Task :"), report.DataSet[(object) RMReportReaderPM.Keys.EndProjectTask] as string, valueOrDefault3, num3, str12, str11, armParams);
      bool valueOrDefault4 = extension.RequestEndInventory.GetValueOrDefault();
      int num4 = 2;
      string str13;
      string str14 = str13 = string.Empty;
      if (((PXSelectBase) ((PXGraphExtension<RMReportReader>) this).Base.DataSourceByID).Cache.GetStateExt<RMDataSourcePM.startInventory>((object) null) is PXFieldState stateExt7 && !string.IsNullOrEmpty(stateExt7.ViewName))
      {
        str14 = stateExt7.ViewName;
        if (stateExt7 is PXStringState)
          str13 = ((PXStringState) stateExt7).InputMask;
      }
      ((PXGraphExtension<RMReportReader>) this).Base.CreateParameter((object) RMReportReaderPM.Keys.StartInventory, "StartInventory", Messages.GetLocal("Start Inventory :"), report.DataSet[(object) RMReportReaderPM.Keys.StartInventory] as string, extension.RequestStartInventory.GetValueOrDefault(), num4, str14, str13, armParams);
      string str15;
      string str16 = str15 = string.Empty;
      if (((PXSelectBase) ((PXGraphExtension<RMReportReader>) this).Base.DataSourceByID).Cache.GetStateExt<RMDataSourcePM.endInventory>((object) null) is PXFieldState stateExt8 && !string.IsNullOrEmpty(stateExt8.ViewName))
      {
        str16 = stateExt8.ViewName;
        if (stateExt8 is PXStringState)
          str15 = ((PXStringState) stateExt8).InputMask;
      }
      ((PXGraphExtension<RMReportReader>) this).Base.CreateParameter((object) RMReportReaderPM.Keys.EndInventory, "EndInventory", Messages.GetLocal("End Inventory :"), report.DataSet[(object) RMReportReaderPM.Keys.EndInventory] as string, valueOrDefault4, num4, str16, str15, armParams);
    }
    return report;
  }

  [PXOverride]
  public virtual List<ARmUnit> ExpandUnit(
    RMDataSource ds,
    ARmUnit unit,
    Func<RMDataSource, ARmUnit, List<ARmUnit>> del)
  {
    if (!(((PXSelectBase<RMReport>) ((PXGraphExtension<RMReportReader>) this).Base.Report).Current.Type == "PM"))
      return del(ds, unit);
    if (unit.DataSet.Expand != "N")
    {
      this.PMEnsureInitialized();
      if (ds.Expand == "G")
        return RMReportUnitExpansion<PMAccountGroup>.ExpandUnit(((PXGraphExtension<RMReportReader>) this).Base, ds, unit, (object) RMReportReaderPM.Keys.StartAccountGroup, (object) RMReportReaderPM.Keys.EndAccountGroup, new Func<ARmDataSet, List<PMAccountGroup>>(this.GetItemsInRange<PMAccountGroup>), (Func<PMAccountGroup, string>) (accountGroup => accountGroup.GroupCD), (Func<PMAccountGroup, string>) (accountGroup => accountGroup.Description), (Action<PMAccountGroup, string>) ((accountGroup, wildcard) =>
        {
          accountGroup.GroupCD = wildcard;
          accountGroup.Description = wildcard;
        }));
      if (ds.Expand == "P")
        return RMReportUnitExpansion<PX.Objects.CS.LightDAC.Contract>.ExpandUnit(((PXGraphExtension<RMReportReader>) this).Base, ds, unit, (object) RMReportReaderPM.Keys.StartProject, (object) RMReportReaderPM.Keys.EndProject, new Func<ARmDataSet, List<PX.Objects.CS.LightDAC.Contract>>(this.GetItemsInRange<PX.Objects.CS.LightDAC.Contract>), (Func<PX.Objects.CS.LightDAC.Contract, string>) (project => project.ContractCD), (Func<PX.Objects.CS.LightDAC.Contract, string>) (project => project.Description), (Action<PX.Objects.CS.LightDAC.Contract, string>) ((project, wildcard) =>
        {
          project.ContractCD = wildcard;
          project.Description = wildcard;
        }));
      if (ds.Expand == "T")
        return RMReportUnitExpansion<PX.Objects.CS.LightDAC.PMTask>.ExpandUnit(((PXGraphExtension<RMReportReader>) this).Base, ds, unit, (object) RMReportReaderPM.Keys.StartProjectTask, (object) RMReportReaderPM.Keys.EndProjectTask, (Func<ARmDataSet, List<PX.Objects.CS.LightDAC.PMTask>>) (rangeToFetch =>
        {
          List<PX.Objects.CS.LightDAC.PMTask> source = this.GetItemsInRange<PX.Objects.CS.LightDAC.PMTask>(rangeToFetch);
          ARmDataSet armDataSet = new ARmDataSet();
          RMReportWildcard.ConcatenateRangeWithDataSet(armDataSet, unit.DataSet, (object) RMReportReaderPM.Keys.StartProject, (object) RMReportReaderPM.Keys.EndProject, (MergingMode) 0);
          if (!string.IsNullOrEmpty(armDataSet[(object) RMReportReaderPM.Keys.StartProject] as string))
          {
            List<PX.Objects.CS.LightDAC.Contract> projects = this.GetItemsInRange<PX.Objects.CS.LightDAC.Contract>(armDataSet);
            source = source.Where<PX.Objects.CS.LightDAC.PMTask>((Func<PX.Objects.CS.LightDAC.PMTask, bool>) (t => projects.Any<PX.Objects.CS.LightDAC.Contract>((Func<PX.Objects.CS.LightDAC.Contract, bool>) (p =>
            {
              int? projectId = t.ProjectID;
              int? contractId = p.ContractID;
              return projectId.GetValueOrDefault() == contractId.GetValueOrDefault() & projectId.HasValue == contractId.HasValue;
            })))).ToList<PX.Objects.CS.LightDAC.PMTask>();
          }
          return source.GroupBy<PX.Objects.CS.LightDAC.PMTask, string>((Func<PX.Objects.CS.LightDAC.PMTask, string>) (t => t.TaskCD)).Select<IGrouping<string, PX.Objects.CS.LightDAC.PMTask>, PX.Objects.CS.LightDAC.PMTask>((Func<IGrouping<string, PX.Objects.CS.LightDAC.PMTask>, PX.Objects.CS.LightDAC.PMTask>) (g => new PX.Objects.CS.LightDAC.PMTask()
          {
            TaskCD = g.Key,
            Description = g.Min<PX.Objects.CS.LightDAC.PMTask, string>((Func<PX.Objects.CS.LightDAC.PMTask, string>) (t => t.Description))
          })).ToList<PX.Objects.CS.LightDAC.PMTask>();
        }), (Func<PX.Objects.CS.LightDAC.PMTask, string>) (task => task.TaskCD), (Func<PX.Objects.CS.LightDAC.PMTask, string>) (project => project.Description), (Action<PX.Objects.CS.LightDAC.PMTask, string>) ((task, wildcard) =>
        {
          task.TaskCD = wildcard;
          task.Description = wildcard;
        }));
      if (ds.Expand == "I")
        return RMReportUnitExpansion<PX.Objects.IN.InventoryItem>.ExpandUnit(((PXGraphExtension<RMReportReader>) this).Base, ds, unit, (object) RMReportReaderPM.Keys.StartInventory, (object) RMReportReaderPM.Keys.EndInventory, new Func<ARmDataSet, List<PX.Objects.IN.InventoryItem>>(this.GetItemsInRange<PX.Objects.IN.InventoryItem>), (Func<PX.Objects.IN.InventoryItem, string>) (item => item.InventoryCD), (Func<PX.Objects.IN.InventoryItem, string>) (item => item.Descr), (Action<PX.Objects.IN.InventoryItem, string>) ((item, wildcard) =>
        {
          item.InventoryCD = wildcard;
          item.Descr = wildcard;
        }));
    }
    return (List<ARmUnit>) null;
  }

  [PXOverride]
  public void FillDataSource(
    RMDataSource ds,
    ARmDataSet dst,
    string rmType,
    Action<RMDataSource, ARmDataSet, string> del)
  {
    del(ds, dst, rmType);
    if (!(rmType == "PM"))
      return;
    this.FillDataSourceInternal(ds, dst, rmType);
  }

  private void FillDataSourceInternal(RMDataSource ds, ARmDataSet dst, string rmType)
  {
    if (ds == null || !ds.DataSourceID.HasValue)
      return;
    RMDataSourcePM extension = ((PXGraph) ((PXGraphExtension<RMReportReader>) this).Base).Caches[typeof (RMDataSource)].GetExtension<RMDataSourcePM>((object) ds);
    dst[(object) RMReportReaderPM.Keys.StartAccountGroup] = (object) extension.StartAccountGroup;
    dst[(object) RMReportReaderPM.Keys.EndAccountGroup] = (object) extension.EndAccountGroup;
    dst[(object) RMReportReaderPM.Keys.StartProject] = (object) extension.StartProject;
    dst[(object) RMReportReaderPM.Keys.EndProject] = (object) extension.EndProject;
    dst[(object) RMReportReaderPM.Keys.StartProjectTask] = (object) extension.StartProjectTask;
    dst[(object) RMReportReaderPM.Keys.EndProjectTask] = (object) extension.EndProjectTask;
    dst[(object) RMReportReaderPM.Keys.StartInventory] = (object) extension.StartInventory;
    dst[(object) RMReportReaderPM.Keys.EndInventory] = (object) extension.EndInventory;
  }

  [PXOverride]
  public object GetExprContext()
  {
    if (this._Data == null)
      this._Data = new ArmDATA();
    return (object) this._Data;
  }

  public enum Keys
  {
    StartAccountGroup,
    EndAccountGroup,
    StartProject,
    EndProject,
    StartProjectTask,
    EndProjectTask,
    StartInventory,
    EndInventory,
  }

  /// <summary>
  /// A local PM reports' context used during iteration over tasks.
  /// </summary>
  private readonly struct TaskIterationContext(
    RMReportReaderPM.SharedContextPM sharedContext,
    PMAccountGroup currentAccountGroup,
    int accountGroupIndex,
    NestedDictionary<string, int, (int ProjectID, int InventoryID), Dictionary<string, PMHistory>> accountGroupDict)
  {
    public RMReportReaderPM.SharedContextPM SharedContext { get; } = sharedContext;

    public PMAccountGroup CurrentAccountGroup { get; } = currentAccountGroup;

    public int AccountGroupIndex { get; } = accountGroupIndex;

    public NestedDictionary<string, int, (int ProjectID, int InventoryID), Dictionary<string, PMHistory>> AccountGroupDict { get; } = accountGroupDict;

    public void TaskIterationNoClosures(int taskIndex)
    {
      RMReportReaderPM.TaskIteration(in this, taskIndex);
    }
  }

  /// <summary>
  /// A local PM reports' context used during iteration over cost codes.
  /// </summary>
  private readonly struct CostCodeIterationContextPM(
    RMReportReaderPM.SharedContextPM sharedContext,
    PMAccountGroup currentAccountGroup,
    int accountGroupIndex,
    PX.Objects.CS.LightDAC.PMTask currentTask,
    int taskIndex,
    PX.Objects.CS.LightDAC.Contract currentProject,
    int projectIndex,
    NestedDictionary<int, (int ProjectID, int InventoryID), Dictionary<string, PMHistory>> taskDict)
  {
    public RMReportReaderPM.SharedContextPM SharedContext { get; } = sharedContext;

    public PMAccountGroup CurrentAccountGroup { get; } = currentAccountGroup;

    public int AccountGroupIndex { get; } = accountGroupIndex;

    public PX.Objects.CS.LightDAC.PMTask CurrentTask { get; } = currentTask;

    public int TaskIndex { get; } = taskIndex;

    public PX.Objects.CS.LightDAC.Contract CurrentProject { get; } = currentProject;

    public int ProjectIndex { get; } = projectIndex;

    public NestedDictionary<int, (int ProjectID, int InventoryID), Dictionary<string, PMHistory>> TaskDict { get; } = taskDict;

    public void CostCodeIterationNoClosures(int costCodeIndex)
    {
      RMReportReaderPM.CostCodeIteration(in this, costCodeIndex);
    }
  }

  /// <summary>
  /// A local PM reports' context used during iteration over inventory items.
  /// </summary>
  private readonly struct InventoryItemIterationContextPM
  {
    public RMReportReaderPM.SharedContextPM SharedContext { get; }

    public PMAccountGroup CurrentAccountGroup { get; }

    public int AccountGroupIndex { get; }

    public PX.Objects.CS.LightDAC.PMTask CurrentTask { get; }

    public int TaskIndex { get; }

    public PX.Objects.CS.LightDAC.Contract CurrentProject { get; }

    public int ProjectIndex { get; }

    public PMCostCode CurrentCostCode { get; }

    public Dictionary<(int ProjectID, int InventoryID), Dictionary<string, PMHistory>> CostDict { get; }

    public InventoryItemIterationContextPM(
      in RMReportReaderPM.CostCodeIterationContextPM costCodeIterationContext,
      PMCostCode currentCostCode,
      Dictionary<(int ProjectID, int InventoryID), Dictionary<string, PMHistory>> costDict)
    {
      this.SharedContext = costCodeIterationContext.SharedContext;
      this.CurrentAccountGroup = costCodeIterationContext.CurrentAccountGroup;
      this.AccountGroupIndex = costCodeIterationContext.AccountGroupIndex;
      this.CurrentTask = costCodeIterationContext.CurrentTask;
      this.TaskIndex = costCodeIterationContext.TaskIndex;
      this.CurrentProject = costCodeIterationContext.CurrentProject;
      this.ProjectIndex = costCodeIterationContext.ProjectIndex;
      this.CurrentCostCode = currentCostCode;
      this.CostDict = costDict;
    }

    public void InvItemIterationNoClosures(int itemIndex)
    {
      RMReportReaderPM.InvItemIteration(in this, itemIndex);
    }
  }

  /// <summary>
  /// A shared PM reports' context used during parallel calculations of the cell value.
  /// </summary>
  private class SharedContextPM
  {
    private static readonly string[] ExpandTypes = new string[4]
    {
      "G",
      "P",
      "T",
      "I"
    };
    private readonly object _locker = new object();
    private Decimal _totalAmount;
    private readonly ImmutableArray<object> _splitLockers;

    public Decimal TotalAmount => this._totalAmount;

    public ImmutableArray<object> SplitLockers => this._splitLockers;

    public RMReportReaderPM This { get; }

    public RMDataSource DataSource { get; }

    public RMDataSourceGL DataSourceGL { get; }

    public RMDataSourcePM DataSourcePM { get; }

    public ARmDataSet DataSet { get; }

    public bool Drilldown { get; }

    public bool Expansion { get; }

    public Dictionary<PMHistoryKeyTuple, PXResult<PMHistory, PMBudget, PX.Objects.CS.LightDAC.Contract, PX.Objects.CS.LightDAC.PMTask, PMAccountGroup, PX.Objects.IN.InventoryItem, PMCostCode>> DrilldownData { get; }

    public List<object[]> SplitReturn { get; }

    public List<PMAccountGroup> AccountGroups { get; }

    public List<PX.Objects.CS.LightDAC.PMTask> Tasks { get; }

    public List<PMCostCode> CostCodes { get; }

    public List<PX.Objects.IN.InventoryItem> Items { get; }

    public Dictionary<int?, (PX.Objects.CS.LightDAC.Contract Project, int ProjectIndex)> ProjectsDict { get; }

    public bool ParallelizeAccountGroups { get; }

    public bool ParallelizeTasks { get; }

    public bool ParallelizeCostCodes { get; }

    public bool ParallelizeInvItems { get; }

    public ParallelOptions ParallelOptions { get; }

    public SharedContextPM(
      RMReportReaderPM @this,
      bool drillDown,
      RMDataSource ds,
      RMDataSourceGL dsGL,
      RMDataSourcePM dsPM,
      ARmDataSet dataSet,
      List<PMAccountGroup> accountGroups,
      List<PX.Objects.CS.LightDAC.PMTask> tasks,
      List<PMCostCode> costCodes,
      List<PX.Objects.IN.InventoryItem> items,
      List<PX.Objects.CS.LightDAC.Contract> projects,
      List<object[]> splitret)
    {
      this.This = @this;
      this.Drilldown = drillDown;
      this.DataSource = ds;
      this.DataSourceGL = dsGL;
      this.DataSourcePM = dsPM;
      this.DataSet = dataSet;
      this.AccountGroups = accountGroups;
      this.Tasks = tasks;
      this.CostCodes = costCodes;
      this.Items = items;
      this.SplitReturn = splitret;
      (int, bool) parallelCalculation = ((PXGraphExtension<RMReportReader>) this.This).Base.GetThreadsCountForCellValueParallelCalculation();
      int workerThreadsCount = parallelCalculation.Item1;
      int num = parallelCalculation.Item2 ? -1 : workerThreadsCount;
      this.ParallelOptions = new ParallelOptions()
      {
        MaxDegreeOfParallelism = num
      };
      if (this.Drilldown)
        this.DrilldownData = new Dictionary<PMHistoryKeyTuple, PXResult<PMHistory, PMBudget, PX.Objects.CS.LightDAC.Contract, PX.Objects.CS.LightDAC.PMTask, PMAccountGroup, PX.Objects.IN.InventoryItem, PMCostCode>>();
      this.ProjectsDict = this.InitializeProjectDictionary(projects);
      (this.ParallelizeAccountGroups, this.ParallelizeTasks, this.ParallelizeCostCodes, this.ParallelizeInvItems) = this.DetermineParallelizationOptions(workerThreadsCount);
      if (this.SplitReturn != null && ((IEnumerable<string>) RMReportReaderPM.SharedContextPM.ExpandTypes).Contains<string>(this.DataSource.Expand))
      {
        this.Expansion = true;
        ImmutableArray<object>.Builder builder = ImmutableArray.CreateBuilder<object>(this.SplitReturn.Count);
        for (int index = 0; index < this.SplitReturn.Count; ++index)
        {
          object obj = new object();
          builder.Add(obj);
        }
        this._splitLockers = builder.ToImmutable();
      }
      else
      {
        this.Expansion = false;
        this._splitLockers = ImmutableArray<object>.Empty;
      }
    }

    private Dictionary<int?, (PX.Objects.CS.LightDAC.Contract Project, int ProjectIndex)> InitializeProjectDictionary(
      List<PX.Objects.CS.LightDAC.Contract> projects)
    {
      Dictionary<int?, (PX.Objects.CS.LightDAC.Contract, int)> dictionary = new Dictionary<int?, (PX.Objects.CS.LightDAC.Contract, int)>(projects.Count);
      for (int index = 0; index < projects.Count; ++index)
        dictionary[projects[index].ContractID] = (projects[index], index);
      return dictionary;
    }

    /// <summary>
    /// Determine parallelization options considering that we want no more than one nested parallel loop with good parallelization on worker threads.
    /// </summary>
    /// <exception cref="T:System.InvalidOperationException">Thrown when the number of worker threads is less than 1.</exception>
    /// <param name="workerThreadsCount">Number of worker threads.</param>
    /// <returns>Parallelization options.</returns>
    private (bool ParallelizeAccountGroups, bool ParallelizeTasks, bool ParallelizeCostCodes, bool ParallelizeInvItems) DetermineParallelizationOptions(
      int workerThreadsCount)
    {
      if (workerThreadsCount <= 0)
        throw new InvalidOperationException("The number of worker threads cannot be less than one");
      if (WebConfig.ParallelizeAllDimensionsInArmReports)
        return (true, true, true, true);
      if (workerThreadsCount == 1)
        return (false, false, false, false);
      if (this.AccountGroups.Count >= workerThreadsCount)
        return (true, false, false, false);
      if (this.Tasks.Count >= workerThreadsCount)
        return (false, true, false, false);
      if (this.CostCodes.Count >= workerThreadsCount)
        return (false, false, true, false);
      if (this.Items.Count >= workerThreadsCount)
        return (false, false, false, true);
      bool flag1 = this.AccountGroups.Count > 1;
      bool flag2 = this.Tasks.Count > 1 && !flag1;
      bool flag3 = this.CostCodes.Count > 1 && !flag1 && !flag2;
      bool flag4 = this.Items.Count > 1 && !flag1 && !flag2 && !flag3;
      return (flag1, flag2, flag3, flag4);
    }

    public void AccountGroupIterationNoClosures(int accountGroupIndex)
    {
      RMReportReaderPM.AccountGroupIteration(this, accountGroupIndex);
    }

    public void AddToTotalAmount(in Decimal amount)
    {
      lock (this._locker)
        this._totalAmount += amount;
    }
  }
}
