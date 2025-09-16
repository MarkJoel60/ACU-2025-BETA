// Decompiled with JetBrains decompiler
// Type: PX.SM.FiltersGraphHelper`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Compilation;

#nullable disable
namespace PX.SM;

internal class FiltersGraphHelper<T> where T : class, IFilterHeader
{
  private PXGraph _filterMaint;
  private string[] _propertyNames;
  private string[] _propertyLabels;
  private PXFieldState[] _propertyStates;
  private readonly List<string> _numFields = new List<string>();
  private readonly List<string> _dateFields = new List<string>();
  private readonly List<string> _stringFields = new List<string>();
  private T _refreshPropertiesKey;
  private static readonly int[] _commonConditionsValues;
  private static readonly string[] _commonConditionsLabels;
  private static readonly int[] _numberConditionsValues;
  private static readonly string[] _numberConditionsLabels;
  private static readonly int[] _dateConditionsValues;
  private static readonly string[] _dateConditionsLabels;

  static FiltersGraphHelper()
  {
    List<int> intList1 = new List<int>();
    List<string> stringList1 = new List<string>();
    List<int> intList2 = new List<int>();
    List<string> stringList2 = new List<string>();
    List<int> intList3 = new List<int>();
    List<string> stringList3 = new List<string>();
    foreach (KeyValuePair<object, KeyValuePair<string, string>> keyValuePair1 in (IEnumerable<KeyValuePair<object, KeyValuePair<string, string>>>) PXEnumDescriptionAttribute.GetFullInfo(typeof (PXCondition)))
    {
      byte num = (byte) (int) Enum.Parse(typeof (PXCondition), keyValuePair1.Key.ToString());
      KeyValuePair<string, string> keyValuePair2 = keyValuePair1.Value;
      switch (keyValuePair2.Key)
      {
        case "NUMBER":
          intList2.Add((int) num);
          List<string> stringList4 = stringList2;
          keyValuePair2 = keyValuePair1.Value;
          string str1 = keyValuePair2.Value;
          stringList4.Add(str1);
          continue;
        case "DATETIME":
          intList3.Add((int) num);
          List<string> stringList5 = stringList3;
          keyValuePair2 = keyValuePair1.Value;
          string str2 = keyValuePair2.Value;
          stringList5.Add(str2);
          continue;
        case "COMMON":
          intList2.Add((int) num);
          List<string> stringList6 = stringList2;
          keyValuePair2 = keyValuePair1.Value;
          string str3 = keyValuePair2.Value;
          stringList6.Add(str3);
          intList3.Add((int) num);
          List<string> stringList7 = stringList3;
          keyValuePair2 = keyValuePair1.Value;
          string str4 = keyValuePair2.Value;
          stringList7.Add(str4);
          intList1.Add((int) num);
          List<string> stringList8 = stringList1;
          keyValuePair2 = keyValuePair1.Value;
          string str5 = keyValuePair2.Value;
          stringList8.Add(str5);
          continue;
        case "HIDDEN":
          continue;
        default:
          intList1.Add((int) num);
          List<string> stringList9 = stringList1;
          keyValuePair2 = keyValuePair1.Value;
          string str6 = keyValuePair2.Value;
          stringList9.Add(str6);
          continue;
      }
    }
    FiltersGraphHelper<T>._commonConditionsValues = intList1.ToArray();
    FiltersGraphHelper<T>._commonConditionsLabels = stringList1.ToArray();
    FiltersGraphHelper<T>._numberConditionsValues = intList2.ToArray();
    FiltersGraphHelper<T>._numberConditionsLabels = stringList2.ToArray();
    FiltersGraphHelper<T>._dateConditionsValues = intList3.ToArray();
    FiltersGraphHelper<T>._dateConditionsLabels = stringList3.ToArray();
  }

  public FiltersGraphHelper(PXGraph filterMaint) => this._filterMaint = filterMaint;

  internal void InvalidateCache() => this._refreshPropertiesKey = default (T);

  internal string[] GetPropertyNames(T key)
  {
    this.ReadProperties(key);
    return this._propertyNames;
  }

  internal string[] GetPropertyLabels(T key)
  {
    this.ReadProperties(key);
    return this._propertyLabels;
  }

  internal PXFieldState[] GetPropertyStates(T key)
  {
    this.ReadProperties(key);
    return this._propertyStates;
  }

