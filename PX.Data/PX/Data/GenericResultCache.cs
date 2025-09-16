// Decompiled with JetBrains decompiler
// Type: PX.Data.GenericResultCache
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.Description.GI;
using PX.Data.GenericInquiry;
using PX.Data.Maintenance.GI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

#nullable disable
namespace PX.Data;

/// <exclude />
public class GenericResultCache : GICache<GenericResult>
{
  private Dictionary<int, GenericResultCache.KeyDescriptor> _KeysMap;
  private static readonly IReadOnlyList<string> BasePlainDacFields = (IReadOnlyList<string>) ((IEnumerable<PropertyInfo>) typeof (GenericResult).GetProperties(BindingFlags.Instance | BindingFlags.Public)).Select<PropertyInfo, string>((Func<PropertyInfo, string>) (prop => prop.Name)).ToArray<string>();
  private readonly int _plainFieldsCount;
  private static readonly string FormulaAlias = typeof (GenericResult).Name;
  private const string LineStartPattern = "^";
  private const string GuidPattern = "[0-9a-f]{32}";
  private const string NumberingPattern = "[0-9]{4}";
  private static readonly Regex FormulaFieldRegex = new Regex("^Formula[0-9a-f]{32}", RegexOptions.IgnoreCase | RegexOptions.Compiled);
  private static readonly Regex CountFieldRegex = new Regex("^Count[0-9a-f]{32}", RegexOptions.IgnoreCase | RegexOptions.Compiled);
  private static readonly Regex StringAggFieldRegex = new Regex("^StringAgg[0-9a-f]{32}", RegexOptions.IgnoreCase | RegexOptions.Compiled);
  private static readonly Regex AggregateFieldRegex = new Regex("(^Aggr[0-9a-f]{32})|(^Aggr[0-9]{4})", RegexOptions.IgnoreCase | RegexOptions.Compiled);
  private readonly Dictionary<System.Type, List<string>> _eventSubscriberAttributes = new Dictionary<System.Type, List<string>>();
  private readonly List<string> _descriptionFields = new List<string>();
  private readonly List<string> _navigationFields = new List<string>();

  internal ISet<string> PlainDacFields { get; private set; }

