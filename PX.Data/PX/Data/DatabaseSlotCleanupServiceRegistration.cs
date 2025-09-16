// Decompiled with JetBrains decompiler
// Type: PX.Data.DatabaseSlotCleanupServiceRegistration
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Autofac;
using Autofac.Builder;
using Autofac.Core;

#nullable enable
namespace PX.Data;

internal class DatabaseSlotCleanupServiceRegistration : Module
{
  protected virtual void Load(
  #nullable disable
  ContainerBuilder builder)
  {
    OptionsContainerBuilderExtensions.BindFromConfiguration<DatabaseSlotCleanupOptions>(builder);
    RegistrationExtensions.AutoActivate<DatabaseSlotCleanup, ConcreteReflectionActivatorData, SingleRegistrationStyle>(RegistrationExtensions.RegisterType<DatabaseSlotCleanup>(builder).SingleInstance()).OnActivated((System.Action<IActivatedEventArgs<DatabaseSlotCleanup>>) (args => PXDBSlotsCleanup.Impl = args.Instance));
  }
}