  internal void GetConditions(string dataField, out int[] values, out string[] labels, T key)
  {
    values = new int[0];
    labels = new string[0];
    if (string.IsNullOrEmpty(dataField))
      return;
    this.ReadProperties(key);
    if (this._numFields.Contains(dataField))
    {
      values = FiltersGraphHelper<T>._numberConditionsValues;
      labels = FiltersGraphHelper<T>._numberConditionsLabels;
    }
    else if (this._dateFields.Contains(dataField))
    {
      values = FiltersGraphHelper<T>._dateConditionsValues;
      labels = FiltersGraphHelper<T>._dateConditionsLabels;
    }
    else
    {
      values = FiltersGraphHelper<T>._commonConditionsValues;
      labels = FiltersGraphHelper<T>._commonConditionsLabels;
    }
  }

  protected virtual PXFieldState[] GetFieldsStates(PXGraph graph, T key)
  {
    PXView pxView;
    return graph.Views.TryGetValue(key.ViewName, out pxView) ? PXFieldState.GetFields(graph, pxView.BqlSelect.GetTables(), false) : Array.Empty<PXFieldState>();
  }

  protected virtual bool GraphViewContains(PXGraph graph, T key)
  {
    return graph.Views.ContainsKey(key.ViewName);
  }

  private void ReadProperties(T key)
  {
    if ((object) this._refreshPropertiesKey != null && ((object) this._refreshPropertiesKey == (object) key || string.Equals(this._refreshPropertiesKey.ScreenID, key.ScreenID) && string.Equals(this._refreshPropertiesKey.ViewName, key.ViewName)))
      return;
    this._propertyNames = new string[0];
    this._propertyLabels = new string[0];
    this._propertyStates = new PXFieldState[0];
    this._numFields.Clear();
    this._dateFields.Clear();
    this._stringFields.Clear();
    this._refreshPropertiesKey = key;
    if ((object) this._refreshPropertiesKey == null)
      return;
    PXSiteMapNode screenIdUnsecure = PXSiteMap.Provider.FindSiteMapNodeByScreenIDUnsecure(this._refreshPropertiesKey.ScreenID);
    if (screenIdUnsecure == null)
      return;
    string graphType = screenIdUnsecure.GraphType;
    string viewName = this._refreshPropertiesKey.ViewName;
    if (string.IsNullOrEmpty(graphType) || string.IsNullOrEmpty(viewName))
      return;
    System.Type type = FiltersGraphHelper<T>.GetType(graphType);
    if (type == (System.Type) null || !typeof (PXGraph).IsAssignableFrom(type))
      return;
    PXGraph graph = type == typeof (PXGenericInqGrph) ? (PXGraph) PXGenericInqGrph.CreateInstance(screenIdUnsecure) : PXGraph.CreateInstance(type);
    if (!this.GraphViewContains(graph, this._refreshPropertiesKey))
      return;
    PXFieldState[] fieldsStates = this.GetFieldsStates(graph, key);
    List<PXFieldState> pxFieldStateList = new List<PXFieldState>();
    foreach (PXFieldState pxFieldState in fieldsStates)
    {
      if (pxFieldState.Visibility != PXUIVisibility.Invisible && !string.IsNullOrEmpty(pxFieldState.DisplayName))
      {
        pxFieldStateList.Add(pxFieldState);
        if (!string.IsNullOrEmpty(pxFieldState.ViewName) && !this._filterMaint.Views.ContainsKey(pxFieldState.ViewName))
          this._filterMaint.Views.Add(pxFieldState.ViewName, graph.Views[pxFieldState.ViewName]);
        switch (System.Type.GetTypeCode(pxFieldState.DataType))
        {
          case TypeCode.Char:
          case TypeCode.String:
            this._stringFields.Add(pxFieldState.Name);
            continue;
          case TypeCode.Byte:
          case TypeCode.Int16:
          case TypeCode.UInt16:
          case TypeCode.Int32:
          case TypeCode.UInt32:
          case TypeCode.Int64:
          case TypeCode.UInt64:
          case TypeCode.Single:
          case TypeCode.Double:
          case TypeCode.Decimal:
            this._numFields.Add(pxFieldState.Name);
            continue;
          case TypeCode.DateTime:
            this._dateFields.Add(pxFieldState.Name);
            continue;
          default:
            continue;
        }
      }
    }
    this._propertyNames = new string[pxFieldStateList.Count];
    this._propertyLabels = new string[pxFieldStateList.Count];
    this._propertyStates = new PXFieldState[pxFieldStateList.Count];
    pxFieldStateList.Sort((Comparison<PXFieldState>) ((fs1, fs2) => string.Compare($"{fs1.DisplayName}${fs1.Name}", $"{fs2.DisplayName}${fs2.Name}")));
    for (int index = 0; index < pxFieldStateList.Count; ++index)
    {
      PXFieldState pxFieldState = pxFieldStateList[index];
      this._propertyNames[index] = pxFieldState.Name;
      this._propertyLabels[index] = pxFieldState.DisplayName;
      this._propertyStates[index] = pxFieldState;
    }
    this.MakeEqualLabelsDistinct();
  }

