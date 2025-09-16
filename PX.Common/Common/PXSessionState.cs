// Decompiled with JetBrains decompiler
// Type: PX.Common.PXSessionState
// Assembly: PX.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35AC74DC-4190-4222-F56C-8CF32A9F1C93
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Common.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Common.xml

using PX.Common.Collection;
using PX.Common.Context;
using PX.Common.Session;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;

#nullable enable
namespace PX.Common;

public class PXSessionState
{
  internal const StringComparison KeyComparison = StringComparison.OrdinalIgnoreCase;
  private static readonly 
  #nullable disable
  PXSessionState.Indexer<object> \u0002 = new PXSessionState.Indexer<object>();
  internal PXSessionState.Indexer<object> PXParamValue = PXSessionState.\u0002;
  internal PXSessionState.Indexer<object> ExportData = PXSessionState.\u0002;
  internal PXSessionState.Indexer<object> RedirectReport = PXSessionState.\u0002;
  [PXInternalUseOnly]
  public PXSessionState.Indexer<object> PageInfo = PXSessionState.\u0002;
  [PXInternalUseOnly]
  public PXSessionState.Indexer<object> LongOpCustomInfo = PXSessionState.\u0002;
  internal PXSessionState.Indexer<Set<Guid>> linkFiles = new PXSessionState.Indexer<Set<Guid>>();
  internal PXSessionState.Indexer<object[]> SubmitFieldErrors = new PXSessionState.Indexer<object[]>();
  internal PXSessionState.Indexer<Queue> VIEWSTATEQUEUE = new PXSessionState.Indexer<Queue>();
  [PXInternalUseOnly]
  public PXSessionState.Indexer<System.Exception> Exception = new PXSessionState.Indexer<System.Exception>();
  internal PXSessionState.Indexer<List<string>> PXSharedScriptFiles = new PXSessionState.Indexer<List<string>>();
  internal PXSessionState.Indexer<HashSet<string>> SubmitFieldKeys = new PXSessionState.Indexer<HashSet<string>>();
  internal PXSessionState.Indexer<DateTime> PxDefaultDate = new PXSessionState.Indexer<DateTime>();
  internal PXSessionState.Indexer<bool?> isFilterUpdated = new PXSessionState.Indexer<bool?>();
  internal PXSessionState.Indexer<Guid?> GenericInquiryDesign = new PXSessionState.Indexer<Guid?>();
  internal PXSessionState.Indexer<bool?> GenericInquiryParametersChanged = new PXSessionState.Indexer<bool?>();
  internal PXSessionState.Indexer<bool?> batchMode = new PXSessionState.Indexer<bool?>();
  [PXInternalUseOnly]
  public PXSessionState.Indexer<bool?> FavoritesExists = new PXSessionState.Indexer<bool?>();
  [PXInternalUseOnly]
  public PXSessionState.Indexer<bool?> TabVisible = new PXSessionState.Indexer<bool?>();

  internal static StringComparer KeyComparer => StringComparer.OrdinalIgnoreCase;

  private static IPXSessionState \u0002()
  {
    return SlotStore.Instance.GetSessionStandIn() ?? HttpContextPXSessionState.TryGet(HttpContext.Current);
  }

  internal IPXSessionState Inner => PXSessionState.\u0002();

  internal IPXSessionState RequireInner()
  {
    return this.Inner ?? throw new InvalidOperationException("PXSessionState not available");
  }

  public bool IsSessionEnabled => PXSessionState.\u0002() != null;

  public object this[string key]
  {
    get => PXSessionState.GetValue(key);
    protected internal set => PXSessionState.SetValue(key, value);
  }

  public void SetGuid(string key, Guid value) => PXSessionState.SetValue(key, (object) value);

  public void SetString(string key, string value) => PXSessionState.SetValue(key, (object) value);

  public void SetValueType(string key, ValueType value)
  {
    PXSessionState.SetValue(key, (object) value);
  }

  public void Remove(string key) => PXSessionState.\u0002(key);

  private protected static void \u0002(string _param0)
  {
    if (_param0 == null)
      throw new ArgumentNullException("key");
    PXSessionState.\u0002()?.Remove(_param0);
  }

  internal void RemoveAll(string _param1)
  {
    PXSessionState.\u0002 obj = new PXSessionState.\u0002();
    obj.\u0002 = _param1;
    IPXSessionState pxSessionState = PXSessionState.\u0002();
    if (pxSessionState == null)
      return;
    pxSessionState.RemoveAll(new Func<string, bool>(obj.\u0002));
  }

  protected static void SetValue(string key, object value)
  {
    if (key == null)
      throw new ArgumentNullException(nameof (key));
    PXSessionState.\u0002()?.Set(key, value);
  }

  protected static object GetValue(string key)
  {
    if (key == null)
      throw new ArgumentNullException(nameof (key));
    return PXSessionState.\u0002()?.Get(key);
  }

  internal int? SchemaMode
  {
    get => (int?) PXSessionState.GetValue(nameof (SchemaMode));
    set => PXSessionState.SetValue(nameof (SchemaMode), (object) value);
  }

  private sealed class \u0002
  {
    public string \u0002;

    internal bool \u0002(
    #nullable enable
    string _param1) => _param1.StartsWith(this.\u0002 + "$", StringComparison.OrdinalIgnoreCase);
  }

  [PXInternalUseOnly]
  public abstract class AlteredKeyIndexerBase<T> : PXSessionState.Indexer<
  #nullable disable
  T>
  {
    protected abstract string AlterKey(string key);

    public override T this[string key]
    {
      get => base[this.AlterKey(key)];
      set => base[this.AlterKey(key)] = value;
    }
  }

  public class Indexer<T>
  {
    public virtual T this[string key]
    {
      get => PXSessionState.GetValue(key) is T obj ? obj : default (T);
      set => PXSessionState.SetValue(key, (object) value);
    }

    internal void Remove(string _param1) => PXSessionState.\u0002(_param1);
  }

  [PXInternalUseOnly]
  public class PrefixedIndexer<T> : PXSessionState.AlteredKeyIndexerBase<T>
  {
    private readonly string \u0002;

    public PrefixedIndexer(string prefix)
    {
      if (prefix == null)
        throw new ArgumentNullException(nameof (prefix));
      this.\u0002 = prefix.EndsWith("$") ? prefix : prefix + "$";
    }

    protected override string AlterKey(string key) => this.\u0002 + key;
  }
}
