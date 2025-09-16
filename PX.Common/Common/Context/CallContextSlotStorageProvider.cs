// Decompiled with JetBrains decompiler
// Type: PX.Common.Context.CallContextSlotStorageProvider
// Assembly: PX.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35AC74DC-4190-4222-F56C-8CF32A9F1C93
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Common.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Common.xml

using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;

#nullable disable
namespace PX.Common.Context;

internal sealed class CallContextSlotStorageProvider : SlotStorageProviderBase
{
  private static readonly CallContextSlotStorageProvider \u0002 = new CallContextSlotStorageProvider();

  internal static CallContextSlotStorageProvider Get() => CallContextSlotStorageProvider.\u0002;

  private static Dictionary<string, object> \u0002()
  {
    return (Dictionary<string, object>) CallContext.GetData("PX.Common.PXContext$Slots");
  }

  private static void \u0002(Dictionary<string, object> _param0)
  {
    CallContext.SetData("PX.Common.PXContext$Slots", (object) _param0);
  }

  protected override object Get(string _param1)
  {
    Dictionary<string, object> dictionary = CallContextSlotStorageProvider.\u0002();
    if (dictionary == null)
      return (object) null;
    object obj;
    return !dictionary.TryGetValue(_param1, out obj) ? (object) null : obj;
  }

  protected override void Set(string _param1, object _param2)
  {
    Dictionary<string, object> dictionary = CallContextSlotStorageProvider.\u0002();
    if (dictionary == null)
    {
      dictionary = new Dictionary<string, object>();
      CallContextSlotStorageProvider.\u0002(dictionary);
    }
    dictionary[_param1] = _param2;
  }

  protected override void Remove(string _param1)
  {
    CallContextSlotStorageProvider.\u0002()?.Remove(_param1);
  }

  protected override IEnumerable<KeyValuePair<string, object>> Items()
  {
    return (IEnumerable<KeyValuePair<string, object>>) CallContextSlotStorageProvider.\u0002() ?? Enumerable.Empty<KeyValuePair<string, object>>();
  }

  protected override void Clear() => CallContextSlotStorageProvider.\u0002()?.Clear();
}
