// Decompiled with JetBrains decompiler
// Type: PX.Common.Async
// Assembly: PX.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35AC74DC-4190-4222-F56C-8CF32A9F1C93
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Common.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Common.xml

using System;

#nullable enable
namespace PX.Common;

internal static class Async
{
  private static readonly string \u0002 = typeof (Async).FullName + ".StatusSetter";

  internal static void SetStatusSetter(Action<string> _param0)
  {
    PXContext.SetSlot<Action<string>>(Async.\u0002, _param0);
  }

  internal static void SetStatus(string _param0)
  {
    Action<string> slot = PXContext.GetSlot<Action<string>>(Async.\u0002);
    if (slot == null)
      return;
    slot(_param0);
  }
}
