// Decompiled with JetBrains decompiler
// Type: PX.Common.PXSessionContext
// Assembly: PX.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35AC74DC-4190-4222-F56C-8CF32A9F1C93
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Common.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Common.xml

using System;
using System.Globalization;
using System.Security.Principal;
using System.Threading;
using System.Web;

#nullable disable
namespace PX.Common;

[Serializable]
public sealed class PXSessionContext
{
  internal string UserName;
  [Obsolete("Use MarkSaved/WasSaved")]
  internal bool Sessioned;
  internal bool Requested;
  internal bool Invalidated;
  [NonSerialized]
  internal IPrincipal Principals;

  public PXSessionContext()
  {
  }

  public PXSessionContext(IPrincipal user, CultureInfo culture, DateTime date)
  {
    this.Principals = user;
    this.Culture = this.UICulture = culture;
    this.BusinessDate = new DateTime?(date);
  }

  public int? BranchID { get; internal set; }

  public CultureInfo Culture { get; internal set; }

  public CultureInfo UICulture { get; internal set; }

  public PXTimeZoneInfo TimeZone { get; internal set; }

  public DateTime? BusinessDate { get; internal set; }

  public IPrincipal User
  {
    get
    {
      if (this.Principals != null)
        return this.Principals;
      return Thread.CurrentPrincipal != null && Thread.CurrentPrincipal.Identity != null && !string.IsNullOrEmpty(Thread.CurrentPrincipal.Identity.Name) || HttpContext.Current == null ? Thread.CurrentPrincipal : HttpContext.Current.User;
    }
  }

  internal void SetUser(IPrincipal _param1) => this.Principals = _param1;

  internal void MarkSaved() => this.Sessioned = true;

  internal bool WasSaved => this.Sessioned;

  public IPrincipal AuthUser => !this.Authenticated ? (IPrincipal) null : this.User;

  public bool Authenticated => !Anonymous.IsAnonymous(this.User);

  public string IdentityName => this.User?.Identity?.Name;

  public PXSessionContext Clone()
  {
    PXSessionContext pxSessionContext = (PXSessionContext) this.MemberwiseClone();
    pxSessionContext.Principals = this.Principals;
    return pxSessionContext;
  }
}