  public GenericResultCache(PXGraph graph)
    : base(graph)
  {
    this._FieldsMap = new Dictionary<string, int>((IDictionary<string, int>) this._FieldsMap, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    this._ClassFields = new List<string>((IEnumerable<string>) this._ClassFields);
    HashSet<string> stringSet = new HashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    EnumerableExtensions.AddRange<string>((ISet<string>) stringSet, (IEnumerable<string>) GenericResultCache.BasePlainDacFields);
    EnumerableExtensions.AddRange<string>((ISet<string>) stringSet, ((IEnumerable<System.Type>) this.GetExtensionTypes()).Select<System.Type, IEnumerable<string>>((Func<System.Type, IEnumerable<string>>) (t => ((IEnumerable<PropertyInfo>) t.GetProperties(BindingFlags.Instance | BindingFlags.Public)).Select<PropertyInfo, string>((Func<PropertyInfo, string>) (prop => prop.Name)))).SelectMany<IEnumerable<string>, string>((Func<IEnumerable<string>, IEnumerable<string>>) (names => names)));
    this.PlainDacFields = (ISet<string>) stringSet;
    this._plainFieldsCount = stringSet.Count;
  }

  private object MapCall(
    object data,
    string fieldName,
    GenericResultCache.CallDelegate<object> callFunc,
    GenericResultCache.FailureDelegate<object> failureFunc,
    bool createInstance = false)
  {
    return this.MapCall<object>(data, fieldName, callFunc, failureFunc, createInstance);
  }

  protected PXCache GetCacheByAlias(string alias)
  {
    if (string.IsNullOrEmpty(alias) || this._GenInqGraph.Description == null)
      return (PXCache) null;
    PXTable pxTable;
    if (!this._GenInqGraph.BaseQueryDescription.Tables.TryGetValue(alias, out pxTable))
      return (PXCache) null;
    System.Type cacheType = pxTable.CacheType;
    return (pxTable.Graph ?? this._GenInqGraph).Caches[cacheType];
  }

  private T MapCall<T>(
    object data,
    string fieldName,
    GenericResultCache.CallDelegate<T> callFunc,
    GenericResultCache.FailureDelegate<T> failureFunc,
    bool createInstance = false)
  {
    if (!string.IsNullOrEmpty(fieldName))
    {
      string[] strArray = this.SplitField(fieldName);
      if (strArray.Length > 1)
      {
        string str = strArray[0];
        string field = strArray[1];
        PXCache cacheByAlias = this.GetCacheByAlias(str);
        if (cacheByAlias != null)
        {
          object actualRow = (object) null;
          if (data is GenericResult genericResult)
          {
            if (genericResult.Values == null)
              genericResult.Values = (IDictionary<string, object>) new Dictionary<string, object>();
            if (genericResult.Values.ContainsKey(str))
              actualRow = genericResult.Values[str];
            else if (createInstance)
              actualRow = genericResult.Values[str] = cacheByAlias.CreateInstance();
          }
          return callFunc(cacheByAlias, actualRow, field);
        }
      }
    }
    return failureFunc(data, fieldName);
  }

  protected string[] SplitField(string fieldName)
  {
    int num = fieldName.LastIndexOf('.');
    if (num > -1 && num < fieldName.Length - 1)
      fieldName = fieldName.Substring(num + 1);
    return fieldName.Split('_', 2);
  }

  internal void NormalizeUpdated()
  {
    if (this._GenInqGraph.Description == null)
      return;
    this.Items.Normalize((GenericResult) null, PXEntryStatus.Updated);
  }

  internal void AdjustState(PXFieldState state)
  {
    if (state == null)
      return;
    state.Nullable = true;
    state.Required = new bool?(false);
    state.Enabled = false;
  }

  /// <summary>
  /// Sets current for item cache based on a field name (aliased).
  /// </summary>
  /// <param name="fieldName">Field name in "Alias_Field" format.</param>
  /// <remarks>
  /// Suitable for a case when GI contains self-joins - in this situation
  /// there is only one PXCache and therefore only one Current for two records of the same type with different aliases.
  /// </remarks>
  internal void SetCurrentForField(string fieldName)
  {
    if (string.IsNullOrEmpty(fieldName))
      return;
    PXCache fieldCache = this.GetFieldCache(fieldName);
    if (fieldCache == null)
      return;
    PXCache cach = this._GenInqGraph.Caches[fieldCache.GetItemType()];
    cach.DisableCloneAttributes = true;
    cach.Current = fieldCache.Current;
    cach.DisableCloneAttributes = false;
  }

  private PXCache GetFieldCache(string fieldName)
  {
    return this.MapCall<PXCache>((object) null, fieldName, (GenericResultCache.CallDelegate<PXCache>) ((cache, _, field) => !(cache is GenericResultCache genericResultCache) ? cache : genericResultCache.GetFieldCache(field)), (GenericResultCache.FailureDelegate<PXCache>) ((_1, _2) => (PXCache) null));
  }

  private object AdjustState(object value)
  {
    if (value is PXFieldState state)
      this.AdjustState(state);
    return value;
  }

  public override object Current
  {
    get => base.Current;
    set
    {
      base.Current = value;
      if (this._GenInqGraph.Design == null)
        return;
      GenericResult genericResult = value as GenericResult;
      foreach (PXTable pxTable in this._GenInqGraph.BaseQueryDescription.Tables.Values)
      {
        object obj = (object) null;
        if (genericResult == null || genericResult.Values == null || genericResult.Values.TryGetValue(pxTable.Alias, out obj))
        {
          PXCache cach = (pxTable.Graph ?? this._GenInqGraph).Caches[pxTable.CacheType];
          if (cach.Current != null && cach.GetStatus(cach.Current) == PXEntryStatus.Notchanged)
            cach.Remove(cach.Current);
          cach.NeverCloneAttributes = true;
          cach.Current = obj;
          cach.NeverCloneAttributes = false;
        }
      }
    }
  }

  public override void SetValue(object data, string fieldName, object value)
  {
    this.MapCall(data, fieldName, (GenericResultCache.CallDelegate<object>) ((cache, row, field) =>
    {
      if (GenericResultCache.IsFormulaField(field) || GenericResultCache.IsCountField(field) || GenericResultCache.IsStringAggField(field) || GenericResultCache.IsAggregateField(field))
        this.SetExtFieldValue(data, fieldName, value);
      else
        cache.SetValue(row, field, value);
      return (object) null;
    }), (GenericResultCache.FailureDelegate<object>) ((row, field) =>
    {
      base.SetValue(data, fieldName, value);
      return (object) null;
    }), true);
  }

  public override void SetAltered(string fieldName, bool isAltered)
  {
    this.MapCall((object) null, fieldName, (GenericResultCache.CallDelegate<object>) ((cache, row, field) =>
    {
      if (GenericResultCache.IsFormulaField(field) || GenericResultCache.IsCountField(field) || GenericResultCache.IsStringAggField(field))
        base.SetAltered(fieldName, isAltered);
      else
        cache.SetAltered(field, isAltered);
      return (object) null;
    }), (GenericResultCache.FailureDelegate<object>) ((row, field) =>
    {
      base.SetAltered(field, isAltered);
      return (object) null;
    }));
  }

  private static bool IsFormulaField(string fieldName)
  {
    return fieldName != null && GenericResultCache.FormulaFieldRegex.IsMatch(fieldName);
  }

  private static bool IsCountField(string fieldName)
  {
    return fieldName != null && GenericResultCache.CountFieldRegex.IsMatch(fieldName);
  }

  private static bool IsStringAggField(string fieldName)
  {
    return fieldName != null && GenericResultCache.StringAggFieldRegex.IsMatch(fieldName);
  }

  private static bool IsAggregateField(string fieldName)
  {
    return fieldName != null && GenericResultCache.AggregateFieldRegex.IsMatch(fieldName);
  }

  /// <summary>Returns external (formula/count) field's value.</summary>
  internal object GetExtFieldValue(object row, string fieldName)
  {
    object obj;
    if (row is GenericResult genericResult && genericResult.Values != null && genericResult.Values.TryGetValue(GenericResultCache.FormulaAlias, out obj))
    {
      Dictionary<string, object> dictionary = (Dictionary<string, object>) obj;
      object extFieldValue;
      if (dictionary != null && dictionary.TryGetValue(fieldName, out extFieldValue))
        return extFieldValue;
    }
    return (object) null;
  }

  internal void SetExtFieldValue(object row, string fieldName, object value)
  {
    if (!(row is GenericResult genericResult))
      return;
    if (genericResult.Values == null)
      genericResult.Values = (IDictionary<string, object>) new Dictionary<string, object>();
    Dictionary<string, object> dictionary = (Dictionary<string, object>) null;
    object obj;
    if (genericResult.Values.TryGetValue(GenericResultCache.FormulaAlias, out obj))
      dictionary = (Dictionary<string, object>) obj;
    if (dictionary == null)
      genericResult.Values[GenericResultCache.FormulaAlias] = (object) (dictionary = new Dictionary<string, object>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase));
    dictionary[fieldName] = value;
  }

