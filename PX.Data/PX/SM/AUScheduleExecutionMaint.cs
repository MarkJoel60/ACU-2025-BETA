// Decompiled with JetBrains decompiler
// Type: PX.SM.AUScheduleExecutionMaint
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using PX.Data;
using PX.Data.BQL;
using PX.Web.UI;
using System;

#nullable enable
namespace PX.SM;

public class AUScheduleExecutionMaint : PXGraph<
#nullable disable
AUScheduleExecutionMaint>
{
  public PXCancel<AUScheduleExecutionMaint.ExecFilter> Cancel;
  public PXFilter<AUScheduleExecutionMaint.ExecFilter> Filter;
  [PXFilterable(new System.Type[] {})]
  public PXSelect<AUScheduleExecution, Where2<Where<Current<AUScheduleExecutionMaint.ExecFilter.scheduleID>, PX.Data.IsNull, Or<AUScheduleExecution.scheduleID, Equal<Current<AUScheduleExecutionMaint.ExecFilter.scheduleID>>>>, PX.Data.And<Where2<Where<Current<AUScheduleExecutionMaint.ExecFilter.dateFrom>, PX.Data.IsNull, Or<AUScheduleExecution.executionDate, GreaterEqual<Current<AUScheduleExecutionMaint.ExecFilter.dateFrom>>>>, PX.Data.And<Where<Current<AUScheduleExecutionMaint.ExecFilter.dateTo>, PX.Data.IsNull, Or<AUScheduleExecution.executionDate, LessEqual<Current<AUScheduleExecutionMaint.ExecFilter.dateTo>>>>>>>>, OrderBy<Desc<AUScheduleExecution.executionDate>>> Executions;
  public PXFilter<AUScheduleExecutionMaint.HistoryFilter> HistoriesFilter;
  [PXFilterable(new System.Type[] {})]
  public PXSelect<AUScheduleHistory, Where<AUScheduleHistory.scheduleID, Equal<Current<AUScheduleExecution.scheduleID>>, And<AUScheduleHistory.executionDate, Equal<Current<AUScheduleExecution.executionDate>>>>> Histories;
  public PXAction<AUScheduleExecutionMaint.ExecFilter> Delete;
  public PXAction<AUScheduleExecutionMaint.ExecFilter> DeleteAll;
  public PXAction<AUScheduleExecutionMaint.ExecFilter> ViewSchedule;
  public PXAction<AUScheduleExecutionMaint.ExecFilter> ViewTotal;

  [PXUIField(DisplayName = "Delete")]
  [PXButton(Tooltip = "Delete selected records (Ctrl+Del)", ConfirmationType = PXConfirmationType.Always, ConfirmationMessage = "Selected records will be deleted.", ShortcutChar = '.', ShortcutCtrl = true)]
  protected void delete()
  {
    PXCache cach = this.Caches[typeof (AUScheduleExecution)];
    foreach (AUScheduleExecution execRow in cach.Updated)
    {
      bool? selected = execRow.Selected;
      bool flag = true;
      if (selected.GetValueOrDefault() == flag & selected.HasValue)
        AUScheduleExecutionMaint.deleteExecution((PXGraph) this, cach, execRow);
    }
  }

  [PXUIField(DisplayName = "Delete All")]
  [PXButton(Tooltip = "Delete all records (Ctrl+Shift+Del)", ConfirmationType = PXConfirmationType.Always, ConfirmationMessage = "All records will be deleted.", ShortcutChar = '.', ShortcutCtrl = true, ShortcutShift = true)]
  protected void deleteAll()
  {
    PXCache cach = this.Caches[typeof (AUScheduleExecution)];
    foreach (PXResult<AUScheduleExecution> pxResult in this.Executions.Select())
    {
      AUScheduleExecution execRow = (AUScheduleExecution) pxResult;
      AUScheduleExecutionMaint.deleteExecution((PXGraph) this, cach, execRow);
    }
  }

  internal static void deleteExecution(
    PXGraph graph,
    PXCache execCache,
    AUScheduleExecution execRow)
  {
    System.DateTime databaseFormat = SyMappingUtils.ConvertDateToDatabaseFormat(execRow.ExecutionDate.Value, execCache, "executionDate");
    PXDataFieldRestrict[] dataFieldRestrictArray = new PXDataFieldRestrict[2]
    {
      new PXDataFieldRestrict("scheduleID", (object) execRow.ScheduleID),
      new PXDataFieldRestrict("executionDate", (object) databaseFormat)
    };
    using (new PXConnectionScope())
    {
      execCache.Delete((object) execRow);
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        using (new PXCommandScope(PXDatabase.Provider.DefaultQueryTimeout * 20))
          PXDatabase.Delete<AUScheduleHistory>(dataFieldRestrictArray);
        using (new PXCancelCommandPreparingScope(execCache, "tStamp"))
          execCache.PersistDeleted((object) execRow);
        transactionScope.Complete();
      }
      execCache.Persisted(false);
    }
  }

  [PXButton]
  protected void viewSchedule()
  {
    AUScheduleExecution current = this.Executions.Current;
    if (current == null || !current.ScheduleID.HasValue)
      return;
    AUScheduleMaint instance = PXGraph.CreateInstance<AUScheduleMaint>();
    instance.Schedule.Current = (AUSchedule) instance.Schedule.Search<AUSchedule.scheduleID>((object) current.ScheduleID);
    if (instance.Schedule.Current != null)
      throw new PXRedirectRequiredException((PXGraph) instance, true, "ViewSchedule");
  }

  [PXButton]
  protected void viewTotal() => this.viewHistories(0);

  private void viewHistories(int tabIndex)
  {
    int? totalCount = this.Executions.Current.TotalCount;
    int num1 = 0;
    if (totalCount.GetValueOrDefault() > num1 & totalCount.HasValue && this.GetHistories(this.Executions.Current).Count == 0)
    {
      int num2 = (int) this.Executions.Ask("Processing Results", "No records are found. The detailed results of processing were deleted. On the Automation Schedules (SM205020) form, you can configure the number of results stored for a specific schedule by setting the Executions to Keep in History parameter on the Details tab.", MessageButtons.OK);
    }
    else
    {
      if (this.HistoriesFilter.Current == null)
        this.HistoriesFilter.Current = new AUScheduleExecutionMaint.HistoryFilter();
      this.HistoriesFilter.Current.TabIndex = tabIndex;
      int num3 = (int) this.HistoriesFilter.AskExt();
    }
  }

  [PXMergeAttributes(Method = MergeMethod.Merge)]
  [PXUIField(DisplayName = "Schedule ID")]
  protected virtual void _(
    Events.CacheAttached<AUScheduleExecution.scheduleID> e)
  {
  }

  protected virtual void AUScheduleExecution_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    AUScheduleExecution row = (AUScheduleExecution) e.Row;
    if (row == null)
      return;
    int? nullable = row.ErrorsCount;
    int num1 = 0;
    if (nullable.GetValueOrDefault() > num1 & nullable.HasValue)
    {
      row.Status = Sprite.Control.GetFullUrl("Error");
      row.Result = "Processing completed with errors";
    }
    else
    {
      nullable = row.WarningsCount;
      int num2 = 0;
      if (nullable.GetValueOrDefault() > num2 & nullable.HasValue)
      {
        row.Status = Sprite.Control.GetFullUrl("Warning");
        row.Result = "Processing completed with warnings";
      }
      else
      {
        row.Status = Sprite.Control.GetFullUrl("Info");
        row.Result = "Processing completed";
      }
    }
  }

  internal PXResultset<AUScheduleHistory> GetHistories(AUScheduleExecution execution)
  {
    return PXSelectBase<AUScheduleHistory, PXSelect<AUScheduleHistory, Where<AUScheduleHistory.scheduleID, Equal<Required<AUScheduleExecution.scheduleID>>, And<AUScheduleHistory.executionDate, Equal<Required<AUScheduleExecution.executionDate>>>>>.Config>.Select((PXGraph) this, (object) execution.ScheduleID, (object) execution.ExecutionDate);
  }

  [Serializable]
  public class ExecFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    private System.DateTime? _DateTo;

    [PXInt]
    [PXUIField(DisplayName = "Schedule")]
    [PXSelector(typeof (Search<AUSchedule.scheduleID>), SubstituteKey = typeof (AUSchedule.description))]
    public virtual int? ScheduleID { get; set; }

    [PXDate(UseTimeZone = true)]
    [PXUIField(DisplayName = "From")]
    public virtual System.DateTime? DateFrom { get; set; }

    [PXDate(UseTimeZone = true)]
    [PXUIField(DisplayName = "To")]
    public virtual System.DateTime? DateTo
    {
      get
      {
        ref System.DateTime? local = ref this._DateTo;
        if (!local.HasValue)
          return new System.DateTime?();
        System.DateTime dateTime = local.GetValueOrDefault();
        dateTime = dateTime.Date;
        dateTime = dateTime.AddDays(1.0);
        return new System.DateTime?(dateTime.AddTicks(-1L));
      }
      set => this._DateTo = value;
    }

    public abstract class scheduleID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      AUScheduleExecutionMaint.ExecFilter.scheduleID>
    {
    }

    public abstract class dateFrom : 
      BqlType<
      #nullable enable
      IBqlDateTime, System.DateTime>.Field<
      #nullable disable
      AUScheduleExecutionMaint.ExecFilter.dateFrom>
    {
    }

    public abstract class dateTo : 
      BqlType<
      #nullable enable
      IBqlDateTime, System.DateTime>.Field<
      #nullable disable
      AUScheduleExecutionMaint.ExecFilter.dateTo>
    {
    }
  }

  [Serializable]
  public class HistoryFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXInt]
    [PXUIField(Visible = false)]
    public virtual int TabIndex { get; set; }

    public abstract class tabIndex : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      AUScheduleExecutionMaint.HistoryFilter.tabIndex>
    {
    }
  }
}
