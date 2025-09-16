// Decompiled with JetBrains decompiler
// Type: PX.Data.Maintenance.PXAppRestartHelper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Web;

#nullable disable
namespace PX.Data.Maintenance;

internal static class PXAppRestartHelper
{
  /// <summary>
  /// Restarts application domain in the current cluster node.
  /// </summary>
  public static void RestartApplication() => HttpRuntime.UnloadAppDomain();
}
