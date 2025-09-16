// Decompiled with JetBrains decompiler
// Type: PX.Data.PXAuditJournal
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Autofac;
using CommonServiceLocator;
using Microsoft.Extensions.Options;
using PX.Common;
using PX.Common.Context;
using PX.Common.Extensions;
using PX.Data.DependencyInjection;
using PX.Data.Update;
using PX.SM;
using System;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;

#nullable disable
namespace PX.Data;

[PXInternalUseOnly]
public static class PXAuditJournal
{
  private static readonly Lazy<ILoginAsUser> _loginAsUser = new Lazy<ILoginAsUser>((Func<ILoginAsUser>) (() => ServiceLocator.Current.GetInstance<ILoginAsUser>()));

  public static void Register(PXAuditJournal.Operation operation, string username)
  {
    PXAuditJournal.Register(operation, username, PXAuditJournal.CurrentIPAddress, (string) null, (string) null);
  }

  internal static void RegisterSessionExpired(
    ILoginAsUserSession session,
    HttpServerUtility server,
    string username)
  {
    PXAuditJournal.Register(session, server, PXAuditJournal.Operation.SessionExpired, username, (string) null, (string) null, (string) null);
  }

  internal static void RegisterODataV4Accessed(string comment)
  {
    PXAuditJournal.Register(PXAuditJournal.Operation.ODataV4Accessed, (string) null, (string) null, comment);
  }

  internal static void RegisterODataAccessed(string comment)
  {
    PXAuditJournal.Register(PXAuditJournal.Operation.ODataAccessed, (string) null, (string) null, comment);
  }

  public static void Register(
    PXAuditJournal.Operation operation,
    string username,
    string ipAddress,
    string comment)
  {
    PXAuditJournal.Register(operation, username ?? PXAccess.GetUserName(), ipAddress ?? PXAuditJournal.CurrentIPAddress, (string) null, comment);
  }

  public static void Register(string screenId)
  {
    PXAuditJournal.Register(PXAuditJournal.Operation.AccessScreen, PXAccess.GetUserName(), PXAuditJournal.CurrentIPAddress, screenId, (string) null);
  }

  public static void Register(string screenId, string comment)
  {
    PXAuditJournal.Register(PXAuditJournal.Operation.AccessScreen, PXAccess.GetUserName(), PXAuditJournal.CurrentIPAddress, screenId, comment);
  }

  public static void RegisterCustomizationPublished(string projects, string user)
  {
    PXAuditJournal.Register(PXAuditJournal.Operation.CustomizationPublished, user, PXAuditJournal.CurrentIPAddress, (string) null, projects);
  }

  private static void Register(
    PXAuditJournal.Operation operation,
    string username,
    string ipAddress,
    string screenId,
    string comment)
  {
    PXAuditJournal.Register((ILoginAsUserSession) AspNetSession.TryCreateFrom(HttpContext.Current), HttpContext.Current?.Server, operation, username, ipAddress, screenId, comment);
  }

