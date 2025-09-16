// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EmployeeCostEngine
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.PM;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.EP;

public class EmployeeCostEngine
{
  protected PXGraph graph;
  protected string defaultUOM = "HOUR";

  public EmployeeCostEngine(PXGraph graph)
  {
    this.graph = graph != null ? graph : throw new ArgumentNullException();
    EPSetup epSetup = PXResultset<EPSetup>.op_Implicit(PXSelectBase<EPSetup, PXSelect<EPSetup>.Config>.Select(graph, Array.Empty<object>()));
    if (epSetup == null || string.IsNullOrEmpty(epSetup.EmployeeRateUnit))
      return;
    this.defaultUOM = epSetup.EmployeeRateUnit;
  }

  public virtual EmployeeCostEngine.Rate GetEmployeeRate(
    int? laborItemID,
    int? projectID,
    int? projectTaskID,
    bool? certifiedJob,
    string unionID,
    int? employeeId,
    DateTime? date)
  {
    return this.GetEmployeeRate(laborItemID, projectID, projectTaskID, certifiedJob, unionID, employeeId, date, (string) null);
  }

  public virtual EmployeeCostEngine.Rate GetEmployeeRate(
    int? laborItemID,
    int? projectID,
    int? projectTaskID,
    bool? certifiedJob,
    string unionID,
    int? employeeId,
    DateTime? date,
    string curyID)
  {
    if (!date.HasValue)
      return (EmployeeCostEngine.Rate) null;
    PXSelect<PMLaborCostRate, Where2<Where<PMLaborCostRate.inventoryID, Equal<Required<PMLaborCostRate.inventoryID>>, Or<PMLaborCostRate.inventoryID, IsNull>>, And2<Where<PMLaborCostRate.employeeID, Equal<Required<PMLaborCostRate.employeeID>>, Or<PMLaborCostRate.employeeID, IsNull>>, And2<Where<PMLaborCostRate.projectID, Equal<Required<PMLaborCostRate.projectID>>, Or<PMLaborCostRate.projectID, IsNull>>, And<Where<PMLaborCostRate.taskID, Equal<Required<PMLaborCostRate.taskID>>, Or<PMLaborCostRate.taskID, IsNull>>>>>>, OrderBy<Asc<PMLaborCostRate.effectiveDate>>> pxSelect = new PXSelect<PMLaborCostRate, Where2<Where<PMLaborCostRate.inventoryID, Equal<Required<PMLaborCostRate.inventoryID>>, Or<PMLaborCostRate.inventoryID, IsNull>>, And2<Where<PMLaborCostRate.employeeID, Equal<Required<PMLaborCostRate.employeeID>>, Or<PMLaborCostRate.employeeID, IsNull>>, And2<Where<PMLaborCostRate.projectID, Equal<Required<PMLaborCostRate.projectID>>, Or<PMLaborCostRate.projectID, IsNull>>, And<Where<PMLaborCostRate.taskID, Equal<Required<PMLaborCostRate.taskID>>, Or<PMLaborCostRate.taskID, IsNull>>>>>>, OrderBy<Asc<PMLaborCostRate.effectiveDate>>>(this.graph);
    List<object> objectList = new List<object>()
    {
      (object) laborItemID,
      (object) employeeId,
      (object) projectID,
      (object) projectTaskID
    };
    if (curyID != null)
    {
      ((PXSelectBase<PMLaborCostRate>) pxSelect).WhereAnd<Where<PMLaborCostRate.curyID, Equal<Required<PMLaborCostRate.curyID>>>>();
      objectList.Add((object) curyID);
    }
    PXResultset<PMLaborCostRate> pxResultset = ((PXSelectBase<PMLaborCostRate>) pxSelect).Select(objectList.ToArray());
    List<PMLaborCostRate> pmLaborCostRateList = new List<PMLaborCostRate>();
    PMLaborCostRate pmLaborCostRate1 = (PMLaborCostRate) null;
    PMLaborCostRate pmLaborCostRate2 = (PMLaborCostRate) null;
    PMLaborCostRate pmLaborCostRate3 = (PMLaborCostRate) null;
    PMLaborCostRate pmLaborCostRate4 = (PMLaborCostRate) null;
    PMLaborCostRate pmLaborCostRate5 = (PMLaborCostRate) null;
    PMLaborCostRate pmLaborCostRate6 = (PMLaborCostRate) null;
    PMLaborCostRate pmLaborCostRate7 = (PMLaborCostRate) null;
    PMLaborCostRate pmLaborCostRate8 = (PMLaborCostRate) null;
    PMLaborCostRate pmLaborCostRate9 = (PMLaborCostRate) null;
    PMLaborCostRate pmLaborCostRate10 = (PMLaborCostRate) null;
    PMLaborCostRate pmLaborCostRate11 = (PMLaborCostRate) null;
    PMLaborCostRate pmLaborCostRate12 = (PMLaborCostRate) null;
    PMLaborCostRate pmLaborCostRate13 = (PMLaborCostRate) null;
    foreach (PXResult<PMLaborCostRate> pxResult in pxResultset)
    {
      PMLaborCostRate pmLaborCostRate14 = PXResult<PMLaborCostRate>.op_Implicit(pxResult);
      DateTime? effectiveDate = pmLaborCostRate14.EffectiveDate;
      DateTime? nullable1 = date;
      if ((effectiveDate.HasValue & nullable1.HasValue ? (effectiveDate.GetValueOrDefault() > nullable1.GetValueOrDefault() ? 1 : 0) : 0) == 0)
      {
        if (pmLaborCostRate14.Type == "C")
        {
          if (pmLaborCostRate1 == null)
          {
            pmLaborCostRate1 = pmLaborCostRate14;
          }
          else
          {
            nullable1 = pmLaborCostRate14.EffectiveDate;
            effectiveDate = pmLaborCostRate1.EffectiveDate;
            if ((nullable1.HasValue & effectiveDate.HasValue ? (nullable1.GetValueOrDefault() > effectiveDate.GetValueOrDefault() ? 1 : 0) : 0) != 0)
              pmLaborCostRate1 = pmLaborCostRate14;
          }
        }
        else if (pmLaborCostRate14.Type == "U" && !string.IsNullOrEmpty(unionID) && pmLaborCostRate14.UnionID == unionID)
        {
          if (pmLaborCostRate2 == null)
          {
            pmLaborCostRate2 = pmLaborCostRate14;
          }
          else
          {
            effectiveDate = pmLaborCostRate14.EffectiveDate;
            nullable1 = pmLaborCostRate2.EffectiveDate;
            if ((effectiveDate.HasValue & nullable1.HasValue ? (effectiveDate.GetValueOrDefault() > nullable1.GetValueOrDefault() ? 1 : 0) : 0) != 0)
              pmLaborCostRate2 = pmLaborCostRate14;
          }
        }
        else if (pmLaborCostRate14.Type == "I")
        {
          if (pmLaborCostRate13 == null)
          {
            pmLaborCostRate13 = pmLaborCostRate14;
          }
          else
          {
            nullable1 = pmLaborCostRate14.EffectiveDate;
            effectiveDate = pmLaborCostRate13.EffectiveDate;
            if ((nullable1.HasValue & effectiveDate.HasValue ? (nullable1.GetValueOrDefault() > effectiveDate.GetValueOrDefault() ? 1 : 0) : 0) != 0)
              pmLaborCostRate13 = pmLaborCostRate14;
          }
        }
        else if (pmLaborCostRate14.Type == "E")
        {
          if (pmLaborCostRate14.InventoryID.HasValue)
          {
            if (pmLaborCostRate11 == null)
            {
              pmLaborCostRate11 = pmLaborCostRate14;
            }
            else
            {
              effectiveDate = pmLaborCostRate14.EffectiveDate;
              nullable1 = pmLaborCostRate11.EffectiveDate;
              if ((effectiveDate.HasValue & nullable1.HasValue ? (effectiveDate.GetValueOrDefault() > nullable1.GetValueOrDefault() ? 1 : 0) : 0) != 0)
                pmLaborCostRate11 = pmLaborCostRate14;
            }
          }
          else if (pmLaborCostRate12 == null)
          {
            pmLaborCostRate12 = pmLaborCostRate14;
          }
          else
          {
            nullable1 = pmLaborCostRate14.EffectiveDate;
            effectiveDate = pmLaborCostRate12.EffectiveDate;
            if ((nullable1.HasValue & effectiveDate.HasValue ? (nullable1.GetValueOrDefault() > effectiveDate.GetValueOrDefault() ? 1 : 0) : 0) != 0)
              pmLaborCostRate12 = pmLaborCostRate14;
          }
        }
        else
        {
          int? projectId1 = pmLaborCostRate14.ProjectID;
          int? nullable2 = projectID;
          if (projectId1.GetValueOrDefault() == nullable2.GetValueOrDefault() & projectId1.HasValue == nullable2.HasValue)
          {
            int? taskId = pmLaborCostRate14.TaskID;
            int? nullable3 = projectTaskID;
            if (taskId.GetValueOrDefault() == nullable3.GetValueOrDefault() & taskId.HasValue == nullable3.HasValue)
            {
              int? employeeId1 = pmLaborCostRate14.EmployeeID;
              int? nullable4 = employeeId;
              if (employeeId1.GetValueOrDefault() == nullable4.GetValueOrDefault() & employeeId1.HasValue == nullable4.HasValue && pmLaborCostRate14.InventoryID.HasValue)
              {
                if (pmLaborCostRate3 == null)
                {
                  pmLaborCostRate3 = pmLaborCostRate14;
                  continue;
                }
                effectiveDate = pmLaborCostRate14.EffectiveDate;
                nullable1 = pmLaborCostRate3.EffectiveDate;
                if ((effectiveDate.HasValue & nullable1.HasValue ? (effectiveDate.GetValueOrDefault() > nullable1.GetValueOrDefault() ? 1 : 0) : 0) != 0)
                {
                  pmLaborCostRate3 = pmLaborCostRate14;
                  continue;
                }
                continue;
              }
            }
          }
          int? projectId2 = pmLaborCostRate14.ProjectID;
          int? nullable5 = projectID;
          if (projectId2.GetValueOrDefault() == nullable5.GetValueOrDefault() & projectId2.HasValue == nullable5.HasValue)
          {
            int? taskId = pmLaborCostRate14.TaskID;
            int? nullable6 = projectTaskID;
            if (taskId.GetValueOrDefault() == nullable6.GetValueOrDefault() & taskId.HasValue == nullable6.HasValue)
            {
              int? employeeId2 = pmLaborCostRate14.EmployeeID;
              int? nullable7 = employeeId;
              if (employeeId2.GetValueOrDefault() == nullable7.GetValueOrDefault() & employeeId2.HasValue == nullable7.HasValue && !pmLaborCostRate14.InventoryID.HasValue)
              {
                if (pmLaborCostRate4 == null)
                {
                  pmLaborCostRate4 = pmLaborCostRate14;
                  continue;
                }
                nullable1 = pmLaborCostRate14.EffectiveDate;
                effectiveDate = pmLaborCostRate4.EffectiveDate;
                if ((nullable1.HasValue & effectiveDate.HasValue ? (nullable1.GetValueOrDefault() > effectiveDate.GetValueOrDefault() ? 1 : 0) : 0) != 0)
                {
                  pmLaborCostRate4 = pmLaborCostRate14;
                  continue;
                }
                continue;
              }
            }
          }
          int? projectId3 = pmLaborCostRate14.ProjectID;
          int? nullable8 = projectID;
          if (projectId3.GetValueOrDefault() == nullable8.GetValueOrDefault() & projectId3.HasValue == nullable8.HasValue)
          {
            int? taskId = pmLaborCostRate14.TaskID;
            int? nullable9 = projectTaskID;
            if (taskId.GetValueOrDefault() == nullable9.GetValueOrDefault() & taskId.HasValue == nullable9.HasValue && !pmLaborCostRate14.EmployeeID.HasValue && pmLaborCostRate14.InventoryID.HasValue)
            {
              if (pmLaborCostRate5 == null)
              {
                pmLaborCostRate5 = pmLaborCostRate14;
                continue;
              }
              effectiveDate = pmLaborCostRate14.EffectiveDate;
              nullable1 = pmLaborCostRate5.EffectiveDate;
              if ((effectiveDate.HasValue & nullable1.HasValue ? (effectiveDate.GetValueOrDefault() > nullable1.GetValueOrDefault() ? 1 : 0) : 0) != 0)
              {
                pmLaborCostRate5 = pmLaborCostRate14;
                continue;
              }
              continue;
            }
          }
          int? projectId4 = pmLaborCostRate14.ProjectID;
          int? nullable10 = projectID;
          if (projectId4.GetValueOrDefault() == nullable10.GetValueOrDefault() & projectId4.HasValue == nullable10.HasValue)
          {
            int? taskId = pmLaborCostRate14.TaskID;
            int? nullable11 = projectTaskID;
            if (taskId.GetValueOrDefault() == nullable11.GetValueOrDefault() & taskId.HasValue == nullable11.HasValue && !pmLaborCostRate14.EmployeeID.HasValue && !pmLaborCostRate14.InventoryID.HasValue)
            {
              if (pmLaborCostRate6 == null)
              {
                pmLaborCostRate6 = pmLaborCostRate14;
                continue;
              }
              nullable1 = pmLaborCostRate14.EffectiveDate;
              effectiveDate = pmLaborCostRate6.EffectiveDate;
              if ((nullable1.HasValue & effectiveDate.HasValue ? (nullable1.GetValueOrDefault() > effectiveDate.GetValueOrDefault() ? 1 : 0) : 0) != 0)
              {
                pmLaborCostRate6 = pmLaborCostRate14;
                continue;
              }
              continue;
            }
          }
          int? projectId5 = pmLaborCostRate14.ProjectID;
          int? nullable12 = projectID;
          if (projectId5.GetValueOrDefault() == nullable12.GetValueOrDefault() & projectId5.HasValue == nullable12.HasValue)
          {
            int? employeeId3 = pmLaborCostRate14.EmployeeID;
            int? nullable13 = employeeId;
            if (employeeId3.GetValueOrDefault() == nullable13.GetValueOrDefault() & employeeId3.HasValue == nullable13.HasValue && pmLaborCostRate14.InventoryID.HasValue)
            {
              if (pmLaborCostRate7 == null)
              {
                pmLaborCostRate7 = pmLaborCostRate14;
                continue;
              }
              effectiveDate = pmLaborCostRate14.EffectiveDate;
              nullable1 = pmLaborCostRate7.EffectiveDate;
              if ((effectiveDate.HasValue & nullable1.HasValue ? (effectiveDate.GetValueOrDefault() > nullable1.GetValueOrDefault() ? 1 : 0) : 0) != 0)
              {
                pmLaborCostRate7 = pmLaborCostRate14;
                continue;
              }
              continue;
            }
          }
          int? projectId6 = pmLaborCostRate14.ProjectID;
          int? nullable14 = projectID;
          if (projectId6.GetValueOrDefault() == nullable14.GetValueOrDefault() & projectId6.HasValue == nullable14.HasValue)
          {
            int? employeeId4 = pmLaborCostRate14.EmployeeID;
            int? nullable15 = employeeId;
            if (employeeId4.GetValueOrDefault() == nullable15.GetValueOrDefault() & employeeId4.HasValue == nullable15.HasValue && !pmLaborCostRate14.InventoryID.HasValue)
            {
              if (pmLaborCostRate8 == null)
              {
                pmLaborCostRate8 = pmLaborCostRate14;
                continue;
              }
              nullable1 = pmLaborCostRate14.EffectiveDate;
              effectiveDate = pmLaborCostRate8.EffectiveDate;
              if ((nullable1.HasValue & effectiveDate.HasValue ? (nullable1.GetValueOrDefault() > effectiveDate.GetValueOrDefault() ? 1 : 0) : 0) != 0)
              {
                pmLaborCostRate8 = pmLaborCostRate14;
                continue;
              }
              continue;
            }
          }
          int? projectId7 = pmLaborCostRate14.ProjectID;
          int? nullable16 = projectID;
          if (projectId7.GetValueOrDefault() == nullable16.GetValueOrDefault() & projectId7.HasValue == nullable16.HasValue && !pmLaborCostRate14.EmployeeID.HasValue && pmLaborCostRate14.InventoryID.HasValue)
          {
            if (pmLaborCostRate9 == null)
            {
              pmLaborCostRate9 = pmLaborCostRate14;
            }
            else
            {
              effectiveDate = pmLaborCostRate14.EffectiveDate;
              nullable1 = pmLaborCostRate9.EffectiveDate;
              if ((effectiveDate.HasValue & nullable1.HasValue ? (effectiveDate.GetValueOrDefault() > nullable1.GetValueOrDefault() ? 1 : 0) : 0) != 0)
                pmLaborCostRate9 = pmLaborCostRate14;
            }
          }
          else
          {
            int? projectId8 = pmLaborCostRate14.ProjectID;
            int? nullable17 = projectID;
            if (projectId8.GetValueOrDefault() == nullable17.GetValueOrDefault() & projectId8.HasValue == nullable17.HasValue && !pmLaborCostRate14.EmployeeID.HasValue && !pmLaborCostRate14.InventoryID.HasValue)
            {
              if (pmLaborCostRate10 == null)
              {
                pmLaborCostRate10 = pmLaborCostRate14;
              }
              else
              {
                nullable1 = pmLaborCostRate14.EffectiveDate;
                effectiveDate = pmLaborCostRate10.EffectiveDate;
                if ((nullable1.HasValue & effectiveDate.HasValue ? (nullable1.GetValueOrDefault() > effectiveDate.GetValueOrDefault() ? 1 : 0) : 0) != 0)
                  pmLaborCostRate10 = pmLaborCostRate14;
              }
            }
          }
        }
      }
    }
    PMLaborCostRate pmLaborCostRate15 = (((((((((pmLaborCostRate3 ?? pmLaborCostRate4) ?? pmLaborCostRate5) ?? pmLaborCostRate6) ?? pmLaborCostRate7) ?? pmLaborCostRate8) ?? pmLaborCostRate9) ?? pmLaborCostRate10) ?? pmLaborCostRate11) ?? pmLaborCostRate12) ?? pmLaborCostRate13;
    Decimal? nullable;
    if (pmLaborCostRate15 == null)
      pmLaborCostRate15 = pmLaborCostRate2;
    else if (pmLaborCostRate2 != null)
    {
      Decimal? rate = pmLaborCostRate15.Rate;
      nullable = pmLaborCostRate2.Rate;
      if (rate.GetValueOrDefault() < nullable.GetValueOrDefault() & rate.HasValue & nullable.HasValue)
        pmLaborCostRate15 = pmLaborCostRate2;
    }
    Decimal? rate1;
    if (certifiedJob.GetValueOrDefault())
    {
      if (pmLaborCostRate15 == null)
        pmLaborCostRate15 = pmLaborCostRate1;
      else if (pmLaborCostRate1 != null)
      {
        nullable = pmLaborCostRate15.Rate;
        rate1 = pmLaborCostRate1.Rate;
        if (nullable.GetValueOrDefault() < rate1.GetValueOrDefault() & nullable.HasValue & rate1.HasValue)
          pmLaborCostRate15 = pmLaborCostRate1;
      }
    }
    if (pmLaborCostRate15 == null)
      return (EmployeeCostEngine.Rate) null;
    Decimal? regularHours1 = pmLaborCostRate15.RegularHours;
    if (pmLaborCostRate11 != null)
      regularHours1 = pmLaborCostRate11.RegularHours;
    int? employeeID = employeeId;
    string employmentType = pmLaborCostRate15.EmploymentType;
    Decimal? rate2 = pmLaborCostRate15.Rate;
    string defaultUom = this.defaultUOM;
    Decimal? regularHours2 = regularHours1;
    Decimal? rateByType;
    if (!(pmLaborCostRate15.EmploymentType == "H"))
    {
      rate1 = pmLaborCostRate15.Rate;
      nullable = regularHours1;
      rateByType = rate1.HasValue & nullable.HasValue ? new Decimal?(rate1.GetValueOrDefault() * nullable.GetValueOrDefault()) : new Decimal?();
    }
    else
      rateByType = pmLaborCostRate15.Rate;
    string curyId = pmLaborCostRate15.CuryID;
    return new EmployeeCostEngine.Rate(employeeID, employmentType, rate2, defaultUom, regularHours2, rateByType, curyId);
  }

