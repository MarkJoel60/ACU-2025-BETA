// Decompiled with JetBrains decompiler
// Type: PX.Common.Context.SlotStorageProviderExtensions
// Assembly: PX.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35AC74DC-4190-4222-F56C-8CF32A9F1C93
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Common.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Common.xml

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Common.Context;

internal static class SlotStorageProviderExtensions
{
  internal static void Remove(this ISlotStorageProvider _param0, Func<string, object, bool> _param1)
  {
    SlotStorageProviderExtensions.\u0002 obj = new SlotStorageProviderExtensions.\u0002();
    obj.\u0002 = _param1;
    obj.\u000E = _param0;
    EnumerableExtensions.ForEach<KeyValuePair<string, object>>((IEnumerable<KeyValuePair<string, object>>) Enumerable.ToArray<KeyValuePair<string, object>>(obj.\u000E.Items().Where<KeyValuePair<string, object>>(new Func<KeyValuePair<string, object>, bool>(obj.\u0002))), new Action<KeyValuePair<string, object>>(obj.\u0002));
  }

  private sealed class \u0002
  {
    public Func<string, object, bool> \u0002;
    public ISlotStorageProvider \u000E;

    internal bool \u0002(KeyValuePair<string, object> _param1)
    {
      return _param1.Key != null && this.\u0002(_param1.Key, _param1.Value);
    }

    internal void \u0002(KeyValuePair<string, object> _param1)
    {
      ((ISlotStore) this.\u000E).Remove(_param1.Key);
    }
  }
}
