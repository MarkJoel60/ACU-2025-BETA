// Decompiled with JetBrains decompiler
// Type: PX.Async.ILongOperationManagerExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;

#nullable enable
namespace PX.Async;

public static class ILongOperationManagerExtensions
{
  /// <summary>
  /// <para>Returns the <see cref="T:PX.Data.PXLongRunStatus" /> status of the long-running operation specified by the <paramref name="key" /> parameter.</para>
  /// </summary>
  /// <param name="key">The ID of the long-running operation.</param>
  public static PXLongRunStatus GetStatus(this ILongOperationManager manager, object key)
  {
    return manager.GetOperationDetails(key).Status;
  }
}
