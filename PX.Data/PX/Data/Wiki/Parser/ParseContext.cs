// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.ParseContext
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Autofac;
using PX.Api;
using PX.CS;
using PX.Data.Api;
using PX.Data.DependencyInjection;
using PX.PushNotifications;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

#nullable disable
namespace PX.Data.Wiki.Parser;

internal class ParseContext : 
  IDictionary<string, object>,
  ICollection<KeyValuePair<string, object>>,
  IEnumerable<KeyValuePair<string, object>>,
  IEnumerable
{
  public const string GraphKey = "TemplateGraph";
  public const string CurrentKey = "TemplateCurrent";
  public const string ParametersKey = "TemplateParameters";
  public const string MultiLineParametersKey = "TemplateMultiLineParameters";
  internal bool UseInternalParameterValue;
  private readonly IDictionary<string, object> _dictionaryImplementation = (IDictionary<string, object>) new Dictionary<string, object>();
  private IForeachIIterator _iteratorState;
  private Regex _phoneRegex = new Regex("^\\+?[0-9()\\-. ]{7,}$");

  public ParseContext()
  {
    ILifetimeScope lifetimeScope = LifetimeScopeHelper.GetLifetimeScope();
    this.ContactProvider = lifetimeScope != null ? ResolutionExtensions.Resolve<IContactProvider>((IComponentContext) lifetimeScope) : (IContactProvider) null;
  }

  public PXGraph TemplateGraph
  {
    get
    {
      object obj;
      return this._dictionaryImplementation.TryGetValue(nameof (TemplateGraph), out obj) ? (PXGraph) obj : (PXGraph) null;
    }
    set => this._dictionaryImplementation[nameof (TemplateGraph)] = (object) value;
  }

  public Dictionary<string, AUWorkflowFormField[]> Forms { get; set; } = new Dictionary<string, AUWorkflowFormField[]>();

  public object CurrentRow
  {
    get
    {
      object obj;
      return !this._dictionaryImplementation.TryGetValue("TemplateCurrent", out obj) ? (object) null : obj;
    }
    set => this._dictionaryImplementation["TemplateCurrent"] = value;
  }

  public Tuple<IDictionary<string, object>, IDictionary<string, object>>[] MultilineTemplateParameters
  {
    get
    {
      object obj;
      return !this._dictionaryImplementation.TryGetValue("TemplateMultiLineParameters", out obj) ? (Tuple<IDictionary<string, object>, IDictionary<string, object>>[]) null : (Tuple<IDictionary<string, object>, IDictionary<string, object>>[]) obj;
    }
    set => this._dictionaryImplementation["TemplateMultiLineParameters"] = (object) value;
  }

  public IDictionary<string, string> TemplateParameters
  {
    get
    {
      object obj;
      return this._dictionaryImplementation.TryGetValue(nameof (TemplateParameters), out obj) ? (IDictionary<string, string>) obj : (IDictionary<string, string>) null;
    }
    set => this._dictionaryImplementation[nameof (TemplateParameters)] = (object) value;
  }

  public IForeachIIterator SetForeachIterator(
    string viewName,
    IDictionary<string, string> parameters,
    string sortby)
  {
    if (this.TemplateGraph != null && (this.MultilineTemplateParameters == null || !(this.TemplateGraph is PXGenericInqGrph)) && this.TemplateParameters == null)
    {
      PXGraph templateGraph = this.TemplateGraph;
      if ((templateGraph != null ? (templateGraph.Views.ContainsKey(viewName) ? 1 : 0) : 0) != 0)
      {
        this._iteratorState = (IForeachIIterator) new ParseContext.GraphIteratorState(this, this._iteratorState, this.TemplateGraph?.Views[viewName], this.CurrentRow, parameters, sortby);
        goto label_9;
      }
    }
    if (this.MultilineTemplateParameters != null)
      this._iteratorState = (IForeachIIterator) new ParseContext.ArrayOfDictionaryIteratorState(this, this._iteratorState, this.MultilineTemplateParameters);
    else if (this.TemplateParameters != null)
    {
      this._iteratorState = (IForeachIIterator) new ParseContext.ArrayOfDictionaryIteratorState(this, this._iteratorState, new Tuple<IDictionary<string, object>, IDictionary<string, object>>[1]
      {
        Tuple.Create<IDictionary<string, object>, IDictionary<string, object>>((IDictionary<string, object>) this.TemplateParameters.ToDictionary<KeyValuePair<string, string>, string, object>((Func<KeyValuePair<string, string>, string>) (c => c.Key), (Func<KeyValuePair<string, string>, object>) (c => (object) c.Value)), (IDictionary<string, object>) new Dictionary<string, object>())
      });
    }
    else
    {
      this._iteratorState = (IForeachIIterator) new ParseContext.ArrayOfDictionaryIteratorState(this, this._iteratorState, new Tuple<IDictionary<string, object>, IDictionary<string, object>>[0]);
      PXGraph templateGraph = this.TemplateGraph;
      if ((templateGraph != null ? (templateGraph.Views.ContainsKey(viewName) ? 1 : 0) : 0) == 0)
        this.TemplateGraph.Views[this.TemplateGraph.PrimaryView].Cache.RaiseExceptionHandling(this.TemplateGraph.Views[this.TemplateGraph.PrimaryView].Cache.Keys[0], this.TemplateGraph.Views[this.TemplateGraph.PrimaryView].Cache.Current, (object) null, (Exception) new PXSetPropertyException("The value of at least one field in the selected email template is not recognized.", PXErrorLevel.Warning));
    }
label_9:
    return this._iteratorState;
  }

  public void CloseForeachIterator() => this._iteratorState.Dispose();

  public string GetValue(string paramName)
  {
    return this.GetValue(paramName, (Func<PXCache, object, string, string>) null);
  }

  public string GetValue(
    string paramName,
    Func<PXCache, object, string, string> getFieldValueFunc)
  {
    if (paramName.Contains("!"))
      return this.ProcessSelectorsTreeValues(paramName);
    if (paramName.Contains<char>(':'))
    {
      int startIndex = paramName.IndexOf(".");
      paramName = ScreenUtils.NormalizeViewName(paramName) + paramName.Substring(startIndex);
    }
    if (this.MultilineTemplateParameters != null && !paramName.StartsWith("GeneralInfo."))
    {
      string str = paramName.Replace(".", "_");
      object obj1 = new object();
      object obj2 = obj1;
      if (this.Previous)
      {
        this.Previous = false;
        object obj3;
        if (this.MultilineTemplateParameters[0].Item2 != null && this.MultilineTemplateParameters[0].Item2.TryGetValue(str, out obj3))
          obj2 = obj3;
      }
      else
      {
        object obj4;
        if (this.MultilineTemplateParameters[0].Item1 != null && this.MultilineTemplateParameters[0].Item1.TryGetValue(str, out obj4))
          obj2 = obj4;
      }
      if (obj2 != obj1)
        return ParseContext.GetValueString(this.TemplateGraph, str, obj2, this.UseInternalParameterValue);
      if (this.TemplateGraph == null || this.TemplateGraph is PXGenericInqGrph)
        return "";
    }
    if (this.Previous)
    {
      this.Previous = false;
      return "";
    }
    if (this.TemplateParameters != null)
    {
      string str;
      return !this.TemplateParameters.TryGetValue(paramName, out str) ? (string) null : str;
    }
    if (this.TemplateGraph == null)
      return (string) null;
    PXGraph templateGraph = this.TemplateGraph;
    EntityHelper eh = new EntityHelper(templateGraph);
    object currentRow = this.CurrentRow;
    int length = paramName.IndexOf('.');
    string str1 = length < 0 ? (string) null : paramName.Substring(0, length);
    if (str1 != null && templateGraph.Views.ContainsKey(str1) && paramName.EndsWith("Attributes"))
      PXDBAttributeAttribute.Activate(templateGraph.Views[str1].Cache);
    if (str1 != null && string.Equals(templateGraph.PrimaryView, str1, StringComparison.OrdinalIgnoreCase))
    {
      paramName = length < 0 || length >= paramName.Length - 1 ? paramName : paramName.Substring(length + 1);
      length = -1;
      str1 = (string) null;
    }
    if (str1 != null && this.Forms.ContainsKey(str1))
    {
      string str2 = paramName.Substring(length + 1);
      length = str2.IndexOf('.');
      string fieldName = str2.Substring(length + 1);
      string formFieldName = str2.Substring(0, length);
      AUWorkflowFormField workflowFormField = ((IEnumerable<AUWorkflowFormField>) this.Forms[str1]).FirstOrDefault<AUWorkflowFormField>((Func<AUWorkflowFormField, bool>) (field => ((IEnumerable<string>) field.SchemaField.Split('.')).Last<string>().OrdinalEquals(formFieldName)));
      if (workflowFormField != null)
      {
        object current = templateGraph.Views["FilterPreview"].Cache.Current;
        PXFieldState fieldState = eh.GetFieldState(current, "FilterPreview", workflowFormField.FieldName);
        object key = fieldState.Value;
        if (key == null)
          return "";
        if (fieldState.ViewName == null)
          return (string) null;
        System.Type dacType;
        FormFieldHelper.TryGetFieldFromFormFieldName(templateGraph, workflowFormField.SchemaField, out dacType, out string _);
        object selectorRow = eh.GetSelectorRow(templateGraph.Caches[dacType].Current, formFieldName, key);
        return selectorRow != null ? ParseContext.GetPropertyOf(eh, selectorRow, (string) null, fieldName, getFieldValueFunc) : "";
      }
    }
    string propertyOf;
    if (str1 == null || !templateGraph.Views.ContainsKey(str1))
    {
      propertyOf = ParseContext.GetPropertyOf(eh, currentRow, (string) null, paramName, getFieldValueFunc);
    }
    else
    {
      object row = templateGraph.Views[str1].Cache.Current;
      if (currentRow != null)
      {
        if (templateGraph.Views[str1].CacheGetItemType() == currentRow.GetType())
          row = templateGraph.Views[str1].Cache.Current = currentRow;
        else if (templateGraph.Views[str1].Cache.Current == null)
        {
          object obj = templateGraph.Views[str1].SelectSingleBound(new object[1]
          {
            currentRow
          }, (object[]) null);
          if (obj is PXResult)
            obj = ((PXResult) obj)[0];
          row = obj;
        }
      }
      propertyOf = ParseContext.GetPropertyOf(eh, row, str1, paramName.Substring(length + 1), getFieldValueFunc);
    }
    return propertyOf;
  }

  public static string GetValueString(PXGraph graph, string paramName, object value)
  {
    return ParseContext.GetValueString(graph, paramName, value, false);
  }

  private static string GetValueString(
    PXGraph graph,
    string paramName,
    object value,
    bool useInternalParameterValue)
  {
    if (graph == null || string.IsNullOrEmpty(paramName) || value == null)
      return EntityHelper.GetValueString(value);
    object o;
    if (value is ValueWithInternal valueWithInternal)
    {
      o = valueWithInternal.ExternalValue == null || useInternalParameterValue ? valueWithInternal.InternalValue : valueWithInternal.ExternalValue;
      if (o == null)
        EntityHelper.GetValueString(o);
    }
    else
      o = value;
    string str1;
    string str2;
    if (graph is PXGenericInqGrph)
    {
      str1 = "Results";
      str2 = paramName;
    }
    else
    {
      int length = paramName.IndexOf('_');
      if (length < 0)
        return EntityHelper.GetValueString(o);
      str1 = paramName.Substring(0, length);
      str2 = paramName.Substring(length + 1);
    }
    if (!graph.Views.ContainsKey(str1) || !(graph.GetStateExt(str1, (object) null, str2) is PXFieldState stateExt))
      return EntityHelper.GetValueString(o);
    if (!stateExt.DataType.IsInstanceOfType(o))
    {
      if (!(o is IConvertible))
        return EntityHelper.GetValueString(o);
      try
      {
        o = Convert.ChangeType(o, stateExt.DataType);
      }
      catch
      {
        return EntityHelper.GetValueString(o);
      }
    }
    if (stateExt is PXStringState pxStringState && !string.IsNullOrEmpty(pxStringState.Language) && o is string str3)
    {
      PXDBLocalizableStringAttribute localizableStringAttribute = graph.Views[str1].Cache.GetAttributesReadonly(str2).OfType<PXDBLocalizableStringAttribute>().FirstOrDefault<PXDBLocalizableStringAttribute>();
      if (localizableStringAttribute != null)
        return localizableStringAttribute.GetTranslationFromPackedValue(str3);
    }
    stateExt.Value = o;
    return EntityHelper.GetFieldString(stateExt);
  }

  private string ProcessSelectorsTreeValues(string paramName)
  {
    try
    {
      string[] fields = paramName.Split(new string[1]{ "!" }, StringSplitOptions.RemoveEmptyEntries);
      AUWorkflowFormField[] source = Array.Empty<AUWorkflowFormField>();
      PXGenericInqGrph templateGraph = this.TemplateGraph as PXGenericInqGrph;
      bool flag = false;
      PXCache cache;
      if (templateGraph != null)
      {
        cache = (PXCache) templateGraph.Results.Cache;
      }
      else
      {
        if (this.CurrentRow == null)
          return paramName;
        if (this.TemplateGraph.Views.ContainsKey(fields[0]))
        {
          cache = this.TemplateGraph.Views[fields[0]].Cache;
          this.CurrentRow = cache.Current;
          fields = ((IEnumerable<string>) fields).Skip<string>(1).ToArray<string>();
        }
        else if (this.Forms.Keys.Contains<string>(fields[0]))
        {
          source = this.Forms[fields[0]];
          cache = this.TemplateGraph.Views["FilterPreview"].Cache;
          this.CurrentRow = cache.Current;
          fields = ((IEnumerable<string>) fields).Skip<string>(1).ToArray<string>();
          flag = true;
        }
        else
        {
          this.CurrentRow = this.TemplateGraph.Views[this.TemplateGraph.PrimaryView].Cache.Current;
          cache = this.TemplateGraph.Caches[this.CurrentRow.GetType()];
        }
      }
      if (fields.Length != 0)
      {
        object data = this.CurrentRow;
        for (int i = 0; i < fields.Length; i++)
        {
          object key1 = (object) null;
          if (i < fields.Length - 1)
          {
            string str = fields[i];
            if (data != null && fields[i].Contains("_"))
              str = fields[i].Split(new string[1]{ "_" }, StringSplitOptions.RemoveEmptyEntries)[1];
            if (this.Previous)
            {
              this.Previous = false;
              string key2 = fields[i];
              object obj = (object) null;
              IDictionary<string, object> dictionary = this.MultilineTemplateParameters[0].Item2;
              if ((dictionary != null ? (dictionary.TryGetValue(key2, out obj) ? 1 : 0) : 0) != 0)
                key1 = ValueWithInternal.UnwrapInternalValue(obj);
            }
            else
            {
              IDictionary<string, object> dictionary = this.MultilineTemplateParameters[0].Item1;
              if ((dictionary != null ? (dictionary.TryGetValue(fields[i], out key1) ? 1 : 0) : 0) != 0)
                key1 = ValueWithInternal.UnwrapInternalValue(key1);
              else if (flag)
              {
                AUWorkflowFormField workflowFormField = ((IEnumerable<AUWorkflowFormField>) source).FirstOrDefault<AUWorkflowFormField>((Func<AUWorkflowFormField, bool>) (f => ((IEnumerable<string>) f.SchemaField.Split('.')).Last<string>().OrdinalEquals(fields[i])));
                if (workflowFormField != null)
                {
                  key1 = cache.GetStateExt(this.CurrentRow, workflowFormField.FieldName) is PXFieldState stateExt ? stateExt.Value : (object) null;
                  System.Type dacType;
                  FormFieldHelper.TryGetFieldFromFormFieldName(this.TemplateGraph, workflowFormField.SchemaField, out dacType, out string _);
                  cache = this.TemplateGraph.Caches[dacType];
                }
              }
              else
                key1 = cache.GetValue(data, fields[i]);
            }
            foreach (PXEventSubscriberAttribute subscriberAttribute in cache.GetAttributesReadonly(str))
            {
              if (subscriberAttribute is PXSelectorAttribute attr)
              {
                if (key1 != null)
                {
                  System.Type type1 = key1.GetType();
                  System.Type fieldType = cache.GetFieldType(str);
                  System.Type type2 = fieldType;
                  if (type1 != type2)
                  {
                    if (key1 is IConvertible)
                    {
                      try
                      {
                        key1 = Convert.ChangeType(key1, fieldType);
                      }
                      catch (InvalidCastException ex)
                      {
                      }
                    }
                  }
                }
                data = PXSelectorAttribute.GetItemUnconditionally(cache, attr, key1);
                break;
              }
            }
            if (data is PXResult)
              data = ((PXResult) data)[0];
            if (data != null)
              cache = this.TemplateGraph.Caches[PXSelectorAttribute.GetItemType(cache, str)];
            else
              break;
          }
          else
          {
            switch (PXFieldState.UnwrapValue(cache.GetValueExt(data, fields[i])))
            {
              case string str1:
                if (str1.Contains("@"))
                  return str1;
                string input = str1;
                if (this._phoneRegex.IsMatch(input))
                  return input;
                break;
              case Guid guid:
                return guid.ToString();
              case int num:
                return num.ToString();
            }
          }
          flag = false;
        }
      }
    }
    catch
    {
      this.TemplateGraph.Views[this.TemplateGraph.PrimaryView].Cache.RaiseExceptionHandling(this.TemplateGraph.Views[this.TemplateGraph.PrimaryView].Cache.Keys[0], this.TemplateGraph.Views[this.TemplateGraph.PrimaryView].Cache.Current, (object) null, (Exception) new PXSetPropertyException("The value of at least one field in the selected email template is not recognized.", PXErrorLevel.Warning));
      return (string) null;
    }
    return (string) null;
  }

  public static string GetPropertyOf(
    EntityHelper eh,
    object row,
    string viewName,
    string fieldName)
  {
    return ParseContext.GetPropertyOf(eh, row, viewName, fieldName, (Func<PXCache, object, string, string>) null);
  }

  public static string GetPropertyOf(
    EntityHelper eh,
    object row,
    string viewName,
    string fieldName,
    Func<PXCache, object, string, string> getFieldValueFunc)
  {
    int length = fieldName.IndexOf('.');
    if (row == null)
      return "";
    if (row is PXResult)
      row = ((PXResult) row)[0];
    if (length < 0)
    {
      if (getFieldValueFunc == null)
        return eh.GetFieldString(row, viewName, fieldName);
      PXCache pxCache = (PXCache) null;
      PXFieldState fieldState = eh.GetFieldState(row, viewName, fieldName);
      PXView pxView;
      if (fieldState != null && fieldState.ViewName != null && eh.Graph.Views.TryGetValue(fieldState.ViewName, out pxView))
        pxCache = pxView?.Cache;
      object obj = eh.GetField(row, viewName, fieldName);
      if (fieldName.StartsWith("Attribute"))
      {
        object fieldExt = eh.GetFieldExt(row, viewName, fieldName);
        if (fieldExt != null)
        {
          obj = fieldExt;
          fieldName = fieldState?.ValueField ?? fieldState?.Name ?? fieldName;
        }
        if (pxCache == null)
        {
          string screenId = PXSiteMap.Provider.FindSiteMapNode(eh.GetPrimaryGraphType(row, false))?.ScreenID;
          KeyValueHelper.Definition def = KeyValueHelper.Def;
          KeyValueHelper.Attribute attribute = def != null ? ((IEnumerable<KeyValueHelper.ScreenAttribute>) def.GetAttributes(screenId)).FirstOrDefault<KeyValueHelper.ScreenAttribute>((Func<KeyValueHelper.ScreenAttribute, bool>) (item => item.AttributeID == fieldName.Substring("Attribute".Length)))?.Attribute : (KeyValueHelper.Attribute) null;
          if (attribute != null)
          {
            pxCache = eh.Graph.Caches[((IEnumerable<string>) attribute.SchemaObject.Split('.')).Last<string>()];
            fieldName = attribute.SchemaField ?? fieldName;
          }
        }
      }
      return getFieldValueFunc(pxCache, obj, fieldName);
    }
    string str = fieldName.Substring(0, length);
    PXFieldState fieldState1 = eh.GetFieldState(row, viewName, str);
    object field = eh.GetField(row, viewName, str);
    if (field == null)
      return "";
    if (fieldState1.ViewName == null)
      return (string) null;
    object selectorRow = eh.GetSelectorRow(row, str, field);
    return selectorRow != null ? ParseContext.GetPropertyOf(eh, selectorRow, (string) null, fieldName.Substring(length + 1), getFieldValueFunc) : "";
  }

  public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
  {
    return this._dictionaryImplementation.GetEnumerator();
  }

  IEnumerator IEnumerable.GetEnumerator() => this._dictionaryImplementation.GetEnumerator();

  public void Add(KeyValuePair<string, object> item) => this._dictionaryImplementation.Add(item);

  public void Clear() => this._dictionaryImplementation.Clear();

  public bool Contains(KeyValuePair<string, object> item)
  {
    return this._dictionaryImplementation.Contains(item);
  }

  public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
  {
    throw new NotSupportedException();
  }

  public bool Remove(KeyValuePair<string, object> item)
  {
    return ((ICollection<KeyValuePair<string, object>>) this._dictionaryImplementation).Remove(item);
  }

  public int Count => this._dictionaryImplementation.Count;

  public bool IsReadOnly => this._dictionaryImplementation.IsReadOnly;

  public bool ContainsKey(string key) => this._dictionaryImplementation.ContainsKey(key);

  public void Add(string key, object value) => this._dictionaryImplementation.Add(key, value);

  public bool Remove(string key) => this._dictionaryImplementation.Remove(key);

  public bool TryGetValue(string key, out object value)
  {
    return this._dictionaryImplementation.TryGetValue(key, out value);
  }

  public object this[string key]
  {
    get => this._dictionaryImplementation[key];
    set => this._dictionaryImplementation[key] = value;
  }

  public ICollection<string> Keys => this._dictionaryImplementation.Keys;

  public ICollection<object> Values => this._dictionaryImplementation.Values;

  internal bool Previous { get; set; }

  public IContactProvider ContactProvider { get; set; }

  private class ArrayOfDictionaryIteratorState : ParseContext.IteratorStateBase
  {
    private readonly Tuple<IDictionary<string, object>, IDictionary<string, object>>[] _foreachCollection;

    protected override IList ForeachCollection => (IList) this._foreachCollection;

    public override string GetValue(string fieldName)
    {
      return this.GetValue(fieldName, (object) this._foreachCollection[this._currentIteratorPosition]);
    }

    public override string GetValue(string fieldName, object row)
    {
      if (!(row is Tuple<IDictionary<string, object>, IDictionary<string, object>> tuple))
        return this.GetValue(fieldName);
      string str = fieldName.Replace(".", "_");
      object obj = (object) null;
      if (this._context.Previous)
      {
        this._context.Previous = false;
        IDictionary<string, object> dictionary = tuple.Item2;
        return (dictionary != null ? (dictionary.TryGetValue(str, out obj) ? 1 : 0) : 0) == 0 ? string.Empty : ParseContext.GetValueString(this._context.TemplateGraph, str, obj);
      }
      IDictionary<string, object> dictionary1 = tuple.Item1;
      return (dictionary1 != null ? (dictionary1.TryGetValue(str, out obj) ? 1 : 0) : 0) == 0 ? string.Empty : ParseContext.GetValueString(this._context.TemplateGraph, str, obj);
    }

    public ArrayOfDictionaryIteratorState(
      ParseContext context,
      IForeachIIterator iteratorState,
      Tuple<IDictionary<string, object>, IDictionary<string, object>>[] foreachCollection)
      : base(context, iteratorState)
    {
      this._foreachCollection = foreachCollection;
    }
  }

  private abstract class IteratorStateBase : 
    IForeachIIterator,
    IEnumerable<object>,
    IEnumerable,
    IEnumerator<object>,
    IDisposable,
    IEnumerator
  {
    protected int _currentIteratorPosition = -1;
    private readonly IForeachIIterator _previousIterator;
    protected readonly ParseContext _context;

    protected abstract IList ForeachCollection { get; }

    protected IteratorStateBase(ParseContext context, IForeachIIterator iteratorState)
    {
      this._context = context;
      this._previousIterator = iteratorState;
    }

    private void Close()
    {
      this._currentIteratorPosition = int.MinValue;
      this._context._iteratorState = this._previousIterator;
    }

    public IEnumerator<object> GetEnumerator() => (IEnumerator<object>) this;

    public object Current => this.ForeachCollection[this._currentIteratorPosition];

    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

    public virtual void Dispose() => this.Close();

    public virtual bool MoveNext()
    {
      ++this._currentIteratorPosition;
      return this._currentIteratorPosition >= 0 && this._currentIteratorPosition < this.ForeachCollection.Count;
    }

    public void Reset() => this._currentIteratorPosition = -1;

    public abstract string GetValue(string fieldName);

    public abstract string GetValue(string fieldName, object row);
  }

  private class GraphIteratorState : ParseContext.IteratorStateBase
  {
    private readonly PXView _foreachView;
    private List<object> _foreachCollection;

    public GraphIteratorState(
      ParseContext context,
      IForeachIIterator iteratorState,
      PXView view,
      object primaryCurrent,
      IDictionary<string, string> parameters,
      string sortby)
      : base(context, iteratorState)
    {
      this._foreachView = view;
      string[] parameterNames = view.GetParameterNames();
      object[] parameters1 = new object[parameterNames.Length];
      for (int index = 0; index < parameterNames.Length; ++index)
      {
        string key = parameterNames[index];
        string str;
        if (parameters.TryGetValue(key, out str))
          parameters1[index] = (object) str;
      }
      List<string> stringList = new List<string>();
      List<bool> boolList = new List<bool>();
      if (sortby != null)
      {
        string str1 = sortby;
        string[] separator = new string[1]{ "," };
        foreach (string str2 in str1.Split(separator, StringSplitOptions.RemoveEmptyEntries))
        {
          char[] chArray = new char[1]{ '$' };
          string[] strArray = str2.Split(chArray);
          if (strArray.Length > 1)
          {
            string str3 = strArray[0];
            string lower = strArray[1].ToLower();
            if (lower == "a" || lower == "d")
            {
              stringList.Add(str3);
              boolList.Add(lower == "d");
            }
          }
        }
      }
      int startRow = 0;
      int totalRows = 0;
      this._foreachCollection = view.Select(new object[1]
      {
        primaryCurrent
      }, parameters1, (object[]) null, stringList.ToArray(), boolList.ToArray(), (PXFilterRow[]) null, ref startRow, 0, ref totalRows);
    }

    public override bool MoveNext()
    {
      if (!base.MoveNext())
        return false;
      object current = this.Current;
      if (!this._foreachView.IsReadOnly)
        this._foreachView.Cache.Current = !(current is PXResult) ? current : ((PXResult) current)[0];
      return true;
    }

    public override string GetValue(string fieldName)
    {
      if (!this._context.Previous)
        return ParseContext.GetPropertyOf(new EntityHelper(this._foreachView.Graph), this.Current, this._foreachView.Name, fieldName);
      this._context.Previous = false;
      return (string) null;
    }

    public override string GetValue(string fieldName, object row) => this.GetValue(fieldName);

    protected override IList ForeachCollection => (IList) this._foreachCollection;
  }
}
