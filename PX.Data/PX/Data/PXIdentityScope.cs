// Decompiled with JetBrains decompiler
// Type: PX.Data.PXIdentityScope
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
public class PXIdentityScope : IDisposable
{
  private readonly WindowsImpersonationContext PrevIdentity;

  public PXIdentityScope()
  {
    IdentitySection section = (IdentitySection) WebConfigurationManager.GetSection("system.web/identity");
    if (section == null || !section.Impersonate || HttpContext.Current == null)
      return;
    HttpWorkerRequest service = (HttpWorkerRequest) ((IServiceProvider) HttpContext.Current).GetService(typeof (HttpWorkerRequest));
    if (service == null)
      return;
    IntPtr userToken = service.GetUserToken();
    if (!(userToken != IntPtr.Zero))
      return;
    this.PrevIdentity = WindowsIdentity.Impersonate(userToken);
  }

  public void Dispose()
  {
    if (this.PrevIdentity == null)
      return;
    this.PrevIdentity.Undo();
  }
}
