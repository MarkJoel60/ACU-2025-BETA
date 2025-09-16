// Decompiled with JetBrains decompiler
// Type: PX.Data.PXSqlLimitsDefaultImplementation
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Data;
using System.Web;

#nullable disable
namespace PX.Data;

public class PXSqlLimitsDefaultImplementation : IPXSqlLimits
{
  public void BeforeExecuteReader(IDbCommand command)
  {
    if (!WebConfig.SqlLimitsEnabled)
      return;
    int num = PXSqlLimitsDefaultImplementation.IsBigDataContext() ? WebConfig.SqlTimeoutSec : WebConfig.SqlTimeoutUISec;
    if (command.CommandTimeout <= num)
      return;
    command.CommandTimeout = num;
  }

  public void ThrowSqlTimeout(IDbCommand cmd)
  {
    throw new PXCustomErrorPageException("Denial Code 510. The system was unable to gather the data needed to complete the operation due to SQL timeout constraints. Please try to optimize the operation or contact your Acumatica support provider for further assistance.");
  }

  public TimeSpan? GetSqlTimeLimit(IDbCommand cmd)
  {
    return !WebConfig.SqlLimitsEnabled ? new TimeSpan?() : new TimeSpan?(new TimeSpan(0, 0, PXSqlLimitsDefaultImplementation.IsBigDataContext() ? WebConfig.SqlTimeoutSec : WebConfig.SqlTimeoutUISec));
  }

  public void LimitExceeded(IDbCommand cmd)
  {
    throw new PXCustomErrorPageException("Denial Code 513. The system was unable to process the request due to data size limits. Please review your operation and consider requesting a smaller dataset. Contact your provider for further assistance.");
  }

  public bool VerifyRowCountLimit(ref long topCount)
  {
    if (!WebConfig.SqlLimitsEnabled)
      return false;
    string key = PXContext.GetScreenID() ?? "";
    if (WebConfig.SqlLimitExceptions.ContainsKey(key))
      return false;
    int num = PXSqlLimitsDefaultImplementation.IsBigDataContext() ? WebConfig.MaxSqlRows : WebConfig.MaxSqlRowsUI;
    if (topCount > 0L && topCount <= (long) num)
      return false;
    topCount = (long) num;
    return true;
  }

  private static bool IsBigDataContext()
  {
    try
    {
      return HttpContext.Current == null || HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.StartsWith("~/odata", StringComparison.OrdinalIgnoreCase);
    }
    catch
    {
      return true;
    }
  }
}
