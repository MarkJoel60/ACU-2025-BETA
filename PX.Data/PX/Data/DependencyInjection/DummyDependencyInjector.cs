// Decompiled with JetBrains decompiler
// Type: PX.Data.DependencyInjection.DummyDependencyInjector
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.DependencyInjection;

public class DummyDependencyInjector : IDependencyInjector
{
  public void InjectDependencies(PXGraph graph, System.Type graphType, string prefix)
  {
  }

  public void InjectDependencies(PXAction action)
  {
  }

  public void InjectDependencies(PXEventSubscriberAttribute attribute, PXCache cache)
  {
  }

  public void InjectDependencies(PXSelectBase indexer)
  {
  }
}
