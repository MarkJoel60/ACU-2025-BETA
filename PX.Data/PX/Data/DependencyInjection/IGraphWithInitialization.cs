// Decompiled with JetBrains decompiler
// Type: PX.Data.DependencyInjection.IGraphWithInitialization
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.DependencyInjection;

/// <summary>The interface that is used only when you inject dependencies in a graph with <tt>InjectDependencyAttribute</tt> and you need to initialize the graph by using
/// the dependencies.</summary>
public interface IGraphWithInitialization
{
  /// <summary>Performs initialization of the graph that implements the interface.</summary>
  void Initialize();
}
