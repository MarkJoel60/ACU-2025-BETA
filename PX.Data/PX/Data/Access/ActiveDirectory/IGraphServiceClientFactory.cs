// Decompiled with JetBrains decompiler
// Type: PX.Data.Access.ActiveDirectory.IGraphServiceClientFactory
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.Graph;

#nullable disable
namespace PX.Data.Access.ActiveDirectory;

/// <summary>
/// Provides ability to create <see cref="T:Microsoft.Graph.IGraphServiceClient" />
/// </summary>
internal interface IGraphServiceClientFactory
{
  /// <summary>
  /// Creates <see cref="T:Microsoft.Graph.IGraphServiceClient" /> based on the client credentials flow.
  /// </summary>
  /// <param name="configuration">The graph API client configuration.</param>
  /// <returns>
  /// <see cref="T:Microsoft.Graph.IGraphServiceClient" /> instance based on provided credentials.
  /// </returns>
  IGraphServiceClient CreateClient(GraphApiClientConfiguration configuration);
}
