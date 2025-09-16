// Decompiled with JetBrains decompiler
// Type: PX.Data.DependencyInjection.IDependencyInjector
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.DependencyInjection;

/// <exclude />
public interface IDependencyInjector
{
  void InjectDependencies(PXGraph graph, System.Type graphType, string prefix);

  void InjectDependencies(PXAction action);

  void InjectDependencies(PXEventSubscriberAttribute attribute, PXCache cache);

  void InjectDependencies(PXSelectBase indexer);
}