  public override object GetValue(object data, string fieldName)
  {
    return this.MapCall(data, fieldName, (GenericResultCache.CallDelegate<object>) ((cache, row, field) => GenericResultCache.IsFormulaField(field) || GenericResultCache.IsCountField(field) || GenericResultCache.IsStringAggField(field) ? this.GetExtFieldValue(data, fieldName) : cache.GetValue(row, field)), (GenericResultCache.FailureDelegate<object>) ((row, field) => base.GetValue(row, field)));
  }

  public override object GetValueInt(object data, string fieldName)
  {
    return !this.FieldSelectingEvents.ContainsKey(fieldName.ToLowerInvariant()) ? this.AdjustState(this.MapCall(data, fieldName, (GenericResultCache.CallDelegate<object>) ((cache, row, field) => cache.GetValueInt(row, field)), (GenericResultCache.FailureDelegate<object>) ((row, field) => base.GetValueInt(data, fieldName)))) : base.GetValueInt(data, fieldName);
  }

  public override object GetValueExt(object data, string fieldName)
  {
    return !this.FieldSelectingEvents.ContainsKey(fieldName.ToLowerInvariant()) ? this.AdjustState(this.MapCall(data, fieldName, (GenericResultCache.CallDelegate<object>) ((cache, row, field) => cache.GetValueExt(row, field)), (GenericResultCache.FailureDelegate<object>) ((row, field) => base.GetValueExt(data, fieldName)))) : base.GetValueExt(data, fieldName);
  }

  internal override object GetStateInt(object data, string fieldName)
  {
    return !this.FieldSelectingEvents.ContainsKey(fieldName.ToLowerInvariant()) ? this.AdjustState(this.MapCall(data, fieldName, (GenericResultCache.CallDelegate<object>) ((cache, row, field) => cache.GetStateInt(row, field)), (GenericResultCache.FailureDelegate<object>) ((row, field) => base.GetStateInt(data, fieldName)))) : base.GetValueExt(data, fieldName);
  }

  public override object GetStateExt(object data, string fieldName)
  {
    return !this.FieldSelectingEvents.ContainsKey(fieldName.ToLowerInvariant()) ? this.AdjustState(this.MapCall(data, fieldName, (GenericResultCache.CallDelegate<object>) ((cache, row, field) => cache.GetStateExt(row, field)), (GenericResultCache.FailureDelegate<object>) ((row, field) => base.GetStateExt(data, fieldName)))) : base.GetValueExt(data, fieldName);
  }

  protected internal override string _ReportGetFirstKeyValueStored(string fieldName)
  {
    return this.MapCall<string>((object) null, fieldName, (GenericResultCache.CallDelegate<string>) ((cache, row, field) => cache._ReportGetFirstKeyValueStored(field)), (GenericResultCache.FailureDelegate<string>) ((row, field) => base._ReportGetFirstKeyValueStored(field)));
  }