  public virtual EmployeeCostEngine.LaborCost CalculateEmployeeCost(
    string timeCardCD,
    string earningTypeID,
    int? laborItemID,
    int? projectID,
    int? projectTaskID,
    bool? certifiedJob,
    string unionID,
    int? employeeID,
    DateTime date,
    int? shiftID = null)
  {
    Decimal otMultiplier = 1M;
    EmployeeCostEngine.Rate employeeRate = this.GetEmployeeRate(laborItemID, projectID, projectTaskID, certifiedJob, unionID, employeeID, new DateTime?(date));
    if (employeeRate == null)
      return (EmployeeCostEngine.LaborCost) null;
    Decimal? rate;
    if (employeeRate.Type == "E" && timeCardCD != null)
    {
      EPTimeCard epTimeCard = EPTimeCard.PK.Find(this.graph, timeCardCD);
      if (!epTimeCard.TotalSpentCalc.HasValue)
      {
        PMTimeActivity pmTimeActivity = PXResultset<PMTimeActivity>.op_Implicit(((PXSelectBase<PMTimeActivity>) new PXSelectGroupBy<PMTimeActivity, Where<PMTimeActivity.timeCardCD, Equal<Required<PMTimeActivity.timeCardCD>>>, Aggregate<Sum<PMTimeActivity.timeSpent>>>(this.graph)).Select(new object[1]
        {
          (object) timeCardCD
        }));
        epTimeCard.TotalSpentCalc = pmTimeActivity.TimeSpent;
      }
      int? totalSpentCalc = epTimeCard.TotalSpentCalc;
      Decimal? nullable1 = totalSpentCalc.HasValue ? new Decimal?((Decimal) totalSpentCalc.GetValueOrDefault()) : new Decimal?();
      Decimal? nullable2 = employeeRate.RegularHours;
      Decimal num = 60M;
      Decimal? nullable3 = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * num) : new Decimal?();
      if (nullable1.GetValueOrDefault() <= nullable3.GetValueOrDefault() & nullable1.HasValue & nullable3.HasValue)
      {
        Decimal? rateByType = employeeRate.RateByType;
        nullable1 = employeeRate.RegularHours;
        Decimal? nullable4;
        if (!(rateByType.HasValue & nullable1.HasValue))
        {
          nullable2 = new Decimal?();
          nullable4 = nullable2;
        }
        else
          nullable4 = new Decimal?(rateByType.GetValueOrDefault() / nullable1.GetValueOrDefault());
        rate = nullable4;
      }
      else
      {
        nullable1 = employeeRate.RateByType;
        totalSpentCalc = epTimeCard.TotalSpentCalc;
        Decimal? nullable5;
        if (!totalSpentCalc.HasValue)
        {
          nullable2 = new Decimal?();
          nullable5 = nullable2;
        }
        else
          nullable5 = new Decimal?((Decimal) totalSpentCalc.GetValueOrDefault() / 60M);
        Decimal? nullable6 = nullable5;
        Decimal? nullable7;
        if (!(nullable1.HasValue & nullable6.HasValue))
        {
          nullable2 = new Decimal?();
          nullable7 = nullable2;
        }
        else
          nullable7 = new Decimal?(nullable1.GetValueOrDefault() / nullable6.GetValueOrDefault());
        rate = nullable7;
      }
    }
    else
    {
      otMultiplier = this.GetOvertimeMultiplier(earningTypeID, employeeRate);
      rate = new Decimal?(employeeRate.HourlyRate * otMultiplier);
    }
    if (shiftID.HasValue && rate.HasValue)
      rate = new Decimal?(EPShiftCodeSetup.CalculateShiftCosting(this.graph, shiftID, new DateTime?(date), rate.Value, otMultiplier));
    return new EmployeeCostEngine.LaborCost(rate, new Decimal?(otMultiplier), employeeRate.CuryID);
  }

  public virtual Decimal GetOvertimeMultiplier(
    string earningTypeID,
    EmployeeCostEngine.Rate employeeRate)
  {
    if (earningTypeID == null || employeeRate != null && employeeRate.Type == "E")
      return 1M;
    EPEarningType epEarningType = PXResultset<EPEarningType>.op_Implicit(PXSelectBase<EPEarningType, PXSelect<EPEarningType>.Config>.Search<EPEarningType.typeCD>(this.graph, (object) earningTypeID, Array.Empty<object>()));
    return epEarningType == null || !epEarningType.IsOvertime.GetValueOrDefault() ? 1M : epEarningType.OvertimeMultiplier.Value;
  }

  public virtual Decimal GetOvertimeMultiplier(
    string earningTypeID,
    int? employeeID,
    int? laborItemID,
    DateTime date)
  {
    EmployeeCostEngine.Rate employeeRate = this.GetEmployeeRate(laborItemID, new int?(), new int?(), new bool?(false), (string) null, employeeID, new DateTime?(date));
    return this.GetOvertimeMultiplier(earningTypeID, employeeRate);
  }

  public virtual Decimal GetEmployeeRegularHours(int? employeeID, DateTime? effectiveDate)
  {
    PMLaborCostRate pmLaborCostRate = PXResultset<PMLaborCostRate>.op_Implicit(((PXSelectBase<PMLaborCostRate>) new PXSelect<PMLaborCostRate, Where<PMLaborCostRate.employeeID, Equal<Required<PMLaborCostRate.employeeID>>, And<PMLaborCostRate.type, Equal<PMLaborCostRateType.employee>, And<PMLaborCostRate.effectiveDate, LessEqual<Required<PMLaborCostRate.effectiveDate>>>>>, OrderBy<Desc<PMLaborCostRate.effectiveDate>>>(this.graph)).Select(new object[2]
    {
      (object) employeeID,
      (object) effectiveDate
    }));
    if (pmLaborCostRate != null)
    {
      Decimal? regularHours = pmLaborCostRate.RegularHours;
      if (regularHours.HasValue)
      {
        regularHours = pmLaborCostRate.RegularHours;
        return regularHours.Value;
      }
    }
    return this.GetEmployeeHoursFromCalendar(employeeID);
  }

  public virtual Decimal GetEmployeeHoursFromCalendar(int? employeeID)
  {
    Decimal hoursFromCalendar = 40M;
    EPEmployee epEmployee = PXResultset<EPEmployee>.op_Implicit(((PXSelectBase<EPEmployee>) new PXSelect<EPEmployee, Where<EPEmployee.bAccountID, Equal<Required<EPEmployee.bAccountID>>>>(this.graph)).Select(new object[1]
    {
      (object) employeeID
    }));
    if (epEmployee != null && epEmployee.CalendarID != null)
    {
      CSCalendar calendar = PXResultset<CSCalendar>.op_Implicit(PXSelectBase<CSCalendar, PXSelect<CSCalendar, Where<CSCalendar.calendarID, Equal<Required<CSCalendar.calendarID>>>>.Config>.Select(this.graph, new object[1]
      {
        (object) epEmployee.CalendarID
      }));
      if (calendar != null)
        hoursFromCalendar = 0M + CalendarHelper.GetHoursWorkedOnDay(calendar, DayOfWeek.Monday) + CalendarHelper.GetHoursWorkedOnDay(calendar, DayOfWeek.Tuesday) + CalendarHelper.GetHoursWorkedOnDay(calendar, DayOfWeek.Wednesday) + CalendarHelper.GetHoursWorkedOnDay(calendar, DayOfWeek.Thursday) + CalendarHelper.GetHoursWorkedOnDay(calendar, DayOfWeek.Friday) + CalendarHelper.GetHoursWorkedOnDay(calendar, DayOfWeek.Saturday) + CalendarHelper.GetHoursWorkedOnDay(calendar, DayOfWeek.Sunday);
    }
    return hoursFromCalendar;
  }

  public virtual int GetEmployeeRegularWeeklyMinutes(int? employeeID, DateTime? effectiveDate)
  {
    PMLaborCostRate pmLaborCostRate = PXResultset<PMLaborCostRate>.op_Implicit(PXSelectBase<PMLaborCostRate, PXViewOf<PMLaborCostRate>.BasedOn<SelectFromBase<PMLaborCostRate, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMLaborCostRate.employeeID, Equal<P.AsInt>>>>, And<BqlOperand<PMLaborCostRate.type, IBqlString>.IsEqual<PMLaborCostRateType.employee>>>>.And<BqlOperand<PMLaborCostRate.effectiveDate, IBqlDateTime>.IsLessEqual<P.AsDateTime>>>.Order<By<BqlField<PMLaborCostRate.effectiveDate, IBqlDateTime>.Desc>>>.ReadOnly.Config>.SelectSingleBound(this.graph, (object[]) null, new object[2]
    {
      (object) employeeID,
      (object) effectiveDate
    }));
    if (pmLaborCostRate != null)
    {
      Decimal? regularHours = pmLaborCostRate.RegularHours;
      if (regularHours.HasValue)
      {
        regularHours = pmLaborCostRate.RegularHours;
        return (int) (regularHours.Value * 60M);
      }
    }
    return this.GetEmployeeWeeklyMinutesFromCalendar(employeeID);
  }

  public virtual int GetEmployeeWeeklyMinutesFromCalendar(int? employeeID)
  {
    int minutesFromCalendar = 2400;
    EPEmployee epEmployee = EPEmployee.PK.Find(this.graph, employeeID);
    if (epEmployee != null && epEmployee.CalendarID != null)
    {
      CSCalendar calendar = CSCalendar.PK.Find(this.graph, epEmployee.CalendarID);
      if (calendar != null)
      {
        minutesFromCalendar = 0;
        for (DayOfWeek dayOfWeek = DayOfWeek.Sunday; dayOfWeek <= DayOfWeek.Saturday; ++dayOfWeek)
          minutesFromCalendar += CalendarHelper.GetMinutesWorkedOnDay(calendar, dayOfWeek);
      }
    }
    return minutesFromCalendar;
  }

  public virtual int? GetLaborClass(PMTimeActivity activity)
  {
    CRCase refCase = PXResultset<CRCase>.op_Implicit(PXSelectBase<CRCase, PXSelectJoin<CRCase, InnerJoin<CRActivityLink, On<CRActivityLink.refNoteID, Equal<CRCase.noteID>>>, Where<CRActivityLink.noteID, Equal<Required<PMTimeActivity.refNoteID>>>>.Config>.Select(this.graph, new object[1]
    {
      (object) activity.RefNoteID
    }));
    EPEmployee employee = PXResultset<EPEmployee>.op_Implicit(PXSelectBase<EPEmployee, PXSelect<EPEmployee>.Config>.Search<EPEmployee.defContactID>(this.graph, (object) activity.OwnerID, Array.Empty<object>()));
    return this.GetLaborClass(activity, employee, refCase);
  }

  public virtual int? GetLaborClass(PMTimeActivity activity, EPEmployee employee, CRCase refCase)
  {
    if (employee == null)
      throw new ArgumentNullException(nameof (employee), "Employee must be set");
    int? laborClass = new int?();
    if (refCase != null)
    {
      CRCaseClass crCaseClass = (CRCaseClass) PXSelectorAttribute.Select<CRCase.caseClassID>(this.graph.Caches[typeof (CRCase)], (object) refCase);
      if (crCaseClass.PerItemBilling.GetValueOrDefault() == 1)
        laborClass = CRCaseClassLaborMatrix.GetLaborClassID(this.graph, crCaseClass.CaseClassID, activity.EarningTypeID);
    }
    if (!laborClass.HasValue && activity.LabourItemID.HasValue)
      laborClass = activity.LabourItemID;
    if (!laborClass.HasValue && activity.ProjectID.HasValue && employee.BAccountID.HasValue)
    {
      PXGraph graph = this.graph;
      int? nullable = activity.ProjectID;
      int projectID = nullable.Value;
      nullable = employee.BAccountID;
      int employeeID = nullable.Value;
      string earningTypeId = activity.EarningTypeID;
      laborClass = EPContractRate.GetProjectLaborClassID(graph, projectID, employeeID, earningTypeId);
    }
    if (!laborClass.HasValue)
      laborClass = EPEmployeeClassLaborMatrix.GetLaborClassID(this.graph, employee.BAccountID, activity.EarningTypeID);
    if (!laborClass.HasValue)
      laborClass = employee.LabourItemID;
    return laborClass;
  }

  public class Rate
  {
    public int? EmployeeID { get; private set; }

    public Decimal HourlyRate { get; private set; }

    public string Type { get; private set; }

    public string UOM { get; private set; }

    public string CuryID { get; private set; }

    /// <summary>Total Hours in week</summary>
    public Decimal? RegularHours { get; private set; }

    /// <summary>Employee Rate for a Week</summary>
    public Decimal? RateByType { get; private set; }

    public Rate(
      int? employeeID,
      string type,
      Decimal? hourlyRate,
      string uom,
      Decimal? regularHours,
      Decimal? rateByType)
    {
      this.EmployeeID = employeeID;
      this.UOM = uom;
      this.HourlyRate = hourlyRate.GetValueOrDefault();
      this.Type = string.IsNullOrEmpty(type) ? "H" : type;
      this.RegularHours = new Decimal?(regularHours.GetValueOrDefault());
      this.RateByType = rateByType;
    }

    public Rate(
      int? employeeID,
      string type,
      Decimal? hourlyRate,
      string uom,
      Decimal? regularHours,
      Decimal? rateByType,
      string curyID)
    {
      this.EmployeeID = employeeID;
      this.UOM = uom;
      this.HourlyRate = hourlyRate.GetValueOrDefault();
      this.Type = string.IsNullOrEmpty(type) ? "H" : type;
      this.RegularHours = new Decimal?(regularHours.GetValueOrDefault());
      this.RateByType = rateByType;
      this.CuryID = curyID;
    }
  }

  public class LaborCost
  {
    public Decimal? Rate { get; private set; }

    public Decimal? OvertimeMultiplier { get; private set; }

    public string CuryID { get; private set; }

    public LaborCost(Decimal? rate, Decimal? mult)
    {
      this.Rate = rate;
      this.OvertimeMultiplier = mult;
    }

    public LaborCost(Decimal? rate, Decimal? mult, string curyID)
    {
      this.Rate = rate;
      this.OvertimeMultiplier = mult;
      this.CuryID = curyID;
    }
  }
}
