// Decompiled with JetBrains decompiler
// Type: PX.Common.Context.SlotStorageProviderBase
// Assembly: PX.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35AC74DC-4190-4222-F56C-8CF32A9F1C93
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Common.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Common.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Common.Context;

internal abstract class SlotStorageProviderBase : ISlotStorageProvider, ISlotStore
{
  protected abstract object Get(string _param1);

  protected abstract void Set(string _param1, object _param2);

  protected abstract void Remove(string _param1);

  protected abstract IEnumerable<KeyValuePair<string, object>> Items();

  protected abstract void Clear();

  private static TValue \u0002<TValue>(object _param0)
  {
    return _param0 == null ? default (TValue) : (TValue) _param0;
  }

  TValue ISlotStore.ywb58c5syaqkkusa7megd3n9n4w2qaen\u2009\u2009\u2009\u000E<TValue>(string _param1)
  {
    return SlotStorageProviderBase.\u0002<TValue>(this.\u000E(this.Get(_param1)));
  }

  void ISlotStore.ywb58c5syaqkkusa7megd3n9n4w2qaen\u2009\u2009\u2009\u0002(
    string _param1,
    object _param2)
  {
    this.Set(_param1, this.\u0002(_param2));
  }

  void ISlotStore.ywb58c5syaqkkusa7megd3n9n4w2qaen\u2009\u2009\u2009\u0002(string _param1)
  {
    this.Remove(_param1);
  }

  IEnumerable<KeyValuePair<string, object>> ISlotStorageProvider.ywb58c5syaqkkusa7megd3n9n4w2qaen\u2009\u2009\u2009\u0002()
  {
    return this.Items();
  }

  void ISlotStorageProvider.ywb58c5syaqkkusa7megd3n9n4w2qaen\u2009\u2009\u2009\u0002()
  {
    this.Clear();
  }

  private object \u0002(object _param1)
  {
    if (!(_param1 is IPXCompanyDependent))
      return _param1;
    int? nullable = this.\u0002();
    if (!nullable.HasValue)
      return _param1;
    int valueOrDefault = nullable.GetValueOrDefault();
    return (object) new SlotStorageProviderBase.\u0002(_param1, new int?(valueOrDefault));
  }

  private object \u000E(object _param1)
  {
    switch (_param1)
    {
      case SlotStorageProviderBase.\u0002 obj:
        int? nullable1 = this.\u0002();
        if (nullable1.HasValue)
        {
          int valueOrDefault1 = nullable1.GetValueOrDefault();
          int? nullable2 = obj.\u000E;
          int valueOrDefault2 = nullable2.GetValueOrDefault();
          if (valueOrDefault1 == valueOrDefault2 & nullable2.HasValue)
            return obj.\u0002;
        }
        return (object) null;
      case IPXCompanyDependent _:
        return this.\u0002().HasValue ? (object) null : _param1;
      default:
        return _param1;
    }
  }

  private int? \u0002() => (int?) this.Get("singleCompanyID");

  private sealed class \u0002
  {
    public readonly object \u0002;
    public readonly int? \u000E;

    public \u0002(object _param1, int? _param2)
    {
      this.\u0002 = _param1;
      this.\u000E = _param2;
    }
  }
}
