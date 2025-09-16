// Decompiled with JetBrains decompiler
// Type: PX.Common.HttpContextExtensions
// Assembly: PX.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35AC74DC-4190-4222-F56C-8CF32A9F1C93
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Common.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Common.xml

using System;
using System.Threading;
using System.Web;
using System.Web.SessionState;

#nullable disable
namespace PX.Common;

public static class HttpContextExtensions
{
  private static ISessionIDManager \u0002;

  public static string CreateSessionId(this HttpContext ctx)
  {
    return LazyInitializer.EnsureInitialized<ISessionIDManager>(ref HttpContextExtensions.\u0002, HttpContextExtensions.\u0002.\u000E ?? (HttpContextExtensions.\u0002.\u000E = new Func<ISessionIDManager>(HttpContextExtensions.\u0002.\u0002.\u0002))).CreateSessionID(ctx);
  }

  [Serializable]
  private sealed class \u0002
  {
    public static readonly HttpContextExtensions.\u0002 \u0002 = new HttpContextExtensions.\u0002();
    public static Func<ISessionIDManager> \u000E;

    internal ISessionIDManager \u0002() => (ISessionIDManager) new SessionIDManager();
  }
}