  protected internal override string _ReportGetFirstKeyValueAttribute(string fieldName)
  {
    return this.MapCall<string>((object) null, fieldName, (GenericResultCache.CallDelegate<string>) ((cache, row, field) => cache._ReportGetFirstKeyValueAttribute(field)), (GenericResultCache.FailureDelegate<string>) ((row, field) => base._ReportGetFirstKeyValueAttribute(field)));
  }

  public override bool IsKvExtAttribute(string fieldName)
  {
    return this.MapCall<bool>((object) null, fieldName, (GenericResultCache.CallDelegate<bool>) ((cache, row, field) => cache.IsKvExtAttribute(field)), (GenericResultCache.FailureDelegate<bool>) ((row, field) => base.IsKvExtAttribute(field)));
  }

  protected override bool OnCommandPreparing(
    string name,
    object genericRow,
    object value,
    PXDBOperation operation,
    System.Type table,
    out PXCommandPreparingEventArgs.FieldDescription description)
  {
    PXCommandPreparingEventArgs.FieldDescription descr = (PXCommandPreparingEventArgs.FieldDescription) null;
    int num = this.MapCall<bool>(genericRow, name, (GenericResultCache.CallDelegate<bool>) ((cache, row, field) => GenericResultCache.IsFormulaField(field) || GenericResultCache.IsCountField(field) || GenericResultCache.IsStringAggField(field) ? base.OnCommandPreparing(name, genericRow, value, operation, this.GetItemType(), out descr) : cache.RaiseCommandPreparing(field, row, value, operation, row != null ? row.GetType() : cache.GetItemType(), out descr)), (GenericResultCache.FailureDelegate<bool>) ((row, field) => base.OnCommandPreparing(field, row, value, operation, table, out descr))) ? 1 : 0;
    description = descr;
    return num != 0;
  }

  protected override bool OnFieldUpdating(string name, object genericRow, ref object newValue)
  {
    object nv = newValue;
    int num = this.MapCall<bool>(genericRow, name, (GenericResultCache.CallDelegate<bool>) ((cache, row, field) => GenericResultCache.IsFormulaField(field) || GenericResultCache.IsCountField(field) || GenericResultCache.IsStringAggField(field) ? base.OnFieldUpdating(name, genericRow, ref nv) : cache.RaiseFieldUpdating(field, row, ref nv)), (GenericResultCache.FailureDelegate<bool>) ((row, field) => base.OnFieldUpdating(field, row, ref nv))) ? 1 : 0;
    newValue = nv;
    return num != 0;
  }

  public override List<PXEventSubscriberAttribute> GetAttributesReadonly(
    string name,
    bool extractEmmbeddedAttr)
  {
    return this.MapCall<List<PXEventSubscriberAttribute>>((object) null, name, (GenericResultCache.CallDelegate<List<PXEventSubscriberAttribute>>) ((cache, row, field) => cache.GetAttributesReadonly(field, extractEmmbeddedAttr)), (GenericResultCache.FailureDelegate<List<PXEventSubscriberAttribute>>) ((row, field) => base.GetAttributesReadonly(field, extractEmmbeddedAttr)));
  }

  public override List<PXEventSubscriberAttribute> GetAttributesReadonly(string name)
  {
    return this.MapCall<List<PXEventSubscriberAttribute>>((object) null, name, (GenericResultCache.CallDelegate<List<PXEventSubscriberAttribute>>) ((cache, row, field) => cache.GetAttributesReadonly(field)), (GenericResultCache.FailureDelegate<List<PXEventSubscriberAttribute>>) ((row, field) => base.GetAttributesReadonly(field)));
  }

  public override IEnumerable<PXEventSubscriberAttribute> GetAttributesReadonly(
    object data,
    string name)
  {
    return this.MapCall<IEnumerable<PXEventSubscriberAttribute>>(data, name, (GenericResultCache.CallDelegate<IEnumerable<PXEventSubscriberAttribute>>) ((cache, row, field) => cache.GetAttributesReadonly(row, field)), (GenericResultCache.FailureDelegate<IEnumerable<PXEventSubscriberAttribute>>) ((row, field) => base.GetAttributesReadonly(row, field)));
  }

  public override List<PXEventSubscriberAttribute> GetAttributes(string name)
  {
    return this.MapCall<List<PXEventSubscriberAttribute>>((object) null, name, (GenericResultCache.CallDelegate<List<PXEventSubscriberAttribute>>) ((cache, row, field) => cache.GetAttributes(field)), (GenericResultCache.FailureDelegate<List<PXEventSubscriberAttribute>>) ((row, field) => base.GetAttributes(field)));
  }

