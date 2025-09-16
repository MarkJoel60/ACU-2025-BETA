// Decompiled with JetBrains decompiler
// Type: PX.Data.SiteMapContainerBuilderExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Autofac;
using Autofac.Builder;
using PX.Common;

#nullable disable
namespace PX.Data;

[PXInternalUseOnly]
public static class SiteMapContainerBuilderExtensions
{
  [PXInternalUseOnly]
  public static IRegistrationBuilder<PXSiteMapProvider, ConcreteReflectionActivatorData, SingleRegistrationStyle> AddSiteMapProvider<TImpl>(
    this ContainerBuilder builder)
    where TImpl : PXSiteMapProvider
  {
    OptionsContainerBuilderExtensions.BindFromConfiguration<PXSiteMapOptions>(builder, new string[1]
    {
      "sitemap"
    });
    return (IRegistrationBuilder<PXSiteMapProvider, ConcreteReflectionActivatorData, SingleRegistrationStyle>) builder.RegisterAsDefaultAllowingSingleOverride<PXSiteMapProvider, TImpl>().SingleInstance();
  }

  internal static IRegistrationBuilder<PXSiteMapProvider, ConcreteReflectionActivatorData, SingleRegistrationStyle> AddWikiSiteMapProvider<TImpl>(
    this ContainerBuilder builder)
    where TImpl : PXWikiProvider
  {
    OptionsContainerBuilderExtensions.BindFromConfiguration<PXWikiSiteMapOptions>(builder, new string[2]
    {
      "wiki",
      "sitemap"
    });
    return (IRegistrationBuilder<PXSiteMapProvider, ConcreteReflectionActivatorData, SingleRegistrationStyle>) builder.RegisterAsDefaultAllowingSingleOverride<PXWikiProvider, TImpl>().SingleInstance();
  }
}
