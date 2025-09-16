// Decompiled with JetBrains decompiler
// Type: PX.Data.Auth.DatabaseIssuerNameRegistry
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System.IdentityModel.Tokens;

#nullable disable
namespace PX.Data.Auth;

[PXInternalUseOnly]
public class DatabaseIssuerNameRegistry : ValidatingIssuerNameRegistry
{
  protected virtual bool IsThumbprintValid(string thumbprint, string issuer) => true;
}