  public override IEnumerable<PXEventSubscriberAttribute> GetAttributes(object data, string name)
  {
    return this.MapCall<IEnumerable<PXEventSubscriberAttribute>>(data, name, (GenericResultCache.CallDelegate<IEnumerable<PXEventSubscriberAttribute>>) ((cache, row, field) => cache.GetAttributes(row, field)), (GenericResultCache.FailureDelegate<IEnumerable<PXEventSubscriberAttribute>>) ((row, field) => base.GetAttributes(row, field)));
  }

  public override PXFieldCollection Fields
  {
    get
    {
      if (this._Fields == null)
      {
        this.InitializeKeys();
        if (this._GenInqGraph.IsSubQueryInstance || this._GenInqGraph.HasSubQueries)
        {
          this.InitializeFieldsWithAttribute<PXDBAttributeAttribute>();
          this.InitializeFieldsWithAttribute<PXNoteAttribute>();
          this.InitializeFieldsWithDescription();
          this.InitializeFieldsRequiredForNavigation();
        }
      }
      return base.Fields;
    }
  }

  private void InitializeKeys()
  {
    if (this._GenInqGraph.Description == null || this._KeysMap != null)
      return;
    this._KeysMap = new Dictionary<int, GenericResultCache.KeyDescriptor>();
    this.InitializeKeys(this._GenInqGraph, string.Empty);
  }

  private void InitializeFieldsRecursively(
    Func<GenericResultCache, bool> skipIfTrue,
    Func<GenericResultCache, IEnumerable<string>> subCacheFieldSelector,
    Func<PXCache, string, IEnumerable<string>> ownFieldSelector,
    System.Action<string> onFieldAdded)
  {
    if (skipIfTrue(this))
      return;
    PXGenericInqGrph genInqGraph = this._GenInqGraph;
    foreach (PXTable pxTable in genInqGraph.BaseQueryDescription.UsedTables.Values)
    {
      PXTable table = pxTable;
      List<string> stringList = new List<string>();
      if (table.Graph != null && table.Graph.Caches[table.CacheType] is GenericResultCache cach1)
      {
        cach1.InitializeFieldsRecursively(skipIfTrue, subCacheFieldSelector, ownFieldSelector, onFieldAdded);
        stringList.AddRange(subCacheFieldSelector(cach1));
      }
      else
      {
        PXCache cach = genInqGraph.Caches[table.CacheType];
        IEnumerable<string> source = ownFieldSelector(cach, table.Alias);
        Dictionary<string, string> originalFieldNames = genInqGraph.Description.Results.Where<GIResult>((Func<GIResult, bool>) (x => x.ObjectName.Equals(table.Alias, StringComparison.OrdinalIgnoreCase) && !string.IsNullOrEmpty(x.Field) && !x.Field.StartsWith("="))).Select<GIResult, string>((Func<GIResult, string>) (x => x.Field)).Distinct<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase).ToDictionary<string, string, string>((Func<string, string>) (x => x), (Func<string, string>) (x => x), (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
        string str;
        Func<string, string> selector = (Func<string, string>) (f => !originalFieldNames.TryGetValue(f, out str) ? f : str);
        IEnumerable<string> collection = source.Select<string, string>(selector);
        stringList.AddRange(collection);
      }
      foreach (string str in stringList)
      {
        string key = $"{table.Alias}_{str}";
        if (!this._FieldsMap.ContainsKey(key))
        {
          this._FieldsMap[key] = this._FieldsMap.Count;
          this._ClassFields.Add(key);
          onFieldAdded(key);
        }
      }
    }
  }

  private void InitializeFieldsWithAttribute<TAttr>() where TAttr : PXEventSubscriberAttribute
  {
    List<string> fields = (List<string>) null;
    this.InitializeFieldsRecursively((Func<GenericResultCache, bool>) (cache =>
    {
      int num = cache._eventSubscriberAttributes.TryGetValue(typeof (TAttr), out fields) ? 1 : 0;
      if (num != 0)
        return num != 0;
      cache._eventSubscriberAttributes[typeof (TAttr)] = fields = new List<string>();
      return num != 0;
    }), (Func<GenericResultCache, IEnumerable<string>>) (cache => (IEnumerable<string>) cache._eventSubscriberAttributes[typeof (TAttr)]), (Func<PXCache, string, IEnumerable<string>>) ((cache, _) => cache.Fields.Where<string>((Func<string, bool>) (fieldName => cache.GetAttributes(fieldName).OfType<TAttr>().Any<TAttr>()))), (System.Action<string>) (field => fields.Add(field)));
  }

  private void InitializeFieldsWithDescription()
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    this.InitializeFieldsRecursively((Func<GenericResultCache, bool>) (cache => cache._descriptionFields.Any<string>()), (Func<GenericResultCache, IEnumerable<string>>) (cache => (IEnumerable<string>) cache._descriptionFields), (Func<PXCache, string, IEnumerable<string>>) ((cache, _) => cache.Fields.Where<string>(GenericResultCache.\u003C\u003EO.\u003C0\u003E__IsDescriptionField ?? (GenericResultCache.\u003C\u003EO.\u003C0\u003E__IsDescriptionField = new Func<string, bool>(GenericInquiryHelpers.IsDescriptionField))).Select<string, string>(GenericResultCache.\u003C\u003EO.\u003C1\u003E__GetBaseFieldNameForDescriptionField ?? (GenericResultCache.\u003C\u003EO.\u003C1\u003E__GetBaseFieldNameForDescriptionField = new Func<string, string>(GenericInquiryHelpers.GetBaseFieldNameForDescriptionField)))), (System.Action<string>) (field => this._descriptionFields.Add(field)));
  }

