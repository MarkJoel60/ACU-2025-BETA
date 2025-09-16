// Decompiled with JetBrains decompiler
// Type: PX.Data.PXImpersonationContext
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Security.Principal;
using System.Web;
using System.Web.Configuration;

#nullable disable
namespace PX.Data;

[PXInternalUseOnly]
public class PXImpersonationContext : IDisposable
{
  private IPrincipal PrevPrincipal;
  private readonly WindowsImpersonationContext PrevIdentity;

  public PXImpersonationContext()
    : this(PXDatabase.Companies.Length != 0 ? "sys@" + PXDatabase.Companies[0] : "sys")
  {
  }

  public PXImpersonationContext(string userName)
    : this(userName, new string[0])
  {
  }

  public PXImpersonationContext(string userName, string[] roles)
  {
    WindowsIdentity windowsIdentity = (WindowsIdentity) null;
    IPrincipal principal = (IPrincipal) new GenericPrincipal((IIdentity) new GenericIdentity(userName), roles);
    IdentitySection section = (IdentitySection) WebConfigurationManager.GetSection("system.web/identity");
    if (section != null && section.Impersonate && HttpContext.Current != null)
    {
      HttpWorkerRequest service = (HttpWorkerRequest) ((IServiceProvider) HttpContext.Current).GetService(typeof (HttpWorkerRequest));
      if (service != null)
      {
        IntPtr userToken = service.GetUserToken();
        if (userToken != IntPtr.Zero)
          windowsIdentity = new WindowsIdentity(userToken);
      }
    }
    this.PrevPrincipal = PXContext.PXIdentity.User;
    PXContext.PXIdentity.SetUser(principal);
    if (windowsIdentity == null)
      return;
    this.PrevIdentity = windowsIdentity.Impersonate();
  }

  public PXImpersonationContext(string userName, WindowsIdentity identity)
  {
    IPrincipal principal = (IPrincipal) new GenericPrincipal((IIdentity) new GenericIdentity(userName), new string[0]);
    this.PrevPrincipal = PXContext.PXIdentity.User;
    PXContext.PXIdentity.SetUser(principal);
    if (identity == null)
      return;
    this.PrevIdentity = identity.Impersonate();
  }

  public void Dispose()
  {
    PXContext.PXIdentity.SetUser(this.PrevPrincipal);
    if (this.PrevIdentity != null)
      this.PrevIdentity.Undo();
    PXDatabase.ResetCredentials();
  }
}
