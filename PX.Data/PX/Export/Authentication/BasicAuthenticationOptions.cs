// Decompiled with JetBrains decompiler
// Type: PX.Export.Authentication.BasicAuthenticationOptions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;

#nullable disable
namespace PX.Export.Authentication;

internal sealed class BasicAuthenticationOptions : AuthenticationSchemeOptions
{
  public bool SetResponseStatusCode { get; set; }

  public Func<HttpContext, string, string> PrepareUsername { get; set; }

  public virtual void Validate()
  {
    if (this.PrepareUsername == null)
      throw new InvalidOperationException("PrepareUsername is not set");
  }
}