  private void InitializeFieldsRequiredForNavigation()
  {
    HashSet<string> fields = new HashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    foreach (GINavigationParameter navigationParameter in this._GenInqGraph.Description.NavigationParameters)
    {
      if (!string.IsNullOrEmpty(navigationParameter.ParameterName) && navigationParameter.ParameterName.StartsWith("="))
      {
        foreach (string str in this._GenInqGraph.GetDataFieldsFromFormula(navigationParameter.ParameterName))
          fields.Add(str);
      }
      else if (!string.IsNullOrEmpty(navigationParameter.TableAlias) && !string.IsNullOrEmpty(navigationParameter.ParameterFieldName))
        fields.Add($"{navigationParameter.TableAlias}_{navigationParameter.ParameterFieldName}");
    }
    foreach (GINavigationCondition navigationCondition in this._GenInqGraph.Description.NavigationConditions)
    {
      fields.Add(navigationCondition.DataField.Replace('.', '_'));
      foreach (string str in this._GenInqGraph.GetDataFieldsFromFormula(navigationCondition.ValueSt))
        fields.Add(str);
      foreach (string str in this._GenInqGraph.GetDataFieldsFromFormula(navigationCondition.ValueSt2))
        fields.Add(str);
    }
    this.InitializeFieldsRecursively((Func<GenericResultCache, bool>) (cache => cache._navigationFields.Any<string>()), (Func<GenericResultCache, IEnumerable<string>>) (cache => (IEnumerable<string>) cache._navigationFields), (Func<PXCache, string, IEnumerable<string>>) ((cache, tableAlias) => cache.Fields.Where<string>((Func<string, bool>) (f => fields.Contains($"{tableAlias}_{f}")))), (System.Action<string>) (field => this._navigationFields.Add(field)));
  }

  private void InitializeKeys(PXGenericInqGrph graph, string prefix)
  {
    if (this._KeysMap.Any<KeyValuePair<int, GenericResultCache.KeyDescriptor>>())
      return;
    foreach (PXTable pxTable in graph.BaseQueryDescription.UsedTables.Values)
    {
      if (pxTable.Graph != null && pxTable.Graph.Caches[pxTable.CacheType] is GenericResultCache cach1)
      {
        cach1.InitializeKeys(pxTable.Graph, pxTable.Alias + "_");
        foreach (KeyValuePair<int, GenericResultCache.KeyDescriptor> keys in cach1._KeysMap)
        {
          int num;
          GenericResultCache.KeyDescriptor keyDescriptor1;
          EnumerableExtensions.Deconstruct<int, GenericResultCache.KeyDescriptor>(keys, ref num, ref keyDescriptor1);
          int ordinal = num;
          GenericResultCache.KeyDescriptor keyDescriptor2 = keyDescriptor1;
          string str = $"{prefix}{pxTable.Alias}_{keyDescriptor2.FieldName}";
          int key = this._plainFieldsCount + this._KeysMap.Count;
          this._KeysMap[key] = new GenericResultCache.KeyDescriptor(ordinal, pxTable.Alias, str, (PXCache) cach1);
          this._FieldsMap[str] = key;
          this._ClassFields.Add(str);
        }
      }
      else
      {
        PXCache cach = graph.Caches[pxTable.CacheType];
        foreach (string key1 in (IEnumerable<string>) cach.Keys)
        {
          string str = $"{pxTable.Alias}_{key1}";
          int fields = cach._FieldsMap[key1];
          int key2 = this._plainFieldsCount + this._KeysMap.Count;
          this._KeysMap[key2] = new GenericResultCache.KeyDescriptor(fields, pxTable.Alias, str, cach);
          this._FieldsMap[str] = key2;
          this._ClassFields.Add(str);
        }
      }
    }
  }

