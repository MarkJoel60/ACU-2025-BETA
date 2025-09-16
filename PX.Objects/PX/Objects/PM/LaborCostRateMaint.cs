// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.LaborCostRateMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.Api.Export;
using PX.Data.BQL;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.CT;
using PX.Objects.EP;
using PX.Objects.IN;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

[NonOptimizable(IgnoreOptimizationBehavior = true)]
public class LaborCostRateMaint : 
  PXGraph<
  #nullable disable
  LaborCostRateMaint>,
  PXImportAttribute.IPXPrepareItems,
  IPXAuditSource
{
  public PXFilter<LaborCostRateMaint.PMLaborCostRateFilter> Filter;
  public PXCancel<LaborCostRateMaint.PMLaborCostRateFilter> Cancel;
  public PXSave<LaborCostRateMaint.PMLaborCostRateFilter> Save;
  [PXImport(typeof (LaborCostRateMaint.PMLaborCostRateFilter))]
  public PXSelectJoin<PMLaborCostRate, LeftJoin<PMProject, On<PMProject.contractID, Equal<PMLaborCostRate.projectID>>>, Where2<Where<PMLaborCostRate.type, Equal<Current<LaborCostRateMaint.PMLaborCostRateFilter.type>>, Or<Current<LaborCostRateMaint.PMLaborCostRateFilter.type>, Equal<PMLaborCostRateType.all>>>, And2<Where<PMLaborCostRate.unionID, Equal<Current<LaborCostRateMaint.PMLaborCostRateFilter.unionID>>, Or<Current<LaborCostRateMaint.PMLaborCostRateFilter.unionID>, IsNull>>, And2<Where<PMLaborCostRate.projectID, Equal<Current<LaborCostRateMaint.PMLaborCostRateFilter.projectID>>, Or<Current<LaborCostRateMaint.PMLaborCostRateFilter.projectID>, IsNull>>, And2<Where<PMLaborCostRate.taskID, Equal<Current<LaborCostRateMaint.PMLaborCostRateFilter.taskID>>, Or<Current<LaborCostRateMaint.PMLaborCostRateFilter.taskID>, IsNull>>, And2<Where<PMLaborCostRate.employeeID, Equal<Current<LaborCostRateMaint.PMLaborCostRateFilter.employeeID>>, Or<Current<LaborCostRateMaint.PMLaborCostRateFilter.employeeID>, IsNull>>, And2<Where<PMLaborCostRate.inventoryID, Equal<Current<LaborCostRateMaint.PMLaborCostRateFilter.inventoryID>>, Or<Current<LaborCostRateMaint.PMLaborCostRateFilter.inventoryID>, IsNull>>, And2<Where<PMLaborCostRate.effectiveDate, Equal<Current<LaborCostRateMaint.PMLaborCostRateFilter.effectiveDate>>, Or<Current<LaborCostRateMaint.PMLaborCostRateFilter.effectiveDate>, IsNull>>, And<Where<PMLaborCostRate.projectID, IsNull, Or<MatchUserFor<PMProject>>>>>>>>>>>> Items;

  string IPXAuditSource.GetMainView() => "Items";

  IEnumerable<System.Type> IPXAuditSource.GetAuditedTables()
  {
    yield return typeof (PMLaborCostRate);
  }

  protected virtual void _(PX.Data.Events.RowSelected<PMLaborCostRate> e)
  {
    if (e.Row == null)
      return;
    PXUIFieldAttribute.SetEnabled<PMLaborCostRate.unionID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMLaborCostRate>>) e).Cache, (object) e.Row, e.Row.Type == "U");
    PXUIFieldAttribute.SetEnabled<PMLaborCostRate.projectID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMLaborCostRate>>) e).Cache, (object) e.Row, e.Row.Type == "C" || e.Row.Type == "P");
    PXUIFieldAttribute.SetEnabled<PMLaborCostRate.employeeID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMLaborCostRate>>) e).Cache, (object) e.Row, e.Row.Type == "E" || e.Row.Type == "P");
    PXUIFieldAttribute.SetEnabled<PMLaborCostRate.employmentType>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMLaborCostRate>>) e).Cache, (object) e.Row, e.Row.Type == "E");
    PXUIFieldAttribute.SetEnabled<PMLaborCostRate.regularHours>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMLaborCostRate>>) e).Cache, (object) e.Row, e.Row.Type == "E");
    PXUIFieldAttribute.SetEnabled<PMLaborCostRate.annualSalary>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMLaborCostRate>>) e).Cache, (object) e.Row, e.Row.EmploymentType != "H");
    PXUIFieldAttribute.SetEnabled<PMLaborCostRate.wageRate>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMLaborCostRate>>) e).Cache, (object) e.Row, e.Row.EmploymentType == "H");
    PXUIFieldAttribute.SetEnabled<PMLaborCostRate.rate>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMLaborCostRate>>) e).Cache, (object) e.Row, e.Row.EmploymentType == "H" || PXAccess.FeatureInstalled<FeaturesSet.payrollModule>());
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMLaborCostRate, PMLaborCostRate.type> e)
  {
    if (((PXSelectBase<LaborCostRateMaint.PMLaborCostRateFilter>) this.Filter).Current != null && ((PXSelectBase<LaborCostRateMaint.PMLaborCostRateFilter>) this.Filter).Current.Type != "A")
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMLaborCostRate, PMLaborCostRate.type>, PMLaborCostRate, object>) e).NewValue = (object) ((PXSelectBase<LaborCostRateMaint.PMLaborCostRateFilter>) this.Filter).Current.Type;
    else
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMLaborCostRate, PMLaborCostRate.type>, PMLaborCostRate, object>) e).NewValue = (object) "E";
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMLaborCostRate, PMLaborCostRate.type> e)
  {
    if (e.Row.Type == "E")
    {
      e.Row.UnionID = (string) null;
      e.Row.ProjectID = new int?();
      e.Row.TaskID = new int?();
    }
    else if (e.Row.Type == "P")
    {
      e.Row.UnionID = (string) null;
      e.Row.RegularHours = new Decimal?();
      e.Row.AnnualSalary = new Decimal?();
      e.Row.EmploymentType = "H";
    }
    else if (e.Row.Type == "I")
    {
      e.Row.ProjectID = new int?();
      e.Row.TaskID = new int?();
      e.Row.UnionID = (string) null;
      e.Row.EmployeeID = new int?();
      e.Row.RegularHours = new Decimal?();
      e.Row.AnnualSalary = new Decimal?();
      e.Row.EmploymentType = "H";
    }
    else if (e.Row.Type == "U")
    {
      e.Row.ProjectID = new int?();
      e.Row.TaskID = new int?();
      e.Row.EmployeeID = new int?();
      e.Row.RegularHours = new Decimal?();
      e.Row.AnnualSalary = new Decimal?();
      e.Row.EmploymentType = "H";
    }
    else
    {
      if (!(e.Row.Type == "C"))
        return;
      e.Row.UnionID = (string) null;
      e.Row.TaskID = new int?();
      e.Row.EmployeeID = new int?();
      e.Row.RegularHours = new Decimal?();
      e.Row.AnnualSalary = new Decimal?();
      e.Row.EmploymentType = "H";
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMLaborCostRate, PMLaborCostRate.unionID> e)
  {
    if (((PXSelectBase<LaborCostRateMaint.PMLaborCostRateFilter>) this.Filter).Current == null || ((PXSelectBase<LaborCostRateMaint.PMLaborCostRateFilter>) this.Filter).Current.UnionID == null)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMLaborCostRate, PMLaborCostRate.unionID>, PMLaborCostRate, object>) e).NewValue = (object) ((PXSelectBase<LaborCostRateMaint.PMLaborCostRateFilter>) this.Filter).Current.UnionID;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMLaborCostRate, PMLaborCostRate.projectID> e)
  {
    if (((PXSelectBase<LaborCostRateMaint.PMLaborCostRateFilter>) this.Filter).Current == null || !((PXSelectBase<LaborCostRateMaint.PMLaborCostRateFilter>) this.Filter).Current.ProjectID.HasValue)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMLaborCostRate, PMLaborCostRate.projectID>, PMLaborCostRate, object>) e).NewValue = (object) ((PXSelectBase<LaborCostRateMaint.PMLaborCostRateFilter>) this.Filter).Current.ProjectID;
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PMLaborCostRate, PMLaborCostRate.effectiveDate> e)
  {
    PMLaborCostRate latestDuplicate = this.GetLatestDuplicate(e.Row);
    DateTime? newValue = (DateTime?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMLaborCostRate, PMLaborCostRate.effectiveDate>, PMLaborCostRate, object>) e).NewValue;
    if (latestDuplicate != null && latestDuplicate.EffectiveDate.HasValue)
    {
      if (newValue.HasValue && newValue.Value.Date <= latestDuplicate.EffectiveDate.Value.Date)
        throw new PXSetPropertyException("The effective date should be greater than {0}.", new object[1]
        {
          (object) latestDuplicate.EffectiveDate
        });
    }
    else
    {
      DateTime? minEffectiveDate = this.GetMinEffectiveDate(e.Row);
      if (!newValue.HasValue || !minEffectiveDate.HasValue)
        return;
      DateTime date1 = newValue.Value.Date;
      DateTime dateTime = minEffectiveDate.Value;
      DateTime date2 = dateTime.Date;
      if (date1 > date2)
      {
        object[] objArray = new object[1];
        dateTime = minEffectiveDate.Value;
        objArray[0] = (object) dateTime.AddDays(1.0);
        throw new PXSetPropertyException("The effective date should be less than {0}.", objArray);
      }
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMLaborCostRate, PMLaborCostRate.taskID> e)
  {
    if (((PXSelectBase<LaborCostRateMaint.PMLaborCostRateFilter>) this.Filter).Current == null || !((PXSelectBase<LaborCostRateMaint.PMLaborCostRateFilter>) this.Filter).Current.TaskID.HasValue)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMLaborCostRate, PMLaborCostRate.taskID>, PMLaborCostRate, object>) e).NewValue = (object) ((PXSelectBase<LaborCostRateMaint.PMLaborCostRateFilter>) this.Filter).Current.TaskID;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMLaborCostRate, PMLaborCostRate.employeeID> e)
  {
    if (((PXSelectBase<LaborCostRateMaint.PMLaborCostRateFilter>) this.Filter).Current == null || !((PXSelectBase<LaborCostRateMaint.PMLaborCostRateFilter>) this.Filter).Current.EmployeeID.HasValue)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMLaborCostRate, PMLaborCostRate.employeeID>, PMLaborCostRate, object>) e).NewValue = (object) ((PXSelectBase<LaborCostRateMaint.PMLaborCostRateFilter>) this.Filter).Current.EmployeeID;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMLaborCostRate, PMLaborCostRate.employeeID> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMLaborCostRate, PMLaborCostRate.employeeID>>) e).Cache.SetDefaultExt<PMLaborCostRate.inventoryID>((object) e.Row);
    if (!e.Row.EffectiveDate.HasValue)
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMLaborCostRate, PMLaborCostRate.employeeID>>) e).Cache.SetDefaultExt<PMLaborCostRate.effectiveDate>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMLaborCostRate, PMLaborCostRate.employeeID>>) e).Cache.SetDefaultExt<PMLaborCostRate.regularHours>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMLaborCostRate, PMLaborCostRate.inventoryID> e)
  {
    int? nullable;
    if (((PXSelectBase<LaborCostRateMaint.PMLaborCostRateFilter>) this.Filter).Current != null)
    {
      nullable = ((PXSelectBase<LaborCostRateMaint.PMLaborCostRateFilter>) this.Filter).Current.InventoryID;
      if (nullable.HasValue)
      {
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMLaborCostRate, PMLaborCostRate.inventoryID>, PMLaborCostRate, object>) e).NewValue = (object) ((PXSelectBase<LaborCostRateMaint.PMLaborCostRateFilter>) this.Filter).Current.InventoryID;
        return;
      }
    }
    PMLaborCostRate row = e.Row;
    int num;
    if (row == null)
    {
      num = 0;
    }
    else
    {
      nullable = row.EmployeeID;
      num = nullable.HasValue ? 1 : 0;
    }
    if (num == 0)
      return;
    EPEmployee epEmployee = PXResultset<EPEmployee>.op_Implicit(PXSelectBase<EPEmployee, PXSelect<EPEmployee, Where<EPEmployee.bAccountID, Equal<Required<EPEmployee.bAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) e.Row.EmployeeID
    }));
    if (epEmployee == null)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMLaborCostRate, PMLaborCostRate.inventoryID>, PMLaborCostRate, object>) e).NewValue = (object) epEmployee.LabourItemID;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMLaborCostRate, PMLaborCostRate.inventoryID> e)
  {
    if (string.IsNullOrEmpty(e.Row.Description))
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMLaborCostRate, PMLaborCostRate.inventoryID>>) e).Cache.SetDefaultExt<PMLaborCostRate.description>((object) e.Row);
    if (e.Row.EffectiveDate.HasValue)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMLaborCostRate, PMLaborCostRate.inventoryID>>) e).Cache.SetDefaultExt<PMLaborCostRate.effectiveDate>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldSelecting<PMLaborCostRate, PMLaborCostRate.uOM> e)
  {
    if (e.Row == null)
      return;
    PX.Objects.IN.InventoryItem inventoryItem = PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(((PXSelectBase<PX.Objects.IN.InventoryItem>) new PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<PX.Objects.IN.InventoryItem.inventoryID>>>>((PXGraph) this)).Select(new object[1]
    {
      (object) e.Row.InventoryID
    }));
    if (inventoryItem != null)
      ((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<PMLaborCostRate, PMLaborCostRate.uOM>>) e).ReturnValue = (object) inventoryItem.BaseUnit;
    else
      ((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<PMLaborCostRate, PMLaborCostRate.uOM>>) e).ReturnValue = (object) "HOUR";
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMLaborCostRate, PMLaborCostRate.description> e)
  {
    if (e.Row == null || !e.Row.InventoryID.HasValue)
      return;
    PX.Objects.IN.InventoryItem inventoryItem = PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(((PXSelectBase<PX.Objects.IN.InventoryItem>) new PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<PX.Objects.IN.InventoryItem.inventoryID>>>>((PXGraph) this)).Select(new object[1]
    {
      (object) e.Row.InventoryID
    }));
    if (inventoryItem == null)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMLaborCostRate, PMLaborCostRate.description>, PMLaborCostRate, object>) e).NewValue = (object) inventoryItem.Descr;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMLaborCostRate, PMLaborCostRate.employmentType> e)
  {
    if (e.Row.EmploymentType == "H")
      e.Row.AnnualSalary = new Decimal?();
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMLaborCostRate, PMLaborCostRate.employmentType>>) e).Cache.SetDefaultExt<PMLaborCostRate.regularHours>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMLaborCostRate, PMLaborCostRate.regularHours> e)
  {
    if (e.Row == null || !e.Row.EmployeeID.HasValue || !(e.Row.Type == "E"))
      return;
    EmployeeCostEngine employeeCostEngine = new EmployeeCostEngine((PXGraph) this);
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMLaborCostRate, PMLaborCostRate.regularHours>, PMLaborCostRate, object>) e).NewValue = (object) employeeCostEngine.GetEmployeeHoursFromCalendar(e.Row.EmployeeID);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMLaborCostRate, PMLaborCostRate.regularHours> e)
  {
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMLaborCostRate, PMLaborCostRate.annualSalary> e)
  {
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMLaborCostRate, PMLaborCostRate.effectiveDate> e)
  {
    if (e.Row == null)
      return;
    PMLaborCostRate latestDuplicate = this.GetLatestDuplicate(e.Row);
    if (latestDuplicate != null)
    {
      DateTime? nullable = latestDuplicate.EffectiveDate;
      if (nullable.HasValue)
      {
        DateTime date = latestDuplicate.EffectiveDate.Value.Date;
        nullable = ((PXGraph) this).Accessinfo.BusinessDate;
        if ((nullable.HasValue ? (date < nullable.GetValueOrDefault() ? 1 : 0) : 0) == 0)
          return;
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMLaborCostRate, PMLaborCostRate.effectiveDate>, PMLaborCostRate, object>) e).NewValue = (object) ((PXGraph) this).Accessinfo.BusinessDate;
        return;
      }
    }
    if (this.AllParametersEntered(e.Row))
    {
      DateTime? minEffectiveDate = this.GetMinEffectiveDate(e.Row);
      if (minEffectiveDate.HasValue)
      {
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMLaborCostRate, PMLaborCostRate.effectiveDate>, PMLaborCostRate, object>) e).NewValue = (object) minEffectiveDate;
      }
      else
      {
        DateTime? employeeStartDate = this.GetProjectOrEmployeeStartDate(e.Row);
        if (employeeStartDate.HasValue)
          ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMLaborCostRate, PMLaborCostRate.effectiveDate>, PMLaborCostRate, object>) e).NewValue = (object) employeeStartDate;
        else
          ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMLaborCostRate, PMLaborCostRate.effectiveDate>, PMLaborCostRate, object>) e).NewValue = (object) ((PXGraph) this).Accessinfo.BusinessDate;
      }
    }
    else
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMLaborCostRate, PMLaborCostRate.effectiveDate>, PMLaborCostRate, object>) e).NewValue = (object) ((PXGraph) this).Accessinfo.BusinessDate;
  }

  protected virtual void _(
    PX.Data.Events.FieldSelecting<PMLaborCostRate, PMLaborCostRate.taskID> e)
  {
    if (e.Row != null && e.Row.Type == "C")
    {
      ((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<PMLaborCostRate, PMLaborCostRate.taskID>>) e).ReturnState = (object) PXStringState.CreateInstance((object) null, new int?(), new bool?(), "TaskID", new bool?(false), new int?(), (string) null, (string[]) null, (string[]) null, new bool?(), (string) null, (string[]) null);
      PXFieldState returnState = ((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<PMLaborCostRate, PMLaborCostRate.taskID>>) e).ReturnState as PXFieldState;
      returnState.Enabled = false;
      returnState.Visible = PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>();
      returnState.Visibility = PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>() ? (PXUIVisibility) 3 : (PXUIVisibility) 1;
      ((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<PMLaborCostRate, PMLaborCostRate.taskID>>) e).Cancel = true;
    }
    else
    {
      if (e.Row != null)
        return;
      ((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<PMLaborCostRate, PMLaborCostRate.taskID>>) e).ReturnState = (object) PXStringState.CreateInstance((object) null, new int?(), new bool?(), "TaskID", new bool?(false), new int?(), (string) null, (string[]) null, (string[]) null, new bool?(), (string) null, (string[]) null);
      PXFieldState returnState = ((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<PMLaborCostRate, PMLaborCostRate.taskID>>) e).ReturnState as PXFieldState;
      returnState.Enabled = true;
      returnState.Visible = PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>();
      returnState.Visibility = PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>() ? (PXUIVisibility) 3 : (PXUIVisibility) 1;
      returnState.DisplayName = PXUIFieldAttribute.GetDisplayName<PMLaborCostRate.taskID>(((PX.Data.Events.Event<PXFieldSelectingEventArgs, PX.Data.Events.FieldSelecting<PMLaborCostRate, PMLaborCostRate.taskID>>) e).Cache);
      ((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<PMLaborCostRate, PMLaborCostRate.taskID>>) e).Cancel = true;
    }
  }

  protected virtual void _(PX.Data.Events.RowUpdated<PMLaborCostRate> e)
  {
    DateTime? nullable1 = e.Row.EffectiveDate;
    DateTime? effectiveDate1 = e.OldRow.EffectiveDate;
    if ((nullable1.HasValue == effectiveDate1.HasValue ? (nullable1.HasValue ? (nullable1.GetValueOrDefault() == effectiveDate1.GetValueOrDefault() ? 1 : 0) : 1) : 0) == 0)
      return;
    PMLaborCostRate latestDuplicate = this.GetLatestDuplicate(e.Row);
    if (latestDuplicate == null)
      return;
    DateTime? effectiveDate2 = latestDuplicate.EffectiveDate;
    nullable1 = e.Row.EffectiveDate;
    if ((effectiveDate2.HasValue == nullable1.HasValue ? (effectiveDate2.HasValue ? (effectiveDate2.GetValueOrDefault() == nullable1.GetValueOrDefault() ? 1 : 0) : 1) : 0) == 0)
      return;
    PMLaborCostRate row = e.Row;
    nullable1 = new DateTime?();
    DateTime? nullable2 = nullable1;
    row.EffectiveDate = nullable2;
  }

  public virtual DateTime? GetMinEffectiveDate(PMLaborCostRate row)
  {
    if (row == null)
      return new DateTime?();
    if (!this.AllParametersEntered(row))
      return new DateTime?();
    DateTime? minEffectiveDate = new DateTime?();
    EPEmployee epEmployee = PXResultset<EPEmployee>.op_Implicit(PXSelectBase<EPEmployee, PXSelect<EPEmployee, Where<EPEmployee.bAccountID, Equal<Required<EPEmployee.bAccountID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.EmployeeID
    }));
    if (epEmployee != null)
    {
      PMTimeActivity pmTimeActivity = PXResultset<PMTimeActivity>.op_Implicit(PXSelectBase<PMTimeActivity, PXSelect<PMTimeActivity, Where<PMTimeActivity.ownerID, Equal<Required<PMTimeActivity.ownerID>>, And<PMTimeActivity.released, NotEqual<True>>>, OrderBy<Asc<PMTimeActivity.date>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
      {
        (object) epEmployee.OwnerID
      }));
      if (pmTimeActivity != null)
      {
        DateTime? date = pmTimeActivity.Date;
        DateTime? nullable = minEffectiveDate;
        if ((date.HasValue & nullable.HasValue ? (date.GetValueOrDefault() < nullable.GetValueOrDefault() ? 1 : 0) : 0) != 0)
          minEffectiveDate = pmTimeActivity.Date;
      }
    }
    return minEffectiveDate;
  }

  public virtual DateTime? GetProjectOrEmployeeStartDate(PMLaborCostRate row)
  {
    if (row == null)
      return new DateTime?();
    if (!this.AllParametersEntered(row))
      return new DateTime?();
    DateTime? nullable1 = new DateTime?();
    DateTime? nullable2 = new DateTime?();
    DateTime? nullable3 = new DateTime?();
    int? nullable4 = row.EmployeeID;
    if (nullable4.HasValue)
    {
      EPEmployeePosition employeePosition = PXResultset<EPEmployeePosition>.op_Implicit(PXSelectBase<EPEmployeePosition, PXSelect<EPEmployeePosition, Where<EPEmployeePosition.employeeID, Equal<Required<EPEmployeePosition.employeeID>>, And<EPEmployeePosition.isActive, Equal<True>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) row.EmployeeID
      }));
      if (employeePosition != null)
        nullable2 = employeePosition.StartDate;
    }
    nullable4 = row.ProjectID;
    if (nullable4.HasValue)
    {
      PMProject pmProject = PMProject.PK.Find((PXGraph) this, row.ProjectID);
      if (pmProject != null)
        nullable3 = pmProject.StartDate;
    }
    DateTime? employeeStartDate;
    if (nullable2.HasValue && nullable3.HasValue)
    {
      DateTime? nullable5 = nullable2;
      DateTime? nullable6 = nullable3;
      employeeStartDate = (nullable5.HasValue & nullable6.HasValue ? (nullable5.GetValueOrDefault() > nullable6.GetValueOrDefault() ? 1 : 0) : 0) == 0 ? nullable3 : nullable2;
    }
    else
      employeeStartDate = nullable2 ?? nullable3;
    return employeeStartDate;
  }

  public virtual bool AllParametersEntered(PMLaborCostRate row)
  {
    if (row.Type == "E")
      return row.EmployeeID.HasValue;
    if (row.Type == "U")
      return row.UnionID != null && row.InventoryID.HasValue;
    if (row.Type == "C")
      return row.ProjectID.HasValue && row.InventoryID.HasValue;
    if (row.Type == "P")
      return row.ProjectID.HasValue;
    return row.Type == "I" && row.InventoryID.HasValue;
  }

  public virtual PMLaborCostRate GetLatestDuplicate(PMLaborCostRate row)
  {
    if (row == null)
      return (PMLaborCostRate) null;
    PMLaborCostRate latestDuplicate = (PMLaborCostRate) null;
    if (row.Type == "E")
    {
      if (row.InventoryID.HasValue)
        latestDuplicate = PXResultset<PMLaborCostRate>.op_Implicit(PXSelectBase<PMLaborCostRate, PXSelect<PMLaborCostRate, Where<PMLaborCostRate.employeeID, Equal<Required<PMLaborCostRate.employeeID>>, And<PMLaborCostRate.inventoryID, Equal<Required<PMLaborCostRate.inventoryID>>, And<PMLaborCostRate.type, Equal<Required<PMLaborCostRate.type>>, And<PMLaborCostRate.recordID, NotEqual<Required<PMLaborCostRate.recordID>>>>>>, OrderBy<Desc<PMLaborCostRate.effectiveDate>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[4]
        {
          (object) row.EmployeeID,
          (object) row.InventoryID,
          (object) row.Type,
          (object) row.RecordID
        }));
      else
        latestDuplicate = PXResultset<PMLaborCostRate>.op_Implicit(PXSelectBase<PMLaborCostRate, PXSelect<PMLaborCostRate, Where<PMLaborCostRate.employeeID, Equal<Required<PMLaborCostRate.employeeID>>, And<PMLaborCostRate.inventoryID, IsNull, And<PMLaborCostRate.type, Equal<Required<PMLaborCostRate.type>>, And<PMLaborCostRate.recordID, NotEqual<Required<PMLaborCostRate.recordID>>>>>>, OrderBy<Desc<PMLaborCostRate.effectiveDate>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[3]
        {
          (object) row.EmployeeID,
          (object) row.Type,
          (object) row.RecordID
        }));
    }
    else if (row.Type == "U")
      latestDuplicate = PXResultset<PMLaborCostRate>.op_Implicit(PXSelectBase<PMLaborCostRate, PXSelect<PMLaborCostRate, Where<PMLaborCostRate.unionID, Equal<Required<PMLaborCostRate.unionID>>, And<PMLaborCostRate.inventoryID, Equal<Required<PMLaborCostRate.inventoryID>>, And<PMLaborCostRate.type, Equal<Required<PMLaborCostRate.type>>, And<PMLaborCostRate.recordID, NotEqual<Required<PMLaborCostRate.recordID>>>>>>, OrderBy<Desc<PMLaborCostRate.effectiveDate>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[4]
      {
        (object) row.UnionID,
        (object) row.InventoryID,
        (object) row.Type,
        (object) row.RecordID
      }));
    else if (row.Type == "C")
    {
      List<object> objectList = new List<object>((IEnumerable<object>) new object[4]
      {
        (object) row.ProjectID,
        (object) row.InventoryID,
        (object) row.Type,
        (object) row.RecordID
      });
      PXSelectBase<PMLaborCostRate> pxSelectBase = (PXSelectBase<PMLaborCostRate>) new PXSelect<PMLaborCostRate, Where<PMLaborCostRate.projectID, Equal<Required<PMLaborCostRate.projectID>>, And<PMLaborCostRate.inventoryID, Equal<Required<PMLaborCostRate.inventoryID>>, And<PMLaborCostRate.type, Equal<Required<PMLaborCostRate.type>>, And<PMLaborCostRate.recordID, NotEqual<Required<PMLaborCostRate.recordID>>>>>>, OrderBy<Desc<PMLaborCostRate.effectiveDate>>>((PXGraph) this);
      if (row.TaskID.HasValue)
      {
        pxSelectBase.WhereAnd<Where<PMLaborCostRate.taskID, Equal<Required<PMLaborCostRate.taskID>>>>();
        objectList.Add((object) row.TaskID);
      }
      else
        pxSelectBase.WhereAnd<Where<PMLaborCostRate.taskID, IsNull>>();
      latestDuplicate = PXResultset<PMLaborCostRate>.op_Implicit(pxSelectBase.SelectWindowed(0, 1, objectList.ToArray()));
    }
    else if (row.Type == "P")
    {
      int? nullable = row.InventoryID;
      PXSelectBase<PMLaborCostRate> pxSelectBase;
      List<object> objectList;
      if (nullable.HasValue)
      {
        pxSelectBase = (PXSelectBase<PMLaborCostRate>) new PXSelect<PMLaborCostRate, Where<PMLaborCostRate.projectID, Equal<Required<PMLaborCostRate.projectID>>, And<PMLaborCostRate.inventoryID, Equal<Required<PMLaborCostRate.inventoryID>>, And<PMLaborCostRate.type, Equal<Required<PMLaborCostRate.type>>, And<PMLaborCostRate.recordID, NotEqual<Required<PMLaborCostRate.recordID>>>>>>, OrderBy<Desc<PMLaborCostRate.effectiveDate>>>((PXGraph) this);
        objectList = new List<object>((IEnumerable<object>) new object[4]
        {
          (object) row.ProjectID,
          (object) row.InventoryID,
          (object) row.Type,
          (object) row.RecordID
        });
      }
      else
      {
        pxSelectBase = (PXSelectBase<PMLaborCostRate>) new PXSelect<PMLaborCostRate, Where<PMLaborCostRate.projectID, Equal<Required<PMLaborCostRate.projectID>>, And<PMLaborCostRate.inventoryID, IsNull, And<PMLaborCostRate.type, Equal<Required<PMLaborCostRate.type>>, And<PMLaborCostRate.recordID, NotEqual<Required<PMLaborCostRate.recordID>>>>>>, OrderBy<Desc<PMLaborCostRate.effectiveDate>>>((PXGraph) this);
        objectList = new List<object>((IEnumerable<object>) new object[3]
        {
          (object) row.ProjectID,
          (object) row.Type,
          (object) row.RecordID
        });
      }
      nullable = row.TaskID;
      if (nullable.HasValue)
      {
        pxSelectBase.WhereAnd<Where<PMLaborCostRate.taskID, Equal<Required<PMLaborCostRate.taskID>>>>();
        objectList.Add((object) row.TaskID);
      }
      else
        pxSelectBase.WhereAnd<Where<PMLaborCostRate.taskID, IsNull>>();
      nullable = row.EmployeeID;
      if (nullable.HasValue)
      {
        pxSelectBase.WhereAnd<Where<PMLaborCostRate.employeeID, Equal<Required<PMLaborCostRate.employeeID>>>>();
        objectList.Add((object) row.EmployeeID);
      }
      else
        pxSelectBase.WhereAnd<Where<PMLaborCostRate.employeeID, IsNull>>();
      latestDuplicate = PXResultset<PMLaborCostRate>.op_Implicit(pxSelectBase.SelectWindowed(0, 1, objectList.ToArray()));
    }
    else if (row.Type == "I")
      latestDuplicate = PXResultset<PMLaborCostRate>.op_Implicit(PXSelectBase<PMLaborCostRate, PXSelect<PMLaborCostRate, Where<PMLaborCostRate.inventoryID, Equal<Required<PMLaborCostRate.inventoryID>>, And<PMLaborCostRate.type, Equal<Required<PMLaborCostRate.type>>, And<PMLaborCostRate.recordID, NotEqual<Required<PMLaborCostRate.recordID>>>>>, OrderBy<Desc<PMLaborCostRate.effectiveDate>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[3]
      {
        (object) row.InventoryID,
        (object) row.Type,
        (object) row.RecordID
      }));
    return latestDuplicate;
  }

  protected virtual void _(PX.Data.Events.RowPersisting<PMLaborCostRate> e)
  {
    if (e.Operation == 3)
      return;
    int? nullable;
    if (e.Row.Type == "C" || e.Row.Type == "P")
    {
      nullable = e.Row.ProjectID;
      if (!nullable.HasValue)
      {
        ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PMLaborCostRate>>) e).Cache.RaiseExceptionHandling<PMLaborCostRate.projectID>((object) e.Row, (object) null, (Exception) new PXSetPropertyException<PMLaborCostRate.projectID>("'{0}' cannot be empty.", new object[1]
        {
          (object) "[ProjectID]"
        }));
        throw new PXRowPersistingException("ProjectID", (object) null, "'{0}' cannot be empty.", new object[1]
        {
          (object) "ProjectID"
        });
      }
    }
    else if (e.Row.Type == "E")
    {
      nullable = e.Row.EmployeeID;
      if (!nullable.HasValue)
      {
        ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PMLaborCostRate>>) e).Cache.RaiseExceptionHandling<PMLaborCostRate.employeeID>((object) e.Row, (object) null, (Exception) new PXSetPropertyException<PMLaborCostRate.employeeID>("'{0}' cannot be empty.", new object[1]
        {
          (object) "[EmployeeID]"
        }));
        throw new PXRowPersistingException("EmployeeID", (object) null, "'{0}' cannot be empty.", new object[1]
        {
          (object) "EmployeeID"
        });
      }
      if (!e.Row.RegularHours.HasValue)
      {
        ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PMLaborCostRate>>) e).Cache.RaiseExceptionHandling<PMLaborCostRate.regularHours>((object) e.Row, (object) null, (Exception) new PXSetPropertyException<PMLaborCostRate.regularHours>("'{0}' cannot be empty.", new object[1]
        {
          (object) "[RegularHours]"
        }));
        throw new PXRowPersistingException("RegularHours", (object) null, "'{0}' cannot be empty.", new object[1]
        {
          (object) "EmployeeID"
        });
      }
    }
    else if (e.Row.Type == "U" && e.Row.UnionID == null)
    {
      ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PMLaborCostRate>>) e).Cache.RaiseExceptionHandling<PMLaborCostRate.unionID>((object) e.Row, (object) null, (Exception) new PXSetPropertyException<PMLaborCostRate.unionID>("'{0}' cannot be empty.", new object[1]
      {
        (object) "[UnionID]"
      }));
      throw new PXRowPersistingException("UnionID", (object) null, "'{0}' cannot be empty.", new object[1]
      {
        (object) "UnionID"
      });
    }
    nullable = e.Row.InventoryID;
    if (!nullable.HasValue && e.Row.Type != "E" && e.Row.Type != "P")
    {
      ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PMLaborCostRate>>) e).Cache.RaiseExceptionHandling<PMLaborCostRate.inventoryID>((object) e.Row, (object) null, (Exception) new PXSetPropertyException<PMLaborCostRate.inventoryID>("'{0}' cannot be empty.", new object[1]
      {
        (object) "[ProjectID]"
      }));
      throw new PXRowPersistingException("InventoryID", (object) null, "'{0}' cannot be empty.", new object[1]
      {
        (object) "InventoryID"
      });
    }
  }

  public virtual Decimal CalculateHourlyRate(Decimal? hours, Decimal? salary)
  {
    return LaborRateAttribute.CalculateHourlyRate(hours, salary);
  }

  public bool PrepareImportRow(string viewName, IDictionary keys, IDictionary values) => true;

  public bool RowImporting(string viewName, object row) => row == null;

  public bool RowImported(string viewName, object row, object oldRow) => oldRow == null;

  public void PrepareItems(string viewName, IEnumerable items)
  {
  }

  [PXHidden]
  [ExcludeFromCodeCoverage]
  [Serializable]
  public class PMLaborCostRateFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected int? _InventoryID;

    [PXDBString(1)]
    [PXDefault("A")]
    [PMLaborCostRateType.FilterList]
    [PXUIField(DisplayName = "Labor Rate Type")]
    public virtual string Type { get; set; }

    [PXRestrictor(typeof (Where<PMUnion.isActive, Equal<True>>), "The {0} union local is inactive.", new System.Type[] {typeof (PMUnion.unionID)})]
    [PXSelector(typeof (Search<PMUnion.unionID>), DescriptionField = typeof (PMUnion.description))]
    [PXDBString(15, IsUnicode = true)]
    [PXUIField(DisplayName = "Union Local", FieldClass = "Construction")]
    public virtual string UnionID { get; set; }

    [Project(typeof (Where<PMProject.baseType, Equal<CTPRType.project>, And<PMProject.nonProject, NotEqual<True>>>), WarnIfCompleted = false)]
    public virtual int? ProjectID { get; set; }

    [ProjectTask(typeof (LaborCostRateMaint.PMLaborCostRateFilter.projectID))]
    public virtual int? TaskID { get; set; }

    [PXEPEmployeeSelector]
    [PXDBInt]
    [PXUIField(DisplayName = "Employee")]
    public virtual int? EmployeeID { get; set; }

    [PXDBInt]
    [PXUIField(DisplayName = "Labor Item")]
    [PXDimensionSelector("INVENTORY", typeof (Search<PX.Objects.IN.InventoryItem.inventoryID, Where<PX.Objects.IN.InventoryItem.itemType, Equal<INItemTypes.laborItem>, And<Match<Current<AccessInfo.userName>>>>>), typeof (PX.Objects.IN.InventoryItem.inventoryCD), DescriptionField = typeof (PX.Objects.IN.InventoryItem.descr))]
    public virtual int? InventoryID
    {
      get => this._InventoryID;
      set => this._InventoryID = value;
    }

    [PXDBDate]
    [PXUIField(DisplayName = "Effective Date")]
    public virtual DateTime? EffectiveDate { get; set; }

    public abstract class type : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      LaborCostRateMaint.PMLaborCostRateFilter.type>
    {
    }

    public abstract class unionID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      LaborCostRateMaint.PMLaborCostRateFilter.unionID>
    {
    }

    public abstract class projectID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      LaborCostRateMaint.PMLaborCostRateFilter.projectID>
    {
    }

    public abstract class taskID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      LaborCostRateMaint.PMLaborCostRateFilter.taskID>
    {
    }

    public abstract class employeeID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      LaborCostRateMaint.PMLaborCostRateFilter.employeeID>
    {
    }

    public abstract class inventoryID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      LaborCostRateMaint.PMLaborCostRateFilter.inventoryID>
    {
    }

    public abstract class effectiveDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      LaborCostRateMaint.PMLaborCostRateFilter.effectiveDate>
    {
    }
  }
}
