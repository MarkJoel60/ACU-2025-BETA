// Decompiled with JetBrains decompiler
// Type: PX.Data.PXSqlLimits
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using CommonServiceLocator;
using System;
using System.Data;
using System.Linq;

#nullable disable
namespace PX.Data;

internal class PXSqlLimits
{
  public static IPXSqlLimits _provider = PXSqlLimits.GetProvider();

  private static IPXSqlLimits GetProvider()
  {
    try
    {
      if (ServiceLocator.IsLocationProviderSet)
        return ServiceLocator.Current.GetAllInstances<IPXSqlLimits>().FirstOrDefault<IPXSqlLimits>() ?? (IPXSqlLimits) new PXSqlLimitsDefaultImplementation();
    }
    catch
    {
    }
    return (IPXSqlLimits) new PXSqlLimitsDefaultImplementation();
  }

  internal static bool VerifyRowCountLimit(ref long topCount)
  {
    return PXSqlLimits._provider.VerifyRowCountLimit(ref topCount);
  }

  internal static void LimitExceeded(IDbCommand cmd) => PXSqlLimits._provider.LimitExceeded(cmd);

  public static void BeforeExecuteReader(IDbCommand command)
  {
    PXSqlLimits._provider.BeforeExecuteReader(command);
  }

  public static void ThrowSqlTimeout(IDbCommand cmd) => PXSqlLimits._provider.ThrowSqlTimeout(cmd);

  public static TimeSpan? GetSqlTimeLimit(IDbCommand cmd)
  {
    return PXSqlLimits._provider.GetSqlTimeLimit(cmd);
  }
}