  public override KeysCollection Keys
  {
    get
    {
      if (this._GenInqGraph.Description == null)
        return new KeysCollection();
      if (this._Keys == null)
      {
        this._Keys = new KeysCollection();
        this.InitializeKeys();
        foreach (GenericResultCache.KeyDescriptor keyDescriptor in this._KeysMap.Values)
          this._Keys.Add(keyDescriptor.FieldName);
      }
      return this._Keys;
    }
  }

  public override bool ObjectsEqual(object a, object b)
  {
    GenericResult genericResult1 = a as GenericResult;
    GenericResult genericResult2 = b as GenericResult;
    if (genericResult1 == null || genericResult2 == null)
      return base.ObjectsEqual(a, b);
    if (genericResult1.Values == null && genericResult2.Values == null)
      return true;
    if (genericResult1.Values == null && genericResult2.Values != null || genericResult1.Values != null && genericResult2.Values == null)
      return false;
    HashSet<string> stringSet = new HashSet<string>(genericResult1.Values.Keys.Concat<string>((IEnumerable<string>) genericResult2.Values.Keys));
    stringSet.Remove(GenericResultCache.FormulaAlias);
    foreach (string str in stringSet)
    {
      PXCache cacheByAlias = this.GetCacheByAlias(str);
      if (cacheByAlias == null)
        return base.ObjectsEqual(a, b);
      if (cacheByAlias.Keys.Any<string>())
      {
        object a1;
        bool flag1 = genericResult1.Values.TryGetValue(str, out a1);
        object b1;
        bool flag2 = genericResult2.Values.TryGetValue(str, out b1);
        if (flag1 && !flag2 || !flag1 & flag2 || flag1 & flag2 && !cacheByAlias.ObjectsEqual(a1, b1))
          return false;
      }
    }
    return true;
  }

  public override int GetObjectHashCode(object data)
  {
    if (!(data is GenericResult genericResult))
      return base.GetObjectHashCode(data);
    int objectHashCode1 = 0;
    foreach (KeyValuePair<string, object> keyValuePair in (IEnumerable<KeyValuePair<string, object>>) genericResult.Values)
    {
      string key = keyValuePair.Key;
      object data1 = keyValuePair.Value;
      if (key != GenericResultCache.FormulaAlias)
      {
        PXCache cacheByAlias = this.GetCacheByAlias(key);
        if (cacheByAlias == null)
          return base.GetObjectHashCode(data);
        if (cacheByAlias.Keys.Any<string>())
        {
          int objectHashCode2 = cacheByAlias.GetObjectHashCode(data1);
          objectHashCode1 ^= objectHashCode2;
        }
      }
    }
    return objectHashCode1;
  }

  protected override object GetValueByOrdinal(
    GenericResult data,
    int ordinal,
    PXCacheExtension[] extensions)
  {
    GenericResultCache.KeyDescriptor keyDescriptor;
    if (data == null || !this._KeysMap.TryGetValue(ordinal, out keyDescriptor))
      return base.GetValueByOrdinal(data, ordinal, extensions);
    object data1;
    return data.Values == null || !data.Values.TryGetValue(keyDescriptor.Alias, out data1) ? (object) null : keyDescriptor.Cache.GetValue(data1, keyDescriptor.Ordinal);
  }

  protected override void SetValueByOrdinal(
    GenericResult data,
    int ordinal,
    object value,
    PXCacheExtension[] extensions)
  {
    GenericResultCache.KeyDescriptor keyDescriptor;
    if (data != null && this._KeysMap.TryGetValue(ordinal, out keyDescriptor))
    {
      if (data.Values == null)
        data.Values = (IDictionary<string, object>) new Dictionary<string, object>();
      object data1;
      if (!data.Values.TryGetValue(keyDescriptor.Alias, out data1))
        data1 = data.Values[keyDescriptor.Alias] = keyDescriptor.Cache.CreateInstance();
      keyDescriptor.Cache.SetValue(data1, keyDescriptor.Ordinal, value);
    }
    else
      base.SetValueByOrdinal(data, ordinal, value, extensions);
  }

  internal override System.Type GetFieldType(string fieldName)
  {
    return this.MapCall<System.Type>((object) null, fieldName, (GenericResultCache.CallDelegate<System.Type>) ((cache, row, field) => cache.GetFieldType(field)), (GenericResultCache.FailureDelegate<System.Type>) ((row, field) => base.GetFieldType(field)));
  }

