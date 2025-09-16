// Decompiled with JetBrains decompiler
// Type: PX.Data.Reports.ReportParameterCache
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.Reports.DAC;
using PX.Reports;
using PX.Reports.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.UI.Design;

#nullable disable
namespace PX.Data.Reports;

public class ReportParameterCache : PXCache<ReportParameter>
{
  private readonly Dictionary<string, System.Type> _parametersCacheTypes;
  private readonly Dictionary<string, PXFieldState> _parametersStates;
  private static readonly Dictionary<string, System.Type> EmptyParametersCacheTypes = new Dictionary<string, System.Type>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
  private static readonly Dictionary<string, PXFieldState> EmptyParametersStates = new Dictionary<string, PXFieldState>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
  private static readonly Lazy<string[]> RealFields = new Lazy<string[]>((Func<string[]>) (() => ((IEnumerable<PropertyInfo>) typeof (ReportParameter).GetProperties(BindingFlags.Instance | BindingFlags.Public)).Select<PropertyInfo, string>((Func<PropertyInfo, string>) (p => p.Name)).ToArray<string>()));
  private const string DescriptionSuffix = "_description";

  private ReportMaint ReportGraph => (ReportMaint) this.Graph;

  public ReportParameterCache(PXGraph graph)
    : base(graph)
  {
    if (this.ReportGraph.ReportDescription == null)
    {
      this._parametersStates = ReportParameterCache.EmptyParametersStates;
      this._parametersCacheTypes = ReportParameterCache.EmptyParametersCacheTypes;
    }
    else
    {
      this._parametersStates = new Dictionary<string, PXFieldState>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
      this._parametersCacheTypes = new Dictionary<string, System.Type>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
      graph.UnattendedMode = false;
      this.FillParametersSettings();
      graph.UnattendedMode = true;
    }
  }

  private void FillParametersSettings()
  {
    IDataNavigator navigator = this.ReportGraph.GetNavigator();
    ReportParameter data = (ReportParameter) (this.Current ?? this.Insert());
    foreach (ReportParameter parameter in (List<ReportParameter>) this.ReportGraph.ReportDescription.Parameters)
    {
      parameter.Process(navigator, this.ReportGraph.ReportDescription);
      if (!(parameter.ProcessedView is PXFieldState pxFieldState1))
        pxFieldState1 = this.GetParameterState(parameter);
      PXFieldState pxFieldState2 = pxFieldState1;
      object processedDefault = parameter.ProcessedDefault;
      string str = parameter.ProcessedMask ?? parameter.InputMask;
      string name = parameter.Name;
      if (!data.Values.ContainsKey(name))
        this.SetValue((object) data, name, processedDefault);
      pxFieldState2.DisplayName = parameter.Prompt;
      pxFieldState2.Required = parameter.Required;
      pxFieldState2.Name = name;
      pxFieldState2.DefaultValue = processedDefault;
      pxFieldState2.Value = processedDefault;
      pxFieldState2.Visible = pxFieldState2.Visible && parameter.IsVisible() && !string.IsNullOrEmpty(parameter.Prompt);
      pxFieldState2.Enabled = true;
      if (!string.IsNullOrEmpty(str) && pxFieldState2 is PXStringState pxStringState)
        pxStringState.InputMask = str;
      this.Fields.Add(name);
      if (!string.IsNullOrEmpty(pxFieldState2.DescriptionName))
        this.Fields.Add(name + "_description");
      this._parametersStates[name] = pxFieldState2;
      string key = pxFieldState2.ViewName ?? parameter.ViewName;
      if (!string.IsNullOrEmpty(key) && !key.StartsWith("="))
        this._parametersCacheTypes[name] = this.ReportGraph.Views[key].CacheType;
    }
  }

  private PXFieldState GetParameterState(ReportParameter par)
  {
    if (par.ProcessedView is PXFieldState processedView)
      return processedView;
    if (((List<ParameterValue>) par.ValidValues).Count > 0)
      return PXStringState.CreateInstance(par.ProcessedDefault, new int?(), new bool?(), par.Name, new bool?(), new int?(), par.ProcessedMask, ((IEnumerable<ParameterValue>) par.ValidValues).Select<ParameterValue, string>((Func<ParameterValue, string>) (v => v.Value)).ToArray<string>(), ((IEnumerable<ParameterValue>) par.ValidValues).Select<ParameterValue, string>((Func<ParameterValue, string>) (v => v.Label)).ToArray<string>(), new bool?(), par.ProcessedDefault?.ToString());
    if (!(par.ProcessedView is string str1))
      str1 = par.ViewName;
    string str2 = str1;
    System.Type type;
    switch ((int) par.Type)
    {
      case 0:
        type = typeof (bool);
        break;
      case 1:
        type = typeof (System.DateTime);
        break;
      case 2:
        type = typeof (float);
        break;
      case 3:
        type = typeof (int);
        break;
      case 4:
        type = typeof (string);
        break;
      default:
        throw new ArgumentOutOfRangeException();
    }
    System.Type dataType = type;
    PXFieldState parameterState1 = dataType == typeof (string) ? PXStringState.CreateInstance(par.ProcessedDefault, new int?(), new bool?(), par.Name, new bool?(), new int?(), par.ProcessedMask, (string[]) null, (string[]) null, new bool?(), par.ProcessedDefault?.ToString()) : PXFieldState.CreateInstance((object) null, dataType);
    PXView pxView;
    if (string.IsNullOrEmpty(str2) || !this.ReportGraph.Views.TryGetOrCreateValue(str2, out pxView))
      return parameterState1;
    PXFieldState[] fields = PXFieldState.GetFields(pxView.Graph, pxView.BqlSelect.GetTables(), false);
    List<string> stringList1 = new List<string>();
    List<string> stringList2 = new List<string>();
    foreach (PXFieldState parameterState2 in fields)
    {
      if (string.Equals(parameterState2.ViewName, str2, StringComparison.OrdinalIgnoreCase))
        return parameterState2;
      if ((parameterState2.Visibility & PXUIVisibility.SelectorVisible) == PXUIVisibility.SelectorVisible)
      {
        stringList1.Add(parameterState2.Name);
        stringList2.Add(parameterState2.DisplayName);
        if (string.IsNullOrEmpty(parameterState1.ValueField))
          parameterState1.ValueField = parameterState2.ValueField;
      }
    }
    parameterState1.FieldList = stringList1.ToArray();
    parameterState1.HeaderList = stringList2.ToArray();
    parameterState1.ViewName = str2;
    return parameterState1;
  }

