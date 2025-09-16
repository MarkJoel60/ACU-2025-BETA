// Decompiled with JetBrains decompiler
// Type: PX.Licensing.LicensingSessionExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System.Web;
using System.Web.SessionState;

#nullable enable
namespace PX.Licensing;

internal static class LicensingSessionExtensions
{
  internal static ILicensingSession? TryGetLicensingSession(this HttpContext httpContext)
  {
    HttpSessionState session = httpContext.Session;
    return session == null ? (ILicensingSession) null : session.ToLicensingSession();
  }

  internal static ILicensingSession ToLicensingSession(this HttpSessionState session)
  {
    return (ILicensingSession) new AspNetSession(session);
  }

  internal static ILicensingSession ToLicensingSession(this HttpSessionStateBase session)
  {
    return (ILicensingSession) new AspNetSession(session);
  }
}