  public override string ValueToString(string fieldName, object val)
  {
    return this.MapCall<string>((object) null, fieldName, (GenericResultCache.CallDelegate<string>) ((cache, row, field) => cache.ValueToString(field, val)), (GenericResultCache.FailureDelegate<string>) ((row, field) => base.ValueToString(field, val)));
  }

  protected override GenericResult readItem(GenericResult item)
  {
    GenericResult data = this._GenInqGraph.Search(item);
    this.PlaceNotChanged(data);
    return data;
  }

  public override System.Type GetBqlField(string fieldName)
  {
    return this.MapCall<System.Type>((object) null, fieldName, (GenericResultCache.CallDelegate<System.Type>) ((cache, row, field) => cache.GetBqlField(field)), (GenericResultCache.FailureDelegate<System.Type>) ((row, field) => base.GetBqlField(fieldName)));
  }

  public override void Load()
  {
    if (this._GenInqGraph.Description == null)
      return;
    base.Load();
  }

  public override bool RaiseRowSelecting(
    object item,
    PXDataRecord record,
    ref int position,
    bool isReadOnly)
  {
    if (!(record is GIDataRecordMap giDataRecordMap) || !(item is GenericResult genericResult))
      return base.RaiseRowSelecting(item, record, ref position, isReadOnly);
    Dictionary<string, GIDataRecordMap.GIFieldEntry> ownFields = giDataRecordMap.FieldMap.OfType<GIDataRecordMap.GIFieldEntry>().Where<GIDataRecordMap.GIFieldEntry>((Func<GIDataRecordMap.GIFieldEntry, bool>) (x => x.TableType == typeof (GenericResult) && x.Owner == this.Graph)).ToDictionary<GIDataRecordMap.GIFieldEntry, string, GIDataRecordMap.GIFieldEntry>((Func<GIDataRecordMap.GIFieldEntry, string>) (x => x.FieldName), (Func<GIDataRecordMap.GIFieldEntry, GIDataRecordMap.GIFieldEntry>) (x => x), (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    if (!ownFields.Any<KeyValuePair<string, GIDataRecordMap.GIFieldEntry>>())
      return base.RaiseRowSelecting(item, record, ref position, isReadOnly);
    List<PXDataRecordMap.FieldEntry> list = this._GenInqGraph.LastCommand.RecordMapEntries.OfType<GIDataRecordMap.GIFieldEntry>().Select<GIDataRecordMap.GIFieldEntry, GIDataRecordMap.GIFieldEntry>((Func<GIDataRecordMap.GIFieldEntry, GIDataRecordMap.GIFieldEntry>) (x =>
    {
      GIDataRecordMap.GIFieldEntry giFieldEntry;
      ownFields.TryGetValue($"{x.Table.Alias}_{x.FieldName}", out giFieldEntry);
      return new GIDataRecordMap.GIFieldEntry(x.FieldName, x.Table, giFieldEntry != null ? giFieldEntry.PositionInResult : -1, giFieldEntry != null ? giFieldEntry.PositionInQuery : -1, x.Owner);
    })).Cast<PXDataRecordMap.FieldEntry>().ToList<PXDataRecordMap.FieldEntry>();
    PXDataRecord innerRecord = giDataRecordMap.InnerRecord;
    RestrictedFieldsSet restrictedFields = this._GenInqGraph.GetRecordRestrictedFields(this._GenInqGraph.BaseQueryDescription, giDataRecordMap.RestrictedFields);
    GIDataRecordMap map = new GIDataRecordMap(list, restrictedFields);
    IReadOnlyCollection<PXTable> tables = this._GenInqGraph.LastCommand.Tables;
    GenericResult row = this._GenInqGraph.ReadGenericRow(innerRecord, (PXDataRecordMap) map, (IEnumerable<PXTable>) tables).Row;
    position += ownFields.Keys.Count<string>((Func<string, bool>) (x => x.Contains<char>('_')));
    genericResult.Values = row.Values;
    genericResult.Row = row.Row;
    genericResult.Selected = row.Selected;
    return true;
  }

  /// <exclude />
  public delegate T CallDelegate<out T>(PXCache actualCache, object actualRow, string field);

  /// <exclude />
  public delegate T FailureDelegate<out T>(object actualRow, string actualFieldName);

  private class KeyDescriptor
  {
    public int Ordinal { get; set; }

    public string Alias { get; set; }

    public string FieldName { get; set; }

    public PXCache Cache { get; set; }

    public KeyDescriptor()
    {
    }

    public KeyDescriptor(int ordinal, string alias, string fieldName, PXCache cache)
    {
      this.Ordinal = ordinal;
      this.Alias = alias;
      this.FieldName = fieldName;
      this.Cache = cache;
    }
  }
}
