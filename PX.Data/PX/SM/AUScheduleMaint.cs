// Decompiled with JetBrains decompiler
// Type: PX.SM.AUScheduleMaint
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using PX.Common;
using PX.Data;
using PX.Data.Description;
using PX.Data.Process;
using PX.Data.Process.Automation;
using PX.Metadata;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Compilation;

#nullable disable
namespace PX.SM;

[PXDisableWorkflow]
public class AUScheduleMaint : PXGraph<AUScheduleMaint>
{
  internal const string ActionProcess = "Process";
  internal const string ActionProcessAll = "ProcessAll";
  internal const string ActionRaiseEvent = "RaiseEvent";
  private const string OuterView = "$Outer$";
  public PXSave<AUSchedule> Save;
  public PXCancel<AUSchedule> Cancel;
  public PXCancel<AUSchedule> Refresh;
  public PXInsert<AUSchedule> Insert;
  public PXDelete<AUSchedule> Delete;
  public PXFirst<AUSchedule, AUSchedule.description> First;
  public PXPrevious<AUSchedule, AUSchedule.description> Prev;
  public PXNext<AUSchedule, AUSchedule.description> Next;
  public PXLast<AUSchedule, AUSchedule.description> Last;
  protected System.DateTime _Today;
  protected PXSiteMap.ScreenInfo _Info;
  [PXNotCleanable]
  public PXFilter<AUScheduleCurrentScreen> ScheduleCurrentScreen;
  public PXSelect<AUSchedule> Schedule;
  public PXSelect<AUSchedule, Where<AUSchedule.scheduleID, Equal<Current<AUSchedule.scheduleID>>>> CurrentSchedule;
  public PXAction<AUSchedule> viewScreen;
  public PXAction<AUSchedule> viewHistory;
  public PXSelect<AUReportProcess.ReportSettings> ReportSettings;
  public PXSelect<AUScheduleFilter, Where<AUScheduleFilter.screenID, Equal<Current<AUSchedule.screenID>>, And<AUScheduleFilter.scheduleID, Equal<Current<AUSchedule.scheduleID>>, And<AUScheduleFilter.isActive, Equal<PX.Data.True>>>>> Filters;
  public PXSelect<AUScheduleFill, Where<AUScheduleFill.screenID, Equal<Current<AUSchedule.screenID>>, And<AUScheduleFill.scheduleID, Equal<Current<AUSchedule.scheduleID>>, And<AUScheduleFill.isActive, Equal<PX.Data.True>>>>> Fills;
  public PXSelect<AUScheduleTemplate> Templates;
  private DataScreenBase _dataScreen;

  [InjectDependency]
  protected internal IDataScreenFactory DataScreenFactory { get; set; }

  [InjectDependency]
  private IScheduleAdjustmentRuleProvider AdjustmentRuleProvider { get; set; }

  [InjectDependency]
  protected ICurrentUserInformationProvider CurrentUserInformationProvider { get; set; }

  public AUScheduleMaint()
  {
    PXUIFieldAttribute.SetVisible<AUScheduleFilter.openBrackets>(this.Filters.Cache, (object) null, false);
    PXUIFieldAttribute.SetVisible<AUScheduleFilter.closeBrackets>(this.Filters.Cache, (object) null, false);
    PXUIFieldAttribute.SetVisible<AUScheduleFilter.operatoR>(this.Filters.Cache, (object) null, false);
    this.Refresh.SetVisible(false);
    this._Today = PXTimeZoneInfo.Today;
    PXSiteMapNodeSelectorAttribute.SetRestriction<AUSchedule.screenID>(this.Schedule.Cache, (object) null, (Func<SiteMap, bool>) (s => SiteMapRestrictionsTypes.IsReport(s)));
  }

  protected PXGraph _Graph => this._dataScreen?.DataGraph;

  internal bool IsScreenInfoCorrectlyCollected => this._Info != null && this._dataScreen != null;

  [PXButton(IsLockedOnToolbar = true)]
  [PXUIField(DisplayName = "View Screen")]
  protected void ViewScreen()
  {
    AUSchedule current = this.Schedule.Current;
    if (current == null || string.IsNullOrEmpty(current.ScreenID))
      return;
    PXSiteMapNode mapNodeByScreenId = PXSiteMap.Provider.FindSiteMapNodeByScreenID(current.ScreenID);
    if (mapNodeByScreenId != null && !string.IsNullOrEmpty(mapNodeByScreenId.Url))
      throw new PXRedirectToUrlException(mapNodeByScreenId.Url, nameof (ViewScreen));
  }

  [PXButton(IsLockedOnToolbar = true)]
  [PXUIField(DisplayName = "View History")]
  protected void ViewHistory()
  {
    AUSchedule current = this.Schedule.Current;
    if (current != null && current.ScheduleID.HasValue)
    {
      AUScheduleExecutionMaint instance = PXGraph.CreateInstance<AUScheduleExecutionMaint>();
      instance.Filter.Current.ScheduleID = current.ScheduleID;
      throw new PXRedirectRequiredException((PXGraph) instance, true, nameof (ViewHistory));
    }
  }

  protected virtual IEnumerable fills()
  {
    if (this._Info != null && this.Schedule.Current != null && !string.IsNullOrEmpty(this.Schedule.Current.FilterName))
    {
      using (new ReadOnlyScope(new PXCache[1]
      {
        this.Fills.Cache
      }))
      {
        string[] array;
        if (!this._Info.Views.TryGetValue(this.Schedule.Current.FilterName, out array))
          return (IEnumerable) null;
        foreach (AUScheduleFill auScheduleFill in this.Fills.Cache.Inserted)
        {
          if (Array.IndexOf<string>(array, auScheduleFill.FieldName) == -1)
            this.Fills.Delete(auScheduleFill);
        }
      }
    }
    return (IEnumerable) null;
  }

