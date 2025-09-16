// Decompiled with JetBrains decompiler
// Type: PX.Common.Context.HttpContextSlotStorageProvider
// Assembly: PX.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35AC74DC-4190-4222-F56C-8CF32A9F1C93
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Common.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Common.xml

using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;

#nullable disable
namespace PX.Common.Context;

internal sealed class HttpContextSlotStorageProvider : SlotStorageProviderBase
{
  private readonly Dictionary<string, object> \u0002 = new Dictionary<string, object>();

  private HttpContextSlotStorageProvider()
  {
  }

  private static ISlotStorageProvider \u0002(IDictionary _param0)
  {
    object obj = _param0[(object) "PXContext.HttpContextSlotStore"];
    if (obj != null)
      return obj is HttpContextSlotStorageProvider slotStorageProvider1 ? (ISlotStorageProvider) slotStorageProvider1 : throw new InvalidOperationException($"Item {"PXContext.HttpContextSlotStore"} has unexpected type {obj.GetType()}");
    HttpContextSlotStorageProvider slotStorageProvider2 = new HttpContextSlotStorageProvider();
    _param0[(object) "PXContext.HttpContextSlotStore"] = (object) slotStorageProvider2;
    return (ISlotStorageProvider) slotStorageProvider2;
  }

  internal static ISlotStorageProvider Get(HttpContext _param0)
  {
    return HttpContextSlotStorageProvider.\u0002(_param0.Items);
  }

  internal static ISlotStorageProvider Get(HttpContextBase _param0)
  {
    return HttpContextSlotStorageProvider.\u0002(_param0.Items);
  }

  private Dictionary<string, object> \u0002() => this.\u0002;

  protected override object Get(string _param1)
  {
    object obj;
    return !this.\u0002().TryGetValue(_param1, out obj) ? (object) null : obj;
  }

  protected override void Set(string _param1, object _param2) => this.\u0002()[_param1] = _param2;

  protected override void Remove(string _param1) => this.\u0002().Remove(_param1);

  protected override IEnumerable<KeyValuePair<string, object>> Items()
  {
    return (IEnumerable<KeyValuePair<string, object>>) this.\u0002();
  }

  protected override void Clear() => this.\u0002().Clear();
}
