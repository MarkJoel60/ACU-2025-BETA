// Decompiled with JetBrains decompiler
// Type: PX.Common.Session.SessionStandInExtensions
// Assembly: PX.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35AC74DC-4190-4222-F56C-8CF32A9F1C93
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Common.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Common.xml

using PX.Common.Context;
using System;

#nullable enable
namespace PX.Common.Session;

internal static class SessionStandInExtensions
{
  internal static IPXSessionState? GetSessionStandIn(this ISlotStore _param0)
  {
    return TypeKeyedOperationExtensions.Get<IPXSessionState>(_param0);
  }

  internal static IDisposable UseSessionStandIn(this ISlotStore _param0, IPXSessionState _param1)
  {
    return UseSlotExtensions.Use<IPXSessionState>(_param0, _param1);
  }
}