  protected virtual void AUSchedule_RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    if (!(e.Row is AUSchedule row) || row.ScreenID != null)
      return;
    row.ScreenID = this.ScheduleCurrentScreen.Current.ScreenID;
  }

  protected virtual void AUSchedule_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    AUSchedule schedule = (AUSchedule) e.Row;
    AUSchedule auSchedule1 = schedule;
    int? scheduleId;
    int num1;
    if (auSchedule1 == null)
    {
      num1 = 0;
    }
    else
    {
      scheduleId = auSchedule1.ScheduleID;
      num1 = scheduleId.HasValue ? 1 : 0;
    }
    if (num1 != 0)
    {
      PXAction<AUSchedule> viewHistory = this.viewHistory;
      scheduleId = schedule.ScheduleID;
      int num2 = 0;
      int num3 = scheduleId.GetValueOrDefault() > num2 & scheduleId.HasValue ? 1 : 0;
      viewHistory.SetEnabled(num3 != 0);
      if (!string.IsNullOrEmpty(schedule.ScreenID))
      {
        this._Info = ScreenUtils.ScreenInfo.TryGet(schedule.ScreenID);
        this.viewScreen.SetEnabled(true);
      }
    }
    else
    {
      this._Info = (PXSiteMap.ScreenInfo) null;
      this.viewHistory.SetEnabled(false);
      this.viewScreen.SetEnabled(false);
    }
    bool flag1 = false;
    if (schedule != null)
    {
      bool isVisible = this.CurrentUserInformationProvider.GetActiveBranches().Count<BranchInfo>() > 1;
      PXUIFieldAttribute.SetVisible<AUSchedule.branchID>(sender, (object) schedule, isVisible);
    }
    if (schedule == null)
    {
      this._dataScreen = (DataScreenBase) null;
      PXStringListAttribute.SetList<AUSchedule.actionName>(this.Schedule.Cache, (object) null, new string[1], new string[1]
      {
        ""
      });
      PXStringListAttribute.SetList<AUScheduleFilter.fieldName>(this.Filters.Cache, (object) null, new string[1], new string[1]
      {
        ""
      });
      PXStringListAttribute.SetList<AUScheduleFill.fieldName>(this.Fills.Cache, (object) null, new string[1], new string[1]
      {
        ""
      });
    }
    else if (string.IsNullOrEmpty(schedule.GraphName))
    {
      this._dataScreen = (DataScreenBase) null;
      PXStringListAttribute.SetList<AUSchedule.actionName>(this.Schedule.Cache, (object) null, new string[1], new string[1]
      {
        ""
      });
      PXStringListAttribute.SetList<AUScheduleFilter.fieldName>(this.Filters.Cache, (object) null, new string[1], new string[1]
      {
        ""
      });
      PXStringListAttribute.SetList<AUScheduleFill.fieldName>(this.Fills.Cache, (object) null, new string[1], new string[1]
      {
        ""
      });
      this.SetControlsState(sender, schedule);
    }
    else
    {
      if (this._Graph == null || CustomizedTypeManager.GetTypeNotCustomized(this._Graph).FullName != schedule.GraphName)
      {
        try
        {
          this.SetDataScreen(schedule.ScreenID);
          sender.RaiseExceptionHandling<AUSchedule.screenID>((object) schedule, (object) schedule.ScreenID, (Exception) null);
        }
        catch (PXException ex)
        {
          this.SetDataScreen((string) null);
          sender.RaiseExceptionHandling<AUSchedule.screenID>((object) schedule, (object) schedule.ScreenID, (Exception) new PXSetPropertyException(ex.Message, PXErrorLevel.Error));
        }
        catch (Exception ex)
        {
          this.SetDataScreen((string) null);
          sender.RaiseExceptionHandling<AUSchedule.screenID>((object) schedule, (object) schedule.ScreenID, (Exception) new PXSetPropertyException("The following error has occurred on the {0} form: \"{1}\". Please, review the form settings.", new object[2]
          {
            (object) schedule.ScreenID,
            (object) ex.Message
          }));
        }
      }
      if (this._Graph == null || string.IsNullOrEmpty(schedule.ViewName))
      {
        PXStringListAttribute.SetList<AUSchedule.actionName>(this.Schedule.Cache, (object) null, new string[1], new string[1]
        {
          ""
        });
        PXStringListAttribute.SetList<AUScheduleFilter.fieldName>(this.Filters.Cache, (object) null, new string[1], new string[1]
        {
          ""
        });
        PXStringListAttribute.SetList<AUScheduleFill.fieldName>(this.Fills.Cache, (object) null, new string[1], new string[1]
        {
          ""
        });
      }
      else
      {
        PXView view;
        if (this._Graph.Views.TryGetValue(schedule.ViewName, out view) && view is IPXProcessingView)
        {
          if (((IPXProcessingView) view).FilterName != null)
          {
            schedule.FilterName = schedule.ViewName;
            schedule.ViewName = ((IPXProcessingView) view).ViewName;
            view = this._Graph.Views[schedule.ViewName];
          }
          sender.RaiseExceptionHandling<AUSchedule.screenID>((object) schedule, (object) schedule.ScreenID, (Exception) null);
          schedule.TableName = this._Graph.Views[schedule.ViewName].GetItemType().FullName;
          OrderedHashSet<string> names = new OrderedHashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
          List<string> displayNames = new List<string>();
          if (this._Info != null)
          {
            foreach (PXSiteMap.ScreenInfo.Action action1 in this._Info.Actions)
            {
              string name = action1.Name;
              if (name == schedule.ActionName || name == "ProcessAll")
              {
                PXAction action2 = this._Graph.Actions[name];
                if (action2 != null)
                {
                  if (action2.GetState((object) null) is PXFieldState state && !string.IsNullOrEmpty(state.DisplayName))
                    AddName(name, state.DisplayName);
                  else
                    AddName(name, name);
                }
                else
                  AddName(name, name);
              }
            }
          }
          else
          {
            foreach (DictionaryEntry action in (OrderedDictionary) this._Graph.Actions)
            {
              string key = (string) action.Key;
              if (key == schedule.ActionName || key == "ProcessAll")
              {
                PXAction pxAction = (PXAction) action.Value;
                if (pxAction != null)
                {
                  if (pxAction.GetState((object) null) is PXFieldState state && !string.IsNullOrEmpty(state.DisplayName))
                    AddName(key, state.DisplayName);
                  else
                    AddName(key, key);
                }
                else
                  AddName(key, key);
              }
            }
          }
          if (this._Graph is PXGenericInqGrph)
            AddName("RaiseEvent", "Raise Business Event");
          PXCache pxCache = sender;
          AUSchedule row = schedule;
          string actionName = schedule.ActionName;
          PXSetPropertyException propertyException;
          if (!(schedule.ActionName == "Process"))
            propertyException = (PXSetPropertyException) null;
          else
            propertyException = new PXSetPropertyException("The {0} action is not valid to be performed by schedule. Select the {1} option because there is no way to perform a selection of records for processing.", new object[3]
            {
              (object) displayNames[((IEnumerable<string>) names).ToList<string>().IndexOf("Process")],
              (object) displayNames[((IEnumerable<string>) names).ToList<string>().IndexOf("ProcessAll")],
              (object) PXErrorLevel.Error
            });
          pxCache.RaiseExceptionHandling<AUSchedule.actionName>((object) row, (object) actionName, (Exception) propertyException);
          PXStringListAttribute.SetList<AUSchedule.actionName>(this.Schedule.Cache, (object) null, ((IEnumerable<string>) names).ToArray<string>(), displayNames.ToArray());
          names.Clear();
          displayNames.Clear();
          string[] array = (string[]) null;
          if (this._Info != null && this._Info.Views.ContainsKey(schedule.ViewName))
            array = this._Info.Views[schedule.ViewName];
          foreach (PXFieldState field in PXFieldState.GetFields(this._Graph, view.BqlSelect.GetTables(), true))
          {
            PXFieldState s = field;
            if (array == null || Array.Exists<string>(array, (Predicate<string>) (name => name == s.Name)))
              AddName(s.Name, s.DisplayName);
          }
          foreach (PXResult<AUScheduleFilter> pxResult in this.Filters.Select())
          {
            AUScheduleFilter auScheduleFilter = (AUScheduleFilter) pxResult;
            if (!string.IsNullOrEmpty(auScheduleFilter.FieldName))
              AddName(auScheduleFilter.FieldName, auScheduleFilter.FieldName);
          }
          PXStringListAttribute.SetList<AUScheduleFilter.fieldName>(this.Filters.Cache, (object) null, ((IEnumerable<string>) names).ToArray<string>(), displayNames.ToArray());
          if (!string.IsNullOrEmpty(schedule.FilterName))
          {
            names.Clear();
            displayNames.Clear();
            Dictionary<string, string> dictionary = (Dictionary<string, string>) null;
            if (this._Info != null && this._Info.Views.ContainsKey(schedule.FilterName))
            {
              IEqualityComparer<string> comparer = this._Info.Containers.Comparer;
              dictionary = this._Info.Containers.Where<KeyValuePair<string, PXViewDescription>>((Func<KeyValuePair<string, PXViewDescription>, bool>) (x => comparer.Equals(SyMappingUtils.CleanViewName(x.Key), schedule.FilterName))).SelectMany<KeyValuePair<string, PXViewDescription>, PX.Data.Description.FieldInfo>((Func<KeyValuePair<string, PXViewDescription>, IEnumerable<PX.Data.Description.FieldInfo>>) (x => (IEnumerable<PX.Data.Description.FieldInfo>) x.Value.Fields)).ToDictionary<PX.Data.Description.FieldInfo, string, string>((Func<PX.Data.Description.FieldInfo, string>) (c => c.DisplayName), (Func<PX.Data.Description.FieldInfo, string>) (c => c.FieldName));
            }
            PXView pxView;
            if (this._Graph.Views.TryGetValue(schedule.FilterName, out pxView))
            {
              PXGraph graph = this._Graph;
              System.Type[] tables = new System.Type[1]
              {
                pxView.BqlSelect.GetTables()[0]
              };
              foreach (PXFieldState field in PXFieldState.GetFields(graph, tables, true))
              {
                if (dictionary == null || dictionary.ContainsKey(field.DisplayName))
                  AddName(dictionary != null ? dictionary[field.DisplayName] : field.Name, field.DisplayName);
              }
            }
            foreach (PXResult<AUScheduleFill> pxResult in this.Fills.Select())
            {
              AUScheduleFill data = (AUScheduleFill) pxResult;
              string fieldName = data.FieldName;
              if (!string.IsNullOrEmpty(fieldName))
              {
                AddName(fieldName, fieldName);
                PXFieldState stateExt = this.Fills.Cache.GetStateExt((object) data, "Value") as PXFieldState;
                if (fieldName == "Action" && stateExt is PXStringState pxStringState && pxStringState.AllowedValues != null && !((IEnumerable<string>) pxStringState.AllowedValues).Contains<string>(data.Value))
                {
                  sender.RaiseExceptionHandling<AUSchedule.isActive>((object) schedule, (object) schedule.ScreenID, (Exception) new PXSetPropertyException("This schedule cannot be run because it has an incorrect value of the Action field in the Value column. Correct the value on the Filter Values tab.", PXErrorLevel.Warning));
                  flag1 = true;
                }
              }
            }
            PXStringListAttribute.SetList<AUScheduleFill.fieldName>(this.Fills.Cache, (object) null, ((IEnumerable<string>) names).ToArray<string>(), displayNames.ToArray());
          }

          void AddName(string name, string displayName)
          {
            AUScheduleMaint.AddUniqueName(name, displayName, names, displayNames);
          }
        }
        else
        {
          sender.RaiseExceptionHandling<AUSchedule.screenID>((object) schedule, (object) schedule.ScreenID, (Exception) new PXSetPropertyException("This form cannot be scheduled.", PXErrorLevel.Error));
          this.viewHistory.SetEnabled(false);
          this.viewScreen.SetEnabled(false);
          return;
        }
      }
      this.SetControlsState(sender, schedule);
    }
    AUSchedule auSchedule2 = schedule;
    bool? nullable1;
    int num4;
    if (auSchedule2 == null)
    {
      num4 = 0;
    }
    else
    {
      nullable1 = auSchedule2.IsActive;
      bool flag2 = false;
      num4 = nullable1.GetValueOrDefault() == flag2 & nullable1.HasValue ? 1 : 0;
    }
    if (num4 != 0)
    {
      short? abortCntr = schedule.AbortCntr;
      int? nullable2 = abortCntr.HasValue ? new int?((int) abortCntr.GetValueOrDefault()) : new int?();
      short? maxAbortCount = schedule.MaxAbortCount;
      int? nullable3 = maxAbortCount.HasValue ? new int?((int) maxAbortCount.GetValueOrDefault()) : new int?();
      if (nullable2.GetValueOrDefault() >= nullable3.GetValueOrDefault() & nullable2.HasValue & nullable3.HasValue)
      {
        sender.RaiseExceptionHandling<AUSchedule.isActive>((object) schedule, (object) schedule.IsActive, (Exception) new PXSetPropertyException<AUSchedule.isActive>("The automation schedule has been deactivated after reaching the maximum number of consecutive aborted executions.", PXErrorLevel.Warning));
        flag1 = true;
      }
    }
    if (flag1)
      return;
    PXCache pxCache1 = sender;
    AUSchedule row1 = schedule;
    AUSchedule auSchedule3 = schedule;
    bool? nullable4;
    if (auSchedule3 == null)
    {
      nullable1 = new bool?();
      nullable4 = nullable1;
    }
    else
      nullable4 = auSchedule3.IsActive;
    // ISSUE: variable of a boxed type
    __Boxed<bool?> newValue = (ValueType) nullable4;
    pxCache1.RaiseExceptionHandling<AUSchedule.isActive>((object) row1, (object) newValue, (Exception) null);
  }

  private static void AddUniqueName(
    string name,
    string displayName,
    OrderedHashSet<string> names,
    List<string> displayNames)
  {
    if (!names.Add(name))
      return;
    displayNames.Add(displayName);
  }

  protected virtual void AUSchedule_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    AUSchedule row = (AUSchedule) e.Row;
    if (row == null)
      return;
    if ((e.Operation & PXDBOperation.Delete) != PXDBOperation.Delete)
    {
      this.checkTimes(sender, row, (AUSchedule) null);
    }
    else
    {
      PXResultset<AUScheduleExecution> pxResultset = PXSelectBase<AUScheduleExecution, PXSelect<AUScheduleExecution, Where<AUScheduleExecution.scheduleID, Equal<Required<AUScheduleExecution.scheduleID>>>>.Config>.Select((PXGraph) this, (object) row.ScheduleID);
      PXCache cach = this.Caches[typeof (AUScheduleExecution)];
      foreach (PXResult<AUScheduleExecution> pxResult in pxResultset)
      {
        AUScheduleExecution execRow = (AUScheduleExecution) pxResult;
        AUScheduleExecutionMaint.deleteExecution((PXGraph) this, cach, execRow);
      }
    }
    if (!string.IsNullOrEmpty(row.ScreenID) && this._Info == null)
      sender.RaiseExceptionHandling<AUSchedule.screenID>((object) row, (object) row.ScreenID, (Exception) new PXSetPropertyException("This form cannot be automated."));
    short? historyRetainCount = row.HistoryRetainCount;
    if (!historyRetainCount.HasValue)
      return;
    historyRetainCount = row.HistoryRetainCount;
    int? nullable = historyRetainCount.HasValue ? new int?((int) historyRetainCount.GetValueOrDefault()) : new int?();
    int num = 1;
    if (!(nullable.GetValueOrDefault() < num & nullable.HasValue))
      return;
    sender.RaiseExceptionHandling<AUSchedule.historyRetainCount>((object) row, (object) row.HistoryRetainCount, (Exception) new PXSetPropertyException("At least one schedule execution must be kept in the history."));
  }

  protected virtual void AUSchedule_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    AUSchedule row = (AUSchedule) e.Row;
    this.OnScreenIdChanged(sender, row);
  }

  private void OnScreenIdChanged(PXCache sender, AUSchedule schedule)
  {
    if (schedule == null)
      return;
    if (this._Info == null && !string.IsNullOrEmpty(schedule.ScreenID))
    {
      this._Info = ScreenUtils.ScreenInfo.TryGet(schedule.ScreenID);
      if (this._Info == null)
        sender.RaiseExceptionHandling<AUSchedule.screenID>((object) schedule, (object) schedule.ScreenID, (Exception) new PXSetPropertyException("This form cannot be automated."));
    }
    else if (string.IsNullOrEmpty(schedule.ScreenID))
    {
      this._Info = (PXSiteMap.ScreenInfo) null;
      this.SetDataScreen(schedule.ScreenID);
      schedule.GraphName = (string) null;
      schedule.ViewName = (string) null;
      schedule.FilterName = (string) null;
    }
    this.ScheduleCurrentScreen.Current.ScreenID = schedule.ScreenID;
    if (this._Info == null)
      return;
    this.SetDataScreen(schedule.ScreenID);
    schedule.GraphName = this._Info.GraphName;
    schedule.ViewName = this._dataScreen.ViewName;
    schedule.FilterName = this._dataScreen.ParametersViewName;
    this._Graph.Actions.PressCancel();
    if (((IEnumerable<PXSiteMap.ScreenInfo.Action>) this._Info.Actions).Any<PXSiteMap.ScreenInfo.Action>((Func<PXSiteMap.ScreenInfo.Action, bool>) (c => c.Name == "ProcessAll")))
      this.Schedule.Cache.SetValue<AUSchedule.actionName>((object) schedule, (object) "ProcessAll");
    else if (this._Graph is PXGenericInqGrph)
      this.Schedule.Cache.SetValue<AUSchedule.actionName>((object) schedule, (object) "RaiseEvent");
    if (!(this._Graph.Views[schedule.ViewName] is IPXProcessingView))
    {
      schedule.ViewName = this._Info.PrimaryView;
      schedule.FilterName = (string) null;
      if (!(this._Graph.Views[schedule.ViewName] is IPXProcessingView view) || view.FilterName == null)
        return;
      schedule.FilterName = schedule.ViewName;
      schedule.ViewName = view.ViewName;
    }
    if (string.IsNullOrEmpty(schedule.FilterName))
      return;
    PXView view1 = this._Graph.Views[schedule.FilterName];
    ParameterExpression parameterExpression;
    // ISSUE: method reference
    AUScheduleFill[] array = this.Fills.Select().Select<PXResult<AUScheduleFill>, AUScheduleFill>(Expression.Lambda<Func<PXResult<AUScheduleFill>, AUScheduleFill>>((Expression) Expression.Call(c, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PXResult.GetItem)), Array.Empty<Expression>()), parameterExpression)).ToArray<AUScheduleFill>();
    foreach (InqField inqField in this._dataScreen?.GetParameters() ?? Enumerable.Empty<InqField>())
    {
      if (!((IEnumerable<AUScheduleFill>) array).Select<AUScheduleFill, string>((Func<AUScheduleFill, string>) (c => c.FieldName)).Contains<string>(inqField.Name))
      {
        if (this._Graph is PXGenericInqGrph)
          view1.Cache.GetValueExt(view1.Cache.Current, inqField.Name);
        object newValue = view1.Cache.GetValue(view1.Cache.Current, inqField.Name);
        if (newValue == null)
          view1.Cache.RaiseFieldDefaulting(inqField.Name, view1.Cache.Current, out newValue);
        this.AddScheduleFill(newValue, inqField.Name, System.Type.GetTypeCode(inqField.FieldType));
      }
    }
  }

  private void AddScheduleFill(object val, string name, TypeCode type)
  {
    if (val is PXFieldState)
      val = ((PXFieldState) val).Value;
    if (val != null)
      val = !(val is IFormattable formattable) ? (val is IConvertible convertible ? (object) convertible.ToString((IFormatProvider) CultureInfo.InvariantCulture) : (object) val.ToString()) : (object) formattable.ToString((string) null, (IFormatProvider) CultureInfo.InvariantCulture);
    if (val == null)
      return;
    this.Fills.Insert(new AUScheduleFill()
    {
      FieldName = name,
      Value = (string) val
    });
  }

  protected virtual void checkTimes(PXCache sender, AUSchedule row, AUSchedule oldRow)
  {
    System.DateTime? nullable1;
    int? nullable2;
    short? interval1;
    if (oldRow != null)
    {
      System.DateTime? startTime = oldRow.StartTime;
      System.DateTime? nullable3 = row.StartTime;
      if ((startTime.HasValue == nullable3.HasValue ? (startTime.HasValue ? (startTime.GetValueOrDefault() != nullable3.GetValueOrDefault() ? 1 : 0) : 0) : 1) == 0)
      {
        nullable3 = oldRow.EndTime;
        nullable1 = row.EndTime;
        if ((nullable3.HasValue == nullable1.HasValue ? (nullable3.HasValue ? (nullable3.GetValueOrDefault() != nullable1.GetValueOrDefault() ? 1 : 0) : 0) : 1) == 0)
        {
          short? interval2 = oldRow.Interval;
          nullable2 = interval2.HasValue ? new int?((int) interval2.GetValueOrDefault()) : new int?();
          interval1 = row.Interval;
          int? nullable4 = interval1.HasValue ? new int?((int) interval1.GetValueOrDefault()) : new int?();
          if (nullable2.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable2.HasValue == nullable4.HasValue)
            return;
        }
      }
    }
    nullable1 = row.StartTime;
    int? nullable5;
    if (nullable1.HasValue)
    {
      nullable1 = row.EndTime;
      if (!nullable1.HasValue)
      {
        interval1 = row.Interval;
        if (interval1.HasValue)
        {
          interval1 = row.Interval;
          int? nullable6;
          if (!interval1.HasValue)
          {
            nullable2 = new int?();
            nullable6 = nullable2;
          }
          else
            nullable6 = new int?((int) interval1.GetValueOrDefault());
          nullable5 = nullable6;
          int num = 0;
          if (nullable5.GetValueOrDefault() > num & nullable5.HasValue)
            sender.RaiseExceptionHandling<AUSchedule.endTime>((object) row, (object) null, (Exception) new PXSetPropertyException("You have to define the stop time when the start time and an interval are defined."));
        }
      }
    }
    nullable1 = row.StartTime;
    if (!nullable1.HasValue)
    {
      nullable1 = row.EndTime;
      if (nullable1.HasValue)
        sender.RaiseExceptionHandling<AUSchedule.startTime>((object) row, (object) null, (Exception) new PXSetPropertyException("You have to define the start time when the stop time is defined."));
    }
    nullable1 = row.StartTime;
    if (nullable1.HasValue)
      return;
    nullable1 = row.EndTime;
    if (nullable1.HasValue)
      return;
    interval1 = row.Interval;
    if (interval1.HasValue)
    {
      interval1 = row.Interval;
      int? nullable7;
      if (!interval1.HasValue)
      {
        nullable2 = new int?();
        nullable7 = nullable2;
      }
      else
        nullable7 = new int?((int) interval1.GetValueOrDefault());
      nullable5 = nullable7;
      int num = 0;
      if (!(nullable5.GetValueOrDefault() == num & nullable5.HasValue))
        return;
    }
    sender.RaiseExceptionHandling<AUSchedule.startTime>((object) row, (object) null, (Exception) new PXSetPropertyException("You have to define the start time for a single schedule start."));
  }

  protected virtual void AUSchedule_RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    AUSchedule newRow = (AUSchedule) e.NewRow;
    AUSchedule row = (AUSchedule) e.Row;
    this.checkTimes(sender, newRow, row);
  }

  protected virtual void AUSchedule_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    AUSchedule schedule = (AUSchedule) e.Row;
    AUSchedule oldrow = (AUSchedule) e.OldRow;
    if (schedule.ScreenID != oldrow.ScreenID)
      this.OnScreenIdChanged(sender, schedule);
    System.DateTime? nullable1;
    System.DateTime? nullable2;
    int? nullable3;
    if (schedule.StartTime.HasValue)
    {
      System.DateTime? startTime = schedule.StartTime;
      nullable1 = oldrow.StartTime;
      if ((startTime.HasValue == nullable1.HasValue ? (startTime.HasValue ? (startTime.GetValueOrDefault() != nullable1.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
      {
        nullable1 = schedule.LastRunDate;
        if (nullable1.HasValue)
          AUScheduleMaint.CheckNextRunTimeInRangeAndSet(sender, schedule, (System.Action<System.DateTime>) (_ => schedule.NextRunTime = schedule.StartTime));
        else
          schedule.NextRunTime = schedule.StartTime;
      }
      else
      {
        nullable1 = schedule.LastRunDate;
        if (nullable1.HasValue)
        {
          nullable1 = schedule.EndTime;
          if (nullable1.HasValue)
          {
            nullable1 = schedule.EndTime;
            nullable2 = oldrow.EndTime;
            if ((nullable1.HasValue == nullable2.HasValue ? (nullable1.HasValue ? (nullable1.GetValueOrDefault() != nullable2.GetValueOrDefault() ? 1 : 0) : 0) : 1) == 0)
            {
              short? interval1 = schedule.Interval;
              nullable3 = interval1.HasValue ? new int?((int) interval1.GetValueOrDefault()) : new int?();
              short? interval2 = oldrow.Interval;
              int? nullable4 = interval2.HasValue ? new int?((int) interval2.GetValueOrDefault()) : new int?();
              if (nullable3.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable3.HasValue == nullable4.HasValue)
                goto label_12;
            }
            AUScheduleMaint.CheckNextRunTimeInRangeAndSet(sender, schedule, (System.Action<System.DateTime>) (nextRunTime =>
            {
              short? interval = schedule.Interval;
              int? nullable5 = interval.HasValue ? new int?((int) interval.GetValueOrDefault()) : new int?();
              int num = 0;
              if (nullable5.GetValueOrDefault() == num & nullable5.HasValue)
                return;
              interval = schedule.Interval;
              nullable5 = interval.HasValue ? new int?((int) interval.GetValueOrDefault()) : new int?();
              interval = oldrow.Interval;
              int? nullable6 = interval.HasValue ? new int?((int) interval.GetValueOrDefault()) : new int?();
              if (nullable5.GetValueOrDefault() == nullable6.GetValueOrDefault() & nullable5.HasValue == nullable6.HasValue)
                return;
              schedule.NextRunTime = new System.DateTime?(nextRunTime);
            }));
          }
        }
      }
    }
label_12:
    nullable2 = schedule.StartDate;
    nullable1 = oldrow.StartDate;
    if ((nullable2.HasValue == nullable1.HasValue ? (nullable2.HasValue ? (nullable2.GetValueOrDefault() != nullable1.GetValueOrDefault() ? 1 : 0) : 0) : 1) == 0 && !(schedule.ScheduleType != oldrow.ScheduleType))
    {
      short? periodFrequency1 = schedule.PeriodFrequency;
      int? nullable7 = periodFrequency1.HasValue ? new int?((int) periodFrequency1.GetValueOrDefault()) : new int?();
      short? periodFrequency2 = oldrow.PeriodFrequency;
      nullable3 = periodFrequency2.HasValue ? new int?((int) periodFrequency2.GetValueOrDefault()) : new int?();
      if (nullable7.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable7.HasValue == nullable3.HasValue && !(schedule.PeriodDateSel != oldrow.PeriodDateSel))
      {
        short? periodFixedDay1 = schedule.PeriodFixedDay;
        nullable3 = periodFixedDay1.HasValue ? new int?((int) periodFixedDay1.GetValueOrDefault()) : new int?();
        short? periodFixedDay2 = oldrow.PeriodFixedDay;
        nullable7 = periodFixedDay2.HasValue ? new int?((int) periodFixedDay2.GetValueOrDefault()) : new int?();
        if (nullable3.GetValueOrDefault() == nullable7.GetValueOrDefault() & nullable3.HasValue == nullable7.HasValue)
        {
          short? weeklyFrequency1 = schedule.WeeklyFrequency;
          nullable7 = weeklyFrequency1.HasValue ? new int?((int) weeklyFrequency1.GetValueOrDefault()) : new int?();
          short? weeklyFrequency2 = oldrow.WeeklyFrequency;
          nullable3 = weeklyFrequency2.HasValue ? new int?((int) weeklyFrequency2.GetValueOrDefault()) : new int?();
          if (nullable7.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable7.HasValue == nullable3.HasValue)
          {
            bool? weeklyOnDay1_1 = schedule.WeeklyOnDay1;
            bool? weeklyOnDay1_2 = oldrow.WeeklyOnDay1;
            if (weeklyOnDay1_1.GetValueOrDefault() == weeklyOnDay1_2.GetValueOrDefault() & weeklyOnDay1_1.HasValue == weeklyOnDay1_2.HasValue)
            {
              bool? weeklyOnDay2_1 = schedule.WeeklyOnDay2;
              bool? weeklyOnDay2_2 = oldrow.WeeklyOnDay2;
              if (weeklyOnDay2_1.GetValueOrDefault() == weeklyOnDay2_2.GetValueOrDefault() & weeklyOnDay2_1.HasValue == weeklyOnDay2_2.HasValue)
              {
                bool? weeklyOnDay3_1 = schedule.WeeklyOnDay3;
                bool? weeklyOnDay3_2 = oldrow.WeeklyOnDay3;
                if (weeklyOnDay3_1.GetValueOrDefault() == weeklyOnDay3_2.GetValueOrDefault() & weeklyOnDay3_1.HasValue == weeklyOnDay3_2.HasValue)
                {
                  bool? weeklyOnDay4_1 = schedule.WeeklyOnDay4;
                  bool? weeklyOnDay4_2 = oldrow.WeeklyOnDay4;
                  if (weeklyOnDay4_1.GetValueOrDefault() == weeklyOnDay4_2.GetValueOrDefault() & weeklyOnDay4_1.HasValue == weeklyOnDay4_2.HasValue)
                  {
                    bool? weeklyOnDay5_1 = schedule.WeeklyOnDay5;
                    bool? weeklyOnDay5_2 = oldrow.WeeklyOnDay5;
                    if (weeklyOnDay5_1.GetValueOrDefault() == weeklyOnDay5_2.GetValueOrDefault() & weeklyOnDay5_1.HasValue == weeklyOnDay5_2.HasValue)
                    {
                      bool? weeklyOnDay6_1 = schedule.WeeklyOnDay6;
                      bool? weeklyOnDay6_2 = oldrow.WeeklyOnDay6;
                      if (weeklyOnDay6_1.GetValueOrDefault() == weeklyOnDay6_2.GetValueOrDefault() & weeklyOnDay6_1.HasValue == weeklyOnDay6_2.HasValue)
                      {
                        bool? weeklyOnDay7_1 = schedule.WeeklyOnDay7;
                        bool? weeklyOnDay7_2 = oldrow.WeeklyOnDay7;
                        if (weeklyOnDay7_1.GetValueOrDefault() == weeklyOnDay7_2.GetValueOrDefault() & weeklyOnDay7_1.HasValue == weeklyOnDay7_2.HasValue)
                        {
                          short? monthlyFrequency1 = schedule.MonthlyFrequency;
                          nullable3 = monthlyFrequency1.HasValue ? new int?((int) monthlyFrequency1.GetValueOrDefault()) : new int?();
                          short? monthlyFrequency2 = oldrow.MonthlyFrequency;
                          nullable7 = monthlyFrequency2.HasValue ? new int?((int) monthlyFrequency2.GetValueOrDefault()) : new int?();
                          if (nullable3.GetValueOrDefault() == nullable7.GetValueOrDefault() & nullable3.HasValue == nullable7.HasValue && !(schedule.MonthlyDaySel != oldrow.MonthlyDaySel))
                          {
                            short? monthlyOnDay1 = schedule.MonthlyOnDay;
                            nullable7 = monthlyOnDay1.HasValue ? new int?((int) monthlyOnDay1.GetValueOrDefault()) : new int?();
                            short? monthlyOnDay2 = oldrow.MonthlyOnDay;
                            nullable3 = monthlyOnDay2.HasValue ? new int?((int) monthlyOnDay2.GetValueOrDefault()) : new int?();
                            if (nullable7.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable7.HasValue == nullable3.HasValue)
                            {
                              short? monthlyOnDayOfWeek1 = schedule.MonthlyOnDayOfWeek;
                              nullable3 = monthlyOnDayOfWeek1.HasValue ? new int?((int) monthlyOnDayOfWeek1.GetValueOrDefault()) : new int?();
                              short? monthlyOnDayOfWeek2 = oldrow.MonthlyOnDayOfWeek;
                              nullable7 = monthlyOnDayOfWeek2.HasValue ? new int?((int) monthlyOnDayOfWeek2.GetValueOrDefault()) : new int?();
                              if (nullable3.GetValueOrDefault() == nullable7.GetValueOrDefault() & nullable3.HasValue == nullable7.HasValue)
                              {
                                short? monthlyOnWeek1 = schedule.MonthlyOnWeek;
                                nullable7 = monthlyOnWeek1.HasValue ? new int?((int) monthlyOnWeek1.GetValueOrDefault()) : new int?();
                                short? monthlyOnWeek2 = oldrow.MonthlyOnWeek;
                                nullable3 = monthlyOnWeek2.HasValue ? new int?((int) monthlyOnWeek2.GetValueOrDefault()) : new int?();
                                if (nullable7.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable7.HasValue == nullable3.HasValue)
                                {
                                  short? dailyFrequency1 = schedule.DailyFrequency;
                                  nullable3 = dailyFrequency1.HasValue ? new int?((int) dailyFrequency1.GetValueOrDefault()) : new int?();
                                  short? dailyFrequency2 = oldrow.DailyFrequency;
                                  nullable7 = dailyFrequency2.HasValue ? new int?((int) dailyFrequency2.GetValueOrDefault()) : new int?();
                                  if (nullable3.GetValueOrDefault() == nullable7.GetValueOrDefault() & nullable3.HasValue == nullable7.HasValue)
                                    goto label_29;
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
    }
    AUSchedule auSchedule1 = schedule;
    nullable1 = schedule.LastRunDate;
    System.DateTime? nullable8 = nullable1 ?? schedule.StartDate;
    auSchedule1.NextRunDate = nullable8;
    schedule.ApplyAdjustmentRule(this.AdjustmentRuleProvider);
label_29:
    AUSchedule auSchedule2 = schedule;
    nullable1 = schedule.NextRunDate;
    System.DateTime date = nullable1.Value;
    date = date.Date;
    ref System.DateTime local = ref date;
    nullable1 = schedule.NextRunTime;
    int hour = nullable1.Value.Hour;
    nullable1 = schedule.NextRunTime;
    int minute = nullable1.Value.Minute;
    TimeSpan timeSpan = new TimeSpan(hour, minute, 0);
    System.DateTime? nullable9 = new System.DateTime?(local.Add(timeSpan));
    auSchedule2.NextRunTime = nullable9;
    this.ScheduleCurrentScreen.Current.ScreenID = schedule.ScreenID;
  }

  protected virtual void _(
    Events.FieldUpdated<AUSchedule, AUSchedule.isActive> e)
  {
    bool? newValue = (bool?) e.NewValue;
    bool flag = true;
    if (!(newValue.GetValueOrDefault() == flag & newValue.HasValue))
      return;
    e.Row.AbortCntr = new short?((short) 0);
  }

  protected virtual void AUSchedule_ScreenID_FieldUpdating(
    PXCache sender,
    PXFieldUpdatingEventArgs e)
  {
    if (!(e.NewValue is string))
      return;
    e.NewValue = (object) ((string) e.NewValue).Replace(".", "");
  }

  protected virtual void AUSchedule_ScreenID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!e.ExternalCall)
      return;
    this.OnScreenIdChanged(sender, e.Row as AUSchedule);
  }

  protected virtual void AUSchedule_TemplateScreenID_FieldUpdating(
    PXCache sender,
    PXFieldUpdatingEventArgs e)
  {
    if (!(e.NewValue is string))
      return;
    e.NewValue = (object) ((string) e.NewValue).Replace(".", "");
  }

  protected virtual void AUSchedule_Interval_FieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    e.Cancel = true;
    string str = "0000";
    if (e.ReturnValue != null)
    {
      short int16 = Convert.ToInt16(e.ReturnValue);
      str = $"{(int) int16 / 60:00}{(int) int16 % 60:00}";
    }
    PXStringState instance = (PXStringState) PXStringState.CreateInstance((object) str, new int?(4), new bool?(false), typeof (AUSchedule.interval).Name, new bool?(false), new int?(0), "99:99", (string[]) null, (string[]) null, new bool?(), (string) null);
    instance.DisplayName = PXLocalizer.Localize("Every (hh:mm)", (string) null);
    e.ReturnState = (object) instance;
  }

  protected virtual void AUSchedule_Interval_FieldUpdating(
    PXCache sender,
    PXFieldUpdatingEventArgs e)
  {
    e.Cancel = true;
    if (e.NewValue is string newValue && newValue.Trim() != "")
    {
      short num = short.Parse(newValue);
      e.NewValue = (object) (short) ((int) num / 100 % 24 * 60 + (int) num % 100 % 60);
    }
    else
      e.NewValue = (object) (short) 0;
  }

  protected virtual void AUSchedule_NextRunTime_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (e.Row is AUSchedule row && row.StartTime.HasValue)
      e.NewValue = (object) row.StartTime;
    else
      e.NewValue = (object) PXTimeZoneInfo.Now;
  }

  protected virtual void AUSchedule_NextRunTime_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (e.NewValue == null)
      throw new PXSetPropertyException(PXMessages.LocalizeFormat("'{0}' cannot be empty.", (object) PXUIFieldAttribute.GetDisplayName<AUSchedule.nextRunTime>(this.Caches[typeof (AUSchedule)])));
    if (!(e.Row is AUSchedule row))
      return;
    System.DateTime? nullable = row.StartTime;
    if (!nullable.HasValue)
      return;
    nullable = row.EndTime;
    if (nullable.HasValue && !row.IsTimeInActiveRange((System.DateTime) e.NewValue))
      throw new PXSetPropertyException("Next Execution Time is not in a valid time range.");
  }

  protected virtual void AUSchedule_StartDate_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) this._Today;
  }

  protected virtual void AUSchedule_NextRunDate_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) this._Today;
  }

  protected virtual void AUSchedule_NextRunDate_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    System.DateTime newValue = (System.DateTime) e.NewValue;
    AUSchedule row = e.Row as AUSchedule;
    System.DateTime dateTime = newValue;
    System.DateTime? startDate = row.StartDate;
    if ((startDate.HasValue ? (dateTime < startDate.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      throw new PXSetPropertyException("Next Execution Date is not in a valid time range.");
  }

  protected virtual void AUSchedule_NoRunLimit_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    AUSchedule row = (AUSchedule) e.Row;
    bool? noRunLimit = row.NoRunLimit;
    bool flag = true;
    if (!(noRunLimit.GetValueOrDefault() == flag & noRunLimit.HasValue))
      return;
    row.RunLimit = new short?();
  }

  protected virtual void AUSchedule_NoEndDate_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    AUSchedule row = (AUSchedule) e.Row;
    bool? noEndDate = row.NoEndDate;
    bool flag = true;
    if (!(noEndDate.GetValueOrDefault() == flag & noEndDate.HasValue))
      return;
    row.EndDate = new System.DateTime?();
  }

  protected virtual void AUSchedule_KeepFullHistory_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    AUSchedule row = (AUSchedule) e.Row;
    bool? keepFullHistory = row.KeepFullHistory;
    bool flag = true;
    row.HistoryRetainCount = keepFullHistory.GetValueOrDefault() == flag & keepFullHistory.HasValue ? new short?() : new short?((short) 1);
  }

  protected virtual void AUSchedule_TimeZoneID_FieldDefaulting(
    PXCache cache,
    PXFieldDefaultingEventArgs e)
  {
    if (e.NewValue != null)
      return;
    e.NewValue = (object) LocaleInfo.GetTimeZone().Id;
  }

  protected virtual void AUScheduleFilter_Value_FieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    this.ValueFieldSelecting<AUScheduleFilter.value>(sender, e);
  }

  protected virtual void AUScheduleFilter_Value2_FieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    this.ValueFieldSelecting<AUScheduleFilter.value2>(sender, e);
  }

  private void ValueFieldSelecting<Field>(PXCache sender, PXFieldSelectingEventArgs e) where Field : IBqlField
  {
    try
    {
      this.SetFilterValue(sender, e, false, typeof (AUScheduleFilter.value).Equals(typeof (Field)));
      if (!(e.ReturnState is PXFieldState returnState))
        return;
      returnState.IsReadOnly = false;
    }
    catch (Exception ex)
    {
      sender.RaiseExceptionHandling<Field>(e.Row, e.ReturnValue, (Exception) new PXSetPropertyException(ex.Message));
      e.ReturnValue = (object) null;
    }
  }

  protected virtual void AUScheduleFill_Value_FieldUpdating(
    PXCache sender,
    PXFieldUpdatingEventArgs e)
  {
    AUScheduleFill row = (AUScheduleFill) e.Row;
    if (row == null || e.NewValue == null || this._Graph == null || this.Schedule.Current == null || string.IsNullOrEmpty(this.Schedule.Current.FilterName) || string.IsNullOrEmpty(row.FieldName))
      return;
    PXCache cache = this._Graph.Views[this.Schedule.Current.FilterName].Cache;
    if ((!RelativeDatesManager.IsRelativeDatesString(e.NewValue as string) ? 1 : (!(cache.GetStateExt(cache.Current, row.FieldName) is PXDateState) ? 1 : 0)) == 0)
      return;
    cache.SetValueExt(cache.Current, row.FieldName, e.NewValue);
    object obj = cache.GetValue(cache.Current, row.FieldName);
    e.NewValue = obj != null ? (object) obj.ToInvariantString() : (object) (string) null;
  }

  protected virtual void AUScheduleFill_Value_FieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    try
    {
      this.SetFilterValue(sender, e, true, true);
    }
    catch (Exception ex)
    {
      sender.RaiseExceptionHandling<AUScheduleFill.value>(e.Row, e.ReturnValue, (Exception) new PXSetPropertyException(ex.Message));
      e.ReturnValue = (object) null;
    }
  }

  protected virtual void _(Events.FieldVerifying<AUSchedule.dailyFrequency> e)
  {
    this.CheckScheduleFrequency(e.NewValue);
  }

  protected virtual void _(
    Events.FieldVerifying<AUSchedule.weeklyFrequency> e)
  {
    this.CheckScheduleFrequency(e.NewValue);
  }

  protected virtual void _(
    Events.FieldVerifying<AUSchedule.periodFrequency> e)
  {
    this.CheckScheduleFrequency(e.NewValue);
  }

  private void CheckScheduleFrequency(object newValue)
  {
    short? nullable1 = (short?) newValue;
    int? nullable2 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
    int num = 0;
    if (!(nullable2.GetValueOrDefault() > num & nullable2.HasValue))
      throw new PXSetPropertyException("The value in this box must be greater than or equal to 1.");
  }

  private void SetFilterValue(
    PXCache sender,
    PXFieldSelectingEventArgs e,
    bool isFilter,
    bool isFirstValue)
  {
    IHistoryFilter row = (IHistoryFilter) e.Row;
    if (row == null)
      return;
    string fieldName = row.FieldName;
    if (string.IsNullOrEmpty(fieldName) || this._Graph == null)
      return;
    AUSchedule current = this.Schedule.Current;
    if (current == null)
      return;
    string str = isFilter ? current.FilterName : current.ViewName;
    if (string.IsNullOrEmpty(str))
      return;
    PXCache cache = this._Graph.Views[str].Cache;
    object obj = (object) null;
    if (isFilter)
      obj = cache.Current;
    if (obj != null)
      cache.RaiseRowSelected(obj);
    PXFieldState pxFieldState;
    if (isFilter)
    {
      object returnValue1 = this.ConvertValueType(sender, e);
      PXUIFieldAttribute.SetError(cache, obj, fieldName, (string) null);
      cache.SetValue(cache.Current, fieldName, returnValue1);
      cache.RaiseFieldSelecting(fieldName, obj, ref returnValue1, true);
      pxFieldState = returnValue1 as PXFieldState;
      object returnValue2 = !RelativeDatesManager.IsRelativeDatesString(e.ReturnValue as string) || !(pxFieldState is PXDateState) ? (object) null : e.ReturnValue;
      e.ReturnState = (object) pxFieldState;
      if (returnValue2 != null)
        e.ReturnValue = returnValue2;
    }
    else
      pxFieldState = this._Graph.GetStateExt(str, obj, fieldName) as PXFieldState;
    if (pxFieldState == null)
      return;
    if (!string.IsNullOrEmpty(pxFieldState.ViewName))
    {
      PXView view = this._Graph.Views[pxFieldState.ViewName];
      pxFieldState.ViewName = "$Outer$" + pxFieldState.ViewName;
      this.Views[pxFieldState.ViewName] = view;
      if (pxFieldState.DataType == typeof (string))
        pxFieldState.DescriptionName = (string) null;
    }
    if (!isFilter)
      pxFieldState.Value = e.ReturnValue;
    if (!isFilter)
      pxFieldState.Enabled = true;
    if (pxFieldState.DataType == typeof (bool) && pxFieldState.Value == null)
    {
      if (isFirstValue)
        row.Value = "False";
      else
        row.Value2 = "False";
    }
    if (pxFieldState is PXStringState pxStringState && pxStringState.AllowedLabels != null && pxStringState.AllowedLabels.Length != 0)
    {
      for (int index1 = pxStringState.AllowedLabels.Length - 1; index1 >= 0; --index1)
      {
        for (int index2 = index1 - 1; index2 >= 0; --index2)
        {
          if (pxStringState.AllowedLabels[index1] == pxStringState.AllowedLabels[index2])
          {
            pxStringState.AllowedLabels[index1] = $"{PXMessages.Localize("Explicit")} - {pxStringState.AllowedLabels[index1]}";
            break;
          }
        }
      }
    }
    if (isFilter && pxFieldState.Enabled)
      pxFieldState.Value = e.ReturnValue;
    e.ReturnState = (object) pxFieldState;
    e.Cancel = true;
  }

  private object ConvertValueType(PXCache sender, PXFieldSelectingEventArgs e)
  {
    object obj = e.ReturnValue;
    if (!(obj is string input) || !(this._Graph.Views[this.Schedule.Current.FilterName].Cache.GetStateExt((object) null, ((AUScheduleFill) e.Row).FieldName) is PXFieldState stateExt))
      return obj;
    System.Type conversionType = stateExt.DataType;
    if (stateExt.ViewName != null)
    {
      PXView view = this._Graph.Views[stateExt.ViewName];
      System.Type type = view.Cache.GetFieldType(view.Cache.GetField(view.BqlSelect is IBqlSearch bqlSelect ? bqlSelect.GetField() : (System.Type) null));
      if ((object) type == null)
        type = stateExt.DataType;
      conversionType = type;
    }
    if (conversionType == (System.Type) null)
      return obj;
    try
    {
      if (conversionType == typeof (Guid))
        obj = (object) Guid.Parse(input);
      else if (conversionType == typeof (bool))
      {
        bool result;
        if (bool.TryParse(input, out result))
        {
          obj = (object) result;
        }
        else
        {
          string a = input.Trim();
          if (string.Equals(a, "0", StringComparison.OrdinalIgnoreCase))
            obj = (object) false;
          else if (string.Equals(a, "1", StringComparison.OrdinalIgnoreCase))
            obj = (object) true;
        }
      }
      else
        obj = !(conversionType == typeof (System.DateTime)) || !RelativeDatesManager.IsRelativeDatesString(input) ? Convert.ChangeType(obj, conversionType, (IFormatProvider) CultureInfo.InvariantCulture) : (object) RelativeDatesManager.EvaluateAsDateTime(input);
    }
    catch (Exception ex)
    {
      return obj;
    }
    return obj;
  }

  private static void CheckNextRunTimeInRangeAndSet(
    PXCache sender,
    AUSchedule schedule,
    System.Action<System.DateTime> setNextRunTime)
  {
    System.DateTime dateTime = schedule.LastRunDate.Value.AddMinutes((double) schedule.Interval.Value);
    if (AUScheduleMaint.IsTimeInRange(AUScheduleMaint.GetMinutes(dateTime), schedule.StartTime.Value, schedule.EndTime))
    {
      sender.RaiseExceptionHandling<AUSchedule.nextRunTime>((object) schedule, (object) schedule.NextRunTime, (Exception) null);
      setNextRunTime(dateTime);
    }
    else
      sender.RaiseExceptionHandling<AUSchedule.nextRunTime>((object) schedule, (object) dateTime, (Exception) new PXException("Next Execution Time is not in a valid time range."));
  }

  private static bool IsTimeInRange(int time, System.DateTime from, System.DateTime? to)
  {
    if (!to.HasValue)
      return time >= AUScheduleMaint.GetMinutes(from);
    System.DateTime dateTime1 = from;
    System.DateTime? nullable1 = to;
    if ((nullable1.HasValue ? (dateTime1 <= nullable1.GetValueOrDefault() ? 1 : 0) : 0) != 0 && time <= AUScheduleMaint.GetMinutes(to.Value) && time >= AUScheduleMaint.GetMinutes(from))
      return true;
    System.DateTime dateTime2 = from;
    System.DateTime? nullable2 = to;
    if ((nullable2.HasValue ? (dateTime2 > nullable2.GetValueOrDefault() ? 1 : 0) : 0) == 0)
      return false;
    return time <= AUScheduleMaint.GetMinutes(to.Value) || time >= AUScheduleMaint.GetMinutes(from);
  }

  private static int GetMinutes(System.DateTime dateTime)
  {
    TimeSpan timeOfDay = dateTime.TimeOfDay;
    int num = timeOfDay.Hours * 60;
    timeOfDay = dateTime.TimeOfDay;
    int minutes = timeOfDay.Minutes;
    return num + minutes;
  }

  public override object GetValueExt(string viewName, object data, string fieldName)
  {
    return viewName.StartsWith("$Outer$") && this._Graph != null ? this._Graph.GetValueExt(viewName.Substring(7), data, fieldName) : base.GetValueExt(viewName, data, fieldName);
  }

  public override IEnumerable ExecuteSelect(
    string viewName,
    object[] parameters,
    object[] searches,
    string[] sortcolumns,
    bool[] descendings,
    PXFilterRow[] filters,
    ref int startRow,
    int maximumRows,
    ref int totalRows)
  {
    if (this._Graph == null || !viewName.StartsWith("$Outer$"))
      return base.ExecuteSelect(viewName, parameters, searches, sortcolumns, descendings, filters, ref startRow, maximumRows, ref totalRows);
    object withFilterValues = this.GetCurrentWithFilterValues();
    string viewName1 = viewName;
    object[] currents;
    if (withFilterValues != null)
      currents = new object[1]{ withFilterValues };
    else
      currents = (object[]) null;
    object[] parameters1 = parameters;
    object[] searches1 = searches;
    string[] sortcolumns1 = sortcolumns;
    bool[] descendings1 = descendings;
    PXFilterRow[] filters1 = filters;
    ref int local1 = ref startRow;
    int maximumRows1 = maximumRows;
    ref int local2 = ref totalRows;
    return this.ExecuteSelect(viewName1, currents, parameters1, searches1, sortcolumns1, descendings1, filters1, ref local1, maximumRows1, ref local2);
  }

  private object GetCurrentWithFilterValues()
  {
    AUScheduleFilter currentFilter = this.Filters.Current;
    AUScheduleFilter[] array = this.Filters.Select().AsEnumerable<PXResult<AUScheduleFilter>>().Select<PXResult<AUScheduleFilter>, AUScheduleFilter>((Func<PXResult<AUScheduleFilter>, AUScheduleFilter>) (c => c.GetItem<AUScheduleFilter>())).Where<AUScheduleFilter>((Func<AUScheduleFilter, bool>) (filter =>
    {
      short? rowNbr1 = filter.RowNbr;
      int? nullable1 = rowNbr1.HasValue ? new int?((int) rowNbr1.GetValueOrDefault()) : new int?();
      short? rowNbr2 = currentFilter.RowNbr;
      int? nullable2 = rowNbr2.HasValue ? new int?((int) rowNbr2.GetValueOrDefault()) : new int?();
      bool withFilterValues = nullable1.GetValueOrDefault() < nullable2.GetValueOrDefault() & nullable1.HasValue & nullable2.HasValue;
      if (withFilterValues)
      {
        nullable2 = filter.Condition;
        bool flag;
        if (nullable2.HasValue)
        {
          switch (nullable2.GetValueOrDefault())
          {
            case 1:
            case 12:
              flag = true;
              goto label_5;
          }
        }
        flag = false;
label_5:
        withFilterValues = flag;
      }
      return withFilterValues;
    })).ToArray<AUScheduleFilter>();
    if (!((IEnumerable<AUScheduleFilter>) array).Any<AUScheduleFilter>())
      return (object) null;
    PXCache cache = this._Graph.Views[this.Schedule.Current.ViewName].Cache;
    object withFilterValues1 = cache.Current ?? cache.CreateInstance();
    foreach (AUScheduleFilter auScheduleFilter in array)
    {
      object returnValue = (object) auScheduleFilter.Value;
      cache.RaiseFieldSelecting(auScheduleFilter.FieldName, withFilterValues1, ref returnValue, false);
      returnValue = PXFieldState.UnwrapValue(returnValue);
      cache.SetValueExt(withFilterValues1, auScheduleFilter.FieldName, returnValue);
    }
    return withFilterValues1;
  }

  public virtual PXGraph CreateGraph(string graphName, bool secure = true)
  {
    string screenId = this.Schedule.Current.ScreenID;
    System.Type t = string.IsNullOrEmpty(graphName) ? typeof (PXGraph) : PXBuildManager.GetType(graphName, false);
    if (t == (System.Type) null)
      t = System.Type.GetType(graphName);
    if (!(t != (System.Type) null))
      return (PXGraph) null;
    System.Type type = PXBuildManager.GetType(CustomizedTypeManager.GetCustomizedTypeFullName(t), false);
    if ((object) type == null)
      type = t;
    System.Type graphType = type;
    using (secure ? new PXPreserveScope() : (PXPreserveScope) null)
    {
      try
      {
        return !(graphType == typeof (PXGenericInqGrph)) || string.IsNullOrEmpty(screenId) ? PXGraph.CreateInstance(graphType) : (PXGraph) PXGenericInqGrph.CreateInstance(screenId);
      }
      catch (TargetInvocationException ex)
      {
        throw PXException.ExtractInner((Exception) ex);
      }
    }
  }

  private void SetControlsState(PXCache cache, AUSchedule schedule)
  {
    bool flag1 = schedule.ScheduleType == "M";
    bool flag2 = schedule.ScheduleType == "P";
    bool flag3 = schedule.ScheduleType == "W";
    bool flag4 = schedule.ScheduleType == "D";
    bool isEnabled = !schedule.LastRunDate.HasValue;
    PXUIFieldAttribute.SetEnabled<AUSchedule.startDate>(cache, (object) schedule, isEnabled);
    PXUIFieldAttribute.SetEnabled<AUSchedule.monthlyFrequency>(cache, (object) schedule, flag1);
    PXUIFieldAttribute.SetVisible<AUSchedule.monthlyFrequency>(cache, (object) schedule, flag1);
    PXUIFieldAttribute.SetVisible<AUSchedule.monthlyLabel>(cache, (object) schedule, flag1);
    PXUIFieldAttribute.SetEnabled<AUSchedule.monthlyDaySel>(cache, (object) schedule, flag1);
    PXUIFieldAttribute.SetVisible<AUSchedule.monthlyDaySel>(cache, (object) schedule, flag1);
    PXUIFieldAttribute.SetEnabled<AUSchedule.monthlyOnDay>(cache, (object) schedule, flag1 && schedule.MonthlyDaySel == "D");
    PXUIFieldAttribute.SetVisible<AUSchedule.monthlyOnDay>(cache, (object) schedule, flag1 && schedule.MonthlyDaySel == "D");
    PXUIFieldAttribute.SetEnabled<AUSchedule.monthlyOnWeek>(cache, (object) schedule, flag1 && schedule.MonthlyDaySel == "W");
    PXUIFieldAttribute.SetVisible<AUSchedule.monthlyOnWeek>(cache, (object) schedule, flag1 && schedule.MonthlyDaySel == "W");
    PXUIFieldAttribute.SetEnabled<AUSchedule.monthlyOnDayOfWeek>(cache, (object) schedule, flag1 && schedule.MonthlyDaySel == "W");
    PXUIFieldAttribute.SetVisible<AUSchedule.monthlyOnDayOfWeek>(cache, (object) schedule, flag1 && schedule.MonthlyDaySel == "W");
    PXUIFieldAttribute.SetEnabled<AUSchedule.periodFrequency>(cache, (object) schedule, flag2);
    PXUIFieldAttribute.SetVisible<AUSchedule.periodFrequency>(cache, (object) schedule, flag2);
    PXUIFieldAttribute.SetVisible<AUSchedule.periodLabel>(cache, (object) schedule, flag2);
    PXUIFieldAttribute.SetEnabled<AUSchedule.periodDateSel>(cache, (object) schedule, flag2);
    PXUIFieldAttribute.SetVisible<AUSchedule.periodDateSel>(cache, (object) schedule, flag2);
    PXUIFieldAttribute.SetEnabled<AUSchedule.periodFixedDay>(cache, (object) schedule, flag2 && schedule.PeriodDateSel == "D");
    PXUIFieldAttribute.SetVisible<AUSchedule.periodFixedDay>(cache, (object) schedule, flag2 && schedule.PeriodDateSel == "D");
    PXUIFieldAttribute.SetEnabled<AUSchedule.weeklyFrequency>(cache, (object) schedule, flag3);
    PXUIFieldAttribute.SetVisible<AUSchedule.weeklyFrequency>(cache, (object) schedule, flag3);
    PXUIFieldAttribute.SetVisible<AUSchedule.weeklyLabel>(cache, (object) schedule, flag3);
    PXUIFieldAttribute.SetEnabled<AUSchedule.weeklyOnDay1>(cache, (object) schedule, flag3);
    PXUIFieldAttribute.SetEnabled<AUSchedule.weeklyOnDay2>(cache, (object) schedule, flag3);
    PXUIFieldAttribute.SetEnabled<AUSchedule.weeklyOnDay3>(cache, (object) schedule, flag3);
    PXUIFieldAttribute.SetEnabled<AUSchedule.weeklyOnDay4>(cache, (object) schedule, flag3);
    PXUIFieldAttribute.SetEnabled<AUSchedule.weeklyOnDay5>(cache, (object) schedule, flag3);
    PXUIFieldAttribute.SetEnabled<AUSchedule.weeklyOnDay6>(cache, (object) schedule, flag3);
    PXUIFieldAttribute.SetEnabled<AUSchedule.weeklyOnDay7>(cache, (object) schedule, flag3);
    PXUIFieldAttribute.SetVisible<AUSchedule.weeklyOnDay1>(cache, (object) schedule, flag3);
    PXUIFieldAttribute.SetVisible<AUSchedule.weeklyOnDay2>(cache, (object) schedule, flag3);
    PXUIFieldAttribute.SetVisible<AUSchedule.weeklyOnDay3>(cache, (object) schedule, flag3);
    PXUIFieldAttribute.SetVisible<AUSchedule.weeklyOnDay4>(cache, (object) schedule, flag3);
    PXUIFieldAttribute.SetVisible<AUSchedule.weeklyOnDay5>(cache, (object) schedule, flag3);
    PXUIFieldAttribute.SetVisible<AUSchedule.weeklyOnDay6>(cache, (object) schedule, flag3);
    PXUIFieldAttribute.SetVisible<AUSchedule.weeklyOnDay7>(cache, (object) schedule, flag3);
    PXUIFieldAttribute.SetEnabled<AUSchedule.dailyFrequency>(cache, (object) schedule, flag4);
    PXUIFieldAttribute.SetVisible<AUSchedule.dailyFrequency>(cache, (object) schedule, flag4);
    PXUIFieldAttribute.SetVisible<AUSchedule.dailyLabel>(cache, (object) schedule, flag4);
    PXCache cache1 = cache;
    AUSchedule data1 = schedule;
    bool? noEndDate = schedule.NoEndDate;
    bool flag5 = true;
    int num1 = !(noEndDate.GetValueOrDefault() == flag5 & noEndDate.HasValue) ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<AUSchedule.endDate>(cache1, (object) data1, num1 != 0);
    PXCache cache2 = cache;
    AUSchedule data2 = schedule;
    bool? noRunLimit = schedule.NoRunLimit;
    bool flag6 = true;
    int num2 = !(noRunLimit.GetValueOrDefault() == flag6 & noRunLimit.HasValue) ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<AUSchedule.runLimit>(cache2, (object) data2, num2 != 0);
    PXCache cache3 = cache;
    AUSchedule data3 = schedule;
    bool? keepFullHistory = schedule.KeepFullHistory;
    bool flag7 = true;
    int num3 = !(keepFullHistory.GetValueOrDefault() == flag7 & keepFullHistory.HasValue) ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<AUSchedule.historyRetainCount>(cache3, (object) data3, num3 != 0);
    PXCache cache4 = cache;
    AUSchedule data4 = schedule;
    bool? nullable = schedule.NoEndDate;
    bool flag8 = true;
    int check1 = nullable.GetValueOrDefault() == flag8 & nullable.HasValue ? 2 : 0;
    PXDefaultAttribute.SetPersistingCheck<AUSchedule.endDate>(cache4, (object) data4, (PXPersistingCheck) check1);
    PXCache cache5 = cache;
    AUSchedule data5 = schedule;
    nullable = schedule.NoRunLimit;
    bool flag9 = true;
    int check2 = nullable.GetValueOrDefault() == flag9 & nullable.HasValue ? 2 : 0;
    PXDefaultAttribute.SetPersistingCheck<AUSchedule.runLimit>(cache5, (object) data5, (PXPersistingCheck) check2);
    PXCache cache6 = cache;
    AUSchedule data6 = schedule;
    nullable = schedule.KeepFullHistory;
    bool flag10 = true;
    int check3 = nullable.GetValueOrDefault() == flag10 & nullable.HasValue ? 2 : 0;
    PXDefaultAttribute.SetPersistingCheck<AUSchedule.historyRetainCount>(cache6, (object) data6, (PXPersistingCheck) check3);
    PXCache cache7 = cache;
    AUSchedule data7 = schedule;
    nullable = schedule.DoNotDeactivate;
    bool flag11 = true;
    int num4 = !(nullable.GetValueOrDefault() == flag11 & nullable.HasValue) ? 1 : 0;
    PXUIFieldAttribute.SetEnabled<AUSchedule.maxAbortCount>(cache7, (object) data7, num4 != 0);
  }

  private void SetDataScreen(string screenId)
  {
    this._dataScreen = string.IsNullOrEmpty(screenId) ? (DataScreenBase) null : this.DataScreenFactory.CreateDataScreen(screenId);
    ((PXDelegationViewCollection) this.Views).DelegateTo = this._dataScreen?.DataGraph;
  }

  protected override PXViewCollection CreateViewCollection()
  {
    return (PXViewCollection) new PXDelegationViewCollection((PXGraph) this);
  }
}
