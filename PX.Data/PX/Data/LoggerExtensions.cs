// Decompiled with JetBrains decompiler
// Type: PX.Data.LoggerExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Serilog;
using Serilog.Events;
using System.Runtime.CompilerServices;

#nullable disable
namespace PX.Data;

internal static class LoggerExtensions
{
  public static ILogger ForContext(
    this ILogger logger,
    LicenseBucket licenseBucket,
    System.DateTime? date = null)
  {
    if (logger.IsEnabled((LogEventLevel) 1))
      logger = logger.ForContext("LicenseRestriction", (object) licenseBucket.Restriction, false);
    return logger.ForContext("LicenseSignature", (object) licenseBucket.Signature, false).ForContext("LicenseKey", (object) licenseBucket.LicenseKey, false).ForContext("LicenseDate", (object) (date ?? licenseBucket.Date), false);
  }

  public static ILogger ForMethodContext(this ILogger logger, [CallerMemberName] string memberName = null)
  {
    return memberName != null ? logger.ForContext("MemberContext", (object) memberName, false) : logger;
  }
}