  private static void Register(
    ILoginAsUserSession session,
    HttpServerUtility server,
    PXAuditJournal.Operation operation,
    string username,
    string ipAddress,
    string screenId,
    string comment)
  {
    screenId = screenId?.Replace(".", "");
    if (username == null)
      username = "Unknown";
    string hostName = PXInstanceHelper.GetHostName(server) ?? "Unknown";
    int num = (operation & (PXAuditJournal.Operation) SitePolicy.AuditOperationMask) > PXAuditJournal.Operation.None || operation == PXAuditJournal.Operation.CustomizationPublished || operation == PXAuditJournal.Operation.AccessScreen || operation == PXAuditJournal.Operation.ODataAccessed ? 1 : (operation == PXAuditJournal.Operation.ODataV4Accessed ? 1 : 0);
    if (session != null)
    {
      string loggedAsUserName = PXAuditJournal._loginAsUser.Value.GetLoggedAsUserName(session);
      if (loggedAsUserName != null)
        username = $"{loggedAsUserName} as {username}";
    }
    if (num == 0 || PXGraph.ProxyIsActive)
      return;
    int? companyId = SlotStore.Instance.GetSingleCompanyId();
    IPrincipal user = PXContext.PXIdentity.User;
    Task.Run((System.Action) (() =>
    {
      using (IDisposableSlotStorageProvider slots = SlotStore.AsyncLocal())
      {
        using (ILifetimeScope ilifetimeScope = ((ISlotStore) slots).BeginLifetimeScope())
        {
          if (companyId.HasValue)
            ((ISlotStore) slots).SetSingleCompanyId(companyId.GetValueOrDefault());
          PXContext.PXIdentity.SetUser(user);
          using (PXTransactionScope transactionScope = new PXTransactionScope(true))
          {
            try
            {
              PXDatabase.Insert<LoginTrace>(new PXDataFieldAssign("ApplicationName", PXDbType.VarChar, new int?(32 /*0x20*/), (object) ((ilifetimeScope != null ? ResolutionExtensions.ResolveOptional<IOptions<AuditJournalOptions>>((IComponentContext) ilifetimeScope)?.Value.ApplicationName : (string) null) ?? "")), new PXDataFieldAssign("Host", PXDbType.NVarChar, new int?(256 /*0x0100*/), (object) hostName), new PXDataFieldAssign("Date", PXDbType.DateTime, new int?(8), (object) System.DateTime.Now.ToUniversalTime()), new PXDataFieldAssign("Username", PXDbType.VarChar, new int?(128 /*0x80*/), (object) username), new PXDataFieldAssign("Operation", PXDbType.Int, new int?(4), (object) (int) operation), new PXDataFieldAssign("IPAddress", PXDbType.VarChar, new int?(50), (object) ipAddress), new PXDataFieldAssign("ScreenID", PXDbType.VarChar, new int?(12), (object) screenId), new PXDataFieldAssign("Comment", PXDbType.VarChar, new int?(1000), (object) StringExtensions.Truncate(comment, 1000)), new PXDataFieldAssign("NoteID", (object) Guid.NewGuid()));
              transactionScope.Complete();
            }
            catch
            {
            }
          }
        }
      }
    }));
  }

  private static string CurrentIPAddress
  {
    get
    {
      return HttpContext.Current == null || HttpContext.Current.Request == null ? "System" : HttpContext.Current.Request.GetUserHostAddress();
    }
  }

  [Flags]
  public enum Operation
  {
    None = 0,
    Login = 1,
    Logout = 2,
    SessionExpired = 4,
    LoginFailed = 8,
    AccessScreen = 16, // 0x00000010
    SendMail = 32, // 0x00000020
    SendMailFailed = 64, // 0x00000040
    CustomizationPublished = 128, // 0x00000080
    LicenseExceeded = 256, // 0x00000100
    SnapshotRestored = 512, // 0x00000200
    ODataAccessed = 1024, // 0x00000400
    ODataV4Accessed = 2048, // 0x00000800
    MaskLogins = SnapshotRestored | LicenseExceeded | LoginFailed | Login, // 0x00000309
    MaskSessions = MaskLogins | SessionExpired | Logout, // 0x0000030F
    MaskAll = MaskSessions | ODataV4Accessed | ODataAccessed | CustomizationPublished | SendMailFailed | SendMail | AccessScreen, // 0x00000FFF
  }

  /// <exclude />
  public class OperationListAttribute : PXIntListAttribute
  {
    public OperationListAttribute()
      : base(new int[11]
      {
        1,
        2,
        4,
        8,
        16 /*0x10*/,
        32 /*0x20*/,
        64 /*0x40*/,
        128 /*0x80*/,
        256 /*0x0100*/,
        1024 /*0x0400*/,
        2048 /*0x0800*/
      }, new string[11]
      {
        "Login",
        "Logout",
        "Session Expired",
        "Login Failed",
        "Access Screen",
        "Send Email Success",
        "Send Email Error",
        "Customization Published",
        "License Exceeded",
        "OData Refresh",
        "ODataV4 Refresh"
      })
    {
    }
  }
}
