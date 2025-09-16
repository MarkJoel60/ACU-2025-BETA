// Decompiled with JetBrains decompiler
// Type: PX.Data.Localization.ContainerBuilderExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Autofac;
using Autofac.Builder;
using PX.Common;
using PX.Data.Localization.Providers;

#nullable disable
namespace PX.Data.Localization;

[PXInternalUseOnly]
public static class ContainerBuilderExtensions
{
  /// <summary>
  /// Registers custom translation provider for strings localization
  /// </summary>
  /// <typeparam name="TTranslationProvider">
  /// A marker type used to provide custom provider implementation
  /// </typeparam>
  /// <returns></returns>
  [PXInternalUseOnly]
  public static IRegistrationBuilder<TTranslationProvider, ConcreteReflectionActivatorData, SingleRegistrationStyle> RegisterTranslationProvider<TTranslationProvider>(
    this ContainerBuilder builder)
    where TTranslationProvider : IPXTranslationProvider
  {
    return RegistrationExtensions.RegisterType<TTranslationProvider>(builder).As<IPXTranslationProvider>().SingleInstance();
  }
}