  public override PXFieldCollection Fields
  {
    get
    {
      if (this._Fields != null)
        return this._Fields;
      Dictionary<string, int> dict = new Dictionary<string, int>((IDictionary<string, int>) this._FieldsMap, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
      this._Fields = new PXFieldCollection((IEnumerable<string>) this._ClassFields, dict);
      foreach (string key in ReportParameterCache.RealFields.Value)
      {
        this._Fields.Remove(key);
        dict.Remove(key);
      }
      return this._Fields;
    }
  }

  public override object GetValue(object data, string fieldName)
  {
    object obj;
    return (data is ReportParameter reportParameter ? reportParameter.Values : (IDictionary<string, object>) null) != null && reportParameter.Values.TryGetValue(fieldName, out obj) ? obj : base.GetValue(data, fieldName);
  }

  public override void SetValue(object data, string fieldName, object value)
  {
    if ((data is ReportParameter reportParameter ? reportParameter.Values : (IDictionary<string, object>) null) != null)
      reportParameter.Values[fieldName] = value;
    else
      base.SetValue(data, fieldName, value);
  }

  public override object GetStateExt(object data, string fieldName)
  {
    PXFieldState state;
    if (!this._parametersStates.TryGetValue(fieldName, out state))
    {
      if (!fieldName.EndsWith("_description"))
        return base.GetStateExt(data, fieldName);
      if (!this._parametersStates.TryGetValue(fieldName.Substring(0, fieldName.IndexOf("_description")), out state) || string.IsNullOrEmpty(state.DescriptionName))
        return base.GetStateExt(data, fieldName);
      string descriptionName = state.DescriptionName;
      PXView view = this.Graph.Views[state.ViewName];
      return !this._parametersStates.TryGetValue(descriptionName, out state) ? view.Cache.GetStateExt(data is ReportParameter ? (object) null : data, descriptionName) : (object) state;
    }
    PXFieldState pxFieldState;
    switch (state)
    {
      case PXBranchSelectorState branchSelectorState:
        pxFieldState = (PXFieldState) new PXBranchSelectorState((object) state)
        {
          DACName = branchSelectorState.DACName
        };
        break;
      case PXStringState pxStringState2:
        PXStringState pxStringState1 = pxStringState2;
        int? length = new int?(pxStringState2.Length);
        bool? isUnicode = new bool?(pxStringState2.IsUnicode);
        string name = pxStringState2.Name;
        bool? isKey = new bool?(false);
        bool? required1 = pxStringState2.Required;
        bool flag = true;
        int? required2 = new int?(required1.GetValueOrDefault() == flag & required1.HasValue ? 1 : 0);
        string inputMask = pxStringState2.InputMask;
        string[] allowedValues = pxStringState2.AllowedValues;
        string[] allowedLabels = pxStringState2.AllowedLabels;
        bool? exclusiveValues = new bool?(pxStringState2.ExclusiveValues);
        string defaultValue = pxStringState2.DefaultValue?.ToString();
        pxFieldState = PXStringState.CreateInstance((object) pxStringState1, length, isUnicode, name, isKey, required2, inputMask, allowedValues, allowedLabels, exclusiveValues, defaultValue);
        break;
      default:
        pxFieldState = PXFieldState.CreateInstance((IDataSourceFieldSchema) state);
        break;
    }
    PXFieldState stateExt = pxFieldState;
    if (!(data is ReportParameter reportParameter))
      return (object) stateExt;
    object obj;
    if (!reportParameter.Values.TryGetValue(fieldName, out obj))
      return (object) null;
    if (obj == state.Value)
      return (object) state;
    stateExt.Value = obj;
    return (object) stateExt;
  }

  internal override System.Type GetFieldType(string paramName)
  {
    return this.MapCall<System.Type>(paramName, (ReportParameterCache.CallDelegate<System.Type>) ((cache, field) => cache.GetFieldType(field)), new ReportParameterCache.FailureDelegate<System.Type>(((PXCache<ReportParameter>) this).GetFieldType));
  }

  private T MapCall<T>(
    string paramName,
    ReportParameterCache.CallDelegate<T> callFunc,
    ReportParameterCache.FailureDelegate<T> failureFunc)
  {
    System.Type key;
    if (string.IsNullOrEmpty(paramName) || !this._parametersCacheTypes.TryGetValue(paramName, out key))
      return failureFunc(paramName);
    PXCache cach = this.ReportGraph.Caches[key];
    return callFunc(cach, paramName);
  }

  public delegate T CallDelegate<out T>(PXCache actualCache, string schemaField);

  public delegate T FailureDelegate<out T>(string paramName);
}
