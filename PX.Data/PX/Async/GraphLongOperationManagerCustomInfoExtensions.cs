// Decompiled with JetBrains decompiler
// Type: PX.Async.GraphLongOperationManagerCustomInfoExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;

#nullable enable
namespace PX.Async;

[PXInternalUseOnly]
public static class GraphLongOperationManagerCustomInfoExtensions
{
  public static object? GetCustomInfoForThis(this IGraphLongOperationManager manager)
  {
    return manager.GetCustomInfoForThis((string) null, out object[] _);
  }

  public static object? GetCustomInfoForThis(
    this IGraphLongOperationManager manager,
    string? customInfoKey)
  {
    return manager.GetCustomInfoForThis(customInfoKey, out object[] _);
  }

  public static object? GetCustomInfoForThis(
    this IGraphLongOperationManager manager,
    out object[]? processingList)
  {
    return manager.GetCustomInfoForThis((string) null, out processingList);
  }
}
