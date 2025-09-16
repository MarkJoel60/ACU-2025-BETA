// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.RMReportUnitExpansion`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CS;
using PX.Data;
using PX.Reports.ARm;
using System;
using System.Collections.Generic;
using System.Text;

#nullable disable
namespace PX.Objects.CS;

public static class RMReportUnitExpansion<T>
{
  public static List<ARmUnit> ExpandUnit(
    RMReportReader report,
    RMDataSource ds,
    ARmUnit unit,
    object startKey,
    object endKey,
    Func<ARmDataSet, List<T>> fetchRangePredicate,
    Func<T, string> unitCodePredicate,
    Func<T, string> unitDescriptionPredicate,
    Action<T, string> applyWildcardToItemAction)
  {
    string nonExpandingWildcard = (string) null;
    RMReportUnitExpansion<T>.PreProcessNonExpandingWildcardChar(unit.DataSet, startKey, out nonExpandingWildcard);
    ARmDataSet target = new ARmDataSet();
    RMReportWildcard.ConcatenateRangeWithDataSet(target, unit.DataSet, startKey, endKey, (MergingMode) 0);
    List<T> list = fetchRangePredicate(target);
    if (nonExpandingWildcard != null)
      list = RMReportUnitExpansion<T>.ReduceListByNonExpandingWildcard(nonExpandingWildcard, list, (Func<T, string>) (u => unitCodePredicate(u)), (Action<T, string>) ((u, mv) => applyWildcardToItemAction(u, mv)));
    switch (ds.RowDescription.Trim().ToUpper())
    {
      case "CD":
        list.Sort((Comparison<T>) ((x, y) => (unitCodePredicate(x) + unitDescriptionPredicate(x)).CompareTo(unitCodePredicate(y) + unitDescriptionPredicate(y))));
        break;
      case "DC":
        list.Sort((Comparison<T>) ((x, y) => (unitDescriptionPredicate(x) + unitCodePredicate(x)).CompareTo(unitDescriptionPredicate(y) + unitCodePredicate(y))));
        break;
      case "D":
        list.Sort((Comparison<T>) ((x, y) => unitDescriptionPredicate(x).CompareTo(unitDescriptionPredicate(y))));
        break;
      default:
        list.Sort((Comparison<T>) ((x, y) => unitCodePredicate(x).CompareTo(unitCodePredicate(y))));
        break;
    }
    List<ARmUnit> armUnitList = new List<ARmUnit>();
    int num = 0;
    foreach (T obj in list)
    {
      ++num;
      ARmUnit armUnit = new ARmUnit();
      report.FillDataSource(ds, armUnit.DataSet, ((PXSelectBase<RMReport>) report.Report).Current.Type);
      armUnit.DataSet[startKey] = (object) unitCodePredicate(obj);
      armUnit.DataSet[endKey] = (object) null;
      armUnit.Code = unit.Code + num.ToString("D5");
      switch (ds.RowDescription.Trim())
      {
        case "CD":
          armUnit.Description = $"{unitCodePredicate(obj).Trim()}{"-"}{unitDescriptionPredicate(obj)}";
          break;
        case "DC":
          armUnit.Description = $"{unitDescriptionPredicate(obj)}{"-"}{unitCodePredicate(obj).Trim()}";
          break;
        case "D":
          armUnit.Description = unitDescriptionPredicate(obj);
          break;
        default:
          armUnit.Description = unitCodePredicate(obj).Trim();
          break;
      }
      armUnit.Formula = unit.Formula;
      armUnit.PrintingGroup = unit.PrintingGroup;
      armUnitList.Add(armUnit);
    }
    return armUnitList;
  }

  private static void PreProcessNonExpandingWildcardChar(
    ARmDataSet dataSet,
    object key,
    out string nonExpandingWildcard)
  {
    bool flag = false;
    if (!(dataSet[key] is string str1))
      str1 = "";
    string str2 = str1;
    StringBuilder stringBuilder = new StringBuilder(str2.Length);
    for (int index = 0; index < str2.Length; ++index)
    {
      if (str2[index] == '*')
      {
        stringBuilder.Append('_');
        flag = true;
      }
      else
        stringBuilder.Append(str2[index]);
    }
    if (flag)
    {
      dataSet[key] = (object) stringBuilder.ToString();
      nonExpandingWildcard = str2;
    }
    else
      nonExpandingWildcard = (string) null;
  }

  private static List<T> ReduceListByNonExpandingWildcard(
    string nonExpandingWildcard,
    List<T> list,
    Func<T, string> value,
    Action<T, string> applyWildcardToItemAction)
  {
    HashSet<string> stringSet = new HashSet<string>();
    List<T> objList = new List<T>();
    foreach (T obj in list)
    {
      string str = RMReportUnitExpansion<T>.MaskStringWithWildcard(value(obj), nonExpandingWildcard);
      if (!stringSet.Contains(str))
      {
        stringSet.Add(str);
        applyWildcardToItemAction(obj, str);
        objList.Add(obj);
      }
    }
    return objList;
  }

  private static string MaskStringWithWildcard(string value, string nonExpandingWildcard)
  {
    if (value.Length > nonExpandingWildcard.Length)
      throw new ArgumentException("The wildcard string is smaller than the string to be masked.");
    StringBuilder stringBuilder = new StringBuilder(value.Length);
    for (int index = 0; index < value.Length; ++index)
    {
      if (nonExpandingWildcard[index] == '*')
        stringBuilder.Append('_');
      else
        stringBuilder.Append(value[index]);
    }
    return stringBuilder.ToString();
  }
}
