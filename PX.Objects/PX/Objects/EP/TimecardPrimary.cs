// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.TimecardPrimary
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.TM;
using System;
using System.Collections;

#nullable enable
namespace PX.Objects.EP;

public class TimecardPrimary : PXGraph<
#nullable disable
TimecardPrimary>
{
  public PXFilter<TimecardPrimary.TimecardFilter> Filter;
  [PXFilterable(new System.Type[] {})]
  public PXSelectJoin<TimecardWithTotals, LeftJoin<EPTimeCard, On<TimecardWithTotals.timeCardCD, Equal<EPTimeCard.origTimeCardCD>>>, Where2<Where2<Where2<Where<TimecardWithTotals.defContactID, Equal<Current<AccessInfo.contactID>>, Or<TimecardWithTotals.createdByID, Equal<Current<AccessInfo.userID>>, Or<TimecardWithTotals.defContactID, IsSubordinateOfContact<Current<AccessInfo.contactID>>, Or<TimecardWithTotals.employeeID, WingmanUser<Current<AccessInfo.userID>, EPDelegationOf.timeEntries>>>>>, And<Current2<TimecardPrimary.TimecardFilter.employeeID>, PX.Data.IsNull>>, PX.Data.Or<Where<Current2<TimecardPrimary.TimecardFilter.employeeID>, PX.Data.IsNotNull, And<TimecardWithTotals.employeeID, Equal<Current2<TimecardPrimary.TimecardFilter.employeeID>>>>>>, And<EPTimeCard.timeCardCD, PX.Data.IsNull>>, OrderBy<Desc<TimecardWithTotals.timeCardCD>>> Items;
  public PXAction<TimecardPrimary.TimecardFilter> create;
  public PXAction<TimecardPrimary.TimecardFilter> update;
  public PXAction<TimecardPrimary.TimecardFilter> delete;

  [PXButton(SpecialType = PXSpecialButtonType.Insert, Tooltip = "Add New Timecard", ImageKey = "AddNew")]
  [PXUIField]
  [PXEntryScreenRights(typeof (EPTimeCard), "Insert")]
  protected virtual void Create()
  {
    using (new PXPreserveScope())
    {
      TimeCardMaint instance = (TimeCardMaint) PXGraph.CreateInstance(typeof (TimeCardMaint));
      instance.Clear(PXClearOption.ClearAll);
      instance.Document.Insert();
      if (this.Filter.Current.EmployeeID.HasValue)
      {
        int? employeeId1 = this.Filter.Current.EmployeeID;
        int? employeeId2 = instance.Document.Current.EmployeeID;
        if (!(employeeId1.GetValueOrDefault() == employeeId2.GetValueOrDefault() & employeeId1.HasValue == employeeId2.HasValue))
        {
          instance.Document.Current.EmployeeID = this.Filter.Current.EmployeeID;
          instance.Document.Update(instance.Document.Current);
        }
      }
      PXRedirectHelper.TryRedirect((PXGraph) instance, PXRedirectHelper.WindowMode.InlineWindow);
    }
  }

  [PXUIField]
  [PXButton(Tooltip = "Edit Timecard", ImageKey = "RecordEdit")]
  protected virtual void Update()
  {
    EPTimeCard row = (EPTimeCard) PXSelectBase<EPTimeCard, PXSelect<EPTimeCard, Where<EPTimeCard.timeCardCD, Equal<Current<TimecardWithTotals.timeCardCD>>>>.Config>.Select((PXGraph) this);
    if (row == null)
      return;
    PXRedirectHelper.TryRedirect((PXGraph) this, (object) row, PXRedirectHelper.WindowMode.InlineWindow);
  }

  [PXUIField]
  [PXDeleteButton(Tooltip = "Delete Timecard")]
  [PXEntryScreenRights(typeof (EPTimeCard))]
  protected void Delete()
  {
    if (this.Items.Current == null)
      return;
    using (new PXPreserveScope())
    {
      TimeCardMaint instance = (TimeCardMaint) PXGraph.CreateInstance(typeof (TimeCardMaint));
      instance.Clear(PXClearOption.ClearAll);
      instance.Document.Current = (EPTimeCard) instance.Document.Search<EPTimeCard.timeCardCD>((object) this.Items.Current.TimeCardCD);
      instance.Delete.Press();
    }
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
    if (viewName == "Items")
    {
      for (int index = 0; index < sortcolumns.Length; ++index)
      {
        if (string.Compare(sortcolumns[index], "WeekID_description", true) == 0)
          sortcolumns[index] = "WeekID";
      }
    }
    return base.ExecuteSelect(viewName, parameters, searches, sortcolumns, descendings, filters, ref startRow, maximumRows, ref totalRows);
  }

  protected virtual void TimecardFilter_EmployeeID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    EPEmployee epEmployee = (EPEmployee) PXSelectBase<EPEmployee, PXSelect<EPEmployee, Where<EPEmployee.userID, Equal<Current<AccessInfo.userID>>>>.Config>.Select((PXGraph) this);
    if (epEmployee == null)
      return;
    e.NewValue = (object) epEmployee.AcctCD;
  }

  [Serializable]
  public class TimecardFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    private int? _employeeId;

    [PXDBInt]
    [PXUIField(DisplayName = "Employee")]
    [PXSubordinateAndWingmenSelector(typeof (EPDelegationOf.timeEntries))]
    [PXFieldDescription]
    public virtual int? EmployeeID
    {
      get => this._employeeId;
      set => this._employeeId = value;
    }

    public abstract class employeeID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      TimecardPrimary.TimecardFilter.employeeID>
    {
    }
  }
}