  private void MakeEqualLabelsDistinct()
  {
    int length = this._propertyLabels.Length;
    bool[] flagArray = new bool[length];
    for (int index1 = 0; index1 < length - 1; ++index1)
    {
      if (!flagArray[index1])
      {
        string propertyLabel = this._propertyLabels[index1];
        bool flag = false;
        for (int index2 = index1 + 1; index2 < length; ++index2)
        {
          if (!flagArray[index2] && propertyLabel == this._propertyLabels[index2])
          {
            flag = true;
            flagArray[index2] = true;
            this._propertyLabels[index2] = $"{this._propertyLabels[index2]} ({this._propertyNames[index2]})";
          }
        }
        if (flag)
        {
          flagArray[index1] = true;
          this._propertyLabels[index1] = $"{this._propertyLabels[index1]} ({this._propertyNames[index1]})";
        }
      }
    }
  }

  public bool CheckCondition(object condition, string dataField, T key)
  {
    if (condition == null)
      return true;
    int[] values;
    this.GetConditions(dataField, out values, out string[] _, key);
    int conditionInt = Convert.ToInt32(condition);
    return ((IEnumerable<int>) values).Any<int>((Func<int, bool>) (val => val == conditionInt));
  }

  public bool CheckProperty(object propertyName, T key)
  {
    return ((IEnumerable<string>) this.GetPropertyNames(key)).Any<string>((Func<string, bool>) (value => object.Equals((object) value, propertyName)));
  }

  private static System.Type GetType(string typename)
  {
    if (typename == null)
      return (System.Type) null;
    System.Type type = PXBuildManager.GetType(typename, false);
    return (object) type != null ? type : System.Type.GetType(typename);
  }

  public static IEnumerable GetViews(string graphTypeName)
  {
    foreach (object view in FiltersGraphHelper<T>.GetViews(FiltersGraphHelper<FilterHeader>.GetType(graphTypeName)))
      yield return view;
  }

  public static IEnumerable GetViews(System.Type graphType)
  {
    if (!(graphType == (System.Type) null))
    {
      System.Type[] first = new System.Type[1]{ graphType };
      foreach (System.Type type in ((IEnumerable<System.Type>) first).Union<System.Type>((IEnumerable<System.Type>) PXGraph._GetExtensions(graphType)))
      {
        System.Reflection.FieldInfo[] fieldInfoArray = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetField);
        for (int index = 0; index < fieldInfoArray.Length; ++index)
        {
          System.Reflection.FieldInfo fieldInfo = fieldInfoArray[index];
          if (fieldInfo.FieldType.IsSubclassOf(typeof (PXSelectBase)) && fieldInfo.IsDefined(typeof (PXFilterableAttribute), true))
          {
            FilterMaint.ViewInfo view = new FilterMaint.ViewInfo()
            {
              Name = fieldInfo.Name,
              DisplayName = fieldInfo.Name
            };
            object[] customAttributes = fieldInfo.GetCustomAttributes(typeof (PXViewNameAttribute), true);
            if (customAttributes.Length != 0)
              view.DisplayName = ((PXNameAttribute) customAttributes[0]).Name;
            yield return (object) view;
          }
        }
        fieldInfoArray = (System.Reflection.FieldInfo[]) null;
      }
    }
  }

  internal static string[] LocalizeFilterConditionLabels(string[] labels)
  {
    if (labels == null)
      return (string[]) null;
    string[] strArray = new string[labels.Length];
    for (int index = 0; index < labels.Length; ++index)
      strArray[index] = PXLocalizer.Localize(labels[index], typeof (InfoMessages).FullName);
    return strArray;
  }

  internal static bool IsConditionWithTwoValue(int? condition)
  {
    return condition.HasValue && condition.Value == 10;
  }

  internal static bool IsConditionWithValue(int? condition)
  {
    if (!condition.HasValue)
      return true;
    PXCondition pxCondition = (PXCondition) condition.Value;
    switch (pxCondition)
    {
      case PXCondition.TODAY:
      case PXCondition.OVERDUE:
      case PXCondition.TODAY_OVERDUE:
      case PXCondition.TOMMOROW:
      case PXCondition.THIS_WEEK:
      case PXCondition.NEXT_WEEK:
      case PXCondition.THIS_MONTH:
        return false;
      default:
        return pxCondition != PXCondition.NEXT_MONTH;
    }
  }
}
