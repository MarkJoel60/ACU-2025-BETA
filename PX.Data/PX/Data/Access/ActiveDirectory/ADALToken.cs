// Decompiled with JetBrains decompiler
// Type: PX.Data.Access.ActiveDirectory.ADALToken
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.IdentityModel.Clients.ActiveDirectory;
using PX.Common;
using System;

#nullable disable
namespace PX.Data.Access.ActiveDirectory;

[PXInternalUseOnly]
public class ADALToken
{
  public AuthenticationResult AdalToken { get; set; }

  public string TokenType { get; set; }

  public string AccessToken { get; set; }

  public DateTimeOffset ExpiresOn { get; set; }

  public bool IsExpired => this.WillExpireIn(0);

  public bool WillExpireIn(int minutes)
  {
    return DateTimeOffset.UtcNow.AddMinutes((double) minutes) > this.ExpiresOn;
  }
}
