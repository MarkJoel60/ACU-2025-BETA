// Decompiled with JetBrains decompiler
// Type: PX.Metadata.DacMetadata
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Autofac;
using CommonServiceLocator;
using PX.Common;
using System;
using System.Threading.Tasks;

#nullable enable
namespace PX.Metadata;

[PXInternalUseOnly]
public static class DacMetadata
{
  private static 
  #nullable disable
  Task _initializationCompleted;

  /// <summary>
  /// A static accessor for <see cref="P:PX.Metadata.IDacMetadataInitializer.InitializationCompleted" /> to use in places where DI is not available.
  /// Must not be used in any other places; inject <see cref="T:PX.Metadata.IDacMetadataInitializer" /> from the DI container instead.
  /// </summary>
  [PXInternalUseOnly]
  [Obsolete("Use PX.Metadata.IDacMetadataInitializer.InitializationCompleted from DI")]
  public static Task InitializationCompleted
  {
    get
    {
      if (DacMetadata._initializationCompleted != null)
        return DacMetadata._initializationCompleted;
      if (PXHostingEnvironment.IsHosted)
        throw new InvalidOperationException("DacMetadata.InitializationCompleted should have been initialized by now");
      return ServiceLocator.Current.GetInstance<IDacMetadataInitializer>().InitializationCompleted;
    }
  }

  private class ServiceRegistration : Module
  {
    protected virtual void Load(ContainerBuilder builder)
    {
      builder.RegisterBuildCallback((Action<ILifetimeScope>) (container => DacMetadata._initializationCompleted = ResolutionExtensions.Resolve<IDacMetadataInitializer>((IComponentContext) container).InitializationCompleted));
    }
  }
}
