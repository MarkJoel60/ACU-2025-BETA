// Decompiled with JetBrains decompiler
// Type: PX.Data.LoginAsUser
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.Extensions.Configuration;
using PX.Common;
using PX.Common.Configuration;
using PX.Data.Access.LoginAs;
using System.Collections.Generic;
using System.Threading;
using System.Web;

#nullable disable
namespace PX.Data;

internal sealed class LoginAsUser : ILoginAsUser
{
  private readonly ILoginAsCookieProvider _cookieProvider;
  private readonly bool _isClusterEnabled;
  private readonly ReaderWriterLock _loggedAsUserLock = new ReaderWriterLock();
  private readonly Dictionary<string, string> _loggedAsUser = new Dictionary<string, string>();
  private readonly object _clusterLoggedAsUserLock = new object();

  public LoginAsUser(ILoginAsCookieProvider cookieProvider, IConfiguration configuration)
  {
    this._cookieProvider = cookieProvider;
    this._isClusterEnabled = configuration.IsClusterEnabled();
  }

  private Dictionary<string, string> LoggedAsUser
  {
    get
    {
      if (!this._isClusterEnabled)
        return this._loggedAsUser;
      Dictionary<string, string> loggedAsUser = (Dictionary<string, string>) PXContext.Session[nameof (LoggedAsUser)];
      if (loggedAsUser != null)
        return loggedAsUser;
      lock (this._clusterLoggedAsUserLock)
      {
        if (loggedAsUser == null)
        {
          loggedAsUser = new Dictionary<string, string>();
          PXContext.Session[nameof (LoggedAsUser)] = (object) loggedAsUser;
        }
      }
      return loggedAsUser;
    }
  }

  void ILoginAsUser.LoginAsUser(
    string userName,
    string userNameOriginal,
    ILoginAsUserSession session)
  {
    this.LoginAsUserImpl(userName);
    PXReaderWriterScope readerWriterScope;
    // ISSUE: explicit constructor call
    ((PXReaderWriterScope) ref readerWriterScope).\u002Ector(this._loggedAsUserLock);
    try
    {
      ((PXReaderWriterScope) ref readerWriterScope).AcquireWriterLock();
      this.LoggedAsUser[session.SessionId] = userNameOriginal;
    }
    finally
    {
      readerWriterScope.Dispose();
    }
  }

  void ILoginAsUser.LoginAsUser(string userName) => this.LoginAsUserImpl(userName);

  private void LoginAsUserImpl(string userName)
  {
    this._cookieProvider.Write(HttpContext.Current, userName);
  }

  string ILoginAsUser.GetLoggedAsUserName(ILoginAsUserSession session)
  {
    PXReaderWriterScope readerWriterScope;
    // ISSUE: explicit constructor call
    ((PXReaderWriterScope) ref readerWriterScope).\u002Ector(this._loggedAsUserLock);
    try
    {
      ((PXReaderWriterScope) ref readerWriterScope).AcquireReaderLock();
      string loggedAsUserName;
      if (this.LoggedAsUser.TryGetValue(session.SessionId, out loggedAsUserName))
      {
        if (!string.IsNullOrEmpty(loggedAsUserName))
          return loggedAsUserName;
      }
    }
    finally
    {
      readerWriterScope.Dispose();
    }
    return (string) null;
  }

  void ILoginAsUser.RemoveLoggedAsUser(ILoginAsUserSession session)
  {
    this._cookieProvider.Remove(HttpContext.Current);
    PXReaderWriterScope readerWriterScope;
    // ISSUE: explicit constructor call
    ((PXReaderWriterScope) ref readerWriterScope).\u002Ector(this._loggedAsUserLock);
    try
    {
      ((PXReaderWriterScope) ref readerWriterScope).AcquireReaderLock();
      string sessionId = session.SessionId;
      if (!this.LoggedAsUser.ContainsKey(sessionId))
        return;
      ((PXReaderWriterScope) ref readerWriterScope).UpgradeToWriterLock();
      if (!this.LoggedAsUser.ContainsKey(sessionId))
        return;
      this.LoggedAsUser.Remove(sessionId);
    }
    finally
    {
      readerWriterScope.Dispose();
    }
  }
}
