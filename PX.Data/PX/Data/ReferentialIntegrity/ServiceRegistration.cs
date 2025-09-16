// Decompiled with JetBrains decompiler
// Type: PX.Data.ReferentialIntegrity.ServiceRegistration
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Autofac;
using Autofac.Builder;
using PX.Data.ReferentialIntegrity.DacRelations;
using PX.Data.ReferentialIntegrity.Inspecting;
using PX.Data.ReferentialIntegrity.Merging;

#nullable disable
namespace PX.Data.ReferentialIntegrity;

public class ServiceRegistration : Module
{
  protected virtual void Load(ContainerBuilder builder)
  {
    RegistrationExtensions.RegisterType<TableReferenceProcessor>(builder).As<ITableReferenceCollector>().As<ITableReferenceInspector>().SingleInstance();
    RegistrationExtensions.RegisterType<ReferenceMerger>(builder).As<IReferenceMerger>().SingleInstance();
    RegistrationExtensions.RegisterType<TableMergedReferencesInspector>(builder).As<ITableMergedReferencesInspector>().SingleInstance();
    RegistrationExtensions.AsSelf<SelectorToReferenceConverter, ConcreteReflectionActivatorData>(RegistrationExtensions.RegisterType<SelectorToReferenceConverter>(builder)).SingleInstance();
    OptionsContainerBuilderExtensions.BindFromConfiguration<GraphViewReferenceAnalyzerOptions>(builder, new string[2]
    {
      "metadata",
      "dac"
    });
    RegistrationExtensions.AsSelf<GraphViewReferenceAnalyzer, ConcreteReflectionActivatorData>(RegistrationExtensions.RegisterType<GraphViewReferenceAnalyzer>(builder)).SingleInstance();
    RegistrationExtensions.RegisterType<DacRelationService>(builder).As<IDacRelationService>().SingleInstance();
  }
}
