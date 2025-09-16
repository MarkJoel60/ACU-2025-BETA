// Decompiled with JetBrains decompiler
// Type: PX.Data.MultiFactorAuth.MultifactorServiceHelper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.AspNetCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web;

#nullable disable
namespace PX.Data.MultiFactorAuth;

public static class MultifactorServiceHelper
{
  public static void GenerateCodesAndShowReport(
    this IMultiFactorService multiFactorService,
    Guid userId)
  {
    int lifetime = 7;
    multiFactorService.GeneratePersistentCodes(userId, 10, lifetime);
    Dictionary<string, string> parameters = new Dictionary<string, string>();
    parameters.Add("UserID", userId.ToString());
    System.DateTime dateTime = System.DateTime.UtcNow;
    dateTime = dateTime.Date;
    dateTime = dateTime.AddDays(7.0);
    parameters.Add("Date", dateTime.ToString((IFormatProvider) CultureInfo.InvariantCulture));
    throw new PXReportRequiredException(parameters, "SM651011", PXBaseRedirectException.WindowMode.NewWindow, "Access Codes");
  }

  public static void RememberDevice(
    this IMultiFactorService multiFactorService,
    string login,
    string password,
    HttpContext context)
  {
    multiFactorService.RememberDevice(login, password, context.GetCoreHttpContext());
  }
}
