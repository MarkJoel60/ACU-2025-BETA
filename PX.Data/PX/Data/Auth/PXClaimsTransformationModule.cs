// Decompiled with JetBrains decompiler
// Type: PX.Data.Auth.PXClaimsTransformationModule
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using CommonServiceLocator;
using PX.Common;
using PX.Data.Access.ActiveDirectory;
using System.Security.Claims;

#nullable disable
namespace PX.Data.Auth;

[PXInternalUseOnly]
public class PXClaimsTransformationModule : ClaimsAuthenticationManager
{
  public override ClaimsPrincipal Authenticate(
    string resourceName,
    ClaimsPrincipal incomingPrincipal)
  {
    if (incomingPrincipal != null && incomingPrincipal.Identity.IsAuthenticated)
    {
      Claim claim1 = (Claim) null;
      Claim claim2 = (Claim) null;
      foreach (Claim claim3 in incomingPrincipal.Claims)
      {
        switch (claim3.Type)
        {
          case "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name":
            claim1 = claim3;
            continue;
          case "http://schemas.microsoft.com/identity/claims/objectidentifier":
            claim2 = claim3;
            continue;
          default:
            continue;
        }
      }
      if (claim1 == null && claim2 != null)
      {
        User userBySid = ServiceLocator.Current.GetInstance<IActiveDirectoryProvider>().GetUserBySID(claim2.Value);
        if (userBySid != null)
          ((ClaimsIdentity) incomingPrincipal.Identity).AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", userBySid.Name.Name));
      }
    }
    return base.Authenticate(resourceName, incomingPrincipal);
  }
}
