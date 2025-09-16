// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ProjectDateSensitiveCostsInquiry
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

#nullable enable
namespace PX.Objects.PM;

[Serializable]
public class ProjectDateSensitiveCostsInquiry : PXGraph<
#nullable disable
ProjectDateSensitiveCostsInquiry>
{
  /// <summary>Project Financial Vision Filter.</summary>
  public PXFilter<PMDateSensitiveDataRevision> Revision;
  public PXCancel<PMDateSensitiveDataRevision> Cancel;
  /// <summary>Project Financial Vision items.</summary>
  [PXVirtualDAC]
  public FbqlSelect<SelectFromBase<PMDateSensitiveDataRevisionLine, TypeArrayOf<IFbqlJoin>.Empty>.Order<By<BqlField<
  #nullable enable
  PMDateSensitiveDataRevisionLine.curveID, IBqlString>.Asc, 
  #nullable disable
  BqlField<
  #nullable enable
  PMDateSensitiveDataRevisionLine.pointNumber, IBqlInt>.Asc>>, 
  #nullable disable
  PMDateSensitiveDataRevisionLine>.View Items;
  public FbqlSelect<SelectFromBase<PMProject, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<
  #nullable enable
  PMProject.contractID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PMDateSensitiveDataRevision.projectID, IBqlInt>.FromCurrent>>, 
  #nullable disable
  PMProject>.View Project;
  public FbqlSelect<SelectFromBase<PMBudget, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PMBudget.projectID, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  PMDateSensitiveDataRevision.projectID, IBqlInt>.FromCurrent>>>>>.And<
  #nullable disable
  BqlOperand<
  #nullable enable
  PMBudget.type, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PMDateSensitiveDataRevision.accountGroups, IBqlString>.FromCurrent>>>.Aggregate<
  #nullable disable
  To<GroupBy<PMBudget.projectID>, Sum<PMBudget.curyActualAmount>, Sum<PMBudget.curyRevisedAmount>>>, PMBudget>.View BudgetTotals;
  public PXAction<PMDateSensitiveDataRevision> viewTransactions;
  public PXAction<PMDateSensitiveDataRevision> zoomIn;
  public PXAction<PMDateSensitiveDataRevision> zoomOut;
  public PXAction<PMDateSensitiveDataRevision> zoomToYear;
  public PXAction<PMDateSensitiveDataRevision> zoomToQuarter;
  public PXAction<PMDateSensitiveDataRevision> zoomToMonth;
  public PXAction<PMDateSensitiveDataRevision> showChart;

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Project Start", Required = false, Enabled = false)]
  protected virtual void _(Events.CacheAttached<PMProject.startDate> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Project End", Required = false, Enabled = false)]
  protected virtual void _(Events.CacheAttached<PMProject.expireDate> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Total Actual Amount", Enabled = false)]
  protected virtual void _(Events.CacheAttached<PMBudget.curyActualAmount> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Total Budgeted Amount", Enabled = false)]
  protected virtual void _(Events.CacheAttached<PMBudget.curyRevisedAmount> e)
  {
  }

  public virtual IEnumerable items()
  {
    PMDateSensitiveDataRevisionLine[] dataRevisionLineArray = Array.Empty<PMDateSensitiveDataRevisionLine>();
    PMDateSensitiveDataRevision current = ((PXSelectBase<PMDateSensitiveDataRevision>) this.Revision).Current;
    if ((current != null ? (!current.ProjectID.HasValue ? 1 : 0) : 1) != 0)
      return (IEnumerable) dataRevisionLineArray;
    if (((PXSelectBase<PMDateSensitiveDataRevision>) this.Revision).Current.StartDate.HasValue && ((PXSelectBase<PMDateSensitiveDataRevision>) this.Revision).Current.EndDate.HasValue)
    {
      DateTime? startDate = ((PXSelectBase<PMDateSensitiveDataRevision>) this.Revision).Current.StartDate;
      DateTime? endDate = ((PXSelectBase<PMDateSensitiveDataRevision>) this.Revision).Current.EndDate;
      if ((startDate.HasValue & endDate.HasValue ? (startDate.GetValueOrDefault() > endDate.GetValueOrDefault() ? 1 : 0) : 0) != 0)
        return (IEnumerable) dataRevisionLineArray;
    }
    return (IEnumerable) this.CalculateItems();
  }

  public virtual IEnumerable<PMDateSensitiveDataRevisionLine> CalculateItems()
  {
    ProjectDateSensitiveCostsInquiry sensitiveCostsInquiry1 = this;
    IDictionary<string, ProjectDateSensitiveCostsInquiry.CurveStatus> curves = sensitiveCostsInquiry1.GetAccumulatedActualsBeforeStartDate();
    DateTime firstPointDate = DateTime.MinValue;
    DateTime currentHistoryPointDate = DateTime.MinValue;
    int currentHistoryPointNumber = 0;
    DateTime historyPointDate;
    PMDateSensitiveDataRevisionLine currentPoint;
    int preHistoryPointNumber;
    KeyValuePair<string, ProjectDateSensitiveCostsInquiry.CurveStatus> curve;
    PMDateSensitiveDataRevisionLine preHistoryPoint;
    foreach (PMHistoryByDate historyItem in sensitiveCostsInquiry1.SelectDraftData())
    {
      string curveId = sensitiveCostsInquiry1.GetCurveID(historyItem);
      ProjectDateSensitiveCostsInquiry.CurveStatus curveStatus;
      if (!curves.TryGetValue(curveId, out curveStatus))
      {
        curveStatus = new ProjectDateSensitiveCostsInquiry.CurveStatus();
        curves.Add(curveId, curveStatus);
      }
      historyPointDate = sensitiveCostsInquiry1.GetHistoryPointDate(historyItem);
      DateTime preHistoryPointDate;
      DateTime? nullable1;
      if (currentHistoryPointNumber == 0)
      {
        firstPointDate = historyPointDate;
        currentHistoryPointDate = firstPointDate;
        currentHistoryPointNumber = 1;
        if (((PXSelectBase<PMDateSensitiveDataRevision>) sensitiveCostsInquiry1.Revision).Current.StartDate.HasValue)
        {
          DateTime? startDate = ((PXSelectBase<PMDateSensitiveDataRevision>) sensitiveCostsInquiry1.Revision).Current.StartDate;
          DateTime dateTime = currentHistoryPointDate;
          if ((startDate.HasValue ? (startDate.GetValueOrDefault() < dateTime ? 1 : 0) : 0) != 0)
          {
            ProjectDateSensitiveCostsInquiry sensitiveCostsInquiry2 = sensitiveCostsInquiry1;
            nullable1 = ((PXSelectBase<PMDateSensitiveDataRevision>) sensitiveCostsInquiry1.Revision).Current.StartDate;
            DateTime dt = nullable1.Value;
            firstPointDate = sensitiveCostsInquiry2.GetHistoryPointDate(dt);
            preHistoryPointDate = firstPointDate;
            preHistoryPointNumber = 0;
            for (; preHistoryPointDate < currentHistoryPointDate; preHistoryPointDate = sensitiveCostsInquiry1.GetNextHistoryPointDate(preHistoryPointDate))
            {
              ++preHistoryPointNumber;
              foreach (KeyValuePair<string, ProjectDateSensitiveCostsInquiry.CurveStatus> keyValuePair in curves.Where<KeyValuePair<string, ProjectDateSensitiveCostsInquiry.CurveStatus>>((Func<KeyValuePair<string, ProjectDateSensitiveCostsInquiry.CurveStatus>, bool>) (x => x.Value.AccumulatedBeforeActualGroup != null)))
              {
                curve = keyValuePair;
                preHistoryPoint = sensitiveCostsInquiry1.CreateNewPoint(curve.Key, preHistoryPointNumber, curve.Value.AccumulatedBeforeActualGroup);
                sensitiveCostsInquiry1.SetPointActualAndModelValues(preHistoryPoint, curve.Value);
                sensitiveCostsInquiry1.SetPointDateProperties(preHistoryPoint, preHistoryPointDate);
                yield return preHistoryPoint;
                curve.Value.CurrentPointDate = new DateTime?(preHistoryPointDate);
                curve.Value.CurrentPointNumber = preHistoryPointNumber;
                curve.Value.LastPoint = preHistoryPoint;
                preHistoryPoint = (PMDateSensitiveDataRevisionLine) null;
                curve = new KeyValuePair<string, ProjectDateSensitiveCostsInquiry.CurveStatus>();
              }
            }
            currentHistoryPointNumber += preHistoryPointNumber;
          }
        }
      }
      else
      {
        for (; currentHistoryPointDate < historyPointDate; currentHistoryPointDate = sensitiveCostsInquiry1.GetNextHistoryPointDate(currentHistoryPointDate))
          ++currentHistoryPointNumber;
      }
      if (curveStatus.LastPoint != null)
      {
        nullable1 = curveStatus.LastPoint.Date;
        preHistoryPointDate = nullable1.Value;
        for (preHistoryPointNumber = curveStatus.CurrentPointNumber + 1; preHistoryPointNumber < currentHistoryPointNumber; ++preHistoryPointNumber)
        {
          PMDateSensitiveDataRevisionLine copy = (PMDateSensitiveDataRevisionLine) ((PXSelectBase) sensitiveCostsInquiry1.Items).Cache.CreateCopy((object) curveStatus.LastPoint);
          copy.PointNumber = new int?(preHistoryPointNumber);
          preHistoryPointDate = sensitiveCostsInquiry1.GetNextHistoryPointDate(preHistoryPointDate);
          sensitiveCostsInquiry1.SetPointActualAndModelValues(copy, curveStatus);
          sensitiveCostsInquiry1.SetPointDateProperties(copy, preHistoryPointDate);
          yield return copy;
        }
      }
      else if (currentHistoryPointNumber > 1)
      {
        preHistoryPointDate = firstPointDate;
        for (preHistoryPointNumber = 1; preHistoryPointNumber < currentHistoryPointNumber; ++preHistoryPointNumber)
        {
          PMDateSensitiveDataRevisionLine newPoint = sensitiveCostsInquiry1.CreateNewPoint(curveId, preHistoryPointNumber, historyItem);
          sensitiveCostsInquiry1.SetPointActualAndModelValues(newPoint, curveStatus);
          sensitiveCostsInquiry1.SetPointDateProperties(newPoint, preHistoryPointDate);
          preHistoryPointDate = sensitiveCostsInquiry1.GetNextHistoryPointDate(preHistoryPointDate);
          yield return newPoint;
        }
      }
      curveStatus.ActualQty += historyItem.ActualQty.GetValueOrDefault();
      ProjectDateSensitiveCostsInquiry.CurveStatus curveStatus1 = curveStatus;
      Decimal curyActualAmount = curveStatus1.CuryActualAmount;
      Decimal? nullable2 = historyItem.CuryActualAmount;
      Decimal valueOrDefault1 = nullable2.GetValueOrDefault();
      curveStatus1.CuryActualAmount = curyActualAmount + valueOrDefault1;
      ProjectDateSensitiveCostsInquiry.CurveStatus curveStatus2 = curveStatus;
      Decimal actualAmount = curveStatus2.ActualAmount;
      nullable2 = historyItem.ActualAmount;
      Decimal valueOrDefault2 = nullable2.GetValueOrDefault();
      curveStatus2.ActualAmount = actualAmount + valueOrDefault2;
      currentPoint = sensitiveCostsInquiry1.CreateNewPoint(curveId, currentHistoryPointNumber, historyItem);
      sensitiveCostsInquiry1.SetPointActualAndModelValues(currentPoint, curveStatus, historyItem);
      sensitiveCostsInquiry1.SetPointDateProperties(currentPoint, historyPointDate);
      yield return currentPoint;
      curveStatus.CurrentPointDate = new DateTime?(currentHistoryPointDate);
      curveStatus.CurrentPointNumber = currentHistoryPointNumber;
      curveStatus.LastPoint = currentPoint;
      curveId = (string) null;
      curveStatus = (ProjectDateSensitiveCostsInquiry.CurveStatus) null;
      currentPoint = (PMDateSensitiveDataRevisionLine) null;
    }
    DateTime? nullable = ((PXSelectBase<PMDateSensitiveDataRevision>) sensitiveCostsInquiry1.Revision).Current.StartDate;
    if (nullable.HasValue)
    {
      if (firstPointDate == DateTime.MinValue)
      {
        ProjectDateSensitiveCostsInquiry sensitiveCostsInquiry3 = sensitiveCostsInquiry1;
        nullable = ((PXSelectBase<PMDateSensitiveDataRevision>) sensitiveCostsInquiry1.Revision).Current.StartDate;
        DateTime dt = nullable.Value;
        firstPointDate = sensitiveCostsInquiry3.GetHistoryPointDate(dt);
      }
      if (currentHistoryPointDate == DateTime.MinValue)
        currentHistoryPointDate = firstPointDate;
    }
    foreach (KeyValuePair<string, ProjectDateSensitiveCostsInquiry.CurveStatus> keyValuePair in (IEnumerable<KeyValuePair<string, ProjectDateSensitiveCostsInquiry.CurveStatus>>) curves)
    {
      curve = keyValuePair;
      nullable = curve.Value.CurrentPointDate;
      DateTime dateTime;
      if (!nullable.HasValue)
      {
        dateTime = firstPointDate;
      }
      else
      {
        ProjectDateSensitiveCostsInquiry sensitiveCostsInquiry4 = sensitiveCostsInquiry1;
        nullable = curve.Value.CurrentPointDate;
        DateTime dt = nullable.Value;
        dateTime = sensitiveCostsInquiry4.GetNextHistoryPointDate(dt);
      }
      historyPointDate = dateTime;
      preHistoryPointNumber = curve.Value.CurrentPointNumber > 0 ? curve.Value.CurrentPointNumber + 1 : 1;
      currentPoint = curve.Value.LastPoint != null ? curve.Value.LastPoint : sensitiveCostsInquiry1.CreateNewPoint(curve.Key, preHistoryPointNumber, curve.Value.AccumulatedBeforeActualGroup);
      while (historyPointDate <= currentHistoryPointDate)
      {
        preHistoryPoint = (PMDateSensitiveDataRevisionLine) ((PXSelectBase) sensitiveCostsInquiry1.Items).Cache.CreateCopy((object) currentPoint);
        preHistoryPoint.PointNumber = new int?(preHistoryPointNumber);
        sensitiveCostsInquiry1.SetPointActualAndModelValues(preHistoryPoint, curve.Value);
        sensitiveCostsInquiry1.SetPointDateProperties(preHistoryPoint, historyPointDate);
        yield return preHistoryPoint;
        curve.Value.CurrentPointDate = new DateTime?(historyPointDate);
        curve.Value.CurrentPointNumber = preHistoryPointNumber;
        curve.Value.LastPoint = preHistoryPoint;
        if (currentHistoryPointNumber < preHistoryPointNumber)
          currentHistoryPointNumber = preHistoryPointNumber;
        historyPointDate = sensitiveCostsInquiry1.GetNextHistoryPointDate(historyPointDate);
        ++preHistoryPointNumber;
        preHistoryPoint = (PMDateSensitiveDataRevisionLine) null;
      }
      currentPoint = (PMDateSensitiveDataRevisionLine) null;
      curve = new KeyValuePair<string, ProjectDateSensitiveCostsInquiry.CurveStatus>();
    }
    nullable = ((PXSelectBase<PMDateSensitiveDataRevision>) sensitiveCostsInquiry1.Revision).Current.EndDate;
    if (nullable.HasValue)
    {
      nullable = ((PXSelectBase<PMDateSensitiveDataRevision>) sensitiveCostsInquiry1.Revision).Current.EndDate;
      DateTime dateTime = currentHistoryPointDate;
      if ((nullable.HasValue ? (nullable.GetValueOrDefault() > dateTime ? 1 : 0) : 0) != 0)
      {
        ProjectDateSensitiveCostsInquiry sensitiveCostsInquiry5 = sensitiveCostsInquiry1;
        nullable = ((PXSelectBase<PMDateSensitiveDataRevision>) sensitiveCostsInquiry1.Revision).Current.EndDate;
        DateTime dt = nullable.Value;
        historyPointDate = sensitiveCostsInquiry5.GetHistoryPointDate(dt);
        while (currentHistoryPointDate < historyPointDate)
        {
          currentHistoryPointDate = sensitiveCostsInquiry1.GetNextHistoryPointDate(currentHistoryPointDate);
          ++currentHistoryPointNumber;
          foreach (KeyValuePair<string, ProjectDateSensitiveCostsInquiry.CurveStatus> keyValuePair in curves.Where<KeyValuePair<string, ProjectDateSensitiveCostsInquiry.CurveStatus>>((Func<KeyValuePair<string, ProjectDateSensitiveCostsInquiry.CurveStatus>, bool>) (x => x.Value.LastPoint != null)))
          {
            curve = keyValuePair;
            currentPoint = (PMDateSensitiveDataRevisionLine) ((PXSelectBase) sensitiveCostsInquiry1.Items).Cache.CreateCopy((object) curve.Value.LastPoint);
            currentPoint.PointNumber = new int?(currentHistoryPointNumber);
            sensitiveCostsInquiry1.SetPointActualAndModelValues(currentPoint, curve.Value);
            sensitiveCostsInquiry1.SetPointDateProperties(currentPoint, currentHistoryPointDate);
            yield return currentPoint;
            curve.Value.CurrentPointDate = new DateTime?(currentHistoryPointDate);
            curve.Value.CurrentPointNumber = currentHistoryPointNumber;
            curve.Value.LastPoint = currentPoint;
            currentPoint = (PMDateSensitiveDataRevisionLine) null;
            curve = new KeyValuePair<string, ProjectDateSensitiveCostsInquiry.CurveStatus>();
          }
        }
      }
    }
  }

  private IEnumerable<T> RunSelectCommandReadOnly<T>(BqlCommand bqlCommand) where T : IBqlTable
  {
    foreach (object obj in new PXView((PXGraph) this, true, bqlCommand).SelectMulti(Array.Empty<object>()))
      yield return PXResult.Unwrap<T>(obj);
  }

  private Type[] ExtendTypeArray(Type[] types, int length)
  {
    if (types.Length >= length)
      return types;
    List<Type> typeList = new List<Type>((IEnumerable<Type>) types);
    for (int length1 = types.Length; length1 < length; ++length1)
      typeList.Add(typeof (PMHistoryByDate.groupID));
    return typeList.ToArray();
  }

  private IEnumerable<PMHistoryByDate> SelectDraftData()
  {
    Type[] typeArray = this.ExtendTypeArray(((IEnumerable<Type>) this.GetPeriodGroupingTypes()).Union<Type>((IEnumerable<Type>) this.GetKeyGroupingTypes()).ToArray<Type>(), 6);
    return this.RunSelectCommandReadOnly<PMHistoryByDate>(BqlTemplate.OfCommand<ProjectDateSensitiveCostsInquiry.BaseDraftDataQueryTemplate>.Replace<ProjectDateSensitiveCostsInquiry.GroupingPlaceholderA>(typeArray[0]).Replace<ProjectDateSensitiveCostsInquiry.GroupingPlaceholderB>(typeArray[1]).Replace<ProjectDateSensitiveCostsInquiry.GroupingPlaceholderC>(typeArray[2]).Replace<ProjectDateSensitiveCostsInquiry.GroupingPlaceholderD>(typeArray[3]).Replace<ProjectDateSensitiveCostsInquiry.GroupingPlaceholderE>(typeArray[4]).Replace<ProjectDateSensitiveCostsInquiry.GroupingPlaceholderF>(typeArray[5]).ToCommand());
  }

  private IEnumerable<PMHistoryByDate> SelectAccumulatedBeforeStartDateData()
  {
    Type[] typeArray = this.ExtendTypeArray(this.GetKeyGroupingTypes(), 4);
    return this.RunSelectCommandReadOnly<PMHistoryByDate>(BqlTemplate.OfCommand<ProjectDateSensitiveCostsInquiry.BaseAccumulatedBeforeStartDateDataQueryTemplate>.Replace<ProjectDateSensitiveCostsInquiry.GroupingPlaceholderA>(typeArray[0]).Replace<ProjectDateSensitiveCostsInquiry.GroupingPlaceholderB>(typeArray[1]).Replace<ProjectDateSensitiveCostsInquiry.GroupingPlaceholderC>(typeArray[2]).Replace<ProjectDateSensitiveCostsInquiry.GroupingPlaceholderD>(typeArray[3]).ToCommand());
  }

  protected virtual Type[] GetKeyGroupingTypes()
  {
    if (((PXSelectBase<PMDateSensitiveDataRevision>) this.Revision).Current == null)
      return Array.Empty<Type>();
    List<Type> typeList = new List<Type>();
    bool? nullable = ((PXSelectBase<PMDateSensitiveDataRevision>) this.Revision).Current.GroupByProjectTaskID;
    if (nullable.GetValueOrDefault())
      typeList.Add(typeof (PMHistoryByDate.projectTaskID));
    nullable = ((PXSelectBase<PMDateSensitiveDataRevision>) this.Revision).Current.GroupByAccountGroupID;
    if (nullable.GetValueOrDefault())
      typeList.Add(typeof (PMHistoryByDate.accountGroupID));
    nullable = ((PXSelectBase<PMDateSensitiveDataRevision>) this.Revision).Current.GroupByInventoryID;
    if (nullable.GetValueOrDefault())
      typeList.Add(typeof (PMHistoryByDate.inventoryID));
    nullable = ((PXSelectBase<PMDateSensitiveDataRevision>) this.Revision).Current.GroupByCostCodeID;
    if (nullable.GetValueOrDefault())
      typeList.Add(typeof (PMHistoryByDate.costCodeID));
    return typeList.ToArray();
  }

  protected virtual Type[] GetPeriodGroupingTypes()
  {
    Type[] periodGroupingTypes;
    switch (((PXSelectBase<PMDateSensitiveDataRevision>) this.Revision).Current?.Period)
    {
      case "Y":
        periodGroupingTypes = new Type[1]
        {
          typeof (PMHistoryByDate.year)
        };
        break;
      case "Q":
        periodGroupingTypes = new Type[2]
        {
          typeof (PMHistoryByDate.year),
          typeof (PMHistoryByDate.quarter)
        };
        break;
      case "M":
        periodGroupingTypes = new Type[2]
        {
          typeof (PMHistoryByDate.year),
          typeof (PMHistoryByDate.month)
        };
        break;
      default:
        periodGroupingTypes = new Type[1]
        {
          typeof (PMHistoryByDate.date)
        };
        break;
    }
    return periodGroupingTypes;
  }

  private PMDateSensitiveDataRevisionLine CreateNewPoint(
    string curveId,
    int number,
    PMHistoryByDate historyItem)
  {
    PMDateSensitiveDataRevisionLine newPoint = new PMDateSensitiveDataRevisionLine();
    newPoint.ProjectID = historyItem.ProjectID;
    newPoint.CurveID = curveId;
    newPoint.PointNumber = new int?(number);
    newPoint.ProjectTaskID = ((PXSelectBase<PMDateSensitiveDataRevision>) this.Revision).Current.GroupByProjectTaskID.GetValueOrDefault() ? historyItem.ProjectTaskID : new int?();
    bool? nullable = ((PXSelectBase<PMDateSensitiveDataRevision>) this.Revision).Current.GroupByAccountGroupID;
    newPoint.AccountGroupID = nullable.GetValueOrDefault() ? historyItem.AccountGroupID : new int?();
    nullable = ((PXSelectBase<PMDateSensitiveDataRevision>) this.Revision).Current.GroupByInventoryID;
    newPoint.InventoryID = nullable.GetValueOrDefault() ? historyItem.InventoryID : new int?();
    nullable = ((PXSelectBase<PMDateSensitiveDataRevision>) this.Revision).Current.GroupByCostCodeID;
    newPoint.CostCodeID = nullable.GetValueOrDefault() ? historyItem.CostCodeID : new int?();
    return newPoint;
  }

  private void SetPointDateProperties(PMDateSensitiveDataRevisionLine row, DateTime dt)
  {
    DateTime dt1 = dt;
    if (((PXSelectBase<PMDateSensitiveDataRevision>) this.Revision).Current.EndDate.HasValue)
    {
      DateTime dateTime = dt1;
      DateTime? endDate = ((PXSelectBase<PMDateSensitiveDataRevision>) this.Revision).Current.EndDate;
      if ((endDate.HasValue ? (dateTime > endDate.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      {
        endDate = ((PXSelectBase<PMDateSensitiveDataRevision>) this.Revision).Current.EndDate;
        dt1 = endDate.Value;
      }
    }
    row.Date = new DateTime?(dt1);
    row.Year = new int?(dt1.Year);
    row.Quarter = new int?(PMQuartetOfTheYearAttribute.GetQuarterByDate(dt1));
    row.Month = new int?(dt1.Month);
    row.Day = new int?(dt1.Day);
  }

  private void SetPointActualAndModelValues(
    PMDateSensitiveDataRevisionLine row,
    ProjectDateSensitiveCostsInquiry.CurveStatus curveStatus,
    PMHistoryByDate delta = null)
  {
    Decimal num1 = curveStatus.ActualQty + curveStatus.AccumulatedBeforeActualQty.GetValueOrDefault();
    Decimal num2 = curveStatus.CuryActualAmount + curveStatus.AccumulatedBeforeCuryActualAmount.GetValueOrDefault();
    Decimal num3 = curveStatus.ActualAmount + curveStatus.AccumulatedBeforeActualAmount.GetValueOrDefault();
    row.ActualQty = new Decimal?(num1);
    row.CuryActualAmount = new Decimal?(num2);
    row.ActualAmount = new Decimal?(num3);
    row.ActualQtyDiff = (Decimal?) delta?.ActualQty;
    row.CuryActualAmountDiff = (Decimal?) delta?.CuryActualAmount;
    row.ActualAmountDiff = (Decimal?) delta?.ActualAmount;
  }

  protected virtual IDictionary<string, ProjectDateSensitiveCostsInquiry.CurveStatus> GetAccumulatedActualsBeforeStartDate()
  {
    PMDateSensitiveDataRevision current = ((PXSelectBase<PMDateSensitiveDataRevision>) this.Revision).Current;
    return (current != null ? (!current.StartDate.HasValue ? 1 : 0) : 1) != 0 ? (IDictionary<string, ProjectDateSensitiveCostsInquiry.CurveStatus>) new Dictionary<string, ProjectDateSensitiveCostsInquiry.CurveStatus>() : (IDictionary<string, ProjectDateSensitiveCostsInquiry.CurveStatus>) this.SelectAccumulatedBeforeStartDateData().ToDictionary<PMHistoryByDate, string, ProjectDateSensitiveCostsInquiry.CurveStatus>((Func<PMHistoryByDate, string>) (x => this.GetCurveID(x)), (Func<PMHistoryByDate, ProjectDateSensitiveCostsInquiry.CurveStatus>) (x => new ProjectDateSensitiveCostsInquiry.CurveStatus()
    {
      AccumulatedBeforeActualGroup = x,
      AccumulatedBeforeActualQty = new Decimal?(x.ActualQty.GetValueOrDefault()),
      AccumulatedBeforeCuryActualAmount = new Decimal?(x.CuryActualAmount.GetValueOrDefault()),
      AccumulatedBeforeActualAmount = new Decimal?(x.ActualAmount.GetValueOrDefault())
    }));
  }

  protected virtual string GetCurveID(PMHistoryByDate historyItem)
  {
    string str1 = (((PXSelectBase<PMDateSensitiveDataRevision>) this.Revision).Current.GroupByProjectTaskID.GetValueOrDefault() ? $"T{historyItem.ProjectTaskID}" : "") ?? "";
    string str2 = (((PXSelectBase<PMDateSensitiveDataRevision>) this.Revision).Current.GroupByAccountGroupID.GetValueOrDefault() ? $"G{historyItem.AccountGroupID}" : "") ?? "";
    string str3 = (((PXSelectBase<PMDateSensitiveDataRevision>) this.Revision).Current.GroupByInventoryID.GetValueOrDefault() ? $"I{historyItem.InventoryID}" : "") ?? "";
    string str4 = (((PXSelectBase<PMDateSensitiveDataRevision>) this.Revision).Current.GroupByCostCodeID.GetValueOrDefault() ? $"C{historyItem.CostCodeID}" : "") ?? "";
    return $"P{historyItem.ProjectID}{str1}{str2}{str3}{str4}";
  }

  protected virtual DateTime GetHistoryPointDate(DateTime dt)
  {
    DateTime historyPointDate;
    switch (((PXSelectBase<PMDateSensitiveDataRevision>) this.Revision).Current?.Period?.Trim())
    {
      case "Y":
        historyPointDate = this.GetDateTimeByYear(dt.Year);
        break;
      case "Q":
        historyPointDate = this.GetDateTimeByQuarter(dt.Year, PMQuartetOfTheYearAttribute.GetQuarterByDate(dt));
        break;
      case "M":
        historyPointDate = this.GetDateTimeByMonth(dt.Year, dt.Month);
        break;
      default:
        historyPointDate = dt;
        break;
    }
    return historyPointDate;
  }

  protected virtual DateTime GetHistoryPointDate(PMHistoryByDate historyItem)
  {
    DateTime historyPointDate;
    switch (((PXSelectBase<PMDateSensitiveDataRevision>) this.Revision).Current?.Period?.Trim())
    {
      case "Y":
        historyPointDate = this.GetDateTimeByYear(historyItem);
        break;
      case "Q":
        historyPointDate = this.GetDateTimeByQuarter(historyItem);
        break;
      case "M":
        historyPointDate = this.GetDateTimeByMonth(historyItem);
        break;
      default:
        historyPointDate = historyItem.Date.Value;
        break;
    }
    return historyPointDate;
  }

  protected virtual DateTime GetNextHistoryPointDate(DateTime dt)
  {
    DateTime historyPointDate;
    switch (((PXSelectBase<PMDateSensitiveDataRevision>) this.Revision).Current?.Period?.Trim())
    {
      case "Y":
        historyPointDate = this.AddYear(dt);
        break;
      case "Q":
        historyPointDate = this.AddQuarter(dt);
        break;
      case "M":
        historyPointDate = this.AddMonth(dt);
        break;
      default:
        historyPointDate = dt.AddDays(1.0);
        break;
    }
    return historyPointDate;
  }

  protected virtual DateTime GetPreviousPointDate(DateTime dt)
  {
    DateTime previousPointDate;
    switch (((PXSelectBase<PMDateSensitiveDataRevision>) this.Revision).Current?.Period?.Trim())
    {
      case "Y":
        previousPointDate = this.AddYear(dt, -1);
        break;
      case "Q":
        previousPointDate = this.AddQuarter(dt, -1);
        break;
      case "M":
        previousPointDate = this.AddMonth(dt, -1);
        break;
      default:
        previousPointDate = dt.AddDays(-1.0);
        break;
    }
    return previousPointDate;
  }

  private DateTime AddYear(DateTime dt, int yearsCount = 1)
  {
    return this.GetDateTimeByYear(dt.Year + yearsCount);
  }

  private DateTime AddQuarter(DateTime dt, int quarterCount = 1)
  {
    return this.AddMonth(dt, quarterCount * 3);
  }

  private DateTime AddMonth(DateTime dt, int monthCount = 1)
  {
    DateTime dateTime = dt.AddMonths(monthCount);
    return this.GetDateTimeByMonth(dateTime.Year, dateTime.Month);
  }

  private DateTime GetNextWeek(DateTime endOfWeek)
  {
    int dayOfWeek = (int) CultureInfo.CurrentCulture.Calendar.GetDayOfWeek(endOfWeek);
    DateTime nextWeek = endOfWeek.AddDays(dayOfWeek == 6 ? 7.0 : (double) (6 - dayOfWeek));
    if (nextWeek.Month == endOfWeek.Month)
      return nextWeek;
    DateTime dateTimeByMonth = this.GetDateTimeByMonth(endOfWeek.Year, endOfWeek.Month);
    return !(endOfWeek < dateTimeByMonth) ? nextWeek : dateTimeByMonth;
  }

  private DateTime GetPreviousWeek(DateTime endOfWeek)
  {
    int dayOfWeek = (int) CultureInfo.CurrentCulture.Calendar.GetDayOfWeek(endOfWeek);
    DateTime dateTime = endOfWeek.AddDays((double) (-dayOfWeek - 1));
    return dateTime.Month == endOfWeek.Month ? dateTime : this.AddMonth(endOfWeek, -1);
  }

  private DateTime GetDateTimeByYear(PMHistoryByDate historyItem)
  {
    return this.GetDateTimeByYear(historyItem.Year.Value);
  }

  private DateTime GetDateTimeByYear(int year) => new DateTime(year + 1, 1, 1).AddDays(-1.0);

  private DateTime GetDateTimeByQuarter(PMHistoryByDate historyItem)
  {
    int? nullable = historyItem.Year;
    int year = nullable.Value;
    nullable = historyItem.Quarter;
    int quarter = nullable.Value;
    return this.GetDateTimeByQuarter(year, quarter);
  }

  private DateTime GetDateTimeByQuarter(int year, int quarter)
  {
    return (quarter == 4 ? new DateTime(year + 1, 1, 1) : new DateTime(year, quarter * 3 + 1, 1)).AddDays(-1.0);
  }

  private DateTime GetDateTimeByMonth(PMHistoryByDate historyItem)
  {
    int? nullable = historyItem.Year;
    int year = nullable.Value;
    nullable = historyItem.Month;
    int month = nullable.Value;
    return this.GetDateTimeByMonth(year, month);
  }

  private DateTime GetDateTimeByMonth(int year, int month)
  {
    return (month == 12 ? new DateTime(year + 1, 1, 1) : new DateTime(year, month + 1, 1)).AddDays(-1.0);
  }

  private DateTime GetDateTimeByWeek(PMHistoryByDate historyItem)
  {
    int? nullable = historyItem.Year;
    int year = nullable.Value;
    nullable = historyItem.Month;
    int month = nullable.Value;
    nullable = historyItem.Week;
    int week = nullable.Value;
    return this.GetDateTimeByWeek(year, month, week);
  }

  private DateTime GetDateTimeByWeek(int year, int month, int week)
  {
    DateTime time = new DateTime(year, month, 1);
    int dayOfWeek = (int) CultureInfo.CurrentCulture.Calendar.GetDayOfWeek(time);
    DateTime dateTime = time.AddDays((double) (6 - dayOfWeek)).AddDays((double) ((week - 1) * 7));
    return dateTime.Month == month ? dateTime : this.GetDateTimeByMonth(year, month);
  }

  private void GetDateRange(
    PMDateSensitiveDataRevisionLine record,
    out DateTime? dateFrom,
    out DateTime dateTill)
  {
    dateTill = record.Date.Value;
    ref DateTime? local = ref dateFrom;
    int? pointNumber = record.PointNumber;
    int num = 1;
    DateTime? nullable = pointNumber.GetValueOrDefault() > num & pointNumber.HasValue ? new DateTime?(this.GetPreviousPointDate(this.GetHistoryPointDate(dateTill))) : ((PXSelectBase<PMDateSensitiveDataRevision>) this.Revision).Current.StartDate;
    local = nullable;
  }

  private void GetDefaultDateRange(
    PMDateSensitiveDataRevisionLine record,
    out DateTime? dateFrom,
    out DateTime dateTill)
  {
    dateTill = this.GetHistoryPointDate(record.Date.Value);
    dateFrom = new DateTime?(this.GetPreviousPointDate(dateTill).AddDays(1.0));
  }

  public static double? GetModelCurveValue(
    double min,
    double max,
    double k,
    double a,
    double x0,
    double x)
  {
    double num = Math.Pow(1.0 + Math.Exp(-k * (x - x0)), a);
    return num == 0.0 ? new double?() : new double?(min + (max - min) / num);
  }

  private PMProject GetDocumentProject(PMDateSensitiveDataRevision document)
  {
    return PMProject.PK.Find((PXGraph) this, document.ProjectID);
  }

  private bool IsGroupByInventorySupported(PMDateSensitiveDataRevision document)
  {
    return ((IEnumerable<string>) BudgetLevels.BudgetLevelsWithItem).Contains<string>(this.GetDocumentProject(document)?.CostBudgetLevel);
  }

  private bool IsGroupByCostCodeSupported(PMDateSensitiveDataRevision document)
  {
    return ((IEnumerable<string>) BudgetLevels.BudgetLevelsWithCostCode).Contains<string>(this.GetDocumentProject(document)?.CostBudgetLevel);
  }

  protected bool IsCurrentBudgetLevelMoreDetailedOrEqual(PMDateSensitiveDataRevision document)
  {
    return document.GroupByAccountGroupID.GetValueOrDefault() && document.GroupByProjectTaskID.GetValueOrDefault() && (document.GroupByInventoryID.GetValueOrDefault() || !this.IsGroupByInventorySupported(document)) && (document.GroupByCostCodeID.GetValueOrDefault() || !this.IsGroupByCostCodeSupported(document));
  }

  protected virtual void _(
    Events.FieldUpdated<PMDateSensitiveDataRevision, PMDateSensitiveDataRevision.projectID> e)
  {
    ((PXSelectBase) this.Project).Cache.Clear();
    ((PXSelectBase<PMProject>) this.Project).Current = PXResultset<PMProject>.op_Implicit(((PXSelectBase<PMProject>) this.Project).Select(Array.Empty<object>()));
    if (e.Row.ProjectID.HasValue)
    {
      ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<PMDateSensitiveDataRevision, PMDateSensitiveDataRevision.projectID>>) e).Cache.SetValue<PMCostProjectionByDate.groupByAccountGroupID>((object) e.Row, (object) true);
      ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<PMDateSensitiveDataRevision, PMDateSensitiveDataRevision.projectID>>) e).Cache.SetValue<PMCostProjectionByDate.groupByProjectTaskID>((object) e.Row, (object) true);
      ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<PMDateSensitiveDataRevision, PMDateSensitiveDataRevision.projectID>>) e).Cache.SetValue<PMCostProjectionByDate.groupByInventoryID>((object) e.Row, (object) this.IsGroupByInventorySupported(e.Row));
      ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<PMDateSensitiveDataRevision, PMDateSensitiveDataRevision.projectID>>) e).Cache.SetValue<PMCostProjectionByDate.groupByCostCodeID>((object) e.Row, (object) this.IsGroupByCostCodeSupported(e.Row));
    }
    else
    {
      ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<PMDateSensitiveDataRevision, PMDateSensitiveDataRevision.projectID>>) e).Cache.SetValue<PMCostProjectionByDate.groupByAccountGroupID>((object) e.Row, (object) false);
      ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<PMDateSensitiveDataRevision, PMDateSensitiveDataRevision.projectID>>) e).Cache.SetValue<PMCostProjectionByDate.groupByProjectTaskID>((object) e.Row, (object) false);
      ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<PMDateSensitiveDataRevision, PMDateSensitiveDataRevision.projectID>>) e).Cache.SetValue<PMCostProjectionByDate.groupByInventoryID>((object) e.Row, (object) false);
      ((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<PMDateSensitiveDataRevision, PMDateSensitiveDataRevision.projectID>>) e).Cache.SetValue<PMCostProjectionByDate.groupByCostCodeID>((object) e.Row, (object) false);
    }
  }

  protected virtual void _(
    Events.FieldVerifying<PMDateSensitiveDataRevision, PMDateSensitiveDataRevision.projectID> e)
  {
    if (((Events.FieldVerifyingBase<Events.FieldVerifying<PMDateSensitiveDataRevision, PMDateSensitiveDataRevision.projectID>, PMDateSensitiveDataRevision, object>) e).NewValue == null)
      return;
    this.CheckProjectWarning(((Events.FieldVerifyingBase<Events.FieldVerifying<PMDateSensitiveDataRevision, PMDateSensitiveDataRevision.projectID>, PMDateSensitiveDataRevision, object>) e).NewValue as int?);
    ((Events.FieldVerifyingBase<Events.FieldVerifying<PMDateSensitiveDataRevision, PMDateSensitiveDataRevision.projectID>>) e).Cancel = true;
  }

  protected virtual void CheckProjectWarning(int? projectID)
  {
    PMProject pmProject = PMProject.PK.Find((PXGraph) this, projectID);
    if (pmProject == null || !pmProject.StatusCode.HasValue || !StatusCodeHelper.CheckStatus(pmProject.StatusCode, StatusCodes.DateSensitiveActualsIntroduced))
      return;
    PXSetPropertyException<PMDateSensitiveDataRevision.projectID> propertyException = new PXSetPropertyException<PMDateSensitiveDataRevision.projectID>("Recalculate the project balance by using the Recalculate Project Balance command on the More menu.", (PXErrorLevel) 2);
    ((PXSelectBase) this.Revision).Cache.RaiseExceptionHandling<PMDateSensitiveDataRevision.projectID>((object) ((PXSelectBase<PMDateSensitiveDataRevision>) this.Revision).Current, (object) pmProject.ContractCD, (Exception) propertyException);
  }

  protected virtual void _(Events.RowSelected<PMDateSensitiveDataRevision> e)
  {
    if (e.Row == null)
      return;
    bool hasValue = e.Row.ProjectID.HasValue;
    PXUIFieldAttribute.SetEnabled<PMDateSensitiveDataRevision.accountGroups>(((PXSelectBase) this.Revision).Cache, (object) e.Row, hasValue && !e.Row.AccountGroupID.HasValue);
    PXUIFieldAttribute.SetEnabled<PMDateSensitiveDataRevision.period>(((PXSelectBase) this.Revision).Cache, (object) e.Row, hasValue);
    PXUIFieldAttribute.SetEnabled<PMDateSensitiveDataRevision.startDate>(((PXSelectBase) this.Revision).Cache, (object) e.Row, hasValue);
    PXUIFieldAttribute.SetEnabled<PMDateSensitiveDataRevision.endDate>(((PXSelectBase) this.Revision).Cache, (object) e.Row, hasValue);
    PXUIFieldAttribute.SetEnabled<PMDateSensitiveDataRevision.groupByProjectTaskID>(((PXSelectBase) this.Revision).Cache, (object) e.Row, hasValue);
    PXUIFieldAttribute.SetEnabled<PMDateSensitiveDataRevision.groupByAccountGroupID>(((PXSelectBase) this.Revision).Cache, (object) e.Row, hasValue);
    PXUIFieldAttribute.SetEnabled<PMDateSensitiveDataRevision.groupByInventoryID>(((PXSelectBase) this.Revision).Cache, (object) e.Row, hasValue);
    PXUIFieldAttribute.SetEnabled<PMDateSensitiveDataRevision.groupByCostCodeID>(((PXSelectBase) this.Revision).Cache, (object) e.Row, hasValue);
    bool valueOrDefault1 = e.Row.GroupByProjectTaskID.GetValueOrDefault();
    bool valueOrDefault2 = e.Row.GroupByAccountGroupID.GetValueOrDefault();
    bool valueOrDefault3 = e.Row.GroupByInventoryID.GetValueOrDefault();
    bool valueOrDefault4 = e.Row.GroupByCostCodeID.GetValueOrDefault();
    PXUIFieldAttribute.SetEnabled<PMDateSensitiveDataRevision.projectTaskID>(((PXSelectBase) this.Revision).Cache, (object) e.Row, hasValue | valueOrDefault1);
    PXUIFieldAttribute.SetEnabled<PMDateSensitiveDataRevision.accountGroupID>(((PXSelectBase) this.Revision).Cache, (object) e.Row, hasValue | valueOrDefault2);
    PXUIFieldAttribute.SetEnabled<PMDateSensitiveDataRevision.inventoryID>(((PXSelectBase) this.Revision).Cache, (object) e.Row, hasValue | valueOrDefault3);
    PXUIFieldAttribute.SetEnabled<PMDateSensitiveDataRevision.costCodeID>(((PXSelectBase) this.Revision).Cache, (object) e.Row, hasValue | valueOrDefault4);
    PXUIFieldAttribute.SetVisible<PMDateSensitiveDataRevisionLine.year>(((PXSelectBase) this.Items).Cache, (object) null, hasValue && e.Row.Period != "D");
    PXUIFieldAttribute.SetVisible<PMDateSensitiveDataRevisionLine.quarter>(((PXSelectBase) this.Items).Cache, (object) null, hasValue && (e.Row.Period == "Q" || e.Row.Period == "M"));
    PXUIFieldAttribute.SetVisible<PMDateSensitiveDataRevisionLine.month>(((PXSelectBase) this.Items).Cache, (object) null, hasValue && e.Row.Period == "M");
    PXUIFieldAttribute.SetVisible<PMDateSensitiveDataRevisionLine.projectTaskID>(((PXSelectBase) this.Items).Cache, (object) null, valueOrDefault1);
    PXUIFieldAttribute.SetVisible<PMDateSensitiveDataRevisionLine.accountGroupID>(((PXSelectBase) this.Items).Cache, (object) null, valueOrDefault2);
    PXUIFieldAttribute.SetVisible<PMDateSensitiveDataRevisionLine.inventoryID>(((PXSelectBase) this.Items).Cache, (object) null, valueOrDefault3);
    PXUIFieldAttribute.SetVisible<PMDateSensitiveDataRevisionLine.costCodeID>(((PXSelectBase) this.Items).Cache, (object) null, valueOrDefault4);
    bool flag = this.IsCurrentBudgetLevelMoreDetailedOrEqual(e.Row);
    PXUIFieldAttribute.SetVisible<PMDateSensitiveDataRevisionLine.actualQty>(((PXSelectBase) this.Items).Cache, (object) null, flag);
    PXUIFieldAttribute.SetVisible<PMDateSensitiveDataRevisionLine.actualQtyDiff>(((PXSelectBase) this.Items).Cache, (object) null, flag);
    ((PXAction) this.zoomIn).SetVisible(hasValue);
    ((PXAction) this.zoomOut).SetVisible(hasValue);
    ((PXAction) this.zoomToYear).SetVisible(hasValue);
    ((PXAction) this.zoomToQuarter).SetEnabled(hasValue);
    ((PXAction) this.zoomToMonth).SetVisible(hasValue);
    ((PXAction) this.viewTransactions).SetVisible(hasValue);
  }

  protected virtual void _(
    Events.RowSelected<PMDateSensitiveDataRevisionLine> e)
  {
    if (((PXSelectBase<PMDateSensitiveDataRevision>) this.Revision).Current == null)
      return;
    int num;
    if (((PXSelectBase<PMDateSensitiveDataRevision>) this.Revision).Current.Period == "Y")
    {
      DateTime? nullable = ((PXSelectBase<PMDateSensitiveDataRevision>) this.Revision).Current.StartDate;
      if (!nullable.HasValue)
      {
        nullable = ((PXSelectBase<PMDateSensitiveDataRevision>) this.Revision).Current.EndDate;
        num = !nullable.HasValue ? 1 : 0;
        goto label_5;
      }
    }
    num = 0;
label_5:
    bool flag1 = num != 0;
    bool flag2 = ((PXSelectBase<PMDateSensitiveDataRevision>) this.Revision).Current.Period == "D";
    ((PXAction) this.zoomIn).SetEnabled(e.Row != null && !flag2);
    ((PXAction) this.zoomOut).SetEnabled(!flag1);
    ((PXAction) this.viewTransactions).SetEnabled(e.Row != null);
    ((PXAction) this.showChart).SetEnabled(e.Row != null);
    ((PXAction) this.viewTransactions).SetVisible(e.Row != null);
  }

  [PXUIField(DisplayName = "View Transactions", Enabled = false)]
  [PXButton]
  public virtual IEnumerable ViewTransactions(PXAdapter adapter)
  {
    if (((PXSelectBase<PMDateSensitiveDataRevisionLine>) this.Items).Current != null)
    {
      TransactionInquiry instance = PXGraph.CreateInstance<TransactionInquiry>();
      ((PXSelectBase<TransactionInquiry.TranFilter>) instance.Filter).Current.ProjectID = ((PXSelectBase<PMDateSensitiveDataRevisionLine>) this.Items).Current.ProjectID;
      TransactionInquiry.TranFilter current1 = ((PXSelectBase<TransactionInquiry.TranFilter>) instance.Filter).Current;
      int? nullable1 = ((PXSelectBase<PMDateSensitiveDataRevisionLine>) this.Items).Current.ProjectTaskID;
      int? nullable2 = nullable1 ?? ((PXSelectBase<PMDateSensitiveDataRevision>) this.Revision).Current.ProjectTaskID;
      current1.ProjectTaskID = nullable2;
      TransactionInquiry.TranFilter current2 = ((PXSelectBase<TransactionInquiry.TranFilter>) instance.Filter).Current;
      nullable1 = ((PXSelectBase<PMDateSensitiveDataRevisionLine>) this.Items).Current.AccountGroupID;
      int? nullable3 = nullable1 ?? ((PXSelectBase<PMDateSensitiveDataRevision>) this.Revision).Current.AccountGroupID;
      current2.AccountGroupID = nullable3;
      TransactionInquiry.TranFilter current3 = ((PXSelectBase<TransactionInquiry.TranFilter>) instance.Filter).Current;
      nullable1 = ((PXSelectBase<TransactionInquiry.TranFilter>) instance.Filter).Current.AccountGroupID;
      string accountGroups = !nullable1.HasValue ? ((PXSelectBase<PMDateSensitiveDataRevision>) this.Revision).Current.AccountGroups : (string) null;
      current3.AccountGroupType = accountGroups;
      TransactionInquiry.TranFilter current4 = ((PXSelectBase<TransactionInquiry.TranFilter>) instance.Filter).Current;
      nullable1 = ((PXSelectBase<PMDateSensitiveDataRevisionLine>) this.Items).Current.CostCodeID;
      int? nullable4 = nullable1 ?? ((PXSelectBase<PMDateSensitiveDataRevision>) this.Revision).Current.CostCodeID;
      current4.CostCode = nullable4;
      TransactionInquiry.TranFilter current5 = ((PXSelectBase<TransactionInquiry.TranFilter>) instance.Filter).Current;
      nullable1 = ((PXSelectBase<PMDateSensitiveDataRevisionLine>) this.Items).Current.InventoryID;
      int? nullable5 = nullable1 ?? ((PXSelectBase<PMDateSensitiveDataRevision>) this.Revision).Current.InventoryID;
      current5.InventoryID = nullable5;
      ((PXSelectBase<TransactionInquiry.TranFilter>) instance.Filter).Current.IncludeUnreleased = new bool?(false);
      DateTime? dateFrom;
      DateTime dateTill;
      this.GetDateRange(((PXSelectBase<PMDateSensitiveDataRevisionLine>) this.Items).Current, out dateFrom, out dateTill);
      TransactionInquiry graph = instance;
      DataViewHelper.DataViewFilter[] dataViewFilterArray = new DataViewHelper.DataViewFilter[3];
      // ISSUE: variable of a boxed type
      __Boxed<DateTime?> local1 = (ValueType) dateFrom;
      int num1;
      if (dateFrom.HasValue)
      {
        nullable1 = ((PXSelectBase<PMDateSensitiveDataRevisionLine>) this.Items).Current.PointNumber;
        int num2 = 1;
        num1 = nullable1.GetValueOrDefault() > num2 & nullable1.HasValue ? 1 : 0;
      }
      else
        num1 = 0;
      dataViewFilterArray[0] = DataViewHelper.DataViewFilter.Create("Date", (PXCondition) 2, (object) local1, num1 != 0);
      // ISSUE: variable of a boxed type
      __Boxed<DateTime?> local2 = (ValueType) dateFrom;
      int num3;
      if (dateFrom.HasValue)
      {
        nullable1 = ((PXSelectBase<PMDateSensitiveDataRevisionLine>) this.Items).Current.PointNumber;
        num3 = nullable1.GetValueOrDefault() == 1 ? 1 : 0;
      }
      else
        num3 = 0;
      dataViewFilterArray[1] = DataViewHelper.DataViewFilter.Create("Date", (PXCondition) 3, (object) local2, num3 != 0);
      dataViewFilterArray[2] = DataViewHelper.DataViewFilter.Create("Date", (PXCondition) 5, (object) dateTill);
      ProjectAccountingService.NavigateToView((PXGraph) graph, "Transactions", "View Transactions", dataViewFilterArray);
    }
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Zoom In", Enabled = false)]
  [PXButton]
  public virtual IEnumerable ZoomIn(PXAdapter adapter)
  {
    this.DoZoomIn();
    return adapter.Get();
  }

  protected virtual void DoZoomIn()
  {
    if (((PXSelectBase<PMDateSensitiveDataRevision>) this.Revision).Current == null || ((PXSelectBase<PMDateSensitiveDataRevisionLine>) this.Items).Current == null)
      return;
    string str;
    switch (((PXSelectBase<PMDateSensitiveDataRevision>) this.Revision).Current.Period)
    {
      case "Y":
        str = "Q";
        break;
      case "Q":
        str = "M";
        break;
      case "M":
        str = "D";
        break;
      default:
        str = "D";
        break;
    }
    string periodTo = str;
    if (periodTo == ((PXSelectBase<PMDateSensitiveDataRevision>) this.Revision).Current.Period)
      return;
    this.DoZoomToPeriod(((PXSelectBase<PMDateSensitiveDataRevision>) this.Revision).Current.Period, periodTo);
  }

  [PXUIField(DisplayName = "Zoom Out", Enabled = false)]
  [PXButton]
  public virtual IEnumerable ZoomOut(PXAdapter adapter)
  {
    this.DoZoomOut();
    return adapter.Get();
  }

  protected virtual void DoZoomOut()
  {
    if (((PXSelectBase<PMDateSensitiveDataRevision>) this.Revision).Current == null)
      return;
    if (((PXSelectBase<PMDateSensitiveDataRevision>) this.Revision).Current.Period == "Y")
    {
      ((PXSelectBase) this.Revision).Cache.SetValueExt<PMDateSensitiveDataRevision.startDate>((object) ((PXSelectBase<PMDateSensitiveDataRevision>) this.Revision).Current, (object) null);
      ((PXSelectBase) this.Revision).Cache.SetValueExt<PMDateSensitiveDataRevision.endDate>((object) ((PXSelectBase<PMDateSensitiveDataRevision>) this.Revision).Current, (object) null);
      ((PXSelectBase) this.Revision).Cache.SetValueExt<PMDateSensitiveDataRevision.accountGroupID>((object) ((PXSelectBase<PMDateSensitiveDataRevision>) this.Revision).Current, (object) null);
      ((PXSelectBase) this.Revision).Cache.SetValueExt<PMDateSensitiveDataRevision.projectTaskID>((object) ((PXSelectBase<PMDateSensitiveDataRevision>) this.Revision).Current, (object) null);
      ((PXSelectBase) this.Revision).Cache.SetValueExt<PMDateSensitiveDataRevision.inventoryID>((object) ((PXSelectBase<PMDateSensitiveDataRevision>) this.Revision).Current, (object) null);
      ((PXSelectBase) this.Revision).Cache.SetValueExt<PMDateSensitiveDataRevision.costCodeID>((object) ((PXSelectBase<PMDateSensitiveDataRevision>) this.Revision).Current, (object) null);
    }
    else
    {
      string str;
      switch (((PXSelectBase<PMDateSensitiveDataRevision>) this.Revision).Current.Period)
      {
        case "D":
          str = "M";
          break;
        case "M":
          str = "Q";
          break;
        case "Q":
          str = "Y";
          break;
        default:
          str = "Y";
          break;
      }
      ((PXSelectBase) this.Revision).Cache.SetValueExt<PMDateSensitiveDataRevision.period>((object) ((PXSelectBase<PMDateSensitiveDataRevision>) this.Revision).Current, (object) str);
      if (((PXSelectBase<PMDateSensitiveDataRevisionLine>) this.Items).Current != null)
      {
        DateTime? dateFrom;
        DateTime dateTill;
        this.GetDefaultDateRange(((PXSelectBase<PMDateSensitiveDataRevisionLine>) this.Items).Current, out dateFrom, out dateTill);
        ((PXSelectBase) this.Revision).Cache.SetValueExt<PMDateSensitiveDataRevision.startDate>((object) ((PXSelectBase<PMDateSensitiveDataRevision>) this.Revision).Current, (object) dateFrom);
        ((PXSelectBase) this.Revision).Cache.SetValueExt<PMDateSensitiveDataRevision.endDate>((object) ((PXSelectBase<PMDateSensitiveDataRevision>) this.Revision).Current, (object) dateTill);
      }
      else
      {
        ((PXSelectBase) this.Revision).Cache.SetValueExt<PMDateSensitiveDataRevision.startDate>((object) ((PXSelectBase<PMDateSensitiveDataRevision>) this.Revision).Current, (object) null);
        ((PXSelectBase) this.Revision).Cache.SetValueExt<PMDateSensitiveDataRevision.endDate>((object) ((PXSelectBase<PMDateSensitiveDataRevision>) this.Revision).Current, (object) null);
      }
    }
  }

  [PXUIField(DisplayName = "Zoom To Year", Enabled = false)]
  [PXButton]
  public virtual IEnumerable ZoomToYear(PXAdapter adapter)
  {
    this.DoZoomToPeriod("Y", "Q");
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Zoom To Quarter", Enabled = false)]
  [PXButton]
  public virtual IEnumerable ZoomToQuarter(PXAdapter adapter)
  {
    this.DoZoomToPeriod("Q", "M");
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Zoom To Month", Enabled = false)]
  [PXButton]
  public virtual IEnumerable ZoomToMonth(PXAdapter adapter)
  {
    this.DoZoomToPeriod("M", "D");
    return adapter.Get();
  }

  protected virtual void DoZoomToPeriod(string periodFrom, string periodTo)
  {
    if (((PXSelectBase<PMDateSensitiveDataRevision>) this.Revision).Current == null || ((PXSelectBase<PMDateSensitiveDataRevisionLine>) this.Items).Current == null)
      return;
    ((PXSelectBase) this.Revision).Cache.SetValueExt<PMDateSensitiveDataRevision.period>((object) ((PXSelectBase<PMDateSensitiveDataRevision>) this.Revision).Current, (object) periodFrom);
    DateTime? dateFrom;
    DateTime dateTill;
    this.GetDefaultDateRange(((PXSelectBase<PMDateSensitiveDataRevisionLine>) this.Items).Current, out dateFrom, out dateTill);
    ((PXSelectBase) this.Revision).Cache.SetValueExt<PMDateSensitiveDataRevision.period>((object) ((PXSelectBase<PMDateSensitiveDataRevision>) this.Revision).Current, (object) periodTo);
    ((PXSelectBase) this.Revision).Cache.SetValueExt<PMDateSensitiveDataRevision.startDate>((object) ((PXSelectBase<PMDateSensitiveDataRevision>) this.Revision).Current, (object) dateFrom);
    ((PXSelectBase) this.Revision).Cache.SetValueExt<PMDateSensitiveDataRevision.endDate>((object) ((PXSelectBase<PMDateSensitiveDataRevision>) this.Revision).Current, (object) dateTill);
    ((PXSelectBase) this.Revision).Cache.SetValueExt<PMDateSensitiveDataRevision.accountGroupID>((object) ((PXSelectBase<PMDateSensitiveDataRevision>) this.Revision).Current, (object) ((PXSelectBase<PMDateSensitiveDataRevisionLine>) this.Items).Current.AccountGroupID);
    ((PXSelectBase) this.Revision).Cache.SetValueExt<PMDateSensitiveDataRevision.projectTaskID>((object) ((PXSelectBase<PMDateSensitiveDataRevision>) this.Revision).Current, (object) ((PXSelectBase<PMDateSensitiveDataRevisionLine>) this.Items).Current.ProjectTaskID);
    ((PXSelectBase) this.Revision).Cache.SetValueExt<PMDateSensitiveDataRevision.inventoryID>((object) ((PXSelectBase<PMDateSensitiveDataRevision>) this.Revision).Current, (object) ((PXSelectBase<PMDateSensitiveDataRevisionLine>) this.Items).Current.InventoryID);
    ((PXSelectBase) this.Revision).Cache.SetValueExt<PMDateSensitiveDataRevision.costCodeID>((object) ((PXSelectBase<PMDateSensitiveDataRevision>) this.Revision).Current, (object) ((PXSelectBase<PMDateSensitiveDataRevisionLine>) this.Items).Current.CostCodeID);
  }

  [PXUIField(DisplayName = "Show Chart", Enabled = false)]
  [PXButton]
  public virtual IEnumerable ShowChart(PXAdapter adapter)
  {
    ((PXSelectBase<PMDateSensitiveDataRevisionLine>) this.Items).AskExt();
    return adapter.Get();
  }

  /// <exclude />
  [PXHidden]
  public sealed class GroupingPlaceholderA : 
    BqlPlaceholder.Named<ProjectDateSensitiveCostsInquiry.GroupingPlaceholderA>
  {
  }

  /// <exclude />
  [PXHidden]
  public sealed class GroupingPlaceholderB : 
    BqlPlaceholder.Named<ProjectDateSensitiveCostsInquiry.GroupingPlaceholderB>
  {
  }

  /// <exclude />
  [PXHidden]
  public sealed class GroupingPlaceholderC : 
    BqlPlaceholder.Named<ProjectDateSensitiveCostsInquiry.GroupingPlaceholderC>
  {
  }

  /// <exclude />
  [PXHidden]
  public sealed class GroupingPlaceholderD : 
    BqlPlaceholder.Named<ProjectDateSensitiveCostsInquiry.GroupingPlaceholderD>
  {
  }

  /// <exclude />
  [PXHidden]
  public sealed class GroupingPlaceholderE : 
    BqlPlaceholder.Named<ProjectDateSensitiveCostsInquiry.GroupingPlaceholderE>
  {
  }

  /// <exclude />
  [PXHidden]
  public sealed class GroupingPlaceholderF : 
    BqlPlaceholder.Named<ProjectDateSensitiveCostsInquiry.GroupingPlaceholderF>
  {
  }

  public class BaseDraftDataQueryTemplate : 
    SelectFromBase<PMHistoryByDate, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PMAccountGroup>.On<PMHistoryByDate.FK.AccountGroup>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
    #nullable enable
    PMHistoryByDate.projectID, 
    #nullable disable
    Equal<BqlField<
    #nullable enable
    PMDateSensitiveDataRevision.projectID, IBqlInt>.FromCurrent>>>>, 
    #nullable disable
    And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
    #nullable enable
    PMHistoryByDate.accountGroupID, 
    #nullable disable
    Equal<BqlField<
    #nullable enable
    PMDateSensitiveDataRevision.accountGroupID, IBqlInt>.FromCurrent>>>>>.Or<
    #nullable disable
    BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<
    #nullable enable
    PMDateSensitiveDataRevision.accountGroupID>, 
    #nullable disable
    IsNull>>>>.And<BqlOperand<
    #nullable enable
    PMAccountGroup.type, IBqlString>.IsEqual<
    #nullable disable
    BqlField<
    #nullable enable
    PMDateSensitiveDataRevision.accountGroups, IBqlString>.FromCurrent>>>>>, 
    #nullable disable
    And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
    #nullable enable
    PMHistoryByDate.projectTaskID, 
    #nullable disable
    Equal<BqlField<
    #nullable enable
    PMDateSensitiveDataRevision.projectTaskID, IBqlInt>.FromCurrent>>>>>.Or<
    #nullable disable
    BqlOperand<Current<
    #nullable enable
    PMDateSensitiveDataRevision.projectTaskID>, IBqlInt>.IsNull>>>, 
    #nullable disable
    And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
    #nullable enable
    PMHistoryByDate.inventoryID, 
    #nullable disable
    Equal<BqlField<
    #nullable enable
    PMDateSensitiveDataRevision.inventoryID, IBqlInt>.FromCurrent>>>>>.Or<
    #nullable disable
    BqlOperand<Current<
    #nullable enable
    PMDateSensitiveDataRevision.inventoryID>, IBqlInt>.IsNull>>>, 
    #nullable disable
    And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
    #nullable enable
    PMHistoryByDate.costCodeID, 
    #nullable disable
    Equal<BqlField<
    #nullable enable
    PMDateSensitiveDataRevision.costCodeID, IBqlInt>.FromCurrent>>>>>.Or<
    #nullable disable
    BqlOperand<Current<
    #nullable enable
    PMDateSensitiveDataRevision.costCodeID>, IBqlInt>.IsNull>>>, 
    #nullable disable
    And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
    #nullable enable
    PMHistoryByDate.date, 
    #nullable disable
    GreaterEqual<BqlField<
    #nullable enable
    PMDateSensitiveDataRevision.startDate, IBqlDateTime>.FromCurrent>>>>>.Or<
    #nullable disable
    BqlOperand<Current<
    #nullable enable
    PMDateSensitiveDataRevision.startDate>, IBqlDateTime>.IsNull>>>>.And<
    #nullable disable
    BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
    #nullable enable
    PMHistoryByDate.date, 
    #nullable disable
    LessEqual<BqlField<
    #nullable enable
    PMDateSensitiveDataRevision.endDate, IBqlDateTime>.FromCurrent>>>>>.Or<
    #nullable disable
    BqlOperand<Current<
    #nullable enable
    PMDateSensitiveDataRevision.endDate>, IBqlDateTime>.IsNull>>>.Aggregate<
    #nullable disable
    To<GroupBy<PMHistoryByDate.projectID>, GroupBy<ProjectDateSensitiveCostsInquiry.GroupingPlaceholderA>, GroupBy<ProjectDateSensitiveCostsInquiry.GroupingPlaceholderB>, GroupBy<ProjectDateSensitiveCostsInquiry.GroupingPlaceholderC>, GroupBy<ProjectDateSensitiveCostsInquiry.GroupingPlaceholderD>, GroupBy<ProjectDateSensitiveCostsInquiry.GroupingPlaceholderE>, GroupBy<ProjectDateSensitiveCostsInquiry.GroupingPlaceholderF>, Sum<PMHistoryByDate.actualQty>, Sum<PMHistoryByDate.curyActualAmount>, Sum<PMHistoryByDate.actualAmount>>>.OrderBy<BqlField<ProjectDateSensitiveCostsInquiry.GroupingPlaceholderA, BqlPlaceholder.IBqlAny>.Asc, BqlField<ProjectDateSensitiveCostsInquiry.GroupingPlaceholderB, BqlPlaceholder.IBqlAny>.Asc, BqlField<ProjectDateSensitiveCostsInquiry.GroupingPlaceholderC, BqlPlaceholder.IBqlAny>.Asc, BqlField<ProjectDateSensitiveCostsInquiry.GroupingPlaceholderD, BqlPlaceholder.IBqlAny>.Asc, BqlField<ProjectDateSensitiveCostsInquiry.GroupingPlaceholderE, BqlPlaceholder.IBqlAny>.Asc, BqlField<ProjectDateSensitiveCostsInquiry.GroupingPlaceholderF, BqlPlaceholder.IBqlAny>.Asc>
  {
  }

  public class BaseAccumulatedBeforeStartDateDataQueryTemplate : 
    SelectFromBase<PMHistoryByDate, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PMAccountGroup>.On<PMHistoryByDate.FK.AccountGroup>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
    #nullable enable
    PMHistoryByDate.projectID, 
    #nullable disable
    Equal<BqlField<
    #nullable enable
    PMDateSensitiveDataRevision.projectID, IBqlInt>.FromCurrent>>>>, 
    #nullable disable
    And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
    #nullable enable
    PMHistoryByDate.accountGroupID, 
    #nullable disable
    Equal<BqlField<
    #nullable enable
    PMDateSensitiveDataRevision.accountGroupID, IBqlInt>.FromCurrent>>>>>.Or<
    #nullable disable
    BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<
    #nullable enable
    PMDateSensitiveDataRevision.accountGroupID>, 
    #nullable disable
    IsNull>>>>.And<BqlOperand<
    #nullable enable
    PMAccountGroup.type, IBqlString>.IsEqual<
    #nullable disable
    BqlField<
    #nullable enable
    PMDateSensitiveDataRevision.accountGroups, IBqlString>.FromCurrent>>>>>, 
    #nullable disable
    And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
    #nullable enable
    PMHistoryByDate.projectTaskID, 
    #nullable disable
    Equal<BqlField<
    #nullable enable
    PMDateSensitiveDataRevision.projectTaskID, IBqlInt>.FromCurrent>>>>>.Or<
    #nullable disable
    BqlOperand<Current<
    #nullable enable
    PMDateSensitiveDataRevision.projectTaskID>, IBqlInt>.IsNull>>>, 
    #nullable disable
    And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
    #nullable enable
    PMHistoryByDate.inventoryID, 
    #nullable disable
    Equal<BqlField<
    #nullable enable
    PMDateSensitiveDataRevision.inventoryID, IBqlInt>.FromCurrent>>>>>.Or<
    #nullable disable
    BqlOperand<Current<
    #nullable enable
    PMDateSensitiveDataRevision.inventoryID>, IBqlInt>.IsNull>>>, 
    #nullable disable
    And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
    #nullable enable
    PMHistoryByDate.costCodeID, 
    #nullable disable
    Equal<BqlField<
    #nullable enable
    PMDateSensitiveDataRevision.costCodeID, IBqlInt>.FromCurrent>>>>>.Or<
    #nullable disable
    BqlOperand<Current<
    #nullable enable
    PMDateSensitiveDataRevision.costCodeID>, IBqlInt>.IsNull>>>>.And<
    #nullable disable
    BqlOperand<
    #nullable enable
    PMHistoryByDate.date, IBqlDateTime>.IsLess<
    #nullable disable
    BqlField<
    #nullable enable
    PMDateSensitiveDataRevision.startDate, IBqlDateTime>.FromCurrent>>>.Aggregate<
    #nullable disable
    To<GroupBy<PMHistoryByDate.projectID>, GroupBy<ProjectDateSensitiveCostsInquiry.GroupingPlaceholderA>, GroupBy<ProjectDateSensitiveCostsInquiry.GroupingPlaceholderB>, GroupBy<ProjectDateSensitiveCostsInquiry.GroupingPlaceholderC>, GroupBy<ProjectDateSensitiveCostsInquiry.GroupingPlaceholderD>, Sum<PMHistoryByDate.actualQty>, Sum<PMHistoryByDate.curyActualAmount>, Sum<PMHistoryByDate.actualAmount>>>.OrderBy<BqlField<ProjectDateSensitiveCostsInquiry.GroupingPlaceholderA, BqlPlaceholder.IBqlAny>.Asc, BqlField<ProjectDateSensitiveCostsInquiry.GroupingPlaceholderB, BqlPlaceholder.IBqlAny>.Asc, BqlField<ProjectDateSensitiveCostsInquiry.GroupingPlaceholderC, BqlPlaceholder.IBqlAny>.Asc, BqlField<ProjectDateSensitiveCostsInquiry.GroupingPlaceholderD, BqlPlaceholder.IBqlAny>.Asc>
  {
  }

  protected class CurveStatus
  {
    public DateTime? CurrentPointDate { get; set; }

    public int CurrentPointNumber { get; set; }

    public Decimal ActualQty { get; set; }

    public Decimal CuryActualAmount { get; set; }

    public Decimal ActualAmount { get; set; }

    public PMHistoryByDate AccumulatedBeforeActualGroup { get; set; }

    public Decimal? AccumulatedBeforeActualQty { get; set; }

    public Decimal? AccumulatedBeforeCuryActualAmount { get; set; }

    public Decimal? AccumulatedBeforeActualAmount { get; set; }

    public PMDateSensitiveDataRevisionLine LastPoint { get; set; }
  }
}
