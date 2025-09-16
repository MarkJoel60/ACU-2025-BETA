// Decompiled with JetBrains decompiler
// Type: PX.Data.Services.Implementations.ContextPXIdentityAccessor
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System.Globalization;
using System.Security.Principal;

#nullable disable
namespace PX.Data.Services.Implementations;

internal class ContextPXIdentityAccessor : IPXIdentityAccessor
{
  private readonly ICurrentUserInformationProvider _currentUserInformationProvider;

  public ContextPXIdentityAccessor(
    ICurrentUserInformationProvider currentUserInformationProvider)
  {
    this._currentUserInformationProvider = currentUserInformationProvider;
  }

  public IPXIdentity Identity
  {
    get
    {
      return !PXContext.PXIdentity.Authenticated ? (IPXIdentity) null : (IPXIdentity) new ContextPXIdentityAccessor.PXIdentity(this._currentUserInformationProvider.GetUserName(), PXAccess.GetCompanyName(), PXAccess.GetBranchID(), PXContext.PXIdentity.Culture ?? PXCultureInfo.InvariantCulture, PXContext.GetBusinessDate() ?? PXTimeZoneInfo.Today, LocaleInfo.GetTimeZone(), PXContext.PXIdentity.User);
    }
  }

  private class PXIdentity : IPXIdentity
  {
    public PXIdentity(
      string username,
      string tenantId,
      int? branchId,
      CultureInfo culture,
      System.DateTime businessDate,
      PXTimeZoneInfo timeZone,
      IPrincipal user)
    {
      this.Username = username;
      this.TenantId = tenantId;
      this.BranchId = branchId;
      this.Culture = culture;
      this.BusinessDate = businessDate;
      this.TimeZone = timeZone;
      this.User = user;
    }

    public string Username { get; }

    public string TenantId { get; }

    public int? BranchId { get; }

    public CultureInfo Culture { get; }

    public System.DateTime BusinessDate { get; }

    public PXTimeZoneInfo TimeZone { get; }

    public IPrincipal User { get; }
  }
}
