// Decompiled with JetBrains decompiler
// Type: PX.Data.PXLoginScope
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Security.Principal;

#nullable disable
namespace PX.Data;

public sealed class PXLoginScope : IDisposable
{
  private IPrincipal prev;
  private string exactUserName;
  private string companyName;
  private string _branch;
  private string[] roles;
  private bool _Keep;

  public PXLoginScope(string userName, params string[] ruleGroups)
  {
    PXDatabase.ResetCredentials();
    this.roles = ruleGroups;
    this.exactUserName = userName;
    if (string.IsNullOrEmpty(userName))
      return;
    this.prev = PXContext.PXIdentity.Principals;
    PXContext.PXIdentity.SetUser((IPrincipal) new GenericPrincipal((IIdentity) new GenericIdentity(userName), ruleGroups));
    LegacyCompanyService.ParseLogin(userName, out this.exactUserName, out this.companyName, out this._branch);
  }

  void IDisposable.Dispose()
  {
    if (!this._Keep)
      PXContext.PXIdentity.SetUser(this.prev);
    PXDatabase.ResetCredentials();
  }

  [Obsolete("Set PXContext.PXIdentity.User directly")]
  public void Keep() => this._Keep = true;

  public string ExtractedUserName => this.exactUserName;

  [Obsolete("For reading, use ExtractedUserName. Instead of writing, create a new PXLoginScope")]
  public string UserName
  {
    get => this.exactUserName;
    set
    {
      if (string.IsNullOrEmpty(value))
        return;
      this.exactUserName = value;
      PXContext.PXIdentity.SetUser((IPrincipal) new GenericPrincipal((IIdentity) new GenericIdentity(LegacyCompanyService.ConcatLogin(this.exactUserName, this.companyName, this._branch)), this.roles));
    }
  }

  public string CompanyName => this.companyName;

  public string Branch => this._branch;
}
