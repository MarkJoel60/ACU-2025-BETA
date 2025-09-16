// Decompiled with JetBrains decompiler
// Type: PX.Data.Services.SessionContextService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Web;
using System.Web.Security;

#nullable disable
namespace PX.Data.Services;

/// <exclude />
public class SessionContextService : ISessionContextService
{
  public bool IsApiRequest
  {
    get
    {
      if (HttpContext.Current != null && HttpContext.Current.User != null && HttpContext.Current.User.Identity is FormsIdentity identity && identity.Ticket != null)
      {
        FormsAuthenticationTicket ticket = identity.Ticket;
        if (!string.IsNullOrEmpty(ticket.UserData) && !ticket.UserData.Equals("/", StringComparison.Ordinal))
          return true;
      }
      return false;
    }
  }
}
