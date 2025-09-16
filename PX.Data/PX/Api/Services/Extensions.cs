// Decompiled with JetBrains decompiler
// Type: PX.Api.Services.Extensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using System;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Security;

#nullable disable
namespace PX.Api.Services;

[PXInternalUseOnly]
public static class Extensions
{
  internal static bool? CheckPrefix(this ILoginService _, IPrincipal user, string prefix)
  {
    return LoginService.CheckPrefix(user, prefix);
  }

  public static bool CheckPrefix(this FormsIdentity formsIdentity, string prefix)
  {
    return prefix.OrdinalEquals(formsIdentity.GetPrefix());
  }

  public static string GetPrefix(this FormsIdentity identity) => identity.Ticket.UserData;

  public static async Task ExecuteAsAdminForAllTenantsAsync(
    this ILoginService loginService,
    Func<CancellationToken, Task> action,
    CancellationToken cancellationToken)
  {
    string[] strArray1 = PXDatabase.Companies;
    if (strArray1.Length == 0)
      strArray1 = new string[1];
    string[] strArray = strArray1;
    for (int index = 0; index < strArray.Length; ++index)
    {
      string company = strArray[index];
      if (cancellationToken.IsCancellationRequested)
        return;
      using (loginService.GetAdminLoginScope(company))
        await action(cancellationToken);
    }
    strArray = (string[]) null;
  }
}
