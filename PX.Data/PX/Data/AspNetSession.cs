// Decompiled with JetBrains decompiler
// Type: PX.Data.AspNetSession
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Licensing;
using System;
using System.Web;
using System.Web.SessionState;

#nullable enable
namespace PX.Data;

[PXInternalUseOnly]
public sealed class AspNetSession : ILicensingSession, ILoginAsUserSession
{
  private readonly string _sessionId;

  private AspNetSession(string sessionId)
  {
    switch (sessionId)
    {
      case null:
        throw new ArgumentNullException(nameof (sessionId));
      case "":
        throw new ArgumentOutOfRangeException(nameof (sessionId));
      default:
        this._sessionId = sessionId;
        break;
    }
  }

  internal AspNetSession(HttpSessionState session)
    : this((session ?? throw new ArgumentNullException(nameof (session))).SessionID)
  {
  }

  internal AspNetSession(HttpSessionStateBase session)
    : this((session ?? throw new ArgumentNullException(nameof (session))).SessionID)
  {
  }

  string ILicensingSession.SessionId => this._sessionId;

  string ILoginAsUserSession.SessionId => this._sessionId;

  internal static AspNetSession? TryCreateFrom(HttpContextBase context)
  {
    HttpSessionStateBase session = context.Session;
    return session == null ? (AspNetSession) null : new AspNetSession(session);
  }

  internal static AspNetSession? TryCreateFrom(HttpContext? context)
  {
    if (context != null)
    {
      HttpSessionState session = context.Session;
      if (session != null)
        return new AspNetSession(session);
    }
    return (AspNetSession) null;
  }
}
